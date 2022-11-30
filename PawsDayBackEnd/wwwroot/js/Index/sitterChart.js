//報表標題設置
setTitle("create-title", `${year}年 保姆申請數 / 月`)
//setTitle("approved-title", "保姆數量（服務中）")
setTitle("status-title", "保姆狀態分析")

//審核通過保姆總數 - 數值
let SitterUrl = "/SitterChartApi/GetApprovedSitter"
let SitterQuantity = document.querySelector("#approved-quantity")
httpGet(SitterUrl).then((res) => {
    if (res.status == 20000) {
        SitterQuantity.innerText = res.data.ApprovedSitter
    } else {
        console.log(`${titleInput}錯誤：${res.Message}`)
    }
})


//熱門保姆（訂單數Top 3）
let sitterTopUrl = "/SitterChartApi/GetSitterTop"
let sitterTopName = ["保姆ID", "保姆名稱", "訂單數量"]
GetTop(sitterTopUrl, "sitterTop", "熱門保姆（訂單數Top3）", sitterTopName)

//保姆狀態分析 - 圓餅圖
let SitterStatusQuantity = document.querySelector("#status-quantity")
let SitterStatusUrl = "/SitterChartApi/CountStatusQuantity"
httpGet(SitterStatusUrl).then((res) => {
    if (res.status == 20000) {
        let labels = ["尚未審核", "審核通過", "審核未通過", "停權"]
        let datas = [
            res.data.awaitToCheck,
            res.data.approved,
            res.data.reject,
            res.data.suspend,
        ]
        CreatePieChart(SitterStatusQuantity, labels, datas)
    } else {
        console.log(`保姆狀態錯誤：${res.Message}`)
    }
})

//每月保姆申請數量 - 折線圖
let monthArr = [
    { id: 1, name: "1月" },
    { id: 2, name: "2月" },
    { id: 3, name: "3月" },
    { id: 4, name: "4月" },
    { id: 5, name: "5月" },
    { id: 6, name: "6月" },
    { id: 7, name: "7月" },
    { id: 8, name: "8月" },
    { id: 9, name: "9月" },
    { id: 10, name: "10月" },
    { id: 11, name: "11月" },
    { id: 12, name: "12月" },
]
let SitterCreateQuantity = document.querySelector("#create-quantity")
let SitterCreateUrl = "/SitterChartApi/CountCreateQuantity"
httpGet(SitterCreateUrl).then((res) => {
    if (res.status == 20000) {
        let labels = monthArr.map((m) => m.name)
        let resData = monthArr.map((m) => {
            let result = res.data.find((r) => r.createMonth == m.id)
            if (result == undefined) {
                return 0
            } else {
                return result.createQuantity
            }
        })
        CreateLineChart(SitterCreateQuantity, labels, "保姆申請數 / 月", resData)
    } else {
        console.log(`保姆增加錯誤：${res.Message}`)
    }
})

//繪製圓餅圖function
function CreatePieChart(ctx, title, datas) {
    let PieChart = new Chart(ctx, {
        type: "pie",
        data: {
            labels: title,
            datasets: [
                {
                    data: datas,
                    backgroundColor: ["#4e73df", "#1cc88a", "#36b9cc", "#D3D3D3"],
                    hoverBackgroundColor: ["#2e59d9", "#17a673", "#2c9faf", "#A9A9A9"],
                    hoverBorderColor: "rgba(234, 236, 244, 1)",
                },
            ],
        },
        options: {
            maintainAspectRatio: false,
            cutoutPercentage: 0,
        },
    })
}

//繪製折線圖function
function CreateLineChart(ctx, title, labelText, datas) {
    var LineChart = new Chart(ctx, {
        type: "line",
        data: {
            labels: title,
            datasets: [
                {
                    label: labelText,
                    data: datas,
                    lineTension: 0.3,
                    backgroundColor: "#4e73df",
                    borderColor: "rgba(78, 115, 223, 1)",
                    pointRadius: 3,
                    pointBackgroundColor: "rgba(78, 115, 223, 1)",
                    pointBorderColor: "rgba(78, 115, 223, 1)",
                    pointHoverRadius: 3,
                    pointHoverBackgroundColor: "#2e59d9",
                    pointHoverBorderColor: "rgba(234, 236, 244, 1)",
                    pointHitRadius: 10,
                    pointBorderWidth: 2,
                },
            ],
        },
        options: {
            responsive: true,
            tooltips: {
                mode: "point",
                intersect: true,
            },
            legend: {
                position: "bottom",
                labels: {
                    fontColor: "black",
                },
            },
            scales: {
                yAxes: {
                    ticks: {
                        stepSize: 25,
                    },
                },
            },
        },
    })
}

//報表標題function
function setTitle(cardId, titleInput) {
    let card = new Vue({
        el: `#${cardId}`,
        data: {
            title: titleInput,
        },
    })
}