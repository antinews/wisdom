using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///用户表
    ///</summary>
    [SugarTable("sys_users")]
    public partial class sys_users
    {
           public sys_users(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:用户名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string UserName {get;set;}

           /// <summary>
           /// Desc:密码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Password {get;set;}

           /// <summary>
           /// Desc:加密码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PassSalt {get;set;}

           /// <summary>
           /// Desc:邮箱
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Email {get;set;}

           /// <summary>
           /// Desc:是否为超级管理员
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int IsAdmin {get;set;}

           /// <summary>
           /// Desc:是否禁用
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int IsDisabled {get;set;}

           /// <summary>
           /// Desc:真名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TrueName {get;set;}

           /// <summary>
           /// Desc:部门ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? DepartmentId {get;set;}

           /// <summary>
           /// Desc:手机
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Mobile {get;set;}

           /// <summary>
           /// Desc:QQ号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QQ {get;set;}

           /// <summary>
           /// Desc:描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remark {get;set;}

           /// <summary>
           /// Desc:菜单配置
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MenusJson {get;set;}

           /// <summary>
           /// Desc:配置信息
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ConfigJson {get;set;}

           /// <summary>
           /// Desc:MAC地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MAC {get;set;}

           /// <summary>
           /// Desc:IMEI手机号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string IMEI {get;set;}

           /// <summary>
           /// Desc:秘钥Token
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Token {get;set;}

           /// <summary>
           /// Desc:是否负责人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? IsLeader {get;set;}

           /// <summary>
           /// Desc:监测APP是否授权,0表示未授权,1表示已授权
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? IsAuthorize {get;set;}

           /// <summary>
           /// Desc:监测APP授权时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? AuthorizeTime {get;set;}

           /// <summary>
           /// Desc:该用户可访问的健康监测系统,桥梁Id以逗号隔开
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AccessBridge {get;set;}

           /// <summary>
           /// Desc:该用户健康监测系统默认桥梁,对应Base_Bridge中的KeyId
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? NowBridge {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CreatedTime {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WxOpenId {get;set;}

           /// <summary>
           /// Desc:所属角色ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int RoleId {get;set;}/// <summary>
                                       /// Desc:
                                       /// Default:
                                       /// Nullable:False
                                       /// </summary>           
        public int CreatedUserId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? ModifyUserId { get; set; }
        public int Sex { get; set; }
    }
}
