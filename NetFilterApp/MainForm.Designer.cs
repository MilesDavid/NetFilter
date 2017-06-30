using System;

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
                #region Custom disposing
                if (netMonStarted)
                {
                    try
                    {
                        NetFilterWrap.Stop(netMon);
                    }
                    catch (Exception e)
                    {
                        logger.write(string.Format("{0} {1}", e.GetType(), e.Message));
                    }
                }

                if (netMon != null)
                {
                    try
                    {
                        NetFilterWrap.Free(netMon);
                    }
                    catch (Exception e)
                    {
                        logger.write(string.Format("{0} {1}", e.GetType(), e.Message));
                    }
                }

                netMon = IntPtr.Zero;
                #endregion

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
            this.dumpsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearDumpsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.httpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearHttpRequestFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearHttpResponseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearHttpAllFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearRawInFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearRawOutFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearRawAllFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllDumpsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abclearRawOutFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filteredAppsTreeViewContainerGroupBox = new System.Windows.Forms.GroupBox();
            this.actionButtonsContainerPanel = new System.Windows.Forms.Panel();
            this.mainStatusStrip.SuspendLayout();
            this.folderContextMenuStrip.SuspendLayout();
            this.fileContextMenuStrip.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.filteredAppsTreeViewContainerGroupBox.SuspendLayout();
            this.actionButtonsContainerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // filteredAppsTreeView
            // 
            this.filteredAppsTreeView.AllowDrop = true;
            this.filteredAppsTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.filteredAppsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filteredAppsTreeView.Location = new System.Drawing.Point(3, 16);
            this.filteredAppsTreeView.Name = "filteredAppsTreeView";
            this.filteredAppsTreeView.Size = new System.Drawing.Size(405, 184);
            this.filteredAppsTreeView.TabIndex = 0;
            this.filteredAppsTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.filteredAppsTreeView_NodeMouseClick);
            this.filteredAppsTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.filteredAppsTreeView_DragDrop);
            this.filteredAppsTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.filteredAppsTreeView_DragEnter);
            // 
            // startFilterButton
            // 
            this.startFilterButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startFilterButton.Location = new System.Drawing.Point(6, 3);
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
            this.stopFilterButton.Location = new System.Drawing.Point(87, 3);
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
            this.addFolderButton.Location = new System.Drawing.Point(168, 3);
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
            this.addAppButton.Location = new System.Drawing.Point(249, 3);
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
            this.refreshSettingsButton.Location = new System.Drawing.Point(330, 3);
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
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 337);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(484, 24);
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
            this.mainMenuStrip.Size = new System.Drawing.Size(484, 24);
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
            this.dumpsToolStripMenuItem,
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
            this.applicationLogToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.applicationLogToolStripMenuItem.Text = "Application";
            this.applicationLogToolStripMenuItem.Click += new System.EventHandler(this.applicationLogToolStripMenuItem_Click);
            // 
            // netfilterLogToolStripMenuItem
            // 
            this.netfilterLogToolStripMenuItem.Name = "netfilterLogToolStripMenuItem";
            this.netfilterLogToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
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
            // dumpsToolStripMenuItem
            // 
            this.dumpsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearDumpsToolStripMenuItem});
            this.dumpsToolStripMenuItem.Name = "dumpsToolStripMenuItem";
            this.dumpsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dumpsToolStripMenuItem.Text = "Dumps";
            // 
            // clearDumpsToolStripMenuItem
            // 
            this.clearDumpsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.httpToolStripMenuItem,
            this.rawToolStripMenuItem,
            this.clearAllDumpsToolStripMenuItem});
            this.clearDumpsToolStripMenuItem.Name = "clearDumpsToolStripMenuItem";
            this.clearDumpsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearDumpsToolStripMenuItem.Text = "Clear";
            // 
            // httpToolStripMenuItem
            // 
            this.httpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearHttpRequestFolderToolStripMenuItem,
            this.clearHttpResponseToolStripMenuItem,
            this.clearHttpAllFoldersToolStripMenuItem});
            this.httpToolStripMenuItem.Name = "httpToolStripMenuItem";
            this.httpToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.httpToolStripMenuItem.Text = "Http";
            // 
            // clearHttpRequestFolderToolStripMenuItem
            // 
            this.clearHttpRequestFolderToolStripMenuItem.Name = "clearHttpRequestFolderToolStripMenuItem";
            this.clearHttpRequestFolderToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearHttpRequestFolderToolStripMenuItem.Text = "Request";
            this.clearHttpRequestFolderToolStripMenuItem.Click += new System.EventHandler(this.clearHttpRequestFolderToolStripMenuItem_Click);
            // 
            // clearHttpResponseToolStripMenuItem
            // 
            this.clearHttpResponseToolStripMenuItem.Name = "clearHttpResponseToolStripMenuItem";
            this.clearHttpResponseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearHttpResponseToolStripMenuItem.Text = "Response";
            this.clearHttpResponseToolStripMenuItem.Click += new System.EventHandler(this.clearHttpResponseToolStripMenuItem_Click);
            // 
            // clearHttpAllFoldersToolStripMenuItem
            // 
            this.clearHttpAllFoldersToolStripMenuItem.Name = "clearHttpAllFoldersToolStripMenuItem";
            this.clearHttpAllFoldersToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearHttpAllFoldersToolStripMenuItem.Text = "All";
            this.clearHttpAllFoldersToolStripMenuItem.Click += new System.EventHandler(this.clearHttpAllFoldersToolStripMenuItem_Click);
            // 
            // rawToolStripMenuItem
            // 
            this.rawToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearRawInFolderToolStripMenuItem,
            this.clearRawOutFolderToolStripMenuItem,
            this.clearRawAllFoldersToolStripMenuItem});
            this.rawToolStripMenuItem.Name = "rawToolStripMenuItem";
            this.rawToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rawToolStripMenuItem.Text = "Raw";
            // 
            // clearRawInFolderToolStripMenuItem
            // 
            this.clearRawInFolderToolStripMenuItem.Name = "clearRawInFolderToolStripMenuItem";
            this.clearRawInFolderToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearRawInFolderToolStripMenuItem.Text = "In";
            this.clearRawInFolderToolStripMenuItem.Click += new System.EventHandler(this.clearRawInFolderToolStripMenuItem_Click);
            // 
            // clearRawOutFolderToolStripMenuItem
            // 
            this.clearRawOutFolderToolStripMenuItem.Name = "clearRawOutFolderToolStripMenuItem";
            this.clearRawOutFolderToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearRawOutFolderToolStripMenuItem.Text = "Out";
            this.clearRawOutFolderToolStripMenuItem.Click += new System.EventHandler(this.clearRawOutFolderToolStripMenuItem_Click);
            // 
            // clearRawAllFoldersToolStripMenuItem
            // 
            this.clearRawAllFoldersToolStripMenuItem.Name = "clearRawAllFoldersToolStripMenuItem";
            this.clearRawAllFoldersToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearRawAllFoldersToolStripMenuItem.Text = "All";
            this.clearRawAllFoldersToolStripMenuItem.Click += new System.EventHandler(this.clearRawAllFoldersToolStripMenuItem_Click);
            // 
            // clearAllDumpsToolStripMenuItem
            // 
            this.clearAllDumpsToolStripMenuItem.Name = "clearAllDumpsToolStripMenuItem";
            this.clearAllDumpsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearAllDumpsToolStripMenuItem.Text = "All";
            this.clearAllDumpsToolStripMenuItem.Click += new System.EventHandler(this.clearAllDumpsToolStripMenuItem_Click);
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
            this.abclearRawOutFolderToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // abclearRawOutFolderToolStripMenuItem
            // 
            this.abclearRawOutFolderToolStripMenuItem.Name = "abclearRawOutFolderToolStripMenuItem";
            this.abclearRawOutFolderToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.abclearRawOutFolderToolStripMenuItem.Text = "About..";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // filteredAppsTreeViewContainerGroupBox
            // 
            this.filteredAppsTreeViewContainerGroupBox.Controls.Add(this.filteredAppsTreeView);
            this.filteredAppsTreeViewContainerGroupBox.Location = new System.Drawing.Point(8, 27);
            this.filteredAppsTreeViewContainerGroupBox.Name = "filteredAppsTreeViewContainerGroupBox";
            this.filteredAppsTreeViewContainerGroupBox.Size = new System.Drawing.Size(411, 203);
            this.filteredAppsTreeViewContainerGroupBox.TabIndex = 8;
            this.filteredAppsTreeViewContainerGroupBox.TabStop = false;
            this.filteredAppsTreeViewContainerGroupBox.Text = "Filtered apps";
            // 
            // actionButtonsContainerPanel
            // 
            this.actionButtonsContainerPanel.BackColor = System.Drawing.SystemColors.Control;
            this.actionButtonsContainerPanel.Controls.Add(this.startFilterButton);
            this.actionButtonsContainerPanel.Controls.Add(this.stopFilterButton);
            this.actionButtonsContainerPanel.Controls.Add(this.addFolderButton);
            this.actionButtonsContainerPanel.Controls.Add(this.addAppButton);
            this.actionButtonsContainerPanel.Controls.Add(this.refreshSettingsButton);
            this.actionButtonsContainerPanel.Location = new System.Drawing.Point(8, 236);
            this.actionButtonsContainerPanel.Name = "actionButtonsContainerPanel";
            this.actionButtonsContainerPanel.Size = new System.Drawing.Size(411, 45);
            this.actionButtonsContainerPanel.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.actionButtonsContainerPanel);
            this.Controls.Add(this.filteredAppsTreeViewContainerGroupBox);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "Network Filter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.folderContextMenuStrip.ResumeLayout(false);
            this.fileContextMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.filteredAppsTreeViewContainerGroupBox.ResumeLayout(false);
            this.actionButtonsContainerPanel.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem abclearRawOutFolderToolStripMenuItem;
        private System.Windows.Forms.GroupBox filteredAppsTreeViewContainerGroupBox;
        private System.Windows.Forms.Panel actionButtonsContainerPanel;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dumpsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearDumpsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem httpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearHttpRequestFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearHttpResponseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearHttpAllFoldersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearRawInFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearRawOutFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearRawAllFoldersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllDumpsToolStripMenuItem;
    }
}

