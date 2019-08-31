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
    public partial class Form_Setting : Form
    {
        public Form_Setting()
        {
            InitializeComponent();
        }

        private void Form_Setting_Load(object sender, EventArgs e)
        {

        }

        private void Btn_DeleteReports_Click(object sender, EventArgs e)
        {
            string DeleteThis = "report";
            string[] Files = Directory.GetFiles(StringPath.PathReport);

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
    }
}
