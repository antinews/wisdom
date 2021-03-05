import axios from '@/libs/api.request'

export function getList(data) {
  return axios.request({
    url: '/permission/List',
    method: 'post',
    data,
  })
}

export function edit(data) {
  return axios.request({
    url: '/permission/edit',
    method: 'post',
    data,
  })
}

export function create(data) {
  return axios.request({
    url: '/permission/create',
    method: 'post',
    data,
  })
}

export function remove(ids) {
  return axios.request({
    url: '/permission/remove?ids=' + ids,
    method: 'get',
  })
}


//load role-permission tree
export const loadPermissionTree = (roleId) => {
  return axios.request({
    url: '/permission/permissionTree?id=' + roleId,
    method: 'get'
  })
}