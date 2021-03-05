import axios from 'axios'
import { MessageBox, Message } from 'element-ui'
import store from '@/store'

axios.defaults.baseURL = 'http://localhost:10010/';
//localhost

const TokenKey = 'vue_admin_template_token'

function getToken() {
    return localStorage.getItem(TokenKey)
}

function setToken(token) {
    return localStorage.setItem(TokenKey, token)
}

function removeToken() {
    return localStorage.removeItem(TokenKey)
}
const service = axios.create({
    baseURL: axios.defaults.baseURL,

    timeout: 35000
})

service.interceptors.request.use(
    config => {
        // if (store.getters.token) {
        //     //  让每个请求携带令牌
        //     config.headers['Token'] = getToken()
        // }
        return config
    },
    error => {
        console.log(error) // for debug
        return Promise.reject(error)
    }
)

// response interceptor
service.interceptors.response.use(

    response => {
        const res = response.data

        if (res && res.status !== 200) {
            Message({
                message: res.info || 'Error',
                type: 'error',
                duration: 5 * 1000
            })

            if (res.code === 50008 || res.code === 50012 || res.code === 50014) {
                // to re-login
                MessageBox.confirm('You have been logged out, you can cancel to stay on this page, or log in again', 'Confirm logout', {
                    confirmButtonText: 'Re-Login',
                    cancelButtonText: 'Cancel',
                    type: 'warning'
                }).then(() => {
                    store.dispatch('user/resetToken').then(() => {
                        location.reload()
                    })
                })
            }

            if (res.status == 300) {
                return res
            } else {
                return Promise.reject(new Error(res.info || 'Error'))
            }
        } else {
            return res
        }
    },
    error => {
        console.log('err' + error) // for debug
        Message({
            message: error.message,
            type: 'error',
            duration: 5 * 1000
        })
        return Promise.reject(error)
    }
)

export default service