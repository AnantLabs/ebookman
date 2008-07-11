using System;
using System.Drawing;
using System.Collections.Generic;

namespace EBookMan
{
    public interface ILibrary
    {
        Guid Guid
        {
            get;
        }

        bool AddBook(Book book, IAsyncProcessHost progress);

        bool AddFiles(Book book, BookFile[] files, IAsyncProcessHost progress);

        List<string> GetLanguages();

        List<string> GetAvailableTags();

        IEnumerator<Book> GetEnumerator(Filter filter);
    }
}
