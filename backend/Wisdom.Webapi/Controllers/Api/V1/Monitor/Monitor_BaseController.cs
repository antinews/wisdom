using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using log4net;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Edge.WebApi.Entity.Monitor;
using Wisdom.Webapi.Configurations;
using Wisdom.Webapi.Models.Response;
using Wisdom.Webapi;
using Wisdom.Webapi.Extensions.CustomException;
using Wisdom.Webapi.Auth.AuthContext;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Edge.WebApi.Controllers
{
    /// <summary>
    /// 基类勿动
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Monitor_BaseController : Controller
    {
        protected ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(GlobalExceptionFilter));
        protected SqlSugarClient db = DbContext.GetDbContextInstance();
        protected ResponseModel response = new ResponseModel();
        protected AuthContextUser CurrentUser = AuthContextService.CurrentUser;
        [NonAction]
        protected void Debug(string message)
        {
            log.Debug(this.GetType().Name + "控制器的" + new System.Diagnostics.StackTrace(true).GetFrame(1).GetMethod().Name + "方法:" + message);
        }
        //protected List<int> FindStruct()
        //{
        //    return string.IsNullOrEmpty(AuthContextService.CurrentUserNew.UserId) ||AuthContextService.CurrentUserNew.UserId == "2b6e64dc-e144-4d4a-bef2-05a92294c3bc" ? db.Queryable<IOT_ConfStruct>().Select(s => s.Id).ToList() : db.Queryable<IOT_ConfUserStruct>().Where(w => w.UserId == AuthContextService.CurrentUserNew.UserId).Select(s => s.StructId).ToList();
        //}
        [NonAction]
        protected void UpdatePointsNumber(int pid)
        {
            int pnumber = db.Queryable<Monitor_Points>().Where(w => w.IsActived == 1 && w.ProjectId == pid).Count();
            Updateable<Monitor_Projects>(t => new Monitor_Projects { PointsNumber = pnumber }, w => w.Id == pid);
        }
        [NonAction]
        protected void UpdateStationNumber(int pid)
        {
            int pnumber = db.Queryable<Monitor_Devices>().Where(w => w.IsActived == 1 && w.ProjectId == pid).Count();
            Updateable<Monitor_Projects>(t => new Monitor_Projects { DeviceNumber = pnumber }, w => w.Id == pid);
        }
        [NonAction]
        protected T Single<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            return db.Queryable<T>().Single(expression);
        }
        /// <summary>
        /// 主键实体更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        [NonAction]
        protected int Updateable(dynamic updateDynamicObject)
        {
            return db.Updateable(updateDynamicObject).ExecuteCommand();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateDynamicObject"></param>
        /// <returns></returns>
        [NonAction]
        protected int Updateable<T>(Expression<Func<T, T>> columns, Expression<Func<T, bool>> expression) where T : class, new()
        {
            return db.Updateable<T>().UpdateColumns(columns).Where(expression).ExecuteCommand();
        }
        /// <summary>
        /// 不返回自增插入的ID,可插入数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        [NonAction]
        protected int Insertable(dynamic insertDynamicObject)
        {
            return db.Insertable(insertDynamicObject).ExecuteCommand();
        }
        /// <summary>
        /// 可返回自增插入的ID,,可插入数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        [NonAction]
        protected int InsertableReturnIdentity<T>(T entity) where T : class, new()
        {
            return db.Insertable<T>(entity).ExecuteReturnIdentity();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        [NonAction]
        protected int Deleteable<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            return db.Deleteable<T>().Where(expression).ExecuteCommand();
        }
    }
}
