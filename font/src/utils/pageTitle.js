import { title } from '@/config'

/**
 * @author antinew 2357729423@qq.com （不想保留author可删除）
 * @description 设置标题
 * @param pageTitle
 * @returns {string}
 */
export default function getPageTitle(pageTitle) {
  if (pageTitle) {
    return `${pageTitle}-${title}`
  }
  return `${title}`
}
