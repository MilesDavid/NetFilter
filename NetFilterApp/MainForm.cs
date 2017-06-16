using System;
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
            Started,
            Stopped,
            Failed
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

            root = filteredAppsTreeView.Nodes.Find("root", true)[0];
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
            bool started = status == NetFilterStatus.Started;

            switch (status)
            {
                case NetFilterStatus.Started:
                    filterStatusLabel.Text = "Filter started..";
                    break;

                case NetFilterStatus.Stopped:
                    filterStatusLabel.Text = "Filter stopped..";
                    break;

                case NetFilterStatus.NotStarted:
                    break;

                case NetFilterStatus.Failed:
                    filterStatusLabel.Text = "Filter fails during start..";
                    break;
            }

            if (started)
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

        private void startFilterButton_Click(object sender, EventArgs e)
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

        }

        private void addAppButton_Click(object sender, EventArgs e)
        {

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
                if (settings.isExistsTracingProcess(item)) {
                    FileAttributes attr = File.GetAttributes(item);
                    if (attr == FileAttributes.Directory)
                    {
                        //MessageBox.Show(string.Format("{0} is directory", item));
                        //recursive adding
                        //filteredAppsTreeView.Nodes.Add(item);
                        //settings.addTracingProcess()
                    }
                    else if (Path.GetExtension(item) == ".exe")
                    {
                        string dirName = Path.GetDirectoryName(item);
                        string fileName = Path.GetFileName(item);

                        settings.addTracingProcess(item);
                        TreeNode node = null;
                        if (!root.Nodes.ContainsKey(dirName))
                        {
                            node = root.Nodes.Add(dirName, dirName);
                            node.Nodes.Add(fileName);
                        }
                        else
                        {
                            int i = root.Nodes.IndexOfKey(dirName);
                            root.Nodes[i].Nodes.Add(item, fileName);
                        }
                    }
                }
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
            //e.Effect = DragDropEffects.None;
        }

        #endregion
    }
}
