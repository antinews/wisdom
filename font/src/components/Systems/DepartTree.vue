<template>
  <div>
    <el-button size="mini" type="primary" plain @click="refreshTree">
      刷新部门树
    </el-button>
    <el-tree
      ref="tree"
      :load="loadNode"
      lazy
      :filter-node-method="filterNode"
      :props="defaultProps"
      node-key="id"
      :icon-class="null"
      @node-click="handleNodeClick"
    ></el-tree>
  </div>
</template>

<script>
import { GetDepartementList } from '@/api/systems/Department'

export default {
  data() {
    return {
      departTree: [],
      defaultProps: {
        label: 'departmentName',
        children: 'zones',
        isLeaf: (data, node) => {
          if (node.childNodes.length !== 0) {
            return true
          }
        },
      },
    }
  },
  mounted() {
    this.doLoadDepartTree()
  },
  methods: {
    doLoadDepartTree() {
      GetDepartementList(0).then((res) => {
        this.departTree = res.data
      })
    },
    async handleNodeClick(data) {
      this.$emit('nodeClick', data)
    },
    async loadNode(node, resolve) {
      if (!node.data) {
        const resDate = await this.GetNode(0)
        resolve(resDate)
      } else {
        const resDate = await this.GetNode(node.data.id)
        resolve(resDate)
      }
    },
    //通过ID读取子部门
    async GetNode(id) {
      const res = await GetDepartementList(id)
      return res.data
    },
    // 节点过滤操作
    filterNode(value, data) {
      if (!value) return true
      return data.name.indexOf(value) !== -1
    },
    change(id) {
      this.$refs.tree.setCurrentKey(id)
    },
    refreshTree() {
      this.doLoadDepartTree()
    },
  },
}
</script>