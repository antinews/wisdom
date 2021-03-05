import axios from '@/libs/api.request'

export function getList(data) {
  return axios.request({
    url: '/user/List',
    method: 'post',
    data,
  })
}

export function edit(data) {
  return axios.request({
    url: '/user/edit',
    method: 'post',
    data,
  })
}

export function create(data) {
  return axios.request({
    url: '/user/create',
    method: 'post',
    data,
  })
}

export function remove(ids) {
  return axios.request({
    url: '/user/remove?ids=' + ids,
    method: 'get',
  })
}

export function generateUserNameByTrueName(name) {
  return axios.request({
    url: '/user/generateUserNameByTrueName?trueName=' + name,
    method: 'get',
  })
}