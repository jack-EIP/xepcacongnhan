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
        public static string dbName = "Database";                                                          // Ten database
        public static string PathFolder = Directory.GetCurrentDirectory();                                 // Duong dan den folder 
        public static string PathFile = PathFolder + "\\" + dbName + "\\";                                 // Duong dan file trong database
        public static string PathReport = PathFolder + "\\Report\\";                                       // Duong dan file report
    }
}
