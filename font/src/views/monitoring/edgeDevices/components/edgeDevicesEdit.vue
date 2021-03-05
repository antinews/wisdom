<template>
  <div>
    <el-dialog
      :title="title"
      :visible.sync="dialogFormVisible"
      width="25%"
      @close="close"
    >
      <el-form ref="form" :model="form" :rules="rules" label-width="25%">
        <div>
          <el-form-item label="结构物区域：" prop="cityAreaId">
            <el-select
              v-model="form.cityAreaId"
              style="width: 100%"
              @change="areaChange"
              :disabled="mode == 'look'"
            >
              <el-option
                v-for="item in areas"
                :label="item.areaName"
                :value="item.keyId"
                :key="item.keyId"
              ></el-option>
            </el-select>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="结构物名称：" prop="bridgeName">
            <el-input v-model="form.bridgeName"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="结构物类型：" prop="BridgeTypeId">
            <el-cascader
              v-model="cascader_type"
              :options="bridgeType"
              :props="{ expandTrigger: 'hover' }"
              @change="handleChange"
              style="width: 100%"
            ></el-cascader>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="结构物位置：">
            <el-input v-model="form.gpsDes" style="width: 72%"></el-input>
            <el-button
              type="primary"
              style="margin-left: 10px"
              @click="getLocation"
            >
              获取位置
            </el-button>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="说明：">
            <el-input type="textarea" v-model="form.remark"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="测站图片：">
            <el-upload
              class="upload-demo"
              :action="baseUrl + '/api/Monitor/Devices/UploadImg'"
              :on-preview="handlePreview"
              :on-remove="handleRemove"
              :on-success="uploadSuccess"
              :file-list="fileList"
              list-type="picture"
              :limit="1"
            >
              <el-button size="small" type="primary">点击上传</el-button>
            </el-upload>
          </el-form-item>
        </div>
      </el-form>
      <div
        v-show="mode !== 'look'"
        slot="footer"
        class="dialog-footer rowCenter"
      >
        <el-button @click="close">取 消</el-button>
        <el-button type="primary" @click="save">确 定</el-button>
      </div>
    </el-dialog>
    <el-dialog
      title="选择位置"
      :visible.sync="dialogFormVisible_map"
      width="40%"
      @close="close_map"
    >
      <div style="height: 500px">
        <baidu-map
          :center="map.center"
          :zoom="map.zoom"
          :scroll-wheel-zoom="true"
          @click="getLocationPoint"
          style="height: 500px"
        >
          <bm-view style="width: 100%; height: 500px; flex: 1"></bm-view>
        </baidu-map>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  findStructuryTypes,
  edit,
  create,
  findStructuryAreas,
} from '@/api/monitoring/edgeDevices'
import url from '@/libs/api.request'
export default {
  name: 'edgeDevicesEdit',
  data() {
    return {
      mode: '',
      form: {},
      rules: {
        cityAreaId: [
          { required: true, trigger: 'change', message: '请选择结构物区域' },
        ],
        bridgeName: [
          { required: true, trigger: 'blur', message: '请输入名称' },
        ],
        bridgeTypeId: [
          { required: true, trigger: 'change', message: '请选择结构物类型' },
        ],
      },
      title: '',
      dialogFormVisible: false,
      bridgeType: '',
      baseUrl: '',
      fileList: [],
      dialogFormVisible_map: false,
      map: {
        center: { lng: 114.35679, lat: 30.57901 },
        zoom: 16,
      },
      areas: [],
      cascader_type: [],
    }
  },
  created() {
    this.getStructuryTypes()
    this.baseUrl = url.baseURL
    findStructuryAreas().then((res) => {
      this.areas = res.data
    })
  },
  mounted() {},
  methods: {
    areaChange(value) {
      const area = this.areas.find((x) => x.keyId == value)
      this.form.cityArea = area.areaName
    },
    handleChange(value) {
      this.form.BridgeTypeId = value[1]
      const type = this.bridgeType
        .find((x) => x.value == value[0])
        .children.find((x) => x.value == value[1]).label
      this.form.BridgeType = value[0] + '_' + type
    },
    getLocationPoint(location) {
      // console.log(location)
      var Geo = new BMap.Geocoder()
      let point = new BMap.Point(location.Ag.lng, location.Ag.lat)
      Geo.getLocation(point, (rs) => {
        // console.log(rs)
        this.form.gPSDes = rs.address
        this.dialogFormVisible_map = false
      })
    },
    getLocation() {
      this.dialogFormVisible_map = true
    },
    handlePreview() {},
    handleRemove() {},
    uploadSuccess(res) {
      this.form.frontalViewPic = res.data.url
    },
    async getStructuryTypes() {
      let res = await findStructuryTypes()
      this.bridgeType = res.data
    },
    showEdit(row, mode) {
      this.mode = mode
      if (mode == 'create') this.title = '新增结构物'
      else {
        this.form = Object.assign({}, row)
        if (mode == 'edit') {
          this.fileList = []
          this.title = '编辑结构物'
          if (this.form.frontalViewPic && this.form.frontalViewPic != '') {
            this.fileList = [
              {
                name: 'test',
                url: this.baseUrl + '/' + this.form.frontalViewPic,
              },
            ]
          }
          if (this.form.bridgeType && this.form.bridgeType != '') {
            let temp = this.form.bridgeType.split('_')
            let type = this.bridgeType
              .find((x) => x.value == temp[0])
              .children.find((x) => x.label == temp[1]).value
            this.cascader_type = [temp[0], type]
          }
        }
        if (mode == 'look') {
          this.fileList = []
          this.title = '结构物详情'
          if (this.form.frontalViewPic && this.form.frontalViewPic != '') {
            this.fileList = [
              {
                name: 'test',
                url: this.baseUrl + '/' + this.form.frontalViewPic,
              },
            ]
          }
          if (this.form.bridgeType && this.form.bridgeType != '') {
            let temp = this.form.bridgeType.split('_')
            let type = this.bridgeType
              .find((x) => x.value == temp[0])
              .children.find((x) => x.label == temp[1]).value
            this.cascader_type = [temp[0], type]
          }
        }
      }
      this.dialogFormVisible = true
    },
    close() {
      this.$refs['form'].resetFields()
      this.form = this.$options.data().form
      this.dialogFormVisible = false
    },
    close_map() {},
    save() {
      this.$refs['form'].validate(async (valid) => {
        if (valid) {
          let res
          if (this.mode === 'edit') res = await edit(this.form)
          else if (this.mode === 'create') res = await create(this.form)
          const { message } = res
          this.$baseMessage(message, 'success')
          this.$emit('fetch-data')
          this.close()
        } else {
          return false
        }
      })
    },
  },
}
</script>
<style scoped>
/* 修改 添加样式*/
.Modification {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
}
.Modification > div {
  width: 90%;
}

html body .el-dialog__body .el-form,
html body .el-message-box__body .el-form {
  padding-right: 0px;
}
.demoDrawe {
  position: absolute;
  top: 40%;
  left: 0;
}
.demoDrawe > button {
  width: 35px;
  height: 200px;
  font-size: 30px;
}
.demoDrawe > .el-button--small {
  padding: 9px 10px;
}
.map {
  width: 400px;
  height: 300px;
}
</style>