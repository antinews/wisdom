<template>
  <el-dialog :title="title" :visible.sync="visible" width="55%" @close="close">
    <el-row>
      <el-col :span="8">
        <div
          v-if="attendanceList.length"
          class="grid-content bg-purple list-scroll"
        >
          <div
            v-for="(item, idx) in attendanceList"
            :key="idx"
            :class="{
              'link-item-active': idx == activeLinkId,
              'link-item-hover': idx == hoverIndex,
            }"
            style="
              padding: 10px;
              border-bottom: rgba(13, 28, 44, 0.1) 1px solid;
            "
            @click="clickAttendance(idx, item.missionId)"
            @mouseover="hoverIndex = idx"
            @mouseout="hoverIndex = -1"
          >
            <div style="font-size: 15px">
              {{ item.startTime | timeFilter }} -
              {{ item.endTime | timeFilter }}
            </div>
            <div class="rowStartCenter">
              <el-col>
                <div>用时：{{ item.totalTime }}分钟</div>
              </el-col>
              <el-col>
                <div>距离：{{ item.distance }}公里</div>
              </el-col>
            </div>
          </div>
        </div>
      </el-col>
      <el-col :span="16">
        <div
          class="grid-content bg-purple"
          style="min-height: 400px; width: 100%"
          v-if="ready"
        >
          <baidu-map
            :center="map.center"
            :zoom="map.zoom"
            :scroll-wheel-zoom="true"
          >
            <bm-view style="width: 100%; height: 400px; flex: 1"></bm-view>
            <bm-polyline
              :path="map.points"
              stroke-color="red"
              :stroke-opacity="0.5"
              :stroke-weight="5"
            ></bm-polyline>
            <!-- <bm-marker :position="points" :dragging="true" /> -->
          </baidu-map>
        </div>
      </el-col>
    </el-row>
    <div slot="footer" class="dialog-footer rowCenter">
      <el-button @click="close">取 消</el-button>
    </div>
  </el-dialog>
</template>
<script>
import { getAttendanceList, getTraceData } from '_api/custody/attendance/record'
let self
export default {
  data() {
    return {
      ready: false,
      hoverIndex: -1, // hover索引
      activeLinkId: 0,
      visible: false,
      map: {},
      title: '',
      attendanceList: [],
    }
  },
  created() {
    self = this
  },
  methods: {
    //根据经纬极值计算绽放级别。
    getZoom(maxLng, minLng, maxLat, minLat) {
      var zoom = [
        '50',
        '100',
        '200',
        '500',
        '1000',
        '2000',
        '5000',
        '10000',
        '20000',
        '25000',
        '50000',
        '100000',
        '200000',
        '500000',
        '1000000',
        '2000000',
      ] //级别18到3。
      var pointA = new BMap.Point(maxLng, maxLat) // 创建点坐标A
      var pointB = new BMap.Point(minLng, minLat) // 创建点坐标B
      var distance = new BMap.Map().getDistance(pointA, pointB).toFixed(1) //获取两点距离,保留小数点后两位
      for (var i = 0, zoomLen = zoom.length; i < zoomLen; i++) {
        if (zoom[i] - distance > 0) {
          return 18 - i + 3 //之所以会多3，是因为地图范围常常是比例尺距离的10倍以上。所以级别会增加3。
        }
      }
    },
    async clickAttendance(idx, id) {
      this.ready = false
      this.activeLinkId = idx
      const { data } = await getTraceData(id)
      this.map.points = data
      this.loadMap(data)
    },
    loadMap(points) {
      var x = 0
      var y = 0
      var len = points.length
      if (len < 1) return
      var maxLng = points[0].lng
      var minLng = points[0].lng
      var maxLat = points[0].lat

      var minLat = points[0].lat
      for (let i = 0; i < len; i++) {
        const el = points[i]
        x += el.lng
        y += el.lat
        if (el.lng > maxLng) maxLng = el.lng
        if (el.lng < minLng) minLng = el.lng
        if (el.lat > maxLat) maxLat = el.lat
        if (el.lat < minLat) minLat = el.lat
      }
      this.map.center = { lng: x / len, lat: y / len }
      this.map.zoom = this.getZoom(maxLng, minLng, maxLat, minLat)
      this.ready = true
    },
    async loadAttendanceList(row) {
      // let { userName, startTime } = row
      // let param = userName + '@' + formatDate2String(startTime, 'yyyy-MM-dd')
      let str = row.attendanceRecordId
      let param = str.substr(0, str.indexOf('@') + 10 + 1)
      this.title = `出勤轨迹（${param}）`
      const { data } = await getAttendanceList(param)
      this.attendanceList = data
    },
    async init(row) {
      await this.loadAttendanceList(row)
      this.clickAttendance(0, this.attendanceList[0].missionId)
    },
    drawTrace() {},
    close() {
      this.visible = false
      this.ready = false
    },
    show(row) {
      this.init(row)
      this.visible = true
    },
  },
}
</script>
<style scoped>
.link-item-hover {
  background-color: rgba(13, 28, 44, 0.1);
}
.link-item-active {
  color: rgb(153, 152, 197);
  font-weight: bold;
  background-color: rgba(174, 194, 243, 0.5);
}
/* 滚动条 */
.list-scroll {
  height: 400px;
  overflow-y: hidden;
  overflow-y: scroll;
}
/* .list-scroll:hover {
  overflow-y: scroll;
} */
/*滚动条整体样式*/
.list-scroll::-webkit-scrollbar {
  width: 5px; /*竖向滚动条的宽度*/
  height: 5px; /*横向滚动条的高度*/
}
.list-scroll::-webkit-scrollbar-thumb {
  /*滚动条里面的小方块*/
  background: rgb(131, 168, 255);
  border-radius: 5px;
}
.list-scroll::-webkit-scrollbar-track {
  /*滚动条轨道的样式*/
  background: #ccc;
  border-radius: 5px;
}
</style>