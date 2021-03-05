<template>
  <div class="table-container">
    <el-row>
      <el-col :span="7">
        <div class="div_block">
          <el-button icon="el-icon-plus" type="primary" @click="handleAddDic">
            添加
          </el-button>
          <el-button icon="el-icon-delete" type="primary" @click="handleSetDic">
            编辑
          </el-button>
          <el-button

            icon="el-icon-delete"
            type="danger"
            @click="handleDeleteDic"
          >
            删除
          </el-button>
        </div>
        <el-tree
          ref="demoTree"
          lazy
          :data="loadNodeData"
          :props="DiccategoryProps"
          :icon-class="icon"
          @node-click="handleNodeClick"
        ></el-tree>
      </el-col>
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
        </vab-query-form>

        <el-table
          ref="tableSort"
          v-loading="listLoading"
          :data="list"
          :element-loading-text="elementLoadingText"
          :height="height"

        >
          <!--          @selection-change="setSelectRows"-->
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
            prop="title"
            label="字典名称"
          ></el-table-column>
          <el-table-column
            show-overflow-tooltip
            label="排序"
            prop="sortNum"
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
          :layout="layout"
        ></el-pagination>
      </el-col>
    </el-row>
    <diccategory-Tab ref="diccategorytab" @fetchdata="formRsp"></diccategory-Tab>
    <dicsTab ref="dicsTab" @fetch-data="TabRsp"></dicsTab>
  </div>
</template>

<script>
import diccategoryTab from './component/DiccategoryTab'
import dicsTab from './component/DicsTab'
import { GetDiccategorys,DelCategory } from '@/api/systems/diccategorys'
import { GetDicsListByID, DelDicsById } from '@/api/systems/Dics'
export default {
  data: () => ({
    icon: null,
    TabList: [],
    background: true,
    selectRows: '',
    elementLoadingText: '正在加载...',
    layout: 'total, sizes, prev, pager, next, jumper',
    indexPage: 1,
    DiccategoryProps: {
      label: 'title',

    },
    loadNodeData:[],
    list: [],
    TypeId: null,
    dicsData: null,
    Departement: {
      Id: null,
      DepartementName: null,
      ParentId: null,
      Sortnum: null,
    },
    listLoading:false
  }),
  components: {
    diccategoryTab: diccategoryTab,
    dicsTab: dicsTab,
  },
  computed: {
    height() {
      return this.$baseTableHeight()
    },
  },
  mounted() {
    this.loadNode();
    },
  methods: {
    filterNode(value, data) {
      if (!value) return true
      return data.name.indexOf(value) !== -1
    },
    async loadNode() {

        const resDate = await this.GetNode()
        this.loadNodeData=resDate;

    },
    async GetNode() {
      const res = await GetDiccategorys()
      console.log(res)
      return res.data
    },
    //点计
    async handleNodeClick(row) {
      this.dicsData = row
      this.TypeId = row.id
      this.listLoading=true;
      const res = await GetDicsListByID(row.id)
      if (res.status == '200') {
        this.TabList = res.data
        this.list = this.TabList.slice(0, 10)
      }
      this.listLoading=false;
    },

    //打开添加字典类型
    handleAddDic() {
      this.$refs['diccategorytab'].showEdit()
    },
    formRsp(){
      this.loadNode();
    },
    //修改字典类型
    handleSetDic() {
      if (this.dicsData == null) {
        this.$baseMessage('请先选择字典类型')
      }
      this.$refs['diccategorytab'].showEdit(this.dicsData)
    },

    async handleDeleteDic() {
      if (this.dicsData == null) {
        this.$baseMessage('请先选择字典类型')
      }
      const res = await DelCategory(this.TypeId)

      this.$baseMessage("删除成功", 'success')
      this.loadNode();
    },
    handleAdd() {
      if (this.dicsData == null) {
        this.$baseMessage('请先选择字典类型')
      }else{
        //获取字典类型Id
        console.log(this.dicsData);
        this.$refs['dicsTab'].showEdit(0,this.dicsData.id);
      }

    },
    //删除字典数据
    async handleDelete(row) {
      const res = await DelDicsById(row.id)
      this.$baseMessage(res.info, 'success');
      this.ByDicsListID(this.dicsData);
    },
    //根据选择的ID进行查找
    async ByDicsListID(row){
      this.listLoading=true;
      const res = await GetDicsListByID(row.id)
      if (res.status == '200') {
        this.TabList = res.data
        console.log(res.data);
        this.list = this.TabList.slice(0, 10)
      }
      this.listLoading=false;
    },
    //字典数据添加成功的回调事件
    TabRsp(){
      this.ByDicsListID(this.dicsData);
    },
    handleEdit(row) {
      this.$refs['dicsTab'].showEdit(row)
    },
    handleCurrentChange() {
      this.list = this.TabList.slice(
        (this.indexPage - 1) * 10,
        this.indexPage * 10
      )
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
  },
}
</script>

<style lang="scss" scoped>
.table_container_left {
  margin-left: 100px;
  .div_block {
    display: block;
    margin: 0px auto;
  }
}
</style>
