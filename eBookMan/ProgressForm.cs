using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EBookMan
{
    public partial class ProgressForm : Form
    {
        public static void Show(string title, IAsyncProcess process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            // give a chance for a short process to finish 
            // without showing the window

            Application.DoEvents();
            if ( process.IsFinished )
                return;


            // create the form, but check if the process
            // has finished while creating the form

            ProgressForm form = new ProgressForm(title, process);
            if ( process.IsFinished )
                return;


            // subscrive to all process evenrs

            process.ErrorReported += form.errorHandler;
            process.Finished += form.finishHandler;
            process.InputNeeded += form.inputNeededHandler;
            process.ProgressChanged += form.progressHandler;
            process.StepChanged += form.stepHandler;


            // show the form. it will close automaticall
            // when the process is done

            form.ShowDialog();


            // unsubscribe from process events

            process.ErrorReported -= form.errorHandler;
            process.Finished -= form.finishHandler;
            process.InputNeeded -= form.inputNeededHandler;
            process.ProgressChanged -= form.progressHandler;
            process.StepChanged -= form.stepHandler;

            return;
        }

        private ProgressForm(string title, IAsyncProcess process)
        {
            if ( process == null )
                throw new ArgumentNullException("process");

            InitializeComponent();

            this.errorHandler = new StatusChangedHandler(OnErrorReported);
            this.finishHandler = new StatusChangedHandler(OnFinishReported);
            this.stepHandler = new StatusChangedHandler(OnStepChange);
            this.progressHandler = new ProgressChangeHandler(OnProgressReported);
            this.inputNeededHandler = new UserInputNeededHandler(OnInputNeeded);

            this.Text = title;


            // show in the compact mode

            this.fullHeight = this.Height;
            this.compactHeight = this.txtErrors.Top - 1; 
            this.ClientSize = new Size(this.ClientSize.Width, this.compactHeight);


            // signal that the UI is ready

            this.process = process;
            this.process.ReportHostReady();
        }


        #region AsyncProcess event handlers

        private void OnErrorReported(string prompt)
        {
            if ( this.InvokeRequired )
            {
                this.BeginInvoke(this.errorHandler, prompt);
                return;
            }

            if (! string.IsNullOrEmpty(this.txtErrors.Text))
                this.txtErrors.Text += "\r\n";

            this.txtErrors.Text += prompt;

            if ( !this.btnMore.Visible )
                this.btnMore.Visible = true;
        }


        private void OnFinishReported(string prompt)
        {
            if ( this.InvokeRequired )
            {
                this.BeginInvoke(this.finishHandler, prompt);
                return;
            }


            // if there is a prompt supplied by a user or any error message
            // show the message and replace the cancel button with the "close" button
            // otherwise just close the window

            if ( ! string.IsNullOrEmpty(this.txtErrors.Text) )
            {
                this.lblProgress.Text = Properties.Resources.ErrorsFound;
                this.lblProgress.ForeColor = Color.Maroon;
            }
            else
            {
                if ( !string.IsNullOrEmpty(prompt) )
                {
                    this.lblProgress.Text = prompt;
                }
                else
                {
                    Close();
                    return;
                }
            }

            this.progressBar.Hide();
            this.lblStep.Hide();
            this.btnOK.Text = Properties.Resources.ButtonClose;
            this.ControlBox = true;
        }


        private void OnStepChange(string prompt)
        {
            if ( this.InvokeRequired )
            {
                this.BeginInvoke(this.stepHandler, prompt);
                return;
            }

            this.lblStep.Text = prompt;
            this.lblStep.ForeColor = SystemColors.WindowText;
            this.progressBar.Value = 0;
        }


        private void OnProgressReported(int percent, string prompt)
        {
            if ( this.InvokeRequired )
            {
                this.BeginInvoke(this.progressHandler, percent, prompt);
                return;
            }

            this.lblProgress.Text = prompt;
            this.progressBar.Value = percent;
        }


        private void OnInputNeeded(Type type, out DialogResult result, params object[] args)
        {
            // TODO: implement

            //if ( this.InvokeRequired )
            //{
            //    this.BeginInvoke(this.inputNeededHandler, type, result, args);
            //    return;
            //}

            result = DialogResult.None;

            //Form form = Activator.CreateInstance(type, args) as Form;
            //if (form == null)
            //    return;

            //result =  form.ShowDialog();
            //return;
        }

        #endregion

        #region UI event handlers

        private void OnMoreClick(object sender, EventArgs e)
        {
            this.SuspendLayout();

            if ( this.ClientSize.Height <= this.compactHeight)
            {
                this.ClientSize = new Size(this.ClientSize.Width, this.fullHeight);
                this.btnMore.Text = Properties.Resources.ButtonLess;
                this.txtErrors.Visible = true;
            }
            else
            {
                this.ClientSize = new Size(this.ClientSize.Width, this.compactHeight);
                this.txtErrors.Visible = false;
                this.btnMore.Text = Properties.Resources.ButtonMore;
            }

            this.ResumeLayout();
        }


        private void OnOkClick(object sender, EventArgs e)
        {
            if ( this.process.IsFinished )
            {
                // works as close button

                Close();
            }
            else
            {
                // cancels the process

                this.process.Cancel();
                this.lblStep.Text = Properties.Resources.PromptCanceling;
                this.progressBar.Value = 100;
            }            
        }

        #endregion

        #region private members

        private StatusChangedHandler errorHandler;
        private StatusChangedHandler finishHandler;
        private StatusChangedHandler stepHandler;
        private ProgressChangeHandler progressHandler;
        private UserInputNeededHandler inputNeededHandler;

        private IAsyncProcess process;

        private int compactHeight;
        private int fullHeight;

        #endregion
    }
}