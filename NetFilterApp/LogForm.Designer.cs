namespace NetFilterApp
{
    partial class LogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogForm));
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.reloadFileButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(10, 10);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.Size = new System.Drawing.Size(262, 205);
            this.logTextBox.TabIndex = 0;
            this.logTextBox.Text = "";
            this.logTextBox.WordWrap = false;
            // 
            // reloadFileButton
            // 
            this.reloadFileButton.Location = new System.Drawing.Point(10, 216);
            this.reloadFileButton.Name = "reloadFileButton";
            this.reloadFileButton.Size = new System.Drawing.Size(262, 23);
            this.reloadFileButton.TabIndex = 1;
            this.reloadFileButton.Text = "Reload file";
            this.reloadFileButton.UseVisualStyleBackColor = true;
            this.reloadFileButton.Click += new System.EventHandler(this.reloadFileButton_Click);
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 245);
            this.Controls.Add(this.reloadFileButton);
            this.Controls.Add(this.logTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LogForm";
            this.Text = "Log viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogForm_FormClosing);
            this.Load += new System.EventHandler(this.LogForm_Load);
            this.Resize += new System.EventHandler(this.LogForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox logTextBox;
        private System.Windows.Forms.Button reloadFileButton;
    }
}