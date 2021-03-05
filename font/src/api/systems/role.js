import axios from '@/libs/api.request'

export function getList(data) {
  return axios.request({
    url: '/role/List',
    method: 'post',
    data,
  })
}

export function edit(data) {
  return axios.request({
    url: '/role/edit',
    method: 'post',
    data,
  })
}

export function create(data) {
  return axios.request({
    url: '/role/create',
    method: 'post',
    data,
  })
}

export function remove(ids) {
  return axios.request({
    url: '/role/remove?ids=' + ids,
    method: 'get',
  })
}

//assign permissions for role
export const assignPermission = (data) => {
  return axios.request({
    url: '/role/assignPerimission',
    method: 'post',
    data
  })
}


export function rolesByUserMg() {
  return axios.request({
    url: '/role/rolesByUserMg',
    method: 'get',
  })
}