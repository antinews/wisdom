<template>
  <el-dialog
    :title="title"
    :visible.sync="dialogFormVisible"
    width="50%"
    @close="close"
  >
    <el-form ref="form" :model="form" :rules="rules" label-width="45%">
      <div class="Modification">
        <div>
          <el-form-item label="测点编码：" prop="pointCode">
            <el-input v-model="form.pointCode"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="测点名称：" prop="pointName">
            <el-input v-model="form.pointName"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item
            label="传感器类型："
            prop="monitoringKindId"
            label-width="45%"
          >
            <el-select v-model="form.monitoringKindId">
              <el-option
                v-for="item in kindinfoList"
                :label="item.kindName"
                :value="item.id"
                :key="item.id"
              ></el-option>
            </el-select>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="通道：" prop="chanel">
            <el-input v-model="form.chanel"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="有效数据上限：" prop="maxValue">
            <el-input v-model="form.maxValue"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="有效数据下限：" prop="minValue">
            <el-input v-model="form.minValue"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="一级阈值上限：" prop="range1Max">
            <el-input v-model="form.range1Max"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="一级阈值下限：" prop="range1Min">
            <el-input v-model="form.range1Min"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="二级阈值上限：" prop="range2Max">
            <el-input v-model="form.range2Max"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="二级阈值下限：" prop="range2Min">
            <el-input v-model="form.range2Min"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="测点参数名：" prop="formulaKey">
            <el-input v-model="form.computeFormula.formulaKey"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="测点参数值：" prop="formulaValue">
            <el-input v-model="form.computeFormula.formulaValue"></el-input>
          </el-form-item>
        </div>
        <div style="width: 24%">
          <el-form-item label="是否启用：" label-width="60%">
            <el-switch
              v-model="form.isActived"
              :active-value="1"
              :inactive-value="0"
            ></el-switch>
          </el-form-item>
        </div>
        <div style="width: 24%">
          <el-form-item label="是否进行合理性校验：" label-width="70%">
            <el-switch
              v-model="form.isReasonableValidate"
              :active-value="1"
              :inactive-value="0"
            ></el-switch>
          </el-form-item>
        </div>
        <div style="width: 24%">
          <el-form-item label="是否基准点：" label-width="60%">
            <el-switch
              v-model="form.isBasePoint"
              :active-value="1"
              :inactive-value="0"
            ></el-switch>
          </el-form-item>
        </div>
        <div style="width: 24%">
          <el-form-item label="是否阈值报警：" label-width="50%">
            <el-switch
              v-model="form.isThresholdAlarm"
              :active-value="1"
              :inactive-value="0"
            ></el-switch>
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
import {
  getPointList,
  deletePoint,
  singlePoint,
  updatePoint,
  BatchPoint,
  addPoint,
  baseUrl,
} from '@/api/monitoring/point'
import { getKindinfoList } from '@/api/sensorTypeSetting/sensorTypeSetting'
export default {
  name: 'pointEdit',
  data() {
    return {
      mode: '',
      form: {
        computeFormula: {},
      },
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
      bridgeType: '',
      kindinfoList: '',
    }
  },
  created() {},
  mounted() {
    getKindinfoList({
      pageSize: 9999,
      currentPage: 1,
      projectId: 112,
    }).then((res) => {
      if (res.code == 200) {
        this.kindinfoList = res.data
      } else {
      }
    })
  },
  methods: {
    showEdit(row, mode) {
      this.mode = mode
      if (mode == 'create') this.title = '新增结构物'
      else {
        this.form = Object.assign({}, row)
        if (mode == 'edit') this.title = '编辑测点'
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
          if (this.mode === 'edit') res = await updatePoint(this.form)
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