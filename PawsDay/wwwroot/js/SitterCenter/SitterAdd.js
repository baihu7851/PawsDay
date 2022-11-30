//共用宣告
let servicedata = []
let servicetimedata = []
let productimg=[]
let productid=""
let previewbtn = document.querySelector('#preview-service')
let loadbox = document.querySelector('.loadingbox')
let formbox = document.querySelector('.container-service-body')
let previewloadbox = document.querySelector('.preview-loadingbox')
let previewbody = document.querySelector('.preview-body')

window.onload = async function () {
    productid = document.querySelector('.productid').id
    if (productid != "") {
        GetProduct(productid)
        ClearNullSpan()
    }
    else {
        //如果建立新的商品不可設定下架
        document.querySelector('.producton-control').style = "pointer-events: none"
        previewbtn.innerText = '預覽上架'
        formbox.style = 'display:block'
        loadbox.style = 'display:none'
    }

    //將儲存掛在previewbtn上
    let preview = document.querySelector('#preview-service')
    preview.addEventListener('click', () => {
        //按一次後就鎖住
        preview.style = 'cursor:default'
        preview.classList.add('d-none')
        //暫時不可按預覽關閉
        document.querySelector('.close').classList.add('d-none')
        document.querySelector('.preview-header').style = "pointer-events: none"

        if (productid != "") {
            //更新基本資料
            UpdateStatus()
            //更新地區、價格、時間、照片
            PostProdcut()
            //待更新後會跑預覽

        }
        else {
            //建立基本資料，後會呼叫PostProduct+openview
            CreateProdcut()
        }
    })
}


//載入product設定
function GetProduct(id) {
    let url = `/api/SitterCenterWebApi/GetServicetDetailById?productid=${id}`

    fetch(url)
        .then(res => res.json())
        .then(response => {
            if (response.isSuccess == true) {
                //處理狀態
                if (response.data.basicInfo.status != true) {
                    statustext.classList.add('productoff')
                    statuspaws.classList.add('product-btn-off')
                    productctrl.classList.add('producton-control-off')
                    producttext()
                }
                //處理縣市
                county = response.data.county.map(item => {
                    return item
                })
                county.forEach(c => {
                    let cou = document.querySelector('.county-box').querySelector(`[data-countyid="${c}"]`)
                    cou.checked = true
                    cou.setAttribute('select', true)
                })
                //處理地區
                district = response.data.district.map(item => {
                    return item
                })
                district.forEach(d => {
                    let dis = document.querySelectorAll(`[data-districtid="${d}"]`)
                    dis.forEach(input => {
                        input.checked = true
                        input.setAttribute('select', true)
                        input.style.display = 'inline-block'
                        input.parentElement.style.display = 'inline-block'
                    })
                })
                //處理支援寵類
                let selectbox = document.querySelectorAll('.typeselection')
                let petselectbox = document.querySelectorAll('.pettypeinput')
                servicedata = response.data.priceDetail.map(r => {
                    selectbox.forEach(box => {
                        if (box.dataset.pet == r.petType && box.dataset.shape == r.shapeType) {
                            box.checked = true
                            box.parentElement.parentElement.parentElement.style.display = 'block'
                        }
                    })
                    petselectbox.forEach(box => {
                        if (box.dataset.pet == r.petType) {
                            box.checked = true
                            box.parentElement.parentElement.parentElement.style.display = 'block'
                        }
                    })
                    //傳出整包資料
                    return {
                        pet: r.petType,
                        shape: r.shapeType,
                        price: r.price,
                        nprice: r.nightPrice
                    }
                })
                //處理時間
                servicetimedata = response.data.time.map(r => {
                    //傳資料
                    return {
                        week: r.week,
                        time: r.partTime
                    }
                })
                //如果有支援的week
                servicetimedata.forEach(t => {
                    let weekid = t.week
                    //找到對應的weekbtn，觸發click
                    document.querySelector(`[data-week="${weekid}"]`).click()
                    let weekbox = document.querySelector(`[data-weekbox="${weekid}"]`)
                    //把已支援的parttime勾選出來
                    if (t.time.length == 4) { weekbox.querySelector('.allday').click() }
                    else {
                        weekbox.querySelector('.part').click()
                        t.time.forEach(pt => {
                            weekbox.querySelector(`[data-parttime="${pt}"]`).click()
                        })
                    }
                })
                //處理照片
                let imgbox = document.querySelectorAll('.image-preview')
                productimg = response.data.imageUrl.map(r => {
                    return r
                })

                for (let index = 0; index < productimg.length; index++) {
                    //掛入image-preview
                    let img = document.createElement('img')
                    let delbtn = document.createElement('div')
                    img.setAttribute('src', productimg[index])
                    img.style = 'position:absolute'
                    imgbox[index].append(img)
                    //掛入刪除btn
                    delbtn.className = 'deleteimg fa-solid fa-xmark'
                    imgbox[index].append(delbtn)
                    delbtn.addEventListener('click', () => {
                        imgbox[index].removeChild(img)
                        imgbox[index].removeChild(delbtn)
                    })
                }
                formbox.style = 'display:block'
                loadbox.style='display:none'
            }
        })
}


