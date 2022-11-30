let typeItem = document.querySelector(".choose-type")
let labelType = document.querySelector('.type-label')
let chooseType, typeLast, typeSecondLast, pettypeSelect, shapetypeSelect, pettypeSelectSecond, shapetypeSelectSecond
let plus = document.querySelector('#type-plus')
let minus = document.querySelector('#type-minus')
let petError = document.getElementById('pet-error');
let quantityError = document.getElementById('quantity-error');
let createBtn = document.getElementById("create-cart");
let toBookingBtn;
let timeError = document.getElementById("time-error")
let salonError = document.getElementById("salon-error")
let servicetype = document.querySelector(".service-type").textContent
let createMessage = document.getElementById("create-message")
let createError = document.getElementById("create-error")
let createNotLogin = document.getElementById("create-notLogin")
//傳出的值
let selectmember = document.getElementById("selected-member")
let id = document.getElementById("selected-id")
let day = document.getElementById("selected-day")
let time = document.getElementById("select-time")
let county = document.getElementById("select-county")
let district = document.getElementById("select-district")
let petshape = document.getElementById("select-shapetype")
let priceTocart = document.getElementById("select-price")

window.onload = function () {
    emptyValue()
    init()
    chooseCalendar()
    chooseTime()
    chooseAllTime()
    ChooseType()
    PlusType()
    MinusType()
}

//行事曆
const today = new Date
let year = today.getFullYear()
let month = today.getMonth()
let thedate = today.getDate()
const banner = document.querySelector('.banner')
const body = document.querySelector('.calendar-body')

//初始化
function init() {
    let slogan = document.createElement('h3')
    slogan.classList = 'slogan slogan-vice col-12'
    slogan.innerText = `${year}/${month + 1}`
    banner.append(slogan)

    let firstday = new Date(year, month, 1).getDay() //1號星期幾
    let days = new Date(year, month + 1, 0).getDate() //天數
    let day = 1
    let rows = Math.ceil((firstday + days) / 7)

    //判斷day從第幾格開始 (0~6)
    //判斷要多少row
    for (r = 0; r < rows; r++) {
        let row = document.createElement('div')
        row.classList = 'calendar-body-row row'
        for (c = 0; c < 7; c++) {
            //第一排 firstday-0的部分=上個月
            //最後一排 day>days的部分=下個月

            let col = document.createElement('button')
            let date = day - firstday

            if (r == 0 && c < firstday) { col.classList = 'calendar-body-col othercol' }
            else if (r == rows - 1 && date > days) { col.className = 'calendar-body-col othercol' }
            else {
                col.className = 'calendar-body-col body-col'

                let title = document.createElement('h3')
                title.innerText = date
                col.append(title)
            }
            day++
            row.append(col)
        }
        body.append(row)
    }
    //日期禁用
    let weekdays = document.querySelectorAll(".calendar-body-col")
    weekdays.forEach((weekday, index) => {
        weekday.value = index % 7
        weekday.disabled = true;
        weekdayjs.forEach(item => {
            if (weekday.value == item) {
                weekday.disabled = false;
            }
        })
    })
    let others = document.querySelectorAll(".othercol")
    others.forEach(other => {
        other.disabled = true;
    })
    tagthedate()
}
//上月按鈕
function premonth() {
    month--
    if (month == -1) {
        month = 11
        year--
    }
    clean()
    init()
    chooseCalendar()
}
//下月按鈕
function nextmonth() {
    month++
    if (month == 12) {
        month = 0
        year++
    }
    clean()
    init()
    chooseCalendar()
}
//清除格子
function clean() {
    let slogan = document.querySelector('.slogan-vice')
    banner.removeChild(slogan)
    let rows = document.querySelectorAll('.calendar-body-row')

    for (i = 0; i < rows.length; i++) {
        let row = document.querySelector('.calendar-body-row')
        body.removeChild(row)
    }
}
//標示當日
function tagthedate() {
    let slogan = document.querySelector('.slogan')
    let sloganText = slogan.innerText.split("/")
    if (slogan.innerText == `${today.getFullYear()}/${today.getMonth() + 1}`) {
        let h3 = document.querySelectorAll('h3')
        let todaycol = Array.from(h3).find(x => x.innerText == thedate)
        todaycol.parentElement.classList = 'calendar-body-col body-col thiscol'

        //過時禁用
        h3.forEach(other => {
            if (other.innerText < thedate) {
                other.parentElement.disabled = true
                other.parentElement.classList = 'calendar-body-col othercol'
            }
        })
    }
    if (Number(sloganText[0]) < today.getFullYear()) {
        let cols = document.querySelectorAll(".body-col")
        cols.forEach(col => {
            col.disabled = true
            col.classList = 'calendar-body-col othercol'
        })
    }
    if (Number(sloganText[0]) == today.getFullYear() && Number(sloganText[1]) < (today.getMonth() + 1)) {
        let cols = document.querySelectorAll(".body-col")
        cols.forEach(col => {
            col.disabled = true
            col.classList = 'calendar-body-col othercol'
        })
    }
}
//用input變換年月
function changemonth() {
    let selectdate = document.querySelector('#inputmonth').value.split('-')
    if (selectdate != '') {
        year = selectdate[0]
        month = parseInt(selectdate[1]) - 1
        clean()
        init()
    }
}

