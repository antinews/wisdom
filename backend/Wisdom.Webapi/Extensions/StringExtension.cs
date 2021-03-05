using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Extensions
{
    public static class StringExtension
    {
        #region 得到汉字拼音
        /// <summary>
        /// 得到汉字拼音(全拼)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToPinying(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return "";
            }
            var format = new Pinyin.format.PinyinOutputFormat(Pinyin.format.ToneFormat.WITHOUT_TONE,
                Pinyin.format.CaseFormat.LOWERCASE, Pinyin.format.VCharFormat.WITH_U_AND_COLON);
            var py = Pinyin.Pinyin4Net.GetPinyin(str, format);
            return Regex.Replace(py, @"\s", "");
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetInitial(this string str)
        {
            var str_list = str.ToCharArray();
            string res = "";
            for (int i = 0; i < str_list.Length; i++)
            {
                res += Pinyin.Pinyin4Net.GetPinyin(str_list[i])[0][0];
            }
            return res;
        }

        /// <summary>
        /// 判断一个字符串是否是GUID
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsGuid(this string str, out Guid guid)
        {
            return Guid.TryParse(str, out guid);
        }

        /// <summary>
        /// 字符串MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5Encrypt(this string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //将要加密的字符串转换成字节数组
            byte[] palindata = Encoding.Default.GetBytes(str);
            //通过字节数组进行加密
            byte[] encryptdata = md5.ComputeHash(palindata);
            //将加密后的字节数组转换成字符串
            string returnData = Convert.ToBase64String(encryptdata);
            return returnData;
        }
    }

}
