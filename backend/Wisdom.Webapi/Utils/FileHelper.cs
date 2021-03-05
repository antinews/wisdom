using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wisdom.Webapi.Extensions;
using Wisdom.Webapi.Models.Response;

namespace Wisdom.Webapi.Utils
{
    public class FileHelper
    {
        /// <summary>
        /// 泛型集合转字节数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static byte[] ListToByteArray<T>(List<T> list)
        {
            if (list.Count <= 0) return null;
            XSSFWorkbook workbook = new XSSFWorkbook();  // xlsx格式
            try
            {
                ISheet sheet = workbook.CreateSheet((typeof(T).GetCustomAttributes(false)[0] as  DescriptionAttribute).Description);  // 创建sheet

                int rowIndex = 0, colIndex = 0;

                var head = sheet.CreateRow(rowIndex++);
                var fields = typeof(T).GetProperties();
                foreach (var f in fields)  // 打表头
                    head.CreateCell(colIndex++).SetCellValue((f.GetCustomAttributes(false)[0] as DescriptionAttribute).Description);
                foreach (var t in list)  // 内容
                {
                    var row = sheet.CreateRow(rowIndex++);
                    colIndex = 0;
                    foreach (var f in fields)
                        row.CreateCell(colIndex++).SetCellValue(f.GetValue(t).ToString());
                }

                // 保存
                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.Write(ms);
                    return ms.ToArray();
                }
            }
            finally
            {
                workbook.Close();
            }
        }
    
    
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="files">文件</param>
        /// <param name="savePath">保存路径（相当于wwwroot）</param>
        /// <returns></returns>
        public static ResponseModel UploadFile(IFormFileCollection files, string savePath)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (files.Count == 0)
            {
                response.Message = "没有发现文件";
                response.Code = 403;
            }
            else
            {
                //long size = files.Sum(f => f.Length);
                var fileFolder = Directory.GetCurrentDirectory() + @"\wwwroot" + savePath;

                if (!Directory.Exists(fileFolder))
                    Directory.CreateDirectory(fileFolder);
                var file = files[0];
                if (file.Length > 0)
                {
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") +
                                   Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(fileFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    response.Data = new { url = $@"{savePath }\{ fileName}", name = fileName };
                }
                else
                {
                    response.SetFailed("不能上传空文件！");
                }
            }
            return response;
        }
    
    }
}
