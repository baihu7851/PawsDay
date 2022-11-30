let stars = document.getElementById('order-feature-star')
let input = document.querySelector('.feature-star-input')
let btn = document.querySelector('#submit-btn')
let text = document.querySelector('.pawsday-textarea')
let msg = document.querySelector('#error-message')

window.onload = function () {
    CheckButton()
}

stars.addEventListener('click', function (event) {
    if (event.target.localName == "i") {
        event.target.style.color = 'var(--clr-border)'
        console.log(event.target.title)
        input.value = event.target.title
        var prev = event.target.previousElementSibling;
        while (prev) {
            prev.style.color = 'var(--clr-border)'
            prev = prev.previousElementSibling;
        }
        CheckButton()
    }
})
stars.addEventListener('mouseover', function (event) {
    if (event.target.localName == "i") {
        event.target.style.color = 'var(--clr-primary)'
        var prev = event.target.previousElementSibling
        while (prev) {
            prev.style.color = 'var(--clr-primary)'
            prev = prev.previousElementSibling;
        }
        var next = event.target.nextElementSibling
        while (next) {
            next.style.color = 'var(--clr-bg-g)'
            next = next.nextElementSibling;
        }

    }

})
stars.addEventListener('mouseout', function (event) {
    if (event.target.localName == "i" && event.target.style.color != 'var(--clr-border)') {
        event.target.style.color = 'var(--clr-bg-g)'
        var prev = event.target.previousElementSibling;
        while (prev) {
            prev.style.color = 'var(--clr-bg-g)'
            prev = prev.previousElementSibling;
        }
        input.value = ''
    }
    CheckButton()
})

text.addEventListener('input', function () {
    CheckButton()
    if ((text.value).length > 100) {
        msg.innerHTML = '字數不能超過100'
    } else if ((text.value).trim() == '') {
        msg.innerHTML = '內容不能空白'
    } else {
        msg.innerHTML = ''    
    }


})

function CheckButton() {
    if (text.value != '' && input.value != '' && (text.value).length < 100 && (text.value).trim()!='') {
        btn.disabled = false
        btn.style.cursor = 'pointer'
    }
    else {
        btn.disabled = true
        btn.style.cursor = 'not-allowed'
    }
}