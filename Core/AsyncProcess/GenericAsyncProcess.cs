using System;
using System.Threading;
using System.Windows.Forms;

namespace EBookMan
{
    public class GenericAsyncProcess : IAsyncProcess, IAsyncProcessHost
    {
        #region IAsyncProcess Members

        void IAsyncProcess.ReportHostReady()
        {
            this.startEvent.Set();
        }


        void IAsyncProcess.Cancel()
        {
            this.cancelled = true;
        }


        bool IAsyncProcess.IsFinished
        {
            get { return this.finished; }
        }


        event ProgressChangeHandler IAsyncProcess.ProgressChanged
        {
            add { this.progressChanged += value; }
            remove { this.progressChanged -= value; }
        }


        event StatusChangedHandler IAsyncProcess.Finished
        {
            add { this.finishChanged += value; }
            remove { this.finishChanged -= value; }
        }


        event StatusChangedHandler IAsyncProcess.ErrorReported
        {
            add { this.errorReported += value; }
            remove { this.errorReported -= value; }
        }


        event StatusChangedHandler IAsyncProcess.StepChanged
        {
            add { this.stepChanged += value; }
            remove { this.stepChanged -= value; }
        }


        event UserInputNeededHandler IAsyncProcess.InputNeeded
        {
            add { this.inputNeeded += value; }
            remove { this.inputNeeded -= value; }
        }

        #endregion

        #region IAsyncProcessHost Members

        bool IAsyncProcessHost.IsCancelled
        {
            get { return this.cancelled; }
        }


        void IAsyncProcessHost.ReportProgress(int percent, string prompt)
        {
            this.finished = false;
            ProgressChangeHandler h = this.progressChanged;
            if (h != null) h((end - start) * percent / 100 + start, prompt);
        }


        void IAsyncProcessHost.ReportError(string prompt)
        {
            StatusChangedHandler h = this.errorReported;
            if (h != null) h(prompt);
        }


        void IAsyncProcessHost.StartStep(string prompt)
        {
            this.finished = false;
            this.start = 0;
            this.end = 100;
            StatusChangedHandler h = this.stepChanged;
            if (h != null) h(prompt);
        }


        void IAsyncProcessHost.Finish (string prompt)
        {
            this.finished = true;
            StatusChangedHandler h = this.finishChanged;
            if (h != null) h(prompt);
        }


        void IAsyncProcessHost.WaitForHost()
        {
            this.startEvent.WaitOne();
            this.finished = false;
        }


        DialogResult IAsyncProcessHost.AskInput(Type formType, params object[] args)
        {
            this.finished = false;
            UserInputNeededHandler h = this.inputNeeded;

            DialogResult result = DialogResult.None;
            if (h != null) h(formType, out result, args);

            return result;
        }


        void IAsyncProcessHost.SetInterval(int start, int end)
        {
            if ( start < 0 || start > 100 || end < 0 || end > 100 )
                throw new ArgumentOutOfRangeException();

            if ( start >= end )
                throw new ArgumentException("start cannot be greater or equal then end");

            this.start = start;
            this.end = end;
        }

        #endregion

        #region private members

        private ManualResetEvent startEvent = new ManualResetEvent(false);

        private bool cancelled;
        private bool finished;

        private int start = 0;
        private int end = 100;

        private ProgressChangeHandler progressChanged;
        private StatusChangedHandler errorReported;
        private StatusChangedHandler finishChanged;
        private StatusChangedHandler stepChanged;
        private UserInputNeededHandler inputNeeded;

        #endregion
    }
}
