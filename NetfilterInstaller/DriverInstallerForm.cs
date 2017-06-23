using System;
using System.Windows.Forms;

namespace NetfilterInstaller
{
    public partial class DriverInstallerForm : Form
    {
        public DriverInstallerForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (OsProxy.CurrentUserIsAdministrator())
            {
                MessageBox.Show("To continue installing, please run this program as Administrator", 
                    "Installation failed..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void startInstallButton_Click(object sender, EventArgs e)
        {
            string errorMsg = "";
            DriverInstaller.DriverType driverType = (wfpDriverRadioButton.Checked) ?
                DriverInstaller.DriverType.WFP : DriverInstaller.DriverType.TDI;

            if (DriverInstaller.InstallDriver(ref errorMsg, driverType))
            {
                MessageBox.Show("Driver successfully installed!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(string.Format("Couldn't install driver..\r\nError: {0}", errorMsg), "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Close();
        }
    }
}
