using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Wisdom.Webapi.Auth.AuthContext;
using Wisdom.Webapi.Configurations;
using Wisdom.Webapi.Extensions;
using Wisdom.Webapi.Extensions.CustomException;
using Wisdom.Webapi.Models.Response;

namespace Wisdom.Webapi.Controllers
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class BaseController : Controller
    {
        protected ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(GlobalExceptionFilter));
        protected SqlSugarClient db = Configurations.DbContext.GetDbContextInstance();
        static volatile IMapper mapper;
        static readonly object _locker = new object();
        protected AuthContextUser CurrentUser = AuthContextService.CurrentUser;
        protected ResponseResultModel response = ResponseModelFactory.CreateResultInstance; // 存在线程安全问题
        protected IMapper Mapper
        {
            get
            {
                if (mapper is null)
                {
                    lock (_locker)
                    {
                        if (mapper is null)
                        {
                            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
                            mapper = configuration.CreateMapper();
                        }
                    }
                }
                return mapper;
            }
        }
        //[NonAction]
        //protected void Debug(string message)
        //{
        //    log.Debug(this.GetType().Name + "控制器的" + new System.Diagnostics.StackTrace(true).GetFrame(1).GetMethod().Name + "方法:" + message);
        //}
    }
}
