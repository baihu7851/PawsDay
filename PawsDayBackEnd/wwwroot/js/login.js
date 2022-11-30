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
  //httpPost(url,���alogin��T)
  apiCaller.login(loginQuery)
  .then((res) => {
      if (res.status === 20000) {
      //��response�^�Ǹ��token���^�Өé�Jcookie
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
