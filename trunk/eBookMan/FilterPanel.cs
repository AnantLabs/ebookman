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


            // generate list of formats

            List<IBookFormat> formats = DataManager.Instance.Formats;
            int x = this.chkSeries.Left;
            int y = this.chkFrmtNone.Top;

            this.SuspendLayout();

            EventHandler checkHandler = new EventHandler(OnCriteriaChanged);

            foreach ( IBookFormat format in formats )
            {
                CheckBox chkFmt = new CheckBox();
                chkFmt.AutoSize = true;
                chkFmt.Location = new System.Drawing.Point(x, y);
                chkFmt.Size = new System.Drawing.Size(64, 17);
                chkFmt.Text = format.Name;
                chkFmt.UseVisualStyleBackColor = true;
                chkFmt.Tag = format.Guid;
                chkFmt.CheckedChanged += checkHandler;
                this.Controls.Add(chkFmt);

                if (x == this.chkSeries.Left)
                {
                    y += 22;
                    x = this.chkFrmtNone.Left;

                    this.groupFormat.Height += 22;
                    this.groupTags.Height -= 22;
                    this.groupTags.Top += 22;
                }
                else
                {
                    x = this.chkSeries.Left;
                }
            }

            this.ResumeLayout();


            // add timer

            this.waitCounter = 0;

            this.timer = new Timer();
            this.timer.Interval = 200;
            this.timer.Tick += new EventHandler(OnTimerTick);

        }

        #endregion

        #region public methods

        public void UpdateContent()
        {
            ILibrary lib = DataManager.Instance.ActiveLibrary;


            // update languages

            this.cmbLanguage.Items.Clear();
            string[] langs = (lib != null) ? lib.GetLanguages() : null;

            if (langs != null)
            {
                foreach (string lang in langs)
                    this.cmbLanguage.Items.Add(lang);
            }
                
            this.cmbLanguage.Enabled = ( langs != null && langs.Length > 0 );

            
            // update list of tags

            this.listTags.BeginUpdate();
            this.listTags.Items.Clear();

            string[] tags = ( lib != null ) ? lib.GetTags() : null;

            if ( tags != null )
            {
                foreach ( string tag in tags )
                    this.listTags.Items.Add(tag);
            }

            this.listTags.Enabled = (tags != null && tags.Length > 0);
            this.listTags.EndUpdate();


            // reset filter criteria 
            // and update UI

            if ( !this.filters.ContainsKey(lib) )
                this.filters.Add(lib, new FilterCriteria());

            SetFilterToUI(this.filters[lib]);
        }


        public FilterCriteria Filter
        {
            get 
            { 
                return (DataManager.Instance.ActiveLibrary == null) ? null : this.filters[DataManager.Instance.ActiveLibrary]; 
            }
        }


        public event EventHandler FilterChanged;

        #endregion

        #region UI event handlers

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
            if (DataManager.Instance.ActiveLibrary == null)
                return;

            FilterCriteria filter = this.filters[DataManager.Instance.ActiveLibrary];
            if ( filter.IsDefault(DataManager.Instance.ActiveLibrary) )
                return;

            filter.Reset(DataManager.Instance.ActiveLibrary);
            SetFilterToUI(filter);
            FireFilterChanged();
        }


        private void OnTimerTick(object sender, EventArgs e)
        {
            if (this.waitCounter <= 0)
            {
                this.timer.Stop();
                if (DataManager.Instance.ActiveLibrary != null)
                {
                    if (SetUIToFilter())
                        FireFilterChanged();
                }
            }

            this.waitCounter --;
        }


        #endregion

        #region helpers

        private void FireFilterChanged ()
        {
            EventHandler h = FilterChanged;
            if (h != null) h(this, EventArgs.Empty);
        }


        private void SetFilterToUI(FilterCriteria filter)
        {
            // text search

            this.txtSearch.Text = filter.Text;
            this.chkAuthor.Checked = ( ( filter.TextSearchLocation & TextSearchLocation.Authors ) == TextSearchLocation.Authors );
            this.chkTitle.Checked = ( ( filter.TextSearchLocation & TextSearchLocation.Title) == TextSearchLocation.Title);
            this.chkSeries.Checked = ( ( filter.TextSearchLocation & TextSearchLocation.Series) == TextSearchLocation.Series);
            this.chkAnnotation.Checked = ( ( filter.TextSearchLocation & TextSearchLocation.Annotation ) == TextSearchLocation.Annotation );


            // TODO: rating


            // language

            this.cmbLanguage.Text = filter.Language;


            // formats

            foreach (Control control in this.groupFormat.Controls)
            {
                CheckBox chk = control as CheckBox;
                if (chk != null)
                {
                    if (chk.Tag == null)
                    {
                        // no files
                        chk.Checked = filter.NoFiles;
                    }
                    else
                    {
                        // different formats
                       
                        bool found = false;

                        if (filter.Formats != null)
                        {
                            foreach (IBookFormat format in filter.Formats)
                                if (format.Guid.Equals((Guid)chk.Tag))
                                {
                                    found = true;
                                    break;
                                }
                        }

                        chk.Checked = found;
                    }
                }
            }


            // tags

            this.listTags.BeginUpdate();

            for (int i=0; i<this.listTags.Items.Count; i++)
                this.listTags.SetItemChecked(i, false);

            if (filter.Tags != null && filter.Tags.Length > 0)
            {
                foreach (string tag in filter.Tags)
                {
                    int index  = this.listTags.FindStringExact(tag);
                    if (index != -1)
                        this.listTags.SetItemChecked(index, true);
                }
            }

        }


        private bool SetUIToFilter()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region private members

        private Dictionary<ILibrary, FilterCriteria> filters = new Dictionary<ILibrary, FilterCriteria>();
        private Timer timer;
        private int waitCounter;

        #endregion
    }
}
