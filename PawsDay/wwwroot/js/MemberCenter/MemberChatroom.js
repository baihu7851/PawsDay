let textarea = document.querySelector('#contact-us')
let btn = document.querySelector('#submit-btn')
let msg = document.querySelector('#error-message')
let sitterId = document.querySelector('#sitterid')
let order = document.querySelector('.order-message')
let nullbody = document.querySelector('.table-body-null')

window.onload = function () {
    CheckButton()
    textarea.innerHTML=''
}

textarea.addEventListener('input', function () {
    CheckButton()
    if ((textarea.value).length > 200) {
        msg.innerHTML='字數不能超過200'
    }
    else if ((textarea.value).trim() == '') {
        msg.innerHTML = '訊息內容不能空白'
    } else{
        msg.innerHTML =''
    }
})

btn.addEventListener('click', function () {
    let url = "/api/MemberCenterWebApi/ChatroomSisterDetail"

    let data = {
        //已先建立基本資料
        SitterId: sitterId.value,
        Message: textarea.value
    }
    let time

    fetch(url, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    })
        .then(res => res.json())
        .then(response => {
            time = response.data.time
            console.log(response)
            AddSpeak(time)
            textarea.value = ''
            CheckButton()
            nullbody.style.display='none'
        }) 
    
})

function AddSpeak(timestring) {   
    let image = document.querySelector('.profileimg').src
    console.log(image)
    let ask = document.createElement('div')
    ask.classList = 'order-message-ask'
    let img = document.createElement('img')
    img.src = image
    img.classList ='userid'
    ask.appendChild(img)    
    let askp = document.createElement('div')
    askp.classList = 'message-ask-p'
    let before = document.createElement('div')
    before.classList = 'message-ask-before'
    askp.appendChild(before)
    let p = document.createElement('p')
    p.innerText = textarea.value    
    askp.appendChild(p)
    let time = document.createElement('p')
    time.classList = 'message-ask-date'
    time.innerText = timestring
    askp.appendChild(time)

    ask.appendChild(askp)
    order.prepend(ask)               
}



function CheckButton() {
    if (textarea.value != '' && (textarea.value).length < 200 && (textarea.value).trim()!='') {
        btn.disabled = false
        btn.style.cursor = 'pointer'
    }
    else {
        btn.disabled = true
        btn.style.cursor = 'not-allowed'
    }
}
function StringLength(str) {
    return str.replace(/[^\x00-\xff]/g, "xx").length;
}