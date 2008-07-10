using System;
using System.Drawing;

namespace EBookMan
{
    public delegate void HandleBookDelegate (Book book);

    public interface ILibrary
    {
        bool Find(FilterCriteria criteria, HandleBookDelegate handleBookDelegate);

        void Add(Book book, IAsyncProcessHost progress);    // progress percent 50%-100%

        bool Delete(Book book);

        string[] GetLanguages();

        string[] GetTags();
    }
}
