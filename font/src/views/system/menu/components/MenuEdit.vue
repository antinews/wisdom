<template>
  <el-dialog
    :title="title"
    :visible.sync="dialogFormVisible"
    width="45%"
    @close="close"
  >
    <!-- <el-divider content-position="left">
      这里就不具体写了，请自行完善
    </el-divider> -->
    <el-form ref="form" :model="form" :rules="rules" label-width="35%">
      <div class="Modification">
        <div>
          <el-form-item label="上级菜单：" prop="parentName">
            <el-select
              v-model="form.parentName"
              placeholder="请选择上级菜单"
              ref="menuTree"
            >
              <el-option
                :label="form.parentId"
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
        </div>
        <div>
          <el-form-item label="路由：" prop="linkUrl">
            <el-input
              v-model="form.linkUrl"
              :maxlength="32"
              placeholder="本段路由即可"
            ></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="菜单名称：" prop="navTitle">
            <el-input
              :maxlength="12"
              v-model="form.navTitle"
              placeholder="菜单栏显示的中文"
            ></el-input>
          </el-form-item>
        </div>

        <div>
          <el-form-item label="是页面：" prop="isPage">
            <el-select
              v-model="form.isPage"
              @change="isPageChange"
              :disabled="mode !== 'create'"
            >
              <el-option label="是" :value="1"></el-option>
              <el-option label="否" :value="0"></el-option>
            </el-select>
          </el-form-item>
        </div>
        <div v-show="form.isPage == 0">
          <el-form-item label="模板：" prop="component">
            <el-select v-model="form.component" clearable>
              <el-option
                v-for="(item, idx) in ['EmptyLayout', 'Layout']"
                :key="idx"
                :label="item"
                :value="item"
              ></el-option>
            </el-select>
          </el-form-item>
        </div>
        <div v-show="form.isPage == 0">
          <el-form-item label="重定向：" prop="redirect">
            <el-input :maxlength="32" v-model="form.redirect"></el-input>
          </el-form-item>
        </div>
        <div v-show="form.isPage">
          <el-form-item label="页面路径：" prop="component">
            <el-input
              :maxlength="128"
              v-model="form.component"
              placeholder="组件在src下的绝对路径"
            ></el-input>
          </el-form-item>
        </div>
        <div v-if="form.isPage">
          <el-form-item label="页面名称：" prop="name">
            <el-input
              :maxlength="32"
              v-model="form.name"
              placeholder="请与页面name属性一致"
            ></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="图标：" prop="iconUrl">
            <el-select
              :maxlength="32"
              v-model="form.iconUrl"
              filterable
              remote
              :remote-method="handleIconFilter"
              clearable
            >
              <el-option v-for="(o, i) in icons" :key="i" :value="o.iconName">
                <svg
                  aria-hidden="true"
                  focusable="false"
                  data-prefix="fas"
                  data-icon="users-cog"
                  role="img"
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 640 512"
                  class="vab-fas-icon svg-inline--fa fa-users-cog fa-w-20"
                >
                  <path fill="currentColor" :d="o.code" class=""></path>
                </svg>
                {{ o.iconName }}
              </el-option>
            </el-select>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="标签：" prop="badge">
            <el-input :maxlength="10" v-model="form.badge"></el-input>
          </el-form-item>
        </div>

        <div>
          <el-form-item label="是否显示：" prop="isVisible">
            <el-select v-model="form.isVisible">
              <el-option label="是" :value="1"></el-option>
              <el-option label="否" :value="0"></el-option>
            </el-select>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="是否固定：" prop="isAffix">
            <el-select v-model="form.isAffix">
              <el-option label="是" :value="1"></el-option>
              <el-option label="否" :value="0"></el-option>
            </el-select>
          </el-form-item>
        </div>
        <div v-show="form.isPage">
          <el-form-item label="是否缓存：" prop="noKeepAlive">
            <el-select v-model="form.noKeepAlive">
              <el-option label="是" :value="0"></el-option>
              <el-option label="否" :value="1"></el-option>
            </el-select>
          </el-form-item>
        </div>

        <div v-if="mode != 'create'">
          <el-form-item label="菜单层级：" prop="level">
            <el-input v-model="form.level" disabled></el-input>
          </el-form-item>
        </div>
        <div v-if="mode != 'create'">
          <el-form-item label="排序：" prop="sort">
            <el-input-number
              v-model="form.sort"
              style="width: 100%"
              :min="0"
              :max="10000"
            ></el-input-number>
          </el-form-item>
        </div>
      </div>
    </el-form>
    <div v-show="mode !== 'look'" slot="footer" class="dialog-footer rowCenter">
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </div>
  </el-dialog>
