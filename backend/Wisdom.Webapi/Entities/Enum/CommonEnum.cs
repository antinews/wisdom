using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Entities.Enum
{
    /// <summary>
    /// 通用枚举类
    /// </summary>
    public static class CommonEnum
    {
        public enum PermissionType
        {
            Button =0, Page=1
        }

        public enum IsDisabled // 是否禁用
        {
            YES = 1, NO = 0, ALL = -1
        }
        public enum IsVisible
        {
            True = 1, Flase = 0
        }
        /// <summary>
        /// 用户类型
        /// </summary>
        public enum UserType
        {  /// <summary>
           /// 超级管理员
           /// </summary>
            SuperAdministrator = 1,
            /// <summary>
            /// 管理员
            /// </summary>
            Admin = 2,
            Other = 3
        }
    }
}
