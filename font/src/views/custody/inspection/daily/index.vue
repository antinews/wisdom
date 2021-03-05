<template>
  <div class="menuManagement-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="18">
        <el-form
          :class="[toggle ? 'conditional' : '']"
          :inline="true"
          :model="queryForm"
          @submit.native.prevent
        >
          <a
            v-show="toggle"
            @click="toggle = !toggle"
            style="line-height: 43px"
          >
            展开∨
          </a>
          <a
            v-show="!toggle"
            @click="toggle = !toggle"
            style="line-height: 43px"
          >
            收起∧
          </a>
          <el-form-item label="区域：">
            <el-select
              v-model="queryForm.areaId"
              clearable
              @change="handleAreaChange"
            >
              <el-option
                v-for="(item, index) in sources.areaList"
                :key="index"
                :label="item.text"
                :value="item.id"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="桥梁：">
            <el-select v-model="queryForm.bridgeId" clearable>
              <el-option
                v-for="(item, index) in sources.bridgeListCopy"
                :key="index"
                :label="item.text"
                :value="item.id"
              ></el-option>
            </el-select>
          </el-form-item>

          <el-form-item label="起止时间：">
            <date-filter ref="date-filter"></date-filter>
          </el-form-item>
          <el-form-item label="部门：">
            <el-select
              v-model="queryForm.departName"
              placeholder="请选择上级菜单"
              ref="departTree"
              clearable
            >
              <el-option
                :label="'xx'"
                :value="0"
                style="
                  width: 100%;
                  height: 200px;
                  overflow: auto;
                  background-color: #fff;
                "
              >
                <depart-tree
                  ref="mytree"
                  @nodeClick="handleNodeClickEven"
                ></depart-tree>
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="巡检员：">
            <el-input
              v-model="queryForm.userName"
              clearable
              placeholder="模糊查询"
            ></el-input>
          </el-form-item>
          <el-form-item label="状态：">
            <el-select v-model="queryForm.statusId" clearable>
              <el-option
                v-for="(item, index) in sources.statusList"
                :key="index"
                :label="item.text"
                :value="item.id"
              ></el-option>
            </el-select>
          </el-form-item>

          <!-- <el-form-item>
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
          </el-form-item> -->
        </el-form>
      </vab-query-form-left-panel>
      <vab-query-form-right-panel :span="6">
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
      @selection-change="handleSelectionChange"
    >
      <el-table-column type="selection"></el-table-column>
      <ex-table-column
        prop="bridgeName"
        label="结构名称"
        :autoFit="true"
        :fitHeader="true"
      ></ex-table-column>
      <ex-table-column show-overflow-tooltip label="巡检日期">
        <template #default="{ row }">
          <span>{{ row.recordTime | timeFilter('yyyy/MM/dd') }}</span>
        </template>
      </ex-table-column>
      <ex-table-column
        show-overflow-tooltip
        prop="trueName"
        label="巡检员"
      ></ex-table-column>

      <ex-table-column
        show-overflow-tooltip
        prop="itemName"
        label="巡检项"
      ></ex-table-column>
      <ex-table-column
        show-overflow-tooltip
        prop="optionParentName"
        label="部位"
      ></ex-table-column>
      <ex-table-column show-overflow-tooltip label="病害">
        <template #default="{ row }">
          <a @click="handleItemClickEvent(row)">{{ row.itemName }}</a>
        </template>
      </ex-table-column>
      <ex-table-column
        show-overflow-tooltip
        prop="missionType"
        label="病害类型"
      ></ex-table-column>
      <ex-table-column show-overflow-tooltip label="病害等级">
        <template #default="{ row }">
          <span>
            {{
              row.missionLevel === '0'
                ? ''
                : row.missionLevel === '1'
                ? '一般'
                : row.missionLevel === '2'
                ? '严重'
                : '紧急'
            }}
          </span>
        </template>
      </ex-table-column>
      <ex-table-column
        show-overflow-tooltip
        prop="status"
        label="状态"
      ></ex-table-column>
      <ex-table-column show-overflow-tooltip label="病害图片">
        <template #default="{ row }">
          <el-image
            v-if="row.pic"
            style="width: 50px; height: 50px"
            :src="picPath(row)"
            :preview-src-list="[picPath(row)]"
          ></el-image>
          <span v-else>暂无图片</span>
        </template>
      </ex-table-column>
      <ex-table-column show-overflow-tooltip label="操作">
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
          </div>
        </template>
      </ex-table-column>
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
    <edit ref="edit" :areas="sources.areaList" @fetch-data="fetchData"></edit>
    <disease ref="disease" :title="disease"></disease>
  </div>
