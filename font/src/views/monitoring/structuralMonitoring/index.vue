<template>
  <div class="devices-container">
    <el-card>
      <div slot="header" class="clearfix">
        <p style="font-size: 16px; font-weight: bold">XX大桥</p>
      </div>
      <div>
        <span style="font-size: 15px">选择传感器类型：</span>
        <span>
          <el-button-group v-if="sensorTypeList.length">
            <el-button
              v-for="item in sensorTypeList"
              :key="item.id"
              :type="currentSensorId === item.id ? 'primary' : 'default'"
              @click="getPointsKind(item.id)"
            >
              {{ item.alias }}
            </el-button>
          </el-button-group>
        </span>
      </div>
      <div style="margin-top: 12px">
        <span style="font-size: 15px">选择测点：</span>
        <span>
          <el-select
            v-model="pid"
            placeholder="请选择"
            @change="handlePointChange"
          >
            <el-option
              v-for="item in monPointList"
              :key="item.id"
              :label="item.pointCode"
              :value="item.id"
            ></el-option>
          </el-select>
        </span>
      </div>
      <div style="margin-top: 12px">
        <el-button-group>
          <el-button type="primary">实时监测</el-button>
          <el-button type="default">历史数据</el-button>
        </el-button-group>
      </div>
    </el-card>
    <el-card style="width: 100%; height: 450px">
      <div slot="header" class="clearfix">
        <p style="font-size: 16px; font-weight: bold">实时监测</p>
      </div>
      <div style="width: 100%; height: 370px">
        <p></p>
        <div style="position: relative">
          <el-button
            style="position: absolute; top: -10px; right: 0px; z-index: 999"
            type="primary"
          >
            阈值
          </el-button>
          <div id="MainChart"></div>
        </div>
      </div>
    </el-card>
  </div>
</template>

