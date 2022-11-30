let content = new Vue({
    el: '#linebotvue',
    data: {
        //頁籤
        sheet: "關鍵字設定",
        keyword: true,
        news: false,
        survey: false,
        custom: false,
        //new=1,custom=2,survey=4
        tempalteId: "",
        //主表單
        form: {
            title: "",
            text: "",
            image: "",
            detail: [],
        },
        check: {
            inputerror: false,
        },
        //舊資料
        oldtitle: '',
        oldtext: '',
        oldimgurl: '',
        olddetail: [],
        fields: [
            { key: 'text', label: '文字' },
            { key: 'type', label: '類別', formatter(value, key, item) { 
                if (value == 'message') { return '純文字' }
                else if (value == 'url') { return '連結' }
                else { return '-' }
            } },
            { key: 'url', label: '連結', formatter(value, key, item) { 
                if (value ==null) { return '-' }
                else { return value}
            } }
        ],
        //關鍵字
        keywords: [],
        kwfields: [
            { key: 'index', label: '序號' },
            //{key:'id',label:'編號'},
            { key: 'keyword', label: '關鍵字' },
            {
                key: 'action', label: '連結動作', formatter(value, key, item) {
                    if (value == 'search') { return '開始搜尋' }
                    else if (value == 'question') { return '開始發問' }
                    else if (value == 'service') { return '服務介紹' }
                    else if (value == 'process') { return '預約流程' }
                    else if (value == 'website') { return '前往官網' }
                    else if (value == 'survey') { return '調查所' }
                    else if (value == 'new') { return '最新消息' }
                    else { return '自訂功能' }
                }
            },
            { key: 'actions', label: '功能' }
        ],
        //關鍵字選項
        options: [
            { text: '開始搜尋', value: 'search' },
            { text: '開始發問', value: 'question' },
            { text: '服務介紹', value: 'service' },
            { text: '預約流程', value: 'process' },
            { text: '前往官網', value: 'website' },
            { text: '最新消息', value: 'new' },
            { text: '調查所', value: 'survey' },
            { text: '自訂功能', value: 'custom' },
        ],
        //api用
        url: '',
        request: {},
        apimessage: '',
        deleteid: ''
    },
    created() {
        this.getkeyword()
    },
    computed: {
        checksave() {
            let check = false
            this.form.detail.forEach((item) => {
                if (Object.values(item).filter(i => i == '').length === 0) { check = true }
                else {
                    if (item.url == '' && item.type == 'message') { check = true }
                    else if (this.survey == true && item.text!='') {check = true }
                    else { check = false }
                }
            })
            return check
        }
    },
    methods: {
        //取得模板
        gettemplate() {
            this.url = `/LineApi/GetTemplate?templateid=${this.tempalteId}`
            httpGet(this.url)
                .then(res => {
                    if (res.status == 20000) {
                        let data = res.data
                        this.olddetail = data.detail
                        this.oldtitle = data.title
                        this.oldtext = data.text
                        this.oldimgurl = data.image
                    }
                })
        },
        //將選項推入detail陣列
        adddetail() {
            let temp = {
                type: "",
                text: "",
                url: ""
            }
            this.form.detail.push(temp)
        },
        //控制頁籤
        sheetdefault() {
            this.keyword = false
            this.news = false
            this.survey = false
            this.custom = false
        },
        isKeyWord() {
            this.sheet = "關鍵字設定"
            this.tempalteId = ''
            this.sheetdefault()
            this.keyword = true
            this.getkeyword()
        },
        isnews() {
            this.sheet = "最新消息設定"
            this.tempalteId = 1
            this.sheetdefault()
            this.news = true
            this.gettemplate()
            this.clearform()
        },
        issurvey() {
            this.sheet = "調查所設定"
            this.tempalteId = 4
            this.sheetdefault()
            this.survey = true
            this.gettemplate()
            this.clearform()
        },
        iscustom() {
            this.sheet = "自訂功能設定"
            this.tempalteId = 2
            this.sheetdefault()
            this.custom = true
            this.gettemplate()
            this.clearform()
        },
        clearform() {
            this.form.detail = []
            this.form.title = ''
            this.form.text = ''
            this.form.image=''
        },
        //儲存Template
        save() {
            let request = this.form
            this.url = `/LineApi/UpdateTemplate?templateid=${this.tempalteId}`
            httpPost(this.url, {
                ...request
            })
                .then(res => {
                    if (res.status == 20000) {
                        this.gettemplate()
                        toastr.success("儲存成功")
                    }
                    else {
                        toastr.error("儲存失敗")
                    }
                })
        },
        //取得keyword
        getkeyword() {
            this.url = '/LineApi/GetKeyWord'
            httpGet(this.url)
                .then(res => {
                    if (res.status == 20000) {
                        this.keywords = res.data.map((item, index) => ({
                            index: index + 1,
                            id: item.id,
                            keyword: item.keyword,
                            action: item.action,
                            canbeedit: item.canbeedit
                        }))
                    }
                })
        },
        //存取keyword
        addkeywordrow() {
            let temp = {
                id: "",
                keyword: "",
                action: "",
                canbeedit: true
            }
            this.keywords.push(temp)
        },
        changeselect(data) {
            if (data.type != 'url') {
                data.url = ''
            }
        },
        savekeyword(row) {
            this.request.keyword = row.item.keyword
            this.request.action = row.item.action
            if (row.item.id == '') {
                this.url = `/LineApi/CreateKeyWord`
                this.apimessage = '新增成功'
                this.sendkeywordapi()
            }
            else {
                this.request.id = row.item.id
                this.url = `/LineApi/UpdateKeyWord`
                this.apimessage = '儲存成功'
                this.sendkeywordapi()
            }
        },
        deletekeyword(row) {
            this.deleteid = row.item.id
            this.url = `/LineApi/DeleteKeyWord?keywordid=${this.deleteid}`
            httpPost(this.url)
                .then(res => {
                    if (res.status == 20000) {
                        toastr.success("刪除成功")
                        this.getkeyword()
                    }
                    else {
                        toastr.error("刪除失敗")
                    }
                })
        },
        sendkeywordapi() {
            httpPost(this.url, {
                ...this.request
            })
                .then(res => {
                    if (res.status == 20000) {
                        toastr.success(this.apimessage)
                        this.getkeyword()
                    }
                    else {
                        toastr.error("更新失敗")
                    }
                })
        }
    }
})
