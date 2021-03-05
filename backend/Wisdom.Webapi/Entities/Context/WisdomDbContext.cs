using SqlSugar;
using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Entities.Context
{
    public class WisdomDbContext
    {
        public WisdomDbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                DbType = DbType.MySql,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了

            });
            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };

        }
        //注意：不能写成静态的
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作

        public SimpleClient<sys_buttons> sys_buttonsDb { get { return new SimpleClient<sys_buttons>(Db); } }//用来处理sys_buttons表的常用操作
        public SimpleClient<sys_departments> sys_departmentsDb { get { return new SimpleClient<sys_departments>(Db); } }//用来处理sys_departments表的常用操作
        public SimpleClient<sys_diccategorys> sys_diccategorysDb { get { return new SimpleClient<sys_diccategorys>(Db); } }//用来处理sys_diccategorys表的常用操作
        public SimpleClient<sys_dics> sys_dicsDb { get { return new SimpleClient<sys_dics>(Db); } }//用来处理sys_dics表的常用操作
        public SimpleClient<sys_logs> sys_logsDb { get { return new SimpleClient<sys_logs>(Db); } }//用来处理sys_logs表的常用操作
        public SimpleClient<sys_menubutton> sys_menubuttonDb { get { return new SimpleClient<sys_menubutton>(Db); } }//用来处理sys_menubutton表的常用操作
        public SimpleClient<sys_menus> sys_menusDb { get { return new SimpleClient<sys_menus>(Db); } }//用来处理sys_menus表的常用操作
        public SimpleClient<sys_roles> sys_rolesDb { get { return new SimpleClient<sys_roles>(Db); } }//用来处理sys_roles表的常用操作
        public SimpleClient<sys_users> sys_usersDb { get { return new SimpleClient<sys_users>(Db); } }//用来处理sys_users表的常用操作
    }
}
