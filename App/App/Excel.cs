using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Windows.Forms;

namespace App
{
    class Excel
    {
        public Excel()
        {

        }
        public void ImportExcel(DataGridView dataGridView1)
        {
            string fname = "";
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Excel File Dialog";
            fdlg.InitialDirectory = @"D:\Work\WindowApp\xepcacongnhan\Doc";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                fname = fdlg.FileName;
            }

            FileInfo fileInfo = new FileInfo(fname);
            ExcelPackage xlApp = new ExcelPackage(fileInfo);
            ExcelWorkbook xlWorkBook = xlApp.Workbook;
            ExcelWorksheet xlWorkSheet = xlWorkBook.Worksheets.First();

            xlWorkSheet.Column(4).Style.Numberformat.Format = "HH:mm";
            xlWorkSheet.Column(5).Style.Numberformat.Format = "HH:mm";

            int rowCount = xlWorkSheet.Dimension.End.Row;
            int colCount = xlWorkSheet.Dimension.End.Column;

            DateTime dt = Convert.ToDateTime(xlWorkSheet.Cells[3, 7].Value);
            string dtfm = dt.ToString("yyyyMMdd");

            if (!Directory.Exists(StringPath.PathFolder + "\\" + StringPath.dbName))
                Directory.CreateDirectory(StringPath.PathFolder + "\\" + StringPath.dbName);

            string test = Convert.ToDateTime(xlWorkSheet.Cells[3, 7].Value).ToString("yyyyMMdd");
            DateTime TimeIn, TimeOut;
            TimeSpan PeriodTime;
            int i_TimeData;

            for (int i = 6; i <= rowCount && xlWorkSheet.Cells[i, 2].Text != ""; i++)
            {
                string FileName = StringPath.PathFile + xlWorkSheet.Cells[i, 2].Text + ".txt";
                TimeIn = Convert.ToDateTime(xlWorkSheet.Cells[i, 4].Value);
                TimeOut = Convert.ToDateTime(xlWorkSheet.Cells[i, 5].Value);
                PeriodTime = TimeOut - TimeIn;
                i_TimeData = (int)PeriodTime.TotalHours;

                if (!File.Exists(FileName))
                {
                    FileInfo fi = new FileInfo(FileName);
                    using (StreamWriter sw = fi.CreateText())
                    {
                        sw.WriteLine(test + "\t" + i_TimeData);
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(FileName, true))
                    {
                        sw.WriteLine(test + "\t" + i_TimeData + "\t" + "Lastline");
                    }
                }
                
            }
        }
    }
}
