using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace EBookMan
{
    sealed class MdbEnumerator : IEnumerator<Book>
    {
        public MdbEnumerator(string query, OleDbConnection connection)
        {
            this.query = query;
            this.connection = connection;
        }


        public Book Current
        {
            get
            {
                if ( this.reader == null || !this.reader.HasRows )
                    throw new InvalidOperationException();

                return ( this.cachedBook == null ) ? BuildBook() : this.cachedBook;
            }
        }


        public void Dispose()
        {
            if ( this.reader != null )
            {
                this.reader.Close();
                this.reader = null;
                this.connection = null;
                this.cachedBook = null;
            }
        }


        object System.Collections.IEnumerator.Current
        {
            get { return this.Current; }
        }


        public bool MoveNext()
        {
            if ( this.reader == null )
                throw new InvalidOperationException();

            this.cachedBook = null;

            return this.reader.Read();
        }


        public void Reset()
        {
            if ( this.reader != null )
                this.reader.Close();

            this.cachedBook = null;

            OleDbCommand command = new OleDbCommand(query, connection);
            this.reader = command.ExecuteReader();
        }


        private Book BuildBook ()
        {
            // get common fields

            Book book = new Book();
            book.ID = this.reader[ "ID" ].ToString();
            book.Title = this.reader[ "Title" ].ToString(); ;
            book.Authors = this.reader[ "Authors" ].ToString(); ;
            book.SeriesName = this.reader[ "SeriesName" ].ToString();
            book.SeriesNum = (byte)this.reader[ "SeriesNum" ];
            book.Rating = (byte)this.reader["Rating"];
            book.CoverPath = this.reader[ "Cover" ].ToString();
            book.Annotation = this.reader[ "Annotation" ].ToString();
            book.Isbn = this.reader[ "Isbn" ].ToString();
            book.Language = this.reader[ "BookLanguage" ].ToString();


            // get tags

            OleDbCommand cmdTags = new OleDbCommand(
                string.Format("SELECT Tags.Name FROM Tags INNER JOIN lkpBookTag ON Tags.ID=lkpBookTag.TagId WHERE lkpBookTag.BookId={0}", book.ID),
                this.connection);

            OleDbDataReader readerTags = cmdTags.ExecuteReader();
            if ( readerTags.HasRows )
                while ( readerTags.Read() )
                    book.AddTag(readerTags.GetString(0));

            readerTags.Close();


            // get files

            OleDbCommand cmdFiles = new OleDbCommand(
                string.Format("SELECT * FROM Files WHERE BookId={0}", book.ID),
                this.connection);

            OleDbDataReader readerFiles = cmdFiles.ExecuteReader();
            if ( readerFiles.HasRows )
            {
                List<BookFile> files = new List<BookFile>();
                while ( readerFiles.Read() )
                    files.Add(new BookFile(
                        readerFiles[ "Path" ].ToString(), 
                        (Guid)readerFiles[ "FormatGuid" ], 
                        (DateTime)readerFiles[ "AddDate" ]));

                book.Files = files.ToArray();
            }

            readerFiles.Close();

            return book;
        }

        private OleDbDataReader reader;
        private OleDbConnection connection;
        private string query;
        private Book cachedBook;
    }
}
