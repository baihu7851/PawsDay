const today = new Date
let year = today.getFullYear()
let month = today.getMonth()
let thedate = today.getDate()
let source = [];
let dailyul = document.querySelector('.daily-content')
let datetitle = document.querySelector('.daily-date')

const banner = document.querySelector('.banner')
const body = document.querySelector('.calendar-body')
const dailybox = document.querySelector('.daily-box')

window.onload = function () {
    getorder()
}

function getorder() {
    let url = "/api/MemberCenterCalenderWebApi/GetCalenderDate"

    fetch(url)
        .then(res => res.json())
        .then(response => {
            //轉換資料
            if (response.isSuccess == true) {
                source = response.data.map(d => {
                    console.log(d)
                    return {                        
                        id: d.orderId,
                        num: d.orderNumber,
                        year: new Date(d.beginTime).getFullYear(),
                        month: new Date(d.beginTime).getMonth(),
                        begin: new Date(d.beginTime),
                        end: new Date(d.endTime),
                        service: d.serviceType,
                        address: d.address
                    }
                })
            }
            init()
        })
        .catch(ex => {
            console.log(ex)
        })
}

//初始化
function init() {
    let slogan = document.createElement('h2')
    slogan.classList = 'slogan slogan-vice col-12'
    slogan.innerText = `${year}/${month + 1}`
    banner.append(slogan)

    let firstday = new Date(year, month, 1).getDay() //1號星期幾
    let days = new Date(year, month + 1, 0).getDate() //天數
    let day = 1
    let rows = Math.ceil((firstday + days) / 7)

    console.log("init")
    console.log(source)

    //判斷day從第幾格開始 (0~6)
    //判斷要多少row
    for (r = 0; r < rows; r++) {
        let row = document.createElement('div')
        row.classList = 'calendar-body-row row'
        for (c = 0; c < 7; c++) {
            //第一排 firstday-0的部分=上個月
            //最後一排 day>days的部分=下個月

            let col = document.createElement('div')
            let date = day - firstday

            if (r == 0 && c < firstday) { col.classList = 'calendar-body-col othercol' }
            else if (r == rows - 1 && date > days) { col.className = 'calendar-body-col othercol' }
            else {
                col.className = 'calendar-body-col body-col'

                let title = document.createElement('h3')
                title.innerText = date
                col.addEventListener('click', () => { show(col) })
                col.append(title)
            }
            day++
            row.append(col)
        }
        body.append(row)
    }
    tagthedate(slogan)
    tagOrder(slogan)
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
function tagthedate(slogan) {
    if (slogan.innerText == `${today.getFullYear()}/${today.getMonth() + 1}`) {
        let h3 = document.querySelectorAll('h3')
        let todaycol = Array.from(h3).find(x => x.innerText == thedate)
        todaycol.parentElement.classList = 'calendar-body-col body-col thiscol'
    }
}
function tagOrder(slogan) {
    source.map(s => {
        //比對year、month
        if (slogan.innerText == `${s.year}/${(s.month) + 1}`) {
            //比對date標示icon
            let h3 = document.querySelectorAll('h3')
            h3.forEach(h => {
                if (h.innerText == `${s.begin.getDate()}`) {
                    let schedule = document.createElement('div')
                    schedule.classList = 'thisschedule'
                    let content = document.createElement('div')
                    content.classList = 'thiscontent fa-solid fa-paw'
                    schedule.append(content)
                    h.parentElement.append(schedule)
                }
            })
        }
    })
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
function show(col) {
    let cols = document.querySelectorAll('.body-col')
    cols.forEach(c => { c.classList.remove('now-col') })
    col.classList.add('now-col')
    //依據選擇日期顯示每日行程
    let month = document.querySelector('.slogan-vice').innerText.split('/')[1]
    dailyul.innerHTML = ''
    let date = col.querySelector('h3')
    datetitle.innerText = `${month}/${date.innerText}`
    let schedule = []
    source.map(s => {
        if (date.innerText == `${s.begin.getDate()}`) { schedule.push(s) }
    })
    schedule.forEach(sch => {
        console.log(sch)
        let a = document.createElement('a')
        let li = document.createElement('li')
        li.className = 'daily-item'
        let time = document.createElement('div')
        time.className = 'daily-time'
        let begintime = `${sch.begin.getMinutes()}`
        time.innerText = `${sch.begin.getHours()}:${begintime.padStart(2, '0')}~${sch.end.getHours()}:${sch.end.getMinutes()}`;
        let content = document.createElement('p')
        content.className = 'daily-service'
        content.innerText = `${sch.service}`
        li.append(time, content)
        a.setAttribute('href', `/MemberCenter/OrderDetail/${sch.num}`)
        a.append(li)
        dailyul.append(a)
    })
}