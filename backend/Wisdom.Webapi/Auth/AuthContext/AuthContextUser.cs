using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Auth.AuthContext
{
    public class AuthContextUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 显示名
        /// </summary>
        public string TrueName { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }
    }
}
