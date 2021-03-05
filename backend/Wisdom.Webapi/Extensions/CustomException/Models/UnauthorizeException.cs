using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Extensions.CustomException.Models
{
    /// <summary>
    /// 未授权异常
    /// </summary>
    public class UnauthorizeException:Exception
    {
        public UnauthorizeException()
        {

        }
    }
}
