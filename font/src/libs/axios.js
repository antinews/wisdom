import Vue from 'vue'
import axios from 'axios'
import {
  contentType,
  debounce,
  invalidCode,
  noPermissionCode,
  requestTimeout,
  successCode,
  loginInterception,
} from '@/config'
import store from '@/store'
import qs from 'qs'
import router from '@/router'
import { isArray } from '@/utils/validate'

let loadingInstance

/**
 * @author antinew 2357729423@qq.com （不想保留author可删除）
 * @description 处理code异常
 * @param {*} code
 * @param {*} message
 */
const handleCode = (code, message) => {
  // console.log(code);
  // console.log(message);
  switch (code) {
    case invalidCode:
      Vue.prototype.$baseMessage(message || `后端接口${code}异常`, 'error')
      store.dispatch('user/resetAccessToken').catch(() => { })
      if (loginInterception) {
        location.reload()
      }
      break
    case noPermissionCode:
      router.push({ path: '/401' }).catch(() => { })
      break
    default:
      Vue.prototype.$baseMessage(message || `后端接口${code}异常`, 'error')
      break
  }
}

class HttpRequest {
  constructor(baseURL, defaultPrefix) {
    this.baseURL = baseURL
    this.defaultPrefix = defaultPrefix
  }

  getConfig() {
    const config = {
      baseURL: this.baseURL,
      timeout: requestTimeout,
      headers: {
        'Content-Type': contentType,
      },
    }
    return config
  }

  interceptors(instance) {
    instance.interceptors.request.use(
      (config) => {
        if (store.getters['user/accessToken']) {
          config.headers['Authorization'] = "Bearer " + store.getters['user/accessToken']
        }
        //这里会过滤所有为空、0、false的key，如果不需要请自行注释
        // if (config.data)
        //   config.data = Vue.prototype.$baseLodash.pickBy(
        //     config.data,
        //     Vue.prototype.$baseLodash.identity
        //   )
        if (
          config.data &&
          config.headers['Content-Type'] ===
          'application/x-www-form-urlencoded;charset=UTF-8'
        )
          config.data = qs.stringify(config.data)
        if (debounce.some((item) => config.url.includes(item)))
          loadingInstance = Vue.prototype.$baseLoading()
        return config
      },
      (error) => {
        return Promise.reject(error)
      }
    )

    instance.interceptors.response.use(
      (response) => {
        if (loadingInstance) loadingInstance.close()
        const { data, config } = response
        const { code, message } = data
        if (config.responseType && config.responseType === "blob") return data
        // 操作正常Code数组
        const codeVerificationArray = isArray(successCode)
          ? [...successCode]
          : [...[successCode]]
        // 是否操作正常
        if (codeVerificationArray.includes(code)) return data
        handleCode(code, message)
        return Promise.reject(
          'vue-admin-beautiful请求异常拦截:' +
          JSON.stringify({ url: config.url, code, message }) || 'Error'
        )
      },
      (error) => {
        if (loadingInstance) loadingInstance.close()
        const { response, message } = error
        if (error.response && error.response.data) {
          const { status, data } = response
          handleCode(status, data.message || message)
          return Promise.reject(error)
        } else {
          let { message } = error
          if (message === 'Network Error') {
            message = '后端接口连接异常'
          }
          if (message.includes('timeout')) {
            message = '后端接口请求超时'
          }
          if (message.includes('Request failed with status code')) {
            const code = message.substr(message.length - 3)
            message = '后端接口' + code + '异常'
          }
          Vue.prototype.$baseMessage(message || `后端接口未知异常`, 'error')
          return Promise.reject(error)
        }
      }
    )
  }

  request(options) {
    const instance = axios.create();
    let withPrefix = true;
    if (options.withPrefix !== undefined && options.withPrefix == false) withPrefix = false
    let url = options.url;
    if (options.prefix) url = options.prefix + url;
    else if (withPrefix) url = this.defaultPrefix + options.url;
    options.url = url;
    options = Object.assign(this.getConfig(), options);
    this.interceptors(instance)  // 注册拦截器
    return instance(options);
  }
}

export default HttpRequest
