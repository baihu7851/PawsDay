let productbox = document.querySelector('.product-box')
let loadbox = document.querySelector('.loadingbox')
let addbtn = document.querySelector('.sale-add')

window.onload = function () {
    SetCountbtn()
    SetProductBox()
    savebtn()
    delbtn()

    addbtn.addEventListener('mouseover', () => {
        //檢核是否有儲存按鈕為null
        if (!document.querySelector('.null')) {
            addbtn.classList.remove('btn-disabled')
            document.querySelector('.add-span').innerText = ''
        }
        else {
            document.querySelector('.add-span').innerText='尚有促銷商品未儲存'
        }
        
    })
}


//讓box可以展開、關閉
function SetProductBox()
{
    let closebtn = document.querySelector('.product-list-close')
    let listbox = document.querySelector('.product-list')
    addbtn.addEventListener('click', () => {
        listbox.style.display = 'block';
        GetOrderList()
    })
    closebtn.addEventListener('click', () => {
        listbox.style.display = 'none';
    })
}

//點box商品卡將資料帶入discount畫面
function AddtoDiscountBox()
{
    let productlist = document.querySelector('.product-list')
    let products = document.querySelectorAll('.listbox-product')
    let body = document.querySelector('.container-service-body')
    addbtn.classList.add('btn-disabled')
    products.forEach(product => {
        product.addEventListener('click', () => {
            let imgurl = product.querySelector('img').src
            let title = product.querySelector('.commodity-title').innerText
            let area = product.querySelectorAll('#commodity-place')
            let id = product.querySelector('.id-box').innerText

            let temp = document.querySelector('#salebox').content.cloneNode(true)
            temp.querySelector('img').src = imgurl
            temp.querySelector('.commodity-title').innerText = title
            temp.querySelector('.sale-save').classList.add('null') //先將儲存按鈕添加null
            temp.querySelector('.amount').value=2
            temp.querySelector('.discount').value = 8
            temp.querySelector('#productid').innerText = id
            let place = temp.querySelector('.commodity-place')
            area.forEach(a => {
                let p = document.createElement('p')
                p.id = 'commodity-place'
                p.innerText = a.innerText
                place.append(p)
            })
            let nullimg = document.querySelector('.service-null')
            if (nullimg) { body.removeChild(nullimg) } 
            productbox.removeChild(product)
            body.append(temp)        
            productlist.style.display = 'none'
            savebtn()
            delbtn()
            SetCountbtn()
            checkinput()
        })
    })
}



//加減數字功能
function SetCountbtn()
{
    let minus = document.querySelectorAll('.minus')
    let plus = document.querySelectorAll('.plus')
    minus.forEach(m => {
        m.addEventListener('click', () => {
            let amount = m.parentElement.querySelector('.amount').value
            m.parentElement.querySelector('.amount').value = (parseInt(amount) - 1)
            if ((parseInt(amount) <= 1)) { m.parentElement.querySelector('.amount').value = 1 }
        })
    })
    plus.forEach(p => {
        p.addEventListener('click', () => {
            let amount = p.parentElement.querySelector('.amount').value
            p.parentElement.querySelector('.amount').value = (parseInt(amount) + 1)
        })
    })
}

//watch數量及折扣
function checkinput() {
    let amount = document.querySelectorAll('.amount')
    amount.forEach(am => {
        am.addEventListener('change', (target) => {
            let textbox = target.target.parentElement.querySelector('.checkinfo')
            let savebtn = target.target.parentElement.querySelector('.sale-save')
            if (am.value == '' || parseInt(am.value) < 1) {
                textbox.innerText = "請輸入1以上商品數量"
                savebtn.disabled = true
            }
            else if (!Number(am.value)) {
                textbox.innerText = "請輸入數字"
                savebtn.disabled = true
            }
            else {
                textbox.innerText = ""
                savebtn.disabled = false
            }
        })
    })
    let discount = document.querySelectorAll('.discount')
    discount.forEach(dis => {
        dis.addEventListener('change', (target) => {
            let textbox = target.target.parentElement.querySelector('.checkinfo')
            let savebtn = target.target.parentElement.querySelector('.sale-save')
            if (dis.value == '' || parseInt(dis.value) < 1 || parseInt(dis.value) > 9) {
                textbox.innerText = "請輸入1~9折扣數字"
                savebtn.disabled = true
            }
            else if (!Number(dis.value)) {
                textbox.innerText = "請輸入數字"
                savebtn.disabled = true
            }
            else {
                textbox.innerText = ""
                savebtn.disabled = false
            }
        })
    })
}




