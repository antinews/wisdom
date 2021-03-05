<template>
  <el-dialog
    :title="title"
    :visible.sync="dialogFormVisible"
    width="50%"
    @close="close"
  >
    <el-form ref="form" :model="form" :rules="rules" label-width="30%">
      <div class="Modification">
        <div>
          <el-form-item label="名称：" prop="alias">
            <el-input v-model="form.alias"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="编号：" prop="kindCode">
            <el-input v-model="form.kindCode"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="单位：" prop="unit">
            <el-input v-model="form.unit"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="名称：" prop="kindName">
            <el-input v-model="form.kindName"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="有效位数：" prop="precision">
            <el-input v-model="form.precision"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="最大值：" prop="maxValue">
            <el-input v-model="form.maxValue"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="最小值：" prop="minValue">
            <el-input v-model="form.minValue"></el-input>
          </el-form-item>
        </div>
        <div style="width: 100%"></div>
        <div style="width: 50%">
          <el-form-item label="正常状态图标：" label-width="30%">
            <el-upload
              class="upload-demo"
              :action="baseUrl + '/api/Monitor/Devices/UploadImg'"
              :on-preview="handlePreview"
              :on-remove="handleRemove"
              :on-success="uploadSuccess1"
              :file-list="fileList1"
              list-type="picture"
              :limit="1"
            >
              <el-button size="small" type="primary">点击上传</el-button>
            </el-upload>
          </el-form-item>
        </div>
        <div style="width: 50%">
          <el-form-item label="不可用状态图标：" label-width="30%">
            <el-upload
              class="upload-demo"
              :action="baseUrl + '/api/Monitor/Devices/UploadImg'"
              :on-preview="handlePreview"
              :on-remove="handleRemove"
              :on-success="uploadSuccess2"
              :file-list="fileList2"
              list-type="picture"
              :limit="1"
            >
              <el-button size="small" type="primary">点击上传</el-button>
            </el-upload>
          </el-form-item>
        </div>
        <div style="width: 50%">
          <el-form-item label="一级报警图标：" label-width="30%">
            <el-upload
              class="upload-demo"
              :action="baseUrl + '/api/Monitor/Devices/UploadImg'"
              :on-preview="handlePreview"
              :on-remove="handleRemove"
              :on-success="uploadSuccess3"
              :file-list="fileList3"
              list-type="picture"
              :limit="1"
            >
              <el-button size="small" type="primary">点击上传</el-button>
            </el-upload>
          </el-form-item>
        </div>
        <div style="width: 50%">
          <el-form-item label="二级报警图标：" label-width="30%">
            <el-upload
              class="upload-demo"
              :action="baseUrl + '/api/Monitor/Devices/UploadImg'"
              :on-preview="handlePreview"
              :on-remove="handleRemove"
              :on-success="uploadSuccess4"
              :file-list="fileList4"
              list-type="picture"
              :limit="1"
            >
              <el-button size="small" type="primary">点击上传</el-button>
            </el-upload>
          </el-form-item>
        </div>
      </div>
    </el-form>
    <div v-show="mode !== 'look'" slot="footer" class="dialog-footer rowCenter">
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </div>
  </el-dialog>
</template>

<script>
import { getKindinfoEdit } from '@/api/sensorTypeSetting/sensorTypeSetting'
import url from '@/libs/api.request'
export default {
  name: 'sensorTypeEdit',
  data() {
    return {
      mode: '',
      form: {},
      rules: {
        projectName: [
          { required: true, trigger: 'blur', message: '请输入名称' },
        ],
        monitoringKindId: [
          { required: true, trigger: 'change', message: '请选择传感器类型' },
        ],
      },
      title: '',
      dialogFormVisible: false,
      fileList1: [],
      fileList2: [],
      fileList3: [],
      fileList4: [],
      baseUrl: '',
    }
  },
  created() {},
  mounted() {
    this.baseUrl = url.baseURL
  },
  methods: {
    handleRemove(file, fileList) {
      console.log(file, fileList)
    },
    handlePictureCardPreview(file) {},
    //正常图标
    uploadSuccess1(res) {
      //this.form.
    },
    uploadSuccess1(res) {
      this.form.normalIcon = res.data.url
    },
    uploadSuccess2(res) {
      this.form.loseIcon = res.data.url
    },
    uploadSuccess3(res) {
      this.form.earlyWarningIcon = res.data.url
    },
    uploadSuccess4(res) {
      this.form.alarmIcon = res.data.url
    },
    handlePreview(file) {},
    showEdit(row, mode) {
      this.mode = mode
      if (mode == 'create') this.title = '新增结构物'
      else {
        this.form = Object.assign({}, row)
        if (mode == 'edit') {
          this.title = '编辑传感器类型'
          this.fileList1 = this.fileList2 = this.fileList3 = this.fileList4 = []
          if (this.form.normalIcon && this.form.normalIcon != '') {
            this.fileList1 = [
              { name: 'test', url: this.baseUrl + '/' + this.form.normalIcon },
            ]
          }
          if (this.form.loseIcon && this.form.loseIcon != '') {
            this.fileList2 = [
              { name: 'test', url: this.baseUrl + '/' + this.form.loseIcon },
            ]
          }
          if (this.form.earlyWarningIcon && this.form.earlyWarningIcon != '') {
            this.fileList3 = [
              {
                name: 'test',
                url: this.baseUrl + '/' + this.form.earlyWarningIcon,
              },
            ]
          }
          if (this.form.alarmIcon && this.form.alarmIcon != '') {
            this.fileList4 = [
              { name: 'test', url: this.baseUrl + '/' + this.form.alarmIcon },
            ]
          }
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
          if (this.mode === 'edit') res = await getKindinfoEdit(this.form)
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
  width: 30%;
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