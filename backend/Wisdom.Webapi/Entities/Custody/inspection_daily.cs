using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Entities.Maintenance
{
    [SugarTable("inspection_daily")]
    public class inspection_daily
    {

        #region Model
        private int _keyid;
        private int? _userid = 0;
        private string _userpositiondescription = "0";
        private decimal? _xlongitude = 0M;
        private decimal? _ylatitude = 0M;
        private DateTime? _recordtime = Convert.ToDateTime("2014-05-23 09:51:49");
        private string _recorddescription = "0";
        private int? _cityareaid = 0;
        private int? _bridgeid = 0;
        private int? _optionid = 0;
        private string _itemdescription = "0";
        private int? _itemid = 0;
        private string _unit1 = "0";
        private string _unit2 = "0";
        private string _unit3 = "0";
        private string _pic = "0";
        private string _video = "0";
        private string _sound = "0";
        private DateTime? _createdtime;
        private int? _statusid = 0;
        private string _weather = "0";
        private string _tempreture = "0";
        private string _mileage = "0";
        private string _maintainremark = "0";
        private string _missiontype = "0";
        private string _missionlevel = "0";
        private string _x = "0";
        private string _y = "0";
        private string _z = "0";
        private decimal? _azimuth;
        private decimal? _pitch;
        private decimal? _roll;
        private int? _historydefectid = 0;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
        public int KeyId
        {
            set { _keyid = value; }
            get { return _keyid; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int? UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 用户位置
        /// </summary>
        public string UserPositionDescription
        {
            set { _userpositiondescription = value; }
            get { return _userpositiondescription; }
        }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal? XLongitude
        {
            set { _xlongitude = value; }
            get { return _xlongitude; }
        }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal? YLatitude
        {
            set { _ylatitude = value; }
            get { return _ylatitude; }
        }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? RecordTime
        {
            set { _recordtime = value; }
            get { return _recordtime; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string RecordDescription
        {
            set { _recorddescription = value; }
            get { return _recorddescription; }
        }
        /// <summary>
        /// 区域ID
        /// </summary>
        public int? CityAreaId
        {
            set { _cityareaid = value; }
            get { return _cityareaid; }
        }
        /// <summary>
        /// 桥梁ID
        /// </summary>
        public int? BridgeId
        {
            set { _bridgeid = value; }
            get { return _bridgeid; }
        }
        /// <summary>
        /// 巡检项ID
        /// </summary>
        public int? OptionId
        {
            set { _optionid = value; }
            get { return _optionid; }
        }
        /// <summary>
        /// 病害项描述
        /// </summary>
        public string ItemDescription
        {
            set { _itemdescription = value; }
            get { return _itemdescription; }
        }
        /// <summary>
        /// 病害项ID
        /// </summary>
        public int? ItemId
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// 单位1
        /// </summary>
        public string Unit1
        {
            set { _unit1 = value; }
            get { return _unit1; }
        }
        /// <summary>
        /// 单位2
        /// </summary>
        public string Unit2
        {
            set { _unit2 = value; }
            get { return _unit2; }
        }
        /// <summary>
        /// 单位3
        /// </summary>
        public string Unit3
        {
            set { _unit3 = value; }
            get { return _unit3; }
        }
        /// <summary>
        /// 照片
        /// </summary>
        public string Pic
        {
            set { _pic = value; }
            get { return _pic; }
        }
        /// <summary>
        /// 视频
        /// </summary>
        public string Video
        {
            set { _video = value; }
            get { return _video; }
        }
        /// <summary>
        /// 录音
        /// </summary>
        public string Sound
        {
            set { _sound = value; }
            get { return _sound; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedTime
        {
            set { _createdtime = value; }
            get { return _createdtime; }
        }
        /// <summary>
        /// 养护流程状态ID
        /// </summary>
        public int? StatusId
        {
            set { _statusid = value; }
            get { return _statusid; }
        }
        /// <summary>
        /// 天气
        /// </summary>
        public string Weather
        {
            set { _weather = value; }
            get { return _weather; }
        }
        /// <summary>
        /// 温度
        /// </summary>
        public string Tempreture
        {
            set { _tempreture = value; }
            get { return _tempreture; }
        }
        /// <summary>
        /// 里程
        /// </summary>
        public string Mileage
        {
            set { _mileage = value; }
            get { return _mileage; }
        }
        /// <summary>
        /// 养护描述
        /// </summary>
        public string MaintainRemark
        {
            set { _maintainremark = value; }
            get { return _maintainremark; }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string MissionType
        {
            set { _missiontype = value; }
            get { return _missiontype; }
        }
        /// <summary>
        /// 等级
        /// </summary>
        public string MissionLevel
        {
            set { _missionlevel = value; }
            get { return _missionlevel; }
        }
        /// <summary>
        /// 坐标X
        /// </summary>
        public string X
        {
            set { _x = value; }
            get { return _x; }
        }
        /// <summary>
        /// 坐标Y
        /// </summary>
        public string Y
        {
            set { _y = value; }
            get { return _y; }
        }
        /// <summary>
        /// 坐标Z
        /// </summary>
        public string Z
        {
            set { _z = value; }
            get { return _z; }
        }
        /// <summary>
        /// 方向角，0表示正北，90表示正东，180/-180表示正南，-90表示正西。
        /// </summary>
        public decimal? Azimuth
        {
            set { _azimuth = value; }
            get { return _azimuth; }
        }
        /// <summary>
        /// 倾斜角，0表示手机平放，90/-90表示手机立起
        /// </summary>
        public decimal? Pitch
        {
            set { _pitch = value; }
            get { return _pitch; }
        }
        /// <summary>
        /// 旋转角，0表示手机平放，90/-90表示手机侧面立起，180/-180表示手机反面平放
        /// </summary>
        public decimal? Roll
        {
            set { _roll = value; }
            get { return _roll; }
        }
        /// <summary>
        /// 历史病害ID
        /// </summary>
        public int? HistoryDefectId
        {
            set { _historydefectid = value; }
            get { return _historydefectid; }
        }
        #endregion Model

    }
}
