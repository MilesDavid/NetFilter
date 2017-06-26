using System;
using System.Windows.Forms;

namespace NetfilterInstaller
{
    public partial class DriverInstallerForm : Form
    {
        enum FormStatus
        {
            Install = 100,
            Uninstall = 200
        }

        public DriverInstallerForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!OsProxy.CurrentUserIsAdministrator())
            {
                MessageBox.Show("To continue installing, please run this program as Administrator",
                    "Installation failed..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            string errMsg = string.Empty;
            if (DriverInstaller.DriverAlreadyExits(ref errMsg))
            {
                UpdateForm(FormStatus.Uninstall);
            }

            if (errMsg != string.Empty)
            {
                MessageBox.Show(errMsg,
                    "Installation failed..", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActionButton_Click(object sender, EventArgs e)
        {
            string errMsg = string.Empty;
            if (DriverInstaller.DriverAlreadyExits(ref errMsg))
            {
                string errorMsg = string.Empty;
                if (DriverInstaller.DeleteDriver(ref errorMsg))
                {
                    MessageBox.Show(
                        "Netfilter successfully uninstalled!", string.Empty,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        string.Format("Couldn't uninstall Netfilter..\r\nError: {0}", errorMsg),
                        "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string errorMsg = string.Empty;
                DriverInstaller.DriverType driverType = (wfpDriverRadioButton.Checked) ?
                    DriverInstaller.DriverType.WFP : DriverInstaller.DriverType.TDI;

                if (DriverInstaller.InstallDriver(ref errorMsg, driverType))
                {
                    MessageBox.Show(
                        "Netfilter successfully installed!", string.Empty,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        string.Format("Couldn't install Netfilter..\r\nError: {0}", errorMsg),
                        "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (errMsg != string.Empty)
            {
                MessageBox.Show(
                    string.Format("Couldn't install Netfilter..\r\nError: {0}", errMsg),
                    "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Close();
        }

        private void UpdateForm(FormStatus status)
        {
            switch (status)
            {
                case FormStatus.Install:
                    break;
                case FormStatus.Uninstall:
                    driverChooseGroupBox.Visible = false;
                    ClientSize = new System.Drawing.Size(
                         actionButton.Width + 10,
                         actionButton.Height + 10);
                    actionButton.Left = (ClientSize.Width - actionButton.Width) / 2;
                    actionButton.Top = (ClientSize.Height - actionButton.Height) / 2;
                    actionButton.Text = "Uninstall";

                    break;
            }
        }
    }
}
