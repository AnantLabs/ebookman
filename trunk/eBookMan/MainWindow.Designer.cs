namespace EBookMan
{
    partial class MainWindow
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
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ToolStripSplitButton btnAdd;
            System.Windows.Forms.ToolStripButton btnRemove;
            System.Windows.Forms.ToolStripButton btnEdit;
            System.Windows.Forms.ToolStripButton btnCopy;
            System.Windows.Forms.ToolStripButton btnRead;
            System.Windows.Forms.ToolStripButton btnOptions;
            this.addFromUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnView = new System.Windows.Forms.ToolStripSplitButton();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.viewPlaceHolder = new System.Windows.Forms.Panel();
            this.bookPanel = new EBookMan.BookPanel();
            this.filterPanel = new EBookMan.FilterPanel();
            btnAdd = new System.Windows.Forms.ToolStripSplitButton();
            btnRemove = new System.Windows.Forms.ToolStripButton();
            btnEdit = new System.Windows.Forms.ToolStripButton();
            btnCopy = new System.Windows.Forms.ToolStripButton();
            btnRead = new System.Windows.Forms.ToolStripButton();
            btnOptions = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            btnAdd.AutoSize = false;
            btnAdd.AutoToolTip = false;
            btnAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFromUrlToolStripMenuItem,
            this.addfileToolStripMenuItem,
            this.addFolderToolStripMenuItem});
            btnAdd.Image = global::EBookMan.Properties.Resources.add;
            btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(60, 49);
            btnAdd.Text = "Add";
            btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            btnAdd.ButtonClick += new System.EventHandler(this.OnToolBarAddClicked);
            // 
            // addFromUrlToolStripMenuItem
            // 
            this.addFromUrlToolStripMenuItem.Name = "addFromUrlToolStripMenuItem";
            this.addFromUrlToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.addFromUrlToolStripMenuItem.Text = "Add from &Url";
            this.addFromUrlToolStripMenuItem.Click += new System.EventHandler(this.OnAddUrl);
            // 
            // addfileToolStripMenuItem
            // 
            this.addfileToolStripMenuItem.Name = "addfileToolStripMenuItem";
            this.addfileToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.addfileToolStripMenuItem.Text = "Add &file";
            this.addfileToolStripMenuItem.Click += new System.EventHandler(this.OnAddFile);
            // 
            // addFolderToolStripMenuItem
            // 
            this.addFolderToolStripMenuItem.Name = "addFolderToolStripMenuItem";
            this.addFolderToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.addFolderToolStripMenuItem.Text = "Add f&older";
            this.addFolderToolStripMenuItem.Click += new System.EventHandler(this.OnAddFolder);
            // 
            // btnRemove
            // 
            btnRemove.AutoSize = false;
            btnRemove.AutoToolTip = false;
            btnRemove.Image = global::EBookMan.Properties.Resources.delete;
            btnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new System.Drawing.Size(60, 49);
            btnRemove.Text = "Remove";
            btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            btnRemove.Click += new System.EventHandler(this.OnDeleteClicked);
            // 
            // btnEdit
            // 
            btnEdit.AutoSize = false;
            btnEdit.AutoToolTip = false;
            btnEdit.Image = global::EBookMan.Properties.Resources.edit;
            btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new System.Drawing.Size(60, 49);
            btnEdit.Text = "Edit";
            btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            btnEdit.Click += new System.EventHandler(this.OnEditClicked);
            // 
            // btnCopy
            // 
            btnCopy.AutoSize = false;
            btnCopy.AutoToolTip = false;
            btnCopy.Image = global::EBookMan.Properties.Resources.copy;
            btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new System.Drawing.Size(60, 49);
            btnCopy.Text = "Copy &To";
            btnCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            btnCopy.Click += new System.EventHandler(this.OnCopyClicked);
            // 
            // btnRead
            // 
            btnRead.AutoSize = false;
            btnRead.AutoToolTip = false;
            btnRead.Image = global::EBookMan.Properties.Resources.read;
            btnRead.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnRead.Name = "btnRead";
            btnRead.Size = new System.Drawing.Size(60, 49);
            btnRead.Text = "Read";
            btnRead.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            btnRead.Click += new System.EventHandler(this.OnReadClicked);
            // 
            // btnOptions
            // 
            btnOptions.AutoSize = false;
            btnOptions.AutoToolTip = false;
            btnOptions.Image = global::EBookMan.Properties.Resources.options;
            btnOptions.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            btnOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnOptions.Name = "btnOptions";
            btnOptions.Size = new System.Drawing.Size(60, 49);
            btnOptions.Text = "Options";
            btnOptions.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            btnOptions.Click += new System.EventHandler(this.OnOptionsClicked);
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            btnAdd,
            btnRemove,
            btnEdit,
            btnCopy,
            btnRead,
            btnOptions,
            this.btnView});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(3, 0, 1, 0);
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(737, 52);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip";
            // 
            // btnView
            // 
            this.btnView.AutoSize = false;
            this.btnView.AutoToolTip = false;
            this.btnView.Image = global::EBookMan.Properties.Resources.view;
            this.btnView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(60, 49);
            this.btnView.Text = "View";
            this.btnView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnView.ButtonClick += new System.EventHandler(this.OnViewClicked);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.Control;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(574, 52);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 432);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 329);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(574, 5);
            this.splitter2.TabIndex = 6;
            this.splitter2.TabStop = false;
            // 
            // viewPlaceHolder
            // 
            this.viewPlaceHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPlaceHolder.Location = new System.Drawing.Point(0, 52);
            this.viewPlaceHolder.Name = "viewPlaceHolder";
            this.viewPlaceHolder.Size = new System.Drawing.Size(574, 277);
            this.viewPlaceHolder.TabIndex = 7;
            // 
            // bookPanel
            // 
            this.bookPanel.AutoScroll = true;
            this.bookPanel.BackColor = System.Drawing.Color.PeachPuff;
            this.bookPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bookPanel.Location = new System.Drawing.Point(0, 334);
            this.bookPanel.Name = "bookPanel";
            this.bookPanel.Size = new System.Drawing.Size(574, 150);
            this.bookPanel.TabIndex = 5;
            // 
            // filterPanel
            // 
            this.filterPanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.filterPanel.Location = new System.Drawing.Point(579, 52);
            this.filterPanel.MinimumSize = new System.Drawing.Size(158, 360);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(158, 432);
            this.filterPanel.TabIndex = 1;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(737, 484);
            this.Controls.Add(this.viewPlaceHolder);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.bookPanel);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.filterPanel);
            this.Controls.Add(this.toolStrip);
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private FilterPanel filterPanel;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripMenuItem addFromUrlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton btnView;
        private BookPanel bookPanel;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel viewPlaceHolder;
    }
}

