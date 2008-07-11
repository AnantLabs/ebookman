using System;
using System.Windows.Forms;
using System.Drawing;

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


        void IViewer.BeginUpdate()
        {
            this.BeginUpdate();
        }


        void IViewer.EndUpdate()
        {
            this.EndUpdate();
        }


        void IViewer.Add(Book book)
        {
            string[] fields = new string[] { book.Title, book.Authors, book.Rating.ToString() };

            ListViewItem item = new ListViewItem(fields);
            item.Tag = book.ID;

            this.Items.Add(item);
        }


        void IViewer.Clear()
        {
            this.Items.Clear();
        }

        #endregion
    }
}