//建立新商品
function CreateProdcut() {
    let url = `/api/SitterCenterWebApi/UpdateProductBasic`

    data = {
        //已先建立基本資料
        Status: statustext.classList.contains('productoff') ? false : true,
        ServiceType: document.querySelector('#service-type').value,
        Introduce: editor.getData()
    }

    fetch(url, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    })
        .then(res => res.json())
        .then(response => {
            if (response.isSuccess == true) {
                productid = response.data.productId
                //取得id後開始更新
                PostProdcut()
            }
        })
        .catch(ex => {
        })
}

//更新地區、種類價格、時間、照片
function PostProdcut()
{
    let data=[]
    //存已選地區
    let districtarray = []
    let districts = document.querySelectorAll('.districtinput')
    //有select屬性的都加入array
    districts.forEach(d => {
        if (d.hasAttribute('select')) {
            let area = d.dataset.districtid
            districtarray.push(area)
        }
    })
    
    //存已選規格及價格
    let pricearray=[]
    let servicepriceboxs = document.querySelectorAll('.tbody')
    servicepriceboxs.forEach(box => {
        //先包成獨立pricedto
        let pricedto = []
        pricedto = {
            PetType: box.id.split('-')[0],
            ShapeType: box.id.split('-')[1],
            Price: box.querySelector('.normalprice').value,
            NightPrice: box.querySelector('.nightprice').value
        }
        //加入datadto(陣列)
        pricearray.push(pricedto)
    })

    //存已選時間
    let timearray=[]
    let timebox = document.querySelectorAll('.timeset')
    timebox.forEach(box => {
        let array = []
        let boxid = box.dataset.weekbox
        let timeinputs = box.querySelectorAll('.parttimeinput')
        //把有勾選的inputid加入timearray
        timeinputs.forEach(input => {
            if (input.checked) { array.push(input.dataset.parttime) }
        })
        timedata = {
            Week: boxid,
            PartTime: array
        }
        //整包加入datadto
        timearray.push(timedata)
    })

    //存照片
    let imgarray=[]
    let imgbox = document.querySelector('.image-preview-box')
    let imgs = imgbox.querySelectorAll('img')

    imgs.forEach(img => imgarray.push(img.src))

    //包裝
    data = {
        id: productid,
        district: districtarray,
        priceDetail: pricearray,
        time: timearray,
        imageUrl: imgarray

    }

    let url = `/api/SitterCenterWebApi/UpdateServicetDetailById`
    fetch(url, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    })
        .then(res => res.json())
        .then(response => {
            if (response.isSuccess == true) {
                //預覽
                Openpreview()
                document.querySelector('.save-span').innerText = '儲存成功'
                //打開按預覽關閉
                document.querySelector('.close').classList.remove('d-none')
                document.querySelector('.preview-header').style = "pointer-events: default"
            }
        })
        .catch(ex => {
            document.querySelector('.save-span').innerText = '儲存失敗'
        })
}

