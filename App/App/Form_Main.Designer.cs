namespace App
{
    partial class Form_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.btn_ImportXLSFile = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btn_OpenReportFolder = new System.Windows.Forms.Button();
            this.btn_Setting = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_ImportXLSFile
            // 
            this.btn_ImportXLSFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ImportXLSFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_ImportXLSFile.Location = new System.Drawing.Point(110, 12);
            this.btn_ImportXLSFile.Name = "btn_ImportXLSFile";
            this.btn_ImportXLSFile.Size = new System.Drawing.Size(157, 33);
            this.btn_ImportXLSFile.TabIndex = 0;
            this.btn_ImportXLSFile.Text = "Browse Excel File";
            this.btn_ImportXLSFile.UseVisualStyleBackColor = true;
            this.btn_ImportXLSFile.Click += new System.EventHandler(this.btn_ImportXLSFile_Click);
            // 
            // btn_OpenReportFolder
            // 
            this.btn_OpenReportFolder.Location = new System.Drawing.Point(110, 51);
            this.btn_OpenReportFolder.Name = "btn_OpenReportFolder";
            this.btn_OpenReportFolder.Size = new System.Drawing.Size(157, 33);
            this.btn_OpenReportFolder.TabIndex = 3;
            this.btn_OpenReportFolder.Text = "Open report folder";
            this.btn_OpenReportFolder.UseVisualStyleBackColor = true;
            this.btn_OpenReportFolder.Click += new System.EventHandler(this.Btn_OpenReportFolder_Click);
            // 
            // btn_Setting
            // 
            this.btn_Setting.Location = new System.Drawing.Point(110, 90);
            this.btn_Setting.Name = "btn_Setting";
            this.btn_Setting.Size = new System.Drawing.Size(157, 33);
            this.btn_Setting.TabIndex = 4;
            this.btn_Setting.Text = "Setting";
            this.btn_Setting.UseMnemonic = false;
            this.btn_Setting.UseVisualStyleBackColor = true;
            this.btn_Setting.Click += new System.EventHandler(this.Btn_Setting_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(394, 142);
            this.Controls.Add(this.btn_Setting);
            this.Controls.Add(this.btn_OpenReportFolder);
            this.Controls.Add(this.btn_ImportXLSFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SHIFT MANAGEMENT";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ImportXLSFile;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btn_OpenReportFolder;
        private System.Windows.Forms.Button btn_Setting;
    }
}

