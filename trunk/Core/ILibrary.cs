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

        void Add(Book book, IAsyncProcessHost progress);

        List<string> GetLanguages();

        List<string> GetAvailableTags();

        IEnumerator<Book> GetEnumerator(Filter filter);
    }
}
