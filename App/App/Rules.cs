using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    // @Cac thuoc tinh duoc luu trong database
    public enum Attr
    {
        WEEKDAY,
        DAY,
        MONTH,
        YEAR,
        TOTAL,
        SHIFT,
        IN,
        OUT,
        EX
    }
    class Rules
    {
        public Rules()
        {

        }

        // @Rule 1: Khong dat lich 7 ngay lam viec lien tuc
        public bool Rule1(int NumberOfLine, string[] AllFile)
        {
            return true;
        }

        // @Rule 2: Thoi gian nghi giua 2 ca lam viec lien tiep phai bang hoac nhieu hon 12 tieng
        public bool Rule2(int NumberOfLine, string[] AllFile)
        {
            if (NumberOfLine > 2)
            {
                string[] LastLine = AllFile[NumberOfLine - 1].Split('\t');
                string[] LastLinePrevious = AllFile[NumberOfLine - 2].Split('\t');
                int TimeInToday = int.Parse(LastLine[(int)Attr.IN]);
                int TimeOutDayBefore = int.Parse(LastLinePrevious[(int)Attr.OUT]);
                if (TimeInToday < TimeOutDayBefore)
                    TimeInToday += 24;
                int TimeOffBetweenShift = TimeInToday - TimeOutDayBefore;
                if (TimeOffBetweenShift < 12)
                    return false;
            }
            return true;
        }

        // @Rule 3: Thoi gian lam viec binh thuong + thoi gian tang ca ko qua 12 tieng
        public bool Rule3(int NumberOfLine, string[] AllFile)
        {
            if (NumberOfLine > 1)
            {
                string[] LastLine = AllFile[NumberOfLine - 1].Split('\t');
                int TimeSpan = int.Parse(LastLine[(int)Attr.TOTAL]);
                if (TimeSpan > 12)
                    return false;
            }
            return true;
        }

        // @Rule 4: Khong dang ky 2 lan lam viec cho 1 nguoi trong ngay
        public bool Rule4(int NumberOfLine, string[] AllFile)
        {
            return true;
        }

        // @Rule 5: Thoi gian tang ca khong qua 30h / 1 thang va 200h trong 1 nam
        public bool Rule5_1(int NumberOfLine, string[] AllFile)
        {
            int TotalEx = 0;
            int index = NumberOfLine - 1;
            string[] CurrentLine = AllFile[index].Split('\t');
            int GetMonth = int.Parse(CurrentLine[(int)Attr.MONTH]);
            int GetCurrentMonth = GetMonth;
            while (GetMonth == GetCurrentMonth)
            {
                CurrentLine = AllFile[index].Split('\t');
                GetCurrentMonth = int.Parse(CurrentLine[2]);
                TotalEx += int.Parse(CurrentLine[(int)Attr.EX]);
                index--;
                if (index == 0)
                    break;
            }
            if (TotalEx > 30)
                return false;
            return true;
        }

        public bool Rule5_2(int NumberOfLine, string[] AllFile)
        {
            int TotalEx = 0;
            int index = NumberOfLine - 1;
            string[] CurrentLine = AllFile[index].Split('\t');
            int GetCurrentYear = int.Parse(CurrentLine[(int)Attr.YEAR]);
            while (GetCurrentYear == int.Parse(CurrentLine[(int)Attr.YEAR]))
            {
                CurrentLine = AllFile[index].Split('\t');
                TotalEx += int.Parse(CurrentLine[(int)Attr.EX]);
                index--;
                if (index == 0)
                    break;
            }
            if (TotalEx > 200)
                return false;
            return true;
        }

        // @Rule6: Thoi gian lam viec ko qua 60 tieng trong 6 ngay lien tuc
        public bool Rule6(int NumberOfLine, string[] AllFile)
        {
            if (NumberOfLine > 6)
            {
                int ToTalHourWorking = 0;
                for (int i = NumberOfLine - 1; i >= NumberOfLine - 6; i--)
                {
                    string[] CurrentLine = AllFile[i].Split('\t');
                    int TimeSpan = int.Parse(CurrentLine[(int)Attr.TOTAL]);
                    ToTalHourWorking += TimeSpan;
                }
                if (ToTalHourWorking > 60)
                    return false;
            }
            return true;
        }
    }
}
