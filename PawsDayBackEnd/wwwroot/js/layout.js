

//(function IIFE() {
const api = {
    logout: '/AuthApi/Logout',
    User: '/AuthApi/GetUserInfo'
}

const apiCaller = {
    logout: (query) => httpPost(api.logout, query),
    user: () => httpGet(api.User)
}

let logoutModalVue = new Vue({
    el: '#logoutModal',
    methods: {
        logoutBtn() {
            const query = { token: getToken() }
            apiCaller.logout(query)
                .then((res) => {
                    removeToken()
                    redirectToLogin()
                })
        }
    }
})

const userinfo_jimmy = new Vue({
    el: '#userbox',
    data: {
        name: undefined,
        imgurl: 'https://res.cloudinary.com/dnsu1sjml/image/upload/v1666031374/paw_kt6ck3.png'
    },
    created() {
        this.getuserinfo()
    },
    methods: {
        getuserinfo() {
            apiCaller.user()
                .then(res => {
                    if (res.status === 20000) {
                        this.name = res.data.name
                        this.imgurl = res.data.imgUrl


                    }
                })
        }
    }
})

function getToken() {
    return Cookies.get('token')
}

function removeToken() {
    Cookies.remove('token')
}

function redirectToLogin() {
    const LOGIN_PAGE = '/login'
    window.location.href = LOGIN_PAGE
}


//})()