import axios from '@/libs/api.request'

export function findCodecsList(data) {
  return axios.request({
    url: '/monitor/codecs/list',
    method: 'post',
    prefix: '/api',
    data,
  })
}