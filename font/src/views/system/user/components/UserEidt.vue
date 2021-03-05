<template>
  <el-dialog
    :title="title"
    :visible.sync="dialogFormVisible"
    width="44%"
    @close="close"
  >
    <el-form ref="form" :model="form" :rules="rules" label-width="39%">
      <div class="Modification">
        <div>
          <el-form-item label="姓名：" prop="trueName">
            <el-input
              v-model="form.trueName"
              :maxlength="10"
              placeholder="请输入姓名"
            ></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="性别：" prop="sex">
            <el-select v-model="form.sex">
              <el-option :value="0" label="女"></el-option>
              <el-option :value="1" label="男"></el-option>
            </el-select>
          </el-form-item>
        </div>

        <div>
          <el-form-item label="所属部门：" prop="departName">
            <el-select
              v-model="form.departName"
              placeholder="请选择上级菜单"
              ref="departTree"
            >
              <el-option
                :label="form.id"
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
        </div>
        <div>
          <el-form-item label="角色：" prop="roleId">
            <role-selector
              @roleChange="handleRoleChange"
              ref="role-selector"
            ></role-selector>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="用户名：" prop="userName">
            <el-input
              v-model="form.userName"
              :maxlength="24"
              placeholder="请输入用户名"
            >
              <el-button
                v-if="mode === 'create'"
                slot="append"
                icon="el-icon-mouse"
                @click="generateUserName(form.trueName)"
              >
                生成
              </el-button>
            </el-input>
          </el-form-item>
        </div>
        <div v-if="mode === 'create'">
          <el-form-item label="密码：" prop="password">
            <el-input
              :key="passwordType"
              ref="password"
              v-model.trim="form.password"
              :type="passwordType"
              tabindex="2"
              placeholder="请输入密码"
            >
              <div slot="append" @click="handlePassword">
                <span v-if="passwordType === 'password'" class="show-password">
                  <vab-icon :icon="['fas', 'eye-slash']"></vab-icon>
                </span>
                <span v-else class="show-password">
                  <vab-icon :icon="['fas', 'eye']"></vab-icon>
                </span>
              </div>
            </el-input>
            <!-- <el-input
              placeholder="默认为姓名全拼123"
              v-model="form.password"
              :maxlength="24"
              type="password"
            >
              <el-button
                v-if="mode === 'create'"
                slot="append"
                icon="el-icon-mouse"
                @click="generatePwd(form.trueName)"
              >
                生成
              </el-button>
            </el-input> -->
          </el-form-item>
        </div>
        <div>
          <el-form-item label="手机：" prop="mobile">
            <el-input v-model="form.mobile" :maxlength="11"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="IMEI：" prop="imei">
            <el-input v-model="form.imei" :maxlength="17"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="邮箱：" prop="email">
            <el-input v-model="form.email" :maxlength="64"></el-input>
          </el-form-item>
        </div>
        <div>
          <el-form-item label="QQ：" prop="qq">
            <el-input v-model="form.qq" :maxlength="11"></el-input>
          </el-form-item>
        </div>
        <div v-show="mode !== 'create'">
          <el-form-item label="是否为超级管理员：" prop="isAdmin">
            <el-select v-model="form.isAdmin">
              <el-option :value="0" label="否"></el-option>
              <el-option :value="1" label="是"></el-option>
            </el-select>
          </el-form-item>
        </div>
        <div v-show="mode !== 'create'">
          <el-form-item label="是否禁用：" prop="isDisabled">
            <el-select v-model="form.isDisabled">
              <el-option :value="0" label="否"></el-option>
              <el-option :value="1" label="是"></el-option>
            </el-select>
          </el-form-item>
        </div>
        <div v-show="mode !== 'create'">
          <el-form-item label="是否是负责人：" prop="isLeader">
            <el-select v-model="form.isDisabled">
              <el-option :value="0" label="否"></el-option>
              <el-option :value="1" label="是"></el-option>
            </el-select>
          </el-form-item>
        </div>
        <div v-show="mode !== 'create'">
          <el-form-item label="监测App是否授权：" prop="isDisabled">
            <el-select v-model="form.isDisabled">
              <el-option :value="0" label="否"></el-option>
              <el-option :value="1" label="是"></el-option>
            </el-select>
          </el-form-item>
        </div>
      </div>
    </el-form>
    <div slot="footer" class="dialog-footer rowCenter">
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </div>
  </el-dialog>
