using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("sys_dics")]
    public partial class sys_dics
    {
           public sys_dics(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Title {get;set;}

           /// <summary>
           /// Desc:字典编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Code {get;set;}

           /// <summary>
           /// Desc:序号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? SortNum {get;set;}

           /// <summary>
           /// Desc:父节点
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ParentId {get;set;}

           /// <summary>
           /// Desc:字典类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? CategoryId {get;set;}

           /// <summary>
           /// Desc:字典描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Reamrk {get;set;}

           /// <summary>
           /// Desc:状态1 启用 0停用
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Status {get;set;}

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
           public int? CreatedUserId {get;set;}

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
           public int? ModifyUserId {get;set;}

    }
}
