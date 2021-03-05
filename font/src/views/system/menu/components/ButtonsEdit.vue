<template>
  <el-dialog
    :title="title"
    :visible.sync="dialogFormVisible"
    width="45%"
    @close="close"
  >
    <el-form ref="form" :model="form" :rules="rules" label-width="35%">
      <el-divider content-position="left">页面信息</el-divider>
      <div class="Modification">
        <div>
          <el-form-item label="菜单名称：" prop="navTitle">
            <el-input
              v-model="form.navTitle"
              placeholder="菜单栏显示的中文"
            ></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="上级菜单：" prop="parentName">
            <el-input
              v-model="form.parentName"
              placeholder="菜单栏显示的中文"
            ></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="页面路径：" prop="component">
            <el-input
              v-model="form.component"
              placeholder="页面在src下的绝对路径"
            ></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="页面名称：" prop="name">
            <el-input
              v-model="form.name"
              placeholder="请与页面name属性一致"
            ></el-input>
          </el-form-item>
        </div>
      </div>
      <el-divider content-position="left">权限信息</el-divider>
      <el-form-item label="权限：" label-width="14%">
        <el-checkbox-group v-model="checkedPermission">
          <el-checkbox
            v-for="(o, i) in buttons"
            :label="o.code"
            :key="i"
            @change="handleCheckedPermissionChange($event, o)"
          >
            {{ o.title }}
          </el-checkbox>
        </el-checkbox-group>
      </el-form-item>
      <div class="list-scroll">
        <div class="Permission" v-for="(o, i) in form.permissions" :key="i">
          <div>
            <el-form-item
              label="权限名称："
              :prop="'permissions.' + i + '.name'"
              :rules="{
                required: true,
                message: '权限名称不能为空',
                trigger: 'blur',
              }"
            >
              <el-select
                v-model="o.name"
                @change="handlePermissionChangEvent($event, i)"
              >
                <el-option
                  v-for="(o, i) in buttons"
                  :label="o.title"
                  :value="o.code"
                  :key="i"
                ></el-option>
              </el-select>
            </el-form-item>
          </div>
          <div>
            <el-form-item
              label="权限码："
              :prop="'permissions.' + i + '.code'"
              :rules="{
                required: true,
                message: '权限码不能为空',
                trigger: 'blur',
              }"
            >
              <el-input v-model="o.code"></el-input>
            </el-form-item>
          </div>
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
import { editPermissions, getPermissionsByMenuId } from '_api/systems/menu'
import { getDicsByKey } from '_api/other/common'
export default {
  data() {
    return {
      title: '菜单权限分配',
      dialogFormVisible: false,
      form: {},
      rules: {},
      buttons: '',
      checkedPermission: [],
    }
  },
  created() {
    this.__init()
  },
  methods: {
    handleCheckedPermissionChange(v, p) {
      if (v) {
        let permission = {
          name: p.title,
          code: this.form.name + '_' + p.code,
        }
        this.addPermission(permission)
      } else this.removePermission(p.title)
    },
    async __init() {
      let buttons_res = await getDicsByKey('buttons')
      this.buttons = buttons_res.data
    },
    async show(row) {
      this.form = Object.assign({}, row)
      let { data } = await getPermissionsByMenuId(this.form.id)
      data.forEach((x) => this.addPermission(x))
      this.checkedPermission = data.map((x) =>
        x.code.substr(x.code.indexOf('_') + 1)
      )
      this.dialogFormVisible = true
    },
    close() {
      this.$refs['form'].resetFields()
      this.form = this.$options.data().form
      this.dialogFormVisible = false
    },
    removePermission(name) {
      let o = Array.from(this.form.permissions).find((x) => x.name == name)
      let idx = Array.from(this.form.permissions).indexOf(o)
      if (~idx) this.form.permissions.splice(idx, 1)
    },
    addPermission(permission) {
      if (this.form.permissions === undefined)
        this.$set(this.form, 'permissions', [])
      this.form.permissions.push(permission)
    },
    async save() {
      let valid
      this.$refs['form'].validate((res) => (valid = res))
      if (valid) {
        let data = {
          menuId: this.form.id,
          permissions: this.form.permissions,
        }
        let res = await editPermissions(data)
        const { message } = res
        this.$baseMessage(message, 'success')
        this.close()
      }
    },
    handlePermissionChangEvent(v, idx) {
      this.form.permissions[idx].code = this.$parent.$options.name + '_' + v
    },
  },
}
</script>

<style scoped>
.Modification,
.Permission {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
}
.Modification > div {
  width: 50%;
}
.Permission .el-select {
  width: 100%;
}
.Permission > div {
  width: 50%;
}

/* 滚动条 */
.list-scroll {
  max-height: 200px;
  overflow-y: hidden;
  overflow-y: auto;
}
/* .list-scroll:hover {
  overflow-y: scroll;
} */
/*滚动条整体样式*/
.list-scroll::-webkit-scrollbar {
  width: 5px; /*竖向滚动条的宽度*/
  height: 5px; /*横向滚动条的高度*/
}
.list-scroll::-webkit-scrollbar-thumb {
  /*滚动条里面的小方块*/
  background: rgb(131, 168, 255);
  border-radius: 5px;
}
.list-scroll::-webkit-scrollbar-track {
  /*滚动条轨道的样式*/
  background: #ccc;
  border-radius: 5px;
}
</style>