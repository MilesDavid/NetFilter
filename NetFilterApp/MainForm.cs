using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace NetFilterApp
{
    public partial class MainForm : Form
    {
        #region Fields

        IntPtr netMon;
        bool netMonStarted;
        bool isInit;
        const string configPath = "settings.conf";

        Settings settings;
        Logger logger;
        TreeNode root;

        #endregion

        enum NetFilterStatus
        {
            NotStarted,
            Ready,
            Started,
            Stopped,
            Failed
        }

        bool directoryIsChild(string expectedChild, ref TreeNode parent)
        {
            bool result = false;
            parent = null;

            TreeNode[] foundNodes = null;
            while (expectedChild != null)
            {
                foundNodes = root.Nodes.Find(expectedChild, true);
                if (foundNodes.Length > 0)
                {
                    parent = foundNodes[0];
                    return true;
                }
                expectedChild = Path.GetDirectoryName(expectedChild);
            }

            return result;
        }

        void RemoveEmptyDirectoryNode(TreeNode node, ref List<TreeNode> toRemoveNodeList)
        {
            foreach (TreeNode childNode in node.Nodes)
            {
                RemoveEmptyDirectoryNode(childNode, ref toRemoveNodeList);
                bool endWithExeExt = childNode.Name.EndsWith(".exe");
                if (!endWithExeExt && childNode.Nodes.Count == 0)
                {
                    toRemoveNodeList.Add(childNode);
                }
            }
        }

        void AddDirectory(string directoryPath)
        {
            try
            {
                TreeNode[] foundNodes = root.Nodes.Find(directoryPath, true);
                //if (foundNodes.Length == 0)
                //{
                    TreeNode parentNode = null, currentNode = root;

                    string textNode = directoryPath;
                    if (directoryIsChild(directoryPath, ref currentNode))
                    {
                        textNode = Path.GetFileName(directoryPath);
                    }
                    else
                    {
                        currentNode = (foundNodes.Length > 0) ? foundNodes[0] : root;
                    }
                    parentNode = currentNode.Nodes.Add(directoryPath, textNode);

                    foreach (var subdir in
                        Directory.GetDirectories(directoryPath, "*", SearchOption.TopDirectoryOnly))
                    {
                        AddDirectory(subdir);
                    }

                    foreach (var process in
                        Directory.GetFiles(directoryPath, "*.exe", SearchOption.TopDirectoryOnly))
                    {
                        AddProcess(process, parentNode);
                    }
                //}
            }
            catch(Exception e)
            {
                logger.write(e.Message);
            }
        }

        void AddProcess(string processPath, TreeNode parent=null)
        {
            TreeNode[] foundNodes = root.Nodes.Find(processPath, true);
            if (foundNodes.Length == 0)
            {
                string dirName = Path.GetDirectoryName(processPath);
                string fileName = Path.GetFileName(processPath);

                settings.addTracingProcess(processPath);
                if (foundNodes.Length == 0)
                {
                    if (parent == null)
                    {
                        TreeNode foundParentNode= null;
                        parent = (directoryIsChild(dirName, ref foundParentNode)) ?  
                            foundParentNode : root.Nodes.Add(dirName, dirName);
                    }
                    parent.Nodes.Add(processPath, fileName);
                }
            }
        }

        bool RefreshSettings()
        {
            bool settingsWrited = settings.write();
            if (settingsWrited)
            {
                logger.write("Couldn't refresh settings");
            }
            else
            {
                NetFilterWrap.NetMonRefreshSetting(netMon);
                logger.write("Settings has been refreshed..");
            }

            return settingsWrited;
        }

        void InitializeFields()
        {
            logger = new Logger();

            netMon = IntPtr.Zero;
            netMonStarted = false;
            isInit = false;
            settings = new Settings(configPath, logger);

            root = filteredAppsTreeView.Nodes.Add("root", Environment.MachineName);
        }

        #region MainForm handlers

        public MainForm()
        {
            InitializeComponent();
            InitializeFields();

            netMon = NetFilterWrap.NetMonCreate();
            isInit = (netMon != IntPtr.Zero);
        }

        ~MainForm()
        {
            if (netMonStarted)
            {
                NetFilterWrap.NetMonStop(netMon);
            }

            if (netMon != null)
            {
                NetFilterWrap.NetMonFree(netMon);
            }

            netMon = IntPtr.Zero;
        }

        void updateFormItems(NetFilterStatus status)
        {
            if (status == NetFilterStatus.Started)
            {
                stopFilterButton.Enabled = true;
                refreshSettingsButton.Enabled = true;

                addFolderButton.Enabled = false;
                addAppButton.Enabled = false;
                startFilterButton.Enabled = false;
            }
            else
            {
                startFilterButton.Enabled = true;
                addFolderButton.Enabled = true;
                addAppButton.Enabled = true;

                stopFilterButton.Enabled = false;
                refreshSettingsButton.Enabled = false;
            }

            switch (status)
            {
                case NetFilterStatus.NotStarted:
                    startFilterButton.Enabled = false;
                    break;

                case NetFilterStatus.Ready:
                    filterStatusLabel.Text = "Filter ready to start";
                    root.ExpandAll();
                    break;

                case NetFilterStatus.Started:
                    filterStatusLabel.Text = "Filter started..";
                    break;

                case NetFilterStatus.Stopped:
                    filterStatusLabel.Text = "Filter stopped..";
                    break;

                case NetFilterStatus.Failed:
                    filterStatusLabel.Text = "Filter fails during start..";
                    break;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!isInit)
            {
                // write to log
                logger.write("Couldn't create netMon instance");
                MessageBox.Show("Couldn't create netMon instance", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            updateFormItems(NetFilterStatus.NotStarted);

            // Read settings
            if (!settings.read())
            {
                // write to log
                logger.write("Couldn't read settings");
                return;
            }

            settingsStatusLabel.Text = "Settings readed..";
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isInit)
            {
                DialogResult dialogResult = MessageBox.Show("Do you wanna exit?", "Quit",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region button handlers

        private void startStopFilterButton_Click(object sender, EventArgs e)
        {
            if (RefreshSettings() && NetFilterWrap.NetMonStart(netMon))
            {
                updateFormItems(NetFilterStatus.Started);
                logger.write("Netfilter started..");
            }
            else
            {
                updateFormItems(NetFilterStatus.Failed);
                logger.write("Error during starting netfilter..");
            }
        }

        private void stopFilterButton_Click(object sender, EventArgs e)
        {
            if (NetFilterWrap.NetMonIsStarted(netMon))
            {
                NetFilterWrap.NetMonStop(netMon);
                updateFormItems(NetFilterStatus.Stopped);

                logger.write("Netfilter has been stopped..");
            }
            else
            {
                logger.write("Netfilter is not started..");
            }
        }

        private void addFolderButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                AddDirectory(folderBrowserDialog.SelectedPath);
                updateFormItems(NetFilterStatus.Ready);
            }
        }

        private void addAppButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog.ShowDialog();
            {
                if (dialogResult == DialogResult.OK)
                {
                    foreach (var process in openFileDialog.FileNames)
                    {
                        AddProcess(process);
                        updateFormItems(NetFilterStatus.Ready);
                    }
                }
            }
        }

        private void refreshSettingsButton_Click(object sender, EventArgs e)
        {
            RefreshSettings();
        }

        #endregion

        #region Tree view handlers

        private void filteredAppsTreeView_DragDrop(object sender, DragEventArgs e)
        {
            string[] draggedItems = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            foreach (string item in draggedItems)
            {
                FileAttributes attr = File.GetAttributes(item);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    AddDirectory(item);
                    updateFormItems(NetFilterStatus.Ready);
                }
                else if (Path.GetExtension(item) == ".exe")
                {
                    if (!settings.isExistsTracingProcess(item))
                    {
                        AddProcess(item);
                        updateFormItems(NetFilterStatus.Ready);
                    }
                }
            }

            List<TreeNode> toRemove = new List<TreeNode>();
            RemoveEmptyDirectoryNode(root, ref toRemove);
            while (toRemove.Count > 0)
            {
                foreach (var removeItem in toRemove)
                {
                    removeItem.Remove();
                }
                toRemove.Clear();
                RemoveEmptyDirectoryNode(root, ref toRemove);
            }
        }

        private void filteredAppsTreeView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }

        }

        private void filteredAppsTreeView_DragLeave(object sender, EventArgs e)
        {
        }

        private void filteredAppsTreeView_DragOver(object sender, DragEventArgs e)
        {
        }

        #endregion
    }
}
