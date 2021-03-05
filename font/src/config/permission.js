/**
 * @author antinew 2357729423@qq.com （不想保留author可删除）
 * @description 路由守卫，目前两种模式：all模式与intelligence模式
 */
import router from '@/router'
import store from '@/store'
import VabProgress from 'nprogress'
import 'nprogress/nprogress.css'
import getPageTitle from '@/utils/pageTitle'
import {
  authentication,
  loginInterception,
  progressBar,
  recordRoute,
  routesWhiteList,
  administrator,
} from '@/config'

VabProgress.configure({
  easing: 'ease',
  speed: 500,
  trickleSpeed: 200,
  showSpinner: false,
})
router.beforeResolve(async (to, from, next) => {
  if (progressBar) VabProgress.start()
  let hasToken = store.getters['user/accessToken']

  if (!loginInterception) hasToken = true
  if (routesWhiteList.indexOf(to.path) !== -1) {  // 不经校验的路由
    next()
    return
  }
  if (hasToken) {
    if (to.path === '/login') {
      next({ path: '/' })
      if (progressBar) VabProgress.done()
    }
    else {
      let checkPermission = true;
      const hasPermissions = Object.keys(store.getters['user/permissions']).length

      if (hasPermissions) {
        checkPermission = store.getters['user/usertype'] != administrator; // 不检查超级管理员
        next()
        turnTo(to, checkPermission, store.getters['user/permissions'], next)
      } else {
        try {
          await store.dispatch('user/getUserInfo')
          if (Object.keys(store.getters['user/permissions']).length) {
            let accessRoutes = await store.dispatch('routes/setAllRoutes')
            router.addRoutes(accessRoutes)
            next({ ...to, replace: true }) // 抛回
          } else {
            next({ path: '/401' })
          }
        } catch (err) {
          console.log(err);
          await store.dispatch('user/resetAccessToken')
          if (progressBar) VabProgress.done()
        }
      }
    }
  }
  else {
    if (recordRoute) {
      next(`/login?redirect=${to.path}`)
    } else {
      next('/login')
    }

    if (progressBar) VabProgress.done()

  }
  document.title = getPageTitle(to.meta.title)
})
router.afterEach(() => {
  if (progressBar) VabProgress.done()
})


const turnTo = (to, checkPermission, permissions, next) => {
  to.meta.checkPermission = checkPermission;
  if (!checkPermission) {
    next(); return
  }
  if (to.path === '/index') {
    next(); return
  }
  permissions = permissions || [];
  if (permissions && permissions[to.name]) {
    to.meta.permissions = permissions[to.name];
    next()
  }
  else next({ path: '/401' })
};
