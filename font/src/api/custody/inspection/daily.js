import axios from '@/libs/api.request'

export function getList(data) {
  return axios.request({
    url: '/dailyInspection/List',
    method: 'post',
    data,
  })
}


// 病害
export function edit(data) {
  return axios.request({
    url: '/dailyInspection/edit',
    method: 'post',
    data,
  })
}

export function create(data) {
  return axios.request({
    url: '/dailyInspection/create',
    method: 'post',
    data,
  })
}
export const uploadAction = axios.baseURL + axios.defaultPrefix + '/dailyInspection/upload'
export const serverFilePath = axios.baseURL + '/Uploads/DailyInspection/'

export function remove(ids) {
  return axios.request({
    url: '/dailyInspection/remove?ids=' + ids,
    method: 'get',
  })
}

export function getDropDownListData(type, a = 0, b = 0) {
  return axios.request({
    url: `/dailyInspection/getDropDownListData?type=${type}&a=${a}&b=${b}`,
    method: 'get',
  })
}