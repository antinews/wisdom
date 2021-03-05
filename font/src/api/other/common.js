import axios from '@/libs/api.request'

export function getDicsByKey(code) {
  return axios.request({
    url: '/common/getDicsByKey?key=' + code,
    method: 'get',
  })
}