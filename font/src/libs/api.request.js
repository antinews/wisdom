import HttpRequest from '@/libs/axios'
import {
  baseUrl,
} from '@/config'

const url = process.env.NODE_ENV === 'development' ? baseUrl.dev : process.env.NODE_ENV === 'staging' ? baseUrl.stage : baseUrl.pro
const prefix = baseUrl.defaultPrefix;
const axios = new HttpRequest(url, prefix)
export default axios