import axios from '@/libs/api.request'

export function getKindbyisactivepoint(id) {
  return axios.request({
    url: 'monitor/kindinfo/kindbyisactivepoint?pid=' + id,
    method: 'get',
    withPrefix: false,
    prefix: "api/"
  })
}

export function getPointsbykind(id) {
  return axios.request({
    url: 'monitor/points/pointsbykind?kid=' + id,
    method: 'get',
    withPrefix: false,
    prefix: "api/"
  })
}

export function getLineData(data) {
  const { point, type, lx, stime, etime } = data;
  if (stime && etime) {
    return axios.request({
      url: 'monitor/monitoringdata/getlinedata?point=' + point + '&type='
        + type + '&lx=' + lx + '&stime=' + stime + '&etime=' + etime,
      method: 'post',
      withPrefix: false,
      prefix: "api/"
    })
  }
  return axios.request({
    url: 'monitor/monitoringdata/getlinedata?point=' + point + '&type=' + type + '&lx=' + lx,
    method: 'post',
    withPrefix: false,
    prefix: "api/"
  })
}

export function getDevicesInfo(id) {
  return axios.request({
    url: 'monitor/devices/edit/' + id,
    method: 'get',
    withPrefix: false,
    prefix: "api/"
  })
}