let selectedDay, selectedYear, selectedMonth, dayDate, selectedWeekday
//選擇日期
function chooseCalendar() {
    let cols = document.querySelectorAll('.body-col')
    cols.forEach(col => {
        col.addEventListener('click', () => {
            //清空原本選擇時段
            time.value = ""
            disabledCreate()
            GetPrice()
            //點選日期
            cols.forEach(col => {
                col.classList.remove("body-col-choose")
            })
            this.event.currentTarget.classList.add("body-col-choose")
            //傳出選擇的日期
            selectedDay = document.getElementById("selected-day")
            selectedYear = document.querySelector(".slogan").textContent.split("/")[0]
            selectedMonth = document.querySelector(".slogan").textContent.split("/")[1]
            selectedThisDay = document.querySelector(".body-col-choose")
            let timeul = document.querySelector(".commodity-time")
            dayDate = `${selectedYear}/${selectedMonth}/${selectedThisDay.textContent}`
            selectedDay.value = dayDate
            timeul.innerHTML = ""
            timejs.forEach((time) => {
                let number = 1 + (time.Time * 12)
                if (time.Weekday == selectedThisDay.value) {
                    let timeli = document.createElement('li')
                    timeli.classList = 'accordion-item'
                    timeli.innerHTML = `<h4 class="accordion-header" id="heading${time.Time}">
										  <button class="accordion-button collapsed" 
											type="button" data-bs-toggle="collapse" data-bs-target="#collapse${time.Time}" 
											aria-expanded="false" aria-controls="collapse${time.Time}">
												<p>${time.TimeTitle}</p>
												<button type="button" class="choose-all">All</button>
										  </button>
										</h4>
										<div id="collapse${time.Time}" class="accordion-collapse collapse" 
											aria-labelledby="heading${time.Time}" data-bs-parent="#accordionExample">
										  <div class="accordion-body">
										  </div>
										</div>`
                    timeul.append(timeli)
                    //時間表
                    let timelis = document.querySelectorAll(".accordion-item")

                    time.TimeDesrcipt.forEach(desrcipt => {
                        let timebuttonList = timelis[timelis.length - 1].querySelector(".accordion-body")
                        let timebutton = document.createElement('button')
                        timebutton.classList = 'time-item'
                        timebutton.type = "button"
                        timebutton.innerText = desrcipt
                        timebutton.dataset.index = number

                        timebuttonList.append(timebutton)
                        number++
                    })
                }
            })
            //時段禁用
            GetDisabledTime(id.value, selectedYear, selectedMonth, selectedThisDay.textContent)
            chooseTime()
            chooseAllTime()
        })
    });
}

