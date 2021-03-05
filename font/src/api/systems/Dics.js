import request from '@/utils/requestaxios'

//通过类型读取数据
export function GetDicsListByID(data) {
    return request({
        url: 'api/Dics/GetDicsListByID/' + data,
        method: 'Get',
    })
}
//添加字典
export function AddDics(data) {
    return request({
        url: '/api/dics/adddics',
        method: "post",
        data
    })
}
//修改字典
export function SetdicsbyId(data) {
    return request({
        url: '/api/dics/setdicsbyid',
        method: 'post',
        data
    })
}
//批量删除
export function DelDicsList() {
    return request({
        url: '/api/dics/deldicslist',
        method: 'post',
        data
    })
}
//单个删除
export function DelDicsById(data) {
    return request({
        url: '/api/dics/deldics/' + data,
        method: 'get'
    })
}