//更新商品基本資料
function UpdateStatus()
{
    let url = `/api/SitterCenterWebApi/UpdateProductBasic`

    data = {
        Productid: productid,
        Status: statustext.classList.contains('productoff') ? false : true,
        ServiceType: document.querySelector('#service-type').value,
        Introduce: editor.getData()
    }
    
    fetch(url, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    })
        .then(res => res)
        .then(response => {
            if (response.isSuccess == true) {
                //預覽
                Openpreview()
                document.querySelector('.save-span').innerText = '儲存成功'
            }
        })
        .catch(ex => {
            document.querySelector('.save-span').innerText = '儲存失敗'

        })
}

//取得預覽資訊
function GetPreviewdata(id) {
    let url = `/api/SitterCenterWebApi/GetPreviewData?productid=${id}`
    fetch(url)
        .then(res => res.json())
        .then(response => {
            if (response.isSuccess == true) {
                if (response.data.quantity != 0) {
                    previewbox.querySelector('.preview-discount').innerText = `選擇${response.data.quantity}個時段(含)以上享${response.data.discount}折`
                }
                else {
                    previewbox.querySelector('.preview-discount').innerText = "未有促銷優惠"
                }
                previewbox.querySelector('.service-type-content').innerHTML = response.data.serviceNotice
                previewbox.querySelector('.sitter-info').innerHTML = response.data.sitterInfo
                previewbody.style = 'display:block'
                previewloadbox.style='display:none'
            }
        })
        .catch(ex => {
        })
}



//切換上下架
const productctrl = document.querySelector('.producton-control')
const statustext = document.querySelector('.producttext')
const statuspaws = document.querySelector('.product-btn')

productctrl.addEventListener('click', () => {
    statustext.classList.toggle('productoff')
    statuspaws.classList.toggle('product-btn-off')
    productctrl.classList.toggle('producton-control-off')
    producttext()
})

function producttext() {
    if (statustext.classList.contains('productoff')) { statustext.innerText = '關閉中' }
    else { statustext.innerText = '上架中' }
}


//控制縣市及區域顯示
//把區域選項先隱藏，找出有勾選的縣市再呈現轄下區域
let counties = document.querySelectorAll('.countyinput')
let disticts = document.querySelectorAll('.districtinput')
let box = document.querySelector('.district-box')

counties.forEach(county => {
    county.addEventListener('change', () => {
        county.toggleAttribute('select')
        //change觸發區域的dataset==countyid者顯示，但如果沒checked則隱藏
        let text = county.dataset.countyid
        let dis = box.querySelectorAll(`[data-countyid="${text}"]`)
        dis.forEach(d => {
            d.style.display = 'inline-block'
            d.parentElement.style.display = 'inline-block'
            if (!county.hasAttribute('select')) {
                d.style.display = 'none'
                d.parentElement.style.display = 'none'
                //當縣市取消勾選時，記得把區域的select、勾選拿掉
                d.removeAttribute('select')
                d.checked = false
            }
        })
    })
})
//區域切換select屬性
disticts.forEach(d => {
    d.addEventListener('change', () => {
        d.toggleAttribute('select')
    })
})




// 控制pettypebox
const pettypeinput = document.querySelectorAll('.pettypeinput')
const pettypelabel = document.querySelectorAll('.pettypelabel')

pettypelabel.forEach(label => {
    label.addEventListener('click', () => {
        let thetag = label.attributes.for.value
        let targetinput = document.querySelector(`#${thetag}`)
        let targetbox = document.querySelector(`.${thetag}shapebox`)
        controltypebox(targetinput, targetbox)
    })
})
pettypeinput.forEach(input => {
    input.addEventListener('click', () => {
        let thetag = input.id
        let targetbox = document.querySelector(`.${thetag}shapebox`)
        controltypebox(input, targetbox)
    })
});
function controltypebox(input, box) {
    if (input.checked == true) { box.style = 'display:block' }
    else {
        box.style = 'display:none'
        let checkboxs = box.querySelectorAll('input')
        checkboxs.forEach(check => check.checked = false)
    }
}



//控制價格表
const pricesection = document.querySelector('#headingTwo')
const pricetemp = document.querySelector('#pricetemp')
const table = document.querySelector('.price-table')

