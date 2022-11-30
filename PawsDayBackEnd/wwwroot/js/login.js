const HOME_PAGE = '/'
const api =
{
    login: '/AuthApi/Login'
}
const apiCaller =
{
    login:
        //function
        (loginQuery) => httpPost(api.login, loginQuery)
}

const authLoginVue = new Vue({
  el: '#authLogin',
  data: {
    login: {
      userName: '',
      password: ''
    }
  },
  methods: {
    loginBtn() {
      handleLogin({ ...this.login })
    }
  }
})



function handleLogin(loginQuery) {
  //httpPost(url,夾帶login資訊)
  apiCaller.login(loginQuery)
  .then((res) => {
      if (res.status === 20000) {
      //把response回傳資料token接回來並放入cookie
      const { token, expireTime } = res.data
      setToken(token, expireTime)
      redirectToHome()
    }
  })
}

function setToken(token, expire) {
  Cookies.set('token', token, {
    expires: new Date(expire * 1000)
  })
}

function redirectToHome() {
  window.location.href = HOME_PAGE
}
