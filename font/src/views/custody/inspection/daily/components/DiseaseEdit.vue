<template>
  <div>
    <el-dialog
      :title="title"
      :visible.sync="dialogFormVisible"
      width="44%"
      @close="close"
    >
      <!-- <el-divider content-position="left">
      这里就不具体写了，请自行完善
    </el-divider> -->
      <el-form ref="form" :model="form" :rules="rules" label-width="30%">
        <div v-show="mode === 'create'" class="Modification">
          <div>
            <el-form-item label="桥梁：" prop="bridgeId">
              <el-cascader
                ref="bridge-cascader"
                :props="bridgeProps"
                v-model="form.bridgeId"
                @change="bridgeChange"
                :show-all-levels="false"
              ></el-cascader>
            </el-form-item>
          </div>
          <div>
            <el-form-item v-if="form.bridgeId" label="部位：" prop="optionId">
              <el-cascader
                :props="optionProps"
                v-model="form.optionId"
                :show-all-levels="false"
                @change="optionChange"
              ></el-cascader>
            </el-form-item>
            <el-form-item v-else label="部位：" prop="optionId">
              <el-select
                placeholder="请先选择桥梁"
                disabled
                v-model="form.optionId"
              >
                <el-option
                  v-for="(item, idx) in []"
                  :key="idx"
                  :label="item.text"
                  :value="item.id"
                ></el-option>
              </el-select>
            </el-form-item>
          </div>
          <div>
            <el-form-item label="病害：" prop="itemId">
              <el-select
                v-model="form.itemId"
                :disabled="!form.optionId"
                placeholder="请先选择部位"
                @change="handleDiseaseChangeEvent"
                clearable
              >
                <el-option
                  v-for="(item, idx) in diseases"
                  :key="idx"
                  :label="item.text"
                  :value="item.id"
                ></el-option>
              </el-select>
            </el-form-item>
          </div>
          <div>
            <el-form-item label="大小：">
              <el-input :disabled="!form.itemId" placeholder="请先选择病害">
                <template slot="append">{{ unit }}</template>
              </el-input>
            </el-form-item>
          </div>
          <div>
            <el-form-item label="病害等级：" prop="missionLevel">
              <el-select v-model="form.missionLevel">
                <el-option
                  v-for="(item, idx) in diseaseLevels"
                  :key="idx"
                  :label="item.title"
                  :value="item.code"
                ></el-option>
              </el-select>
            </el-form-item>
          </div>
          <div>
            <el-form-item label="新旧：" prop="missionType">
              <el-select v-model="form.missionType">
                <el-option
                  v-for="(item, idx) in ['旧病害', '新病害']"
                  :key="idx"
                  :label="item"
                  :value="item"
                ></el-option>
              </el-select>
            </el-form-item>
          </div>
          <div>
            <el-form-item label="病害描述：" prop="recordDescription">
              <el-input
                type="textarea"
                :rows="4"
                :maxlength="200"
                placeholder="请输入描述信息"
                v-model="form.recordDescription"
                style="width: 243%"
              ></el-input>
            </el-form-item>
          </div>
          <div></div>
          <div>
            <el-form-item label="巡检时间：" prop="recordTime">
              <el-date-picker
                v-model="form.recordTime"
                type="date"
                value-format="yyyy-MM-dd"
              ></el-date-picker>
            </el-form-item>
          </div>
          <!-- <div>
          <el-form-item label="巡检员：" prop="trueName">
            <el-input v-model="form.trueName"></el-input>
          </el-form-item>
        </div> -->
          <div></div>
          <div>
            <el-form-item label="病害图片：">
              <el-upload
                class="upload-demo"
                ref="upload"
                :action="actionUrl"
                :auto-upload="false"
                :on-preview="handlePreview"
                :before-remove="handleBeforeRemove"
                :on-change="handleUpload"
                :file-list="fileList"
                list-type="picture"
              >
                <el-button size="small" type="primary">点击上传</el-button>
              </el-upload>
            </el-form-item>
          </div>
        </div>
        <div v-show="mode === 'edit'" class="Modification">
          <div>
            <el-form-item label="区域：" prop="areaName">
              <el-input v-model="form.areaName" disabled></el-input>
            </el-form-item>
          </div>
          <div>
            <el-form-item label="桥型：" prop="bridgeType">
              <el-input v-model="form.bridgeType" disabled></el-input>
            </el-form-item>
          </div>
          <div>
            <el-form-item label="桥梁：" prop="bridgeName">
              <el-input v-model="form.bridgeName" disabled></el-input>
            </el-form-item>
          </div>
          <div>
            <el-form-item label="部位：" prop="optionParentName">
              <el-input v-model="form.optionParentName" disabled></el-input>
            </el-form-item>
          </div>
          <div>
            <el-form-item label="项目：" prop="optionName">
              <el-input v-model="form.optionName" disabled></el-input>
            </el-form-item>
          </div>
          <div>
            <el-form-item label="大小：">
              <el-input>
                <template slot="append">{{ form.unit }}</template>
              </el-input>
            </el-form-item>
          </div>
          <div>
            <el-form-item label="病害等级：" prop="missionLevel">
              <el-select v-model="form.missionLevel">
                <el-option
                  v-for="(item, idx) in diseaseLevels"
                  :key="idx"
                  :label="item.title"
                  :value="item.code"
                ></el-option>
              </el-select>
            </el-form-item>
          </div>
          <div>
            <el-form-item label="新旧：" prop="missionType">
              <el-select v-model="form.missionType">
                <el-option
                  v-for="(item, idx) in ['旧病害', '新病害']"
                  :key="idx"
                  :label="item"
                  :value="item"
                ></el-option>
              </el-select>
            </el-form-item>
          </div>
          <el-form-item label="地址：" style="width: 100%" label-width="15%">
            <el-input
              style="width: 100%"
              v-model="form.userPositionDescription"
              maxlength="256"
              placeholder="请输入住址"
            >
              <el-button
                slot="append"
                type="primary"
                class="get-location"
                @click="handleLocation"
              >
                获取位置
              </el-button>
            </el-input>
          </el-form-item>
          <div>
            <el-form-item label="病害描述：" prop="recordDescription">
              <el-input
                type="textarea"
                :rows="4"
                :maxlength="200"
                placeholder="请输入描述信息"
                v-model="form.recordDescription"
                style="width: 243%"
              ></el-input>
            </el-form-item>
          </div>
          <div></div>
          <div>
            <el-form-item label="巡检时间：" prop="recordTime">
              <el-date-picker
                v-model="form.recordTime"
                type="date"
                value-format="yyyy-MM-dd"
              ></el-date-picker>
            </el-form-item>
          </div>
          <div>
            <el-form-item label="巡检员：" prop="trueName">
              <el-input v-model="form.trueName"></el-input>
            </el-form-item>
          </div>
          <div>
            <el-form-item label="病害图片：">
              <el-upload
                class="upload-demo"
                ref="upload"
                :action="actionUrl"
                :auto-upload="false"
                :on-preview="handlePreview"
                :before-remove="handleBeforeRemove"
                :on-change="handleUpload"
                :on-success="handleUploadSuccess"
                :file-list="fileList"
                list-type="picture"
              >
                <el-button size="small" type="primary">点击上传</el-button>
              </el-upload>
            </el-form-item>
          </div>
        </div>
      </el-form>
      <div slot="footer" class="dialog-footer rowCenter">
        <el-button @click="close">取 消</el-button>
        <el-button type="primary" @click="save">确 定</el-button>
      </div>
    </el-dialog>
    <ant-map ref="map" @location="getLocation"></ant-map>
  </div>
