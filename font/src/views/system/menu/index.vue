<template>
  <div class="menuManagement-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="12">
        <el-form :inline="true" :model="queryForm" @submit.native.prevent>
          <el-form-item label="菜单名称：">
            <el-input
              v-model.trim="queryForm.kw"
              placeholder="模糊查询"
              clearable
            />
          </el-form-item>
          <el-form-item>
            <el-button icon="el-icon-search" type="primary" @click="queryData">
              查询
            </el-button>
            <el-button
              icon="el-icon-refresh-right"
              type="info"
              plain
              @click="resetQuery"
            >
              重置
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-left-panel>
      <vab-query-form-right-panel :span="12">
        <el-button
          v-can="'menuManagement_add'"
          icon="el-icon-plus"
          type="primary"
          @click="handleEdit"
        >
          新增
        </el-button>
      </vab-query-form-right-panel>
    </vab-query-form>
    <el-table
      v-loading="listLoading"
      :height="height"
      :data="list"
      :element-loading-text="elementLoadingText"
      row-key="linkUrl"
      border
      ref="menuTable"
      default-expand-all
      :tree-props="{ children: 'children', hasChildren: 'hasChildren' }"
    >
      <!-- <el-table-column type="selection"></el-table-column> -->
      <el-table-column show-overflow-tooltip label="菜单名称">
        <template #default="{ row }">
          <a @click="handleEdit(row, true)">
            {{ row.navTitle }}
          </a>
        </template>
      </el-table-column>

      <el-table-column
        show-overflow-tooltip
        prop="linkUrl"
        label="路径"
      ></el-table-column>
      <el-table-column show-overflow-tooltip label="图标">
        <template #default="{ row }">
          <span>
            <vab-icon
              v-if="row.iconUrl"
              :icon="['fas', row.iconUrl]"
            ></vab-icon>
          </span>
        </template>
      </el-table-column>

      <el-table-column
        show-overflow-tooltip
        prop="component"
        label="vue文件路径"
        width="500%"
      >
        <template #default="{ row }">
          <span>
            {{ row.component }}
          </span>
        </template>
      </el-table-column>

      <el-table-column show-overflow-tooltip label="是否显示">
        <template #default="{ row }">
          <span v-if="row.isPage">{{ row.isVisible ? '是' : '否' }}</span>
        </template>
      </el-table-column>
      <el-table-column show-overflow-tooltip label="是否缓存">
        <template #default="{ row }">
          <span v-if="row.isPage">{{ !row.noKeepAlive ? '是' : '否' }}</span>
        </template>
      </el-table-column>
      <el-table-column
        show-overflow-tooltip
        prop="sort"
        label="排序"
      ></el-table-column>
      <el-table-column show-overflow-tooltip label="操作" width="200">
        <template #default="{ row }">
          <el-button
            v-can="'menuManagement_edit'"
            style="font-size: 14px"
            type="text"
            @click="handleEdit(row)"
          >
            编辑
          </el-button>
          <el-button
            v-can="'menuManagement_del'"
            style="font-size: 14px"
            type="text"
            @click="handleDelete(row)"
          >
            删除
          </el-button>
          <el-button
            v-show="row.isPage"
            v-can="'menuManagement_assign_permission'"
            style="font-size: 14px"
            type="text"
            @click="handleButtons(row)"
          >
            分配权限
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <el-pagination
      background
      :current-page="queryForm.pageNo"
      :page-sizes="[5, 10, 20, 50]"
      :page-size="queryForm.pageSize"
      layout="total, sizes, prev, pager, next, jumper"
      :total="total"
      @size-change="handleSizeChange"
      @current-change="handleCurrentChange"
    ></el-pagination>
    <menu-edit ref="menu-edit" @fetch-data="fetchData"></menu-edit>
    <buttons-edit ref="buttons-edit" @fetch-data="fetchData"></buttons-edit>
  </div>
</template>

<script>
import { getList, remove } from '@/api/systems/menu'
// import { getTree, doDelete } from '@/api/menuManagement'
import MenuEdit from './components/MenuEdit'
import ButtonsEdit from './components/ButtonsEdit'

export default {
  name: 'menuManagement',
  components: { MenuEdit, ButtonsEdit },
  data() {
    return {
      data: [],
      defaultProps: {
        children: 'children',
        label: 'label',
      },
      list: [],
      listLoading: true,
      elementLoadingText: '正在加载...',
      total: 0,
      queryForm: {
        pageNo: 1,
        pageSize: 10,
        kw: '',
      },
      selecteds: [],
    }
  },
  async created() {
    this.fetchData()
  },
  computed: {
    height() {
      return this.$baseTableHeight()
    },
    selectedIds() {
      return this.selecteds.map((x) => x.id).join(',')
    },
  },
  methods: {
    handleButtons(row) {
      if (!row.id) return
      if (!row.isPage) return // 页面才有按钮
      this.$refs['buttons-edit'].show(row)
    },
    handleEdit(row, flag = false) {
      if (flag) this.$refs['menu-edit'].showEdit(row, 'look')
      else if (row.id) this.$refs['menu-edit'].showEdit(row, 'edit')
      else this.$refs['menu-edit'].showEdit({}, 'create')
    },
    handleDelete(row) {
      if (row.id) {
        this.$baseConfirm('你确定要删除当前项吗', null, async () => {
          const { message } = await remove(row.id)
          this.$baseMessage(message, 'success')
          this.fetchData()
        })
      }
    },
    async fetchData() {
      this.listLoading = true

      const { data, totalCount } = await getList(this.queryForm)
      this.list = data
      this.total = totalCount
      setTimeout(() => {
        this.listLoading = false
      }, 300)
    },
    handleNodeClick(data) {
      this.fetchData()
    },
    handleSizeChange(val) {
      this.queryForm.pageSize = val
      this.fetchData()
    },
    handleCurrentChange(val) {
      this.queryForm.pageNo = val
      this.fetchData()
    },
    queryData() {
      this.queryForm.pageNo = 1
      this.fetchData()
    },
    resetQuery() {
      this.queryForm = {
        pageNo: 1,
        pageSize: 10,
      }
    },
  },
}
</script>
