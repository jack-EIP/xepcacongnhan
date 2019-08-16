using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace App
{
    class StringPath
    {
        public static string dbName = "Database";
        public static string PathFolder = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
        public static string PathFile = PathFolder + "\\" + dbName + "\\";
    }
}
