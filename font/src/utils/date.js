export const format = date => {
  return date !== null && date !== ""
    ? new Date(date)
      .toISOString()
      .replace(/T/g, " ")
      .replace(/\.[\d]{3}Z/, "")
    : "";
};
// 时间格式化
export function formatDate(dt, fmt = "yyyy-MM-dd hh:mm:ss") {
  var date = new Date(dt);
  let timestamp = date.getTime();
  date = new Date(timestamp + 8 * 60 * 60 * 1000)
  let o = {
    'M+': date.getMonth() + 1, // 月份
    'd+': date.getDate(), // 日
    'h+': date.getHours(), // 小时
    'm+': date.getMinutes(), // 分
    's+': date.getSeconds(), // 秒
    'S': date.getMilliseconds() // 毫秒
  }
  if (/(y+)/.test(fmt)) {
    fmt = fmt.replace(RegExp.$1, (date.getFullYear() + '').substr(4 - RegExp.$1.length))
  }
  for (var k in o) {
    if (new RegExp('(' + k + ')').test(fmt)) {
      fmt = fmt.replace(RegExp.$1, (RegExp.$1.length === 1) ? (o[k]) : (('00' + o[k]).substr(('' + o[k]).length)))
    }
  }
  return fmt
}
export function string2date(str) {
  try {
    let times = str.replace(/\//g, " ").replace(/-/g, " ").replace(/\./g, " ").replace(/:/g, " ").replace(/T/g, " ").split(" ");
    let time = new Date((times.length > 0 ? times[0] : "2000") + "-" + (times.length > 1 ? times[1] : "01") + "-" + (
      times
        .length > 2 ? times[2] : "01") + " " + (times.length > 3 ? times[3] : "00") + ":" + (times.length > 4 ? times[4] :
          "00") + ":" + (times.length > 5 ? times[5] : "00") + "." + (times.length > 6 ? times[6] : "000"));
    return time
  } catch (e) {
    return new Date();
  }
}

// 时间格式化
export function formatDate2String(str, fmt) {
  return formatDate(string2date(str), fmt);
}
