namespace NetfilterInstaller
{
    partial class DriverInstallerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DriverInstallerForm));
            this.wfpDriverRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tdiDriverRadioButton = new System.Windows.Forms.RadioButton();
            this.startInstallButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wfpDriverRadioButton
            // 
            this.wfpDriverRadioButton.AutoSize = true;
            this.wfpDriverRadioButton.Checked = true;
            this.wfpDriverRadioButton.Location = new System.Drawing.Point(6, 19);
            this.wfpDriverRadioButton.Name = "wfpDriverRadioButton";
            this.wfpDriverRadioButton.Size = new System.Drawing.Size(78, 17);
            this.wfpDriverRadioButton.TabIndex = 0;
            this.wfpDriverRadioButton.TabStop = true;
            this.wfpDriverRadioButton.Text = "WFP driver";
            this.wfpDriverRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tdiDriverRadioButton);
            this.groupBox1.Controls.Add(this.wfpDriverRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(174, 72);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select driver type for installation";
            // 
            // tdiDriverRadioButton
            // 
            this.tdiDriverRadioButton.AutoSize = true;
            this.tdiDriverRadioButton.Location = new System.Drawing.Point(6, 43);
            this.tdiDriverRadioButton.Name = "tdiDriverRadioButton";
            this.tdiDriverRadioButton.Size = new System.Drawing.Size(72, 17);
            this.tdiDriverRadioButton.TabIndex = 1;
            this.tdiDriverRadioButton.Text = "TDI driver";
            this.tdiDriverRadioButton.UseVisualStyleBackColor = true;
            // 
            // startInstallButton
            // 
            this.startInstallButton.Location = new System.Drawing.Point(12, 91);
            this.startInstallButton.Name = "startInstallButton";
            this.startInstallButton.Size = new System.Drawing.Size(174, 23);
            this.startInstallButton.TabIndex = 2;
            this.startInstallButton.Text = "Install";
            this.startInstallButton.UseVisualStyleBackColor = true;
            this.startInstallButton.Click += new System.EventHandler(this.startInstallButton_Click);
            // 
            // driverInstallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(196, 121);
            this.Controls.Add(this.startInstallButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "driverInstallerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Install";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton wfpDriverRadioButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton tdiDriverRadioButton;
        private System.Windows.Forms.Button startInstallButton;
    }
}

