using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EBookMan
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            DataManager.Instance.MainWindow = this;
            DataManager.Instance.DataChange += new EventHandler(OnDataChange);
            if ( DataManager.Instance.ActiveLibrary != null )
                OnDataChange(this, EventArgs.Empty);
        }

        #region system event handlers

        private void OnDataChange(object sender, EventArgs e)
        {
            //if ( this.InvokeRequired )
            //{
            //    this.BeginInvoke(new EventHandler(OnActiveLibraryChange));
            //    return;
            //}

            //// TODO: implement
        }

        #endregion

        #region UI event handlers

        private void OnAddUrl(object sender, EventArgs e)
        {
            // TODO:implement real functionality

            //GenericAsyncProcess process = new GenericAsyncProcess();

            //MethodInvoker action = delegate
            //{
            //    IAsyncProcessHost progress = (process as IAsyncProcessHost);

            //    for (int i=0; i<100; i++)
            //    {
            //        if ((i%15)==0)
            //        {
            //            progress.StartStep(string.Format("step number {0}", i));
            //        }

            //        System.Threading.Thread.Sleep(100);
            //        if (progress.IsCancelled)
            //            break;

            //        progress.ReportProgress(i, i.ToString() + "  ------- " + i.ToString());
            //    }

            //    progress.Finish("aaaaaaaaaaaaaaaaaaaaa");
            //};

            //action.BeginInvoke(null, null);

            //ProgressForm.Show("test", process as IAsyncProcess);
        }


        private void OnAddFile(object sender, EventArgs e)
        {
            string[]  filenames = AddFileHelper();
            if (filenames == null || filenames.Length == 0)
                return;


            // prepare asyncronious process host

            GenericAsyncProcess process = new GenericAsyncProcess();


            // generate anonim delegate to be executed on a separate thread

            MethodInvoker action = delegate
            {
                IAsyncProcessHost progress = ( process as IAsyncProcessHost );

                foreach ( string file in filenames )
                {
                    if ( progress.IsCancelled )
                        break;

                    progress.StartStep(string.Format(Properties.Resources.PromptAddingFile, file));

                    DataManager.Instance.AddBook(file, null, progress);
                }
            };


            // launch the generated anonim delegate in a separate
            // thread from the thread pool

            action.BeginInvoke(null, null);


            // show the progress form
            // if the operation is finished before the form is initialized
            // the form is not shown

            ProgressForm.Show(Properties.Resources.TitleAddingFiles, process as IAsyncProcess);


            // update filter with new languages and tags

            this.filterPanel.UpdateUI();
        }


        private void OnAddFolder(object sender, EventArgs e)
        {
            // TODO: implement
        }

        #endregion

        #region helpers

        private string[] AddFileHelper()
        {
            if ( DataManager.Instance.ActiveLibrary == null )
                return null;

            OpenFileDialog dlg = new OpenFileDialog();


            // build filter list

            Dictionary<string, object> uniqueExtension = new Dictionary<string, object>();

            foreach ( IBookFormat format in DataManager.Instance.Formats )
            {
                foreach ( string extension in format.SupportedExtensions )
                {
                    if ( !uniqueExtension.ContainsKey(extension) )
                        uniqueExtension.Add(extension, null);
                }
            }

            StringBuilder str = new StringBuilder(uniqueExtension.Keys.Count * 7);
            foreach ( string ext in uniqueExtension.Keys )
            {
                if ( str.Length > 0 )
                    str.Append(", ");

                str.Append(ext);
            }

            string extList = str.ToString();
            dlg.Filter = string.Format("Supported Formats ({0})|{1}", extList, extList.Replace(',', ';'));
            dlg.Multiselect = true;

            return ( dlg.ShowDialog() == DialogResult.OK ) ? dlg.FileNames : null;
        }

        #endregion
    }
}