</template>

<script>
import { edit, create } from '@/api/systems/menu'
import MenuTree from '_c/Systems/MenuTree'
import { mapGetters, mapMutations, mapActions } from 'vuex'
export default {
  name: 'MenuManagementEdit',
  components: { MenuTree },
  data() {
    return {
      icons: [],
      mode: '',
      form: {},
      rules: {
        name: [{ required: true, trigger: 'blur', message: '请输入标题' }],
        isPage: [
          { required: true, trigger: 'blur', message: '请选择是否是页面' },
        ],
        navTitle: [{ required: true, trigger: 'blur', message: '请输入标题' }],
        parentName: [
          { required: true, trigger: 'blur', message: '请选择上级菜单' },
        ],
        linkUrl: [{ required: true, trigger: 'blur', message: '请输入路由' }],
        component: [{ required: true, trigger: 'blur', message: '请输入组件' }],
      },
      title: '',
      dialogFormVisible: false,
    }
  },
  computed: {
    ...mapGetters({
      iconFilterAfter: 'common/iconFilterAfter',
      iconSources: 'common/iconSources',
    }),
  },
  created() {
    this.icons = this.iconSources
  },
  mounted() {},
  methods: {
    ...mapMutations({
      iconFilter: 'common/filterIcon',
    }),
    isPageChange(v) {
      this.$set(this.form, 'component', '')
      if (v) {
        this.$set(this.form, 'redirect', '')
        return
      }
      this.$set(this.form, 'name', '')
    },
    handleIconFilter(key) {
      this.iconFilter(key)
      this.icons = this.iconFilterAfter
    },
    handleNodeClickEven(data) {
      this.$set(this.form, 'parentId', data.id)
      this.$set(this.form, 'parentName', data.label)
      this.$set(this.form, 'level', parseInt(data.level) + 1)
      this.$refs.menuTree.blur()
    },
    showEdit(row, mode) {
      this.mode = mode
      if (mode == 'create') {
        this.title = '新增菜单'
        this.$set(this.form, 'isVisible', 1)
        this.$set(this.form, 'noKeepAlive', 0)
        this.$set(this.form, 'isAffix', 0)
      } else {
        this.form = Object.assign({}, row)
        this.$nextTick(() => {
          this.$refs.mytree.change(this.form.parentId)
        })
        if (mode == 'edit') this.title = '编辑菜单'
        if (mode == 'look') this.title = '菜单详情'
      }

      this.dialogFormVisible = true
    },
    close() {
      this.$refs['form'].resetFields()
      this.form = this.$options.data().form
      this.dialogFormVisible = false
    },
    save() {
      this.$refs['form'].validate(async (valid) => {
        if (valid) {
          this.form.name = this.form.name.replace('_', '')
          let res
          if (this.mode === 'edit') res = await edit(this.form)
          else if (this.mode === 'create') res = await create(this.form)
          const { message } = res
          this.$baseMessage(message, 'success')
          this.$emit('fetch-data')
          this.close()
        } else {
          return false
        }
      })
    },
  },
}
</script>
<style scoped>
/* 修改 添加样式*/
.Modification {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
}
.Modification > div {
  width: 50%;
}
.Modification .el-select {
  width: 100%;
}

html body .el-dialog__body .el-form,
html body .el-message-box__body .el-form {
  padding-right: 0px;
}
.demoDrawe {
  position: absolute;
  top: 40%;
  left: 0;
}
.demoDrawe > button {
  width: 35px;
  height: 200px;
  font-size: 30px;
}
.demoDrawe > .el-button--small {
  padding: 9px 10px;
}
</style>