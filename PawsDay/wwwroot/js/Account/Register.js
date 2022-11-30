
// 宣告





// DOM

let citySelectDom = document.querySelector('#address-city');
let districtSelectDom = document.querySelector('#address-area');



// window.onload
window.onload = function () {

}


// function


// function CreateCountyArray() {
//     // let countyArray = areaJson.filter(item => item.CountyId == dataCountyId);
//     console.log(areaJson);
//     // countyArray = areaJson.map(x => { CountyId = x.CountyId, County = x.County });
//     // console.log('-----------------')
//     // console.log(countyArray)
// }

// function cityChange(event) {


//     let dataCountyId = citySelectDom.selectedOptions[0].getAttribute("data-countyid");

//     initDistrict()

//     let countyArray = areaJson.filter(item => item.CountyId == dataCountyId)
//     countyArray.forEach((c, index) => {
//         c.Areas.forEach((d, index) => {
//             let district = document.createElement('option');
//             district.value = d.DistrictId;
//             district.text = d.District;
//             districtSelectDom.add(district, null)
//         })
//     })
//     districtSelectDom.disabled = false;
// }

// function initDistrict() {
//     districtSelectDom.innerHTML = ""
//     let district = document.createElement('option');
//     district.text = '-行政區-';
//     district.setAttribute('selected', '')
//     district.setAttribute('disabled', '')
//     districtSelectDom.add(district, null);
// }



// function disabledTrue(select) {
//     select.disabled = true;
// }



// Vue

const registerVue = new Vue({

    el: '#register-vue',
    data: {
        inputData: {
            name: '',
            sex: '',
            countySelectVal: '',
            county: areaJson,
            districtSelectVal: '',
            district: '',
            address: '',
            phone: ''
        },
        inputDataCheck: {
            nameError: true,
            nameErrorMsg: '',
            sexError: true,
            sexErrorMsg: '',
            countyError: true,
            countyErrorMsg: '縣市不得為空',
            districtError: true,
            districtErrorMsg: '',
            addressError: true,
            addressErrorMsg: '',
            phoneError: true,
            phoneErrorMsg: ''
        }
    },
    methods: {

    },
    computed: {
        isVerify() {
            for (let prop in this.inputDataCheck) {
                if (this.inputDataCheck[prop] == true) {
                    return false
                }
            }
            return true
        }
    },
    watch: {
        'inputData.countySelectVal': {
            // immediate: true,
            handler: function () {
                this.inputData.district = this.inputData.county.filter(x => x.CountyId == this.inputData.countySelectVal)[0].Areas;
                // console.log(selectCountyData)

                if (this.inputData.countySelectVal == '') {
                    this.inputDataCheck.countyError = true;
                    this.inputDataCheck.countyErrorMsg = '縣市不得為空'
                }
                else {
                    this.inputDataCheck.countyError = false;
                    this.inputDataCheck.countyErrorMsg = '';
                }
            }

        },
        // name: '',sex: '',countySelectVal: '',county: areaJson,districtSelectVal: '',district: '',address: '',phone: ''
        'inputData.name': {
            immediate: true,
            handler: function () {
                if (this.inputData.name == '') {
                    this.inputDataCheck.nameError = true;
                    this.inputDataCheck.nameErrorMsg = '姓名不得為空'
                } else if (this.inputData.name.length > 20) {
                    this.inputDataCheck.nameError = true;
                    this.inputDataCheck.nameErrorMsg = '姓名長度不得大於 20 個字元'
                } else {
                    this.inputDataCheck.nameError = false;
                    this.inputDataCheck.nameErrorMsg = '';
                }
            }
        },
        'inputData.sex': {
            immediate: true,
            handler: function () {
                if (this.inputData.sex == '') {
                    this.inputDataCheck.sexError = true;
                    this.inputDataCheck.sexErrorMsg = '性別不得為空'
                } else {
                    this.inputDataCheck.sexError = false;
                    this.inputDataCheck.sexErrorMsg = '';
                }
            }
        },
        'inputData.districtSelectVal': {
            immediate: true,
            handler: function () {

                if (this.inputData.districtSelectVal == '') {
                    this.inputDataCheck.districtError = true;
                    this.inputDataCheck.districtErrorMsg = '行政區不得為空'
                }
                else {
                    this.inputDataCheck.districtError = false;
                    this.inputDataCheck.districtErrorMsg = '';
                }
            }

        },
        'inputData.address': {
            immediate: true,
            handler: function () {
                if (this.inputData.address == '') {
                    this.inputDataCheck.addressError = true;
                    this.inputDataCheck.addressErrorMsg = '地址不得為空'
                } else if (this.inputData.name.length > 50) {
                    this.inputDataCheck.addressError = true;
                    this.inputDataCheck.addressErrorMsg = '地址長度不得大於 50 個字元'
                } else {
                    this.inputDataCheck.addressError = false;
                    this.inputDataCheck.addressErrorMsg = '';
                }
            }
        },
        'inputData.phone': {
            immediate: true,
            handler: function () {
                let phoneRegex = /^09[0-9]{8}$/;
                if (this.inputData.phone == '') {
                    this.inputDataCheck.phoneError = true;
                    this.inputDataCheck.phoneErrorMsg = '電話不得為空'
                } else if (!phoneRegex.test(this.inputData.phone)) {
                    this.inputDataCheck.phoneError = true;
                    this.inputDataCheck.phoneErrorMsg = '不符合手機格式'
                } else {
                    this.inputDataCheck.phoneError = false;
                    this.inputDataCheck.phoneErrorMsg = '';
                }
            }
        }
    }







})