pricesection.addEventListener('click', () => {
    //如果第一次click，觸發生成價格表+帶資料
    if (pricesection.dataset.first == "1") {
        let types = document.querySelectorAll('.typeselection')
        types.forEach(type => {
            if (type.checked == true) {CreateTempTable(type)}
        })
        InjectPricedata()
        pricesection.dataset.first="2"
    }
    //如果非第一次click，比對上方選項是否有勾選
    else 
    {
        let types = document.querySelectorAll('.typeselection')
        types.forEach(type => {
            if (type.checked == true) {
                let target = document.getElementById(`${type.dataset.pet}-${type.dataset.shape}`)
                //如果目標不存在，要長出
                if (target == null)
                {
                    CreateTempTable(type)
                }
            }
            else {
                //如果未勾選，目標卻存在，要刪除
                let target = document.getElementById(`${type.dataset.pet}-${type.dataset.shape}`)
                if (target){table.removeChild(target)}
            }
        })
    }
})
//長出價格表
function CreateTempTable(type)
{
    let temp = pricetemp.content.cloneNode(true)
    temp.querySelector('.typetext').innerText = type.id.includes('dog') ? "狗狗" : "貓咪"
    temp.querySelector('.shapetext').innerText = type.parentElement.innerText
    temp.querySelector('.tbody').id = `${type.dataset.pet}-${type.dataset.shape}`
    temp.querySelector('.normalprice').style = 'border: 1px solid #db3700'
    temp.querySelector('.nightprice').style = 'border: 1px solid #db3700'
    temp.querySelector('.normalprice').classList.add('null')
    temp.querySelector('.nightprice').classList.add('null')
    table.append(temp)
}
//找到價格表，如等於servicedata，把值帶入
function InjectPricedata()
{
    let bodys = document.querySelectorAll('.tbody')
    bodys.forEach(body => {
        servicedata.forEach(data => {
            if (body.id == `${data.pet}-${data.shape}`) {
                body.querySelector('.normalprice').value = data.price
                body.querySelector('.normalprice').style = 'border: 1px solid #bbbbbb'
                body.querySelector('.nightprice').value = data.nprice
                body.querySelector('.nightprice').style = 'border: 1px solid #bbbbbb'
                body.querySelector('.normalprice').classList.remove('null')
                body.querySelector('.nightprice').classList.remove('null')
            }
        })
    })
}



//選擇週期
const weeksection = document.querySelector('.weeksection')
const btns = document.querySelectorAll('.week')
const timetemp = document.querySelector('#timetemp')

btns.forEach(btn => {
    btn.addEventListener('click', function (target) {
        target.target.classList.add('week-selected')
        target.target.setAttribute('disabled', true)
        let weekid = target.target.id
        let weekname = target.target.innerText
        let weekdata = target.target.dataset.week
        let temp = timetemp.content.cloneNode(true)
        let div = temp.querySelector('.timeset')
        div.setAttribute('weekid', weekid)
        //設定排序
        div.style = `order:${weekdata}` 
        div.dataset.weekbox = weekdata
        //設定文字
        let day = temp.querySelector('.day') 
        day.innerText = `每周${weekname}` 
        //設定radio及label
        temp.querySelector('.allday').id = `allday${weekdata}`
        temp.querySelector('.alllabel').setAttribute('for', `allday${weekdata}`) 
        temp.querySelector('.part').id = `part${weekdata}`
        temp.querySelector('.partlabel').setAttribute('for', `part${weekdata}`)
        //設定checkboxvalue
        let checkboxs = temp.querySelectorAll('.checkbox')
        let index = 0
        checkboxs.forEach(box => {     
            box.dataset.parttime=index
            index++
            box.disabled = true //初始為不選
        })
        //設定checkboxlabel
        temp.querySelector('.parttimeinput').id = `${weekdata}-morning`
        temp.querySelector('.c-morning').setAttribute('for', `${weekdata}-morning`)
        temp.querySelector('.afternoon').id = `${weekdata}-afternoon`
        temp.querySelector('.c-afternoon').setAttribute('for', `${weekdata}-afternoon`)
        temp.querySelector('.night').id = `${weekdata}-night`
        temp.querySelector('.c-night').setAttribute('for', `${weekdata}-night`)
        temp.querySelector('.midnight').id = `${weekdata}-midnight`
        temp.querySelector('.c-midnight').setAttribute('for', `${weekdata}-midnight`)

        //控制選全天就不可選部分時段
        let radios = temp.querySelectorAll('.radio')
        radios.forEach(radio => {
            radio.name = weekid
            radio.addEventListener('click', () => {
                if (document.querySelector(`#allday${weekdata}`).checked) {
                    checkboxs.forEach(box => {
                        box.disabled = true
                        box.checked = true
                    })
                }
                else {
                    checkboxs.forEach(box => {
                        box.disabled = ''
                    })
                }
            })
        })
        //設定刪除btn:刪除timeset+恢復weekbtn
        let del = temp.querySelector('.day-delete')
        del.addEventListener('click', (target) => {
            let thisbox = target.target.parentElement
            let weekid = thisbox.getAttribute('weekid')
            let btn = document.querySelector(`#${weekid}`)
            btn.disabled = false
            btn.classList.remove('week-selected')
            weeksection.removeChild(thisbox)
        })
        weeksection.appendChild(temp)
    })

    


}) 



