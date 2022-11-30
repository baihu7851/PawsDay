window.onload = function () {
    GetPoint()
}

function GetPoint() {
    let url = "/api/SitterAptitudeWebApi"
    fetch(url)
        .then((res) => {
            return res.json()
        })
        .then((res) => {
            InitChart(res)
        })
        .catch()
}

function InitChart(pointData) {
    const data = {
        labels: [
            'Extrovert 外向',
            'Introvert 內向',
            'Thinking 理性',
            'Feeling 感性',
        ],
        datasets: [{
            label: '保姆屬性測驗',
            data: pointData,
            fill: true,
            backgroundColor: 'rgba(254,244,221,0.5)',
            borderColor: '#ffce56',
            pointBackgroundColor: '#562f09',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: '#562f09'
        }]
    };
    let delayed;
    const config = {
        type: 'radar',

        data: data,
        options: {
            elements: {
                line: {
                    borderWidth: 4
                }
            },
            scale: {
                min: 0,
                max: 10,
                stepSize: 2,
            },
            plugins: {
                legend: {
                    labels: {
                        font: {
                            size: 16
                        }
                    }
                }
            }
        },
    };
    const myChart = new Chart(
        document.getElementById('myChart'),
        config
    );
}