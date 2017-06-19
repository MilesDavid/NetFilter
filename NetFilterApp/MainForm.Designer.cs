namespace NetFilterApp
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.filteredAppsTreeView = new System.Windows.Forms.TreeView();
            this.startFilterButton = new System.Windows.Forms.Button();
            this.stopFilterButton = new System.Windows.Forms.Button();
            this.addFolderButton = new System.Windows.Forms.Button();
            this.addAppButton = new System.Windows.Forms.Button();
            this.refreshSettingsButton = new System.Windows.Forms.Button();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.filterStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.settingsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // filteredAppsTreeView
            // 
            this.filteredAppsTreeView.AllowDrop = true;
            this.filteredAppsTreeView.Location = new System.Drawing.Point(13, 13);
            this.filteredAppsTreeView.Name = "filteredAppsTreeView";
            this.filteredAppsTreeView.Size = new System.Drawing.Size(399, 194);
            this.filteredAppsTreeView.TabIndex = 0;
            this.filteredAppsTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.filteredAppsTreeView_DragDrop);
            this.filteredAppsTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.filteredAppsTreeView_DragEnter);
            this.filteredAppsTreeView.DragOver += new System.Windows.Forms.DragEventHandler(this.filteredAppsTreeView_DragOver);
            this.filteredAppsTreeView.DragLeave += new System.EventHandler(this.filteredAppsTreeView_DragLeave);
            // 
            // startFilterButton
            // 
            this.startFilterButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startFilterButton.Location = new System.Drawing.Point(13, 213);
            this.startFilterButton.Name = "startFilterButton";
            this.startFilterButton.Size = new System.Drawing.Size(75, 37);
            this.startFilterButton.TabIndex = 1;
            this.startFilterButton.Text = "Start";
            this.startFilterButton.UseVisualStyleBackColor = true;
            this.startFilterButton.Click += new System.EventHandler(this.startStopFilterButton_Click);
            // 
            // stopFilterButton
            // 
            this.stopFilterButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stopFilterButton.Location = new System.Drawing.Point(94, 213);
            this.stopFilterButton.Name = "stopFilterButton";
            this.stopFilterButton.Size = new System.Drawing.Size(75, 37);
            this.stopFilterButton.TabIndex = 2;
            this.stopFilterButton.Text = "Stop";
            this.stopFilterButton.UseVisualStyleBackColor = true;
            this.stopFilterButton.Click += new System.EventHandler(this.stopFilterButton_Click);
            // 
            // addFolderButton
            // 
            this.addFolderButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addFolderButton.Location = new System.Drawing.Point(175, 213);
            this.addFolderButton.Name = "addFolderButton";
            this.addFolderButton.Size = new System.Drawing.Size(75, 37);
            this.addFolderButton.TabIndex = 3;
            this.addFolderButton.Text = "Add catalog";
            this.addFolderButton.UseVisualStyleBackColor = true;
            this.addFolderButton.Click += new System.EventHandler(this.addFolderButton_Click);
            // 
            // addAppButton
            // 
            this.addAppButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addAppButton.Location = new System.Drawing.Point(256, 213);
            this.addAppButton.Name = "addAppButton";
            this.addAppButton.Size = new System.Drawing.Size(75, 37);
            this.addAppButton.TabIndex = 4;
            this.addAppButton.Text = "Add app";
            this.addAppButton.UseVisualStyleBackColor = true;
            this.addAppButton.Click += new System.EventHandler(this.addAppButton_Click);
            // 
            // refreshSettingsButton
            // 
            this.refreshSettingsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refreshSettingsButton.Location = new System.Drawing.Point(337, 213);
            this.refreshSettingsButton.Name = "refreshSettingsButton";
            this.refreshSettingsButton.Size = new System.Drawing.Size(75, 37);
            this.refreshSettingsButton.TabIndex = 5;
            this.refreshSettingsButton.Text = "Refresh settings";
            this.refreshSettingsButton.UseVisualStyleBackColor = true;
            this.refreshSettingsButton.Click += new System.EventHandler(this.refreshSettingsButton_Click);
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterStatusLabel,
            this.settingsStatusLabel});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 254);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(425, 24);
            this.mainStatusStrip.TabIndex = 6;
            this.mainStatusStrip.Text = "MAIN STATUS STRIP";
            // 
            // filterStatusLabel
            // 
            this.filterStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.filterStatusLabel.Name = "filterStatusLabel";
            this.filterStatusLabel.Size = new System.Drawing.Size(103, 19);
            this.filterStatusLabel.Text = "Filter not running";
            // 
            // settingsStatusLabel
            // 
            this.settingsStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.settingsStatusLabel.Name = "settingsStatusLabel";
            this.settingsStatusLabel.Size = new System.Drawing.Size(113, 19);
            this.settingsStatusLabel.Text = "Settings not readed";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "exe";
            this.openFileDialog.Filter = "Executable files|*.exe";
            this.openFileDialog.Multiselect = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 278);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.refreshSettingsButton);
            this.Controls.Add(this.addAppButton);
            this.Controls.Add(this.addFolderButton);
            this.Controls.Add(this.stopFilterButton);
            this.Controls.Add(this.startFilterButton);
            this.Controls.Add(this.filteredAppsTreeView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Network Filter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView filteredAppsTreeView;
        private System.Windows.Forms.Button startFilterButton;
        private System.Windows.Forms.Button stopFilterButton;
        private System.Windows.Forms.Button addFolderButton;
        private System.Windows.Forms.Button addAppButton;
        private System.Windows.Forms.Button refreshSettingsButton;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel filterStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel settingsStatusLabel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

