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
    /// 字典
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DicsController : BaseController
    {
        /// <summary>
        /// 获取字典类型获取字典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet,Route("{id}")]
        public IActionResult GetDicsListByID(int id)
        {
            JsonResponse response = null;
            var sql = "select * from sys_dics where CategoryId = " + id+"";
            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    var list = db.SqlQueryable<sys_dics>(sql).ToList();
                    response = new JsonResponse(200, "获取成功", list);
                }

            }
            catch(Exception ex)
            {
                response = new JsonResponse(300, ex.Message, null);
            }
            return Ok(response);
        }
        /// <summary>
        /// 添加字典
        /// </summary>
        /// <param name="dics"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddDics(Dics dics)
        {
            JsonResponse resp = null;
            try
            {
                var dc = new Dictionary<string, Object>();
                dc.Add("CreatedUserId", dics.UserId);
                dc.Add("Title", dics.Title);
                dc.Add("CreatedTime",DateTime.Now);
                dc.Add("SortNum", dics.SortNum);
                dc.Add("Reamrk", dics.Reamrk);
                dc.Add("Code", dics.Code);
                dc.Add("Status", 1);
                dc.Add("CategoryId", dics.CategoryId);
                using(var db = SugarDao.GetInstance())
                {
                    int i = db.Insertable(dc).AS("sys_dics").ExecuteCommand();
                    if (i > 0)
                    {
                        resp = new JsonResponse(200, "添加成功",null);
                    }
                    else
                    {
                        resp = new JsonResponse(300, "添加失败", null);
                    }
                }
            }catch(Exception ex)
            {
                resp = new JsonResponse(300, ex.Message, null);
            }
            return Ok(resp);
        }
        /// <summary>
        /// 单个字典删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet,Route("{id}")]
        public IActionResult DelDics(int id)
        {
            JsonResponse response = null;
            try
            {
                using(var db = SugarDao.GetInstance())
                {
                    int i = db.Deleteable<sys_dics>().Where(it => it.Id == id).ExecuteCommand();
                    if (i> 0)
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
        /// 修改字典信息
        /// </summary>
        /// <param name="dics"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SetDicsById(Dics dics)
        {
            JsonResponse resp = null;
            try
            {
                string sql = string.Format("select * from sys_dics where id={0}", dics.Id);
                var list = db.SqlQueryable<sys_dics>(sql).ToList();
                list[0].ModifyTime = DateTime.Now;
                list[0].ModifyUserId = dics.UserId;
                list[0].Reamrk = dics.Reamrk;
                list[0].Status = dics.Status;
                list[0].Title = dics.Title;
                list[0].SortNum = dics.SortNum;
                list[0].Code = dics.Code;
                int i = db.Updateable(list[0]).Where(it => it.Id == list[0].Id).ExecuteCommand();
                if (i > 0)
                {
                    resp = new JsonResponse(200, "修改成功", null);
                }
                else
                {
                    resp = new JsonResponse(300, "修改失败", null);
                }
            }
            catch(Exception ex)
            {
                resp = new JsonResponse(300, ex.Message, null);
            }
            return Ok(resp);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="dics"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DelDicsList(Dics dics)
        {
            JsonResponse response = null;
            var ID = dics.IDs.Split(',');
            int[] list = new int[ID.Length];
            for (int index = 0; index < ID.Length; index++)
            {
                list[index] = int.Parse(ID[index]);
            }
            try
            {
                using(var db = SugarDao.GetInstance())
                {
                    int i = db.Deleteable<sys_dics>().In(list).ExecuteCommand();
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
    }
}
