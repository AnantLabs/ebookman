using System;
using System.Windows.Forms;
using System.Xml;

namespace EBookMan
{
    public interface IPlugin : IDisposable
    {
        void RegisterMainWindowUI(Form mainWindow);

        void RegisterOptionsUI(Form optionDialog);
    }
}