</template>

<script>
import {
  edit,
  create,
  getDropDownListData,
  uploadAction,
  serverFilePath,
} from '@/api/custody/inspection/daily'
import { getDicsByKey } from '_api/other/common'
import { mapGetters } from 'vuex'
import AntMap from '_c/Global/Map'
let self
export default {
  name: 'DiseaseEdit',
  props: ['areas'],
  components: {
    AntMap,
  },
  data() {
    return {
      mode: '',
      parentMenuName: '',
      form: {},
      rules: {
        bridgeId: [{ required: true, trigger: 'blur', message: '请选择桥梁' }],
        optionId: [{ required: true, trigger: 'blur', message: '请选择部位' }],
        menuId: [{ required: true, trigger: 'blur', message: '请选择菜单' }],
        type: [{ required: true, trigger: 'blur', message: '请选择按钮' }],
        // component: [{ required: true, trigger: 'blur', message: '请输入组件' }],
      },
      title: '',
      unit: '',
      diseases: [],
      diseaseLevels: [],
      dialogFormVisible: false,
      fileList: [],
      fileNameList: [],
      actionUrl: '',
      loading: false,
      bridgeProps: {
        lazy: true,
        emitPath: false,
        async lazyLoad(node, resolve) {
          const { level } = node
          let params = []
          let list = []
          if (level == 0) list = self.areas
          else {
            if (level == 1) {
              self.$set(self.form, 'cityAreaId', node.data.value)
              params = ['bridgeType']
            }
            if (level == 2) {
              self.$set(self.form, 'bridgeTypeId', node.data.value)
              params = ['bridge', self.form.cityAreaId, node.data.value]
            }
            const { data } = await getDropDownListData(...params) // load sons
            list = data
          }
          const nodes = list.map((item) => ({
            value: item.id,
            label: item.text,
            leaf: level == 2,
          }))
          // 通过调用resolve将子节点数据返回，通知组件数据加载完成
          resolve(nodes)
        },
      },
      optionProps: {
        lazy: true,
        emitPath: false,
        async lazyLoad(node, resolve) {
          const { level } = node
          let params = []
          if (level == 0) params = ['part', self.form.bridgeTypeId]
          if (level == 1) {
            self.$set(self.form, 'partId', node.data.value)
            params = ['item', self.form.bridgeTypeId, node.data.value]
          }
          let { data } = await getDropDownListData(...params)
          const nodes = data.map((item) => ({
            value: item.id,
            label: item.text,
            leaf: level == 1,
          }))
          // 通过调用resolve将子节点数据返回，通知组件数据加载完成
          resolve(nodes)
        },
      },
    }
  },
  computed: {
    ...mapGetters({
      username: 'user/username',
      userid: 'user/userid',
    }),
  },
  created() {
    self = this
    self.actionUrl = uploadAction
    self.__init()
  },
  methods: {
    async __init() {
      const { data } = await getDicsByKey('diseaseLevel')
      this.diseaseLevels = data
    },
    getLocation(map) {
      this.$set(this.form, 'userPositionDescription', map.address)
      this.$set(this.form, 'xLongitude', map.lng)
      this.$set(this.form, 'yLatitude', map.lat)
    },
    //#region 文件
    handlePreview(file) {},
    handleBeforeRemove(file, list) {
      if (file && file.status === 'success') {
        return this.$confirm(`确定移除 ${file.name}？`).then(() => {
          const index = this.fileNameList.indexOf(file.name)
          if (index !== -1) this.fileNameList.splice(index, 1)
        })
      }
      return true
    },
    handleUpload(file, fileList) {
      let type = file.raw.type
      let formats = ['image/jpeg', 'image/jpg', 'image/png']
      const isLt2M = file.raw.size / 1024 / 1024 < 2
      if (!formats.includes(type)) {
        this.$message.warning(`图片格式只支持：${formats.toString()}!`)
        this.fileList = fileList.slice(0, -1)
        return
      }
      if (!isLt2M) {
        this.$message.warning('上传文件大小不能超过 2MB!')
        this.fileList = fileList.slice(0, -1)
        return
      }
    },
    handleUploadSuccess(res) {
      console.log(2)
      this.fileNameList.push(res.data.name)
      this.$set(this.form, 'pic', this.fileNameList.join('@'))
      this.loading = true
    },
    //#endregion
    handleDiseaseChangeEvent(v) {
      let disease = this.diseases.find((x) => x.id == v)
      if (disease) this.unit = disease.unit
    },
    bridgeChange(v) {
      console.log(v)
    },
    async optionChange(v) {
      if (this.form.itemId) this.form.itemId = ''
      let { data } = await getDropDownListData(
        ...['disease', this.form.optionId]
      )
      this.diseases = data
    },
    handleLocation() {
      this.$refs['map'].show()
    },
    showEdit(row) {
      if (!row) {
        this.title = '新增病害'
        this.mode = 'create'
      } else {
        this.title = '编辑病害信息'
        this.mode = 'edit'
        this.form = Object.assign({}, row)
        this.list = this.form.pic.split('@').forEach((x) => {
          this.fileList.push({ name: x, url: serverFilePath + x })
          this.fileNameList.push(x)
        })
      }
      this.dialogFormVisible = true
    },
    close() {
      if (this.$refs['form']) this.$refs['form'].resetFields()
      this.form = this.$options.data().form
      this.fileList = []
      this.fileNameList = []
      this.dialogFormVisible = false
    },
    async save() {
      var valid
      this.$refs['form'].validate((res) => (valid = res))
      if (valid) {
        console.log(1)
        this.$refs.upload.submit() // 先上传 触发upload
        setTimeout(async () => {
          console.log(3)
          this.$set(this.form, 'unit', this.unit)
          this.$set(this.form, 'userId', this.userid)
          let res
          if (this.mode === 'edit') res = await edit(this.form)
          else if (this.mode === 'create') res = await create(this.form)
          const { message } = res
          this.$baseMessage(message, 'success')
          this.$emit('fetch-data')
          this.close()
        }, 500)
      }
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
  width: 50%;
}
.Modification .el-select,
.Modification .el-cascader {
  width: 100%;
}
</style>