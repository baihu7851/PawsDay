let templatebtn = document.querySelectorAll('.templatebtn')
templatebtn.forEach(btn => {
    btn.addEventListener('click', () => {
        let oldimg = document.querySelector('.uploadimg')
        let olddelbtn = document.querySelector('.deleteimg')
        if (oldimg != undefined) {
            let imgbox = oldimg.parentElement
            imgbox.removeChild(oldimg)
            imgbox.removeChild(olddelbtn)
        }
        Upload() 
    })
})
function Upload() {

    //照片上傳預覽
    const imgpreview = document.querySelector('.upload')
    const imgurl = document.querySelector('#input-3')
    imgpreview.addEventListener('change', () => {
        
        //imgbox=要顯示的區域
        let imgbox = imgpreview.parentElement.parentElement
        let img = document.createElement('img')
        img.className = 'uploadimg'
        let delbtn = document.createElement('div')
        let file = imgpreview.files[0]
        let reader = new FileReader
        reader.onload = function () {
            //把file上傳

            const formData = new FormData();
            formData.append("file", imgpreview.files[0])
            let url = "/api/UploadImage/UploadImage"
            fetch(url, {
                method: 'POST',
                body: formData,
            })
                .then(res => res.json())
                .then(response => {
                    if (response.isSuccess == true) {
                        //把照片掛入
                        img.style = 'position:absolute'
                        imgbox.append(img)
                        img.setAttribute('src', response.data[0])
                        //把刪除按鈕掛入
                        delbtn.className = 'deleteimg fa-solid fa-xmark'
                        imgbox.append(delbtn)
                        delbtn.addEventListener('click', () => {
                            imgbox.removeChild(img)
                            imgbox.removeChild(delbtn)
                        })
                        imgurl.value = response.data[0]
                        imgurl.select()
                    }
                })
                .catch(ex => {
                })
        }
        reader.readAsDataURL(file)
    })


}