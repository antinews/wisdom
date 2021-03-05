<template>
  <el-dialog
    :title="title"
    :visible.sync="dialogFormVisible"
    width="30%"
    @close="close"
  >
    <el-form ref="form" :model="form" :rules="rules" label-width="25%">
      <div>
        <el-form-item label="区域：" prop="deviceName">
          <el-input v-model="form.deviceName"></el-input>
        </el-form-item>
      </div>
      <div>
        <el-form-item label="测站名称：" prop="deviceName">
          <el-input v-model="form.deviceName"></el-input>
        </el-form-item>
      </div>
      <div>
        <el-form-item label="网络通信方式：" prop="netProtocol">
          <el-select v-model="form.netProtocol" style="width: 100%">
            <el-option
              v-for="item in protocolList"
              :label="item.value"
              :value="item.key"
              :key="item.key"
            ></el-option>
          </el-select>
        </el-form-item>
      </div>
      <div style="height: 50px">
        <el-col :span="14">
          <el-form-item label="上报地址：" prop="reportAddr" label-width="43%">
            <el-input v-model="form.reportAddr"></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="1" class="TextCenter">:</el-col>
        <el-col :span="4">
          <el-form-item prop="reportPort" label-width="0%">
            <el-input v-model="form.reportPort"></el-input>
          </el-form-item>
        </el-col>
      </div>
      <div>
        <el-form-item label="解算方式：" prop="codecsId">
          <el-select v-model="form.codecsId" @change="changecodes">
            <el-option
              v-for="item in codecsList"
              :label="item.codecsName"
              :value="item.codecsId"
              :key="item.codecsId"
            ></el-option>
          </el-select>
        </el-form-item>
      </div>
      <div>
        <el-form-item label="通道数量：" prop="pointNums">
          <el-input-number
            v-model="form.pointNums"
            controls-position="right"
            :min="0"
            :max="99"
          ></el-input-number>
        </el-form-item>
      </div>
      <div>
        <el-form-item label="采样间隔：" prop="collectInterval">
          <el-input-number
            v-model="form.collectInterval"
            controls-position="right"
            :min="1"
            :max="99999"
            style="width: 78%"
          ></el-input-number>
          毫秒
        </el-form-item>
      </div>
      <!-- <div>
        <el-form-item label="数据模式：" prop="uploadMethod" label-width="50%">
          <el-select v-model="form.uploadMethod">
            <el-option
              v-for="item in uploadMethodList"
              :label="item.value"
              :value="item.key"
              :key="item.key"
            ></el-option>
          </el-select>
        </el-form-item>
      </div> -->
      <div style="width: 100%">
        <el-form-item label="是否启用：" prop="isActived">
          <el-switch
            v-model="form.isActived"
            active-color="#13ce66"
            inactive-color="#ebeef5"
            :active-value="1"
            :inactive-value="0"
          ></el-switch>
        </el-form-item>
      </div>
      <div style="width: 100%">
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
      <!-- <div style="width: 100%">
        <el-col :span="14">
          <el-form-item label="测站地址：" prop="deviceAddr" label-width="43%">
            <el-input v-model="form.deviceAddr"></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="1" class="TextCenter">:</el-col>
        <el-col :span="4">
          <el-form-item prop="devicePort" label-width="0%">
            <el-input v-model="form.devicePort"></el-input>
          </el-form-item>
        </el-col>
      </div> -->
      <!-- <div style="width: 100%">
        <el-col :span="14">
          <el-form-item
            label="中间件地址："
            prop="edgeMiddleAddr"
            label-width="29%"
          >
            <el-input v-model="form.edgeMiddleAddr"></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="1" class="TextCenter">:</el-col>
        <el-col :span="4">
          <el-form-item prop="edgeMiddlePort" label-width="0%">
            <el-input v-model="form.edgeMiddlePort"></el-input>
          </el-form-item>
        </el-col>
      </div> -->
    </el-form>
    <div v-show="mode !== 'look'" slot="footer" class="dialog-footer rowCenter">
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </div>
  </el-dialog>
</template>

