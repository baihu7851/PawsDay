const currentpage = document.querySelector('.currenttitle').innerText
const ul = document.querySelector('.member-function-list')
const sheetnames = ul.querySelectorAll('p')
let imginput = document.getElementById('profileimginput')

sheetnames.forEach(sheet => {
    if (sheet.innerText == currentpage) {
        sheet.parentElement.parentElement.classList.add('this-a')
    }
})

imginput.addEventListener('change', () => {
    
    //imgbox=要顯示的區域
    let file = imginput.files[0]
    let reader = new FileReader
    let img = document.querySelector('.profileimg')
    reader.onload = function (target) {
        img.src = target.target.result
    }
    reader.readAsDataURL(file)
    postImg()
})


//window.onload = function () {
//    //照片上傳預覽
//    let imginput = document.getElementById('profileimginput')
//    console.log(img)

//    imginput.addEventListener('change', () => {
//        let img = document.querySelector('.profileimg')
//        console.log(img)
//        //imgbox=要顯示的區域
//        let file = imginput.files[0]
//        let reader = new FileReader
//        reader.onload = function (target) {
//            img.src = target.target.result
//        }
//        reader.readAsDataURL(file)
//        postImg()
//    })

//}




//上傳到Cloidinary
function postImg() {
    let url = "/api/UploadImage"
    const formData = new FormData();
    let updateimgs = [] //接資料用
    let imginput = document.getElementById('profileimginput')
    formData.append("file", imginput.files[0])
    updateimgs.push(formData)

    fetch(url, {
        method: 'POST',
        body: formData,
    })
        .then(res => res.json())
        .then(response => {
            console.log(response)
            updateimgs = response.data.map(r => {
                return r
            })
            document.querySelector('.profileimg').src = updateimgs[0]
            UpdateImage()
        })
        .catch(ex => {
            console.log(ex)
        })

}


//更新照片
function UpdateImage() {
    let userid = document.querySelector('.userid').id
    let img = document.querySelector('.profileimg').src

    data = {
        Userid: userid,
        Image: img
    }

    let url = '/api/MemberCenterWebApi/UpdateUserImage'

    fetch(url, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    })
        .then(res => res.json())
        .then(response => {
            console.log(response)
        })
        .catch(ex => {
            console.log(ex)
        })
}