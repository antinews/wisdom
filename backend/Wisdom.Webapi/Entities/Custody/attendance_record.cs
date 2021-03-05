using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("attendance_record")]
    public partial class attendance_record
    {
           public attendance_record(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int KeyId {get;set;}

           /// <summary>
           /// Desc:出勤记录标识（用户名@时间）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AttendanceRecordId {get;set;}

           /// <summary>
           /// Desc:用户ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? UserId {get;set;}

           /// <summary>
           /// Desc:出勤车辆牌照
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Cars {get;set;}

           /// <summary>
           /// Desc:开始时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? StartTime {get;set;}

           /// <summary>
           /// Desc:结束时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? EndTime {get;set;}

           /// <summary>
           /// Desc:评论
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remark {get;set;}

           /// <summary>
           /// Desc:出勤任务ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? MissionId {get;set;}

           /// <summary>
           /// Desc:任务合格率
           /// Default:
           /// Nullable:True
           /// </summary>           
           public float? QualifiedRate {get;set;}

           /// <summary>
           /// Desc:平均速度
           /// Default:
           /// Nullable:True
           /// </summary>           
           public float? Speed {get;set;}

           /// <summary>
           /// Desc:距离
           /// Default:
           /// Nullable:True
           /// </summary>           
           public float? Distance {get;set;}

           /// <summary>
           /// Desc:出勤时间（分钟）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? TotalTime {get;set;}

    }
}
