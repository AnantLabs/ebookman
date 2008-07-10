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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripSplitButton();
            this.addFromUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRemove = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnRead = new System.Windows.Forms.ToolStripButton();
            this.btnOptions = new System.Windows.Forms.ToolStripButton();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.listView1 = new System.Windows.Forms.ListView();
            this.bookPanel1 = new BookPanel();
            this.filterPanel = new FilterPanel();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnRemove,
            this.btnEdit,
            this.btnCopy,
            this.btnRead,
            this.btnOptions});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(3, 0, 1, 0);
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(688, 52);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip";
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = false;
            this.btnAdd.AutoToolTip = false;
            this.btnAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFromUrlToolStripMenuItem,
            this.addfileToolStripMenuItem,
            this.addFolderToolStripMenuItem});
            this.btnAdd.Image = ( ( System.Drawing.Image ) ( resources.GetObject("btnAdd.Image") ) );
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 49);
            this.btnAdd.Text = "Add";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
            this.btnRemove.AutoSize = false;
            this.btnRemove.AutoToolTip = false;
            this.btnRemove.Image = ( ( System.Drawing.Image ) ( resources.GetObject("btnRemove.Image") ) );
            this.btnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(60, 49);
            this.btnRemove.Text = "Remove";
            this.btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = false;
            this.btnEdit.AutoToolTip = false;
            this.btnEdit.Image = ( ( System.Drawing.Image ) ( resources.GetObject("btnEdit.Image") ) );
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(60, 49);
            this.btnEdit.Text = "Edit";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnCopy
            // 
            this.btnCopy.AutoSize = false;
            this.btnCopy.AutoToolTip = false;
            this.btnCopy.Image = ( ( System.Drawing.Image ) ( resources.GetObject("btnCopy.Image") ) );
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(60, 49);
            this.btnCopy.Text = "Copy &To";
            this.btnCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnRead
            // 
            this.btnRead.AutoSize = false;
            this.btnRead.AutoToolTip = false;
            this.btnRead.Image = ( ( System.Drawing.Image ) ( resources.GetObject("btnRead.Image") ) );
            this.btnRead.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(60, 49);
            this.btnRead.Text = "Read";
            this.btnRead.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnOptions
            // 
            this.btnOptions.AutoSize = false;
            this.btnOptions.AutoToolTip = false;
            this.btnOptions.Image = ( ( System.Drawing.Image ) ( resources.GetObject("btnOptions.Image") ) );
            this.btnOptions.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(60, 49);
            this.btnOptions.Text = "Options";
            this.btnOptions.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.Control;
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(527, 52);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 434);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.SystemColors.Control;
            this.splitter2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 333);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(527, 3);
            this.splitter2.TabIndex = 4;
            this.splitter2.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 52);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(527, 281);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // bookPanel1
            // 
            this.bookPanel1.BackColor = System.Drawing.Color.PeachPuff;
            this.bookPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bookPanel1.Location = new System.Drawing.Point(0, 336);
            this.bookPanel1.Name = "bookPanel1";
            this.bookPanel1.Size = new System.Drawing.Size(527, 150);
            this.bookPanel1.TabIndex = 3;
            // 
            // filterPanel
            // 
            this.filterPanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.filterPanel.Location = new System.Drawing.Point(530, 52);
            this.filterPanel.MinimumSize = new System.Drawing.Size(158, 360);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(158, 434);
            this.filterPanel.TabIndex = 1;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(688, 486);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.bookPanel1);
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
        private BookPanel bookPanel1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStripSplitButton btnAdd;
        private System.Windows.Forms.ToolStripMenuItem addFromUrlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnRemove;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.ToolStripButton btnRead;
        private System.Windows.Forms.ToolStripButton btnOptions;
    }
}

