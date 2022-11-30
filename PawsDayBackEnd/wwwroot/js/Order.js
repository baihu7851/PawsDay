let content = new Vue({
    el: '#ordervue',
    data: {
        status: 1,
        page: 1,
        tabIndex: 0,
        statusIndex: 0,
        fields: [
            { key: 'number', label: '訂單編號' },
            { key: 'status', label: '狀態' },
            { key: 'name', label: '姓名' },
            { key: 'totalPrice', label: '總價格' },
            { key: 'actions', label: '功能' }
        ],
        fieldsSuccess: [
            { key: 'number', label: '訂單編號' },
            { key: 'status', label: '狀態' },
            { key: 'name', label: '姓名' },
            { key: 'totalPrice', label: '總價格' },
            { key: 'actions', label: '功能' },
            { key: 'changestatus', label: '狀態修改' },
        ],
        fieldsAlready: [
            { key: 'number', label: '訂單編號' },
            { key: 'status', label: '狀態' },
            { key: 'name', label: '姓名' },
            { key: 'totalPrice', label: '總價格' },
            { key: 'actions', label: '功能' },
            { key: 'changestatus', label: '狀態修改' },
            { key: 'checkbox', label: '完成' },
        ],
        orderAllList: '',
        orderContent: '',
        petContect: '',
        changeStatusId: '',
        //分頁設定
        perPage: 10,
        currentPage: 1,
        totalPage: 0,
        filter: '',
        //下拉式選單
        selected: 10,
        options: [
            { value: '10', text: '10' },
            { value: '25', text: '25' },
            { value: '50', text: '50' }
        ],
        selectedSearch: '訂單ID',
        optionsSearch: [
            { value: '訂單ID', text: '訂單ID' },
            { value: '訂單編號', text: '訂單編號' },
        ],
        selectedFinishDay: '全選',
        optionsFinishDay: [
            { value: '全選', text: '全選' },
            { value: '三天', text: '三天' },
            { value: '七天', text: '七天' }
        ],
        selectedStatus: '-請選擇-',
        optionsStatus: [
            { value: '-請選擇-', text: '-請選擇-', disabled: true},
            { value: '訂單成立', text: '訂單成立' },
            { value: '訂單取消', text: '訂單取消' },
            { value: '訂單完成', text: '訂單完成' }
        ],
        //Checkbox
        selectedCheckbox: [],
        checked: false,
        checkBtn: false,

        //評論
        evaluationList: [],

        //搜尋
        searchinput: '',
        searchOrderNumInput: '',
        searchBtn: false,
        searchNumBtn: false,

        //取得取消原因
        cancelContect: '',
        //回傳取消原因
        cancelReason: '',
        refundPersent: '',
        //確認是否可以改成完成
        orderDateComplete: false,
        orderStatusBtn: false
    },
    created() {
        this.getAllOrderList()

    },
    computed: {
        isVerify() {
            if (this.searchinput == '') return false
            else return true
        }
    },
    methods: {
        getAllOrderList() {
            let current = this.currentPage - 1
            let url = `/OrderApi/GetALLOrderList?currentPage=${current}&perPage=${this.perPage}&stauts=${this.tabIndex - 1}`
            httpGet(url)
                .then(res => {
                    if (res.status == 20000) {

                        this.orderAllList = res.data.contact.map((item) => ({
                            id: item.orderId,
                            name: item.userName,
                            number: item.orderName,
                            status: item.status,
                            totalPrice: item.totalPrice
                        }))
                        this.totalPage = res.data.totalCount
                    }
                })
        },
        orderAllListBtn(id) {
            let url = `/OrderApi/GetOrderContent?id=${id}`
            httpGet(url)
                .then(res => {
                    if (res.status == 20000) {
                        this.orderContent = res.data
                    }
                })
        },
        orderPetListBtn(id) {
            let url = `/OrderApi/GetOrderPetContent?id=${id}`
            httpGet(url)
                .then(res => {
                    if (res.status == 20000) {

                        this.petContect = res.data.map((item) => ({
                            orderId: item.orderId,
                            petId: item.orderPetId,
                            petName: item.petName,
                            petType: item.petType,
                            shapeType: item.shapeType,
                            petSex: item.petSex,
                            birthYear: item.birthYear,
                            ligation: item.ligation,
                            vaccine: item.vaccine,
                            petDiscription: item.petDiscription,
                            petIntro: item.petIntro

                        }))

                    }
                })
        },
        getOrderSuccess() {
            let current = this.currentPage - 1
            this.statusIndex = 0
            let url = `/OrderApi/GetOrderSuccessList?currentPage=${current}&perPage=${this.perPage}&stauts=${this.tabIndex - 1}`
            httpGet(url)
                .then(res => {
                    if (res.status == 20000) {

                        this.orderAllList = res.data.contact.map((item) => ({
                            id: item.orderId,
                            name: item.userName,
                            number: item.orderName,
                            status: item.status,
                            totalPrice: item.totalPrice
                        }))
                        this.totalPage = res.data.totalCount
                    }
                })
        },
        getOrderCancel(id) {

            let url = `/OrderApi/GetOrderCancelList?orderId=${id}`
            httpGet(url)
                .then(res => {
                    if (res.status == 20000) {
                        this.cancelContect = res.data
                    }
                })
        },
        getAlreadyOrderList() {
            let current = this.currentPage - 1
            this.statusIndex = 1
            let url = `/OrderApi/GetOrderAlreadyList?currentPage=${current}&perPage=${this.perPage}&stauts=${this.tabIndex - 1}`
            httpGet(url)
                .then(res => {
                    if (res.status == 20000) {

                        this.orderAllList = res.data.contact.map((item) => ({
                            id: item.orderId,
                            name: item.userName,
                            number: item.orderName,
                            status: item.status,
                            totalPrice: item.totalPrice
                        }))
                        this.totalPage = res.data.totalCount
                    }
                })
        },
        changeTabIndex(index) {
            this.tabIndex = index
            this.currentPage = 1
            if (index == 1) {
                if (this.statusIndex == 0) {
                    this.getOrderSuccess()
                    return
                }
                else {
                    this.getAlreadyOrderList()
                    return
                }
            }

            this.getAllOrderList()
        },
        getOrderEvaluation(id) {

            let url = `/OrderApi/GetOrderEvaluation?orderId=${id}`
            httpGet(url)
                .then(res => {
                    if (res.status == 0) {
                        this.evaluationList = ''
                    }
                    if (res.status == 20000 & res.data != null) {

                        this.evaluationList = res.data.map((item) => ({
                            evaluationId: item.evaluationId,
                            orderId: item.orderId,
                            userId: item.userId,
                            userType: item.userType,
                            evaluationScore: item.evaluationScore,
                            message: item.message,
                            createTime: item.createTime
                        }))

                    }

                    console.log(this.evaluationList)
                })

        },
        changeStatusidToId(id) {
            this.selectedStatus = '-請選擇-'
            this.cancelReason = ''
            this.refundPersent = ''
            this.changeStatusId = id
            let url = `/OrderApi/GetOrderDate?orderId=${id}`
            httpGet(url)
                .then(res => {
                    if (res.status == 20000 & res.data != null) {
                        this.orderDateComplete = res.data
                        console.log(this.orderDateComplete)
                    }
                })
        },
        changeHandleStatus(id) {
            this.changeStatusId = id
        },
        orderChangeStatus() {
            let data = {
                OrderId: this.changeStatusId,
                Status: 3
            }
            let url = `/OrderApi/ChangeOrderSuccessList`
            httpPost(url, data)
                .then(res => {
                    console.log(res)
                    if (res.message == '更新成功') {
                        toastr.success('更新成功')
                        $('#changeStatus').modal('hide')
                        this.getAllOrderList()
                    }
                    else {
                        toastr.error('更新失敗')
                        this.getAllOrderList()
                    }
                })
                .catch(err => toastr.error('更新失敗'))
        },
        handleOrderChangeStatus() {
            let status = this.handleStatusSwitch(this.selectedStatus)
            let data = {}
            let url = ''
            if (status == 1) {
                data = {
                    OrderId: this.changeStatusId,
                    Status: status,
                    CancelReason: this.cancelReason,
                    RefundPersent: this.refundPersent
                }
                url = `/OrderApi/ChangeOrderHandleStatus`
            }
            else {
                data = {
                    OrderId: this.changeStatusId,
                    Status: status
                }
                url = `/OrderApi/ChangeOrderSuccessList`
            }

            httpPost(url, data)
                .then(res => {
                    if (res.message == '更新成功') {
                        toastr.success('更新成功')
                        $('#orderHandle').modal('hide')
                        this.getAllOrderList()
                    }
                    else {
                        toastr.error('更新失敗')
                        this.getAllOrderList()
                    }
                })
                .catch(err => toastr.error('更新失敗'))
        },
        orderChangeStatusClear() {
            let url = `/OrderApi/ChangeOrderStatusClear`
            let data = this.selectedCheckbox
            httpPost(url, data)
                .then(res => {
                    if (res.message == '更新成功') {
                        toastr.success('更新成功')
                        $('#changeStatusClear').modal('hide')
                        this.getAllOrderList()
                        this.checked = false
                    }
                    else {
                        toastr.error('更新失敗')
                        this.getAllOrderList()
                    }
                })
                .catch(err => toastr.error('更新失敗'))
        },
        changeAllChecked() {
            if (this.checked) {
                this.selectedCheckbox = this.orderAllList.map((item) => item.id)
                console.log(this.selectedCheckbox)
            }
            else {
                this.selectedCheckbox = []
            }
        },

        serachOrderNum() {
            let url = `/OrderApi/GetOrderSearchNum?type=${this.tabIndex - 1}&name=${this.searchOrderNumInput}`

            httpGet(url)
                .then(res => {
                    console.log(res)
                    if (res.status == 0) {
                        this.orderAllList = ''
                        this.totalPage = 0
                        this.searchOrderNumInput = ''
                    }
                    if (res.status == 20000) {
                        list = [res.data.contact]
                        this.orderAllList = list.map((item) => ({
                            id: item.orderId,
                            name: item.userName,
                            number: item.orderName,
                            status: item.status,
                            totalPrice: item.totalPrice
                        }))
                        this.totalPage = res.data.totalCount

                        this.searchOrderNumInput = ''
                    }
                })
        },
        serachOrderNumStatus(input) {
            console.log(input)
            let url = `/OrderApi/GetOrderSearchNumStatusSuccess?type=${this.tabIndex - 1}&name=${this.searchOrderNumInput}&status=${input}`

            httpGet(url)
                .then(res => {
                    if (res.status == 0) {
                        this.orderAllList = ''
                        this.totalPage = 0
                        this.searchOrderNumInput = ''
                    }
                    if (res.status == 20000) {
                        list = [res.data.contact]
                        this.orderAllList = list.map((item) => ({
                            id: item.orderId,
                            name: item.userName,
                            number: item.orderName,
                            status: item.status,
                            totalPrice: item.totalPrice
                        }))
                        this.totalPage = res.data.totalCount

                        this.searchOrderNumInput = ''
                    }

                })
        },
        searchContact() {
            console.log(this.searchinput)
            let url = ''
            switch (this.selectedSearch) {
                case '訂單ID':
                    url = `/OrderApi/GetOrderSearchId?id=${this.searchinput}`
                    break;
                case '訂單編號':
                    url = `/OrderApi/GetOrderSearchNum?type=-1&name=${this.searchinput}`
                    break;
            }

            httpGet(url)
                .then(res => {
                    console.log(res)
                    if (res.status == 0) {
                        this.orderAllList = ''
                        this.totalPage = 0
                    }
                    if (res.status == 20000) {
                        list = [res.data.contact]
                        console.log(list)
                        this.orderAllList = list.map((item) => ({
                            id: item.orderId,
                            name: item.userName,
                            number: item.orderName,
                            status: item.status,
                            totalPrice: item.totalPrice
                        }))
                        this.totalPage = res.data.totalCount
                    }
                    this.searchinput = ''

                })
        },
        changeSelect() {
            this.perPage = this.selected
            this.currentPage = 1
            this.getAllOrderList()
        },
        changeOrderHandleToOther() {
            console.log(this.selectedStatus)
            console.log(this.orderStatusBtn)
            if (this.selectedStatus == '-請選擇-') {
                this.orderStatusBtn = false
            }
            else if (this.selectedStatus == '訂單完成' && !this.orderDateComplete) {
                this.orderStatusBtn = false
            }
            else if (this.selectedStatus == '訂單取消') {
                if ((this.cancelReason).trim() == '' || this.refundPersent == '') {
                    this.orderStatusBtn = false
                }
            }
            else {
                this.orderStatusBtn = true
            }
            
            console.log(this.orderStatusBtn)
        },
        changePage(current) {
            this.getAllContact()
            this.scrollToTop()
        },
        changeOrderAllPage(current) {
            this.getAllOrderList()
            this.scrollToTop()
        },
        changeOrderSuccessPage(current) {
            if (this.statusIndex == 0) {
                this.getOrderSuccess()
            }
            else {
                this.getAlreadyOrderList()
            }
            this.scrollToTop()
        },
        scrollToTop() {
            window.scrollTo(0, 0);
        },
        handleStatusSwitch(input) {
            switch (input) {
                case '訂單成立': return 0
                case '訂單取消': return 1
                case '訂單完成': return 2
                default: return 3
            }
        },
    },
    filters: {
        orderStatus(int) {
            switch (int) {
                case 0: return "訂單成立"
                case 1: return "訂單取消"
                case 2: return "訂單完成"
                case 3: return "處理中"
                default: return "發生錯誤"
            }

        },
        changeTime(time) {
            if (time === undefined) return ''
            return time.split('T')[0] + ' ' + time.split('T')[1].split(':')[0] + ':' + time.split('T')[1].split(':')[1]
        },
        changeInvoice(type) {
            switch (type) {
                case 1: return "一般發票"
                case 2: return "統編發票"
                default: return "發生錯誤"
            }
        },
        changeUserType(type) {
            switch (type) {
                case 1: return "消費者"
                case 2: return "保姆"
                default: return "發生錯誤"
            }
        },
    },
    watch: {
        'selectedCheckbox': {
            handler() {
                if (this.orderAllList.length == this.selectedCheckbox.length) {
                    this.checked = true
                }
                else {
                    this.checked = false
                }
                console.log(this.selectedCheckbox)
                if (this.selectedCheckbox.length == 0) {
                    this.checkBtn = false
                }
                else {
                    this.checkBtn = true
                }
            }
        },
        'searchOrderNumInput': {
            handler() {
                if ((this.searchOrderNumInput).trim() == '') {
                    this.searchNumBtn = false
                }
                else {
                    this.searchNumBtn = true
                }
            }
        },
        'searchinput': {
            handler() {
                let test = /^[+]{0,1}(\d+)$/

                if ((this.searchinput).trim() == '') {
                    this.searchBtn = false
                }
                else if (!test.test(this.searchinput) && this.selectedSearch == '訂單ID') {
                    this.searchBtn = false
                }
                else {
                    this.searchBtn = true
                }
            },
        },
        'selectedSearch': {
            handler() {
                let test = /^[+]{0,1}(\d+)$/

                if ((this.searchinput).trim() == '') {
                    this.searchBtn = false
                }
                else if (!test.test(this.searchinput) && this.selectedSearch == '訂單ID') {
                    this.searchBtn = false
                }
                else {
                    this.searchBtn = true
                }
            }
        },
        'cancelReason': {
            handler() {
                console.log(this.refundPersent)

                if ((this.cancelReason).trim() == '' || this.refundPersent == '') {
                    this.orderStatusBtn = false
                }
                else {
                    this.orderStatusBtn = true
                }
            }
        },
        'refundPersent': {
            handler() {
                if ((this.cancelReason).trim() == '' || this.refundPersent == '') {
                    this.orderStatusBtn = false
                }
                else {
                    this.orderStatusBtn = true
                }
            }
        }
    }
})