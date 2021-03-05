<template>
  <div class="edgeDevices-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="20">
        <el-form :inline="true" :model="queryForm" @submit.native.prevent>
          <el-form-item label="区域：">
            <el-select v-model="queryForm.area" style="width: 150px">
              <el-option
                v-for="item in areas"
                :label="item.areaName"
                :value="item.keyId"
                :key="item.keyId"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="类型：">
            <el-cascader
              v-model="cascader_type"
              :options="types"
              :props="{ expandTrigger: 'hover' }"
              @change="handleChange"
              style="width: 150px"
            ></el-cascader>
          </el-form-item>
          <el-form-item label="名称：">
            <el-input
              v-model.trim="queryForm.kw"
              placeholder="请输入查询条件"
              clearable
              style="width: 150px"
            />
          </el-form-item>
          <el-form-item>
            <el-button icon="el-icon-search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
          <el-form-item>
            <el-button icon="el-icon-s-tools" type="primary" @click="reset">
              重置
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-left-panel>
      <vab-query-form-right-panel :span="4">
        <el-button
          v-can="'menu_create'"
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
      border
      ref="menuTable"
    >
      <el-table-column
        show-overflow-tooltip
        label="结构物区域"
        prop="cityArea"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="结构物名称"
        prop="bridgeName"
      ></el-table-column>

      <el-table-column
        show-overflow-tooltip
        label="结构物类型"
        prop="bridgeType"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="结构物位置"
        prop="gpsDes"
      ></el-table-column>
      <el-table-column show-overflow-tooltip label="创建时间">
        <template #default="{ row }">
          {{ row.createdTime ? row.createdTime.replace('T', ' ') : '' }}
        </template>
      </el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="描述"
        prop="remark"
      ></el-table-column>

      <el-table-column show-overflow-tooltip label="操作" width="200">
        <template #default="{ row }">
          <el-button
            v-can="'menu_edit'"
            style="font-size: 14px"
            type="text"
            @click="handleEdit(row)"
          >
            编辑
          </el-button>
          <el-button
            v-can="'menu_del'"
            style="font-size: 14px"
            type="text"
            @click="handleDelete(row)"
          >
            删除
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <!-- </el-col>
    </el-row> -->
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
import {
  getList,
  remove,
  findStructuryAreas,
  findStructuryTypes,
} from '@/api/monitoring/edgeDevices'
import Edit from './components/edgeDevicesEdit'
export default {
  name: 'edgeDevices',
  components: { Edit },
  data() {
    return {
      list: [],
      listLoading: true,
      elementLoadingText: '正在加载...',
      total: 0,
      queryForm: {
        currentPage: 1,
        pageSize: 10,
        kw: '',
        area: 0,
        type: 0,
      },
      areas: [],
      types: [],
      cascader_type: [],
    }
  },
  async created() {
    this.fetchData()
    const { data } = await findStructuryAreas()
    this.areas = [{ keyId: 0, areaName: '全部区域' }].concat(data)
    findStructuryTypes().then((res) => {
      this.types = res.data
    })
  },
  computed: {
    height() {
      return this.$baseTableHeight()
    },
  },
  methods: {
    rowClick(row) {
      this.handleEdit(row, true)
    },
    handleChange(value) {
      this.queryForm.type = value[1]
    },
    handleEdit(row, flag = false) {
      if (flag) this.$refs['edit'].showEdit(row, 'look')
      else if (row.keyId) this.$refs['edit'].showEdit(row, 'edit')
      else this.$refs['edit'].showEdit({}, 'create')
    },
    handleDelete(row) {
      if (row.keyId) {
        this.$baseConfirm('你确定要删除当前项吗', null, async () => {
          const { message } = await remove(row.keyId)
          this.$baseMessage(message, 'success')
          this.fetchData()
        })
      }
    },
    reset() {
      this.queryForm.area = 0
      this.queryForm.type = 0
      this.cascader_type = []
      this.queryForm.kw = ''
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
  },
}
</script>
