using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("sys_buttons")]
    public partial class sys_buttons
    {
           public sys_buttons(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:按钮文字
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ButtonText {get;set;}

           /// <summary>
           /// Desc:序号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int SortNum {get;set;}

           /// <summary>
           /// Desc:图标简称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string IconCls {get;set;}

           /// <summary>
           /// Desc:图标路径
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string IconUrl {get;set;}

           /// <summary>
           /// Desc:按钮提示
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ButtonTag {get;set;}

           /// <summary>
           /// Desc:按钮描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remark {get;set;}

           /// <summary>
           /// Desc:按钮页面样式
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ButtonHtml {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CreatedTime {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string CreatedUserId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? ModifyTime {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ModifyUserId {get;set;}

    }
}