//timebutton點擊
function chooseTime() {
    let times = document.querySelectorAll('.time-item')
    times.forEach(time => {
        time.addEventListener('click', () => {
            this.event.currentTarget.classList.toggle("time-item-choose")
            timeAll()
        })
    });
}
//時段禁用
function GetDisabledTime(productId, year, month, day) {
    let nowDate = new Date()
    let timeBnts = document.querySelectorAll(".time-item")
    timeBnts.forEach(btn => {
        let nowtime = new Date(year, month - 1, day, btn.innerText.substring(0, 2) - 3, btn.innerText.substring(3, 5))
        if (nowDate > nowtime) {
            btn.disabled = true
            btn.classList.add("time-item-disabled")
            btn.classList.remove("time-item")
        }
    })   
    let url = `/api/ProductWebApi/GetDisabledTime?productId=${productId}&year=${year}&month=${month}&day=${day}`;
    fetch(url)
        .then(res => res.json())
        .then(response => {
            if (response != []) {
                response.forEach(times => {
                    timeBnts.forEach(btn => {
                        if (btn.dataset.index == times) {
                            btn.disabled = true
                            btn.classList.add("time-item-disabled")
                            btn.classList.remove("time-item")
                            chooseAllTime()
                        }
                    })
                })
            }
        })
        .catch(ex => {
            console.log(ex)
        })     
}
//點擊All
function chooseAllTime() {
    let timeItems = document.querySelectorAll('.accordion-item')
    timeItems.forEach(timeItem => {
        let chooseAll = timeItem.querySelector('.choose-all')
        let times = timeItem.querySelectorAll('.time-item')
        chooseAll.addEventListener('click', () => {
            chooseAll.classList.toggle("choose-all-choose")
            times.forEach(time => {
                if (!time.classList.contains("time-item-disabled")) {
                    if (chooseAll.classList.contains("choose-all-choose")) {
                        time.classList.add("time-item-choose")
                    } else {
                        time.classList.remove("time-item-choose")
                    }
                }
            });
            timeAll()
        })
        //All按鈕禁用
        let timedisabled = timeItem.querySelectorAll(".time-item-disabled")
        if (timedisabled.length == 12) {
            chooseAll.disabled = true
            chooseAll.classList.add("choose-all-disabled")
            chooseAll.innerText ="sold out"
        }
    });
}

//算時間
function timeAll() {
    let selectTime = document.getElementById("select-time")
    let timeList = []
    let timenumList = []
    let timeChooses = document.querySelectorAll(".time-item-choose")
    timeChooses.forEach(choose => {
        timeList.push(choose.textContent)
        timenumList.push(choose.dataset.index)
    })
    selectTime.value = timenumList
    let num = timenumList[timenumList.length - 1] - timenumList[0]
    //時間不連續
    if (timenumList.length != num + 1) {
        timeError.classList.remove("d-none")
        disabledCreateBtn()
    } else {
        timeError.classList.add("d-none")
        disabledCreate()
    }
    let timequantity = time.value.split(",").length
    if (servicetype == '到府洗澡' && timequantity < 2) {
        salonError.classList.remove("d-none")
        disabledCreateBtn()
    }
    GetPrice()
}



//PetType
function ChooseType() {
    chooseType = document.querySelectorAll('.choose-type')
    typeLast = chooseType[chooseType.length - 1]
    pettypeSelect = typeLast.querySelector('.pettype')
    shapetypeSelect = typeLast.querySelector('.shapetype')
    DisabledTrue(shapetypeSelect)
    pettypeSelect.addEventListener("change", PetChange);
    shapetypeSelect.addEventListener("change", PetList);
}
function PlusType() {
    plus.addEventListener('click', () => {
        if (chooseType.length < 5 && pettypeSelect.value != "" && shapetypeSelect.value != "") {
            let typeAdd = typeItem.cloneNode(true)
            labelType.append(typeAdd)
            ChooseType()
            typeSecondLast = chooseType[chooseType.length - 2]
            pettypeSelectSecond = typeSecondLast.querySelector('.pettype')
            shapetypeSelectSecond = typeSecondLast.querySelector('.shapetype')
            DisabledFalse(pettypeSelect)
            DisabledTrue(pettypeSelectSecond)
            DisabledTrue(shapetypeSelectSecond)
            petError.classList.add('d-none')
        } else if (chooseType.length >= 5) {
            quantityError.classList.remove('d-none')
        } else {
            petError.classList.remove('d-none')
        }
    })
}
function MinusType() {
    minus.addEventListener('click', () => {
        if (chooseType.length > 1) {
            labelType.removeChild(typeLast)
            ChooseType()
            PetListPop()
            typeSecondLast = chooseType[chooseType.length - 2]
            if (chooseType.length > 2) {
                pettypeSelectSecond = typeSecondLast.querySelector('.pettype')
                shapetypeSelectSecond = typeSecondLast.querySelector('.shapetype')
            }
            DisabledFalse(pettypeSelect)
            DisabledFalse(shapetypeSelect)
            quantityError.classList.add('d-none')
        }
        disabledCreate()
        GetPrice()
    })
}
function PetChange(event) {
    let pettypeText = pettypeSelect.selectedOptions[0].text;
    shapetypeSelect.disabled = false;
    shapetypeSelect.innerHTML = '';
    let shapetype = document.createElement('option');
    shapetype.value = '';
    shapetype.text = '-- 類型 --';
    shapetype.setAttribute('selected', '')
    shapetype.setAttribute('disabled', '')
    shapetypeSelect.add(shapetype, null);

    let pettypeArray = typesjs.filter(item => item.PetType == pettypeText);
    pettypeArray.forEach((item, index) => {
        let shapetype = document.createElement('option');
        shapetype.value = `${item.PetTypeId}-${item.ShapeTypeId}`;
        shapetype.text = item.ShapeType;
        shapetypeSelect.add(shapetype);
    });
    shapetypeSelect.disabled = false;
}
//把寵物選擇傳進List
let shapeList = []
let selectShapetype = document.getElementById("select-shapetype")
function PetList(event) {
    let shapes = document.querySelectorAll(".shapetype")
    shapeList = []
    shapes.forEach(shape => {
        shapeList.push(shape.value)
    })
    selectShapetype.value = shapeList
    GetPrice()
    disabledCreate()
}
//刪除最後一個寵物組合
function PetListPop() {
    shapeList.pop()
    selectShapetype.value = shapeList
}


