//訂單總數
var annualTotal = document.getElementById("annualTotalCount")
var annualurl = '/OrderChartApi/GetTotalSalesQuan'


httpGet(annualurl)
    .then(res => {
        annualTotal.innerText = res.data;
    })

//年度總收益
var grossRevenu = document.getElementById('grossRevenue');

var grossurl = 'OrderChartApi/GetTotalRevenue'


httpGet(grossurl)
    .then(res => {
        grossRevenu.innerText = res.data;
    })

//訂單服務占比
const ctx = document.getElementById('myPie');
const data = {
    labels: [

    ],
    datasets: [{
        label: '',
        data: [],
        backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc'],
        hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf'],
        hoverBorderColor: "rgba(234, 236, 244, 1)"
    }]
};
const config = {
    type: 'pie',
    data: data,
    options: {
        maintainAspectRatio: false,
        cutoutPercentage: 0,
    }
};
const myChart = new Chart(ctx, config);

var url = '/OrderChartApi/CreateSercicePie'
var serviceArr = []
var quanArr = []

httpGet(url)
    .then(res => {
        serviceArr = res.data.map(x => {
            var serviceType = switchServiceType(x.serviceType)
            return serviceType
        })
        myChart.config.data.labels = serviceArr;
        quanArr = res.data.map(y => y.quan)
        myChart.config.data.datasets[0].data = quanArr;
        myChart.update()
    })


function switchServiceType(str) {
    switch (str) {
        case 'C':
            return '到府照顧';
            break;
        case 'S':
            return '到府洗澡';
            break;
        case 'W':
            return '陪伴散步';
            break;
    }
}


//銷售報表
var barChart
var barCtx = document.getElementById('myChart');

var xArr = [
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
    { id: 12, name: '12月' },
]

LineChart(barCtx, true, 'circle', 1, true, [0.1])

var url = '/OrderChartApi/GetMonthlyData'
httpGet(url)
    .then(res => {
        if (res.status == 20000) {

            let resAmountData = xArr.map(x => {
                let target = res.data.find(y => y.orderMonth == x.id)
                if (target == undefined) return 0
                else return parseInt(target.totalSales / 1000)
            })
            barChart.config.data.datasets[0].data = resAmountData //第一個

            let resCountData = xArr.map(x => {
                let target = res.data.find(y => y.orderMonth == x.id)
                if (target == undefined) return 0
                else return target.totalCount
            })
            barChart.config.data.datasets[1].data = resCountData
            barChart.update()
        }
    })

function LineChart(context, fillmode, point, curve, showline, dash) {
    barChart = new Chart(context, {

        type: "bar",
        data: {
            labels: this.xArr.map(x => x.name),
            datasets: [
                {
                    label: '銷售額(K,NTD)',
                    data: this.salesAmount,
                    fill: fillmode, //填充模式
                    backgroundColor: '#4e73df', //mark填充色
                    pointStyle: point, //點的形狀
                    lineTension: curve, //貝茲曲線參數
                    showLine: showline,//是否顯示線條
                    borderDash: dash,
                    pointBackgroundColor: '#2e59d9',
                    pointRadius: 5, //點的半徑
                    pointHoverRadius: 10, //Hover時候的點半徑
                },
                {
                    label: '銷售數量(per Order)',
                    data: this.salesQuan,
                    fill: fillmode, //填充模式
                    backgroundColor: '#1cc88a', //mark填充色
                    pointStyle: point, //點的形狀
                    lineTension: curve, //貝茲曲線參數
                    showLine: showline,//是否顯示線條
                    borderDash: dash,
                    pointBackgroundColor: '#17a673',
                    pointRadius: 5, //點的半徑
                    pointHoverRadius: 10, //Hover時候的點半徑
                },

            ]
        },
        options: {
            responsive: true,
            title: {
                display: true,
                fontSize: 26,
                text: ''
            },
            tooltips: {
                mode: 'point',
                intersect: true,
            },
            hover: {
                mode: 'nearest',
                intersect: true
            },
            scales: {
                y: {
                    beginAtZero: true
                }
            },
            animation: {
                duration: 3000
            }
        }

    })
}
