import request from '@/utils/requestaxios'

//读取全部
export function GetDiccategorys() {
    return request({
        url: 'api/diccategorys/getdiccategorys',
        method: 'Get',
    })
}

//添加类型
export function adddiccategory(data) {
    return request({
        url: '/api/diccategorys/adddiccategory',
        method: 'post',
        data
    })
}
//修改字典类型
export function SetDiccategoryById(data) {
    return request({
        url: "/api/diccategorys/setdiccategorybyid",
        method: 'post',
        data
    })
}
//获取字典类型名
export function GetDiccategorysName(data) {
    return request({
        url: '/api/diccategorys/getdiccategorysbyid/' + data,
        method: 'get'
    })
}
//删除
export function DelCategory(data) {
    return request({
        url: '/api/diccategorys/deldiccategorysbyid/' + data,
        method: 'get'
    })
}
