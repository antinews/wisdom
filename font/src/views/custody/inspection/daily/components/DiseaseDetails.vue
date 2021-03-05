<template>
  <el-dialog
    :title="title"
    :visible.sync="dialogFormVisible"
    width="40%"
    @close="close"
  >
    <el-tabs v-model="activeName" @tab-click="handleClick">
      <el-tab-pane label="病害信息" name="first">
        <el-form
          ref="form"
          :model="form"
          label-width="30%"
          class="Modification"
        >
          <div>
            <el-form-item label="桥名：">
              {{ form.bridgeName }}
            </el-form-item>
          </div>
          <div>
            <el-form-item label="桥型：">
              {{ form.bridgeType }}
            </el-form-item>
          </div>
          <el-form-item label="部位：">
            {{ form.itemDescription !== '0' ? form.weather : '' }}
          </el-form-item>
          <el-form-item label="检查员：">
            {{ form.trueName }}
          </el-form-item>
          <el-form-item label="检查时间：">
            {{ form.recordTime | timeFilter }}
          </el-form-item>
          <el-form-item label="天气：">
            {{ form.weather !== '0' ? form.weather : '' }}
          </el-form-item>
          <el-form-item label="地址：" style="width: 100%" label-width="15%">
            {{ form.userPositionDescription }}
          </el-form-item>
          <el-form-item
            label="病害图片："
            style="width: 100%"
            label-width="15%"
          >
            <el-scrollbar>
              <el-image
                v-for="(o, i) in fileList"
                :key="i"
                style="width: 120px; height: 120px"
                :src="o.url"
                :preview-src-list="[o.url]"
              ></el-image>
            </el-scrollbar>
          </el-form-item>
        </el-form>
      </el-tab-pane>
      <el-tab-pane label="流程信息" name="second">暂无</el-tab-pane>
      <el-tab-pane label="历史病害" name="third">暂无</el-tab-pane>
    </el-tabs>
    <div slot="footer" class="dialog-footer rowCenter">
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </div>
  </el-dialog>
</template>

<script>
import { serverFilePath } from '@/api/custody/inspection/daily'
export default {
  name: 'DiseaseDetails',
  props: ['title'],
  data() {
    return {
      activeName: 'first',
      dialogFormVisible: false,
      form: {},
      fileList: [],
    }
  },
  created() {},
  methods: {
    handleClick() {},
    show(row) {
      this.form = Object.assign({}, row)
      this.list = this.form.pic.split('@').forEach((x) => {
        this.fileList.push({ name: x, url: serverFilePath + x })
      })
      this.dialogFormVisible = true
    },
    close() {
      this.dialogFormVisible = false
      this.fileList = []
    },
    save() {},
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
.Modification .el-select {
  width: 100%;
}

.el-scrollbar .el-scrollbar__wrap .el-scrollbar__view {
  white-space: nowrap;
}
</style>