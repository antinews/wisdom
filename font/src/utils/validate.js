/**
 * @author antinew 2357729423@qq.com
 * @description 判读是否为外链
 * @param path
 * @returns {boolean}
 */
export function isExternal(path) {
  return /^(https?:|mailto:|tel:)/.test(path)
}

/**
 * @author antinew 2357729423@qq.com
 * @description 校验密码是否小于6位
 * @param str
 * @returns {boolean}
 */
export function isPassword(str) {
  return str.length >= 3
}

/**
 * @author antinew 2357729423@qq.com
 * @description 判断是否为数字
 */
export const isNumber = /^[0-9]*$/


/**
 * @author antinew 2357729423@qq.com
 * @description 判断是否是名称
 */
export const isName = /^[\u4e00-\u9fa5a-zA-Z0-9]+$/

/**
 * @author antinew 2357729423@qq.com
 * @description 判断是否为IP
 */
export const isIP = /^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$/

/**
 * @author antinew 2357729423@qq.com
 * @description 判断是否是传统网站
 */
export const isUrl = /^(https?|ftp):\/\/([a-zA-Z0-9.-]+(:[a-zA-Z0-9.&%$-]+)*@)*((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])){3}|([a-zA-Z0-9-]+\.)*[a-zA-Z0-9-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(:[0-9]+)*(\/($|[a-zA-Z0-9.,?'\\+&%$#=~_-]+))*$/

/**
 * @author antinew 2357729423@qq.com
 * @description 判断是否是小写字母
 */
export const isLowerCase = /^[a-z]+$/

/**
 * @author antinew 2357729423@qq.com
 * @description 判断是否是大写字母
 */
export const isUpperCase = /^[A-Z]+$/

/**
 * @author antinew 2357729423@qq.com
 * @description 判断是否是大写字母开头
 */
export const isAlphabets = /^[A-Za-z]+$/

/**
 * @author antinew 2357729423@qq.com
 * @description 判断是否是字符串
 * @param str
 * @returns {boolean}
 */
export function isString(str) {
  return typeof str === 'string' || str instanceof String
}

/**
 * @author antinew 2357729423@qq.com
 * @description 判断是否是数组
 * @param arg
 * @returns {arg is any[]|boolean}
 */
export function isArray(arg) {
  if (typeof Array.isArray === 'undefined') {
    return Object.prototype.toString.call(arg) === '[object Array]'
  }
  return Array.isArray(arg)
}

/**
 * @author antinew 2357729423@qq.com
 * @description 判断是否是端口号
 */
export const isPort = /^([0-9]|[1-9]\d|[1-9]\d{2}|[1-9]\d{3}|[1-5]\d{4}|6[0-4]\d{3}|65[0-4]\d{2}|655[0-2]\d|6553[0-5])$/

/**
 * @author antinew 2357729423@qq.com
 * @description 判断是否是手机号 手机号如果不算前缀的国家码，一般由11位数字组成
 */
export const isPhone = /^1(3|4|5|6|7|8|9)\d{9}$/

/**
 * @author antinew 2357729423@qq.com
 * @description 判断是否是身份证号(第二代)
 */
export const isIdCard = /^[1-9]\d{5}(18|19|([23]\d))\d{2}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\d{3}[0-9Xx]$/

/**
 * @author antinew 2357729423@qq.com
 * @description 判断是否是邮箱
 */
export const isEmail = /^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z0-9]{2,6}$/

/**
 * @author antinew 2357729423@qq.com
 * @description 判断是否中文
 */
export const isChina = /^[\u4E00-\u9FA5]{2,4}$/

/**
 * @author antinew 2357729423@qq.com
 * @description 判断是否为空
 * @param str
 * @returns {boolean}
 */
export function isBlank(str) {
  return (
    str == null ||
    false ||
    str === '' ||
    str.trim() === '' ||
    str.toLocaleLowerCase().trim() === 'null'
  )
}

/**
 * @author antinew 2357729423@qq.com
 * @description 判断是否为固话
 */
export const isTel = /^(400|800)([0-9\\-]{7,10})|(([0-9]{4}|[0-9]{3})(-| )?)?([0-9]{7,8})((-| |转)*([0-9]{1,4}))?$/

/**
 * @author antinew 2357729423@qq.com
 * @description 判断是否为IMEI
 */
export const isIMEI = /\d{15}|\d{17}/

/**
 * @author antinew 2357729423@qq.com
 * @description 判断是否为QQ 5~11位
 */
export const isQQ = /^[1-9][0-9]{3,9}$/