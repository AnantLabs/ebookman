using System;

namespace EBookMan
{
    public class BookFile
    {
        public BookFile(string path, Guid formatGuid)
        {
            this.path = path;
            this.formatId = formatGuid;
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

        private string path;
        private Guid formatId;
    }
}
