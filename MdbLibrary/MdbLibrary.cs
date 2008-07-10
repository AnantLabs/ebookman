using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace EBookMan
{
    public partial class MdbLibrary : ILibrary, IPlugin
    {
        public MdbLibrary()
        {
            string path = Properties.Settings.Default.DbRoot;
            if (string.IsNullOrEmpty(path))
            {
                path = IOHelper.CompletePath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)) + "My Books\\";
                Properties.Settings.Default.DbRoot = path;
                Properties.Settings.Default.Save();
            }

            string connString = string.Format(
                "Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}books.mdb;Jet OLEDB:Database Password=BookMan",
                path);

            this.connection = new OleDbConnection(connString);
            this.connection.Open();

            string list = Properties.Settings.Default.CompressableFiles;
            if ( !string.IsNullOrEmpty(list) )
            {
                string[] exts = list.Split(';', ' ', ',');
                if ( exts != null )
                {
                    foreach ( string e in exts )
                        this.compressable.Add(e.Replace("*", "").ToLower(), null);
                }
            }
        }

        #region IPlugin Members

        public void RegisterMainWindowUI(Form mainWindow)
        {
            Control[] controls = mainWindow.Controls.Find("toolStrip", false);
            ToolStrip toolstrip = ( controls.Length > 0 ) ? controls[ 0 ] as ToolStrip: null;

            if ( toolstrip == null )
                return;

            this.button = new ToolStripButton(
                Properties.Resources.MdbName,
                Properties.Resources.computer,
                new EventHandler(OnToolbarButtonClicked));

            this.button.TextImageRelation = TextImageRelation.ImageAboveText;
            this.button.Alignment = ToolStripItemAlignment.Right;
            this.button.Size = new Size(60, 49);
            this.button.ToolTipText = Properties.Resources.MdbTooltip;

            toolstrip.Items.Add(this.button);

            if ( Properties.Settings.Default.Active )
                OnToolbarButtonClicked(this, EventArgs.Empty);
        }


        public void RegisterOptionsUI(Form optionDialog)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IEnumeratable

        public IEnumerator<Book> GetEnumerator()
        {
            throw new NotImplementedException();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator(); 
        }


        #endregion

        #region Events

        private void OnToolbarButtonClicked(object sender, EventArgs e)
        {
            if ( this.button.Checked )
                return;

            this.button.Checked = true;
            DataManager.Instance.ActiveLibrary = this;
            Properties.Settings.Default.Active = true;
        }

        #endregion

        #region IDisposable implementation

        /// <summary>
		/// Track whether Dispose has been called
		/// </summary>
		private bool disposed = false;
        
		/// <summary>
		/// Implement IDisposable.
		/// Do not make this method virtual.
		/// A derived class should not be able to override this method.
		/// </summary>
		public void Dispose()
		{
			if(! this.disposed)
			{
				Dispose(true);
			}

			// Take yourself off the Finalization queue 
			// to prevent finalization code for this object
			// from executing a second time.
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Dispose(bool disposing) executes in two distinct scenarios.
		/// If disposing equals true, the method has been called directly
		/// or indirectly by a user's code. Managed and unmanaged resources
		/// can be disposed.
		/// If disposing equals false, the method has been called by the 
		/// runtime from inside the finalizer and you should not reference 
		/// other objects. Only unmanaged resources can be disposed.
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose (bool disposing)
		{
            if ( this.connection != null )
            {
                this.connection.Close();
                this.connection = null;
            }

			this.disposed = true;         
		}

		/// <summary>
		/// Use C# destructor syntax for finalization code.
		/// This destructor will run only if the Dispose method 
		/// does not get called.
		/// It gives your base class the opportunity to finalize.
		/// Do not provide destructors in types derived from this class.
		/// </summary>
        ~MdbLibrary()      
		{
			// Do not re-create Dispose clean-up code here.
			// Calling Dispose(false) is optimal in terms of
			// readability and maintainability.
			if (! this.disposed)
				Dispose(false);
		}

		#endregion

        #region Private members

        private OleDbConnection connection;
        private ToolStripButton button;
        private Dictionary<string, object> compressable = new Dictionary<string, object>();

        private List<string> languanges;
        private List<string> tags;

        #endregion
    }
}
