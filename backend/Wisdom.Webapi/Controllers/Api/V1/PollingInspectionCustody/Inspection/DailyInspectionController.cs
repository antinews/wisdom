using Entitys;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wisdom.Webapi.Entities.Maintenance;
using Wisdom.Webapi.Extensions;
using Wisdom.Webapi.Extensions.DataBase;
using Wisdom.Webapi.Models.Request.PollingInspectionCustody.Inspection;
using Wisdom.Webapi.Utils;

namespace Wisdom.Webapi.Controllers.Api.V1.PollingInspectionCustody.Inspection
{

    /// <summary>
    /// 操作主体对象--病害
    /// </summary>
    [Route("api/v1/[controller]/[action]")]
    public class DailyInspectionController : BaseController
    {
        [HttpPost]
        public IActionResult List(DailyInspectionRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            var query = base.db.Queryable<v_daily_defectrecord>();
            if (payload.departId != null) query = query.Where(x => x.DepartmentId == payload.departId);
            if (payload.userId != null) query = query.Where(x => x.UserId == payload.userId);
            if (payload.stime != null) query = query.Where(x => x.RecordTime >= payload.stime);
            if (payload.etime != null) query = query.Where(x => x.RecordTime <= payload.etime);
            if (!string.IsNullOrWhiteSpace(payload.status)) query = query.Where(x => x.StatusHandle == payload.status);
            if (payload.areaId != null) query = query.Where(x => x.CityAreaId == payload.areaId);
            if (payload.bridgeId != null) query = query.Where(x => x.BridgeId == payload.bridgeId);
            int total = 0;
            var data = query.ToPageList(payload.PageNo, payload.PageSize, ref total);
            response.SetData(data);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Edit(inspection_daily model)
        {
            var response = ResponseModelFactory.CreateInstance;
            base.db.Updateable<inspection_daily>(model).IgnoreColumns(x => new { x.CreatedTime }).ExecuteCommand();
            return Ok(response);
        }
        [HttpPost]
        public IActionResult Create(inspection_daily model)
        {
            var response = ResponseModelFactory.CreateInstance;
            model.CreatedTime = DateTime.Now;
            base.db.Insertable<inspection_daily>(model).ExecuteCommand();
            return Ok(response);
        }
        [HttpGet]
        public IActionResult Remove(string ids)
        {
            var list = ids.Split(',').Select(x => int.Parse(x)).ToList();
            var response = base.db.UseTransaction(() =>
            {
                base.db.Deleteable<inspection_daily>(list).ExecuteCommand();
            });
            return Ok(response);
        }
        [HttpPost]
        public IActionResult Upload()
        {
            var files = base.Request.Form.Files;
            string path = @"\Uploads\DailyInspection";
            var response = FileHelper.UploadFile(files, path);
            return Ok(response);
        }


        /// <summary>
        /// lazy下拉框
        /// </summary>
        /// <param name="type"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDropDownListData(DropDownType type, int? a,int? b)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            string sql = GetSqlByDropDownType(type, a, b);
            var data = base.db.Ado.SqlQuery<dynamic>(sql);
            response.SetData(data);
            return Ok(response);
        }

        private string GetSqlByDropDownType(DropDownType type, int? a, int? b)
        {
            switch (type)
            {
                case DropDownType.Area: // 区域
                    return "select keyid as id, areaname as text from base_cityarea";
                case DropDownType.BridgeType: // 桥型
                    return "select KeyId as id,BridgeTypeName as text From Base_Ass_BridgeType";
                case DropDownType.Bridge: // 桥梁  a：区域id，b：桥梁类型
                    return $"SELECT KeyId as id,BridgeName as text FROM Base_BuildBridge where IsDelete!=1 AND CityAreaId={a} and bridgetypeid = {b}";
                case DropDownType.Part: // 部位  a：桥梁类型id
                    return $"SELECT OptionTypeId id, OptionName as text FROM Base_Daily_InspectionOption where BridgeTypeId ={a} AND level=1";
                case DropDownType.Item: // 项目  a：桥梁类型id， b：部位id 
                    return $"SELECT OptionTypeId id, OptionName as text FROM Base_Daily_InspectionOption where BridgeTypeId={a} AND ParentId ={b}";
                case DropDownType.Disease: // 病害  a：项目id
                    return $"select keyid id,itemname text,unit from Base_Daily_InspectionItem where OptionTypeId = {a}";
                default:
                    throw new Exception($"Wrong DropDownType：{type}, params：{a},{b}");
            }
        }
    }

    public enum DropDownType
    {
        Area,BridgeType,Bridge,Part,Item,Disease
    }
}
