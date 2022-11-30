let labels = []
let price = []
let orders = []
let customer = []

let pricebox = document.querySelector('.pricebox')
let orderbox = document.querySelector('.orderbox')
let cusbox = document.querySelector('.cusbox')


const option = document.querySelector('#chart-cycle')
option.addEventListener('click', (target) => {

    let url = `/api/SitterCenterWebApi/GetChart?type=${target.target.value}`
    fetch(url)
        .then(res => res.json())
        .then(response => {
            if (response.isSuccess == true) {
                labels = response.data.label
                price = response.data.totalPrice
                orders = response.data.ordercount
                customer = response.data.customercount

                //把已存在的報表移除
                let exist = document.querySelectorAll('canvas')
                if (exist.length != 0) {
                    exist.forEach(e => {
                        e.remove()
                    })
                }
                //將預覽圖隱藏
                document.querySelector('.chartimg').classList.add('d-none')
                CreateTitle()
                Create()
            }
                        
        })
        .catch(ex => {
        })
})

function CreateTitle() {
    let title = document.querySelectorAll('.chart-title')
    title.forEach(t => {
        t.classList.toggle('d-none')
    })
}

function Create() {
    let pricechart = document.createElement('canvas')
    let chart_price = new Chart(
        pricechart,
        {
            type: 'line',
            data: {
                labels: labels,
                datasets: [{
                    label: '訂單付款總金額',
                    backgroundColor: 'rgb(255, 99, 132)',
                    borderColor: 'rgb(255, 99, 132)',
                    data: price,
                }]
            },
            options: {}
        }
    );
    pricebox.append(pricechart)

    let orderchart = document.createElement('canvas')
    let chart_order = new Chart(
        orderchart,
        {
            type: 'line',
            data: {
                labels: labels,
                datasets: [{
                label: '訂單筆數',
                backgroundColor: 'rgb(75, 192, 192)',
                borderColor: 'rgb(75, 192, 192)',
                    data: orders,
                }]
            },
            options: {}
        }
    );
    orderbox.append(orderchart)

    let cuschart = document.createElement('canvas')
    let chart_customer = new Chart(
        cuschart,
        {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    label: '下單客數',
                    backgroundColor: 'rgb(255, 205, 86)',
                    borderColor: 'rgb(255, 205, 86)',
                    data: customer,
                }]
            },
            options: {}
        }
    );
    cusbox.append(cuschart)
}

function Clean() {
    let exist = document.querySelector('canvas')
    box.remove(exist)
}
