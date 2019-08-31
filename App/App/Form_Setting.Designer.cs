namespace App
{
    partial class Form_Setting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Setting));
            this.btn_DeleteReports = new System.Windows.Forms.Button();
            this.btn_ClearDatabase = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_DeleteReports
            // 
            this.btn_DeleteReports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DeleteReports.Location = new System.Drawing.Point(38, 12);
            this.btn_DeleteReports.Name = "btn_DeleteReports";
            this.btn_DeleteReports.Size = new System.Drawing.Size(157, 33);
            this.btn_DeleteReports.TabIndex = 4;
            this.btn_DeleteReports.Text = "Delele all report files";
            this.btn_DeleteReports.UseVisualStyleBackColor = true;
            this.btn_DeleteReports.Click += new System.EventHandler(this.Btn_DeleteReports_Click);
            // 
            // btn_ClearDatabase
            // 
            this.btn_ClearDatabase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ClearDatabase.Location = new System.Drawing.Point(38, 51);
            this.btn_ClearDatabase.Name = "btn_ClearDatabase";
            this.btn_ClearDatabase.Size = new System.Drawing.Size(157, 33);
            this.btn_ClearDatabase.TabIndex = 3;
            this.btn_ClearDatabase.Text = "Clear Database";
            this.btn_ClearDatabase.UseVisualStyleBackColor = true;
            this.btn_ClearDatabase.Click += new System.EventHandler(this.Btn_ClearDatabase_Click);
            // 
            // Form_Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 90);
            this.Controls.Add(this.btn_DeleteReports);
            this.Controls.Add(this.btn_ClearDatabase);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Setting";
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.Form_Setting_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_DeleteReports;
        private System.Windows.Forms.Button btn_ClearDatabase;
    }
}