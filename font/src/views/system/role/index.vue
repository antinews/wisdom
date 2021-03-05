<template>
  <div class="menuManagement-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="12">
        <el-form :inline="true" :model="queryForm" @submit.native.prevent>
          <el-form-item label="角色名称：">
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
          icon="el-icon-plus"
          v-can="'roleManagement_add'"
          type="primary"
          @click="handleEdit"
        >
          新增
        </el-button>
        <el-button
          v-can="'roleManagement_del'"
          icon="el-icon-delete"
          type="danger"
          plain
          @click="handleDelete"
        >
          删除
        </el-button>
        <!-- <el-button icon="el-icon-apple" type="primary" @click="handleEdit">
          按钮组
        </el-button> -->
      </vab-query-form-right-panel>
    </vab-query-form>
    <el-table
      v-loading="listLoading"
      :height="height"
      :data="list"
      :element-loading-text="elementLoadingText"
      border
      ref="menuTable"
      @selection-change="handleSelectionChange"
    >
      <el-table-column type="selection"></el-table-column>
      <el-table-column
        show-overflow-tooltip
        prop="roleName"
        label="角色名称"
      ></el-table-column>
      <!-- <el-table-column
        show-overflow-tooltip
        prop="sortNum"
        label="排序"
      ></el-table-column> -->
      <el-table-column
        show-overflow-tooltip
        prop="remark"
        label="描述"
      ></el-table-column>
      <!-- <el-table-column
        show-overflow-tooltip
        prop="isDefault"
        label="是否默认"
      ></el-table-column> -->
      <el-table-column
        show-overflow-tooltip
        prop="createdTime"
        label="创建时间"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        prop="createdUser"
        label="创建用户"
      ></el-table-column>
      <el-table-column show-overflow-tooltip label="操作" width="200">
        <template #default="{ row }">
          <div v-if="row.id != 1">
            <el-button
              v-can="'roleManagement_edit'"
              type="text"
              style="font-size: 14px"
              @click="handleEdit(row)"
            >
              编辑
            </el-button>
            <el-button
              v-can="'roleManagement_del'"
              type="text"
              style="font-size: 14px"
              @click="handleDelete(row)"
            >
              删除
            </el-button>
            <el-button
              v-can="'roleManagement_assign_permission'"
              type="text"
              style="font-size: 14px"
              @click="assignPermission(row)"
            >
              分配权限
            </el-button>
          </div>
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
    <edit ref="edit" @fetch-data="fetchData"></edit>
    <permission-tree ref="tree"></permission-tree>
  </div>
</template>

<script>
import { getList, remove } from '@/api/systems/role'
import Edit from './components/RoleEdit'
import PermissionTree from './components/PermissionTree'
export default {
  name: 'roleManagement',
  components: { Edit, PermissionTree },
  data() {
    return {
      list: [],
      listLoading: true,
      elementLoadingText: '正在加载...',
      total: 0,
      queryForm: {
        pageNo: 1,
        pageSize: 10,
        kw: '',
      },
      multipleSelection: [],
    }
  },
  created() {
    this.fetchData()
  },
  computed: {
    height() {
      return this.$baseTableHeight()
    },
    multipleSelectedIds() {
      return this.multipleSelection.map((x) => x.id)
    },
  },
  methods: {
    async fetchData() {
      this.listLoading = true

      const { data, totalCount } = await getList(this.queryForm)
      this.list = data
      this.total = totalCount
      setTimeout(() => {
        this.listLoading = false
      }, 300)
    },
    handleSelectionChange(val) {
      this.multipleSelection = val
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
    handleEdit(row) {
      if (row.id) {
        this.$refs['edit'].showEdit(row)
      } else {
        this.$refs['edit'].showEdit()
      }
    },
    async handleDelete(row) {
      if (row.id) {
        this.$baseConfirm('你确定要删除当前项吗', null, async () => {
          const { message } = await remove(row.id)
          this.$baseMessage(message, 'success')
          this.fetchData()
        })
        return
      }
      if (!this.multipleSelectedIds.length) {
        this.$baseMessage('至少选中一条数据')
        return
      }
      const { message } = await remove(this.multipleSelectedIds.join(','))
      this.$baseMessage(message, 'success')
      this.fetchData()
    },
    assignPermission(row) {
      if (row.id) {
        this.$refs['tree'].show(row)
      }
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
