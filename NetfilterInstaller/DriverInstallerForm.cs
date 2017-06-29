#if DEBUG
//#define INSTALL_ONLY_TDI
//#define DISABLE_ADMIN_RIGHTS
#endif

using System;
using System.Diagnostics;
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
#if ! DISABLE_ADMIN_RIGHTS
            if (!OsProxy.CurrentUserIsAdministrator())
            {
                MessageBox.Show("To continue installing, please run this program as Administrator",
                    "Installation failed..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();

                return;
            }
#endif
            FormStatus status = (DriverInstaller.Installed()) ?
                FormStatus.Uninstall : FormStatus.Install;

            UpdateForm(status);
        }

        private void ActionButton_Click(object sender, EventArgs e)
        {
            if (DriverInstaller.Installed())
            {
                string errorMsg = string.Empty;
                if (DriverInstaller.DeleteDriver(ref errorMsg))
                {
                    DialogResult dialogResult = MessageBox.Show(
                        "Netfilter successfully uninstalled!\n" +
                        "For complete uninstallation require reboot.\nReboot now?", "",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        Process.Start("shutdown", "/r /t 10");
                    }
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

            Close();
        }

        private void UpdateForm(FormStatus status)
        {
            switch (status)
            {
                case FormStatus.Install:
#if INSTALL_ONLY_TDI
                    driverChooseGroupBox.Visible = false;
                    tdiDriverRadioButton.Checked = true;
                    ClientSize = new System.Drawing.Size(
                         actionButton.Width + 10,
                         actionButton.Height + 10);
                    actionButton.Left = (ClientSize.Width - actionButton.Width) / 2;
                    actionButton.Top = (ClientSize.Height - actionButton.Height) / 2;
                    actionButton.Text = "Install";
#endif
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
