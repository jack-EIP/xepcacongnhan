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


namespace App
{
    class Api
    {
        // @function: Mo hop thoai chon file .xls
        // @brief: tra ve duong dan cua file da chon
        // @param: none
        public static string OpenDialogFile()
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Excel File Dialog";
            fdlg.InitialDirectory = @"D:\Extra";
            //fdlg.InitialDirectory = @"D:\Extra\TestRule5";
            //fdlg.InitialDirectory = @"D:\Work\WindowApp\xepcacongnhan\Doc";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                return fdlg.FileName;
            }
            return null;
        }

        // @function: Tinh toan ca lam viec
        // @brief: ca 1: 6am - 14pm, ca 2: 14pm - 22pm, ca 3: 22pm - 6pm
        // @param: obj_timeIn: thoi gian vao lam
        //         obj_timeOut: thoi gian tan ca
        public static string GetShift(Object obj_timeIn, Object obj_timeOut)
        {
            DateTime TimeIn = Convert.ToDateTime(obj_timeIn);
            DateTime TimeOut = Convert.ToDateTime(obj_timeOut);
            if (TimeIn.Hour == 6 || TimeOut.Hour == 14)
                return "1";
            else if (TimeIn.Hour == 14 || TimeOut.Hour == 22)
                return "2";
            else if (TimeIn.Hour == 22 || TimeOut.Hour == 6)
                return "3";
            return "0";
        }

        // @function: Tinh toan ca lam viec
        // @brief: ca 1: 6am - 14pm, ca 2: 14pm - 22pm, ca 3: 22pm - 6pm
        // @param: int_TimeIn: thoi gian vao lam viec
        //         int_TimeOut: thoi gian tan ca
        public static string GetShift_int(int int_timeIn, int int_timeOut)
        {
            if (int_timeIn == 6 || int_timeOut == 14)
                return "1";
            else if (int_timeIn == 14 || int_timeOut == 22)
                return "2";
            else if (int_timeIn == 22 || int_timeOut == 6)
                return "3";
            return "0";
        }

        // @function: Tinh toan khoang thoi gian lam viec
        // @brief: thoi gian tan ca - thoi gian vao lam
        // @param: obj_timeIn: thoi gian vao lam
        //         obj_timeOut: thoi gian tan ca
        public static String GetTimeSpan(Object obj_timeIn, Object obj_timeOut)
        {
            DateTime TimeIn = Convert.ToDateTime(obj_timeIn);
            DateTime TimeOut = Convert.ToDateTime(obj_timeOut);
            if (DateTime.Compare(TimeIn, TimeOut) > 0)
                TimeOut = TimeOut.AddHours(24);
            TimeSpan timespan = TimeOut - TimeIn;
            string s_timespan = timespan.TotalHours.ToString();
            return s_timespan;
        }

        // @function: Tinh toan khoang thoi gian lam viec
        // @brief: thoi gian tan ca - thoi gian vao lam
        // @param: int_timeIn: thoi gian vao lam
        //         int_timeOut: thoi gian tan ca
        public static int GetTimeSpan_int(int int_timeIn, int int_timeOut)
        {
            //DateTime TimeIn = Convert.ToDateTime(obj_timeIn);
            //DateTime TimeOut = Convert.ToDateTime(obj_timeOut);
            if (int_timeIn > int_timeOut)
                int_timeOut += 24;
            int int_timespan = int_timeOut - int_timeIn;
            return int_timespan;
        }

        // @function: Tinh toan thoi gian tang ca
        // @brief: Tong thoi gian lam viec - 8h
        // @param: int_timeIn: thoi gian vao lam
        //         int_timeOut: thoi gian tan ca
        public static int GetEx(int int_timeIn, int int_timeOut)
        {
            int Ex = GetTimeSpan_int(int_timeIn, int_timeOut) - 8;
            if (Ex < 0)
                return 0;
            return Ex;
        }


        // @function: Kiem tra ngay truoc khi ghi thong tin vao database
        // @brief: Neu trung ngay co trong database thi tra ve false, nguoc lai true
        // @param: FileName: duong dan file muon ghi ngay thang vao
        //         Day: ngay
        //         Month: thang
        //         Year: nam
        public static bool CheckDateWrittenToFile(string FileName, int Day, int Month, int Year)
        {
            string[] line = File.ReadAllLines(FileName);
            string[] DateFromFile;
            for (int i = 1; i < line.Length; i++)
            {
                DateFromFile = line[i].Split('\t');
                if (Day == int.Parse(DateFromFile[0]) && Month == int.Parse(DateFromFile[1]) && Year == int.Parse(DateFromFile[2]))
                    return false;
            }
            return true;
        }

        // @function: kiem tra dieu kien
        // @brief: khi thoan man het 6 dieu kien tra ve true, nguoc lai false
        // @param: FileName: duong dan file chua thong tin de kiem tra
        public static bool validation(string FileName)
        {
            Rules rules = new Rules();
            int Lines = File.ReadAllLines(FileName).Count();
            string[] AllLine = File.ReadAllLines(FileName);
            if (rules.Rule1(Lines, AllLine)
                && rules.Rule2(Lines, AllLine)
                && rules.Rule3(Lines, AllLine)
                && rules.Rule4(Lines, AllLine)
                && rules.Rule5_1(Lines, AllLine)
                && rules.Rule5_2(Lines, AllLine)
                && rules.Rule6(Lines, AllLine)
               )
                return true;
            return false;
        }

        // @function: Tim kiem ID cua nhan vien nao do trong file .xls
        // @brief: tim thay tra ve vi tri cua row trong file .xls, nguoc lai thi 0
        // @param: xlWorkSheet: worksheet cua file excel chua danh sach ID
        //         EmployeeID: ID can tim kiem
        public static int SearchEmployeeID(ExcelWorksheet xlWorkSheet, string EmployeeID)
        {
            int rowCount = xlWorkSheet.Dimension.End.Row;
            int i;
            string[] GetEmployeeID = EmployeeID.Split('.');
            for (i = 6; i < rowCount; i++)
            {
                if (String.Equals(GetEmployeeID[0], xlWorkSheet.Cells[i, 2].Text))
                    return i;
            }
            return 0;
        }

        // @function: Kiem tra folder co empty hay khong
        // @brief: 
        // @param: none
        public static bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        // @function: Lay ID nhan vien tu ten cua file .txt
        // @brief: 
        // @param: FileName: duong dan cua file .txt
        public static string GetIdFromFileName(string FileName)
        {
            string[] id = FileName.Split('.');
            return id[0];
        }

        // @function: Nhap du lieu thoi gian lam viec moi cua nhan vien chua co trong database
        // @brief: cac thong tin nhap vao file .txt: 
        //                  Thu, Ngay, Thang, Nam, Tong gio lam, Ca lam, Gio vao lam viec, Gio tan ca, Thoi gian tang ca, Ten nhan vien
        // @param: FileName: duong dan file .txt de ghi
        //         WorkingDay: ngay
        //         WorkingMonth: thang
        //         WorkingYear: nam
        //         TimeIn: thoi gian vao lam
        //         TimeOut: thoi gian tan ca
        //         Name: ten nhan vien
        public static void UpdateFirstWorkData(string FileName, int WorkingDay, int WorkingMonth, int WorkingYear, int TimeIn, int TimeOut, string Name)
        {
            using (StreamWriter sw = File.CreateText(FileName))
            {
                sw.WriteLine(
                        "Dw" + "\t"          // header thu                  
                        + "D" + "\t"         // hearder ngay
                        + "M" + "\t"         // header thang
                        + "Y" + "\t"         // header nam
                        + "Tol" + "\t"       // header tong gio lam
                        + "Sft" + "\t"       // header ca lam
                        + "In" + "\t"        // header thoi gian vao lam
                        + "Out" + "\t"       // header thoi gian tan ca
                        + "Ex" + "\t"        // header thoi gian tang ca
                        + Name               // header ten
                );
                sw.WriteLine(
                        Api.GetDayWeek(WorkingDay, WorkingMonth, WorkingYear) + "\t"      // thu
                        + WorkingDay + "\t"                                                 // ngay
                        + WorkingMonth + "\t"                                               // thang
                        + WorkingYear + "\t"                                                // nam
                        + Api.GetTimeSpan_int(TimeIn, TimeOut) + "\t"                       // thoi gian lam viec
                        + Api.GetShift_int(TimeIn, TimeOut) + "\t"                          // ca lam
                        + TimeIn + "\t"                                                     // thoi gian bat dau lam
                        + TimeOut + "\t"                                                    // thoi gian tan ca
                        + Api.GetEx(TimeIn, TimeOut)                                        // thoi gian tang ca
                        );
            }
        }

        // @function: Cap nhat thoi gian lam viec cua nhan vien da co trong database
        // @brief: cac thong tin nhap vao file .txt: 
        //                  Thu, Ngay, Thang, Nam, Tong gio lam, Ca lam, Gio vao lam viec, Gio tan ca, Thoi gian tang ca, Ten nhan vien
        // @param: FileName: duong dan file .txt de ghi
        //         WorkingDay: ngay
        //         WorkingMonth: thang
        //         WorkingYear: nam
        //         TimeIn: thoi gian vao lam
        //         TimeOut: thoi gian tan ca
        public static void UpdateWorkData(string FileName, int WorkingDay, int WorkingMonth, int WorkingYear, int TimeIn, int TimeOut)
        {
            using (StreamWriter sw = new StreamWriter(FileName, true))
            {
                sw.WriteLine(
                        Api.GetDayWeek(WorkingDay, WorkingMonth, WorkingYear) + "\t"      // thu
                        + WorkingDay + "\t"                                                 // ngay
                        + WorkingMonth + "\t"                                               // thang
                        + WorkingYear + "\t"                                                // nam
                        + Api.GetTimeSpan_int(TimeIn, TimeOut) + "\t"                       // thoi gian lam viec
                        + Api.GetShift_int(TimeIn, TimeOut) + "\t"                          // ca lam
                        + TimeIn + "\t"                                                     // thoi gian bat dau lam
                        + TimeOut + "\t"                                                    // thoi gian tan ca
                        + Api.GetEx(TimeIn, TimeOut)                                        // thoi gian tang ca
                    );
            }
        }

        // @function: Cap nhat thoi gian lam viec cua nhan vien da co trong database vao ngay thu 7
        // @brief: cac thong tin nhap vao file .txt, dong thoi ghi vao ngay CN thong tin khong lam viec.
        // @param: FileName: duong dan file .txt de ghi
        //         WorkingDay: ngay
        //         WorkingMonth: thang
        //         WorkingYear: nam
        //         TimeIn: thoi gian vao lam
        //         TimeOut: thoi gian tan ca
        public static void UpdateWorkDataOnSat(string FileName, int WorkingDay, int WorkingMonth, int WorkingYear, int TimeIn, int TimeOut)
        {
            using (StreamWriter sw = new StreamWriter(FileName, true))
            {
                sw.WriteLine(
                        7 + "\t"                                                            // thu
                        + WorkingDay + "\t"                                                 // ngay
                        + WorkingMonth + "\t"                                               // thang
                        + WorkingYear + "\t"                                                // nam
                        + Api.GetTimeSpan_int(TimeIn, TimeOut) + "\t"                       // thoi gian lam viec
                        + Api.GetShift_int(TimeIn, TimeOut) + "\t"                          // ca lam
                        + TimeIn + "\t"                                                     // thoi gian bat dau lam
                        + TimeOut + "\t"                                                    // thoi gian tan ca
                        + Api.GetEx(TimeIn, TimeOut)                                        // thoi gian tang ca
                    );
                sw.WriteLine(
                        8 + "\t"                                                            // thu
                        + 0 + "\t"                                                          // ngay
                        + 0 + "\t"                                                          // thang
                        + 0 + "\t"                                                          // nam
                        + 0 + "\t"                                                          // thoi gian lam viec
                        + 0 + "\t"                                                          // ca lam
                        + 0 + "\t"                                                          // thoi gian bat dau lam
                        + 0 + "\t"                                                          // thoi gian tan ca
                        + 0                                                                 // thoi gian tang ca
                    );
            }
        }
        // @function: cap nhat thoi gian lam viec khi nhan vien khong co trong danh sach file .xls
        // @brief: khi nhan vien khong lam viec ngay do, cap nhat thong tin la 0
        // @param: FileName: duong dan file .txt de ghi
        //         WorkingDay: ngay
        //         WorkingMonth: thang
        //         WorkingYear: nam
        public static void UpdateNonWorkingData(string FileName, int WorkingDay, int WorkingMonth, int WorkingYear)
        {
            using (StreamWriter sw = new StreamWriter(FileName, true))
            {
                sw.WriteLine(
                        Api.GetDayWeek(WorkingDay, WorkingMonth, WorkingYear) + "\t"
                        + WorkingDay + "\t"
                        + WorkingMonth + "\t"
                        + WorkingYear + "\t"
                        + 0 + "\t"
                        + 0 + "\t"
                        + 0 + "\t"
                        + 0 + "\t"
                        + 0
                );
            }
        }

        // @function: tinh toan thu khi biet ngay, thang, nam
        // @brief: tra ve 1 so int
        //         2 - Thu hai, 3 - Thu ba, 4 - Thu tu, 5 - thu nam, 6 - thu sau, 7 - thu bay, 8 - chu nhat
        // @param: WorkingDay: ngay
        //         WorkingMonth: thang
        //         WorkingYear: nam
        public static int GetDayWeek(int WorkingDay, int WorkingMonth, int WorkingYear)
        {
            return ((WorkingDay + ((153 * (WorkingMonth + 12 * ((14 - WorkingMonth) / 12) - 3) + 2) / 5) +
                  (365 * (WorkingYear + 4800 - ((14 - WorkingMonth) / 12))) +
                  ((WorkingYear + 4800 - ((14 - WorkingMonth) / 12)) / 4) -
                  ((WorkingYear + 4800 - ((14 - WorkingMonth) / 12)) / 100) +
                  ((WorkingYear + 4800 - ((14 - WorkingMonth) / 12)) / 400) - 32045) % 7) + 2;
        }

        public static void UpdateWorkingDataForInvalidEmployee(string FileName, int WorkingDay, int WorkingMonth, int WorkingYear)
        {
            List<string> linesList = File.ReadAllLines(FileName).ToList();

            linesList.RemoveAt(linesList.Capacity - 1);

            File.WriteAllLines(FileName, linesList.ToArray());

            using (StreamWriter sw = new StreamWriter(FileName, true))
            {
                sw.WriteLine(
                        Api.GetDayWeek(WorkingDay, WorkingMonth, WorkingYear) + "\t"
                        + WorkingDay + "\t"
                        + WorkingMonth + "\t"
                        + WorkingYear + "\t"
                        + 0 + "\t"
                        + 0 + "\t"
                        + 0 + "\t"
                        + 0 + "\t"
                        + 0
                );
            }
        }

    }
}
