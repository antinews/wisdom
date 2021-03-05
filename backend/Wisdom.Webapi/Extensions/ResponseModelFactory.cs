using Wisdom.Webapi.Models.Response;

namespace Wisdom.Webapi.Extensions
{
    public class ResponseModelFactory
    {
        /// <summary>
        /// 
        /// </summary>
        public static ResponseModel CreateInstance => new ResponseModel();
        /// <summary>
        /// 
        /// </summary>
        public static ResponseResultModel CreateResultInstance => new ResponseResultModel();
    }
}
