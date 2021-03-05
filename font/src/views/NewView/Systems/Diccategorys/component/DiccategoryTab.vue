<template>
  <el-dialog
    :title="title"
    :visible.sync="dialogFormVisible"
    width="500px"
    @close="close"
  >
    <el-form ref="form" :model="form" :rules="rules" label-width="100px">
      <el-form-item label="字典类型名" prop="title">
        <el-input v-model.trim="form.title" autocomplete="off"></el-input>
      </el-form-item>
      <el-form-item label="编码" prop="code">
        <el-input v-model.trim="form.code" autocomplete="off"></el-input>
      </el-form-item>
      <el-form-item label="排序" prop="sortNum">
        <el-input v-model.trim="form.sortNum" autocomplete="off"></el-input>
      </el-form-item>

      <el-form-item label="描述" prop="remark">
        <el-input
          type="textarea"
          :rows="2"
          v-model.trim="form.remark"
          autocomplete="off"
        />
      </el-form-item>
    </el-form>
    <div slot="footer" class="dialog-footer">
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </div>
  </el-dialog>
</template>

<script>
import { doEdit } from '@/api/table'
import { adddiccategory, SetDiccategoryById } from '@/api/systems/diccategorys'
export default {
  name: 'TableEcharts',
  data() {
    return {
      form: {
        id: 0,
        title: null,
        code: null,
        sortNum: null,
        remark: null,
      },
      rules: {
        title: [
          { required: true, trigger: 'blur', message: '请输入字典类型名' },
        ],
        code: [{ required: true, trigger: 'blur', message: '请输入编码' }],
      },
      options: [],
      title: '',
      dialogFormVisible: false,
    }
  },
  created() {},
  mounted() {},
  methods: {
    showEdit(row) {
      console.log(row)
      if (!row) {
        this.title = '添加'
      } else {
        this.title = '编辑'
        this.form = Object.assign({}, row)
      }
      console.log(this.form);
      this.dialogFormVisible = true
    },
    close() {
      this.$refs['form'].resetFields() //清空内容
      this.form = this.$options.data().form
      this.dialogFormVisible = false
      this.$emit('fetch-data')
    },
    async save() {
      this.$refs['form'].validate(async (valid) => {
        if (valid) {
          let res = null
          if (this.title == '添加') {
            res = await adddiccategory(this.form)
          } else {
            res = await SetDiccategoryById(this.form)
          }
          if (res.status == 200) {
            this.$baseMessage(res.info, 'success')
          } else {
            this.$baseMessage(res.info, 'error')
          }
          this.$emit('fetchdata')
          this.$refs['form'].resetFields() //清空内容
          this.dialogFormVisible = false

          this.form = this.$options.data().form
        } else {
          return false
        }
      })
    },
  },
}
</script>
