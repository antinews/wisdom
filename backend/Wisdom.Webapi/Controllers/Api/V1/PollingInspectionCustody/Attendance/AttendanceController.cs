using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wisdom.Webapi.Extensions;
using Sugar.Enties;
using SqlSugar;
using Wisdom.Webapi.ViewModels.Maintenance.Attendance;
using Wisdom.Webapi.Utils;
using Wisdom.Webapi.Models.Export;
using Wisdom.Webapi.Extensions.DataBase;
using Wisdom.Webapi.Models.Request.PollingInspectionCustody.Attendance;

namespace Wisdom.Webapi.Controllers.Api.V1.PollingInspectionCustody.Attendance
{
    [Route("api/v1/[controller]/[action]")]
    public class AttendanceController : BaseController
    {
        [HttpPost]
        public IActionResult List(AttendanceRecordRequestPayload payload)
        {
            //var query = base.db.Queryable<attendance_record, sys_users, sys_departments>((a, b, c) => new JoinQueryInfos(
            //    JoinType.Left, a.UserId == b.Id, JoinType.Left, b.DepartmentId == c.Id
            //    )).GroupBy("userid, date_format(starttime,'%Y-%m-%d')"); // left(AttendanceRecordId, locate('@',AttendanceRecordId) + 10)
            //if (payload.userId != null) query = query.Where(a => a.UserId == payload.userId);
            //if (payload.stime != null) query = query.Where(a => a.StartTime >= payload.stime);
            //if (payload.etime != null) query = query.Where(a => a.EndTime <= payload.etime);
            //int totalCount = 0;
            //var list = query.Select<AttendanceRecordJsonModel>().OrderBy(a => a.StartTime).ToPageList(payload.PageNo, payload.PageSize, ref totalCount);

            string sql = $"SELECT distinct attendanceRecordId,department,date_format(starttime,'%Y-%m-%d') date, sum(TotalTime) totalTime, sum(Distance) distance FROM attendance_record where 1=1";

            if (payload.userId != null) sql += $" and UserId={payload.userId}";
            if (payload.stime != null) sql += $" and StartTime >= '{payload.stime}'";
            if (payload.etime != null) sql += $" and EndTime <= '{payload.etime}'";
            sql += " GROUP BY userid, 3 order by 3";
            int totalCount = 0;
            var list = base.db.SqlQueryable<dynamic>(sql).ToPageList(payload.PageNo, payload.PageSize, ref totalCount);
            response.SetData(list, totalCount);
            return Ok(response);
        }
        /// <summary>
        /// 用户出勤轨迹
        /// </summary>
        /// <param name="param">admin@2017-10-13</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AttendanceList(string param)
        {
            string sql = $"left(AttendanceRecordId, locate('@',AttendanceRecordId) + 10) = '{param}'";
            var data = base.db.Queryable<attendance_record>().Where(sql).ToList();
            base.response.SetData(data);
            return Ok(base.response);
        }
        /// <summary>
        /// 轨迹数据
        /// </summary>
        /// <param name="id">MissionId</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Trace(int id)
        {
            string sql = $"select XLongitude lng, YLatitude lat  from attendance_missionpoint where pathid = (select pathid from attendance_mission where keyid = {id})";
            var data = base.db.Ado.SqlQuery<dynamic>(sql);
            base.response.SetData(data);
            return Ok(base.response);
        }
        [HttpGet]
        public IActionResult Remove(string param)
        {
            string sql = $"left(AttendanceRecordId, locate('@',AttendanceRecordId) + 10) = '{param}'";
            base.db.Deleteable<attendance_record>().Where(sql).ExecuteCommand();
            base.response.SetSuccess();
            return Ok(base.response);
        }
        [HttpPost]
        public IActionResult Export(AttendanceRecordRequestPayload payload)
        {
            var query = base.db.Queryable<attendance_record, sys_users, sys_departments>((a, b, c) => new JoinQueryInfos(
                JoinType.Left, a.UserId == b.Id, JoinType.Left, b.DepartmentId == c.Id
                ));
            if (payload.userId != null) query = query.Where(a => a.UserId == payload.userId);
            if (payload.stime != null) query = query.Where(a => a.StartTime >= payload.stime);
            if (payload.etime != null) query = query.Where(a => a.EndTime <= payload.etime);
            int totalCount = 0;
            var list = query.OrderBy(a => a.StartTime).Select<AttendanceRecordExportModel>().ToPageList(payload.PageNo, payload.PageSize, ref totalCount);
            var contents = FileHelper.ListToByteArray<AttendanceRecordExportModel>(list);
            return File(contents, "application/ms-excel", "考勤记录" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx");
        }

    }
}
