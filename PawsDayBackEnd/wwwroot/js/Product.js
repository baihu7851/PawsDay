let content = new Vue({
    el: '#productvue',
    data: {
        //取list相關參數
        status: 0,
        index: 0,
        takecount: 10,
        productlist: [],
        productinfo: [],
        pricelist: [],
        imglist: [],
        fields: [
            { key: 'index', label: '序號' },
            { key: 'id', label: '商品ID' },
            { key: 'sitterid', label: '保姆ID' },
            { key: 'service', label: '服務類型' },
            {
                key: 'createTime', label: '建立時間',
                formatter(value, key, item) {
                    return value.split('T')[0] + ' ' + value.split('T')[1].split(':')[0] + ':' + value.split('T')[1].split(':')[1]
                }
            },
            { key: 'status', label: '狀態' },
            { key: 'actions', label: '變更狀態' }
        ],
        pricefields: [
            { key: 'pet', label: '寵物類型' },
            { key: 'shape', label: '寵物體型' },
            { key: 'price', label: '一般價格' },
            { key: 'nightPrice', label: '夜間價格' }
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
            { value: 's-id', text: '以商品ID查詢' },
            { value: 's-sittername', text: '以保姆名字查詢' },
            { value: 's-sitterid', text: '以保姆ID查詢' }
        ],
        searchinput: '',
        searchtype: '',
        statuscheck: '',
        updatestatus: ''
    },
    created() {
        this.getAllNormalProduct()
    },
    computed: {
        //驗證搜尋input是否為空值
        isVerify() {
            if (this.searchinput == '') return false
            else return true
        },
        //鎖定復權與停權按鈕
        isnormalproduct() {
            if (this.statuscheck == true) return true
            else return false
        }
    },
    methods: {
        getAllNormalProduct() {
            this.status = 0
            this.currentPage=1
            this.statuscheck = true
            this.getAllProduct()
        },
        getAllOffsaleProduct() {
            this.status = 1
            this.currentPage = 1
            this.statuscheck = false
            this.getAllProduct()
        },
        getAllProduct() {
            let url = `/ProductApi/ProductListByStatus?status=${this.status}&index=${this.index}&count=${this.takecount}`
            httpGet(url)
                .then(res => {
                    if (res.status == 20000) {
                        this.createlist(res)
                    }
                })
        },
        createlist(res) {
            this.productlist = res.data.map((item, index) => ({
                index: this.index + index + 1,
                id: item.productId,
                sitterid: item.sitterId,
                service: item.service,
                createTime: item.createTime,
                status: item.status
            }))
            this.totalPage = res.data[0].count
        },
        //個人會員資訊
        showData(item) {
            let url = `/ProductApi/ProductInfo?id=${item.id}`
            httpGet(url)
                .then(res => {
                    if (re.status == 20000) {
                        let info = res.data
                        this.productinfo = info
                        this.pricelist = info.price
                        this.imglist = info.imageUrl
                        $('#info-modal').modal('show')
                    }
                })
        },
        //上架
        onsalestatus(row) {
            let id = row.item.id
            this.updatestatus = 0
            this.changestatus(id)
        },
        //下架
        offsalestatus(row) {
            let id = row.item.id
            this.updatestatus = 1
            this.changestatus(id)
        },
        changestatus(id) {
            let url = `/ProductApi/ProductStatus?id=${id}&status=${this.updatestatus}`
            httpPost(url)
                .then(res => {
                    if (res.status == 20000) {
                        toastr.success('變更成功')
                        this.getAllProduct()
                    }
                })
        },
        //刪除
        deleteproduct(row) {
            let id = row.item.id
            //刪除modal
            this.$bvModal.msgBoxConfirm('確定刪除資料', {
                title: '警告',
                size: 'sm',
                okVariant: 'danger',
                okTitle: '確認',
                cancelTitle: '取消',
                footerClass: 'p-2',
                hideHeaderClose: true,
                centered: true,
            }).then(value => {
                let url = `/ProductApi/Delete?id=${id}`
                httpPost(url)
                    .then(res => {
                        if (res.status == 20000) {
                            toastr.success('刪除成功')
                            this.getAllProduct()
                        }
                    })
            }).catch(err => {
                console.warn(err);
            });
        },
        getCare() {
            this.searchtype = 1
            this.selected = 's-type'
            this.search()
        },
        getSalon() {
            this.searchtype = 2
            this.selected = 's-type'
            this.search()
        },
        getWalking() {
            this.searchtype = 3
            this.selected = 's-type'
            this.search()
        },
        search() {
            let input = this.searchinput
            let url = ''
            switch (this.selected) {
                case 's-id':
                    url = `/ProductApi/ProductListById?id=${input}`
                    break;
                case 's-sittername':
                    url = `/ProductApi/ProductListBySitterName?name=${input}`
                    break;
                case 's-sitterid':
                    url = `/ProductApi/ProductListBySitterId?id=${input}`
                    break;
                case 's-type':
                    url = `/ProductApi/ProductListByType?type=${this.searchtype}&status=${this.status}&index=${this.index}&count=${this.takecount}`
                    break;
            }
            httpGet(url)
                .then(res => {
                    if (res.status == 20000) {
                        this.createlist(res)
                        this.totalPage = res.count
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
            this.getAllProduct()
            //傳入要搜第幾筆index(每次click要用index*takecount)、takecount要拿幾筆(綁一個變數)
            //傳回 count 全部有幾筆
            //currentPage 哪一頁
            //totalpage 總共有幾頁
        }
    },
    filters: {
        //轉換時間格式
        datetime(date) {
            return `${new Date(date).getFullYear()}-${new Date(date).getMonth()}-${new Date(date).getDate()}`
        }
    }
})
