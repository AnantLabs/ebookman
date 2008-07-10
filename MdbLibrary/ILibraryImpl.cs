using System;
using System.Windows.Forms;
using System.Xml;
using System.Data.OleDb;
using System.Text;
using System.IO;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Zip;

namespace EBookMan
{
    partial class MdbLibrary
    {
        #region ILibrary Members

        // TODO: remove?
        //public bool Find(FieldFilter criteria, HandleBookDelegate handleBookDelegate)
        //{
        //    StringBuilder query = new StringBuilder("SELECT * FROM Books");
        //    if ( criteria != null )
        //    {
        //        query.Append(" WHERE ");

        //        if ( !string.IsNullOrEmpty(criteria.Text) )
        //        {
        //            if ( ( criteria.TextSearchLocation & TextSearchLocation.Authors ) == TextSearchLocation.Authors )
        //                query.Append(string.Format(" (Authors CONTAINS {0})", criteria.Text));
        //        }
        //    }

        //    try
        //    {
        //        OleDbCommand command = new OleDbCommand(query.ToString(), this.connection);
        //        OleDbDataReader reader = command.ExecuteReader();

        //        if ( !reader.HasRows )
        //            return false;

        //        while ( reader.Read() )
        //        {
        //            System.Diagnostics.Debug.WriteLine(reader[ "Title" ]);
        //            System.Diagnostics.Debug.WriteLine(reader[ "Authors" ]);
        //            System.Diagnostics.Debug.WriteLine(reader[ "Series" ]);
        //        }

        //        reader.Close();
        //        return true;
        //    }

        //    catch ( OleDbException )
        //    {
        //        return false;
        //    }
        //}


        public void Add(Book book, IAsyncProcessHost progress)
        {
            if (book == null || ! book.IsValid())
                throw new ArgumentException("Bad or invalid book");


            // check for duplicates

            if (progress != null) progress.ReportProgress(0, Properties.Resources.PromptCheckingDuplicates);

            Int32 bookId = GetRecordID("SELECT ID FROM Books WHERE Title='{0}' AND Authors='{1}'", book.Title, book.Authors);
            if (bookId != -1)
            {
                // TODO: handle update!!!
                return;
            }


            bool moveFiles = Properties.Settings.Default.MoveFiles;
            string bookFolder = GetBookFolder(book);


            // save the cover page with the book file

            if (progress != null) progress.ReportProgress(10, Properties.Resources.PromptAddingBook);

            if (book.CoverImage != null )
            {
                // if we have somehow generated image, it overwrites the existing cover
                // we need to save it either in the automatically generated folder
                // if "MoveFiles" option is true or together with the file

                book.CoverPath = IOHelper.GetUniqueFileName(bookFolder + "cover.png");
                try
                {
                    book.CoverImage.Save(book.CoverPath, System.Drawing.Imaging.ImageFormat.Png);
                }

                catch (IOException)
                {
                    book.CoverPath = null; // drop cover
                }

                book.CoverImage.Dispose();
                book.CoverImage = null;
            }
            else
            {
                // if the cover is specified by the path and the "MoveFiles" options is ON
                // and the cover is located somewhere - move it to the designated book folder

                if (moveFiles && ! string.IsNullOrEmpty(book.CoverPath))
                    book.CoverPath = MoveToFolder(false, bookFolder, book.CoverPath, progress);
            }


            if ( progress != null ) progress.ReportProgress(20, null);


            // add book to the database

            bookId = InsertRecord(
                "INSERT INTO Books(Title,Authors,SeriesName,SeriesNum,Rating,Cover,Annotation,Isbn,BookLanguage) " +
                "VALUES ('{0}','{1}','{2}',{3},{4},'{5}','{6}','{7}','{8}')",
                NormalizeString(book.Title, 128), NormalizeString(book.Authors, 128), NormalizeString(book.SeriesName, 128),
                book.SeriesNum, book.Rating, book.CoverPath,
                NormalizeString(book.Annotation, 65553), NormalizeString(book.Isbn, 16), NormalizeString(book.Language, 16));

            if (bookId == -1)
            {
                if ( progress != null ) progress.ReportError(string.Format(Properties.Resources.ErrorAddingBookFailed, book.Title));
                return;
            }


            // force update available language list and tag list

            this.tags = null;
            this.languanges = null;


            // add files to the database

            bool compress = Properties.Settings.Default.CompressFiles;

            if ( book.Files != null )
            {
                int percent = 30;
                int percent_inc = ( book.Files.Length > 0 ) ? 60 / book.Files.Length : 60;

                for ( int i = 0 ; i < book.Files.Length ; i++ )
                {
                    if ( progress != null ) 
                        progress.ReportProgress(percent, string.Format(Properties.Resources.PromptAddingFiles, book.Files[i].Path));

                    percent += percent_inc;

                    if ( moveFiles )
                        book.Files[i].Path = MoveToFolder(compress, bookFolder, book.Files[ i ].Path, progress);

                    book.Files[ i ].BookId = bookId;

                    Int32 fileId = InsertRecord(
                        "INSERT INTO Files(Path,FormatGuid,BookId,AddDate) VALUES('{0}','{1}',{2},Date())",
                        NormalizeString(book.Files[ i ].Path, 255), 
                        NormalizeString(book.Files[ i ].FormatId.ToString(), 36), 
                        bookId);

                    if (fileId == -1)
                    {
                        if ( progress != null ) progress.ReportError(string.Format(Properties.Resources.ErrorAddFileFailed, book.Files[i].Path));
                    }
                }
            }   


            // add tags to the database

            if ( progress != null ) progress.ReportProgress(95, Properties.Resources.PromptUpdatingTags);

            if ( book.Tags != null )
            {
                foreach ( string tag in book.Tags )
                {
                    if ( string.IsNullOrEmpty(tag) )
                        continue;

                    Int32 tagId = GetTagId(tag); // will create if does not exist

                    if ( tagId == -1 )
                        continue;

                    if (InsertRecord("INSERT INTO lkpBookTag(BookId, TagId) VALUES({0},{1})", bookId, tagId) == -1)
                        if ( progress != null ) progress.ReportError(Properties.Resources.ErrorAddTagFailed);
                }
            }
        }


