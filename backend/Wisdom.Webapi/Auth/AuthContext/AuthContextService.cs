using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Wisdom.Webapi.Entities.Enum.CommonEnum;

namespace Wisdom.Webapi.Auth.AuthContext
{
    public class AuthContextService
    {
        private static IHttpContextAccessor _context;
        /// <summary>
        /// 以依赖注入的方式，配置http上下文
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor;
        }
        /// <summary>
        /// http上下文
        /// </summary>
        public static HttpContext Current => _context.HttpContext;
        /// <summary>
        /// 当前用户
        /// </summary>
        public static AuthContextUser CurrentUser
        {
            get
            {
                if (Current.User.FindFirstValue("id") is null) return null;
                var user = new AuthContextUser
                {
                    UserName = Current.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    TrueName = Current.User.FindFirstValue("trueName"),
                    Email = Current.User.FindFirstValue("email"),
                    Id = Convert.ToInt32(Current.User.FindFirstValue("id")),
                    RoleId = Convert.ToInt32(Current.User.FindFirstValue("roleId")),
                };
                return user;
            }
        }

        /// <summary>
        /// 是否已授权
        /// </summary>
        public static bool IsAuthenticated
        {
            get
            {
                return Current.User.Identity.IsAuthenticated;
            }
        }

        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public static bool IsSupperAdministator
        {
            get
            {
                return Convert.ToInt32(Current.User.FindFirstValue("roleId")) == (int)UserType.SuperAdministrator;
            }
        }
    }
}
