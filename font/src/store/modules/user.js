/**
 * @author antinew 2357729423@qq.com （不想保留author可删除）
 * @description 登录、获取用户信息、退出登录、清除accessToken逻辑，不建议修改
 */

import Vue from 'vue'
import { getUserInfo, login, logout } from '@/api/other/user'
import {
  getAccessToken,
  removeAccessToken,
  setAccessToken,
} from '@/utils/accessToken'
import { resetRouter } from '@/router'
import { title, tokenName } from '@/config'

const state = () => ({
  accessToken: getAccessToken(),
  username: '',
  avatar: '',
  usertype: -1,
  permissions: {},
  userid: '',
})
const getters = {
  accessToken: (state) => state.accessToken,
  username: (state) => state.username,
  avatar: (state) => state.avatar,
  usertype: (state) => state.usertype,
  permissions: (state) => state.permissions,
  userid: (state) => state.userid,
}
const mutations = {
  setAccessToken(state, accessToken) {
    state.accessToken = accessToken
    setAccessToken(accessToken)
  },
  setUsername(state, username) {
    state.username = username
  },
  setAvatar(state, avatar) {
    state.avatar = avatar
  },
  setPermissions(state, permissions) {
    state.permissions = permissions
  },
  setUsertype(state, usertype) {
    state.usertype = usertype
  },
  setUserid(state, userid) {
    state.userid = userid
  }
}
const actions = {
  setPermissions({ commit }, permissions) {
    commit('setPermissions', permissions)
  },
  async login({ commit }, userInfo) {
    const { data } = await login(userInfo)
    const accessToken = data
    // const accessToken = await login(userInfo)
    if (accessToken) {
      commit('setAccessToken', accessToken)
      const hour = new Date().getHours()
      const thisTime =
        hour < 8
          ? '早上好'
          : hour <= 11
            ? '上午好'
            : hour <= 13
              ? '中午好'
              : hour < 18
                ? '下午好'
                : '晚上好'
      Vue.prototype.$baseNotify(`欢迎登录${title}`, `${thisTime}！`)
    } else {
      Vue.prototype.$baseMessage(
        `登录接口异常，未正确返回${tokenName}...`,
        'error'
      )
    }
  },
  async getUserInfo({ commit, state }) {
    const { data, code } = await getUserInfo(state.accessToken)
    if (!data) {
      Vue.prototype.$baseMessage('验证失败，请重新登录...', 'error')
      return false
    }
    if (code !== 200) {
      Vue.prototype.$baseMessage('用户信息接口异常', 'error')
      return false
    }
    let { permissions, username, avatar, usertype, userid } = data

    commit('setPermissions', permissions)
    commit('setUsername', username)
    commit('setAvatar', avatar)
    commit('setUsertype', usertype)
    commit('setUserid', userid)
    return permissions
  },
  async logout({ dispatch }) {
    await logout(state.accessToken)
    await dispatch('resetAccessToken')
    await resetRouter()
  },
  resetAccessToken({ commit }) {
    commit('setPermissions', [])
    commit('setAccessToken', '')
    removeAccessToken()
  },
}
export default { state, getters, mutations, actions }