        public Filter Filter
        {
            get { throw new NotImplementedException(); }
            set
            {
                EventHandler h = this.FilterChanged;
                if ( h != null ) h(this, EventArgs.Empty);
                throw new NotImplementedException();
            }
        }


        public event EventHandler FilterChanged;


        public List<string> GetLanguages()
        {
            if ( this.languanges == null )
                this.languanges = GetStrings("SELECT DISTINCT BookLanguage FROM Books ORDER BY BookLanguage");

            return this.languanges;
        }


        public List<string> GetAvailableTags()
        {
            if ( this.tags == null )
                this.tags = GetStrings("SELECT Name FROM Tags ORDER BY Name");

            return this.tags;
        }

        #endregion

        #region Helpers

        private string MoveToFolder(bool compress, string destFolder, string file, IAsyncProcessHost progress)
        {
            string filename = Path.GetFileName(file);


            // if the file can be and should be compressed - compress it directly to a new folder

            if ( compress )
            {
                string extension = Path.GetExtension(file);
                if ( this.compressable.ContainsKey(extension.ToLower()) )
                {
                    ZipOutputStream output = null;
                    FileStream source = null;
                    bool success = false;

                    string dest = IOHelper.GetUniqueFileName(string.Format("{0}{1}.zip", destFolder, filename));

                    try
                    {
                        output = new ZipOutputStream(File.Create(dest));
                        output.IsStreamOwner = true;
                        output.PutNextEntry(new ZipEntry(filename));

                        source = new FileStream(file, FileMode.Open);
                        byte[] buffer = new byte[1024*200];
                        int read;

                        while ((read = source.Read(buffer, 0, buffer.Length)) > 0)
                            output.Write(buffer, 0, read);

                        success = true;
                    }

                    catch
                    {
                        if ( progress != null ) progress.ReportError(string.Format(Properties.Resources.ErrorZipFileFailed, file));
                    }

                    finally
                    {
                        if ( output != null )
                        {
                            output.Close();
                            output.Dispose();
                        }

                        if (source != null)
                        {
                            source.Close();
                            source.Dispose();
                        }
                    }

                    if ( success )
                    {
                        try
                        {
                            File.Delete(file);
                        }
                        catch
                        { }

                        return dest;
                    }
                }
            }


            // file is already compressed or should not be compressed
            // move it if it is not in the desitnation folder already

            string currFolder = IOHelper.CompletePath(Path.GetDirectoryName(file));

            if ( string.Compare(destFolder, currFolder, true) != 0 )
            {
                string dest = IOHelper.GetUniqueFileName(destFolder + filename);

                try
                {
                    File.Move(file, dest);
                    return dest;
                }

                catch
                {
                    if ( progress != null ) progress.ReportError(string.Format(Properties.Resources.ErrorMoveFileFailed, file));
                    return file;
                }
            }

            return file;
        }


