using System;

namespace EBookMan
{
    public interface IDataProvider
    {
        string Name
        {
            get;
        }

        bool CanHandle(string url);

        bool DoesRequireAuthentication
        {
            get;
        }

        string User
        {
            get;
        }

        string Password
        {
            get;
        }

        bool Update(Book book);

        bool CreateBook(string url, out Book book, IAsyncProcessHost progress);
    }
}
