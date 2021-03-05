using SqlSugar;
using Sugar.Enties;
using System.Collections.Generic;

namespace Wisdom.Webapi.Extensions.DataBase
{
    public static class RemoveTran
    {
        public static void RemoveMenu(this SqlSugarClient db, List<int> ids) => db.Deleteable<sys_menus>(ids).ExecuteCommand();
        public static void RemoveRole(this SqlSugarClient db, List<int> ids) => db.Deleteable<sys_roles>(ids).ExecuteCommand();
        public static void RemovePermission(this SqlSugarClient db, List<int> ids) => db.Deleteable<sys_permissions>(ids).ExecuteCommand();
        public static void RemoveAttendance(this SqlSugarClient db, List<int> ids)
        {
            db.Deleteable<attendance_mission>(ids).ExecuteCommand();
        }
        public static void RemoveUser(this SqlSugarClient db, List<int> ids)
        {
            db.Deleteable<sys_users>(ids).ExecuteCommand();
        }
    }
}
