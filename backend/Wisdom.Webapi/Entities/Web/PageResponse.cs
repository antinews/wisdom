using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wisdom.Webapi.Models.Response;

namespace Edge.WebApi.Entity.Web
{
    public class PageResponse:ResponseModel
    {  
        /// <summary>
       /// 
       /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="totalCount"></param>
        public void SetData(object data, int totalCount = 0)
        {
            Data = data;
            TotalCount = totalCount;
        }
    }
}