<script>
import { formatDate, formatDate2String, string2date } from '@/utils/date.js'
import {
  getKindbyisactivepoint,
  getPointsbykind,
  getLineData,
} from '@/api/structuralMonitoring/structuralMonitoring'
var echarts = require('echarts')
export default {
  name: 'pointEdit',
  data() {
    return {
      polar2: {
        //实时
        grid: {
          top: 33,
          left: 50,
          right: 40,
          bottom: 20,
        },
        tooltip: {
          trigger: 'axis',
          formatter: function (params) {
            params = params[0]
            var date = new Date(params.name)
            return (
              formatDate(date, 'yyyy-MM-dd hh:mm:ss.S') +
              '<br/>' +
              params.marker +
              ' 监测值: ' +
              params.value[1]
            )
          },
          axisPointer: {
            animation: false,
          },
        },
        xAxis: {
          type: 'category',
          splitLine: {
            show: false,
          },
          axisLabel: {
            // interval: 50,
            formatter: function (v) {
              return formatDate2String(v, 'hh:mm') //0表示小数为0位，1表示1位小数，2表示2位小数
            },
          },
          name: '时间(小时:分钟)',
          nameLocation: 'middle',
          nameGap: 40,
          splitNumber: 8,
        },
        yAxis: {
          axisLine: { show: true },
          axisTick: { show: true },
          type: 'value',
          boundaryGap: ['10%', '10%'],
          splitLine: {
            show: true,
          },
          scale: true,
          splitArea: {
            show: true,
          },
          name: '单位',
          // nameLocation: 'middle', //位置居中
          nameGap: 15, //与y轴距离
        },
        series: [
          {
            name: '实时数据',
            type: 'line',
            showSymbol: false,
            hoverAnimation: false,
            data: [],
            itemStyle: {
              normal: {
                color: '#1890FF',
              },
            },
          },
          {
            name: '一级最小值',
            type: 'line',
            showSymbol: false,
            hoverAnimation: false,
            data: [],
            itemStyle: {
              normal: {
                color: '#FF9900',
              },
            },
          },
          {
            name: '一级最大值',
            type: 'line',
            showSymbol: false,
            hoverAnimation: false,
            data: [],
            itemStyle: {
              normal: {
                color: '#FF9900',
              },
            },
          },
          {
            name: '二级最小值',
            type: 'line',
            showSymbol: false,
            hoverAnimation: false,
            data: [],
            itemStyle: {
              normal: {
                color: '#FF0000',
              },
            },
          },
          {
            name: '二级最大值',
            type: 'line',
            showSymbol: false,
            hoverAnimation: false,
            data: [],
            itemStyle: {
              normal: {
                color: '#FF0000',
              },
            },
          },
        ],
      },
      mainChart: '',
      sensorTypeList: [],
      currentSensorId: '',
      currentPoint: { id: -1, edgeMiddleAddr: '' },
      monPointList: [],
      pid: '',
      lastdeviceId: -1,
      client: null,
      isStart: false,
      intervalId: null,
      lastdeviceId: -1,
      sLine: [[], [], [], [], []], //实时+阈值线4条
      setime: ['', ''],
      isShowAlarm: false,
      minValue: 0.0,
      maxValue: 0.0,
    }
  },
  created() {},
  mounted() {
    var that = this
    this.getPointsType()
    this.mainChartSetOption()
    setInterval(function () {
      // var dt = new Date()
      // that.polar2.series[0].data.push({
      //   name: dt,
      //   value: [dt, 1],
      // })
      that.mainChartSetOption()
    }, 1000)
  },
  methods: {
    handlePointChange(e) {
      this.pid = e
      let tempcurrentPoint = this.monPointList.find((el) => el.id === e)
      let one =
        tempcurrentPoint.edgeMiddleAddr != this.currentPoint.edgeMiddleAddr
      let two = tempcurrentPoint.id != this.currentPoint.id
      this.lastdeviceId = this.currentPoint.deviceId
      this.currentPoint = tempcurrentPoint
      this.realtimeline(one, two)
      // this.historyline()
    },
    realtimeline(isRestOpen, isNewPoint) {
      var me = this
      if (!isNewPoint) {
        //还是老测点
        return
      }
      me.isStart = false
      if (isRestOpen) {
        //重新初始化
        if (this.client != null) {
          this.client.unsubscribe(
            `/${this.lastdeviceId}/devicedata/report`,
            function (err) {
              if (!err) {
                console.log('退订成功')
              }
            }
          )
          this.client.end()
        }
        this.client = mqtt.connect(
          `ws://${this.currentPoint.edgeMiddleAddr.split(':')[0]}:8083/mqtt`,
          {
            //this.client = mqtt.connect(`ws://106.53.77.195:8083/mqtt`, {
            connectTimeout: 4000, // 超时时间
            // 认证信息 按自己需求填写
            clientId: 'clientId123456',
            username: 'admin',
            password: 'public',
          }
        )
        console.log(this.client)
        let topic = `/${this.currentPoint.deviceId}/devicedata/report`
        //let topic = `/727/devicedata/report`;
        //监听mq的返回
        this.client.on('message', function (topic, message, packet) {
          var list = packet.topic.split('/')
          console.log(list)
          console.log(packet)
          console.log(me.currentPoint.deviceId)
          if (
            list.length > 1 &&
            list[1] == me.currentPoint.deviceId.toString()
          ) {
            console.log('yes')
            me.drawRealtime(JSON.parse(JSON.parse(packet.payload.toString())))
          }
        })
        // this.client.on('reconnect', (error) => {
        //     console.log('正在重连:', error)
        //      //订阅一个主题
        //      this.client.subscribe(topic, { qos: 0 }, function (err) {
        //         if (!err) {
        //             console.log("订阅成功")
        //         }

        //     })
        // })
        this.client.on('error', (error) => {
          console.log('连接失败:', error)
        })
        this.client.on('connect', (e) => {
          // console.log('成功连接服务器')
          //订阅一个主题
          this.client.subscribe(topic, { qos: 0 }, function (err) {
            if (err) {
              console.log('订阅失败')
            }
          })
        })
      } else if (this.lastdeviceId != this.currentPoint.deviceId) {
        if (this.client != null) {
          this.client.unsubscribe(
            `/${this.lastdeviceId}/devicedata/report`,
            function (err) {
              if (!err) {
                console.log('退订成功')
              }
            }
          )
          //订阅一个主题
          this.client.subscribe(
            `/${this.currentPoint.deviceId}/devicedata/report`,
            { qos: 0 },
            function (err) {
              if (err) {
                console.log('订阅失败')
              }
            }
          )
        }
      }
      // me.polar.yAxis.name = '单位：' + me.currentPoint.unit
      me.polar2.yAxis.name = '单位：' + me.currentPoint.unit

      setTimeout(() => {
        me.sLine[0] = []
        me.sLine[1] = []
        me.sLine[2] = []
        me.sLine[3] = []
        me.sLine[4] = []
        me.isStart = true
      }, 1000)
    },
    drawRealtime(datas) {
      var me = this
      if (this.isStart) {
        for (let z = 0; z < datas.length; z++) {
          let data = datas[z]
          var MsgTime = new Date(data.msgtime)
          var gettime = MsgTime.getTime()
          let col = 'col' + this.currentPoint.chanel
          var value = data[col]
          if (me.sLine[0].length == 0) {
            for (var i = 650; i > 0; i--) {
              var tt = gettime - i * this.currentPoint.collectInterval //new Date(gettime - i * this.currentPoint.collectInterval);
              me.sLine[0].push({
                name: tt,
                value: [tt, value],
              })
              me.sLine[1].push({
                name: tt,
                value: [tt, this.currentPoint.range1Min],
              })
              me.sLine[2].push({
                name: tt,
                value: [tt, this.currentPoint.range1Max],
              })
              me.sLine[3].push({
                name: tt,
                value: [tt, this.currentPoint.range2Min],
              })
              me.sLine[4].push({
                name: tt,
                value: [tt, this.currentPoint.range2Max],
              })
            }
          }
          me.sLine[0].shift()
          me.sLine[1].shift()
          me.sLine[2].shift()
          me.sLine[3].shift()
          me.sLine[4].shift()
          me.sLine[0].push({
            name: gettime,
            value: [gettime, value],
          })
          me.sLine[1].push({
            name: gettime,
            value: [gettime, this.currentPoint.range1Min],
          })
          me.sLine[2].push({
            name: gettime,
            value: [gettime, this.currentPoint.range1Max],
          })
          me.sLine[3].push({
            name: gettime,
            value: [gettime, this.currentPoint.range2Min],
          })
          me.sLine[4].push({
            name: gettime,
            value: [gettime, this.currentPoint.range2Max],
          })
        }
      }
    },
    getPointsKind(id) {
      this.currentSensorId = id
      getPointsbykind(id).then((res) => {
        if (res.code == 200) {
          this.monPointList = res.data
          if (this.monPointList.length > 0) {
            this.handlePointChange(this.monPointList[0].id)
          }
        } else {
          // this.$Message.info(res.message)
        }
      })
    },
    getPointsType() {
      getKindbyisactivepoint(112).then((res) => {
        if (res.code == 200) {
          this.sensorTypeList = res.data
          // console.log(this.sensorTypeList)
          if (this.sensorTypeList.length > 0) {
            this.getPointsKind(this.sensorTypeList[0].id)
          }
        } else {
          // this.$Message.info(res.message)
        }
      })
    },
    mainChartSetOption() {
      if (this.mainChart || this.mainChart == '') {
        this.mainChart = echarts.init(document.getElementById('MainChart'))
      }
      this.mainChart.setOption(this.polar2)
    },
  },
}
</script>

<style>
#MainChart {
  width: 100%;
  height: 330px;
}
</style>