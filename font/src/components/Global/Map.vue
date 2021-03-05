<template>
  <el-dialog :visible="dialogVisible" :title="title" @close="close" width="44%">
    <div id="r-result" v-if="enableSearch">
      <el-input
        id="suggestId"
        placeholder="地名模糊查询"
        v-model="address"
        class="mr-1"
        ref="suggest"
      >
        <el-button slot="append" icon="el-icon-search"></el-button>
      </el-input>
    </div>
    <div
      id="searchResultPanel"
      style="
        border: 1px solid #c0c0c0;
        width: 150px;
        height: auto;
        display: none;
      "
    ></div>
    <div id="l-_map"></div>

    <div style="width: 100%; margin-top: 10px" class="rowCenter">
      <el-button type="primary" @click="save">确定</el-button>
      <el-button type="primary" plain @click="close">取消</el-button>
      <!-- <el-button @click="resetForm">清空</el-button> -->
    </div>
  </el-dialog>
</template>
<script>
import BMap from 'BMap'
let _self
let _map
export default {
  name: 'bt-map',
  props: {
    title: {
      // 对话框标题
      type: String,
      default: '选择地址',
    },
    center: {
      // 初始地图中心坐标
      type: Object,
      default: function () {
        return {
          lng: 116.404,
          lat: 39.915,
        }
      },
    },
    zoom: {
      // 初始地图缩放等级
      type: Number,
      default: 11,
    },
    city: {
      // 初始地图城市名称
      type: String,
      default: '北京',
    },
    enableScroll: {
      // 开启滚动
      type: Boolean,
      default: true,
    },
    enableSearch: {
      // 开启搜索功能
      type: Boolean,
      default: true,
    },
    enableGPS: {
      // 开启GPS定位
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      isHide: true,
      addressList: [],
      address: '',
      lng: '',
      lat: '',
      dialogVisible: false,
    }
  },
  created() {
    _self = this
  },
  mounted() {},
  methods: {
    save() {
      let data = {
        lat: this.lat,
        lng: this.lng,
        address: this.address,
      }
      this.$emit('location', data)
      this.close()
    },
    show() {
      this.dialogVisible = true
      this.$nextTick(() => {
        if (!_map) {
          _self.loadMap()
        }
        if (_self.enableGPS) {
          var geolocation = new BMap.Geolocation()
          geolocation.getCurrentPosition(
            function (r) {
              console.log(r)
              if (this.getStatus() == BMAP_STATUS_SUCCESS) {
                var mk = new BMap.Marker(r.point)
                _map.addOverlay(mk)
                _map.panTo(r.point)
                alert('您的位置：' + r.point.lng + ',' + r.point.lat)
              } else {
                alert('failed' + this.getStatus())
              }
            },
            { enableHighAccuracy: true }
          )
        }
      })
    },
    close() {
      this.dialogVisible = false
      this.$refs['suggest'].focus()
    },
    loadMap() {
      _map = new BMap.Map('l-_map')
      // 初始化地图,设置中心点坐标和地图级别
      const centerPoint = new BMap.Point(_self.center.lng, _self.center.lat)
      _map.centerAndZoom(centerPoint, _self.zoom)
      //添加地图类型控件
      _map.addControl(
        new BMap.MapTypeControl({
          mapTypes: [BMAP_NORMAL_MAP, BMAP_HYBRID_MAP],
        })
      )
      // 设置地图显示的城市 此项是必须设置的
      _map.setCurrentCity(_self.city)
      //开启鼠标滚轮缩放
      _map.enableScrollWheelZoom(_self.enableScroll)

      var geocoder = new BMap.Geocoder()
      _map.addEventListener('click', function (e) {
        _self.lng = e.point.lng
        _self.lat = e.point.lat
        var point = new BMap.Point(e.point.lng, e.point.lat)
        _map.clearOverlays()
        _map.addOverlay(new BMap.Marker(point)) //添加标注
        geocoder.getLocation(point, function (geocoderResult, LocationOptions) {
          //测试输出坐标（指的是输入框最后确定地点的经纬度）
          // console.log(geocoderResult)
          _self.address = geocoderResult.address
        })
        self.isHide = false
      })

      if (_self.enableSearch) {
        this.loadSeach()
      }
    },
    loadSeach() {
      function G(id) {
        return document.getElementById(id)
      }
      var ac = new BMap.Autocomplete({
        input: 'suggestId',
        location: _map,
        onSearchComplete: function (data) {
          if (!isHide) {
            ac.hide()
            isHide = true
          }
        },
      }) //建立一个自动完成的对象
      var myValue
      ac.addEventListener('onconfirm', function (e) {
        //鼠标点击下拉列表后的事件
        var _value = e.item.value
        myValue =
          _value.province +
          _value.city +
          _value.district +
          _value.street +
          _value.business
        G('searchResultPanel').innerHTML =
          'onconfirm<br />index = ' +
          e.item.index +
          '<br />myValue = ' +
          myValue

        _self.address = myValue

        setPlace()
      })

      function setPlace() {
        _map.clearOverlays() //清除地图上所有覆盖物
        function myFun() {
          var pp = local.getResults().getPoi(0).point //获取第一个智能搜索的结果
          _self.lat = pp.lat
          _self.lng = pp.lng
          _map.centerAndZoom(pp, 18)
          _map.addOverlay(new BMap.Marker(pp)) //添加标注
        }
        var local = new BMap.LocalSearch(_map, {
          //智能搜索
          onSearchComplete: myFun,
        })
        local.search(myValue)
      }
    },
  },
}
</script>
<style scoped>
.el-dialog__header {
  height: 0 !important;
}
.el-dialog__body {
  padding-top: 0 !important;
}
#l-_map {
  height: 400px;
  width: 100%;
}
#r-result {
  width: 100%;
  margin: -10px 0 2px 0;
}
.tangram-suggestion-main {
  z-index: 999999;
}
</style>
