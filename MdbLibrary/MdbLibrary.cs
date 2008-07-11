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
        #region Constructor/Dispose

        public MdbLibrary()
        {
            // load the path to the dbfile or create a default one

            string path = Properties.Settings.Default.DbRoot;
            if (string.IsNullOrEmpty(path))
            {
                path = IOHelper.CompletePath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)) + "My Books\\";
                Properties.Settings.Default.DbRoot = path;
                Properties.Settings.Default.Save();
            }

            
            // if file does not exist there - copy the one supplied

            if ( !File.Exists(string.Format("{0}books.mdb", path)) )
            {
                if ( !Directory.Exists(path) )
                    Directory.CreateDirectory(path);

                File.Copy(DataManager.Instance.AppFolder + "books.mdb", path);

                Logger.Warning("MDBLibrary: MDB file was not fount at {0}. Copying default.", path);
            }


            // connect to the database

            string connString = string.Format(
                "Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}books.mdb;Jet OLEDB:Database Password=BookMan",
                path);

            this.connection = new OleDbConnection(connString);
            this.connection.Open();


            // preparing the list of compressable extensions

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


        public void Dispose()
        {
            if ( this.connection != null )
            {
                this.connection.Close();
                this.connection = null;
            }
        }

        #endregion

        #region IPlugin implementation

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
        }


        public void RegisterOptionsUI(Form optionDialog)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region Events

        private void OnToolbarButtonClicked(object sender, EventArgs e)
        {
            if ( this.button.Checked )
                return;

            this.button.Checked = true;
            DataManager.Instance.ActiveLibrary = this;
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
