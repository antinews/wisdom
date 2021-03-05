<template>
  <el-dialog
    :title="title"
    :visible.sync="dialogFormVisible"
    width="40%"
    @close="close"
  >
    <el-tree
      :data="permissionTree"
      @node-click="nodeClick"
      node-key="id"
      highlight-current
      default-expand-all
      :expand-on-click-node="false"
      ref="tree"
      :render-content="renderContent"
    ></el-tree>
    <div slot="footer" class="dialog-footer rowCenter">
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </div>
  </el-dialog>
</template>

<script>
import { loadPermissionTree } from '@/api/systems/permission'
import { assignPermission } from '@/api/systems/role'

export default {
  data() {
    return {
      permissionTree: [],
      selectedPermissions: [],
      dialogFormVisible: false,
      title: '',
      currentRoleId: '',
    }
  },
  methods: {
    async save() {
      let data = {
        id: this.currentRoleId,
        permissions: this.selectedPermissions,
      }
      let res = await assignPermission(data)
      const { message } = res
      this.$baseMessage(message, 'success')
      this.close()
    },
    show(row) {
      this.doLoad(row.id)
      this.title = row.roleName + '权限分配'
      this.dialogFormVisible = true
    },
    doLoad(id) {
      this.currentRoleId = id
      loadPermissionTree(id).then((res) => {
        let data = res.data
        this.selectedPermissions = data.selectedPermissions
        this.permissionTree = data.tree
      })
    },
    close() {
      this.dialogFormVisible = false
    },
    nodeClick(data) {
      this.$emit('nodeClick', data)
    },
    change(id) {
      this.$refs.tree.setCurrentKey(id)
    },
    renderContent(h, { node, data }) {
      return (
        <span class="custom-tree-node">
          <span>{data.title}</span>
          <span>
            <el-checkbox-group
              style="display:inline-block; margin-right:25px"
              v-model={this.selectedPermissions}
              on-change={() => {
                this.changePermission(node)
              }}
            >
              {data.permissions.map((item) => {
                return (
                  <el-checkbox label={item.id} key={item.id}>
                    {item.name}
                  </el-checkbox>
                )
              })}
            </el-checkbox-group>
            <el-checkbox
              label="全选"
              v-model={data.allAssigned}
              style={
                'visibility:' +
                (data.permissions && data.permissions.length
                  ? 'visible'
                  : 'hidden')
              }
              on-change={(checked) =>
                this.changeAllPermission(checked, data.permissions, node)
              }
            ></el-checkbox>
          </span>
        </span>
      )
    },
    changePermission(node) {
      let counter = 0,
        len = node.data.permissions.length
      for (let i = 0; i < len; i++) {
        const el = node.data.permissions[i]
        if (~this.selectedPermissions.indexOf(el.id)) counter++
      }
      this.$set(node.data, 'allAssigned', counter === len)
    },
    changeAllPermission(checked, permissions, node) {
      this.$set(node.data, 'allAssigned', checked)
      if (!permissions || permissions.length <= 0) {
        return
      }
      if (checked) {
        // 全选
        for (let i = 0, len = permissions.length; i < len; i++) {
          var d = permissions[i]
          var index = this.selectedPermissions.indexOf(d.id)
          if (index == -1) {
            this.selectedPermissions.push(d.id)
          }
        }
      } else {
        // 取消全选
        for (let i = 0, len = permissions.length; i < len; i++) {
          var d = permissions[i]
          var index = this.selectedPermissions.indexOf(d.id)
          if (index !== -1) {
            this.selectedPermissions.splice(index, 1)
          }
        }
      }
    },
  },
}
</script>

<style>
.custom-tree-node {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: space-between;
  font-size: 14px;
  padding-right: 2px;
}
</style>