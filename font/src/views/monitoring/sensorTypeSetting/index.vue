<template>
  <div class="devices-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="12">
        <el-form :inline="true" :model="queryForm" @submit.native.prevent>
          <el-form-item>
            <el-input
              v-model.trim="queryForm.kw"
              placeholder="请输入查询条件"
              clearable
            />
          </el-form-item>
          <el-form-item>
            <el-button icon="el-icon-search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-left-panel>
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
        label="传感器类型名称"
        prop="alias"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="类型编号"
        prop="kindCode"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="单位"
        prop="unit"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="有效小数点位数"
        prop="precision"
      ></el-table-column>
      <el-table-column label="状态">
        <template #default="{ row }">
          <el-switch
            v-model="row.isActived == 1"
            active-color="#13ce66"
            inactive-color="#ebeef5"
            @change="onStatusChange(row, $event)"
          ></el-switch>
        </template>
      </el-table-column>
      <el-table-column show-overflow-tooltip label="正常状态图标">
        <template #default="{ row }">
          <el-image
            style="width: 50px; height: 50px"
            v-if="row.normalIcon"
            :src="baseUrl + row.normalIcon"
            :preview-src-list="[baseUrl + row.normalIcon]"
          ></el-image>
        </template>
      </el-table-column>
      <el-table-column show-overflow-tooltip label="不可以状态图标">
        <template #default="{ row }">
          <el-image
            style="width: 50px; height: 50px"
            v-if="row.loseIcon"
            :src="baseUrl + row.loseIcon"
            :preview-src-list="[baseUrl + row.loseIcon]"
          ></el-image>
        </template>
      </el-table-column>
      <el-table-column show-overflow-tooltip label="一级报警">
        <template #default="{ row }">
          <el-image
            style="width: 50px; height: 50px"
            v-if="row.earlyWarningIcon"
            :src="baseUrl + row.earlyWarningIcon"
            :preview-src-list="[baseUrl + row.earlyWarningIcon]"
          ></el-image>
        </template>
      </el-table-column>
      <el-table-column show-overflow-tooltip label="二级报警">
        <template #default="{ row }">
          <el-image
            style="width: 50px; height: 50px"
            v-if="row.alarmIcon"
            :src="baseUrl + row.alarmIcon"
            :preview-src-list="[baseUrl + row.alarmIcon]"
          ></el-image>
        </template>
      </el-table-column>
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
      :current-page="queryForm.currentPage"
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
  getKindinfoList,
  getKindinfoEdit,
  getKindinfoDelete,
} from '@/api/sensorTypeSetting/sensorTypeSetting'
import Edit from './components/sensorTypeEdit'
import url from '@/libs/api.request'
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
        pageSize: 10,
        currentPage: 1,
        kw: '',
        projectId: 112,
      },
      baseUrl: '',
    }
  },
  async created() {
    this.fetchData()
    this.baseUrl = url.baseURL + '/'
  },
  mounted() {},
  computed: {
    height() {
      return this.$baseTableHeight()
    },
  },
  methods: {
    onStatusChange(el, e) {
      el.isActived = e ? 1 : 0
      const obj = {
        Id: el.id,
        KindName: el.kindName,
        ProjectId: el.projectId,
        Unit: el.unit,
        Precision: el.precision,
        MinValue: el.minValue,
        MaxValue: el.maxValue,
        TempId: el.tempId,
        Alias: el.alias,
        KindCode: el.kindCode,
        projectId: 112,
        isActived: e ? 1 : 0,
      }
      getKindinfoEdit(obj).then((res) => {
        if (res.code == 200) {
        } else {
        }
      })
    },
    handleEdit(row, flag = false) {
      if (flag) this.$refs['edit'].showEdit(row, 'look')
      else if (row.id) this.$refs['edit'].showEdit(row, 'edit')
      else this.$refs['edit'].showEdit({}, 'create')
    },
    handleDelete(row) {
      if (row.id) {
        this.$baseConfirm('你确定要删除当前项吗', null, async () => {
          const { message } = await getKindinfoDelete(row.id)
          this.$baseMessage(message, 'success')
          this.fetchData()
        })
      }
    },
    async fetchData() {
      this.listLoading = true
      const { data, totalCount } = await getKindinfoList(this.queryForm)
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
      this.queryForm.currentPage = val
      this.fetchData()
    },
    queryData() {
      this.queryForm.currentPage = 1
      this.fetchData()
    },
  },
}
</script>