</template>

<script>
import {
  getList,
  remove,
  getDropDownListData,
} from '@/api/custody/inspection/daily'
import DateFilter from '_c/Filters/DateFilter'
import axios from '@/libs/api.request'
import Edit from './components/DiseaseEdit'
import DepartTree from '_c/Systems/DepartTree'
import Disease from './components/DiseaseDetails'
export default {
  name: 'dailyInspection',
  components: {
    DateFilter,
    Edit,
    DepartTree,
    Disease,
  },
  data() {
    return {
      list: [],
      listLoading: true,
      elementLoadingText: '正在加载...',
      toggle: true,
      multipleSelection: [],
      total: 0,
      disease: '',
      queryForm: {
        pageNo: 1,
        pageSize: 10,
      },
      sources: {
        statusList: [],
        areaList: [],
        bridgeList: [],
        bridgeListCopy: [],
      },
    }
  },
  created() {
    this.fetchData()
    this.init()
  },
  computed: {
    height() {
      return this.$baseTableHeight()
    },
    multipleSelectedIds() {
      return this.multipleSelection.map((x) => x.keyId)
    },
  },
  methods: {
    async init() {
      {
        const { data } = await getDropDownListData('area')
        this.sources.areaList = data
      }
      {
        const { data } = await getDropDownListData('status')
        this.sources.statusList = data
      }
      {
        const { data } = await getDropDownListData('bridge')
        this.sources.bridgeList = data
        this.sources.bridgeListCopy = data
      }
    },
    handleNodeClickEven(data) {
      this.$set(this.queryForm, 'departName', data.departmentName)
      this.$set(this.queryForm, 'departId', data.id)
      this.$refs.departTree.blur()
    },
    async handleAreaChange(v) {
      this.queryForm.bridgeId = null
      if (!v) this.sources.bridgeListCopy = this.sources.bridgeList.slice()
      else {
        const { data } = await getDropDownListData('bridge', v)
        this.sources.bridgeListCopy = data
      }
    },
    handleItemClickEvent(row) {
      this.disease = row.itemName
      if (row.keyId) this.$refs['disease'].show(row)
    },
    picPath(row) {
      let basePath = axios.baseURL + row.path
      let pics = row.pic.split('@')
      if (pics.length > 0) return basePath + pics[0]
      return ''
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
      let date = this.$refs['date-filter']
      this.$set(this.queryForm, 'stime', date.stime)
      this.$set(this.queryForm, 'etime', date.etime)
      this.fetchData()
    },
    handleEdit(row) {
      if (row.keyId) {
        this.$refs['edit'].showEdit(row)
      } else {
        this.$refs['edit'].showEdit()
      }
    },
    async handleDelete(row) {
      if (row.keyId) {
        this.$baseConfirm('你确定要删除当前项吗', null, async () => {
          const { message } = await remove(row.keyId)
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
    resetQuery() {
      this.queryForm = {
        pageNo: 1,
        pageSize: 10,
      }
    },
  },
}
</script>
<style scoped>
.conditional {
  max-height: 46px !important;
  overflow-y: hidden;
}
.el-table .cell {
  white-space: nowrap;
  width: fit-content;
}
</style>