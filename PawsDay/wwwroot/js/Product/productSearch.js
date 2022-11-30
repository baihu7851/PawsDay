let searchInput = document.getElementById('search-input')
let searchBtn = document.getElementById('search-button')

//BtnClick事件
searchBtn.addEventListener('click', function () {
    
    History()
    if (searchInput.value == "") {
        window.location.href = `/Search/Pawsday/好評推薦`
    } else {
        window.location.href = `/Search/Pawsday/${searchInput.value}`
    }
})

//InputEnter事件
searchInput.addEventListener("keyup", function (event) {
    event.preventDefault()
    if (event.keyCode === 13) {
        searchBtn.click()
    }
})

//歷史記錄
let historicals = document.querySelectorAll(".historical-item")

GetHistorical()
function GetHistorical() {
    if (Cookies.get('pawsdaySearch') != null) {
        let searchList = JSON.parse(Cookies.get('pawsdaySearch'))
        historicals.forEach((item, index) => {
            if (Cookies.get('pawsdaySearch') != null) {
                if (searchList[searchList.length - 1 - index] != null) {
                    let inputvalue = searchList[searchList.length - 1 - index].input
                    item.innerHTML = inputvalue
                    item.addEventListener("click", function () {
                        window.location.href = `/Search/Pawsday/${inputvalue}`
                    })
                }
            }
        })
    }
}
function History() {
    let historyList = []
    let addhistoryList = {
        input: searchInput.value
    }
    if (searchInput.value != "") {
        if (Cookies.get('pawsdaySearch') == null) {
            historyList.push(addhistoryList)
        }
        else if (JSON.parse(Cookies.get('pawsdaySearch')).length >= 4){
            historyList = JSON.parse(Cookies.get('pawsdaySearch')).slice(1)
            historyList.push(addhistoryList)
        }
        else {
            historyList = JSON.parse(Cookies.get('pawsdaySearch'))
            historyList.push(addhistoryList)
        }
        Cookies.set('pawsdaySearch', JSON.stringify(historyList), { expires: 7, path: '/' });
    }
}