//商品總數量
let productTotalQuantity = document.getElementById("TotalQuantity");
let productUrl = '/ChartProductApi/GetProductQuantity'
httpGet(productUrl)
    .then(res => {
        if (res.status == 20000) {
            let count = res.data.ProductCount
            productTotalQuantity.innerText = `${count}`
        }
    })

//商品成長曲線圖
const today = new Date
let year = today.getFullYear()
let month = [
    { id: 1, name: '1月' },
    { id: 2, name: '2月' },
    { id: 3, name: '3月' },
    { id: 4, name: '4月' },
    { id: 5, name: '5月' },
    { id: 6, name: '6月' },
    { id: 7, name: '7月' },
    { id: 8, name: '8月' },
    { id: 9, name: '9月' },
    { id: 10, name: '10月' },
    { id: 11, name: '11月' },
    { id: 12, name: '12月' }
]
let productCreate = document.getElementById("createQuantity");
let productCreateTitle = document.getElementById("createTitle");
productCreateTitle.innerText = `${year}年 商品增加數 / 月`
let productCreateUrl = '/ChartProductApi/GetCreateQuantity'
httpGet(productCreateUrl)
    .then(res => {
        if (res.status == 20000) {
            let title = month.map(x => x.name)
            let resCountData = month.map(x => {
                let result = res.data.find(y => y.createMonth == x.id)
                if (result == undefined) return 0
                else return result.totalCount
            })
            ProductLineChart(productCreate, title, `商品增加數 / 月`, resCountData)
        }
    })

//上架狀態圓餅圖
let productSale = document.getElementById("saleQuantity");
let productSaleUrl = '/ChartProductApi/GetSaleQuantity'
httpGet(productSaleUrl)
    .then(res => {
        if (res.status == 20000) {
            let title = ["上架中", "下架中", "已刪除"]
            let result = [res.data.onSale, res.data.offSale, res.data.isDelete]
            ProductPieChart(productSale, title, result)
        }
    })

//服務類別圓餅圖
let productServiceType = document.getElementById("serviceTypeQuantity");
let productTypeUrl = '/ChartProductApi/GetServiceTypeQuantity'
httpGet(productTypeUrl)
    .then(res => {
        if (res.status == 20000) {
            let title = []
            let result = []
            res.data.forEach(item => {
                title.push(item.serviceType)
                result.push(item.totalCount)
            })
            ProductPieChart(productServiceType, title, result)
        }
    })


//曲線圖
function ProductLineChart(ctx, title, labeltext, datas) {
    let LineChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: title,
            datasets: [{
                label: labeltext,
                data: datas,
                lineTension: 0.3,
                backgroundColor: '#4e73df',
                borderColor: "rgba(78, 115, 223, 1)",
                pointRadius: 3,
                pointBackgroundColor: "rgba(78, 115, 223, 1)",
                pointBorderColor: "rgba(78, 115, 223, 1)",
                pointHoverRadius: 3,
                pointHoverBackgroundColor: '#2e59d9',
                pointHoverBorderColor: "rgba(234, 236, 244, 1)",
                pointHitRadius: 10,
                pointBorderWidth: 2,
            }]
        },
        options: {
            responsive: true,
            tooltips: {
                mode: 'point',
                intersect: true,
            },
            legend: {
                position: 'bottom',
                labels: {
                    fontColor: 'black',
                }
            },
            scales: {
                yAxes: {
                    ticks: {
                        stepSize: 25
                    }
                }
            }
        }
    });
}

//圓餅圖
function ProductPieChart(ctx, title, datas) {
    let PieChart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: title,
            datasets: [{
                data: datas,
                backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc'],
                hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf'],
                hoverBorderColor: "rgba(234, 236, 244, 1)",
            }],
        },
        options: {
            maintainAspectRatio: false,
            cutoutPercentage: 0,
        }
    });
}


//熱門商品(評價數Top3)
let evaluationUrl = '/ChartProductApi/GetEvaluationTop'
let evaluationName = ["商品ID","保姆ID", "評價", "評價數量"]
GetTop(evaluationUrl, 'evaluationTop', '熱門商品（評價數Top3）', evaluationName)

//熱門商品(訂購數Top3)
let orderUrl = '/ChartProductApi/GetOrderTop'
let orderName = ["商品ID","保姆ID", "訂購數量"]

GetTop(orderUrl, 'orderTop', '熱門商品（訂購數Top3）', orderName)

//TopVue
function GetTop(url, id, topTitle, topName) {
    httpGet(url)
        .then(res => {
            if (res.status == 20000) {
                new Vue({
                    el: `#${id}`,
                    data: {
                        title: topTitle,
                        tops: res.data,
                        name: topName
                    }
                })
            }
        })
}