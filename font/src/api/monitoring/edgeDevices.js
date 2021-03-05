import axios from '@/libs/api.request'

export function getList(data) {
  return axios.request({
    url: '/monitor/projects/list',
    method: 'post',
    prefix: '/api',
    data,
  })
}

export function getAll() {
  return axios.request({
    url: '/monitor/projects/GetAll',
    method: 'post',
    prefix: '/api',
  })
}

export function remove(ids) {
  return axios.request({
    url: '/monitor/projects/delete/' + ids,
    method: 'get',
    prefix: '/api',
  })
}

export function edit(data) {
  return axios.request({
    url: '/monitor/projects/edit',
    method: 'post',
    prefix: '/api',
    data,
  })
}

export function create(data) {
  return axios.request({
    url: '/monitor/projects/create',
    method: 'post',
    prefix: '/api',
    data,
  })
}

// 获取结构物类型列表
export function findStructuryTypes() {
  return axios.request({
    url: '/monitor/projects/projecttypes',
    method: 'get',
    prefix: '/api',
  })
}

// 获取结构物类型列表
export function findStructuryAreas() {
  return axios.request({
    url: '/area/list',
    method: 'post',
  })
}