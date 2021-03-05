using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Configurations
{
    public class AppSettings
    {
        // 配置文件单例  是否线程安全？
        private static IConfigurationSection sections = null;

        public static string GetAppSeetingSectionByKey(string key)
        {
            string str = "";
            if (sections.GetSection(key) != null)
            {
                str = sections.GetSection(key).Value;
            }
            return str;
        }


        public static void SetAppSetting(IConfigurationSection section)
        {
            sections = section;
        }
    }
}
