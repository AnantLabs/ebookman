using System;
using System.Text;
using System.Windows.Forms;

namespace EBookMan
{
    public delegate void UserInputNeededHandler (Type t, out DialogResult result, params object[] o);

    public delegate void StatusChangedHandler (string prompt);

    public delegate void ProgressChangeHandler (int percent, string prompt);


    public interface IAsyncProcess
    {
        void ReportHostReady();

        void Cancel();

        bool IsFinished
        {
            get;
        }

        event ProgressChangeHandler ProgressChanged;

        event StatusChangedHandler Finished;

        event StatusChangedHandler ErrorReported;

        event StatusChangedHandler StepChanged;

        event UserInputNeededHandler InputNeeded;
    }
}
