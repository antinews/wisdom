
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Edge.WebApi.Entity.Monitor;
using Edge.WebApi.Entity.Web;
using Entitys;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SqlSugar;

namespace Edge.WebApi.Controllers.Monitor
{
    [Route("api/Monitor/[controller]/[action]")]
    [ApiController]
    public class PointsController : Monitor_BaseController
    {
        private IHostingEnvironment _hostingEnvironment;
        string[] fieds = new string[] { "Id", "PointCode", "PointName", "MonitoringKindId", "Chanel", "MaxValue", "MinValue", "Range1Min", "Range1Max", "Range2Min", "Range2Max", "IsActived", "IsReasonableValidate", "IsThresholdAlarm", "IsBasePoint", "FormulaKey", "FormulaValue" };
        string[] fiedsname = new string[] { "主键勿动", "测点编码", "测点名称", "传感器类型", "通道号", "有效数据上限", "有效数据下限", "一级阈值上限", "一级阈值下限", "二级阈值上限", "二级阈值下限", "是否启用", "是否进行合理性校验", "是否阈值报警", "是否基准点", "测点参数名", "测点参数值" };
        public PointsController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public class PointPageRequest : PageRequest
        {
            /// <summary>	
            /// 状态 1启用0禁用-1默认/全部	
            /// </summary>	
            public int IsActived = -1;
            /// <summary>
            /// 结构物Id
            /// </summary>
            public int ProjectId { get; set; }

            /// <summary>
            /// 测站Id
            /// </summary>
            public int DeviceId { get; set; }
            /// <summary>
            /// 传感器ID
            /// </summary>
            public int MonitoringKindId { get; set; }

            public int AreaId { get; set; }
        }
        /// <summary>
        /// 获取列表（测点）
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(PointPageRequest payload)
        {
            var response = new PageResponse();
            int total = 0;
            //List<int> structIds = FindStruct();
            var list = db.Queryable<Monitor_Points, base_buildbridge_main>((mo, a) => new object[] {
            JoinType.Left,mo.ProjectId==a.KeyId
            });
            if (payload.ProjectId != 0)
            {
                list = list.Where(mo => mo.ProjectId == payload.ProjectId);
            }
            if (payload.AreaId != 0)
            {
                list = list.Where((mo,a)=>a.CityAreaId == payload.AreaId);
            }
            if (payload.DeviceId != 0)
            {
                list = list.Where(mo => mo.DeviceId == payload.DeviceId);
            }
            if (payload.MonitoringKindId != 0)
            {
                list = list.Where(mo => mo.MonitoringKindId == payload.MonitoringKindId);
            }
            if (!string.IsNullOrEmpty(payload.Kw))
            {
                list = list.Where(mo => mo.PointCode.Contains(payload.Kw));
            }
            if (payload.IsActived > -1)
            {
                list = list.Where(mo => mo.IsActived == payload.IsActived);
            }
            var points = list.Select<Monitor_Points>("mo.*,a.BridgeName ProjectName,a.CityArea Area").OrderBy(mo => mo.Id, OrderByType.Desc).ToPageList(payload.CurrentPage, payload.PageSize, ref total);
            var kinds=db.Queryable<Monitor_Kindinfo>().Where(w => points.Select(s=> s.MonitoringKindId).ToList().Contains(w.Id)).ToList();
            var parms = db.Queryable<Monitor_Computeformulainfo>().Where(w => points.Select(s => s.ComputeFormulaId).ToList().Contains(w.Id)).ToList();
            foreach(var p in points)
            {
                p.MonitoringKind = kinds.FirstOrDefault(f => f.Id == p.MonitoringKindId)??new Monitor_Kindinfo();
                p.ComputeFormula = parms.FirstOrDefault(f => f.Id == p.ComputeFormulaId)??new Monitor_Computeformulainfo();
            }
            response.SetData(points);
            response.TotalCount = total;
            return Ok(response);
        }
        /// <summary>
        /// 导出筛选的测点
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult BackPointExcel(int ProjectId, int DeviceId,string Kw, int IsActived = -1)
        {
            if (ProjectId == 0)
            {
                response.SetError("结构物为空了");
                return Ok(response);
            }
            var list = db.Queryable<Monitor_Points, Monitor_Projects>((mo, a) => new object[] {
            JoinType.Left,mo.ProjectId==a.Id
            }).Select<Monitor_Points>("mo.*,a.ProjectName ProjectName");
            if (ProjectId != 0)
            {
                list = list.Where(mo => mo.ProjectId ==ProjectId);
            }
            if (DeviceId != 0)
            {
                list = list.Where(mo => mo.DeviceId == DeviceId);
            }
            if (!string.IsNullOrEmpty(Kw))
            {
                list = list.Where(mo => mo.PointName.Contains(Kw));
            }
            if (IsActived > -1)
            {
                list = list.Where(mo => mo.IsActived == IsActived);
            }
            var points = list.OrderBy(mo => mo.Id, OrderByType.Desc).ToList();
            var kinds = db.Queryable<Monitor_Kindinfo>().Where(w => points.Select(s => s.MonitoringKindId).ToList().Contains(w.Id)).ToList();
            var parms = db.Queryable<Monitor_Computeformulainfo>().Where(w => points.Select(s => s.ComputeFormulaId).ToList().Contains(w.Id)).ToList();
            foreach (var p in points)
            {
                p.MonitoringKind = kinds.FirstOrDefault(f => f.Id == p.MonitoringKindId) ?? new Monitor_Kindinfo();
                p.ComputeFormula = parms.FirstOrDefault(f => f.Id == p.ComputeFormulaId) ?? new Monitor_Computeformulainfo();
            }


            XSSFWorkbook work = new XSSFWorkbook();
           
            ISheet sheet = work.CreateSheet("测点属性");
            int index = 1;
            int cnum = fieds.Length;
            sheet.SetColumnWidth(0, 25 * 256);
            
            ICellStyle stylea = work.CreateCellStyle();
            stylea.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            stylea.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            stylea.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            stylea.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            stylea.BottomBorderColor = HSSFColor.Black.Index;
            stylea.LeftBorderColor = HSSFColor.Black.Index;
            stylea.RightBorderColor = HSSFColor.Black.Index;
            stylea.TopBorderColor = HSSFColor.Black.Index;
            stylea.VerticalAlignment = VerticalAlignment.Center;//垂直对齐(默认应该为center，如果center无效则用justify)
            stylea.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;//水平对齐
            //表头
            IRow rowf = sheet.CreateRow(0);
            for (int i = 0; i < cnum; i++)
            {
                ICell cell = rowf.CreateCell(i);
                cell.CellStyle = stylea;
                cell.SetCellValue(fiedsname[i]);
            }
            for (int i = index; i < index + points.Count; i++)
            {
                IRow row = sheet.CreateRow(i);
                for (int j = 0; j < cnum; j++)
                {
                    ICell cell = row.CreateCell(j);

                    cell.CellStyle = stylea;
                    if(fieds[j]== "FormulaKey"|| fieds[j] == "FormulaValue")
                    {
                        cell.SetCellValue(GetModelValue(fieds[j], points[i - 1].ComputeFormula));
                    }
                    else
                    {
                        cell.SetCellValue(GetModelValue(fieds[j], points[i - 1]));
                    }
                }
            }


            ISheet sheet2 = work.CreateSheet("传感器类型");//表头
            IRow rowf2 = sheet2.CreateRow(0);
            ICell cell2 = rowf2.CreateCell(0);
            cell2.CellStyle = stylea;
            cell2.SetCellValue("主键ID");
            cell2 = rowf2.CreateCell(1);
            cell2.CellStyle = stylea;
            cell2.SetCellValue("传感器名称");
            var kindsall=db.Queryable<Monitor_Kindinfo>().Where(w => w.ProjectId == ProjectId).ToList();

            for (int i = 1; i < kindsall.Count+1; i++)
            {
                IRow row = sheet2.CreateRow(i);
                ICell cell = row.CreateCell(0);
                cell2.CellStyle = stylea;
                cell.SetCellValue(kindsall[i-1].Id);
                cell = row.CreateCell(1);
                cell2.CellStyle = stylea;
                cell.SetCellValue(kindsall[i - 1].KindName);

            }


            var fileFolder = Path.Combine(_hostingEnvironment.ContentRootPath + "/wwwroot", "Uploads/Points/downfile");

            if (!Directory.Exists(fileFolder))
                Directory.CreateDirectory(fileFolder);
            string filename = DateTime.Now.ToString("yyyy-MM-dd-HH:mm-ss.fff") + ".xlsx";
            var filePath = Path.Combine(fileFolder, filename);
            FileStream fex = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            work.Write(fex);
            work.Close();
            fex.Close();
            var imgByte = System.IO.File.ReadAllBytes(filePath);

            return File(new MemoryStream(imgByte), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);//application/vnd.openxmlformats-officedocument.spreadsheetml.sheet//application/vnd.ms-excel

        }
        /// <summary>
        /// 获取类中的属性值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        [NonAction]
        public string GetModelValue(string FieldName, object obj)
        {
            try
            {
                Type Ts = obj.GetType();
                object o = Ts.GetProperty(FieldName).GetValue(obj, null);
                string Value = Convert.ToString(o);
                if (string.IsNullOrEmpty(Value)) return null;
                return Value;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 设置类中的属性值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [NonAction]
        public bool SetModelValue(string FieldName, object obj,object? value)
        {
            try
            {
                Type Ts = obj.GetType();
                value = Convert.ChangeType(value, Ts.GetProperty(FieldName).PropertyType);
                Ts.GetProperty(FieldName).SetValue(obj, value);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取传感器下激活测点数组
        /// </summary>
        /// <param name="kid">传感器Id</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult PointsByKind(int kid)
        {
            response.SetData(db.Queryable<Monitor_Points, Monitor_Devices, Monitor_Kindinfo>((mo, a,b) => new object[] {
            JoinType.Left,mo.DeviceId==a.Id,
            JoinType.Left,mo.MonitoringKindId==b.Id
            }).Select<Monitor_PointsView>("mo.*,concat(a.EdgeMiddleAddr,':',a.EdgeMiddlePort) EdgeMiddleAddr,a.CollectInterval,b.Unit Unit").Where(mo=>mo.MonitoringKindId==kid&&mo.IsActived==1).ToList());
            return Ok(response);
        }
        /// <summary>
        /// 根据ids获取多个测点数组（测点）
        /// </summary>
        /// <param name="id">id多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPointOne(string id)
        {
            List<int> pointids = id.Split(",").Select(s => Convert.ToInt32(s)).ToList();
            if (pointids.Count <= 0)
            {
                response.SetError("意外参数");
                return Ok(response);
            }
            var data = db.Queryable<Monitor_Points, Monitor_Projects>((mo, a) => new object[] {
            JoinType.Left,mo.ProjectId==a.Id
            }).Where((mo, a) => pointids.Contains(mo.Id)).Select<Monitor_Points>("mo.*,a.ProjectName ProjectName").ToList();

            response.SetData(data);
            return Ok(response);
        }
        /// <summary>	
        /// 新增测点
        /// </summary>	
        /// <param name="model"></param>	
        /// <returns></returns>	
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(Monitor_Points model)
        {
            model.Id = InsertableReturnIdentity(model);
            response.SetData(model.Id);

            if (model.ComputeFormula != null && model.ComputeFormula.Id != 0)
            {
                Updateable(model.ComputeFormula);
            }
            else if (model.ComputeFormula != null && model.ComputeFormula.Id == 0)
            {
                if (!string.IsNullOrEmpty(model.ComputeFormula.FormulaKey))
                {
                    model.ComputeFormulaId = InsertableReturnIdentity(model.ComputeFormula);
                    Updateable(model);
                }
            }
            response.SetSuccess();
            return Ok(response);
        }

        /// <summary>	
        /// 获取单个测点详情
        /// </summary>	
        /// <param name="id">惟一编码</param>	
        /// <returns></returns>	
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(int id)
        {
            var point = Single<Monitor_Points>(s => s.Id == id);
            point.MonitoringKind= db.Queryable<Monitor_Kindinfo>().First(w => point.MonitoringKindId==w.Id)??new Monitor_Kindinfo();
            point.ComputeFormula = db.Queryable<Monitor_Computeformulainfo>().First(w => point.ComputeFormulaId == w.Id)??new Monitor_Computeformulainfo();
            response.SetData(point);
            return Ok(response);
        }

        /// <summary>	
        /// 编辑（测点） 切记：先获取，再修改，不管你改没改其他信息，都需要传回来
        /// </summary>	
        /// <param name="model"></param>	
        /// <returns></returns>	
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(Monitor_Points model)
        {
            if (model.ComputeFormula != null && model.ComputeFormula.Id != 0)
            {
                Updateable(model.ComputeFormula);
            }
            else if(model.ComputeFormula != null && model.ComputeFormula.Id== 0)
            {
                if (!string.IsNullOrEmpty(model.ComputeFormula.FormulaKey))
                {
                    model.ComputeFormulaId=InsertableReturnIdentity(model.ComputeFormula);
                }
            }
            //if (model.MonitoringKind != null && model.MonitoringKind.Id != 0)
            //{
            //    Updateable(model.MonitoringKind);
            //}
            response.SetData(Updateable(model));
            UpdatePointsNumber(model.ProjectId);
            response.SetSuccess();
            return Ok(response);
        }

        /// <summary>	
        /// 删除（测点）	
        /// </summary>	
        /// <param name="ids">角色ID,多个以逗号分隔</param>	
        /// <returns></returns>	
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(string ids)
        {
            List<int> id = ids.Split(",").Select(s => Convert.ToInt32(s)).ToList();
            response.SetData(Deleteable<Monitor_Points>(s => id.Contains(s.Id)));
            response.SetSuccess();
            return Ok(response);
        }

        /// <summary>	
        /// 批量操作（测点）	
        /// </summary>	
        /// <param name="command"></param>	
        /// <param name="ids">ID,多个以逗号分隔</param>	
        /// <returns></returns>	
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Batch(string command, string ids)
        {
            List<int> id = ids.Split(",").Select(s => Convert.ToInt32(s)).ToList();
            switch (command)
            {
                case "enable":
                    response.SetData(Updateable<Monitor_Points>(t => new Monitor_Points { IsActived = 1 }, w => id.Contains(w.Id)));
                    response.SetSuccess();
                    break;
                case "disable":
                    response.SetData(Updateable<Monitor_Points>(t => new Monitor_Points { IsActived = 0 }, w => id.Contains(w.Id)));
                    response.SetSuccess();
                    break;
                default:
                    break;
            }
            return Ok(response);
        }
        /// <summary>
        /// 上传（测点）
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadImg(IFormFileCollection files)
        {
            files = Request.Form.Files;
            if (files.Count == 0)
            {
                response.Message = "没有发现文件";
                response.Code = 403;
            }
            else
            {
                //long size = files.Sum(f => f.Length);
                var fileFolder = Path.Combine(_hostingEnvironment.ContentRootPath + "/wwwroot", "Uploads/Points");

                if (!Directory.Exists(fileFolder))
                    Directory.CreateDirectory(fileFolder);
                var file = files[0];
                if (file.Length > 0)
                {
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") +
                                   Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(fileFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    response.Data = new { url = "Uploads/Points/" + fileName, name = fileName };
                }
            }
            return Json(response);
        } 
        /// <summary>
        /// 上传（测点）文件，先下模板
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadPointsFile(IFormFileCollection files)
        {
            files = Request.Form.Files;
            if (files.Count == 0)
            {
                response.Message = "没有发现文件";
                response.Code = 403;
            }
            else
            {
                //long size = files.Sum(f => f.Length);
                var fileFolder = Path.Combine(_hostingEnvironment.ContentRootPath + "/wwwroot", "Uploads/Points/file");

                if (!Directory.Exists(fileFolder))
                    Directory.CreateDirectory(fileFolder);
                var file = files[0];
                if (file.Length > 0)
                {
                    try
                    {
                        var lastcol = fieds.Length;

                        var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") +
                                       Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(fileFolder, fileName);
                        string fileExt = Path.GetExtension(file.FileName).ToLower();
                        IWorkbook workbook = null;
                        DataTable dt = new DataTable();
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            //先存下来，做记录，没其他用
                            file.CopyTo(stream);
                            stream.Position = 0;
                            //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                            if (fileExt == ".xlsx")
                            {
                                workbook = new XSSFWorkbook(stream);
                            }
                            else if (fileExt == ".xls")
                            {
                                workbook = new HSSFWorkbook(stream);
                            }
                            else
                            {
                                workbook = null;
                            }
                            if (workbook == null)
                            {
                                return null;
                            }
                            ISheet sheet = workbook.GetSheetAt(0);

                            ////表头  
                            //IRow header = sheet.GetRow(sheet.FirstRowNum);
                            //List<int> columns = new List<int>();
                            //for (int i = 0; i < header.LastCellNum; i++)
                            //{
                            //    object obj = GetValueType(header.GetCell(i));
                            //    if (obj == null || obj.ToString() == string.Empty)
                            //    {
                            //        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                            //    }
                            //    else
                            //        dt.Columns.Add(new DataColumn(obj.ToString()));
                            //    columns.Add(i);
                            //}
                            //数据  
                            List<Monitor_Points> points = new List<Monitor_Points>();
                            List<Monitor_Computeformulainfo> parms = new List<Monitor_Computeformulainfo>();
                            for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                            {
                                var point=db.Queryable<Monitor_Points>().Where(w=>w.Id==Convert.ToInt32(GetValueType(sheet.GetRow(i).GetCell(0)))).Single();
                                point.ComputeFormula = db.Queryable<Monitor_Computeformulainfo>().Where(w => w.Id ==point.ComputeFormulaId).Single() ?? new Monitor_Computeformulainfo();

                                for (int j= 1;j < lastcol-2;j++)//-2除去参数
                                {
                                    object dr= GetValueType(sheet.GetRow(i).GetCell(j));
                                    if (dr != null && dr.ToString() != string.Empty)
                                    {
                                        SetModelValue(fieds[j], point, dr);
                                    }
                                }
                                points.Add(point);
                                for (int j = lastcol - 2; j < lastcol; j++)//参数
                                {
                                    object dr = GetValueType(sheet.GetRow(i).GetCell(j));
                                    if (dr != null && dr.ToString() != string.Empty)
                                    {
                                        SetModelValue(fieds[j], point.ComputeFormula, dr);
                                    }
                                }
                                parms.Add(point.ComputeFormula);

                            }
                            //更新，这里是不考虑Monitor_Computeformulainfo是否有对应记录，这里只更新，如果point.ComputeFormulaId==0，务必先编辑一下测点，新增一下{
                            Updateable(points);
                            Updateable(parms);
                        }
                        response.SetData("上传成功");
                    }catch(Exception ex)
                    {
                        response.SetData("解析错误"+ex.Message);
                    }
                    //response.Data = new { url = "Uploads/Points/file/" + fileName, name = fileName };
                }
            }
            return Json(response);
        }
        /// <summary>
        /// 获取单元格类型
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)//cell.CellType
            {
                case CellType.Blank: //BLANK:  
                    return null;
                case CellType.Boolean: //BOOLEAN:  
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:  
                    return cell.NumericCellValue;
                case CellType.String: //STRING:  
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:  
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:  
                    switch (cell.CachedFormulaResultType)//cell.CellType
                    {
                        case CellType.Blank: //BLANK:  
                            return null;
                        case CellType.Boolean: //BOOLEAN:  
                            return cell.BooleanCellValue;
                        case CellType.Numeric: //NUMERIC:  
                            return cell.NumericCellValue;
                        case CellType.String: //STRING:  
                            return cell.StringCellValue;
                        case CellType.Error: //ERROR:  
                            return cell.ErrorCellValue;
                        default:
                            return "=" + cell.CellFormula;
                    }
                default:
                    return "=" + cell.CellFormula;
            }

        }
    }
}