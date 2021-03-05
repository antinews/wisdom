<template>
  <div class="table-container">
    <el-row>
      <!-- 机构树 -->
      <el-col :span="7" class="table_container_left">
        <el-divider content-position="left">机构树</el-divider>
        <el-tree
          ref="demoTree"
          :data='TreeList'
          :props="defaultProps"
          @node-click="handleNodeClick"
        ></el-tree>
      </el-col>
      <!-- 表格 -->
      <el-col :span="16" :offset="1">
        <vab-query-form>
          <vab-query-form-left-panel>
            <el-button icon="el-icon-plus" type="primary" @click="handleAdd">
              添加
            </el-button>
            <el-button
              icon="el-icon-delete"
              type="danger"
              @click="handleDelete"
            >
              删除
            </el-button>
          </vab-query-form-left-panel>
          <vab-query-form-right-panel>
            <el-form
              ref="form"
              :model="Departement"
              :inline="true"
              @submit.native.prevent
            >
              <el-form-item>
                <el-input
                  v-model="Departement.DepartementName"
                  placeholder="标题"
                />
              </el-form-item>
              <el-form-item>
                <el-button
                  icon="el-icon-search"
                  type="primary"
                  native-type="submit"
                  @click="handleQuery"
                >
                  查询
                </el-button>
              </el-form-item>
            </el-form>
          </vab-query-form-right-panel>
        </vab-query-form>

        <el-table
          ref="tableSort"
          v-loading="listLoading"
          :data="list"
          :element-loading-text="elementLoadingText"
          :height="height"
          @selection-change="setSelectRows"
        >
          <el-table-column
            show-overflow-tooltip
            type="selection"
            width="55"
          ></el-table-column>
          <el-table-column show-overflow-tooltip label="序号" width="95">
            <template #default="scope">
              {{ scope.$index + 1 }}
            </template>
          </el-table-column>
          <el-table-column
            show-overflow-tooltip
            prop="departmentName"
            label="机构名称"
          ></el-table-column>
          <el-table-column
            show-overflow-tooltip
            label="排序"
            prop="sortnum"
          ></el-table-column>

          <el-table-column
            show-overflow-tooltip
            label="创建时间"
            prop="createdTime"
            sortable
            :formatter="dateFormat"
          ></el-table-column>
          <el-table-column show-overflow-tooltip label="操作" width="180px">
            <template #default="{ row }">
              <el-button type="text" @click="handleEdit(row)">编辑</el-button>
              <el-button type="text" @click="handleDelete(row)">删除</el-button>
            </template>
          </el-table-column>
        </el-table>
        <el-pagination
          :total="TabList.length"
          :current-page.sync="indexPage"
          @current-change="handleCurrentChange"
          @size-change="handleSizeChange"
          :layout="layout"
        ></el-pagination>
      </el-col>
    </el-row>
    <echartsx ref="tab" @fetch-data="fetchDatas"></echartsx>
  </div>
</template>

<script>
import {
  GetDepartement,
  GetTreeList,
  GetDepartementList,
  DelDepartement,
  DelDepartementList,
  GetDepartementByName,
} from '@/api/systems/Department'
import tab from './component/tab'
export default {
  name: 'ComprehensiveTable',
  components: {
    echartsx: tab,
  },
  filters: {
    statusFilter(status) {
      const statusMap = {
        published: 'success',
        draft: 'gray',
        deleted: 'danger',
      }
      return statusMap[status]
    },
  },
  data() {
    return {
      data2: [],
      defaultProps: {
        children: "children",
        label: "departmentName",
      },
      indexPage: 1,
      filterText: null, //过滤
      list: [],
      TabList: [],
      TreeList:[],
      listLoading: true,
      layout: 'total, sizes, prev, pager, next, jumper',
      icon: null,
      background: true,
      selectRows: '',
      elementLoadingText: '正在加载...',
      Departement: {
        Id: null,
        DepartementName: null,
        ParentId: null,
        Sortnum: null,
      },
    }
  },

  computed: {
    height() {
      return this.$baseTableHeight()
    },
  },
  watch: {
    filterText(val) {
      this.$refs.demoTree.filter(val)
    },
  },
  created() {
    this.fetchDatas();
  },
  methods: {
    async handleNodeClick(data) {
      this.listLoading=true;
      let NodeDate = await this.GetNode(data.id)
      this.list = []
      if (NodeDate) {
        this.list = NodeDate
      }
      console.log(data);
      this.list.push(data);
      this.listLoading=false;
    },

    //通过ID读取子部门
    async GetNode(id) {
      const res = await GetDepartementList(id);
      return res.data
    },

    //选中的数组
    setSelectRows(val) {
      this.selectRows = val
    },
    handleAdd() {
      this.$refs['tab'].showEdit()
    },
    handleEdit(row) {
      this.$refs['tab'].showEdit(row)
    },
    handleDelete(row) {
      if (row.id) {
        this.$baseConfirm('你确定要删除当前项吗', null, async () => {
          const message = await DelDepartement({ id: row.id })

          if (message.status == 200) {
            this.$baseMessage(message.info, 'success')
          } else {
            this.$baseMessage(message.info, 'error')
          }
          this.fetchDatas()
        })
      } else {
        if (this.selectRows.length > 0) {
          const ids = this.selectRows.map((item) => item.id).join()
          this.$baseConfirm('你确定要删除选中项吗', null, async () => {
            const message = await DelDepartementList({ IDs: ids })
            if (message.status == 200) {
              this.$baseMessage(message.info, 'success')
            } else {
              this.$baseMessage(message.info, 'error')
            }
            this.fetchDatas()
          })
        } else {
          this.$baseMessage('未选中任何行', 'error')
          return false
        }
      }
    },
    handleCurrentChange() {
      this.list = this.TabList.slice(
        (this.indexPage - 1) * 10,
        this.indexPage * 10
      )
    },
    handleSizeChange(val){
      this.list = this.TabList.slice(
        (this.indexPage - 1) * val,
        this.indexPage * val
      )
    },
    //查询树状图数据
    async GetTree(){
      const  res=await  GetTreeList();
      this.TreeList=res.data;
    },
    //标题搜索
    async handleQuery() {
      if (this.Departement.DepartementName != null) {
        const res = await GetDepartementByName(this.Departement.DepartementName)
        this.TabList = res.data
        this.list = this.TabList.slice(
          (this.indexPage - 1) * 10,
          this.indexPage * 10
        )
      } else {
        this.fetchData()
      }
    },
    //查询表格數據
    async fetchData() {
      const res = await GetDepartement()
      if (res.status == '200') {
        this.TabList = res.data
        this.list = this.TabList.slice(
          (this.indexPage - 1) * 10,
          this.indexPage * 10
        )
      } else {
        this.$baseMessage(res.info, 'error')
      }
      this.listLoading = true

      setTimeout(() => {
        this.listLoading = false
      }, 500)
    },
    //时间处理
    dateFormat(row, column) {
      let date = row[column.property]
      let nums = new Date(date)
      let time =
        nums.getFullYear() +
        '-' +
        (nums.getMonth() + 1) +
        '-' +
        nums.getDate() +
        ' ' +
        nums.getHours() +
        ':' +
        nums.getMinutes() +
        ':' +
        nums.getSeconds()
      return time
    },
    fetchDatas() {
      this.GetTree();
      this.fetchData()
    },
  },
}
</script>
<style  scoped>
.table_container_left::before {
  margin-left: 100px;
}
.el-tree >>> .is-focusable >>> .el-icon-caret-right {
  display: none;
}
</style>