</template>

<script>
import { edit, create, generateUserNameByTrueName } from '@/api/systems/user'
import DepartTree from '_c/Systems/DepartTree'
import RoleSelector from './RoleSelector'
import { isPhone, isIMEI, isEmail, isQQ } from '@/utils/validate'
export default {
  name: 'UserEdit',
  components: { DepartTree, RoleSelector },
  data() {
    return {
      mode: '',
      form: {},
      passwordType: 'password',
      rules: {
        trueName: [{ required: true, trigger: 'blur', message: '请输入姓名' }],
        // userName: [
        //   { required: true, trigger: 'blur', message: '请输入用户名' },
        // ],
        // password: [{ required: true, trigger: 'blur', message: '请输入密码' }],
        departName: [
          { required: true, trigger: 'blur', message: '请选择部门' },
        ],
        sex: [{ required: true, trigger: 'blur', message: '请选择性别' }],
        roleId: [{ required: true, trigger: 'blur', message: '请选择角色' }],

        mobile: [
          {
            pattern: isPhone,
            message: '请输入11位有效手机号码',
          },
        ],
        imei: [
          {
            pattern: isIMEI,
            message: 'IMEI必须是15或17位数字',
          },
        ],
        email: [
          {
            pattern: isEmail,
            trigger: 'blur',
            message: '请输入正确格式的邮箱：xxxxxxx@xx.xx.xx',
          },
        ],
        qq: [
          {
            pattern: isQQ,
            trigger: 'blur',
            message: '请输入正确格式的QQ',
          },
        ],
      },
      title: '',
      dialogFormVisible: false,
    }
  },
  created() {},
  methods: {
    handlePassword() {
      this.passwordType === 'password'
        ? (this.passwordType = '')
        : (this.passwordType = 'password')
      this.$nextTick(() => {
        this.$refs.password.focus()
      })
    },
    async generateUserName(trueName) {
      if (!trueName) {
        this.$baseMessage('姓名不能为空！', 'warning')
        return
      }
      const { data } = await generateUserNameByTrueName(trueName)
      this.$set(this.form, 'userName', data)
    },
    handleRoleChange(role) {
      this.$set(this.form, 'roleId', role)
    },
    handleNodeClickEven(data) {
      this.$set(this.form, 'departName', data.departmentName)
      this.$set(this.form, 'departmentId', data.id)
      this.$refs.departTree.blur()
    },
    showEdit(row) {
      if (!row) {
        this.title = '新增用户'
        this.mode = 'create'
      } else {
        this.title = '编辑用户信息'
        this.mode = 'edit'

        this.form = Object.assign({}, row)
      }
      this.dialogFormVisible = true
      this.$nextTick(() => {
        if (row && row.roleId) this.$refs['role-selector'].entry(row.roleId)
      })
    },
    close() {
      this.$refs['form'].resetFields()
      this.form = this.$options.data().form
      this.$refs['role-selector'].exit()
      this.dialogFormVisible = false
    },
    save() {
      this.$refs['form'].validate(async (valid) => {
        if (valid) {
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

.show-password {
  width: 100%;
  cursor: pointer;
  user-select: none;
  -webkit-box-sizing: border-box; /* Safari/Chrome, other WebKit */
  -moz-box-sizing: border-box; /* Firefox, other Gecko */
  box-sizing: border-box; /* Opera/IE 8+ */
}
</style>