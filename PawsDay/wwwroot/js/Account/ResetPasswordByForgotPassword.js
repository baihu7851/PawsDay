const passwordRegexp = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$/

const password = new Vue({
    el: '#password',
    data: {
        inputData: {
            newPassword: '',
            newCheckPassword: '',
        },
        inputDataCheck: {
            newPasswordError: false,
            newPasswordErrorMsg: '',
            newCheckPasswordError: false,
            newCheckPasswordErrorMsg: ''
        },
        isDifferentPasswordMsg: ''
    },
    computed: {
        isVerify() {
            for (let prop in this.inputDataCheck) {
                if (this.inputDataCheck[prop] == true) {
                    this.isDifferentPasswordMsg = ''
                    return false
                }
            }
            for (let prop in this.inputData) {
                if (this.inputData[prop] == '') {
                    this.isDifferentPasswordMsg = ''
                    return false
                }
            }
            if (this.inputData.newPassword !== this.inputData.newCheckPassword) {
                this.isDifferentPasswordMsg = '確認輸入兩次相同的密碼'
                return false
            }
            this.isDifferentPasswordMsg = ''

            return true
        },

    },
    watch: {

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
                }
                else {
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
                }
                else {
                    this.inputDataCheck.newCheckPasswordError = false
                    this.inputDataCheck.newCheckPasswordErrorMsg = ''
                }
            }

        },
    },
})