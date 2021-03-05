import request from '@/utils/requestaxios'

//读取
export function GetDepartement() {
    return request({
        url: 'api/Department/GetDepartement',
        method: 'Get',
    })
}
//获取树形结构的数据
export  function GetTreeList() {
  return request({
    url:'/api/department/gettreedate',
    methos:'Get'
  })
}




//通过父id读取
export function GetDepartementList(data) {
    return request({
        url: 'api/Department/GetDepartementList/' + data,
        method: 'Get',
    })
}

export function SetDepartementById(data) {
return request({
  url:'/api/department/setdepartementbyid',
  method:'post',
  data:data
})
}


//删除
export function DelDepartement(data) {
    return request({
        url: 'api/Department/DelDepartement',
        method: 'post',
        data: data
    })
}
//批量删除
export function DelDepartementList(data) {
    return request({
        url: 'api/Department/DelDepartementList',
        method: 'post',
        data: data
    })
}
//部门名模糊查询
export function GetDepartementByName(data) {
    return request({
        url: 'api/Department/GetDepartementByName/' + data,
        method: 'Get',
    })
}
//添加
export function AddDepartement(data) {
    return request({
        url: 'api/Department/AddDepartement',
        method: 'post',
        data: data
    })
}
