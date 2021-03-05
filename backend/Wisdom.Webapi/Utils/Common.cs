using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace Wisdom.Webapi.Utils
{
    public static class Common
    {
        public static bool IsDevelopment = true;
        /// <summary>
        /// 0 restapi 1tcp
        /// </summary>
        public static int TaosModel = 0;
        public static string ConnectionStringsReplace(string connectionStrings)
        {
            return connectionStrings;
            //if (IsDevelopment)
            //{
            //    return connectionStrings.Replace("127.0.0.1", "58.48.215.214").Replace("10.14.118.42", "120.202.27.77,30012");
            //}
            //else
            //{
            //    return connectionStrings.Replace("120.202.27.77,30075", "127.0.0.1").Replace("10.14.118.42", "120.202.27.77,30012");//.Replace("120.202.27.77,30012", "10.14.118.42");
            //}
        }
        public static DateTime StrTransDateTime(string str)
        {
            string[] times = str.Replace("/", " ").Replace("-", " ").Replace(".", " ").Replace(":", " ").Replace("T", " ").Split(" ");
            return Convert.ToDateTime((times.Length > 0 ? times[0] : "2000") + "-" + (times.Length > 1 ? times[1] : "01") + "-" + (
            times
            .Length > 2 ? times[2] : "01") + " " + (times.Length > 3 ? times[3] : "00") + ":" + (times.Length > 4 ? times[4] :
            "00") + ":" + (times.Length > 5 ? times[5] : "00") + "." + (times.Length > 6 ? times[6] : "000"));
        }
        public static void CopyDirectory(string srcDir, string destDir)
        {
            DirectoryInfo srcDirectory = new DirectoryInfo(srcDir);
            DirectoryInfo destDirectory = new DirectoryInfo(destDir);

            //if (destDirectory.FullName.StartsWith(srcDirectory.FullName, StringComparison.CurrentCultureIgnoreCase))
            //{
            //    throw new Exception("cannot copy parent to child directory.");
            //}

            if (!srcDirectory.Exists)
            {
                return;
            }

            if (!destDirectory.Exists)
            {
                destDirectory.Create();
            }

            FileInfo[] files = srcDirectory.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                CopyFile(files[i].FullName, destDirectory.FullName);
            }

            DirectoryInfo[] dirs = srcDirectory.GetDirectories();

            for (int j = 0; j < dirs.Length; j++)
            {
                CopyDirectory(dirs[j].FullName, destDirectory.FullName + @"/" + dirs[j].Name);
            }
        }
        public static void CopyFile(string srcFile, string destDir)
        {
            DirectoryInfo destDirectory = new DirectoryInfo(destDir);
            string fileName = Path.GetFileName(srcFile);
            if (!System.IO.File.Exists(srcFile))
            {
                return;
            }

            if (!destDirectory.Exists)
            {
                destDirectory.Create();
            }
            Console.WriteLine(destDirectory.FullName + @"/" + fileName);
            System.IO.File.Copy(srcFile, destDirectory.FullName + @"/" + fileName, true);

        }
    }
}