const body = document.querySelector('.container-service-body')
function savebtn() {
    let savebtn = document.querySelectorAll('.sale-save')
    //儲存btn
    savebtn.forEach(btn => {
        let id = btn.parentElement.querySelector('div').id
        btn.addEventListener('click', (target) => {
            let temp = target.target.parentElement.parentElement.parentElement
            let id = temp.querySelector('#productid').innerText
            let amountvalue = temp.querySelector('.amount').value
            let discountvalue = temp.querySelector('.discount').value
            let info = temp.querySelector('.checkinfo')
            info.innerText = ''
            //檢查input數值
            if (amountvalue < 1 || discountvalue < 1 || discountvalue > 9 ) {
                info.innerText = "請確認件數及折扣(1~9折)"
            }
            else if (!Number(amountvalue) || !Number(discountvalue)) {
                info.innerText = "請輸入數字"
            }
            else {
                Update(temp, id)
                btn.classList.remove('null')
            }
        })
    })
}
function delbtn() {
    //刪除btn
    let delbtn = document.querySelectorAll('.sale-delete')
    delbtn.forEach(btn => {
        let thisid = btn.parentElement.querySelector('div').innerText
        btn.addEventListener('click', (target) => {
            let temp = target.target.parentElement.parentElement.parentElement
            let id = temp.querySelector('#productid').innerText
            body.removeChild(temp)
            Delete(temp, thisid)
        })
    })
}



//取得最新資料
function GetSalesBox(temp, id) {
    let img = temp.querySelector('.commodity-img')
    let title = temp.querySelector('#commodity-title')
    let place = temp.querySelector('#commodity-place')
    let amount = temp.querySelector('.amount')
    let discount = temp.querySelector('.discount')

    let url = `/api/SitterCenterWebApi/GetSales?id=${id}`

    fetch(url)
        .then(res => res.json())
        .then(response => {
            if (response.isSuccess == true) {
                let r = response.data

                amount.value = r.quantity,
                discount.value = r.discount,
                id.value = r.productID
            }
        })
        .catch(ex => {
        })
}

//更新促銷資料
function Update(temp, id) {
    let amountvalue = temp.querySelector('.amount').value
    let discountvalue = temp.querySelector('.discount').value

    let data = {
        productId: id,
        quantity: amountvalue,
        discount: discountvalue
    }

    let url = `/api/SitterCenterWebApi/UpdateSales`

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
                GetSalesBox(temp, id)
            }
        })
        .catch(ex => {
        })
}

//刪除促銷資料
function Delete(temp, id) {

    let url = `/api/SitterCenterWebApi/DeleteSales?id=${id}`

    fetch(url, {
        method: 'DELETE',
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    })
        .then(res => res)
        .then(response => {
            if (response.isSuccess == true) {
                body.removeChild(temp);
            }
        })
        .catch(ex => {
        })
}

//取得productlist
function GetOrderList()
{
    let url = `/api/SitterCenterWebApi/GetProductBox`

    fetch(url)
        .then(res => res.json())
        .then(response => {
            if (response.isSuccess == true) {
                productbox.innerHTML = ''
                let products = response.data

                products.forEach(pro => {
                    let temp = document.querySelector('#productbox').content.cloneNode(true)
                    temp.querySelector('.id-box').innerText = pro.productID
                    temp.querySelector('img').src = pro.productImage
                    temp.querySelector('.commodity-title').innerText = `${pro.sitterName} | ${pro.serviceType}`
                    temp.querySelector('.commodity-service').innerHTML = pro.introduce

                    let area = pro.serviceArea
                    area.forEach(a => {
                        let p = document.createElement('p')
                        p.innerText = `${a.countyName} ${a.districtName}`
                        p.id = 'commodity-place'
                        temp.querySelector('.commodity-place').append(p)
                    })
                    productbox.append(temp)
                })
                AddtoDiscountBox()
                loadbox.style='display:none'
            }
        })
        .catch(ex => {
        })
}