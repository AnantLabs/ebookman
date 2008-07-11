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

        Guid Guid
        {
            get;
        }

        void Fill(ILibrary library, Filter filter);

        void Clear();
    }
}