//清除價格
let clear = document.querySelector('#clearprice')
let step2 = document.querySelector('.step-2-sheet')
clear.addEventListener('click', () => {
    let inputs = step2.querySelectorAll('input')
    inputs.forEach(input => {
        input.value=''
    })

})


let updateimgs = []
let uploadimgUrl=""
//照片上傳預覽
const imgpreviws = document.querySelectorAll('.upload')
const imgs = document.querySelectorAll('.image-preview')
imgpreviws.forEach(imgpreview => {
    imgpreview.addEventListener('change', () => {
        //imgbox=要顯示的區域
        let imgbox = imgpreview.parentElement.parentElement
        let img = document.createElement('img')
        img.className='uploadimg'
        let delbtn = document.createElement('div')
        let file = imgpreview.files[0]
        let loadcdd = imgbox.querySelector('.cssload-square')
        loadcdd.classList.remove('d-none')
        let reader = new FileReader
        reader.onload = function () {
            //把file上傳
            
            const formData = new FormData();
            formData.append("file", imgpreview.files[0])
            let url = "/api/UploadImage"
            fetch(url, {
                method: 'POST',
                body: formData,
            })
                .then(res => res.json())
                .then(response => {
                    if (response.isSuccess == true) {
                        updateimgs = response.data.map(r => {
                            return r
                        })
                        //把照片掛入
                        img.style = 'position:absolute'
                        imgbox.append(img)
                        img.setAttribute('src', updateimgs[0])
                        loadcdd.classList.add('d-none')
                        //把刪除按鈕掛入
                        delbtn.className = 'deleteimg fa-solid fa-xmark'
                        imgbox.append(delbtn)
                        delbtn.addEventListener('click', () => {
                            imgbox.removeChild(img)
                            imgbox.removeChild(delbtn)
                        })
                    }
                })
                .catch(ex => {
                })
        }
        reader.readAsDataURL(file)
    })
})

//照片拖曳換位
imgs.forEach(img => {
    img.ondragstart = function () {
        container = this
        event.target.style.opacity = .5;
    }
    img.ondragover = function () {
        event.preventDefault();
    }
    img.ondrop = function () {
        if (container != null && container != this) {
            // 具體思路跟變量值互換一樣
            let temp = document.createElement("div");
            let box = document.querySelector('.image-preview-box')
            box.replaceChild(temp, this);   //用新建的div占據目的位置
            box.replaceChild(this, container);// 目的div放置在起始位置
            box.replaceChild(container, temp)  // 起始div放置在目的位置
        }
    }
    img.ondragend = function () {
        event.target.style.opacity = ''
    }
})




//控制預覽商品頁面
const previewbox = document.querySelector('.preview-box')
const previewbg = document.querySelector('.preview-bg')

