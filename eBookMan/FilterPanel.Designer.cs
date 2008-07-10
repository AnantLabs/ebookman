namespace EBookMan
{
    partial class FilterPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();

                if ( this.timer != null )
                {
                    this.timer.Stop();
                    this.timer.Dispose();
                    this.timer = null;
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.GroupBox groupBox3;
            System.Windows.Forms.GroupBox groupTags;
            this.chkAnnotation = new System.Windows.Forms.CheckBox();
            this.chkTitle = new System.Windows.Forms.CheckBox();
            this.chkSeries = new System.Windows.Forms.CheckBox();
            this.chkAuthor = new System.Windows.Forms.CheckBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.chkRating = new System.Windows.Forms.CheckBox();
            this.chkIncludeHigher = new System.Windows.Forms.CheckBox();
            this.rating = new EBookMan.StarRating();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.listTags = new System.Windows.Forms.CheckedListBox();
            this.btnReset = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            groupBox3 = new System.Windows.Forms.GroupBox();
            groupTags = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupTags.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            groupBox1.Controls.Add(this.chkAnnotation);
            groupBox1.Controls.Add(this.chkTitle);
            groupBox1.Controls.Add(this.chkSeries);
            groupBox1.Controls.Add(this.chkAuthor);
            groupBox1.Controls.Add(this.txtSearch);
            groupBox1.Location = new System.Drawing.Point(3, 1);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(152, 94);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Text Search";
            // 
            // chkAnnotation
            // 
            this.chkAnnotation.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.chkAnnotation.AutoSize = true;
            this.chkAnnotation.Location = new System.Drawing.Point(66, 70);
            this.chkAnnotation.Name = "chkAnnotation";
            this.chkAnnotation.Size = new System.Drawing.Size(77, 17);
            this.chkAnnotation.TabIndex = 4;
            this.chkAnnotation.Text = "A&nnotation";
            this.chkAnnotation.UseVisualStyleBackColor = true;
            this.chkAnnotation.Click += new System.EventHandler(this.OnCriteriaChanged);
            // 
            // chkTitle
            // 
            this.chkTitle.AutoSize = true;
            this.chkTitle.Location = new System.Drawing.Point(9, 70);
            this.chkTitle.Name = "chkTitle";
            this.chkTitle.Size = new System.Drawing.Size(46, 17);
            this.chkTitle.TabIndex = 2;
            this.chkTitle.Text = "&Title";
            this.chkTitle.UseVisualStyleBackColor = true;
            this.chkTitle.Click += new System.EventHandler(this.OnCriteriaChanged);
            // 
            // chkSeries
            // 
            this.chkSeries.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.chkSeries.AutoSize = true;
            this.chkSeries.Location = new System.Drawing.Point(66, 48);
            this.chkSeries.Name = "chkSeries";
            this.chkSeries.Size = new System.Drawing.Size(55, 17);
            this.chkSeries.TabIndex = 3;
            this.chkSeries.Text = "&Series";
            this.chkSeries.UseVisualStyleBackColor = true;
            this.chkSeries.Click += new System.EventHandler(this.OnCriteriaChanged);
            // 
            // chkAuthor
            // 
            this.chkAuthor.AutoSize = true;
            this.chkAuthor.Location = new System.Drawing.Point(9, 48);
            this.chkAuthor.Name = "chkAuthor";
            this.chkAuthor.Size = new System.Drawing.Size(57, 17);
            this.chkAuthor.TabIndex = 1;
            this.chkAuthor.Text = "&Author";
            this.chkAuthor.UseVisualStyleBackColor = true;
            this.chkAuthor.Click += new System.EventHandler(this.OnCriteriaChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.txtSearch.Location = new System.Drawing.Point(6, 19);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(140, 20);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.OnCriteriaChanged);
            this.txtSearch.Click += new System.EventHandler(this.OnCriteriaChanged);
            // 
            // groupBox2
            // 
            groupBox2.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            groupBox2.Controls.Add(this.chkRating);
            groupBox2.Controls.Add(this.chkIncludeHigher);
            groupBox2.Location = new System.Drawing.Point(3, 98);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(152, 88);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Rating";
            // 
            // chkRating
            // 
            this.chkRating.AutoSize = true;
            this.chkRating.Location = new System.Drawing.Point(9, 19);
            this.chkRating.Name = "chkRating";
            this.chkRating.Size = new System.Drawing.Size(84, 17);
            this.chkRating.TabIndex = 1;
            this.chkRating.Text = "Only 5 starts";
            this.chkRating.UseVisualStyleBackColor = true;
            this.chkRating.Click += new System.EventHandler(this.OnRatingCheckChanged);
            // 
            // chkIncludeHigher
            // 
            this.chkIncludeHigher.AutoSize = true;
            this.chkIncludeHigher.Location = new System.Drawing.Point(9, 64);
            this.chkIncludeHigher.Name = "chkIncludeHigher";
            this.chkIncludeHigher.Size = new System.Drawing.Size(77, 17);
            this.chkIncludeHigher.TabIndex = 0;
            this.chkIncludeHigher.Text = "And h&igher";
            this.chkIncludeHigher.UseVisualStyleBackColor = true;
            this.chkIncludeHigher.Click += new System.EventHandler(this.OnCriteriaChanged);
            // 
            // groupBox3
            // 
            groupBox3.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            groupBox3.Controls.Add(this.rating);
            groupBox3.Controls.Add(this.cmbLanguage);
            groupBox3.Location = new System.Drawing.Point(3, 189);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(152, 49);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Language";
            // 
            // rating
            // 
            this.rating.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rating.ImageChecked = global::EBookMan.Properties.Resources.sternvoll;
            this.rating.ImageEmpty = global::EBookMan.Properties.Resources.sternhalb;
            this.rating.Location = new System.Drawing.Point(6, -54);
            this.rating.MaximumStars = 5;
            this.rating.Name = "rating";
            this.rating.Size = new System.Drawing.Size(140, 24);
            this.rating.Stars = 3;
            this.rating.TabIndex = 0;
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.cmbLanguage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbLanguage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbLanguage.Location = new System.Drawing.Point(6, 18);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(140, 21);
            this.cmbLanguage.Sorted = true;
            this.cmbLanguage.TabIndex = 0;
            this.cmbLanguage.TextUpdate += new System.EventHandler(this.OnCriteriaChanged);
            // 
            // groupTags
            // 
            groupTags.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            groupTags.Controls.Add(this.listTags);
            groupTags.Location = new System.Drawing.Point(3, 241);
            groupTags.Name = "groupTags";
            groupTags.Size = new System.Drawing.Size(152, 122);
            groupTags.TabIndex = 4;
            groupTags.TabStop = false;
            groupTags.Text = "Tags";
            // 
            // listTags
            // 
            this.listTags.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.listTags.CheckOnClick = true;
            this.listTags.IntegralHeight = false;
            this.listTags.Location = new System.Drawing.Point(6, 19);
            this.listTags.Name = "listTags";
            this.listTags.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listTags.Size = new System.Drawing.Size(140, 93);
            this.listTags.Sorted = true;
            this.listTags.TabIndex = 0;
            this.listTags.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.OnTagChanged);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnReset.Location = new System.Drawing.Point(38, 368);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.OnReset);
            // 
            // FilterPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.btnReset);
            this.Controls.Add(groupTags);
            this.Controls.Add(groupBox3);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox1);
            this.MinimumSize = new System.Drawing.Size(158, 360);
            this.Name = "FilterPanel";
            this.Size = new System.Drawing.Size(158, 394);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupTags.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkAnnotation;
        private System.Windows.Forms.CheckBox chkTitle;
        private System.Windows.Forms.CheckBox chkSeries;
        private System.Windows.Forms.CheckBox chkAuthor;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.CheckedListBox listTags;
        private System.Windows.Forms.Button btnReset;
        private StarRating rating;
        private System.Windows.Forms.CheckBox chkIncludeHigher;
        private System.Windows.Forms.CheckBox chkRating;

    }
}
