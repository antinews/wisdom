import axios from '@/libs/api.request'

export function getRouterList() {
    return axios.request({
        url: '/Account/Menu',
        method: 'get',
    })
}