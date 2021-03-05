import { timeFilter } from './date'

const importFilter = Vue => {
  Vue.filter('timeFilter', timeFilter)
}

export default importFilter