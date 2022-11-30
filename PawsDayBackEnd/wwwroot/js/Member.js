
let content = new Vue({
    el: '#membervue',
    data: {
        //取list相關參數
        status: 1,
        index: 0,
        takecount: 10,
        memberlist: [],
        memberinfo: [],
        fields: [
            { key: 'index', label: '序號' },
            { key: 'id', label: 'ID' },
            { key: 'name', label: '姓名/暱稱' },
            { key: 'registerType', label: '註冊方式' },
            {
                key: 'createTime', label: '加入時間',
                formatter(value, key, item) {
                    return value.split('T')[0] + ' ' + value.split('T')[1].split(':')[0] + ':' + value.split('T')[1].split(':')[1]
                }
            },
            { key: 'status', label: '狀態' },
            { key: 'actions', label: '變更權限' }
        ],
        //分頁設定
        perPage: 10,
        currentPage: 1,
        totalPage: 0,
        filter: '',
        //搜尋選項
        selected: 's-ori',
        options: [
            { value: 's-ori', text: '--請選擇查詢方式--', disabled: true },
            { value: 's-id', text: '以ID查詢' },
            { value: 's-name', text: '以名字查詢' },
            { value: 's-email', text: '以Email查詢' }
        ],
        searchinput: '',
        statuscheck: '',
        updatestatus: ''
    },
    created() {
        this.getAllNormalMember()
    },
    computed: {
        //驗證搜尋input是否為空值
        isVerify() {
            if (this.searchinput == '') return false
            else return true
        },
        //鎖定復權與停權按鈕
        isnormalmember() {
            if (this.statuscheck == true) return true
            else return false
        }
    },
    methods: {
        getAllNormalMember() {
            this.status = 1
            this.currentPage=1
            this.statuscheck = true
            this.getAllMember()
        },
        getAllSuspendMember() {
            this.status = 3
            this.currentPage = 1
            this.statuscheck = false
            this.getAllMember()
        },
        getAllMember() {
            let url = `/MemberApi/MemberListByStatus?status=${this.status}&index=${this.index}&count=${this.takecount}`
            httpGet(url)
                .then(res => {
                    if ( res.status == 20000) {
                        this.createlist(res)
                    }
                })
        },
        createlist(res) {
            this.memberlist = res.data.map((item, index) => ({
                index: index + 1,
                id: item.memberId,
                name: item.name,
                registerType: item.registerType,
                createTime: item.createTime,
                status: item.status
            }))
            this.totalPage = res.data[0].count
        },
        //個人會員資訊
        showData(item) {
            httpGet(`/MemberApi/GetMemberInfo?id=${item.id}`)
                .then(res => {
                    if (res.status == 20000) {
                        let info = res.data
                        this.memberinfo = info
                        $('#info-modal').modal('show')
                    }
                })
        },
        //復權
        returnstatus(row) {
            let id = row.item.id
            this.updatestatus = 1
            this.changestatus(id)
        },
        //停權
        stopstatus(row) {
            let id = row.item.id
            this.updatestatus = 3
            this.changestatus(id)
        },
        changestatus(id) {
            httpPost(`/MemberApi/UpdateMemberStatus?id=${id}&updatestatus=${this.updatestatus}`)
                .then(res => {
                    if (res.status == 20000) {
                        toastr.success('變更成功')
                        this.getAllMember()
                    }
                })
        },
        search() {
            let input = this.searchinput
            let url = ''
            switch (this.selected) {
                case 's-id':
                    url = `/MemberApi/MemberListByID?id=${input}`
                    break;
                case 's-name':
                    url = `/MemberApi/MemberListByName?name=${input}`
                    break;
                case 's-email':
                    url = `/MemberApi/MemberListByMail?email=${input}`
                    break;
            }
            httpGet(url)
                .then(res => {
                    if (res.status == 20000) {
                        this.createlist(res)
                        this.totalPage = res.data[0].count
                        toastr.success('查詢成功')
                    }
                    else {
                        toastr.error('查詢失敗')
                    }
                })
        },
        //清空搜尋input
        clean() {
            document.querySelector('#searchinput').value = ""
            this.searchinput = ''
        },
        changepage() {
            let page = this.currentPage - 1
            this.index = page * this.takecount
            this.getAllMember()
        }
    },
    filters: {
        //轉換時間格式
        datetime(date) {
            return `${new Date(date).getFullYear()}-${new Date(date).getMonth()}-${new Date(date).getDate()}`
        }
    }
})