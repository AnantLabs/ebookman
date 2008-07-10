using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace EBookMan
{
    internal sealed partial class FilterPanel : UserControl
    {
        #region constructor/destuctor

        public FilterPanel()
        {
            InitializeComponent();

            this.rating.StarsChanged += new EventHandler(OnStarsChanged);


            // create timer used to delay
            // reaction to user changes

            this.waitCounter = 0;

            this.timer = new Timer();
            this.timer.Interval = 200;
            this.timer.Tick += new EventHandler(OnTimerTick);


            // subscribe to the library change event
            // when the library is changed - load
            // the last used filter

            DataManager.Instance.ActiveLibraryChange += new EventHandler(OnActiveLibraryChanged);
            OnActiveLibraryChanged(this, EventArgs.Empty);
        }

        #endregion

        #region public methods

        public void UpdateUI()
        {
            ILibrary lib = DataManager.Instance.ActiveLibrary;

            this.SuspendLayout();


            // update language drop down

            this.cmbLanguage.BeginUpdate();
            this.cmbLanguage.Items.Clear();

            List<string> langs = ( lib != null ) ? lib.GetLanguages() : null;

            if ( langs != null )
            {
                foreach ( string lang in langs )
                    this.cmbLanguage.Items.Add(lang);
            }

            this.cmbLanguage.Enabled = ( langs != null && langs.Count > 0 );


            this.cmbLanguage.ResumeLayout();

            this.listTags.Items.Clear();


            // update list of tags

            this.listTags.BeginUpdate();
            this.listTags.Items.Clear();

            List<string> tags = ( lib != null ) ? lib.GetAvailableTags() : null;

            if ( tags != null )
            {
                foreach ( string tag in tags )
                    this.listTags.Items.Add(tag);
            }

            this.listTags.Enabled = ( tags != null && tags.Count > 0 );
            this.listTags.EndUpdate();


            // update control values

            if ( lib != null && lib.Filter != null )
                UpdateUiFromFilter(lib.Filter);

            this.ResumeLayout();
        }

        #endregion

        #region Event handlers

        private void OnActiveLibraryChanged(object sender, EventArgs e)
        {
            Update();
        }


        private void OnCriteriaChanged(object sender, EventArgs e)
        {
            this.waitCounter = 5;
            if ( !this.timer.Enabled )
                this.timer.Start();
        }


        private void OnTagChanged(object sender, ItemCheckEventArgs e)
        {
            OnCriteriaChanged(sender, EventArgs.Empty);
        }

        
        private void OnReset(object sender, EventArgs e)
        {
            if ( DataManager.Instance.ActiveLibrary != null )
            {
                DataManager.Instance.ActiveLibrary.Filter = null;
                UpdateUiFromFilter(null);
            }
        }


        private void OnTimerTick(object sender, EventArgs e)
        {
            if (this.waitCounter <= 0)
            {
                this.timer.Stop();
                if (DataManager.Instance.ActiveLibrary != null)
                {
                    DataManager.Instance.ActiveLibrary.Filter = GetFilterFromUi();
                    return;
                }
            }

            this.waitCounter --;
        }


        private void OnStarsChanged(object sender, EventArgs e)
        {
            this.chkRating.Text = string.Format(Properties.Resources.RatingTextNumbered, this.rating.Stars);
        }


        private void OnRatingCheckChanged(object sender, EventArgs e)
        {
            this.rating.Enabled = this.chkRating.Checked;
            this.chkIncludeHigher.Enabled = this.chkIncludeHigher.Checked;

            this.chkRating.Text = Properties.Resources.RatingTextEmpty;
        }
        
        #endregion

        #region helpers

        private Filter GetFilterFromUi()
        {
            Filter filter = new Filter();


            // search text

            if ( !string.IsNullOrEmpty(this.txtSearch.Text) )
            {
                if ( this.chkAuthor.Checked )
                    filter.Add(Filter.Author, this.txtSearch.Text, FilterOperation.Contains);

                if ( this.chkTitle.Checked )
                    filter.Add(Filter.Title, this.txtSearch.Text, FilterOperation.Contains);

                if ( this.chkSeries.Checked )
                    filter.Add(Filter.Series, this.txtSearch.Text, FilterOperation.Contains);

                if ( this.chkAnnotation.Checked )
                    filter.Add(Filter.Annotation, this.txtSearch.Text, FilterOperation.Contains);
            }


            // rating

            if ( this.chkRating.Checked )
                filter.Add(Filter.Rating, this.rating.Stars,
                    ( this.chkIncludeHigher.Checked ) ? FilterOperation.GreaterOrEqual : FilterOperation.Equal);


            // languange

            if ( !string.IsNullOrEmpty(this.cmbLanguage.Text) )
                filter.Add(Filter.Language, this.cmbLanguage.Text, FilterOperation.Equal);


            // tags

            if ( this.listTags.CheckedIndices.Count < this.listTags.Items.Count )
            {
                List<string> tags = new List<string>(this.listTags.Items.Count);

                for ( int i = 0 ; i < this.listTags.Items.Count ; i++ )
                    if ( this.listTags.GetItemChecked(i) )
                        tags.Add(this.listTags.Items[ i ].ToString());

                filter.Add(Filter.Tags, tags.ToArray(), FilterOperation.Contains);
            }


            return filter.IsEmpty ? null : filter;
        }


        private void UpdateUiFromFilter(Filter filter)
        {
            object value;
            FilterOperation op;


            // update text and text checkboxes

            string text = null;
            string[] fields = new string[]{Filter.Author, Filter.Title, Filter.Series, Filter.Annotation};
            CheckBox[] chkBoxes = new CheckBox[]{this.chkAuthor, this.chkTitle, this.chkSeries, this.chkAnnotation};

            for (int i=0; i<fields.Length; i++)
            {
                if (filter.Get(fields[i], out value, out op) && 
                    (op == FilterOperation.Contains || op == FilterOperation.Equal) &&
                    (value.ToString().Length > 0))
                {
                    string val = value.ToString();
                    if (text == null)
                    {
                        text = val;
                        chkBoxes[i].Checked = true;
                    }
                    else 
                        chkBoxes[i].Checked = (string.Compare(text, val, true) == 0);
                }
                else
                {
                    chkBoxes[i].Checked = false;
                }
            }

            this.txtSearch.Text = text;
                    

            // update rating

            if (filter.Get(Filter.Rating, out value, out op) && 
                (op == FilterOperation.GreaterOrEqual || op == FilterOperation.Equal))
            {
                this.chkRating.Checked = true;
                this.rating.Stars = (byte)value;
                this.chkIncludeHigher.Checked = (op == FilterOperation.GreaterOrEqual);
                OnStarsChanged(this, EventArgs.Empty);
            }
            else
            {
                this.chkRating.Checked = false;
            }


            // update language

            if ( filter.Get(Filter.Language, out value, out op) &&
                ( op == FilterOperation.Contains || op == FilterOperation.Equal ) )
            {
                this.cmbLanguage.Text = value.ToString();
            }
            else
            {
                this.cmbLanguage.Text = "";
            }


            // update tags

            if (this.listTags.Items.Count > 0)
            {
                string[] tags = (filter.Get(Filter.Tags, out value, out op) && op == FilterOperation.Contains) 
                    ? (string[])value 
                    : null;

                for (int i=0; i<this.listTags.Items.Count; i++)
                {
                    if (tags == null)
                    {
                        this.listTags.SetItemChecked(i, true);
                    }
                    else
                    {
                        bool found = false;

                        foreach (string filterTag in tags)
                        {
                            if (string.Compare(this.listTags.Items[i].ToString(), filterTag, true) == 0)
                            {
                                found = true;
                                break;
                            }
                        }

                        this.listTags.SetItemChecked(i, found);
                    }
                }
            }
        }

        #endregion

        #region private members

        private Timer timer;
        private int waitCounter;

        #endregion
    }
}
