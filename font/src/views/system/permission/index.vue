<template>
  <div class="menuManagement-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="18">
        <el-form ref="formxx" :inline="true" :model="queryForm">
          <el-form-item label="权限名称：">
            <el-input
              v-model.trim="queryForm.permissionName"
              placeholder="模糊查询"
              clearable
            />
          </el-form-item>
          <el-form-item label="关联菜单：">
            <el-select
              v-model="queryForm.menuName"
              ref="menuTree"
              clearable
              @clear="handleClearMenu"
            >
              <el-option
                :label="queryForm.menuName"
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

          <el-form-item label="类型：">
            <el-select v-model="queryForm.type" clearable>
              <el-option label="菜单" :value="0"></el-option>
              <el-option label="按钮" :value="1"></el-option>
            </el-select>
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
      <vab-query-form-right-panel :span="6">
        <el-button
          v-can="'permission_add'"
          icon="el-icon-plus"
          type="primary"
          @click="handleEdit"
        >
          新增
        </el-button>
        <el-button
          v-can="'permission_del'"
          icon="el-icon-delete"
          type="danger"
          plain
          @click="handleDelete"
        >
          删除
        </el-button>
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
        prop="name"
        label="权限名称"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        prop="actionCode"
        label="权限码"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        prop="menuName"
        label="关联菜单"
      ></el-table-column>
      <el-table-column show-overflow-tooltip label="类型">
        <template #default="{ row }">
          <el-tag style="font-size: 14px" :type="!row.type ? 'success' : ''">
            {{ !row.type ? '按钮' : '页面' }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column
        show-overflow-tooltip
        prop="createdTime"
        label="创建时间"
      ></el-table-column>
      <!-- <el-table-column
        show-overflow-tooltip
        prop="createdUser"
        label="创建用户"
      ></el-table-column> -->
      <el-table-column show-overflow-tooltip label="操作" width="200">
        <template #default="{ row }">
          <el-button
            v-can="'permission_edit'"
            style="font-size: 14px"
            type="text"
            @click="handleEdit(row)"
          >
            编辑
          </el-button>
          <el-button
            v-can="'permission_del'"
            style="font-size: 14px"
            type="text"
            @click="handleDelete(row)"
          >
            删除
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
    <edit ref="edit" @fetch-data="fetchData"></edit>
  </div>
</template>

<script>
import { getList, remove } from '@/api/systems/permission'
import Edit from './components/PermissionEdit'
import MenuTree from '_c/Systems/MenuTree'
export default {
  name: 'permission',
  components: { Edit, MenuTree },
  data() {
    return {
      list: [],
      listLoading: true,
      elementLoadingText: '正在加载...',
      total: 0,
      queryForm: {
        pageNo: 1,
        pageSize: 10,
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
    handleSelectionChange(val) {
      this.multipleSelection = val
    },
    handleClearMenu() {
      this.$set(this.queryForm, 'menuId', '')
    },
    handleNodeClickEven(data) {
      this.$set(this.queryForm, 'menuId', data.id)
      this.$set(this.queryForm, 'menuName', data.label)
      this.$refs.menuTree.blur()
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
  },
}
</script>
