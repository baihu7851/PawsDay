const btns = document.querySelectorAll('.service-btn')
const sheets = document.querySelectorAll('.container-illustrate-body')
const productbtn = document.querySelector('.product-info')
const servicebtn = document.querySelector('.service-info')
const buyerbtn = document.querySelector('.buyer-info')
const productsheet = document.querySelector('.product-sheet')
const servicesheet = document.querySelector('.service-sheet')
const buyersheet = document.querySelector('.buyer-sheet')

window.onload = function () {
    sheets.forEach(sheet => { sheet.style = 'display:none' })
    productsheet.style = 'display:block'
    productbtn.classList.add('this-page')
}

// 控制頁籤
productbtn.addEventListener('click', (target) => {
    clearsheetfocus()
    productsheet.style = 'display:block'
    target.target.classList.add('this-page')
})
servicebtn.addEventListener('click', (target) => {
    clearsheetfocus()
    servicesheet.style = 'display:block'
    target.target.classList.add('this-page')
})
buyerbtn.addEventListener('click', (target) => {
    clearsheetfocus()
    buyersheet.style = 'display:block'
    target.target.classList.add('this-page')
})

function clearsheetfocus() {
    sheets.forEach(sheet => { sheet.style = 'display:none' })
    btns.forEach(btn => { btn.classList.remove('this-page') })
}


let options = document.querySelectorAll('option')
let select = document.querySelector('#cancel-select')
let btn = document.querySelector('#cancel-btn')
let cancelbtn = document.querySelector('#cancel-model')

select.addEventListener('change', function () {
    Check()
})
cancelbtn.addEventListener('click', function () {
    Check()
})

function Check() {
    if (select.value == '-選擇取消原因-') {
        btn.disabled = true
        btn.style.cursor = 'not-allowed'
    }
    else {
        btn.disabled = false
        btn.style.cursor = 'pointer'
    }
}