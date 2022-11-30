

let memberCountData = {
    labels: [],
    datasets: [
        {
            label: '總會員數',
            data: '',
            borderColor: 'rgb(23, 166, 115)',
            backgroundColor: 'rgb(23, 166, 115)',
            tension: 0.3,
            yAxisID: 'y',
        },
        {
            label: '新增會員數',
            data: '',
            borderColor: 'rgb(47, 89, 217)',
            backgroundColor: 'rgb(47, 89, 217)',
            tension: 0.3,
            yAxisID: 'y1',
        }
    ]
};




const memberCountConfig = {
    type: 'line',
    data: memberCountData,
    options: {
        responsive: true,
        interaction: {
            mode: 'index',
            intersect: false,
        },
        stacked: false,
        scales: {
            y: {
                type: 'linear',
                display: true,
                position: 'left',
            },
            y1: {
                type: 'linear',
                display: true,
                position: 'right',

                // grid line settings
                grid: {
                    drawOnChartArea: false, // only want the grid lines for one axis to show up
                },
            },
        }
    },
};

let memberCountAnalyze = new Chart(
    document.getElementById('member-count-analyze'),
    memberCountConfig
);

// Vue

const totalMemberAmount = new Vue({
    el: '#total-member-amount',
    data: {
        totalMemberAmountData: ''
    }
});

const preMonthVue = new Vue({
    el: '#pre-month-vue',
    data: {
        preMonthList: [3, 6, 12]
    },
    created() {
        this.getMemberCountDate(3)
    },
    methods: {
        getMemberCountDate(inputPastMonth) {
            let startDate = new Date();
            let startYear = startDate.getFullYear();
            let startMonth = startDate.getMonth();
            let startYearAndMonthString = '';

            // 輸入從前幾個月開始畫圖
            startDate = new Date(startYear, startMonth - inputPastMonth);

            // 轉成 string
            startYearAndMonthString = `${startDate.getFullYear()}-${startDate.getMonth() + 1}`;

            let url = `/MemberAnalyzeApi/ReadMemberCountStatistics`
            let contact = { stringStartDate: startYearAndMonthString }
            httpPost(url, contact)
                .then(res => {
                    totalMemberAmount.totalMemberAmountData = res.data.totalMemberCount
                    memberCountData.labels = res.data.monthList;
                    memberCountData.datasets[0].data = res.data.accumulateMemberAmount;
                    memberCountData.datasets[1].data = res.data.newMemberPerMonth;
                    memberCountAnalyze.memberCountConfig;
                    memberCountAnalyze.update();
                })
        }

    },

});