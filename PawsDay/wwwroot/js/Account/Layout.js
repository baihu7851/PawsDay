// 宣告

// DOM
let lineLoginBtn = document.querySelector('#line-login-btn');
let googleLoginBtn = document.querySelector('#google-login-btn');
let loginRegister = document.getElementById('login-register');
let loginModal = document.getElementById('login');
let forgotModal = document.getElementById('forgot');
let signUpModal = document.getElementById('sign-up');
let accountMessageModal = document.getElementById('account-message');

// BootStrap Modal Control object
let loginRegisterBS = new bootstrap.Modal(loginRegister, {
    keyboard: false
});
let loginModalBS = new bootstrap.Modal(loginModal, {
    keyboard: false
});
let forgotModalBS = new bootstrap.Modal(forgotModal, {
    keyboard: false
});
let signUpModalBS = new bootstrap.Modal(signUpModal, {
    keyboard: false
});
let accountMessageModalBS = new bootstrap.Modal(accountMessageModal, {
    keyboard: false
});

// window.onload
lineLoginBtn.addEventListener('click', LineLogin);
googleLoginBtn.addEventListener('click', GoogleLogin);

getNowUrl();


if (isLoginPop) {
    loginRegisterBS.show();
}
if (isLoginEmailPop) {
    loginModalBS.show();
}
if (isForgotPop) {
    forgotModalBS.show();
}
if (isSignUpPop) {
    signUpModalBS.show();
}
if (isAccountMsgPop) {
    accountMessageModalBS.show();
}

if (isLoginPop == false) {
    loginRegisterBS.hide();
    loginModalBS.hide();
    forgotModalBS.hide();
    accountMessageModalBS.hide();
    signUpModalBS.hide();
}

// function


function getNowUrl() {
    let thisIsRouteUrl = location.pathname;

    Cookies.set('thisIsNowUrl', thisIsRouteUrl, { path: '/' });
}


const signUpPartial = new Vue({
    el: "#signUpPartial",
    data: {
        inputEmailSignUp: {
            email: '',
            password: '',
            agree: false,
        },
        inputEmailSignUpCheck: {
            checkboxError: false,
            checkboxErrorMsg: '',
            emailError: false,
            emailErrorMsg: '',
            passwordError: false,
            passwordErrorMsg: ''
        }
    },
    computed: {
        isInputEmailSignUpVerify() {
            for (let prop in this.inputEmailSignUpCheck) {
                if (this.inputEmailSignUpCheck[prop] == true) {
                    return false
                }
            }
            return true
        }
    },
    watch: {
        'inputEmailSignUp.email': {
            immediate: true,
            handler: function () {
                let emailRegex = /^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4})*$/;
                if (this.inputEmailSignUp.email == '') {
                    this.inputEmailSignUpCheck.emailError = true;
                    this.inputEmailSignUpCheck.emailErrorMsg = '請輸入帳號';
                } else if (!emailRegex.test(this.inputEmailSignUp.email)) {
                    this.inputEmailSignUpCheck.emailError = true;
                    this.inputEmailSignUpCheck.emailErrorMsg = '請輸入正確格式電子郵件';
                } else {
                    this.inputEmailSignUpCheck.emailError = false;
                    this.inputEmailSignUpCheck.emailErrorMsg = '';
                }
            }
        },
        'inputEmailSignUp.password': {
            immediate: true,
            handler: function () {
                let passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$/;
                if (this.inputEmailSignUp.password == '') {
                    this.inputEmailSignUpCheck.passwordError = true;
                    this.inputEmailSignUpCheck.passwordErrorMsg = '請輸入密碼';
                } else if (this.inputEmailSignUp.password.length < 6) {
                    this.inputEmailSignUpCheck.passwordError = true;
                    this.inputEmailSignUpCheck.passwordErrorMsg = '輸入英文及數字6~12字元內組合';
                } else if (this.inputEmailSignUp.password.length > 12) {
                    this.inputEmailSignUpCheck.passwordError = true;
                    this.inputEmailSignUpCheck.passwordErrorMsg = '輸入英文及數字6~12字元內組合';
                } else if (!passwordRegex.test(this.inputEmailSignUp.password)) {
                    this.inputEmailSignUpCheck.passwordError = true;
                    this.inputEmailSignUpCheck.passwordErrorMsg = '密碼必須至少有一個數字與英文';
                } else {
                    this.inputEmailSignUpCheck.passwordError = false;
                    this.inputEmailSignUpCheck.passwordErrorMsg = '';
                }
            }
        },
        'inputEmailSignUp.agree': {
            immediate: true,
            handler: function () {
                if (this.inputEmailSignUp.agree == false) {
                    this.inputEmailSignUpCheck.checkboxError = true;
                    this.inputEmailSignUpCheck.checkboxErrorMsg = '請確認使用者條款';
                } else {
                    this.inputEmailSignUpCheck.checkboxError = false;
                    this.inputEmailSignUpCheck.checkboxErrorMsg = '';
                }
            }
        }
    },

})

