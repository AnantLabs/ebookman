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
            book.CoverPath = this.reader[ "Cover" ].ToString();
            book.Annotation = this.reader[ "Annotation" ].ToString();
            book.Isbn = this.reader[ "Isbn" ].ToString();
            book.Language = this.reader[ "BookLanguage" ].ToString();
            book.SeriesName = this.reader[ "SeriesName" ].ToString();

            object obj = this.reader[ "SeriesNum" ];
            book.SeriesNum = ( obj is System.DBNull ) ? (byte) 0 : (byte) obj;

            obj = this.reader[ "Rating" ];
            book.Rating = ( obj is System.DBNull ) ? (byte) 0 : ( byte ) obj;

            obj = this.reader[ "Position" ];
            book.Position = ( obj is System.DBNull ) ? ( float ) 0.0 : ( float ) obj;


            // get tags

            OleDbCommand cmdTags = new OleDbCommand(
                string.Format("SELECT DISTINCT Tags.Name FROM Tags INNER JOIN lkpBookTag ON Tags.ID=lkpBookTag.TagId WHERE lkpBookTag.BookId={0}", book.ID),
                this.connection);

            OleDbDataReader readerTags = cmdTags.ExecuteReader();
            if ( readerTags.HasRows )
                while ( readerTags.Read() )
                    book.Tags.Add(readerTags.GetString(0));

            readerTags.Close();

            return book;
        }

        private OleDbDataReader reader;
        private OleDbConnection connection;
        private string query;
        private Book cachedBook;
    }
}
