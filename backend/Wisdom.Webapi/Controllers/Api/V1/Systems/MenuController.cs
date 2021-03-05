using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wisdom.Webapi.Extensions;
using Wisdom.Webapi.Models.Request.Systems;
using Sugar.Enties;
using Wisdom.Webapi.ViewModels.Systems.Mneu;
using Wisdom.Webapi.Extensions.DataBase;
using Wisdom.Webapi.ViewModels.Systems.Permission;
using static Wisdom.Webapi.Entities.Enum.CommonEnum;

namespace Wisdom.Webapi.Controllers.Api.V1.Systems
{
    [Route("api/v1/[controller]/[action]")]
    public class MenuController:BaseController
    {
        /// <summary>
        /// 菜单管理页面列表
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(MenuRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            var menuList = base.db.Queryable<sys_menus>().ToList();
            var query = menuList.Where(x => x.ParentId == 0);
            if (!string.IsNullOrEmpty(payload.kw)) query = query.Where(x => x.NavTitle.Contains(payload.kw));
            query =query.OrderBy(x => x.Sort);
            List<MenuNode> menu = new List<MenuNode>();
            foreach (var root in query)
            {
                var item = base.Mapper.Map<sys_menus, MenuNode>(root);
                item.Children = BuildTree(menuList, root.Id);
                menu.Add(item);

            };
            int size = payload.PageSize,star = size * (payload.PageNo - 1);
            int total = menu.Count;
            int count = star + size > total ? total - star : star + size;
            menu = menu.GetRange(star, count);
            response.SetData(menu, total);
            return Ok(response);
        }

        /// <summary>
        /// 编辑菜单基础信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(sys_menus model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (model.ParentId == 0) model.Level = 1;
            else model.Level = base.db.Queryable<sys_menus>().First(x => x.Id == model.ParentId).Level + 1;
            model.ModifyUserId = base.CurrentUser.Id;
            model.ModifyTime = DateTime.Now;
            base.db.Updateable(model).IgnoreColumns(x=>new { x.CreatedTime, x.CreatedUserId}).ExecuteCommand();
            return Ok(response);
        }

        /// <summary>
        /// 新建菜单，排序默认为当前Level中排序最大值+5
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(sys_menus model)
        {
            if (model.ParentId == 0) model.Level = 1;
            else model.Level = base.db.Queryable<sys_menus>().First(x => x.Id == model.ParentId).Level + 1;
            var menus = base.db.Queryable<sys_menus>().Where(x => x.ParentId == model.ParentId).ToList();
            model.Sort = menus.Max(x => x.Sort) + 5??0;
            model.CreatedUserId = base.CurrentUser.Id;
            model.CreatedTime = DateTime.Now;
            var response = base.db.UseTransaction(() =>
            {
                int id = base.db.Insertable<sys_menus>(model).ExecuteReturnIdentity();
                if (model.IsPage == 1) // 页面才有permissions
                {
                    base.db.Insertable<sys_permissions>(new sys_permissions
                    {
                        MenuId = id,
                        Name = "浏览",
                        ActionCode = model.Name + "_look",
                        Type = 1,
                        CreatedTime = DateTime.Now,
                        CreatedUserId = base.CurrentUser.Id
                    }).ExecuteCommand();
                }
            });
            return Ok(response);
        }
        [HttpGet]
        public IActionResult Remove(string ids) => Ok(base.db.Delete(ids, x => db.RemoveMenu(x)));
       
        /// <summary>
        /// 编辑菜单权限
        /// </summary>
        [HttpPost]
        public IActionResult EditPermissions(PermissionEditModel model)
        {
            var data = model.Permissions.Select(x => new sys_permissions
            {
                MenuId = model.MenuId,
                Name = x.Name,
                ActionCode = x.Code,
                Type = (int)(x.Name is "浏览"? PermissionType.Page:PermissionType.Button),
                CreatedTime = DateTime.Now,
                CreatedUserId = base.CurrentUser.Id
            }).ToList();
            var response = base.db.UseTransaction(() =>
            {
                base.db.Deleteable<sys_permissions>().Where(x => x.MenuId == model.MenuId).ExecuteCommand();
                base.db.Insertable<sys_permissions>(data).ExecuteCommand();
            });

            return Ok(response);
        }

        /// <summary>
        /// 获取菜单权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPermissionsByMenuId(int id)
        {
            var response = ResponseModelFactory.CreateInstance;
            var data = base.db.Queryable<sys_permissions>().Where(x => x.MenuId == id).Select<Permission>("ActionCode Code,Name").ToList();
            response.SetData(data);
            return Ok(response);
        }
        /// <summary>
        /// 菜单树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Tree(int? id)
        {
            var response = ResponseModelFactory.CreateInstance;
            var tree = LoadMenuTree(id);
            response.SetData(tree);
            return Ok(response);
        }
       
        
        /// <summary>
        /// 加载菜单树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [NonAction]
        private List<MenuTree> LoadMenuTree(int? id)
        {
            var temp = base.db.Queryable<sys_menus>().ToList().Select(x => new MenuTree
            {
                Id = x.Id,
                ParentId = x.ParentId ?? 0,
                Label = x.NavTitle,
                Level = x.Level?? - 1
            }).ToList();
            var root = new MenuTree
            {
                Label = "顶级菜单",
                Id = 0,
                ParentId = -1,
                Level = 0
            };
            temp.Insert(0, root);
            var tree = temp.BuildTree(id);
            return tree;
        }

        /// <summary>
        /// 为列表构建树
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        [NonAction]
        private List<MenuNode> BuildTree(List<sys_menus> menus, int? pid)
        {
            var list = menus.Where(x => x.ParentId == pid).OrderBy(x => x.Sort).ToList();
            var res = new List<MenuNode>();
            foreach (var node in list)
            {
                var item = base.Mapper.Map<sys_menus, MenuNode>(node);
                item.Children = BuildTree(menus, node.Id);
                res.Add(item);
            }

            return res;
        }
    }

    public static class MenuTreeHelper
    {
        /// <summary>
        /// 构建Element-Tree
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="selectedId">选择的id</param>
        /// <returns></returns>
        public static List<MenuTree> BuildTree(this List<MenuTree> menus, int? selectedId = -1)
        {
            var lookup = menus.ToLookup(x => x.ParentId);
            Func<int, List<MenuTree>> build = null;
            build = pid =>
            {
                return lookup[pid]
                 .Select(x => new MenuTree()
                 {
                     Id = x.Id,
                     ParentId = x.ParentId,
                     Label = x.Label,
                     Level = x.Level,
                     Expand = x.ParentId == 0,
                     Selected = selectedId == x.Id,
                     Children = build(x.Id),
                 })
                 .ToList();
            };
            return build(-1);
        }
    }
}