//City
let citySelect = document.getElementById('city');
let districtSelect = document.getElementById('district');
let thisCounty = document.getElementById('select-county');
let thisDistrict = document.getElementById('select-district');

DisabledTrue(districtSelect)
citySelect.addEventListener("change", CityChange);
districtSelect.addEventListener("change", DistrictChange);
districtSelect.addEventListener("click", disabledCreate);
function DisabledTrue(Select) {
    Select.disabled = true;
}
function DisabledFalse(Select) {
    Select.disabled = false;
}
function CityChange(event) {
    let countyText = citySelect.selectedOptions[0].text;
    districtSelect.disabled = false;
    districtSelect.innerHTML = '';
    let district = document.createElement('option');
    district.text = '-- 區 --';
    district.setAttribute('selected', '')
    district.setAttribute('disabled', '')
    districtSelect.add(district, null);

    let countyArray = areajs.filter(item => item.County == countyText);
    countyArray.forEach((item, index) => {
        let district = document.createElement('option');
        district.value = item.District;
        district.text = item.District;
        districtSelect.add(district);
    });
    districtSelect.disabled = false;
}
function DistrictChange(event) {
    thisCounty.value = citySelect.value
    thisDistrict.value = districtSelect.value
}

//防呆
function disabledCreate() {
    let timequantity = time.value.split(",").length
    if (id.value == "" || day.value == "" || time.value == "" || county.value == "" || district.value == "" || petshape.value == "") {
        salonError.classList.add("d-none")
        disabledCreateBtn()
    } else if (servicetype == '到府洗澡' && timequantity < 2) {
        salonError.classList.remove("d-none")
        disabledCreateBtn()
    } else {
        salonError.classList.add("d-none")
        availableCreateBtn()
    }
    createMessage.classList.add("d-none")
}

//createBtn是否禁用
if (selectmember.value != 0) {
    toBookingBtn = document.getElementById("to-booking");
}
function disabledCreateBtn() {
    createBtn.disabled = true
    createError.classList.remove("d-none")
    createBtn.classList.add("btn-disabled")
    if (selectmember.value != 0) {
        toBookingBtn.disabled = true
        toBookingBtn.classList.add("btn-disabled")
    }
}
function availableCreateBtn() {
    createBtn.disabled = false
    createError.classList.add("d-none")
    createBtn.classList.remove("btn-disabled")
    if (selectmember.value != 0) {
        toBookingBtn.disabled = false
        toBookingBtn.classList.remove("btn-disabled")
    }
}

