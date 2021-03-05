import Vue from 'vue'
import App from './App'
import store from './store'
import router from './router'
import '@/common/reset.css'
import '@/common/common.css'
import './plugins'
import '@/layouts/export'

import hasPermission from '@/directives/hasPermission.js';
Vue.use(hasPermission);

import BaiduMap from 'vue-baidu-map'
Vue.use(BaiduMap, {
  ak: 'QE5iXR5hL0YxR1oh4NGjARTDpCW9amwB'
})

import importFilter from '@/utils/filters'
importFilter(Vue)

import ExTableColumn from 'ex-table-column';
Vue.component(ExTableColumn.name, ExTableColumn);

/**
 * @author antinew 2357729423@qq.com （不想保留author可删除）
 * @description 生产环境默认都使用mock，如果正式用于生产环境时，记得去掉
 */

if (process.env.NODE_ENV === 'production') {
  const { mockXHR } = require('@/utils/static')
  mockXHR()
}

Vue.config.productionTip = false

new Vue({
  el: '#vue-admin-beautiful',
  router,
  store,
  render: (h) => h(App),
})
