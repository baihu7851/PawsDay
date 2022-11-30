var map = L.map('map').setView([25.041933456806102, 121.53627184917345], 17);

L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {

    maxZoom: 19,
    attribution: 'Build School'
}).addTo(map);

L.marker([25.041933456806102, 121.53627184917345]).addTo(map).bindPopup('PawsDay').openPopup();

const phoneRegexp = /^09[0-9]{8}$/
const mailRegexp =  /^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4})*$/

const contactVue = new Vue({
    el: '#contactVue',
    data: {
        inputData: {
            nameInput: '',
            mailInput: '',
            phoneInput: '',
            titleInput: '',
            contentInput: ''
        },
        inputCheck: {
            nameInputError: false,
            nameInputErrorMsg: '',
            mailInputError: false,
            mailInputErrorMsg: '',
            phoneInputError: false,
            phoneInputErrorMsg: '',
            titleInputError: false,
            titleInputErrorMsg: '',
            contentInputError: false,
            contentInputErrorMsg: ''
        },
    },
    computed: {
        isVerify() {
            for (let prop in this.inputCheck) {
                if (this.inputCheck[prop] == true) {
                    return false
                }
            }
            for (let prop in this.inputData) {
                if (this.inputData[prop] == '') {
                    return false
                }
            }

            return true
        }
        
    },
    watch: {
        'inputData.nameInput': {
            //immediate: true,
            handler: function () {
                if (this.inputData.nameInput == '') {
                    this.inputCheck.nameInputError = true
                    this.inputCheck.nameInputErrorMsg = '姓名不可空白'
                }
                else {
                    this.inputCheck.nameInputError = false
                    this.inputCheck.nameInputErrorMsg = ''
                }
            }

        },
        'inputData.mailInput': {
            //immediate: true,
            handler: function () {
                if (this.inputData.mailInput == '')
                {
                    this.inputCheck.mailInputError = true
                    this.inputCheck.mailInputErrorMsg = '信箱不可空白'
                }
                else if (!mailRegexp.test(this.inputData.mailInput))
                {
                    this.inputCheck.mailInputError = true
                    this.inputCheck.mailInputErrorMsg = '請輸入正確信箱'
                }
                else {
                    this.inputCheck.mailInputError = false
                    this.inputCheck.mailInputErrorMsg = ''
                }
            }

        },
        'inputData.phoneInput': {
            //immediate: true,
            handler: function () {
                if (this.inputData.phoneInput == '') {
                    this.inputCheck.phoneInputError = true
                    this.inputCheck.phoneInputErrorMsg = '電話不可空白'
                }
                else if (!phoneRegexp.test(this.inputData.phoneInput)) {
                    this.inputCheck.phoneInputError = true
                    this.inputCheck.phoneInputErrorMsg = '請輸入09開頭，共10碼手機號碼'
                } else {
                    this.inputCheck.phoneInputError = false
                    this.inputCheck.phoneInputErrorMsg = ''
                }
            }

        },
        'inputData.titleInput': {
            //immediate: true,
            handler: function () {
                if (this.inputData.titleInput == '') {
                    this.inputCheck.titleInputError = true
                    this.inputCheck.titleInputErrorMsg = '主旨不可空白'
                }
                else {
                    this.inputCheck.titleInputError = false
                    this.inputCheck.titleInputErrorMsg = ''
                }
            }
        },
        'inputData.contentInput': {
            //immediate: true,
            handler: function () {
                if (this.inputData.contentInput == '') {
                    this.inputCheck.contentInputError = true
                    this.inputCheck.contentInputErrorMsg = '內容不可空白'
                }
                else {
                    this.inputCheck.contentInputError = false
                    this.inputCheck.contentInputErrorMsg = ''
                }
            }
        }
    }
})