using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sugar.Enties;
using Wisdom.Webapi.Models;
using Wisdom.Webapi.Models.System.Departement;
using Wisdom.Webapi.Util;

namespace Wisdom.Webapi.Controllers.Systematic
{
    /// <summary>
    /// 部门
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : BaseController
    {
        protected readonly log4net.ILog logger = log4net.LogManager.GetLogger("DepartmentController");

        /// <summary>
        /// 查看所有部门列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDepartement()
        {
            JsonResponse response = null;
            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    var list = db.Queryable<sys_departments>().OrderBy(x => x.Sortnum).ToList();
                    response = new JsonResponse(200, "获取成功", list);
                }
            }
            catch (Exception ex)
            {
                response = new JsonResponse(300, ex.Message, null);
            }
            return Ok(response);
        }
        /// <summary>
        /// 查看所有部门列表2
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetTreeDate()
        {
            JsonResponse response = null;
            using (var db = SugarDao.GetInstance())
            {

                var list = db.Queryable<sys_departments>().OrderBy(x=>x.Sortnum).Select(x => new Trees
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    DepartmentName = x.DepartmentName,
                    Sortnum = x.Sortnum,
                    CreatedTime=x.CreatedTime,
                    Remark = x.Remark
                }).ToList();
                var tree = list.BuildTree();
                var TreeDate= tree.Where(x => x.ParentId == 0);
                response = new JsonResponse(200, "获取成功", TreeDate);
            }
            return Ok(response);
        }
        /// <summary>
        /// 查找父级
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet, Route("{ID}")]
        public IActionResult GetDepartementList(int ID)
        {
            JsonResponse response = null;
            try
            {
                string sql = string.Format("select * FROM sys_departments where ParentId={0}  ORDER BY Sortnum", ID);
                using (var db = SugarDao.GetInstance())
                {
                    logger.Debug("GetDepartementList sql:" + sql);
                    var list = db.SqlQueryable<sys_departments>(sql).ToList();

                    response = new JsonResponse(200, "获取成功", list);
                }
            }
            catch (Exception ex)
            {
                response = new JsonResponse(300, ex.Message, null);
            }
            return Ok(response);
        }
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddDepartement(Departement departement)
        {
            JsonResponse response = null;
            try
            {
                var dc = new Dictionary<string, object>();
                dc.Add("DepartmentName", departement.departmentName);
                dc.Add("CreatedTime", DateTime.Now);
                dc.Add("CreatedUserId", departement.createdUserId);
                dc.Add("ParentId", departement.parentId);
                dc.Add("Sortnum", departement.sortnum);
                using(var db = SugarDao.GetInstance())
                {
                    int i= db.Insertable(dc).AS("sys_departments").ExecuteCommand();
                    if (i > 0)
                    {
                        response = new JsonResponse(200, "添加成功", null);
                    }
                    else
                    {
                        response = new JsonResponse(300, "添加失败", null);
                    }
                }
            }
            catch(Exception ex)
            {
                response = new JsonResponse(300, ex.Message, null);
            }
            return Ok(response);
        }
        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="departement"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SetDepartementById(Departement departement)
        {
            JsonResponse response = null;
            string sql = string.Format("select * FROM sys_departments where Id={0}  ORDER BY Sortnum", departement.Id);
            using (var db = SugarDao.GetInstance())
            {
                logger.Debug("GetDepartementList sql:" + sql);
                var list = db.SqlQueryable<sys_departments>(sql).ToList();
                list[0].DepartmentName = departement.departmentName;
                list[0].ParentId = departement.parentId;
                list[0].Sortnum = departement.sortnum;
                list[0].ModifyUserId = departement.createdUserId;
                list[0].CreatedTime = DateTime.Now;
               int index = db.Updateable(list[0]).Where(it => it.Id == list[0].Id).ExecuteCommand();
                if (index>0)
                {
                    response = new JsonResponse(200, "修改成功", null);
                }
                else
                {
                    response = new JsonResponse(300, "修改失败", null);
                }
            }
            return Ok(response);
        }
        /// <summary>
        /// 模糊查询名字
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        [HttpGet,Route("{Name}")]
        public IActionResult GetDepartementByName(string Name)
        {
            JsonResponse resp = null;
            try
            {
                using(var db = SugarDao.GetInstance())
                {

                    var sql = string.Format(" select * from sys_departments where DepartmentName like '%{0}%'", Name);
                    logger.Debug("GetDepartementList sql:" + sql);
                    var list = db.SqlQueryable<sys_departments>(sql).ToList();

                    resp = new JsonResponse(200, "获取成功", list);
                }
            }catch(Exception ex)
            {
                resp = new JsonResponse(300, ex.Message, null);
            }
            return Ok(resp);
        }
        /// <summary>
        /// 删除单个
        /// </summary>
        /// <param name="departement"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DelDepartement(Departement departement)
        {
            JsonResponse resp = null;
            
            try
            {
                    using (var db = SugarDao.GetInstance())
                    {

                        int i = db.Deleteable<sys_departments>().Where(it => it.Id == departement.Id).ExecuteCommand();
                        if (i > 0)
                        {
                            resp = new JsonResponse(200, "删除成功", null);
                        }
                        else
                        {
                            resp = new JsonResponse(300, "删除失败", null);
                        }
                    }
            
            }catch(Exception ex)
            {
                resp = new JsonResponse(300, ex.Message, null);
            }
            return Ok(resp);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="departement"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DelDepartementList(Departement departement)
        {
            var ID = departement.IDs.Split(',');
            int[] list = new int[ID.Length];
            for (int index = 0; index < ID.Length; index++)
            {
                list[index] =int.Parse(ID[index]);
            }
           
            JsonResponse resp = null;
            try
            {
                using(var db = SugarDao.GetInstance())
                {
                    int i= db.Deleteable<sys_departments>().In(list).ExecuteCommand();
                    if (i > 0)
                    {
                        resp = new JsonResponse(200, "删除成功", null);
                    }
                    else
                    {
                        resp = new JsonResponse(300, "删除失败", null);
                    }
                }
                
            }
            catch(Exception ex)
            {
                resp = new JsonResponse(300, ex.Message, null);
            }
            return Ok(resp);
        }

       
    }
    public static class MenuTreeHelperS
    {
        public static List<Trees> BuildTree(this List<Trees> menu)
        {
            var lookup = menu.ToLookup(x => x.ParentId);
            Func<int, List<Trees>> build = null;
            build = pid =>
            {
                return lookup[pid].Select(x=>new Trees { 
                    Id=x.Id,
                    ParentId=x.ParentId,
                    Remark=x.Remark,
                    Sortnum=x.Sortnum,
                    CreatedTime=x.CreatedTime,
                    Children=build(x.Id),
                    DepartmentName=x.DepartmentName
                }).ToList();
            };

            var items = menu.Where((x, i) => menu.FindIndex(z => z.ParentId == x.ParentId) == i).ToList();

            List<Trees> trees = new List<Trees>();
            for (var i = 0; i < items.Count; i++)
            {
                int x = int.Parse(items[i].ParentId.ToString());
                for(var j=0;j< build(x).Count; j++)
                {
                    
                        trees.Add(build(x)[j]);
                   
                    
                }
            }
           
            
            return trees;
        }
    }

    public class Trees
    {
        public int Id { get; set; }

        /// <summary>
        /// Desc:部门名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DepartmentName { get; set; }

        /// <summary>
        /// Desc:父ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? ParentId { get; set; }

        /// <summary>
        /// Desc:排序
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Sortnum { get; set; }

        /// <summary>
        /// Desc:部门描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Remark { get; set; }

        public DateTime CreatedTime { get; set; }
        public List<Trees> Children { get; set; }
    }
}
