import request from '@/utils/request'
import axios from "@/libs/api.request"
import { encryptedData } from '@/utils/encrypt'
import { loginRSA, tokenName } from '@/config'

export async function login(data) {
  if (loginRSA) {
    data = await encryptedData(data)
  }
  let { username, password } = data;
  return axios.request({
    url: `/auth/gettoken?username=${username}&password=${password}`,
    method: 'get',
    data,
  })
}

export function getUserInfo(accessToken) {
  return axios.request({
    url: `/Account/Profile`,
    method: 'get',
  })
}

export function logout() {
  return request({
    url: '/logout',
    method: 'post',
  })
}

export function register() {
  return request({
    url: '/register',
    method: 'post',
  })
}