function showpreview() {
    previewbox.style = 'height:90%'
    previewbg.style = 'display:block'
}
function hidepreview() {
    previewbox.style = 'height:0px'
    previewbg.style = 'display:none'
}

//顯示預覽
function Openpreview() {
    previewbody.style='display:none'
    //生成照片
    let imgbox = document.querySelector('.image-preview-box')
    let imgs = imgbox.querySelectorAll('img')

    let previewimgbox = previewbox.querySelector('.carousel-inner') //外殼
    imgs.forEach(img => {
        let preimgdiv = document.createElement('div')
        preimgdiv.className = 'carousel-item'
        let preimg = document.createElement('img')
        preimg.src = img.src
        preimgdiv.append(preimg)
        previewimgbox.append(preimgdiv)
    })
    document.querySelector('.carousel-item').classList.add('active')

    //標題
    let selectindex = document.querySelector('#service-type').selectedIndex
    let text = document.querySelectorAll('option')[selectindex].text
    let title = previewbox.querySelector('.previewtitle')
    title.innerText = text

    //地區
    let areabox = previewbox.querySelector('.place-span')
    let countybox = document.querySelector('.county-box')
    let areas = countybox.querySelectorAll('[select]')
    areabox.innerHTML = ''
    let dot = document.createElement('i')
    dot.className = 'fa-solid fa-location-dot'
    areabox.append(dot)
    areas.forEach(a => {
        place = document.createElement('p')
        place.className = 'commodity-place'
        place.innerText = a.parentElement.querySelector('label').innerText
        areabox.append(place)
    })

    //說明
    let intro = editor.getData()
    previewbox.querySelector('.service-intro').innerHTML=intro

    //如果是新建的product，要先傳id
    GetPreviewdata(productid)
}






//檢核各個選項是否都有選填值

//檢核服務類別
let servicetype = document.querySelector('#service-type')
let servicespan = document.querySelector('.servicetypespan')
servicetype.addEventListener('change', (target) => {
    if (servicetype.value == '請選擇服務類型')
    {
        servicespan.innerText = "請選擇服務類型"
        servicespan.classList.add('null')
    }
    else
    {
        servicespan.innerText = ""
        servicespan.classList.remove('null')
    }
})

//檢核服務縣市、地區
let countybox = document.querySelector('.county-box')
let countyspan = document.querySelector('.countybox-span')
let districtbox = document.querySelector('.district-box')
let districtspan = document.querySelector('.districtbox-span')
countybox.addEventListener('change', () => {
    if (countybox.querySelector('[select]') == null)
    {
        countyspan.innerText = "請選擇至少一種服務縣市"
        countyspan.classList.add('null')
    }
    else
    {
        countyspan.innerText = ""
        countyspan.classList.remove('null')
        if (districtbox.querySelector('[select]') == null) {
            districtspan.innerText = "請選擇至少一種服務地區"
            districtspan.classList.add('null')
        }
        else {
            districtspan.innerText = ""
            districtspan.classList.remove('null')
        }
    }
})

districtbox.addEventListener('change', () => {
    if (districtbox.querySelector('[select]') == null) {
        districtspan.innerText = "請選擇至少一種服務地區"
        districtspan.classList.add('null')
    }
    else {
        districtspan.innerText = ""
        districtspan.classList.remove('null')
    }
})

//檢核服務寵物類別、體型類別
let petbox = document.querySelector('.petbox')
let petspan = document.querySelector('.petbox-span')
let dogbox = document.querySelector('.dogbox')
let dogspan = document.querySelector('.dogbox-span')
let catbox = document.querySelector('.catbox')
let catspan = document.querySelector('.catbox-span')
petbox.addEventListener('change', () => {
    if (petbox.querySelector('input[type=checkbox]:checked') == null) {
        petspan.innerText = "請選擇至少一種寵物類別"
        petspan.classList.add('null')
    }
    else {
        petspan.innerText = ""
        petspan.classList.remove('null')
    }
})