        private int InsertRecord (string format, params object[] args)
        {
            try
            {
                OleDbCommand command = new OleDbCommand(string.Format(format, args), this.connection);
                command.ExecuteNonQuery();

                command = new OleDbCommand("SELECT @@IDENTITY", this.connection);
                object o = command.ExecuteScalar();
                return ( o != null ) ? ( Int32 ) o : -1;
            }

            catch ( InvalidOperationException )
            {
                return -1;
            }

            catch ( OleDbException )
            {
                return -1;
            }
        }


        private Int32 GetRecordID(string format, params object[] args)
        {
            OleDbDataReader reader = null;

            try
            {
                OleDbCommand command = new OleDbCommand(string.Format(format, args), this.connection);
                reader = command.ExecuteReader();

                if ( !reader.HasRows )
                    return -1;

                reader.Read();
                return ( Int32 ) reader[ "ID" ];
            }

            catch ( InvalidOperationException )
            {
                return -1;
            }

            catch ( OleDbException )
            {
                return -1;
            }

            finally
            {
                if ( reader != null )
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
        }


        private List<string> GetStrings(string format, params object[] args)
        {
            OleDbDataReader reader = null;

            try
            {
                OleDbCommand command = new OleDbCommand(string.Format(format, args), this.connection);
                reader = command.ExecuteReader();

                if ( !reader.HasRows )
                    return null;

                List<string> list = new List<string>();

                while (reader.Read())
                    list.Add(reader.GetString(0));

                return list;
            }

            catch ( InvalidOperationException )
            {
                return null;
            }

            catch ( OleDbException )
            {
                return null;
            }

            finally
            {
                if ( reader != null )
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
        }


        private string GetBookFolder(Book book)
        {
            if ( book == null || !book.IsValid() )
                return ".\\";

            if ( Properties.Settings.Default.MoveFiles )
            {
                string author;

                if ( string.IsNullOrEmpty(book.Authors) )
                    author = Properties.Resources.UnknownAuthor;
                else
                {
                    string[] a = book.Authors.Split(',');
                    author = ( a == null || a.Length == 0 ) ? Properties.Resources.UnknownAuthor : IOHelper.Normalize(a[ 0 ].Trim());
                }

                return ( string.IsNullOrEmpty(book.SeriesName) )
                    ? string.Format("{0}{1}\\{2}\\", Properties.Settings.Default.DbRoot, author, IOHelper.Normalize(book.Title))
                    : string.Format("{0}{1}\\{2}\\{3}\\", Properties.Settings.Default.DbRoot, author, IOHelper.Normalize(book.SeriesName), IOHelper.Normalize(book.Title));
            }
            else
            {
                if ( book.Files != null && book.Files.Length > 0 && !string.IsNullOrEmpty(book.Files[ 0 ].Path) )
                {
                    return IOHelper.CompletePath(Path.GetDirectoryName(book.Files[ 0 ].Path));
                }
                else
                {
                    return ".\\";
                }
            }
        }


        private Int32 GetTagId(string tag)
        {
            tag = NormalizeString(tag.ToUpper(), 50);

            string format = "SELECT ID From Tags WHERE Name='{0}'";

            Int32 id = GetRecordID(format, tag);
            if ( id != -1 )
                return id;

            return InsertRecord("INSERT INTO Tags(Name) VALUES('{0}')", NormalizeString(tag, 50));
        }


        private string NormalizeString(string source, int maxLen)
        {
            if ( string.IsNullOrEmpty(source) )
                return null;

            if ( source.Length > maxLen )
                source = source.Substring(0, maxLen);

            return ( source.LastIndexOf('\'') != -1 ) ? source.Replace('\'', '"') : source;
        }


        #endregion
    }
}
