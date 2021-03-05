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
      <el-form-item label="类别">
        <el-input v-model.trim="category" autocomplete="off"></el-input>
      </el-form-item>
      <el-form-item label="状态" prop="sortnum">
        <el-select
          v-model="form.status"
          placeholder="请选择"
          style="width: 100%"
        >
          <el-option
            v-for="item in options"
            :key="item.id"
            :label="item.value"
            :value="item.id"
          ></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="描述" prop="Remark">
        <el-input
          type="textarea"
          :rows="2"
          v-model.trim="form.Remark"
          placeholder="请输入内容"
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
import { GetDiccategorysName } from '@/api/systems/diccategorys'
import { AddDics, SetdicsbyId } from '@/api/systems/Dics'
export default {
  name: 'TableEcharts',
  data() {
    return {
      form: {
        id: 0,
        title: null,
        code: null,
        sortNum: null,
        Remark: null,
      },
      category: null,
      rules: {
        title: [
          { required: true, trigger: 'blur', message: '请输入字典类型名' },
        ],
        code: [{ required: true, trigger: 'blur', message: '请输入编码' }],
      },
      options: [
        {
          id: 1,
          value: '启用',
        },
        {
          id: 0,
          value: '禁用',
        },
      ],
      title: '',
      dialogFormVisible: false,
    }
  },
  created() {},
  mounted() {},
  methods: {
    async showEdit(row, id) {
      console.log(row)
      if (id) {
        this.title = '添加'
        this.form.categoryId = id
        const res = await GetDiccategorysName(id)
        this.category = res.data
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
          let res = null
          if (this.title == '添加') {
            res = await AddDics(this.form)
          } else {
            res = await SetdicsbyId(this.form)
          }
          this.$baseMessage(res.info, 'success')
          this.$refs['form'].resetFields() //清空内容
          this.dialogFormVisible = false
          this.$emit('fetch-data')
          this.form = this.$options.data().form
        } else {
          return false
        }
      })
    },
  },
}
</script>