dogbox.addEventListener('change', () => {
    if (dogbox.querySelector('input[type=checkbox]:checked') == null) {
        dogspan.innerText = "請選擇至少一種體型類別"
        dogspan.classList.add('null')
    }
    else
    {
        dogspan.innerText = ""
        dogspan.classList.remove('null')
    }
})

catbox.addEventListener('change', () => {
    if (catbox.querySelector('input[type=checkbox]:checked') == null) {
        catspan.innerText = "請選擇至少一種體型類別"
        catspan.classList.add('null')
    }
    else {
        catspan.innerText = ""
        catspan.classList.remove('null')
    }
})

//檢核服務週期
let weekbox = document.querySelector('.weeksection')
let weekspan = document.querySelector('.weekbox-span')
weekbox.addEventListener('mouseover', () => {
    if (weekbox.querySelector('.week-selected')==null) {
        weekspan.innerText = "請選擇至少一種服務週期"
        weekspan.classList.add('null')
    }
    else {
        weekspan.innerText = ""
        weekspan.classList.remove('null')
    }
})

//檢核價格框
let pricespan = document.querySelector('.pricebox-span')
pricesection.addEventListener('click', () => {
    let pricebox = document.querySelectorAll('.normalprice')
    if (pricebox.length != 0)
    {
        pricebox.forEach(box => {
            box.addEventListener('change', () => {
                if (box.value == '' || !Number(box.value)) {
                    box.style = 'border:1px solid #db3700'
                    box.classList.add('null')
                }
                else {
                    box.style = 'border:1px solid #bbbbbb'
                    box.classList.remove('null')
                }
            })
        })
        let npricebox = document.querySelectorAll('.nightprice')
        npricebox.forEach(box => {
            box.addEventListener('change', () => { 
                if (box.value == '' || !Number(box.value)) {
                    box.style = 'border:1px solid #db3700'
                    box.classList.add('null')
                }
                else {
                    box.style = 'border:1px solid #bbbbbb'
                    box.classList.remove('null')

                }
            })
        })
    }
    
})

//檢核照片
let imgbox = document.querySelector('.image-preview-box')
let imgspan = document.querySelector('.imgbox-span')
imgbox.addEventListener('mouseover', () => {
    if (imgbox.querySelector('img') == null) {
        imgspan.innerText = "請至少上傳一張照片"
        imgspan.classList.add('null')
    }
    else {
        imgspan.innerText = ""
        imgspan.classList.remove('null')
    }
})

//檢核編輯器
let editorbox = document.querySelector('.editorbox')
let editorspan = document.querySelector('.editorbox-span')
editorbox.addEventListener('mouseover', () => {
    if (editor.getData() == '' || editor.getData().length < 80 || editor.getData().length>500) {
        editorspan.innerText = "請填寫服務商品介紹"
        editorspan.classList.add('null')
    }
    else {
        editorspan.innerText = ""
        editorspan.classList.remove('null')
    }
})

//全填完
let contain = document.querySelector('.service-btn')
contain.addEventListener('mouseover', () => {
    if (document.querySelector('.null') == null && document.querySelector('.normalprice') != null) {
        previewbtn.classList.remove('save-disabled')
    }
    else {
        previewbtn.classList.add('save-disabled')
    }
})


function ClearNullSpan() {
    servicespan.classList.remove('null')
    countyspan.classList.remove('null')
    districtspan.classList.remove('null')
    petspan.classList.remove('null')
    dogspan.classList.remove('null')
    catspan.classList.remove('null')
    weekspan.classList.remove('null')
    pricespan.classList.remove('null')
    imgspan.classList.remove('null')
    editorspan.classList.remove('null')
}


//使用者提示步驟
let headerone = document.querySelector('#headingOne')
let headertwo = document.querySelector('#headingTwo')
let headerthree = document.querySelector('#headingThree')
headertwo.addEventListener('click', () => {
    headerone.querySelector('.headerpaw').classList.add('headerpawcheck')
})
headerthree.addEventListener('click', () => {
    headertwo.querySelector('.headerpaw').classList.add('headerpawcheck')
})
contain.addEventListener('mouseover', () => {
    headerthree.querySelector('.headerpaw').classList.add('headerpawcheck')
})