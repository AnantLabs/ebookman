using System;
using System.Windows.Forms;

namespace EBookMan
{
    public interface IAsyncProcessHost
    {
        /// <summary>
        /// Returns true if user has requested to cancel the process
        /// </summary>
        /// <returns></returns>
        bool IsCancelled
        {
            get;
        }


        /// <summary>
        /// Advances progressbar to one step and shows the prompt
        /// If the prompt is null, the ui will remain the same
        /// The percentage will be calculated based on the values
        /// set by the SetInterval function
        /// </summary>
        void ReportProgress(int percent, string prompt);


        /// <summary>
        /// Reports error to the host
        /// </summary>
        void ReportError(string prompt);


        /// <summary>
        /// Reports end of the operation
        /// </summary>
        void Finish(string comment);


        /// <summary>
        /// Starts new step, resets the progressbar to 0
        /// </summary>
        void StartStep(string prompt);


        /// <summary>
        /// Waits unit the host is ready to show the progress
        /// </summary>
        void WaitForHost();


        /// <summary>
        /// Set the relative weight for the next progress reports
        /// so all the following calls percentage 0-100% will be 
        /// compressed to the specified interval
        /// </summary>
        /// <param name="weight"></param>
        void SetInterval(int start, int end);


        /// <summary>
        /// Allows to ask for user input
        /// </summary>
        /// <param name="formType">Type of the form to be shown using reflection</param>
        /// <param name="args">Arguments for the form constructor</param>
        /// <returns>Results of the form</returns>
        DialogResult AskInput(Type formType, params object[] args);
    }
}
