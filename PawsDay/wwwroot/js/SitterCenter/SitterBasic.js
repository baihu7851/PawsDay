let name = document.querySelector('#petsitter-name')
let save = document.querySelector('#save')

name.addEventListener('change', (target) => {
    let span = target.target.parentElement.querySelector('span')
    if (target.target.value == '' || target.target.legnth > 20) {
        span.innerText = "請輸入20字以內保姆暱稱"
        save.classList.add('cannotsave')
        save.style = "pointer-events: none"
    }
    else {
        span.innerText = ""
        save.classList.remove('cannotsave')
        save.style = "pointer-events: auto"

    }
})


let editorbox = document.querySelector('.editorbox')
let editorspan = editorbox.querySelector('span')
editorbox.addEventListener('mouseover', () => {
    if (editor.getData() == ''  ) {
        editorspan.innerText = "請填寫50~200字保姆自我介紹"
        save.classList.add('cannotsave')
        save.style = "pointer-events: none"
    }
    else if (editor.getData().length < 50) {
        editorspan.innerText = "低於50字，請填寫50~200字保姆自我介紹"
        save.classList.add('cannotsave')
        save.style = "pointer-events: none"
    }
    else if (editor.getData().length > 200) {
        editorspan.innerText = "超過字數限制，請填寫50~200字保姆自我介紹"
        save.classList.add('cannotsave')
        save.style = "pointer-events: none"
    }
    else {
        editorspan.innerText = ""
        save.classList.remove('cannotsave')
        save.style = "pointer-events: auto"

    }
})