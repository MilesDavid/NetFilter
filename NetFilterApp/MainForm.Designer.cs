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
            this.components = new System.ComponentModel.Container();
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
            this.folderContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDeleteFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.fileContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDeleteFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.netfilterToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateSelfsignedCertificateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.netfilterLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearProcessesListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCatalogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAppToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStatusStrip.SuspendLayout();
            this.folderContextMenuStrip.SuspendLayout();
            this.fileContextMenuStrip.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // filteredAppsTreeView
            // 
            this.filteredAppsTreeView.AllowDrop = true;
            this.filteredAppsTreeView.Location = new System.Drawing.Point(12, 26);
            this.filteredAppsTreeView.Name = "filteredAppsTreeView";
            this.filteredAppsTreeView.Size = new System.Drawing.Size(399, 194);
            this.filteredAppsTreeView.TabIndex = 0;
            this.filteredAppsTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.filteredAppsTreeView_NodeMouseClick);
            this.filteredAppsTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.filteredAppsTreeView_DragDrop);
            this.filteredAppsTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.filteredAppsTreeView_DragEnter);
            // 
            // startFilterButton
            // 
            this.startFilterButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startFilterButton.Location = new System.Drawing.Point(12, 226);
            this.startFilterButton.Name = "startFilterButton";
            this.startFilterButton.Size = new System.Drawing.Size(75, 37);
            this.startFilterButton.TabIndex = 1;
            this.startFilterButton.Text = "Start";
            this.startFilterButton.UseVisualStyleBackColor = true;
            this.startFilterButton.Click += new System.EventHandler(this.startFilterButton_Click);
            // 
            // stopFilterButton
            // 
            this.stopFilterButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stopFilterButton.Location = new System.Drawing.Point(93, 226);
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
            this.addFolderButton.Location = new System.Drawing.Point(174, 226);
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
            this.addAppButton.Location = new System.Drawing.Point(255, 226);
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
            this.refreshSettingsButton.Location = new System.Drawing.Point(336, 226);
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
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 275);
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
            // folderContextMenuStrip
            // 
            this.folderContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDeleteFolder});
            this.folderContextMenuStrip.Name = "folderContextMenuStrip";
            this.folderContextMenuStrip.Size = new System.Drawing.Size(157, 26);
            this.folderContextMenuStrip.Text = "Folder menu";
            // 
            // toolStripMenuItemDeleteFolder
            // 
            this.toolStripMenuItemDeleteFolder.Name = "toolStripMenuItemDeleteFolder";
            this.toolStripMenuItemDeleteFolder.Size = new System.Drawing.Size(156, 22);
            this.toolStripMenuItemDeleteFolder.Text = "Exclude catalog";
            this.toolStripMenuItemDeleteFolder.Click += new System.EventHandler(this.toolStripMenuItemDeleteFolder_Click);
            // 
            // fileContextMenuStrip
            // 
            this.fileContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDeleteFile});
            this.fileContextMenuStrip.Name = "folderContextMenuStrip";
            this.fileContextMenuStrip.Size = new System.Drawing.Size(158, 26);
            // 
            // toolStripMenuItemDeleteFile
            // 
            this.toolStripMenuItemDeleteFile.Name = "toolStripMenuItemDeleteFile";
            this.toolStripMenuItemDeleteFile.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItemDeleteFile.Text = "Exclude process";
            this.toolStripMenuItemDeleteFile.Click += new System.EventHandler(this.toolStripMenuItemDeleteFile_Click);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(425, 24);
            this.mainMenuStrip.TabIndex = 7;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.netfilterToolStripMenuItem1,
            this.settingsToolStripMenuItem,
            this.readLogsToolStripMenuItem,
            this.clearProcessesListToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // netfilterToolStripMenuItem1
            // 
            this.netfilterToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.refreshSettingsToolStripMenuItem});
            this.netfilterToolStripMenuItem1.Name = "netfilterToolStripMenuItem1";
            this.netfilterToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.netfilterToolStripMenuItem1.Text = "Netfilter";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // refreshSettingsToolStripMenuItem
            // 
            this.refreshSettingsToolStripMenuItem.Name = "refreshSettingsToolStripMenuItem";
            this.refreshSettingsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.refreshSettingsToolStripMenuItem.Text = "Refresh settings";
            this.refreshSettingsToolStripMenuItem.Click += new System.EventHandler(this.refreshSettingsToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateSelfsignedCertificateToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // generateSelfsignedCertificateToolStripMenuItem
            // 
            this.generateSelfsignedCertificateToolStripMenuItem.Name = "generateSelfsignedCertificateToolStripMenuItem";
            this.generateSelfsignedCertificateToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.generateSelfsignedCertificateToolStripMenuItem.Text = "Generate self-signed certificate";
            this.generateSelfsignedCertificateToolStripMenuItem.Click += new System.EventHandler(this.generateSelfsignedCertificateToolStripMenuItem_Click);
            // 
            // readLogsToolStripMenuItem
            // 
            this.readLogsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationLogToolStripMenuItem,
            this.netfilterLogToolStripMenuItem});
            this.readLogsToolStripMenuItem.Name = "readLogsToolStripMenuItem";
            this.readLogsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.readLogsToolStripMenuItem.Text = "Read logs";
            // 
            // applicationLogToolStripMenuItem
            // 
            this.applicationLogToolStripMenuItem.Name = "applicationLogToolStripMenuItem";
            this.applicationLogToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.applicationLogToolStripMenuItem.Text = "Application";
            this.applicationLogToolStripMenuItem.Click += new System.EventHandler(this.applicationLogToolStripMenuItem_Click);
            // 
            // netfilterLogToolStripMenuItem
            // 
            this.netfilterLogToolStripMenuItem.Name = "netfilterLogToolStripMenuItem";
            this.netfilterLogToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.netfilterLogToolStripMenuItem.Text = "Netfilter";
            this.netfilterLogToolStripMenuItem.Click += new System.EventHandler(this.netfilterLogToolStripMenuItem_Click);
            // 
            // clearProcessesListToolStripMenuItem
            // 
            this.clearProcessesListToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCatalogToolStripMenuItem,
            this.addAppToolStripMenuItem,
            this.clearListToolStripMenuItem});
            this.clearProcessesListToolStripMenuItem.Name = "clearProcessesListToolStripMenuItem";
            this.clearProcessesListToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearProcessesListToolStripMenuItem.Text = "Process list";
            // 
            // addCatalogToolStripMenuItem
            // 
            this.addCatalogToolStripMenuItem.Name = "addCatalogToolStripMenuItem";
            this.addCatalogToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.addCatalogToolStripMenuItem.Text = "Add catalog";
            this.addCatalogToolStripMenuItem.Click += new System.EventHandler(this.addCatalogToolStripMenuItem_Click);
            // 
            // addAppToolStripMenuItem
            // 
            this.addAppToolStripMenuItem.Name = "addAppToolStripMenuItem";
            this.addAppToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.addAppToolStripMenuItem.Text = "Add app";
            this.addAppToolStripMenuItem.Click += new System.EventHandler(this.addAppToolStripMenuItem_Click);
            // 
            // clearListToolStripMenuItem
            // 
            this.clearListToolStripMenuItem.Name = "clearListToolStripMenuItem";
            this.clearListToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.clearListToolStripMenuItem.Text = "Clear list";
            this.clearListToolStripMenuItem.Click += new System.EventHandler(this.clearListToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.aboutToolStripMenuItem.Text = "About..";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 299);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.Controls.Add(this.refreshSettingsButton);
            this.Controls.Add(this.addAppButton);
            this.Controls.Add(this.addFolderButton);
            this.Controls.Add(this.stopFilterButton);
            this.Controls.Add(this.startFilterButton);
            this.Controls.Add(this.filteredAppsTreeView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "Network Filter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.folderContextMenuStrip.ResumeLayout(false);
            this.fileContextMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
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
        private System.Windows.Forms.ContextMenuStrip folderContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteFolder;
        private System.Windows.Forms.ContextMenuStrip fileContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteFile;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem netfilterToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateSelfsignedCertificateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readLogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem netfilterLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearProcessesListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCatalogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAppToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

