using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace App
{
    public partial class Form_Main : Form
    {
        private Form_Setting setting;
        public Form_Main()
        {
            InitializeComponent();
        }

        private void btn_ImportXLSFile_Click(object sender, EventArgs e)
        {
            Excel App = new Excel();
            App.ImportExcel();
        }

        private void Btn_ClearDatabase_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(StringPath.PathFolder + "\\" + StringPath.dbName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }
            string message = "Completed delete all files";
            MessageBox.Show(message);
        }

        private void Btn_DeleteReports_Click(object sender, EventArgs e)
        {
            string DeleteThis = "report";
            string[] Files = Directory.GetFiles(Directory.GetCurrentDirectory());

            foreach (string file in Files)
            {
                if (file.Contains(DeleteThis))
                {
                    File.Delete(file);
                }
            }
            File.Delete("date.txt");
            string message = "Completed delete all files";
            MessageBox.Show(message);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Btn_OpenReportFolder_Click(object sender, EventArgs e)
        {
            string path = StringPath.PathReport;
            System.Diagnostics.Process.Start(path);
        }

        private void Btn_Setting_Click(object sender, EventArgs e)
        {
            this.setting = new Form_Setting();
            this.setting.Show();
        }
    }
}
