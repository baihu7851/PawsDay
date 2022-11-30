let contenter = new Vue({
    el: '#contactvue',
    data: {
        status: 1,
        index: 0,
        contactList: [],
        fields: [
            { key: 'index', label: '序號' },
            { key: 'id', label: 'ID' },
            { key: 'name', label: '姓名' },
            {
                key: 'createTime', label: '寄送時間',
                formatter(value, key, item) {
                    return value.split('T')[0] + ' ' + value.split('T')[1].split(':')[0] + ':' + value.split('T')[1].split(':')[1]
                }
            },
            { key: 'status', label: '狀態' },
            { key: 'actions', label: '功能' }
        ],
        //分頁設定
        perPage: 10,
        currentPage: 1,
        totalPage: 0,
        filter: '',
        contactinfo: [],
        selected: 10,
        options: [
            { value: '10', text: '10' },
            { value: '25', text: '25' },
            { value: '50', text: '50' }
        ],
        searchinput: '',
        isSubmit: false,
        isSearch: false
    },
    created() {
        this.getAllContact()
    },
    methods: {
        getAllContact() {
            let current = this.currentPage - 1
            let url = `/MessageApi/GetALLContact?currentPage=${current}&perPage=${this.perPage}`
            httpGet(url)
                .then(res => {                   
                    if (res.status == 20000) {                       
                        this.contactList = res.data.contact.map((item, index) => ({
                            index: index + 1,
                            id: item.contactId,
                            name: item.name,
                            title: item.title,
                            email: item.email,
                            phone: item.phone,
                            createTime: item.createTime,
                            contactContent: item.contactContent,
                            replyContent: item.replyContent,
                            replyTime: item.replyTime,
                            status: item.status,
                        }))
                       
                        this.totalPage = res.data.totalCount
                    }
                })
        },
        changestatus(id) {
            this.contactinfo = this.contactList.filter(x => x.id == id)[0]
            this.contactinfo.createTime = this.filterDatetime(this.contactinfo.createTime)            
            if (this.contactinfo.status == true) {               
                    this.contactinfo.replyTime = this.filterDatetime(this.contactinfo.replyTime)
            }
            if (this.contactinfo.replyContent == null) {
                this.isSubmit = false
            }
        },
        searchContact() {

            let input = this.searchinput
            let url = `/MessageApi/GetSearchContact?name=${input}`
            httpGet(url)
                .then(res => {
                    this.contactList = res.data.contact.map((item, index) => ({
                        index: index + 1,
                        id: item.contactId,
                        name: item.name,
                        title: item.title,
                        email: item.email,
                        phone: item.phone,
                        createTime: item.createTime,
                        contactContent: item.contactContent,
                        replyContent: item.replyContent,
                        replyTime: item.replyTime,
                        status: item.status,
                    }))      
                    
                    this.totalPage = res.data.totalCount
                    this.searchinput = ''
                    this.scrollToTop()
                    
                })
        },
        sendAnswer(id) {
            let url = `/MessageApi/CreateContactAnswer`
            let contact = { ContactId: this.contactinfo.id, ContactAnswer: this.contactinfo.replyContent }
            httpPost(url, contact)
                .then(res => {
                    console.log(res)
                    if (res.status == 20000) {
                        toastr.success('新增成功')
                        $('#contactModal').modal('hide')
                        this.getAllContact()
                    }
                    else {
                        toastr.error('新增失敗')
                    }
                })
                .catch(err => toastr.error('新增失敗'))

        },
        changeSelect(input) {
            this.perPage = input
            this.currentPage = 1
            this.getAllContact()
        },
        changePage(current) {           
            this.getAllContact()            
            this.scrollToTop()
        },
        scrollToTop() {
            window.scrollTo(0, 0);
        },
        filterDatetime(time) {
            return time.split('T')[0] + ' ' + time.split('T')[1].split(':')[0] + ':' + time.split('T')[1].split(':')[1]
        }
    },
    watch: {
        'contactinfo.replyContent': {
            handler(){
                if ((this.contactinfo.replyContent).trim() == '') {
                    this.isSubmit = false
                }
                else {
                    this.isSubmit = true
                }
            }
        },
        'searchinput': {
            handler() {
                if (this.searchinput == '') {
                    this.isSearch = false
                }
                else {
                    this.isSearch = true
                }
            }
        }
    }
})