namespace SaveSoul
{
    partial class DropBoxMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DropBoxMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.archieveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.revokeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.darkSoulsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkSoulsIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkSoulsIIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sekiroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtMode = new System.Windows.Forms.TextBox();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.txtComboValue = new System.Windows.Forms.TextBox();
            this.listView2 = new System.Windows.Forms.ListView();
            this.txtListItem = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem,
            this.backupToolStripMenuItem1,
            this.aboutToolStripMenuItem,
            this.logoutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1289, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archieveToolStripMenuItem,
            this.revokeToolStripMenuItem});
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.preferencesToolStripMenuItem.Text = "System";
            // 
            // archieveToolStripMenuItem
            // 
            this.archieveToolStripMenuItem.Name = "archieveToolStripMenuItem";
            this.archieveToolStripMenuItem.Size = new System.Drawing.Size(168, 26);
            this.archieveToolStripMenuItem.Text = "Preferences";
            this.archieveToolStripMenuItem.Click += new System.EventHandler(this.archieveToolStripMenuItem_Click);
            // 
            // revokeToolStripMenuItem
            // 
            this.revokeToolStripMenuItem.Name = "revokeToolStripMenuItem";
            this.revokeToolStripMenuItem.Size = new System.Drawing.Size(168, 26);
            this.revokeToolStripMenuItem.Text = "Revoke";
            this.revokeToolStripMenuItem.Click += new System.EventHandler(this.revokeToolStripMenuItem_Click);
            // 
            // backupToolStripMenuItem1
            // 
            this.backupToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.darkSoulsToolStripMenuItem,
            this.darkSoulsIIToolStripMenuItem,
            this.darkSoulsIIIToolStripMenuItem,
            this.sekiroToolStripMenuItem});
            this.backupToolStripMenuItem1.Name = "backupToolStripMenuItem1";
            this.backupToolStripMenuItem1.Size = new System.Drawing.Size(71, 24);
            this.backupToolStripMenuItem1.Text = "Backup";
            // 
            // darkSoulsToolStripMenuItem
            // 
            this.darkSoulsToolStripMenuItem.Name = "darkSoulsToolStripMenuItem";
            this.darkSoulsToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.darkSoulsToolStripMenuItem.Text = "DarkSouls";
            this.darkSoulsToolStripMenuItem.Click += new System.EventHandler(this.darkSoulsToolStripMenuItem_Click);
            // 
            // darkSoulsIIToolStripMenuItem
            // 
            this.darkSoulsIIToolStripMenuItem.Name = "darkSoulsIIToolStripMenuItem";
            this.darkSoulsIIToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.darkSoulsIIToolStripMenuItem.Text = "DarkSouls II";
            this.darkSoulsIIToolStripMenuItem.Click += new System.EventHandler(this.darkSoulsIIToolStripMenuItem_Click);
            // 
            // darkSoulsIIIToolStripMenuItem
            // 
            this.darkSoulsIIIToolStripMenuItem.Name = "darkSoulsIIIToolStripMenuItem";
            this.darkSoulsIIIToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.darkSoulsIIIToolStripMenuItem.Text = "DarkSouls III";
            this.darkSoulsIIIToolStripMenuItem.Click += new System.EventHandler(this.darkSoulsIIIToolStripMenuItem_Click);
            // 
            // sekiroToolStripMenuItem
            // 
            this.sekiroToolStripMenuItem.Name = "sekiroToolStripMenuItem";
            this.sekiroToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.sekiroToolStripMenuItem.Text = "Sekiro";
            this.sekiroToolStripMenuItem.Click += new System.EventHandler(this.sekiroToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.logoutToolStripMenuItem.Text = "Rest";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(751, 54);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(86, 34);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // listView1
            // 
            this.listView1.AutoArrange = false;
            this.listView1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.listView1.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(26, 109);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1251, 564);
            this.listView1.TabIndex = 5;
            this.listView1.TileSize = new System.Drawing.Size(2, 2);
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "File List";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(469, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Filters:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "All",
            "DarkSouls",
            "DarkSouls II",
            "DarkSouls III",
            "Sekiro"});
            this.comboBox1.Location = new System.Drawing.Point(538, 58);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(167, 24);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // txtMode
            // 
            this.txtMode.Location = new System.Drawing.Point(193, 60);
            this.txtMode.Name = "txtMode";
            this.txtMode.Size = new System.Drawing.Size(100, 22);
            this.txtMode.TabIndex = 10;
            this.txtMode.Visible = false;
            // 
            // contextMenu
            // 
            this.contextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRestore,
            this.tsDelete});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(129, 52);
            this.contextMenu.Text = "Action";
            // 
            // tsRestore
            // 
            this.tsRestore.Name = "tsRestore";
            this.tsRestore.Size = new System.Drawing.Size(128, 24);
            this.tsRestore.Text = "Restore";
            this.tsRestore.Click += new System.EventHandler(this.tsDownload_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(128, 24);
            this.tsDelete.Text = "Delete";
            this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
            // 
            // txtComboValue
            // 
            this.txtComboValue.Location = new System.Drawing.Point(538, 30);
            this.txtComboValue.Name = "txtComboValue";
            this.txtComboValue.Size = new System.Drawing.Size(167, 22);
            this.txtComboValue.TabIndex = 12;
            this.txtComboValue.Visible = false;
            // 
            // listView2
            // 
            this.listView2.AutoArrange = false;
            this.listView2.BackColor = System.Drawing.SystemColors.HighlightText;
            this.listView2.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(26, 109);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(1251, 564);
            this.listView2.TabIndex = 14;
            this.listView2.TileSize = new System.Drawing.Size(2, 2);
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.List;
            this.listView2.Visible = false;
            this.listView2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView2_MouseClick);
            this.listView2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView2_MouseDoubleClick);
            // 
            // txtListItem
            // 
            this.txtListItem.Location = new System.Drawing.Point(847, 30);
            this.txtListItem.Name = "txtListItem";
            this.txtListItem.Size = new System.Drawing.Size(218, 22);
            this.txtListItem.TabIndex = 15;
            this.txtListItem.Visible = false;
            // 
            // DropBoxMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 703);
            this.Controls.Add(this.txtListItem);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.txtComboValue);
            this.Controls.Add(this.txtMode);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "DropBoxMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Soul Save";
            this.Activated += new System.EventHandler(this.DropBoxMain_Activated);
            this.Load += new System.EventHandler(this.DropBoxMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem archieveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem revokeToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem darkSoulsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkSoulsIIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkSoulsIIIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sekiroToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TextBox txtMode;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem tsRestore;
        private System.Windows.Forms.ToolStripMenuItem tsDelete;
        private System.Windows.Forms.TextBox txtComboValue;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.TextBox txtListItem;
        private System.Windows.Forms.Timer timer1;
    }
}