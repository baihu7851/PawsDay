let content = new Vue({
    el: '#sittervue',
    data: {
        //取list相關參數
        status: 0,
        index: 0,
        takecount: 10,
        sitterlist: [],
        sitterinfo: [],
        fields: [
            { key: 'index', label: '序號' },
            { key: 'id', label: 'ID' },
            { key: 'name', label: '保姆名字' },
            { key: 'score', label: '測驗分數' },
            {
                key: 'submitTime', label: '提交時間',
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
        updatestatus: ''
    },
    created() {
        this.getAllWaitSitter()
    },
    computed: {
        //驗證搜尋input是否為空值
        isVerify() {
            if (this.searchinput == '') return false
            else return true
        },
        //如果未待審核打開通過、不通過btn
        waitToCheck() {
            if (this.status == 0) return true
            else return false
        },
        //如果未通過打開通過btn
        hasRejected() {
            if (this.status == 2) return true
            else return false
        },
        //如果已通過打開不通過btn
        hasApproved() {
            if (this.status == 1) return true
            else return false
        },
    },
    methods: {
        getAllWaitSitter() {
            this.status = 0
            this.currentPage = 1
            this.getAllSitter()
        },
        getAllNormalSitter() {
            this.status = 1
            this.currentPage = 1
            this.getAllSitter()
        },
        getAllRejectSitter() {
            this.status = 2
            this.currentPage = 1
            this.getAllSitter()
        },
        getAllSitter() {
            let url = `/SitterApi/SitterListByStatus?status=${this.status}&index=${this.index}&count=${this.takecount}`
            httpGet(url)
                .then(res => {
                    if (res.status == 20000) {
                        this.createlist(res)
                    }
                })
        },
        createlist(res) {
            this.sitterlist = res.data.map((item, index) => ({
                index: index + 1,
                id: item.memberId,
                name: item.name,
                score: item.score,
                submitTime: item.submitTime,
                status: item.status
            }))
            this.totalPage = res.data[0].count
        },
        //個人會員資訊
        showData(item) {
            httpGet(`/SitterApi/GetSitterInfo?id=${item.id}`)
                .then(res => {
                    if (res.status == 20000) {
                        let info = res.data
                        this.sitterinfo = info
                        $('#info-modal').modal('show')
                    }
                })
        },
        //通過or復權
        returnstatus(row) {
            let id = row.item.id
            this.updatestatus = 1
            this.changestatus(id)
        },
        rejectstatus(row) {
            let id = row.item.id
            this.updatestatus = 2
            this.changestatus(id)
        },
        subspendstatus(row) {
            let id = row.item.id
            this.updatestatus = 3
            this.changestatus(id)
        },
        changestatus(id) {
            httpPost(`/SitterApi/UpdateSitterStatus?id=${id}&updatestatus=${this.updatestatus}`)
                .then(res => {
                    if (res.status == 20000) {
                        toastr.success('變更成功')
                        this.getAllSitter()
                    }
                })
        },
        search() {
            let input = this.searchinput
            let url = ''
            switch (this.selected) {
                case 's-id':
                    url = `/SitterApi/SitterListByID?id=${input}`
                    break;
                case 's-name':
                    url = `/SitterApi/SitterListByName?name=${input}`
                    break;
                case 's-email':
                    url = `/SitterApi/SitterListByMail?email=${input}`
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
            this.getAllSitter()
        }
    },
    filters: {
        //轉換時間格式
        datetime(date) {
            return `${new Date(date).getFullYear()}-${new Date(date).getMonth()}-${new Date(date).getDate()}`
        },
        parseanswer(answer) {
            if (answer != null || answer != undefined) {
                let str = ''
                for (let index = 0; index < answer.length; index++) {
                    str += `第${index + 1}題:${answer[index]}\r\n`
                }
                return str
            }
        }
    }
})