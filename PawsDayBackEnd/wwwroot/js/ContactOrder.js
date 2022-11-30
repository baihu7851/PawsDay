let contenter = new Vue({
    el: '#contactvue',
    data: {
        index: 0,
        //會員或保姆
        status: 1,
        tabIndex: 0,
        //資料
        contactList: [],
        contactinfo: [],
        fields: [
            { key: 'index', label: '序號' },
            { key: 'id', label: '訂單ID' },
            { key: 'number', label: '訂單編號' },
            { key: 'name', label: '姓名' },
            { key: 'status', label: '狀態' },
            { key: 'actions', label: '功能' }
        ],
        //分頁設定
        perPage: 10,
        currentPage: 1,
        totalPage: 0,
        filter: '',
        //下拉筆數
        selected: 10,
        options: [
            { value: '10', text: '10' },
            { value: '25', text: '25' },
            { value: '50', text: '50' }
        ],
        //下拉搜尋
        selectedSearch: '訂單ID',
        optionsSearch: [
            { value: '訂單ID', text: '訂單ID' },
            { value: '訂單編號', text: '訂單編號' },
            { value: '會員ID', text: '會員ID' },
            { value: '會員姓名', text: '會員姓名' },
        ],
        searchinput: '',
        isSearch: false,
        //回覆訊息
        message: '',
        isMessage: false
    },
    created() {
        this.getAllContact()
    },
    methods: {
        getAllContact() {
            let current = this.currentPage - 1
            let url = `/MessageApi/GetOrderContact?currentPage=${current}&perPage=${this.perPage}&type=${this.tabIndex + 1}`
            httpGet(url)
                .then(res => {
                    console.log(res.data)
                    if (res.status == 20000) {

                        this.contactList = res.data.contact.map((item, index) => ({
                            index: index + 1,
                            id: item.orderId,
                            name: item.userName,
                            number: item.orderNum,
                            userId: item.userId,
                            status: item.lastAnswerIsUser,
                            content: item.orderContent
                        }))

                        this.totalPage = res.data.totalCount
                    }
                })
        },
        changestatus(item) {
            this.contactinfo = this.contactList.filter(x => x.id == item.path[2].cells[1].innerText)[0]
        },
        searchContact() {
            let url = ''
            this.currentPage = 1
            switch (this.selectedSearch) {
                case '訂單ID':
                    url = `/MessageApi/GetOrderContactOrderId?currentPage=${this.currentPage - 1}&perPage=${this.perPage}&type=${this.tabIndex + 1}&searchtype=ID&input=${this.searchinput}`
                    break;
                case '訂單編號':
                    url = `/MessageApi/GetOrderContactOrderId?currentPage=${this.currentPage - 1}&perPage=${this.perPage}&type=${this.tabIndex + 1}&searchtype=Number&input=${this.searchinput}`
                    break;
                case '會員ID':
                    url = `/MessageApi/GetOrderContactMemberId?currentPage=${this.currentPage - 1}&perPage=${this.perPage}&type=${this.tabIndex + 1}&input=${this.searchinput}`
                    break;
                case '會員姓名':
                    url = `/MessageApi/GetOrderContactMemberName?currentPage=${this.currentPage - 1}&perPage=${this.perPage}&type=${this.tabIndex + 1}&input=${this.searchinput}`
                    break;
            }

            httpGet(url)
                .then(res => {
                    if (res.status == 20000) {

                        this.contactList = res.data.contact.map((item, index) => ({
                            index: index + 1,
                            id: item.orderId,
                            name: item.userName,
                            number: item.orderNum,
                            userId: item.userId,
                            status: item.lastAnswerIsUser,
                            content: item.orderContent
                        }))

                        this.totalPage = res.data.totalCount
                        this.searchinput=''
                    }

                })
        },
        search() {
            let test = /^[+]{0,1}(\d+)$/                
            console.log(this.selectedSearch)
            console.log(test.test(this.searchinput))
            if (!test.test(this.searchinput) && this.selectedSearch == "訂單ID") {
                this.isSearch = false
            }
            else if (!test.test(this.searchinput) && this.selectedSearch == "會員ID") {
                this.isSearch = false
            }
            else {
                this.isSearch = true
            }
        },
        sendAnswer() {
            let url = `/MessageApi/CreateOrderAnswer`
            let contact =
            {
                OrderId: this.contactinfo.id,
                UserId: this.contactinfo.userId,
                Message: this.message,
                UserType: this.tabIndex + 1
            }

            httpPost(url, contact)
                .then(res => {
                    console.log(res)
                    if (res.status == 20000) {
                        toastr.success('新增成功')
                        $('#contactModal').modal('hide')
                        this.message = ''
                        this.getAllContact()
                    }
                    else {
                        toastr.error('新增失敗')
                        this.message = ''
                        this.getAllContact()
                    }
                })
                .catch(err => toastr.error('新增失敗'))
        },
        changeSelect() {
            this.perPage = this.selected
            this.currentPage = 1
            this.getAllContact()
            this.scrollToTop()
        },
        changeUser(id) {
            this.tabIndex = id
            this.currentPage = 1
            this.getAllContact()
            this.scrollToTop()
        },
        changePage(current) {
            this.getAllContact()
            this.scrollToTop()
        },
        scrollToTop() {
            window.scrollTo(0, 0);
        }
    },
    filters: {
        speakerStatus(bool) {
            return bool == true ? '消費者' : '平台'
        },
        changeTime(time) {
            return time.split('T')[0] + ' ' + time.split('T')[1].split(':')[0] + ':' + time.split('T')[1].split(':')[1]
        }
    },
    watch: {
        'message': {
            handler(){
                if ((this.message).trim() == '') {
                    this.isMessage = false
                }
                else {
                    this.isMessage = true
                }                
            }
        },
        'searchinput': {
            handler() {
                let test = /^[+]{0,1}(\d+)$/                
                if ((this.searchinput).trim() == '') {
                    this.isSearch = false
                }
                else if (!test.test(this.searchinput) && this.selectedSearch == "訂單ID") {
                    this.isSearch = false
                }
                else if (!test.test(this.searchinput) && this.selectedSearch == "會員ID") {
                    this.isSearch = false
                }
                else {
                    this.isSearch = true
                }
            }
        },
    }
    
})