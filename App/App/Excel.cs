using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace App
{
    class DataExcel
    {
        public string Id;
        public string Date;
        public int TimeIn;
        public int TimeOut;
        public string Name;

        public DataExcel(string Id, string Date, int TimeIn, int TimeOut, string Name)
        {
            this.Id = Id;
            this.Date = Date;
            this.TimeIn = TimeIn;
            this.TimeOut = TimeOut;
            this.Name = Name;
        }
    }
    class Excel
    {
        public Excel()
        {
        }
        public void ImportExcel()
        {
            string fname = "";
            string WorkingDate = "";
            string DateFile = "Date.txt";
            int WorkingDateYear = 0;
            int WorkingDateMonth = 0;
            int WorkingDateDay = 0;
            bool ImportErrorFlag = true;
            ExcelPackage xlApp = null;
            ExcelWorkbook xlWorkBook;
            ExcelWorksheet xlWorkSheet = null;

            do
            {
                fname = Api.OpenDialogFile();
                if (fname == null)
                {
                    string error = "Khong file nao duoc chon";
                    MessageBox.Show(error);
                    break;
                }
                FileInfo fileInfo = new FileInfo(fname);
                xlApp = new ExcelPackage(fileInfo);
                xlWorkBook = xlApp.Workbook;
                xlWorkSheet = xlWorkBook.Worksheets.First();

                xlWorkSheet.Column(4).Style.Numberformat.Format = "HH:mm";
                xlWorkSheet.Column(5).Style.Numberformat.Format = "HH:mm";
                WorkingDate = (Convert.ToDateTime(xlWorkSheet.Cells[3, 7].Text)).ToString("yyyMMdd");
                WorkingDateYear = (Convert.ToDateTime(xlWorkSheet.Cells[3, 7].Text)).Year;
                WorkingDateMonth = (Convert.ToDateTime(xlWorkSheet.Cells[3, 7].Text)).Month;
                WorkingDateDay = (Convert.ToDateTime(xlWorkSheet.Cells[3, 7].Text)).Day;

                if (!File.Exists(DateFile))
                {
                    ImportErrorFlag = false;
                    using (StreamWriter sw = File.CreateText(DateFile))
                    {
                        sw.WriteLine(WorkingDate);
                    }
                }
                else
                {
                    string[] date;
                    date = File.ReadAllLines(DateFile);
                    ImportErrorFlag = false;
                    for (int i = 0; i < date.Count(); i++)
                    {
                        if (string.Equals(date[i], WorkingDate))
                        {
                            string error = "Du lieu ngay " + WorkingDateDay + "." + WorkingDateMonth + "." + WorkingDateYear + " da co\nVui long chon file khac";
                            MessageBox.Show(error);
                            ImportErrorFlag = true;
                            break;
                        }
                    }

                }
            } while (ImportErrorFlag);

            using (StreamWriter sw = new StreamWriter(DateFile, true))
            {
                sw.WriteLine(WorkingDate);
            }

            if (!ImportErrorFlag)
            {
                FileInfo ExcelReport = new FileInfo(StringPath.PathReport + "report_" + WorkingDate + ".xlsx");
                ExcelPackage xlReport = new ExcelPackage(ExcelReport);
                ExcelWorksheet xlReportWorkSheet = xlReport.Workbook.Worksheets.Add("Report");
                xlReportWorkSheet.TabColor = System.Drawing.Color.Black;
                xlReportWorkSheet.DefaultRowHeight = 12;

                xlReportWorkSheet.Row(1).Height = 20;
                xlReportWorkSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                xlReportWorkSheet.Row(1).Style.Font.Bold = true;
                xlReportWorkSheet.Cells[1, 1].Value = "Id";
                xlReportWorkSheet.Cells[1, 2].Value = "Name";

                if (!Directory.Exists(StringPath.PathFolder + "\\" + StringPath.dbName))
                    Directory.CreateDirectory(StringPath.PathFolder + "\\" + StringPath.dbName);
                if (!Directory.Exists(StringPath.PathFolder + "\\" + "Report"))
                    Directory.CreateDirectory(StringPath.PathFolder + "\\" + "Report");

                DirectoryInfo dir = new DirectoryInfo(StringPath.PathFolder + "\\" + StringPath.dbName);

                ArrayList IDinExcel = new ArrayList();
                ArrayList al_DataExcel = new ArrayList();
                ArrayList IDinDB = new ArrayList();
                ArrayList IDnotinExcel = new ArrayList();
    
                IDinExcel.Clear();
                IDinDB.Clear();
                IDnotinExcel.Clear();

                if (!Api.IsDirectoryEmpty(StringPath.PathFolder + "\\" + StringPath.dbName))
                {
                    foreach (FileInfo fi in dir.GetFiles())
                    {
                        IDinDB.Add(Api.GetIdFromFileName(fi.Name));
                    }
                }


                for (int i = 6; i <= xlWorkSheet.Dimension.End.Row && xlWorkSheet.Cells[i, 2].Text != ""; i++)
                {
                    DataExcel dt = new DataExcel(xlWorkSheet.Cells[i, 2].Text,
                                                WorkingDate,
                                                Convert.ToDateTime((xlWorkSheet.Cells[i, 4].Value)).Hour,
                                                Convert.ToDateTime((xlWorkSheet.Cells[i, 5].Value)).Hour,
                                                xlWorkSheet.Cells[i, 7].Text);
                    al_DataExcel.Add(dt);
                    string FileName = StringPath.PathFile + dt.Id + ".txt";
                    if (IDinDB.Capacity == 0)
                    {
                        Api.UpdateFirstWorkData(FileName, WorkingDateDay, WorkingDateMonth, WorkingDateYear, dt.TimeIn, dt.TimeOut, dt.Name);
                    }
                    else
                    {
                        bool EndOfFile = true;
                        foreach (Object obj in IDinDB)
                        {
                            if (string.Equals(dt.Id, obj.ToString()))
                            {
                                if (Api.CheckDateWrittenToFile(FileName, WorkingDateDay, WorkingDateMonth, WorkingDateYear))
                                {
                                    EndOfFile = false;
                                    //if (Api.GetDayWeek(WorkingDateDay, WorkingDateMonth, WorkingDateYear) == 7)
                                    //{
                                    //    Api.UpdateWorkDataOnSat(FileName, WorkingDateDay, WorkingDateMonth, WorkingDateYear, dt.TimeIn, dt.TimeOut);
                                    //}
                                    //else
                                    {
                                        Api.UpdateWorkData(FileName, WorkingDateDay, WorkingDateMonth, WorkingDateYear, dt.TimeIn, dt.TimeOut);
                                    }
                                }
                                break;
                            }
                        }
                        if (EndOfFile)
                            Api.UpdateFirstWorkData(FileName, WorkingDateDay, WorkingDateMonth, WorkingDateYear, dt.TimeIn, dt.TimeOut, dt.Name);

                    }
                }

                foreach (FileInfo fi in dir.GetFiles())
                {
                    string FileName = StringPath.PathFile + fi.Name;
                    bool IsExist = false;
                    foreach (DataExcel dt in al_DataExcel)
                    {
                        if (string.Equals(Api.GetIdFromFileName(fi.Name), dt.Id))
                        {
                            IsExist = true;
                            break;
                        }
                    }
                    if (!IsExist)
                    {
                        if (Api.CheckDateWrittenToFile(FileName, WorkingDateDay, WorkingDateMonth, WorkingDateYear))
                        {
                            Api.UpdateNonWorkingData(FileName, WorkingDateDay, WorkingDateMonth, WorkingDateYear);
                        }
                    }
                }

                int index = 2;
                foreach (DataExcel dt in al_DataExcel)
                {
                    string FileName = StringPath.PathFile + dt.Id + ".txt";
                    xlReportWorkSheet.Cells[index, 1].Value = dt.Id;
                    xlReportWorkSheet.Cells[index, 2].Value = dt.Name;
                    if (!Api.validation(FileName))
                    {
                        xlReportWorkSheet.Cells[index, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        xlReportWorkSheet.Cells[index, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        xlReportWorkSheet.Cells[index, 1].Style.Fill.BackgroundColor.SetColor(Color.OrangeRed);
                        xlReportWorkSheet.Cells[index, 2].Style.Fill.BackgroundColor.SetColor(Color.OrangeRed);
                        Api.UpdateWorkingDataForInvalidEmployee(FileName, WorkingDateDay, WorkingDateMonth, WorkingDateYear);
                    }

                    index++;
                }

                xlReportWorkSheet.Column(1).AutoFit();
                xlReportWorkSheet.Column(2).AutoFit();
                xlReportWorkSheet.Column(3).AutoFit();

                xlReport.Save();

                string message = "Report generated";
                MessageBox.Show(message);

                xlApp.Dispose();
                xlReport.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
