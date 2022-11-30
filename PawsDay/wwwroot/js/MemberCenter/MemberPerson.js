let city = []
let memberCounty = 0
let memberArea = 0
let name = document.querySelector('#name')
let nickname = document.querySelector('#nickname')
let sex = document.querySelector('#sex')
let birth = document.querySelector('#birth')
let citySelectDom = document.querySelector('#address-city')
let districtSelectDom = document.querySelector('#address-area')
let address = document.querySelector('#address-text')
let phone = document.querySelector('#phone')
let email = document.querySelector('#email')

let btn = document.querySelector('#submit-btn')

let nameError = document.querySelector('#nameError')
let sexError = document.querySelector('#sexError')
let areaError = document.querySelector('#areaError')
let addressError = document.querySelector('#addressError')
let phoneError = document.querySelector('#phoneError')
let emailError = document.querySelector('#emailError')


window.onload = function () {
    btn.disabled = true
    getcity()
}

function getcity() {
    let url = "/api/MemberCenterCityWebApi/GetCityList"

    fetch(url)
        .then(res => res.json())
        .then(response => {
            //轉換資料
            city = response.data.countyDTO.map(d => {
                return {
                    countyId: d.countyId,
                    county: d.county,
                    areas: d.areas
                }
            })

            memberCounty = response.data.personCountyListDTO.countyId
            memberArea = response.data.personCountyListDTO.districtId

            Cleancity()
            Cleandistrict()
            Getcity()
            GetArea()
        })
        .catch(ex => {
            console.log(ex)
        })
}


citySelectDom.addEventListener('change', function (event) {   
    Cleandistrict()
    GetArea()
    areaError.innerText = '此欄位必填'
    CheckArea()
})

name.addEventListener('input', function () {
    if ((name.value).trim() == '') {
        nameError.innerText = '此欄位必填'
        btn.disabled = true
    }
    else if ((name.value).length>20) {
        nameError.innerText = '名字不能多於20字'
        btn.disabled = true
    }
    else {
        nameError.innerText = ''
        btn.disabled = false
    }
    CheckArea()
})

nickname.addEventListener('input', function () {
    btn.disabled = false
    CheckArea()
})

sex.addEventListener('change', function () {    
    btn.disabled = false
    sexError.innerText =''
    CheckArea()
})

if (sex.value == '-請選擇-') {
    sexError.innerText = '此欄位必填'
    btn.disabled = true
}

birth.addEventListener('change', function () {
    btn.disabled = false
    CheckArea()
})


districtSelectDom.addEventListener('change', function () {
    CheckArea()
    if (districtSelectDom.value != '-行政區-') {
        areaError.innerText = ''
        btn.disabled = false
    }
})

address.addEventListener('input', function () {
    if ((address.value).trim() == '') {
        addressError.innerText = '此欄位必填'
        btn.disabled = true
    }
    else {
        addressError.innerText = ''
        btn.disabled = false
    }
    CheckArea()
})

phone.addEventListener('input', function () {
    let role = /^09\d{8}$/
    if (phone.value == '') {
        phoneError.innerText = '此欄位必填'
        btn.disabled = true
    }
    else if (!role.test(phone.value)) {
        phoneError.innerText = '電話號碼格式錯誤'
        btn.disabled = true
    } else {
        phoneError.innerText = ''
        btn.disabled = false
    }
    CheckArea()
})

email.addEventListener('input', function () {
    let role = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z]+$/
    if (email.value == '') {
        emailError.innerText = '此欄位必填'
        btn.disabled = true
    }
    else if (!role.test(email.value)) {
        emailError.innerText = '信箱格式錯誤'
        btn.disabled = true
    } else {
        emailError.innerText = ''
        btn.disabled = false
    }
    CheckArea()
})

function CheckArea() {
    if (districtSelectDom.value == '-行政區-') {
        btn.disabled = true
    }
    else {
        btn.disabled = false
        areaError.innerText=''
    }
}

function Cleandistrict() {
    districtSelectDom.innerText = ''
    let option = document.createElement('option')
    option.innerText = '-行政區-'
    option.selected = true
    option.disabled = true
    districtSelectDom.appendChild(option)
}

function Cleancity() {
    citySelectDom.innerText = ''
    let option = document.createElement('option')
    option.innerText = '-縣市-'
    option.selected = true
    option.disabled = true
    citySelectDom.appendChild(option)
}
function Getcity() {
    
    city.forEach(x => {
        
        let option = document.createElement('option')
        option.innerText = x.county
        option.value = x.countyId
        if (option.value == memberCounty) {
            option.selected = true
        }
        citySelectDom.appendChild(option)
    })
}
function GetArea() {
    let countyArray = city.filter(item => item.countyId == citySelectDom.value)

    countyArray[0].areas.forEach(x => {
        let option = document.createElement('option')
        option.innerText = x.district
        option.value = x.districtId
        if (option.value == memberArea) {
            option.selected = true
        }
        districtSelectDom.appendChild(option)
    })
}
