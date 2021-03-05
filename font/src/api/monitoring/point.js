import axios from '@/libs/api.request'
// 获取列表
export function getPointList(data) {
  return axios.request({
    url: "monitor/points/list",
    method: "post",
    prefix: "api/",
    data,
  });
};
// 删除
export function deletePoint(id) {
  return axios.request({
    url: "monitor/points/delete/" + id,
    method: "get",
    prefix: "api/"
  });
};

// 单个
export function singlePoint(id) {
  return axios.request({
    url: "monitor/points/edit/" + id,
    method: "get",
    prefix: "api/"
  });
};
// 修改
export function updatePoint(data) {
  return axios.request({
    url: "monitor/points/edit",
    method: "post",
    prefix: "api/",
    data,
  });
};
// 批量操作
export function BatchPoint(params) {
  return axios.request({
    url: `monitor/points/Batch?command=${params.command}&ids=${params.ids}`,
    method: "get",
    prefix: "api/"
  });
};
// 新增
export function addPoint(data) {
  return axios.request({
    url: "monitor/points/create",
    method: "post",
    prefix: "api/",
    data,
  });
};