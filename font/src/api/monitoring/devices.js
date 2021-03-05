import axios from '@/libs/api.request'

export function getDeviceList(data) {
  return axios.request({
    url: '/monitor/devices/list',
    method: 'post',
    prefix: '/api',
    data,
  })
}
// 删除测站
export function deleteDevice(id) {
  return axios.request({
    url: "monitor/devices/delete/" + id,
    method: "get",
    prefix: "api/"
  });
};

// 单个测站
export function singleDevice(id) {
  return axios.request({
    url: "monitor/devices/edit/" + id,
    method: "get",
    prefix: "api/"
  });
};

// strames
export function getstreams(id) {
  return axios.request({
    url: "monitor/devices/getstreams/" + id,
    method: "get",
    prefix: "api/"
  });
};
// 修改测站
export function updateDevice(params) {
  return axios.request({
    url: "monitor/devices/edit",
    method: "post",
    data: params,
    prefix: "api/"
  });
};
// editStreams
export function editStreams(params) {
  return axios.request({
    url: "monitor/devices/editStreams",
    method: "post",
    data: params,
    prefix: "api/"
  });
};
// 批量操作（测站）
export function BatchDevice(params) {
  return axios.request({
    url: `monitor/devices/Batch?command=${params.command}&ids=${params.ids}`,
    method: "get",
    prefix: "api/"
  });
};
// 新增测站
export function addDevice(params) {
  return axios.request({
    url: "monitor/devices/create",
    method: "post",
    data: params,
    prefix: "api/"
  });
};