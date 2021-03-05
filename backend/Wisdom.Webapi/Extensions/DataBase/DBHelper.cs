using log4net;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using Wisdom.Webapi.Configurations;
using Wisdom.Webapi.Extensions.CustomException;
using Wisdom.Webapi.Models.Response;

namespace Wisdom.Webapi.Extensions.DataBase
{
    public static class DBHelper
    {
        private static ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(GlobalExceptionFilter));
        public static ResponseModel UseTransaction(this SqlSugarClient client, Action action)
        {
            var response = ResponseModelFactory.CreateInstance;
            var tran = client.Ado.UseTran(()=>
            {
                action();
            });
            if (!tran.IsSuccess)
            {
                log.Error(tran.ErrorMessage + "\n" + tran.ErrorException);
                response.SetFailed(tran.ErrorMessage);
            }
            return response;
        }
        public static ResponseModel Delete(this SqlSugarClient client, string ids, Action<List<int>> action)
        {
            var list = ids.Split(',').Select(x => int.Parse(x)).ToList();
            return DoDelete(client, list, action);
        }

        public static ResponseModel Delete(this SqlSugarClient client, int id, Action<List<int>> action)
        {
            var list = new List<int>();
            list.Add(id);
            return DoDelete(client, list, action);
        }

        static ResponseModel DoDelete(this SqlSugarClient client, List<int> list, Action<List<int>> action)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (list.Count < 1)
            {
                response.SetFailed("请至少选择一个删除目标");
                return response;
            }
            var tran = client.Ado.UseTran(() =>
            {
                action(list);
            });
            if (tran.IsSuccess)
            {
                response.SetSuccess("删除记录成功！");
            }
            else
            {
                log.Error(tran.ErrorMessage + "\n" + tran.ErrorException);
                response.SetError("删除记录失败，异常信息为：" + tran.ErrorMessage);
            }
            return response;
        }
    }
}
