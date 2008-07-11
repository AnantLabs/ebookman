using System;
using System.Drawing;
using System.Windows.Forms;

namespace EBookMan
{
    public interface IViewer
    {
        string Name
        {
            get;
        }

        Image Icon
        {
            get;
        }

        Control Control
        {
            get;
        }

        void BeginUpdate();

        void Add(Book book);

        void EndUpdate();

        void Clear();
    }
}
