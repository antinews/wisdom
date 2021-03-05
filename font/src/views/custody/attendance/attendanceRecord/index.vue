<template>
  <div class="menuManagement-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="12">
        <el-form :inline="true" :model="queryForm" @submit.native.prevent>
          <el-form-item label="人员：">
            <el-select v-model="queryForm.userId" clearable>
              <el-option label="Administrators" value="1"></el-option>
              <el-option label="admin" value="2"></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="时间范围：">
            <date-filter ref="date-filter"></date-filter>
          </el-form-item>
          <el-form-item>
            <el-button icon="el-icon-search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-left-panel>
      <vab-query-form-right-panel :span="12">
        <el-button
          icon="el-icon-top-right"
          v-can="'attendanceRecord_export'"
          type="primary"
          @click="handleExprot"
        >
          导出
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
        prop="department"
        label="部门"
      ></el-table-column>
      <el-table-column show-overflow-tooltip label="人员">
        <template #default="{ row }">
          <span>
            {{ userName(row.attendanceRecordId) }}
          </span>
        </template>
      </el-table-column>
      <el-table-column
        show-overflow-tooltip
        prop="date"
        label="出勤时间"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        prop="totalTime"
        label="总时长(分钟)"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        prop="distance"
        label="总距离(公里)"
      ></el-table-column>
      <el-table-column show-overflow-tooltip label="操作" width="200">
        <template #default="{ row }">
          <div v-if="row.id != 1">
            <el-button
              type="text"
              style="font-size: 14px"
              @click="details(row)"
            >
              轨迹详情
            </el-button>
            <el-button
              type="text"
              style="font-size: 14px"
              v-can="'attendanceRecord_del'"
              @click="handleDelete(row)"
            >
              删除
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
    <trace ref="trace"></trace>
  </div>
</template>

<script>
import { getList, exportList, remove } from '_api/custody/attendance/record'
import Trace from './components/TraceMap'
import DateFilter from '_c/Filters/DateFilter'
export default {
  name: 'attendanceRecord',
  components: {
    Trace,
    DateFilter,
  },
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
    }
  },
  created() {
    this.fetchData()
  },
  computed: {
    height() {
      return this.$baseTableHeight()
    },
  },
  methods: {
    userName(str) {
      return str.substr(0, str.indexOf('@'))
    },
    details(row) {
      this.$refs['trace'].show(row)
    },
    handleDelete(row) {
      let str = row.attendanceRecordId
      if (str) {
        this.$baseConfirm('你确定要删除当前项吗', null, async () => {
          let param = str.substr(0, str.indexOf('@') + 10 + 1)
          const { message } = await remove(param)
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
      let date = this.$refs['date-filter']
      this.$set(this.queryForm, 'stime', date.stime)
      this.$set(this.queryForm, 'etime', date.etime)
      this.fetchData()
    },
    handleExprot() {
      exportList(this.queryForm, { responseType: 'blob' }).then((res) => {
        // console.log(res)
        let a = document.createElement('a')
        let blob = new Blob([res], {
          type: 'application/vnd.ms-excel',
        })
        let objectUrl = URL.createObjectURL(blob)

        a.setAttribute('href', objectUrl)
        a.setAttribute('download', '考勤记录.xlsx')
        a.click()
      })
    },
  },
}
</script>
<style scoped>
#trace {
  height: 80%;
  width: 100%;
}
</style>