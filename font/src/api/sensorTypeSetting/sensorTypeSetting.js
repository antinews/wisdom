import axios from '@/libs/api.request'
export function getKindinfoList(data) {
  return axios.request({
    url: 'monitor/kindinfo/list',
    method: 'post',
    data,
    prefix: "api/"
  })
}

export function getKindinfoEdit(data) {
  return axios.request({
    url: 'monitor/kindinfo/edit',
    method: 'post',
    data,
    prefix: "api/"
  })
}

export function getKindinfoDelete(id) {
  return axios.request({
    url: `monitor/kindinfo/delete/${id}`,
    method: 'get',
    prefix: "api/"
  })
}