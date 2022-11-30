var barChart

const ctx = document.getElementById('myPie');
const data = {
    labels: [
        
    ],
    datasets: [{
        label: '',
        data: [],
        backgroundColor: [
            'rgb(255, 99, 132)',
            'rgb(54, 162, 235)',
            'rgb(255, 205, 86)'
        ],
        hoverOffset: 4
    }]
};
const config = {
    type: 'pie',
    data: data,
};
const myChart = new Chart(ctx, config);

var url = 'OrderChartApi/CreateSercicePie'
var serviceArr = []
var quanArr = []

httpGet(url)
    .then(res=>{
        //console.log(res.data);
        serviceArr = res.data.map(x => {
            var serviceType = switchServiceType(x.serviceType)
            //console.log(serviceType)
            return serviceType
        })
        //console.log(serviceArr)
        myChart.config.data.labels = serviceArr;


        quanArr = res.data.map(y => y.quan)
        //console.log(quanArr)
        myChart.config.data.datasets[0].data = quanArr;
        //console.log('hello' + myChart.config.data.datasets.data)
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