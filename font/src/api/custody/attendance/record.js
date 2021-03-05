import axios from '@/libs/api.request'

export function getList(data) {
  return axios.request({
    url: '/attendance/List',
    method: 'post',
    data,
  })
}
export function exportList(data, config) {
  return axios.request({
    url: '/attendance/export',
    method: 'post',
    data,
    ...config
  })
}

export function remove(param) {
  return axios.request({
    url: '/attendance/remove?param=' + param,
    method: 'get',
  })
}
export function getAttendanceList(param) {
  return axios.request({
    url: '/attendance/AttendanceList?param=' + param,
    method: 'get',
  })
}
export function getTraceData(id) {
  return axios.request({
    url: '/attendance/Trace?id=' + id,
    method: 'get',
  })
}