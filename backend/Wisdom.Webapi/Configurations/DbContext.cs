using SqlSugar;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Configurations
{
    public class DbContext
    {
        public static bool IsDevelopment;

        /// <summary>
        /// 主数据库（线程通用）
        /// </summary>
        /// <returns></returns>
        public static SqlSugarClient GetDbContextInstance()
        {
            SqlSugarClient dbContext = CallContext.GetData("dbContext") as SqlSugarClient;
            if (dbContext == null)
            {
                dbContext = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = Configurations.AppSettings.GetAppSeetingSectionByKey("BaseConnection"),
                    DbType = DbType.MySql,
                    InitKeyType = InitKeyType.Attribute,//从实体特性中读取主键自增列信息
                    IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放

                });
                if (IsDevelopment)
                {
                    //调式代码 用来打印SQL 
                    dbContext.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        //Console.WriteLine(sql + "\r\n" +
                        //    dbContext.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                        //Console.WriteLine();
                        foreach (var p in pars)
                        {
                            sql = sql.Replace(p.ParameterName, "'" + p.Value + "'");
                        }
                        Console.ForegroundColor = ConsoleColor.Green;//默认ConsoleColor.Gray
                        Console.WriteLine(sql);
                    };
                }
                CallContext.SetData("dbContext", dbContext);
            }


            return dbContext;
        }
    }

    public static class CallContext
    {
        static ConcurrentDictionary<string, AsyncLocal<object>> state = new ConcurrentDictionary<string, AsyncLocal<object>>();

        public static void SetData(string name, object data) =>
            state.GetOrAdd(name, _ => new AsyncLocal<object>()).Value = data;

        public static object GetData(string name) =>
            state.TryGetValue(name, out AsyncLocal<object> data) ? data.Value : null;
    }
}
