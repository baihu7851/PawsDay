let bank = document.querySelector('#bankid')
let account = document.querySelector('#accountid')
let save = document.querySelector('#save-accountinfo')

window.onload = function () {
    save.classList.add('cannotsave')

}

account.addEventListener('input', (target) => {
    let span = target.target.parentElement.querySelector('span')
    if (account.value == '' ||account.value.length<10||account.value.length>16 || bank.value=='') {
        span.innerText = "請輸入10~16位收款帳號"
        save.classList.add('cannotsave')
        save.style = "pointer-events: none"
    }
    else if (!Number(account.value)) {
        span.innerText = "請輸入數字"
        save.classList.add('cannotsave')
        save.style = "pointer-events: none"
    }
    else {
        span.innerText = ""
        save.classList.remove('cannotsave')
        save.style = "pointer-events: auto"
    }
})

