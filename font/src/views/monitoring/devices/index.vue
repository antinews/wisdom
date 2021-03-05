<template>
  <div class="devices-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="22">
        <el-form :inline="true" :model="queryForm" @submit.native.prevent>
          <el-form-item label="区域：">
            <el-select v-model="queryForm.areaId" style="width: 150px">
              <el-option
                v-for="item in areas"
                :label="item.areaName"
                :value="item.keyId"
                :key="item.keyId"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="所属结构物：">
            <el-select v-model="queryForm.projectId" style="width: 150px">
              <el-option
                v-for="item in bridges"
                :label="item.name"
                :value="item.id"
                :key="item.id"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="测站名称：">
            <el-input
              v-model.trim="queryForm.DeviceName"
              placeholder="请输入查询条件"
              clearable
              style="width: 150px"
            />
          </el-form-item>
          <el-form-item label="上报地址：">
            <el-input
              v-model.trim="queryForm.ReportAddr"
              placeholder="请输入查询条件"
              clearable
              style="width: 150px"
            />
          </el-form-item>
          <el-form-item label="上报端口：">
            <el-input
              v-model.trim="queryForm.ReportPort"
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
      <vab-query-form-right-panel :span="2">
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
        label="所属区域"
        prop="area"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="所属结构物"
        prop="projectName"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="测站名称"
        prop="deviceName"
      ></el-table-column>
      <!-- <el-table-column label="是否启用">
        <template #default="{ row }">
          <el-switch
            v-model="row.isActived == 1"
            active-color="#13ce66"
            inactive-color="#ebeef5"
            @change="onStatusChange(row, $event)"
          ></el-switch>
        </template>
      </el-table-column> -->
      <!-- <el-table-column
        show-overflow-tooltip
        label="测站地址"
        prop="deviceAddr"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="测站端口"
        prop="devicePort"
      ></el-table-column> -->
      <!-- <el-table-column show-overflow-tooltip label="创建时间">
        <template #default="{ row }">
          {{ row.createDate.replace('T', ' ') }}
        </template>
      </el-table-column> -->
      <el-table-column
        show-overflow-tooltip
        label="上报地址"
        prop="reportAddr"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="上报端口"
        prop="reportPort"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="采样频率"
        prop="collectInterval"
      ></el-table-column>
      <el-table-column show-overflow-tooltip label="采样模式">
        <template #default="{ row }">
          {{ row.uploadMethod == 'polling' ? '定时采集' : '自动上传' }}
        </template>
      </el-table-column>
      <el-table-column show-overflow-tooltip label="测站照片">
        <template #default="{ row }">
          <el-image
            style="width: 50px; height: 50px"
            v-if="row.pic"
            :src="baseUrl + row.pic"
            :preview-src-list="[baseUrl + row.pic]"
          ></el-image>
        </template>
      </el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="解算方式"
        prop="codecsName"
      ></el-table-column>
      <!-- <el-table-column show-overflow-tooltip label="最后上线时间">
        <template #default="{ row }">
          {{ row.lastCollectTime ? row.lastCollectTime.replace('T', ' ') : '' }}
        </template>
      </el-table-column> -->

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
  getDeviceList,
  deleteDevice,
  BatchDevice,
} from '@/api/monitoring/devices'
import { findStructuryAreas, getAll } from '@/api/monitoring/edgeDevices'
import Edit from './components/devicesEdit'
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
        currentPage: 1,
        pageSize: 10,
        kw: '',
        kwType: 1, //kw类型，1测站/默认  2测站地址3测站端口4目标地址5目标端口
        isActived: -1,
        areaId: 0,
        projectId: 0,
      },
      kwType: [
        { key: 1, value: '测站' },
        { key: 2, value: '测站地址' },
        { key: 3, value: '测站端口' },
        { key: 4, value: '目标地址' },
        { key: 5, value: '目标端口' },
      ],
      baseUrl: '',
      areas: [],
      bridges: [],
    }
  },
  async created() {
    this.fetchData()
    this.baseUrl = url.baseURL + '/'
  },
  computed: {
    height() {
      return this.$baseTableHeight()
    },
  },
  mounted() {
    findStructuryAreas().then((res) => {
      this.areas = [{ keyId: 0, areaName: '全部区域' }].concat(res.data)
    })
    getAll().then((res) => {
      this.bridges = [{ id: 0, name: '全部结构物' }].concat(res.data)
    })
  },
  methods: {
    reset() {
      this.queryForm.areaId = 0
      this.queryForm.projectId = 0
      this.queryForm.DeviceName = ''
      this.queryForm.ReportAddr = ''
      this.queryForm.ReportPort = ''
    },
    onStatusChange(row, e) {
      row.isActived = e ? 1 : 0
      BatchDevice({
        command: row.isActived == 1 ? 'enable' : 'disable',
        ids: row.id,
      }).then((res) => {
        if (res.data.code == 200) {
        } else {
          //this.$Message.info(res.data.message)
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
          const { message } = await deleteDevice(row.id)
          this.$baseMessage(message, 'success')
          this.fetchData()
        })
      }
    },
    async fetchData() {
      this.listLoading = true
      const { data, totalCount } = await getDeviceList(this.queryForm)
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
