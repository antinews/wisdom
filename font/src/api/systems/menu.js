import axios from '@/libs/api.request'

export function getList(data) {
  return axios.request({
    url: '/menu/List',
    method: 'post',
    data,
  })
}

export function edit(data) {
  return axios.request({
    url: '/menu/edit',
    method: 'post',
    data,
  })
}

export function create(data) {
  return axios.request({
    url: '/menu/create',
    method: 'post',
    data,
  })
}
export function editPermissions(data) {
  return axios.request({
    url: '/menu/editPermissions',
    method: 'post',
    data,
  })
}

export function getPermissionsByMenuId(id) {
  return axios.request({
    url: '/menu/getPermissionsByMenuId?id=' + id,
    method: 'get',
  })
}
export function remove(ids) {
  return axios.request({
    url: '/menu/remove?ids=' + ids,
    method: 'get',
  })
}

export const loadMenuTree = (id) => {
  let url = '/menu/tree';
  if (id) url += '?id=' + id;
  return axios.request({
    url,
    method: 'get'
  })
}
