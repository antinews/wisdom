<template>
  <div>
    <el-button size="mini" type="primary" plain @click="refreshTree">
      刷新菜单树
    </el-button>
    <el-tree
      :data="menuTree"
      @node-click="nodeClick"
      node-key="id"
      highlight-current
      :default-expanded-keys="[0]"
      :expand-on-click-node="false"
      ref="tree"
    ></el-tree>
  </div>
</template>

<script>
import { loadMenuTree } from '@/api/systems/menu'

export default {
  data() {
    return {
      menuTree: [],
    }
  },
  mounted() {
    this.doLoadMenuTree()
  },
  methods: {
    doLoadMenuTree() {
      loadMenuTree().then((res) => {
        this.menuTree = res.data
      })
    },
    nodeClick(data) {
      // console.log(data)
      this.$emit('nodeClick', data)
    },
    change(id) {
      this.$refs.tree.setCurrentKey(id)
    },
    refreshTree() {
      this.doLoadMenuTree()
    },
  },
}
</script>