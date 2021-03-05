using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sugar.Enties;
using Wisdom.Webapi.Models;
using Wisdom.Webapi.Models.Request.Systems;
using Wisdom.Webapi.Util;

namespace Wisdom.Webapi.Controllers.Api.V1.Systems
{
    /// <summary>
    /// 字典类型
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DiccategorysController : BaseController
    {
        /// <summary>
        /// 获取字典类型列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDiccategorys()
        {
            JsonResponse response = null;
            try
            {
                using(var db = SugarDao.GetInstance())
                {
                    var list = db.Queryable<sys_diccategorys>().OrderBy(x=>x.SortNum).ToList();
                    response = new JsonResponse(200, "获取成功", list);
                }
            }catch(Exception ex)
            {
                response = new JsonResponse(300, ex.Message, null);
            }
            return Ok(response);
        }
        /// <summary>
        /// 查看字典类型名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet,Route("{id}")]
        public IActionResult GetDiccategorysByID(int id)
        {
            JsonResponse response = null;
            try
            {
                var sql = string.Format("select * from sys_diccategorys where id = {0} Order by SortNum", id);
                using(var db = SugarDao.GetInstance())
                {
                  var list=   db.SqlQueryable<sys_diccategorys>(sql).ToList();
                   response = new JsonResponse(200, "获取成功", list[0].Title);
                }
            }catch(Exception ex)
            {
                response = new JsonResponse(300, ex.Message, null);
            }
            return Ok(response);
        }
        /// <summary>
        /// 删除单个字典类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet,Route("{id}")]
        public IActionResult DelDiccategorysByID(int id)
        {
            JsonResponse response = null;
            try
            {
                using(var db = SugarDao.GetInstance())
                {
                  int i=  db.Deleteable<sys_diccategorys>().Where(item => item.Id == id).ExecuteCommand();
                    if (i > 0)
                    {
                        response = new JsonResponse(200, "删除成功", null);
                    }
                    else
                    {
                        response = new JsonResponse(300, "删除失败", null);
                    }
                }
            }catch(Exception ex)
            {
                response = new JsonResponse(300, ex.Message, null);
            }
            return Ok(response);
        }
        /// <summary>
        /// 添加字典类型
        /// </summary>
        /// <param name="diccategorys"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddDiccategory(Diccategorys diccategorys)
        {
            JsonResponse response = null;
            try
            {
                using(var db = SugarDao.GetInstance())
                {
                    var dc = new Dictionary<string, object>();
                    dc.Add("Title", diccategorys.Title);
                    dc.Add("SortNum", diccategorys.SortNum);
                    dc.Add("Remark", diccategorys.Remark);
                    dc.Add("Code", diccategorys.Code);
                    dc.Add("CreatedUserId", diccategorys.UserID); //base.CurrentUser.Id
                    dc.Add("CreatedTime", DateTime.Now);
                    int i = db.Insertable(dc).AS("sys_diccategorys").ExecuteCommand();
                    if (i > 0)
                    {
                        response = new JsonResponse(200, "添加成功", null);
                    }
                    else
                    {
                        response = new JsonResponse(300, "添加失败", null);
                    }
                }
            }catch(Exception ex)
            {
                response = new JsonResponse(300, ex.Message, null);
            }
            return Ok(response);
        }
        /// <summary>
        /// 修改字典类型
        /// </summary>
        /// <param name="diccategorys"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SetDiccategoryById(Diccategorys diccategorys)
        {
            JsonResponse response = null;
            try
            {
                string sql = string.Format("select * from sys_diccategorys where id={0}", diccategorys.ID);
                var list = db.SqlQueryable<sys_diccategorys>(sql).ToList();
                list[0].ModifyTime = DateTime.Now;
                list[0].ModifyUserId = diccategorys.UserID;
                list[0].Code = diccategorys.Code;
                list[0].Remark = diccategorys.Remark;
                list[0].SortNum = diccategorys.SortNum;
                list[0].Title = diccategorys.Title;
                int index = db.Updateable(list[0]).Where(it => it.Id == list[0].Id).ExecuteCommand();
                if (index > 0)
                {
                    response = new JsonResponse(200, "修改成功", null);
                }
                else
                {
                    response = new JsonResponse(300, "修改失败", null);
                }
            }
            catch(Exception ex)
            {
                response = new JsonResponse(300, ex.Message, null);
            }
            return Ok(response);
        }
    }
}
