using System;
using System.Drawing;
using System.Collections.Generic;

namespace EBookMan
{
    public interface ILibrary : IEnumerable<Book> 
    {
        void Add(Book book, IAsyncProcessHost progress);    // progress percent 50%-100%

        Filter Filter
        {
            get;
            set;
        }

        List<string> GetLanguages();

        List<string> GetAvailableTags();

        event EventHandler FilterChanged;
    }
}
