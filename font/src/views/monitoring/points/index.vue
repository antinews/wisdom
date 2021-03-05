<template>
  <div class="devices-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="24">
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
          <el-form-item label="所属测站：">
            <el-select v-model="queryForm.deviceId" style="width: 150px">
              <el-option
                v-for="item in sedeviceList"
                :label="item.deviceName"
                :value="item.id"
                :key="item.id"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="类型：">
            <el-select
              v-model="queryForm.MonitoringKindId"
              style="width: 150px"
            >
              <el-option
                v-for="item in sekindinfoList"
                :label="item.kindName"
                :value="item.id"
                :key="item.id"
              ></el-option>
            </el-select>
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
      <!-- <vab-query-form-right-panel :span="12">
        <el-button
          v-can="'menu_create'"
          icon="el-icon-plus"
          type="primary"
          @click="handleEdit"
        >
          新增
        </el-button>
      </vab-query-form-right-panel> -->
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
        label="区域"
        prop="area"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="桥梁"
        prop="projectName"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="测站"
        prop="deviceName"
      ></el-table-column>
      <el-table-column show-overflow-tooltip label="传感器类型">
        <template #default="{ row }">
          {{ row.monitoringKind.kindName }}
        </template>
      </el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="编号"
        prop="pointCode"
      ></el-table-column>
      <el-table-column
        show-overflow-tooltip
        label="通道号"
        prop="chanel"
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
      <el-table-column show-overflow-tooltip label="合理值范围">
        <template #default="{ row }">
          {{ row.minValue }}~{{ row.maxValue }}
        </template>
      </el-table-column>
      <el-table-column show-overflow-tooltip label="一级阈值上下限">
        <template #default="{ row }">
          {{ row.range1Min }}~{{ row.range1Max }}
        </template>
      </el-table-column>
      <el-table-column show-overflow-tooltip label="合理值范围">
        <template #default="{ row }">
          {{ row.range2Min }}~{{ row.range2Max }}
        </template>
      </el-table-column>

      <!-- <el-table-column show-overflow-tooltip label="创建时间">
        <template #default="{ row }">
          {{ row.createDate.replace('T', ' ') }}
        </template>
      </el-table-column> -->
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
import { getPointList, deletePoint, BatchPoint } from '@/api/monitoring/point'
import { getDeviceList } from '@/api/monitoring/devices'
import { getKindinfoList } from '@/api/sensorTypeSetting/sensorTypeSetting'
import { findStructuryAreas, getAll } from '@/api/monitoring/edgeDevices'
import Edit from './components/pointEdit'
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
        totalCount: 0,
        pageSize: 10,
        currentPage: 1,
        kw: '',
        isActived: -1,
        deviceId: 0,
        MonitoringKindId: 0,
        areaId: 0,
        projectId: 0,
      },
      deviceList: '',
      kindinfoList: '',
      areas: [],
      bridges: [],
      baseUrl: '',
    }
  },
  async created() {
    this.fetchData()
    this.baseUrl = url.baseURL + '/'
  },
  mounted() {
    getDeviceList({
      pageSize: 999,
      currentPage: 1,
      kw: '',
      isActived: -1,
      ProjectId: this.queryForm.projectId,
    }).then((res) => {
      if (res.code == 200) {
        this.deviceList = res.data
      } else {
      }
    })
    getKindinfoList({
      pageSize: 9999,
      currentPage: 1,
      projectId: this.queryForm.projectId,
    }).then((res) => {
      if (res.code == 200) {
        this.kindinfoList = res.data
      } else {
      }
    })
    findStructuryAreas().then((res) => {
      this.areas = [{ keyId: 0, areaName: '全部区域' }].concat(res.data)
    })
    getAll().then((res) => {
      this.bridges = [{ id: 0, name: '全部结构物' }].concat(res.data)
    })
  },
  computed: {
    height() {
      return this.$baseTableHeight()
    },
    sedeviceList() {
      return [{ id: 0, deviceName: '全部测站' }].concat(this.deviceList)
    },
    sekindinfoList() {
      return [{ id: 0, kindName: '全部传感器' }].concat(this.kindinfoList)
    },
  },
  methods: {
    reset() {
      this.queryForm.areaId = 0
      this.queryForm.projectId = 0
      this.queryForm.deviceId = 0
      this.queryForm.MonitoringKindId = 0
      this.queryForm.kw = ''
    },
    onStatusChange(row, e) {
      row.isActived = e ? 1 : 0
      BatchPoint({
        command: row.isActived == 1 ? 'enable' : 'disable',
        ids: row.id,
      }).then((res) => {
        if (res.data.code == 200) {
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
          const { message } = await deletePoint(row.id)
          this.$baseMessage(message, 'success')
          this.fetchData()
        })
      }
    },
    async fetchData() {
      this.listLoading = true
      const { data, totalCount } = await getPointList(this.queryForm)
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
