<template>
  <el-dialog
    :title="title"
    :visible.sync="dialogFormVisible"
    width="40%"
    @close="close"
  >
    <!-- <el-divider content-position="left">
      这里就不具体写了，请自行完善
    </el-divider> -->
    <el-form ref="form" :model="form" :rules="rules" label-width="35%">
      <div class="Modification">
        <div>
          <el-form-item label="角色名称：" prop="roleName">
            <el-input
              placeholder="请输入角色名称"
              :maxlength="10"
              v-model="form.roleName"
            ></el-input>
          </el-form-item>
        </div>
        <div></div>
        <!-- <div>
          <el-form-item label="排序：" prop="sortNum">
            <el-input-number
              v-model="form.sortNum"
              style="width: 100%"
              :min="0"
              :max="10000"
            ></el-input-number>
          </el-form-item>
        </div> -->
        <!-- <div>
          <el-form-item label="是否默认：" prop="isAffix">
            <el-select v-model="form.isAffix">
              <el-option label="是" :value="1"></el-option>
              <el-option label="否" :value="0"></el-option>
            </el-select>
          </el-form-item>
        </div> -->
        <!-- <div></div> -->
        <div>
          <el-form-item label="描述：" prop="remark">
            <el-input
              type="textarea"
              :rows="4"
              :maxlength="200"
              placeholder="请输入描述信息"
              v-model="form.remark"
              style="width: 250%"
            ></el-input>
          </el-form-item>
        </div>
      </div>
    </el-form>
    <div slot="footer" class="dialog-footer rowCenter">
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </div>
  </el-dialog>
</template>

<script>
import { edit, create } from '@/api/systems/role'

export default {
  name: 'RoleEdit',
  data() {
    return {
      mode: '',
      parentMenuName: '',
      form: {},
      rules: {
        roleName: [
          { required: true, trigger: 'blur', message: '请输入角色名称' },
        ],
      },
      title: '',
      dialogFormVisible: false,
    }
  },
  created() {},

  methods: {
    handleNodeClickEven(data) {
      this.parentMenuName = data.label
      this.form.parentId = data.id
      this.$refs.menuTree.blur()
    },
    showEdit(row) {
      if (!row) {
        this.title = '新增角色'
        this.mode = 'create'
      } else {
        this.title = '编辑角色信息'
        this.mode = 'edit'
        this.form = Object.assign({}, row)
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
  width: 50%;
}
.Modification .el-select {
  width: 100%;
}
</style>