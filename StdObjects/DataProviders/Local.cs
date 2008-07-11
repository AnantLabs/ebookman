using System;
using System.IO;

namespace EBookMan
{
    public class LocalDataProvider : IDataProvider
    {
        #region IDataProvider Members

        public string Name
        {
            get { return Properties.Resource.NameLocal; }
        }


        public bool CanHandle(string url)
        {
            if ( url.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) )
                return false;

            return File.Exists(url);
        }


        public bool DoesRequireAuthentication
        {
            get { return false; }
        }


        public string User
        {
            get { return null;; }
        }


        public string Password
        {
            get { return null; ; }
        }


        public bool Update(Book book)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public bool CreateBook (string url, out Book book, out BookFile file, IAsyncProcessHost progress)
        {
            book = null;
            file = null;

            IBookFormat format = DataManager.Instance.GetFormatByPath(url);
            if ( format == null )
                return false;

            if ( ! format.GetMetaData(url, out book, progress) )
                return false; // could not get enough metadata;

            file = new BookFile(url, format.Guid);

            return true;
        }

        #endregion
    }
}
