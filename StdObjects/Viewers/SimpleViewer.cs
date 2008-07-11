using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace EBookMan
{
    public class SimpleViewer : ListView, IViewer
    {
        #region Constructor

        public SimpleViewer()
        {
            this.View = View.Details;

            ColumnHeader[] headers = new ColumnHeader[ 3 ];
            headers[ 0 ] = new ColumnHeader();
            headers[ 0 ].Text = "Title";

            headers[ 1 ] = new ColumnHeader();
            headers[ 1 ].Text = "Authors";

            headers[ 2 ] = new ColumnHeader();
            headers[ 2 ].Text = "Raiting";

            this.Columns.AddRange(headers);
        }
                
        #endregion

        #region IViewer Members

        Image IViewer.Icon
        {
            get { return null; }
        }

        string IViewer.Name
        {
            get { return "Simple Viewer"; }
        }


        Control IViewer.Control
        {
            get { return this; }
        }


        private readonly Guid guid = new Guid("{42cd58e7-f2c8-4ce4-80e1-229eb3bca363}");

        public Guid Guid
        {
            get { return this.guid; }
        }


        void IViewer.Fill(ILibrary library, Filter filter)
        {
            IEnumerator<Book> enumerator = library.GetEnumerator(filter);

            this.BeginUpdate();
            this.Items.Clear();

            enumerator.Reset();
            while ( enumerator.MoveNext() )
            {
                Book book = enumerator.Current;
                string[] fields = new string[] { book.Title, book.Authors, book.Rating.ToString() };

                ListViewItem item = new ListViewItem(fields);
                item.Tag = book.ID;

                this.Items.Add(item);
            }

            this.EndUpdate();
            enumerator.Dispose();
        }


        void IViewer.Clear()
        {
            this.BeginUpdate();
            this.Items.Clear();
            this.EndUpdate();
        }

        #endregion
    }
}
