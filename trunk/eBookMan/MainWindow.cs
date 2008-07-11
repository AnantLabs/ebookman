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
        #region constructor/init

        public MainWindow()
        {
            InitializeComponent();

            this.filterPanel.FilterChanged += new EventHandler(OnUpdateView);

            CreateViewMenu();
        }


        protected override void OnLoad(EventArgs e)
        {
            if ( this.DesignMode || ( this.Site != null && this.Site.DesignMode ) )
                return;

            DataManager.Instance.MainWindow = this;

            DataManager.Instance.ActiveLibraryChange += new EventHandler(OnUpdateView);


            // select last know viewer

            Guid lastViewer = Properties.Settings.Default.Viewer;

            OnViewChanged(
                DataManager.Instance.Viewers.Find(
                    delegate(IViewer viewer) { return viewer.Guid.Equals(lastViewer); }),
                EventArgs.Empty);

            base.OnLoad(e);
        }

        #endregion

        #region system event handlers

        private void OnUpdateView(object sender, EventArgs e)
        {
            if ( this.InvokeRequired )
            {
                this.BeginInvoke(new EventHandler(OnUpdateView));
                return;
            }

            if (DataManager.Instance.ActiveLibrary == null || this.currViewer == null)
                return;

            this.currViewer.Fill(DataManager.Instance.ActiveLibrary, this.filterPanel.Filter);
        }

        #endregion

        #region UI event handlers

        private void OnToolBarAddClicked(object sender, EventArgs e)
        {

        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {

        }

        private void OnViewClicked(object sender, EventArgs e)
        {

        }

        private void OnOptionsClicked(object sender, EventArgs e)
        {

        }

        private void OnReadClicked(object sender, EventArgs e)
        {

        }

        private void OnCopyClicked(object sender, EventArgs e)
        {

        }

        private void OnEditClicked(object sender, EventArgs e)
        {

        }

        private void OnViewChanged(object sender, EventArgs args)
        {            
            ToolStripItem item = sender as ToolStripItem;
            IViewer newViewer = (item == null)? (sender as IViewer) : (item.Tag as IViewer);
            if (newViewer == null) return;

            this.SuspendLayout();


            // hide the previous view and clear the data

            if (this.currViewer != null && this.currViewer.Control != null)
            {
                this.currViewer.Control.Hide();
                this.currViewer.Clear();
            }

            
            // fill up the new view and show it

            this.currViewer = newViewer;
            OnUpdateView(this, EventArgs.Empty);
            this.currViewer.Control.Show();

            this.ResumeLayout();
        }



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

                progress.Finish(
                    ( filenames.Length > 1 )
                    ? string.Format(Properties.Resources.PromptAddedFiles, filenames.Length)
                    : null);
            };


            // launch the generated anonim delegate in a separate
            // thread from the thread pool

            action.BeginInvoke(null, null);


            // show the progress form
            // if the operation is finished before the form is initialized
            // the form is not shown

            ProgressForm.Show(Properties.Resources.TitleAddingFiles, process as IAsyncProcess);


            // update filter with new languages and tags

            this.filterPanel.UpdateLanguageAndTags();
            OnUpdateView(this, EventArgs.Empty);
        }


        private void OnAddFolder(object sender, EventArgs e)
        {
            // TODO: implement
        }

        #endregion

        #region helpers

        private void CreateViewMenu()
        {
            foreach ( IViewer viewer in DataManager.Instance.Viewers )
            {
                // init viewer control

                if (viewer.Control != null)
                {
                    viewer.Control.Dock = DockStyle.Fill;
                    viewer.Control.Location = new Point(0, 0);
                    viewer.Control.Size = new Size(200, 200);
                    viewer.Control.Visible = false;
                    this.viewPlaceHolder.Controls.Add(viewer.Control);
                }


                // create viewer menu item

                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = viewer.Name;
                item.Tag = viewer.Guid;
                item.Click += new EventHandler(OnViewChanged);

                this.btnView.DropDownItems.Add(item);
            }
        }

        
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

        #region members

        private IViewer currViewer = null;

        #endregion
    }
}