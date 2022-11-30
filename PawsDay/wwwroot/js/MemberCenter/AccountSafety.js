const passwordRegexp = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$/
const btn = document.querySelector('#save-btn')
const password = new Vue({
    el: '#password',
    data: {
        inputData: {
            oldPassword: '',
            newPassword: '',
            newCheckPassword: '',
        },
        inputDataCheck: {
            oldPasswordError: false,
            oldPasswordErrorMsg: '',
            newPasswordError: false,
            newPasswordErrorMsg: '',
            newCheckPasswordError: false,
            newCheckPasswordErrorMsg: '',
            ErrorMsg:''
        },        
    },
    computed: {
        isVerify() {
            for (let prop in this.inputDataCheck) {
                if (this.inputDataCheck[prop] == true) {
                    return false
                }
            }
            for (let prop in this.inputData) {
                if (this.inputData[prop] == '') {
                    return false
                }
            }
            if (this.inputData.newPassword != this.inputData.newCheckPassword) {
                inputDataCheck.ErrorMsg = '新密碼與確認密碼不相同'
                return false
            }
            if (this.inputData.newPassword == this.inputData.oldPassword) {
                inputDataCheck.ErrorMsg = '新密碼不可與舊密碼相同'
                return false
            }

            return true
        }
    },
    watch: {
        'inputData.oldPassword': {
            handler: function () {
                if (this.inputData.oldPassword == '') {
                    this.inputDataCheck.oldPasswordError = true
                    this.inputDataCheck.oldPasswordErrorMsg = '密碼不可空白'
                }
                else if (this.inputData.oldPassword.length < 6) {
                    this.inputDataCheck.oldPasswordError = true
                    this.inputDataCheck.oldPasswordErrorMsg = '帳號不可以少於6碼'
                } else if (this.inputData.oldPassword.length > 12) {
                    this.inputDataCheck.oldPasswordError = true
                    this.inputDataCheck.oldPasswordErrorMsg = '帳號不可以多於12碼'
                } else if (!passwordRegexp.test(this.inputData.oldPassword)) {
                    this.inputDataCheck.oldPasswordError = true
                    this.inputDataCheck.oldPasswordErrorMsg = '密碼最少要有一個英文與數字'
                } else {
                    this.inputDataCheck.oldPasswordError = false
                    this.inputDataCheck.oldPasswordErrorMsg = ''
                }
            }

        },
        'inputData.newPassword': {
            handler: function () {
                if (this.inputData.newPassword == '') {
                    this.inputDataCheck.newPasswordError = true
                    this.inputDataCheck.newPasswordErrorMsg = '密碼不可空白'
                }
                else if (this.inputData.newPassword.length < 6) {
                    this.inputDataCheck.newPasswordError = true
                    this.inputDataCheck.newPasswordErrorMsg = '帳號不可以少於6碼'
                } else if (this.inputData.newPassword.length > 12) {
                    this.inputDataCheck.newPasswordError = true
                    this.inputDataCheck.newPasswordErrorMsg = '帳號不可以多於12碼'
                } else if (!passwordRegexp.test(this.inputData.newPassword)) {
                    this.inputDataCheck.newPasswordError = true
                    this.inputDataCheck.newPasswordErrorMsg = '密碼最少要有一個英文與數字'
                } else if (this.inputData.newPassword == this.inputData.oldPassword) {
                    this.inputDataCheck.newPasswordError = true
                    this.inputDataCheck.newPasswordErrorMsg = '不可與舊密碼相同'
                } else  {
                    this.inputDataCheck.newPasswordError = false
                    this.inputDataCheck.newPasswordErrorMsg = ''
                }
            }

        },
        'inputData.newCheckPassword': {
            handler: function () {
                if (this.inputData.newCheckPassword == '') {
                    this.inputDataCheck.newCheckPasswordError = true
                    this.inputDataCheck.newCheckPasswordErrorMsg = '密碼不可空白'
                }
                else if (this.inputData.newCheckPassword.length < 6) {
                    this.inputDataCheck.newCheckPasswordError = true
                    this.inputDataCheck.newCheckPasswordErrorMsg = '帳號不可以少於6碼'
                } else if (this.inputData.newCheckPassword.length > 12) {
                    this.inputDataCheck.newCheckPasswordError = true
                    this.inputDataCheck.newCheckPasswordErrorMsg = '帳號不可以多於12碼'
                } else if (!passwordRegexp.test(this.inputData.newCheckPassword)) {
                    this.inputDataCheck.newCheckPasswordError = true
                    this.inputDataCheck.newCheckPasswordErrorMsg = '密碼最少要有一個英文與數字'
                } else if (this.inputData.newCheckPassword == this.inputData.oldPassword) {
                    this.inputDataCheck.newCheckPasswordError = true
                    this.inputDataCheck.newCheckPasswordErrorMsg = '不可與舊密碼相同'
                } else {
                    this.inputDataCheck.newCheckPasswordError = false
                    this.inputDataCheck.newCheckPasswordErrorMsg = ''
                }
            }

        },
    }
})