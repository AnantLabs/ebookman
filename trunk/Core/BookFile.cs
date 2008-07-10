using System;

namespace EBookMan
{
    public class BookFile
    {
        public BookFile(string path, Guid formatGuid)
        {
            this.path = path;
            this.formatId = formatGuid;
            this.bookId = -1;
        }

        public string Path
        {
            get { return this.path; }
            set { this.path = value; }
        }

        public Guid FormatId
        {
            get { return this.formatId; }
        }

        public Int32 BookId
        {
            get { return this.bookId; }
            set { this.bookId = value; }
        }

        private string path;
        private Guid formatId;
        private Int32 bookId;
    }
}
