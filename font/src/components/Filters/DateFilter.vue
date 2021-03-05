<template>
  <div style="display: inline">
    <slot></slot>
    <el-date-picker
      value-format="yyyy-MM-dd"
      style="width: 140px"
      placeholder="起始时间"
      v-model="stime"
      :picker-options="{
        disabledDate: (time) => {
          return time.getTime() > new Date(etime)
        },
      }"
    ></el-date-picker>
    <span style="margin: 0 7px">至</span>
    <el-date-picker
      class="mr-1"
      style="width: 140px"
      placeholder="结束时间"
      v-model="etime"
      value-format="yyyy-MM-dd"
      :picker-options="{
        disabledDate: (time) => {
          return time.getTime() < new Date(new Date(stime).getTime() - 86400000)
        },
      }"
    ></el-date-picker>
  </div>
</template>

<script>
export default {
  data() {
    return {
      stime: '',
      etime: '',
    }
  },
}
</script>

<style>
</style>