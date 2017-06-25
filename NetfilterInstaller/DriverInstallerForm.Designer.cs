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
            this.driverChooseGroupBox = new System.Windows.Forms.GroupBox();
            this.tdiDriverRadioButton = new System.Windows.Forms.RadioButton();
            this.actionButton = new System.Windows.Forms.Button();
            this.driverChooseGroupBox.SuspendLayout();
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
            // driverChooseGroupBox
            // 
            this.driverChooseGroupBox.Controls.Add(this.tdiDriverRadioButton);
            this.driverChooseGroupBox.Controls.Add(this.wfpDriverRadioButton);
            this.driverChooseGroupBox.Location = new System.Drawing.Point(12, 12);
            this.driverChooseGroupBox.Name = "driverChooseGroupBox";
            this.driverChooseGroupBox.Size = new System.Drawing.Size(174, 72);
            this.driverChooseGroupBox.TabIndex = 1;
            this.driverChooseGroupBox.TabStop = false;
            this.driverChooseGroupBox.Text = "Select driver type for installation";
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
            // actionButton
            // 
            this.actionButton.Location = new System.Drawing.Point(10, 90);
            this.actionButton.Name = "actionButton";
            this.actionButton.Size = new System.Drawing.Size(176, 23);
            this.actionButton.TabIndex = 2;
            this.actionButton.Text = "Install";
            this.actionButton.UseVisualStyleBackColor = true;
            this.actionButton.Click += new System.EventHandler(this.ActionButton_Click);
            // 
            // DriverInstallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(196, 121);
            this.Controls.Add(this.actionButton);
            this.Controls.Add(this.driverChooseGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DriverInstallerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Netfilter installer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.driverChooseGroupBox.ResumeLayout(false);
            this.driverChooseGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton wfpDriverRadioButton;
        private System.Windows.Forms.GroupBox driverChooseGroupBox;
        private System.Windows.Forms.RadioButton tdiDriverRadioButton;
        private System.Windows.Forms.Button actionButton;
    }
}

