<template>
  <div>
    <el-select v-model="roleId" @change="handleRoleChange" class="role">
      <el-option
        v-for="(o, i) in roles"
        :key="i"
        :label="o.roleName"
        :value="o.id"
      ></el-option>
    </el-select>
  </div>
</template>

<script>
import { rolesByUserMg } from '@/api/systems/role'
export default {
  data() {
    return {
      roles: [],
      roleId: '',
    }
  },
  created() {
    this.loadRoles()
  },
  methods: {
    loadRoles() {
      rolesByUserMg().then((res) => {
        this.roles = res.data
      })
    },
    handleRoleChange(v) {
      this.$emit('roleChange', v)
    },
    entry(id) {
      this.roleId = id
    },
    exit() {
      this.roleId = ''
    },
  },
}
</script>

<style>
.role {
  width: 100%;
}
</style>