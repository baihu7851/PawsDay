var barChart
var barCtx = document.getElementById('myChart');

var xArr = [
    {id: 1, name: '1月'},
    {id: 2, name: '2月'},
    {id: 3, name: '3月'},
    {id: 4, name: '4月'},
    {id: 5, name: '5月'},
    {id: 6, name: '6月'},
    {id: 7, name: '7月'},
    {id: 8, name: '8月'},
    {id: 9, name: '9月'},
    {id: 10, name: '10月'},
    {id: 11, name: '11月'},
    {id: 12, name: '12月'},
]



LineChart(barCtx, true, 'circle', 1, true, [0.1])


var url = 'OrderChartApi/GetMonthlyData'
httpGet(url)
    .then(res=>{
        //console.log(res);
        if(res.status==20000){


            //console.log(res.data)
            let resAmountData = xArr.map(x => {

                let target = res.data.find(y => y.orderMonth == x.id)
                //console.log(target)
                if (target == undefined) return 0
                else return parseInt(target.totalSales/1000)
            })
            //console.log(resAmountData);
            barChart.config.data.datasets[0].data = resAmountData //第一個

            let resCountData = xArr.map(x=>{
                let target = res.data.find(y=>y.orderMonth == x.id)
                if (target == undefined) return 0
                else return target.totalCount
            })
            barChart.config.data.datasets[1].data = resCountData
            //console.log(resCountData);
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
                        backgroundColor: 'rgba(255,165,0,0.3)', //mark填充色
                        borderColor: 'rgb(255,165,0)',
                        pointStyle: point, //點的形狀
                        lineTension: curve, //貝茲曲線參數
                        showLine: showline,//是否顯示線條
                        borderDash: dash,
                        pointBackgroundColor: 'rgb(0,255,0)',
                        pointRadius: 5, //點的半徑
                        pointHoverRadius: 10, //Hover時候的點半徑
                    },
                    {
                        label: '銷售數量(per Order)',
                        data: this.salesQuan,
                        fill: fillmode, //填充模式
                        backgroundColor: 'blue', //mark填充色
                        borderColor: 'red',
                        pointStyle: point, //點的形狀
                        lineTension: curve, //貝茲曲線參數
                        showLine: showline,//是否顯示線條
                        borderDash: dash,
                        pointBackgroundColor: 'rgb(0,255,0)',
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