createBtn.addEventListener("click", CreateCart)
//加入購物車
function CreateCart() {
    if (selectmember.value != 0) {
        data = {
            MemberId: parseInt(selectmember.value),
            SelectedId: id.value,
            SelectedDay: day.value,
            SelectedTime: time.value,
            SelectedCounty: county.value,
            SelectedDistrict: district.value,
            SelectedShapeTypes: petshape.value,
            SelectedPrice: priceTocart.value
        }

        let url = '/api/ProductWebApi/CreateCart'

        fetch(url, {
            method: 'POST',
            body: JSON.stringify(data),
            headers: new Headers({
                'Content-Type': 'application/json'
            })
        })
            .then(res => res.json())
            .then(response => {
                //刷新購物車預覽
                GetShoppingCart()
            })
            .catch(ex => {
                console.log(ex)
            })
        createMessage.classList.remove("d-none")
        CartAnimate()
    } else {
        createMessage.classList.remove("d-none")
        notLoginCreateCart()
        //刷新購物車預覽
        NotLoginGetCart()
    }
}
//未登入時加入購物車
function notLoginCreateCart() {
    let cartList = []
    let addcartList = {
        Id: id.value,
        Day: day.value,
        Time: time.value,
        County: county.value,
        District: district.value,
        ShapeTypes: petshape.value
    }
    if (Cookies.get('pawsdayCarts') == null) {
        cartList.push(addcartList)
        CartAnimate()
    }
    else if (JSON.parse(Cookies.get('pawsdayCarts')).length >= 6) {
        cartList = JSON.parse(Cookies.get('pawsdayCarts'))
        createNotLogin.classList.remove("d-none")
        createMessage.classList.add("d-none")
        createBtn.disabled = true
        createBtn.classList.add("btn-disabled")
        //觸發登入
        let productLogin = document.querySelector(".product-login")
        let layoutLogin = document.querySelector(".login-btn")
        productLogin.addEventListener("click", clickLogin)
        function clickLogin(event) { layoutLogin.click() }
    }
    else {
        cartList = JSON.parse(Cookies.get('pawsdayCarts'))
        cartList.push(addcartList)
        CartAnimate()
    }
    Cookies.set('pawsdayCarts', JSON.stringify(cartList), { expires: 1, path: '/' });
}

//購物車圖示閃動
function CartAnimate() {
    let cartanimate = document.createElement('img')
    let cartIcon = document.querySelector('.cart-icon')
    cartanimate.classList.add('cart-addone')
    cartanimate.src ="/images/createcart.png"
    cartIcon.append(cartanimate)
    setTimeout(() => { cartanimate.style.bottom = '-0px' }, 300)
    setTimeout(() => { cartanimate.style.bottom = '-33px' }, 1800)
    setTimeout(() => { cartanimate.remove() }, 3300)
}


//價錢計算
let price = document.getElementById("commodity-price")
function GetPrice() {
    if (id.value != '' && time.value != '' && petshape.value != '') {
        var totalPrice = GetPetUnitPrice(id.value, petshape.value, time.value)
        totalPrice.then((result) => {
            let Q = time.value.split(",")
            if (Q.length >= discountjs.Quantity && discountjs.Quantity != 0 && discountjs.Discount != 0) {
                discountresult = result * (discountjs.Discount / 10)
                price.innerHTML = `TWD ${Math.round(discountresult).toLocaleString('en-US')} (原價 TWD ${result.toLocaleString('en-US')})`
                priceTocart.value = discountresult;
            } else {

                price.innerText = `TWD ${result.toLocaleString('en-US')}`
                priceTocart.value = result;
            }
        })
    }
    else {
        price.innerText = "TWD 0"
        priceTocart.value = 0;
    }
}

//API 得到組合價錢
async function GetPetUnitPrice(productId, types, times) {
    let url = `/api/ProductWebApi/GetPetPrice?productId=${productId}&types=${types}&times=${times}`;
    const res = await fetch(url);
    const json = await res.json();
    return json;
}


//清空返回值
function emptyValue() {
    day.value = ""
    time.value = ""
    county.value = ""
    district.value = ""
    petshape.value = ""
    priceTocart.value = ""
    citySelect.value = "-- 縣／市 --"
    let firstpet = document.querySelector('.pettype')
    firstpet.value = ""
    let firstshape = document.querySelector('.shapetype')
    firstshape.value = "-- 體型 --"
}