<script>
import {
  updateDevice,
  addDevice,
  getstreams,
  editStreams,
} from '@/api/monitoring/devices'
import { findCodecsList } from '@/api/codecs/codecs'
import url from '@/libs/api.request'
export default {
  name: 'devicesEdit',
  data() {
    return {
      mode: '',
      form: {
        isActived: 1,
        netProtocol: 'UDP',
        deviceAddr: '127.0.0.1',
        devicePort: 0,
        reportAddr: '127.0.0.1',
        reportPort: 0,
        edgeMiddleAddr: '127.0.0.1',
        edgeMiddlePort: 1883,
        pointNums: 4,
        uploadMethod: 'autoupload',
        projectId: 112,
      },
      rules: {
        deviceName: [
          { required: true, trigger: 'blur', message: '请输入名称' },
        ],
        netProtocol: [
          { required: true, trigger: 'change', message: '请选择网络通信方式' },
        ],
        codecsId: [
          { required: true, trigger: 'change', message: '请选择解算方式' },
        ],
        collectInterval: [
          { required: true, trigger: 'blur', message: '请输入采样间隔' },
        ],
        pointNums: [
          { required: true, trigger: 'blur', message: '请输入测点个数' },
        ],
        deviceAddr: [
          { required: true, trigger: 'blur', message: '请输入测站地址' },
        ],
        devicePort: [
          { required: true, trigger: 'blur', message: '请输入测站端口' },
        ],
        reportAddr: [
          { required: true, trigger: 'blur', message: '请输入上报地址' },
        ],
        reportPort: [
          { required: true, trigger: 'blur', message: '请输入上报端口' },
        ],
        edgeMiddleAddr: [
          {
            required: true,
            trigger: 'blur',
            message: '请输入中间件地址',
          },
        ],
        edgeMiddlePort: [
          {
            required: true,
            trigger: 'blur',
            message: '请输入中间件端口',
          },
        ],
      },
      title: '',
      dialogFormVisible: false,
      protocolList: [
        { key: 'TCP', value: 'TCPServer' },
        { key: 'TCPClient', value: 'TCPClient' },
        { key: 'UDP', value: 'UDP' },
        { key: 'MQTT', value: 'MQTT' },
        { key: 'DTU', value: 'DTU' },
      ],
      uploadMethodList: [
        { key: 'autoupload', value: '自动上传' },
        { key: 'polling', value: '指令模式' },
      ],
      codecsList: '',
      streams: {
        file: {
          id: 0,
          deviceId: 0,
          taskName: '数据存储-FILESQL',
          taskDesc: '',
          executeMethod: 'Native',
          executeLanguage: 'asp.net',
          executeFilePath: 'sparrow.EdgeApp.Stream.Native.dll',
          executeParams: '',
          isActived: 1,
        },
        hex: {
          id: 0,
          deviceId: 0,
          taskName: 'UDP转发-Hex',
          taskDesc: '',
          executeMethod: 'Native',
          executeLanguage: 'asp.net',
          executeFilePath: 'sparrow.EdgeApp.Stream.Native.dll',
          executeParams: '',
          isActived: 1,
        },
        before: {
          id: 0,
          deviceId: 0,
          taskName: 'UDP转发-Before',
          taskDesc: '',
          executeMethod: 'Native',
          executeLanguage: 'asp.net',
          executeFilePath: 'sparrow.EdgeApp.Stream.Native.dll',
          executeParams: '0',
          isActived: 0,
        },
        after: {
          id: 0,
          deviceId: 0,
          taskName: 'UDP转发-After',
          taskDesc: '',
          executeMethod: 'Native',
          executeLanguage: 'asp.net',
          executeFilePath: 'sparrow.EdgeApp.Stream.Native.dll',
          executeParams: '',
          isActived: 0,
        },
      },
      fileList: [],
      baseUrl: '',
    }
  },
  created() {},
  mounted() {
    findCodecsList({
      pageSize: 9999,
      currentPage: 1,
    }).then((res) => {
      if (res.code == 200) {
        this.codecsList = res.data
      } else {
      }
    })
    this.baseUrl = url.baseURL
  },
  methods: {
    handlePreview(file) {},
    handleRemove(file) {
      if (file && file.status === 'success') {
        return this.$confirm(`确定移除 ${file.name}？`).then(() => {
          return true
        })
      }
      return false
    },
    uploadSuccess(res) {
      this.form.pic = res.data.url
    },
    changecodes() {
      let code = this.codecsList.find((x) => x.codecsId == this.form.codecsId)
      console.log(code)
      if (code) {
        this.form.pointNums = code.defaultNumber
      }
    },
    showEdit(row, mode) {
      this.mode = mode
      if (mode == 'create') {
        this.title = '新增测站'
        this.fileList = []
      } else {
        this.form = Object.assign({}, row)
        if (mode == 'edit') {
          this.title = '编辑测站'
          this.fileList = []
          if (this.form.pic && this.form.pic != '') {
            this.fileList = [
              { name: 'test', url: this.baseUrl + '/' + this.form.pic },
            ]
          }
          getstreams(row.id).then((res) => {
            if (res.code == 200) {
              this.streams = res.data
            } else {
            }
          })
        }
        if (mode == 'look') this.title = '结构物详情'
      }
      this.dialogFormVisible = true
    },
    close() {
      this.$refs['form'].resetFields()
      this.form = this.$options.data().form
      this.dialogFormVisible = false
    },
    save() {
      this.$refs['form'].validate(async (valid) => {
        if (valid) {
          let res
          if (this.mode === 'edit') {
            res = await updateDevice(this.form)
            editStreams(this.streams).then((res) => {
              if (res.code == 200) {
              } else {
              }
            })
          } else if (this.mode === 'create') {
            res = await addDevice(this.form)
            if (res.code == 200) {
              console.log('addDevice => editStreams')
              this.streams.file.deviceId = res.data
              this.streams.hex.deviceId = res.data
              this.streams.before.deviceId = res.data
              this.streams.after.deviceId = res.data
              editStreams(this.streams).then((res2) => {
                if (res2.code == 200) {
                } else {
                }
              })
            }
          }
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
  width: 100%;
}

.ModificationLeft {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
}
.ModificationLeft > div {
  width: 40%;
}

.TextCenter {
  text-align: center;
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
</style>