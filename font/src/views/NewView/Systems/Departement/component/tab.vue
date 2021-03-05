<template>
  <el-dialog
    :title="title"
    :visible.sync="dialogFormVisible"
    width="500px"
    @close="close"
  >
    <el-form ref="form" :model="form" :rules="rules" label-width="80px">
      <el-form-item label="部门名" prop="departementName">
        <el-input
          v-model.trim="form.departmentName"
          autocomplete="off"
        ></el-input>
      </el-form-item>
      <el-form-item label="上级部门" prop="sortnum">
        <el-select
          v-model="form.parentId"
          placeholder="请选择"
          style="width: 100%"
        >
          <el-option
            v-for="item in options"
            :key="item.id"
            :label="item.departmentName"
            :value="item.id"
          ></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="排序" prop="sortnum">
        <el-input v-model.trim="form.sortnum" autocomplete="off"></el-input>
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
import {
  GetDepartement,
  AddDepartement,
   SetDepartementById,
} from '@/api/systems/Department'
export default {
  name: 'TableEcharts',
  data() {
    return {
      form: {
        createdUserId: 0,
        departmentName: null,
        parentId: null,
        sortnum: null,
      },
      rules: {
        departmentName: [
          { required: true, trigger: 'blur', message: '请输入部门名' },
        ],
        sortnum: [{ required: true, trigger: 'blur', message: '请输入排序' }],
      },
      options: [],
      title: '',
      dialogFormVisible: false,
    }
  },
  created() {},
  mounted() {
    this.GetDepartement()
  },
  methods: {
    async GetDepartement() {
      const res = await GetDepartement()
      this.options = res.data
      this.options.push({ departmentName: '父机构', id: 0 })
    },
    showEdit(row) {
      this.GetDepartement();
      if (!row) {
        this.title = '添加'
      } else {
        this.title = '编辑'
        this.form = Object.assign({}, row)
      }
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
          const { message } = await doEdit(this.form)
          let res = null
          if (this.title == '添加') {
            res = await AddDepartement(this.form)
          } else {
            res = await SetDepartementById(this.form)
          }
          console.log(res);
          this.$baseMessage(res.info, 'success')
          this.$refs['form'].resetFields() //清空内容
          this.dialogFormVisible = false
          this.$emit('fetch-data')
          this.form = this.$options.data().form
          this.GetDepartement()
        } else {
          return false
        }
      })
    },
  },
}
</script>
