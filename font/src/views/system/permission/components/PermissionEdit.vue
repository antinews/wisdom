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
          <el-form-item label="权限名称：" prop="name">
            <el-input
              v-model="form.name"
              :maxlength="10"
              placeholder="请输入权限名称"
            ></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="操作码：" prop="actionCode">
            <el-input
              v-model="form.actionCode"
              :maxlength="20"
              placeholder="请输入操作码"
            ></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="关联菜单：" prop="menuName">
            <el-select
              v-model="form.menuName"
              placeholder="请选择上级菜单"
              ref="menuTree"
            >
              <el-option
                :label="form.parentId"
                :value="0"
                style="
                  width: 100%;
                  height: 200px;
                  overflow: auto;
                  background-color: #fff;
                "
              >
                <menu-tree
                  ref="mytree"
                  @nodeClick="handleNodeClickEven"
                ></menu-tree>
              </el-option>
            </el-select>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="类型：" prop="type">
            <el-select v-model="form.type">
              <el-option :value="1" label="菜单"></el-option>
              <el-option :value="0" label="按钮"></el-option>
            </el-select>
          </el-form-item>
        </div>

        <div>
          <el-form-item label="备注：" prop="description">
            <el-input
              placeholder="请输入备注"
              type="textarea"
              :rows="4"
              v-model="form.description"
              style="width: 254%"
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
import { edit, create } from '@/api/systems/permission'
import MenuTree from '_c/Systems/MenuTree'
export default {
  name: 'PermissionEdit',
  components: { MenuTree },
  data() {
    return {
      mode: '',
      parentMenuName: '',
      form: {},
      rules: {
        name: [{ required: true, trigger: 'blur', message: '请输入权限名称' }],
        actionCode: [
          { required: true, trigger: 'blur', message: '请输入操作码' },
        ],
        menuId: [{ required: true, trigger: 'blur', message: '请选择菜单' }],
        type: [{ required: true, trigger: 'blur', message: '请选择按钮' }],
        // component: [{ required: true, trigger: 'blur', message: '请输入组件' }],
      },
      title: '',
      dialogFormVisible: false,
    }
  },
  created() {},
  methods: {
    handleNodeClickEven(data) {
      // this.form.menuName = data.label
      // this.form.menuId = data.id
      this.$set(this.form, 'menuId', data.id)
      this.$set(this.form, 'menuName', data.label)
      this.$refs.menuTree.blur()
    },
    showEdit(row) {
      if (!row) {
        this.title = '新增权限'
        this.mode = 'create'
        // this.form.type = 0
        this.$set(this.form, 'type', 0)
      } else {
        this.title = '编辑权限信息'
        this.mode = 'edit'
        this.form = Object.assign({}, row)
        this.$nextTick(() => {
          this.$refs.mytree.change(this.form.menuId)
        })
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