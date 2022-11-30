var petQuantity = 0;

var array = []


//DOM

var cartItemUl = document.querySelector('.cart-itemlist');
const deleteTriggerBtn = document.querySelector('#checkout-deleteitem');//觸發單選刪除的modal
var hiddenIds = document.querySelectorAll('.hidden-for-cartdetailid');
var cartItems = document.querySelectorAll('.valid-list');
const selectAllBtn = document.querySelector('#checkoutall-label'); //全選的按鈕
let checkOutBtn = document.querySelector('#checkout-btn');
const cartItemCheckboxs = document.querySelectorAll('.valid-list > input[type="checkbox"]'); //所有購物單上的checkbox
var cartItemList = document.querySelectorAll('.valid-list');

var petNumberInputs = document.querySelectorAll('.pet-change');

var accordionBodies = document.querySelectorAll('.valid-list .accordion-body');




//總商品合計begin

var productQuantity = document.querySelector('.productQuantity');
//總商品合計end
var hiddenIndexList = document.querySelector('.hidden-cartItemIndexList');

/*var hiddenJsonCartIds = document.querySelector('.forJsonCartIds');*/
var finalTotalPrice = document.querySelector('.finalTotalPrice');

//Window.onload
window.onload = () => {
    /*checkOutBtn.disabled = true;*/

    //CheckTotalPrice();

    //ResetEachCheckBox();

    //SelectAllBoxes();

    //SetOptionByCart();

    //UpdatePetInfo();
   
    
    ////刪除已選的Model確認Btn要加入刪除功能
    //deleteTriggerBtn.disabled = true; //一開始無勾選不能點
    //var deleteCfmBtn = document.querySelector('.select-deleted-btn');
    //deleteCfmBtn.addEventListener('click', () => {
    //    deleteBtnClick();
    //})

    ////刪除全部過期商品
    //var deleteExpiredBtn = document.querySelector('.delete-all-expired');
    //deleteExpiredBtn.addEventListener('click', () => {
    //    deleteAllExpiredItem();

    //});

}

function ResetEachCheckBox() {
    cartItemList.forEach(cartItem => {
        var box = cartItem.querySelector('.moneycheckbox');
        box.checked = false;

        //檢查是否有選擇產品，否則不能前往結帳
        box.addEventListener('click', () => {
            checkCanSubmit();
            

        })

    })

}
function checkCanSubmit() {
    var arr = []

    cartItemCheckboxs.forEach(box => {
        var value = box.checked;
        arr.push(value);
        //console.log(arr)
    })

    var isChecked = arr.some(val => val == true)
    //console.log(isChecked)
    if (isChecked) {
        checkOutBtn.disabled = false;
        checkOutBtn.style.backgroundColor = 'var(--clr-border)';
    }
    else {
        checkOutBtn.disabled = true;
        checkOutBtn.style.backgroundColor = 'var(--clr-bg-g)'
    }

}

//設定each的checkbox點擊後，要有的功能
var totalProductQuan = 0;
var selectedIndexList = [] //商品被選擇做動作，所使用的陣列
function CheckTotalPrice() {
    
    
    cartItemList.forEach((cartItem,count) => {
        var box = cartItem.querySelector('.moneycheckbox');
        var petSelects = cartItem.querySelectorAll('.petTypeSelect');
        var shapeSelects = cartItem.querySelectorAll('.shapeTypeSelect');
        /*var hiddenPriceInput = cartItem.querySelector('.hiddenPrice');*/
            
        //let cartId = box.value;
        box.addEventListener('click', () => {
            let priceLabel = cartItem.querySelector('.discount-price-text');
            let price = parseInt(priceLabel.innerText);
            /*hiddenPriceInput.value = price;*/

            var count = 0;
            var cartCheckboxs = document.querySelectorAll('.valid-list > input[type="checkbox"]');
            cartCheckboxs.forEach((c, ind) => {
                if (c.checked) {
                    count++;
                }

            })
            if (count != 0) {
                deleteTriggerBtn.disabled = false;
            }
            else {
                deleteTriggerBtn.disabled = true;
            }

            //console.log('hello');
            var totalPrice = parseInt(finalTotalPrice.innerText);
            if (box.checked) {
                

                totalProductQuan++
                console.log(totalProductQuan)

                totalPrice += price;
                //console.log(`增加價錢${price}`)
                //console.log(price);
                //console.log(totalPrice);

                petSelects.forEach(pet => {
                    pet.disabled = true;
                })
                shapeSelects.forEach(pet => {
                    pet.disabled = true;
                })

                selectedIndexList.push(count)
                

            }
            else {
               

                totalProductQuan--
                console.log(totalProductQuan)


                totalPrice -= price;

                petSelects.forEach(pet => {
                    pet.disabled = false;
                })
                shapeSelects.forEach(pet => {
                    pet.disabled = false;
                })

                var selectedIndex = selectedIndexList.indexOf(count);
                selectedIndexList.splice(selectedIndex, 1);
                
                

            }
            selectedIndexList.sort((a, b) => a - b )
            var JSselectedIndexList = JSON.stringify(selectedIndexList);
            hiddenIndexList.value = JSselectedIndexList;


            productQuantity.innerText = `${totalProductQuan} 件商品合計`;
            finalTotalPrice.innerText = `${totalPrice}`;


        });
    })


   
}

/*checkbox全選*/
var allIsSelect = false;
function SelectAllBoxes() {
    

    allIsSelect = true;
    
     //重設總共勾選商品
    

    //按下去全選
    selectAllBtn.addEventListener('click', () => {
        var reloadCartList = document.querySelectorAll('.valid-list');

        var cartCheckboxs = document.querySelectorAll('.valid-list > input[type="checkbox"]'); 
        
        var count = 0;
        var allCount = cartCheckboxs.length;
        cartCheckboxs.forEach((c, ind) => {
            if (c.checked) {
                count++;
            }

        })

        allIsSelect = count == allCount ? false : true;


        selectedIndexList = [];
        /*cartIdsList = []*/
        totalProductQuan = 0;
        //step1: 清除最終total價格
        finalTotalPrice.innerText = '0';


        ///cartIdsList.push(parseInt(cartId))
        cartCheckboxs.forEach((c, ind) => {
    
             // 1. 所有商品checkbox被勾選
            c.checked = allIsSelect? true : false;
            if (allIsSelect) {

                totalProductQuan++
                selectedIndexList.push(ind);
                deleteTriggerBtn.disabled = false;

            }
            else {

                totalProductQuan = 0;
                selectedIndexList = [];
                deleteTriggerBtn.disabled = true;
            }

        })
        checkCanSubmit();
        productQuantity.innerText = `${totalProductQuan} 件商品合計`; //渲染所有商品數量

        var JSselectedIndexList = JSON.stringify(selectedIndexList);
        hiddenIndexList.value = JSselectedIndexList;
        //console.log(hiddenIndexList.value)


        //3. 抓取所有商品的價格，加入總Total

        
        //step2: 
        var cartTotalPrice = 0;
        

        reloadCartList.forEach(cartItem => {
                //step2: disable全部寵物的選項
                var petSelects = cartItem.querySelectorAll('.petTypeSelect');
                var shapeSelects = cartItem.querySelectorAll('.shapeTypeSelect');
                petSelects.forEach(pet => {
                    pet.disabled = allIsSelect? true : false;
                })
                shapeSelects.forEach(pet => {
                    pet.disabled = allIsSelect ? true : false;
                })


                //step3: 找到所有single Item的價格
                var singleCartPrice = allIsSelect ? cartItem.querySelector('.discount-price-text').innerText : '0';
                //step4: 加總所有Item的價格
                cartTotalPrice += parseInt(singleCartPrice);
            })


        ///

        //step5: 加入最終total價格
        finalTotalPrice.innerText = `${cartTotalPrice}`;

        allIsSelect = !allIsSelect
    })
}

//下拉選單：點選pet類型會生體型
function SetOptionByCart() {

    //var cartItems = document.querySelectorAll('.cart-item');
    cartItems.forEach((cart, index) => {

        let shapeArray = CartsShapeList[index];

        var selectPetTypes = cart.querySelectorAll('.petTypeSelect');
        var selectShapeTypes = cart.querySelectorAll('.shapeTypeSelect');
        selectShapeTypes.forEach(x => {
            x.disabled = true;
        })

        selectPetTypes.forEach((selectPetType, comboNum) => { //comboNum是該selectPet的index
            var defaultShapeOption = selectShapeTypes[comboNum].querySelector('.defaultShape');
            var shapeValue = defaultShapeOption.value;
            var shapeText = defaultShapeOption.innerText;


            selectPetType.addEventListener('click', () => {
                selectShapeTypes[comboNum].innerHTML = '';
                selectShapeTypes[comboNum].disabled = false;
                let defaultOpt = document.createElement('option');
                defaultOpt.disabled = true;
                defaultOpt.selected = true;
                
                defaultOpt.text = shapeText;
                defaultOpt.value = shapeValue;
                selectShapeTypes[comboNum].appendChild(defaultOpt);


                let petTypeText = selectPetType.selectedOptions[0].text;
                shapeArray.forEach(com => {
                    if (com.PetType == petTypeText) {

                        let shapeOption = document.createElement('option');
                        shapeOption.value = com.ShapeType;
                        shapeOption.text = com.ShapeType;
                        selectShapeTypes[comboNum].append(shapeOption);
                    }

                })
            })
        })

    })
}


//藏寵物單價的地方
function UpdatePetInfo() {
    accordionBodies.forEach((body, count) => {
        //
        var cartPrice = body.parentNode.parentNode.parentNode.parentNode.querySelector('.cart-item-price');
        var hiddenPriceInput = body.parentNode.parentNode.parentNode.parentNode.querySelector('.hiddenPrice');//藏cartPrice，送去booking頁面

        //
        var cartId = parseInt(body.querySelector('.hidden-for-cartId').value); //1
        var cartNum = body.getAttribute('cartNum'); //0

        var petSelects = body.querySelectorAll('.petTypeSelect'); //cartId=1底下的petSels
        var shapeSelects = body.querySelectorAll('.shapeTypeSelect'); //cartId=1底下的shapeSels
        var cartDetailId = body.querySelectorAll('.hidden-for-detailId'); //很多個cartDetailId [1,2]

        var detailIdArr = []; //裝著多個cartDetailedId的陣列
        cartDetailId.forEach(detailId => {
            detailIdArr.push(detailId.value);
        })


        //取得productId
        var productId = parseInt(body.querySelector('.hidden-for-productId').value); //productId =1
        //console.log('ProductId:' + productId);

        var cartItem = body.parentNode.parentNode.parentNode.parentNode;

        var discountShowLabel = cartItem.querySelector('.discountedPrice .discount-price-text');
        //console.log(discountShowLabel)
        var discountPercent = cartItem.querySelector('.hiddenDiscount').value;
        //console.log(discountPercent)
        var discountHiddenInput = cartItem.querySelector('.hiddenDiscountedPrice'); 


        //網頁載入時候，自動依據資料庫寵物之 類型/體型，API取得價格，再放到價格標籤上
        var petOptionSelPlaces = body.querySelectorAll('.pet-selection');
        var cartDefaultPrice = 0;
        petOptionSelPlaces.forEach(async (petSel)  => {
            var petTypeDefaultValue = parseInt(petSel.querySelector('.defaultPet').value);
            petSel.querySelector('.defaultPet').setAttribute('price', petTypeDefaultValue)
            var shapeTypeDefaultValue = parseInt(petSel.querySelector('.defaultShape').value);
            petSel.querySelector('.defaultShape').setAttribute('price', shapeTypeDefaultValue)

            var defaultPrice = await GetPetUnitPrice(cartId, productId, petTypeDefaultValue, shapeTypeDefaultValue);

            cartDefaultPrice += await defaultPrice;
            //cartPrice 更新價格的最後地方
            cartPrice.innerText = await cartDefaultPrice;
            hiddenPriceInput.value = await parseInt(cartDefaultPrice);

            //設定discount價格標籤
            var discountedPrice = await parseInt(cartDefaultPrice * discountPercent);
            discountShowLabel.innerText = await discountedPrice;
            discountHiddenInput.value = await discountedPrice;


            //
        });
       
        //

        //下方是設定寵物動態點擊的價格變化

        shapeSelects.forEach((shapeSel, index) => {
            shapeSel.addEventListener('click', async (event) => {
                


                //取得相對應pet的值
                var petValue = switchPetType(petSelects[index].selectedOptions[0].text)
                //取得shape被選到的值
                var shapeValue = switchShapeType(shapeSelects[index].selectedOptions[0].text);
                var singlePrice;
                if (shapeValue == null) {
                    singlePrice = 0;
                }
                else {
                    //放進getPrice得到值
                    singlePrice = await GetPetUnitPrice(cartId, productId, petValue, shapeValue);
                }


                shapeSel.setAttribute('price', singlePrice)

                var totalPrice = await UpdateCartPrice(cartNum);
                
                cartPrice.innerText = await totalPrice;

                hiddenPriceInput.value = await parseInt(totalPrice);

                //加入discount
                

                var discountShowLabel = cartItem.querySelector('.discountedPrice .discount-price-text');
                /*console.log(discountShowLabel)*/
                var discountPercent = cartItem.querySelector('.hiddenDiscount').value;
                console.log(discountPercent)

                var discountHiddenInput = cartItem.querySelector('.hiddenDiscountedPrice'); 
                console.log(discountHiddenInput)
                var discountedPrice = await parseInt(totalPrice * discountPercent);
                discountShowLabel.innerText = await discountedPrice
                var finalCartPrice = cartItem.querySelector('.hiddenDiscountedPrice');
                finalCartPrice.value = await discountedPrice;
                discountHiddenInput.value = await discountedPrice;
                console.log(await discountHiddenInput.value)

                //


                //存入CartPetDetail資料庫
                UpdateDBPetInfos(cartId, detailIdArr[index], petValue, shapeValue);

            });
        })



    })
}


//更新cart價錢的方法
function UpdateCartPrice(cartNum) {
    var cart = cartItems[cartNum];
    var shapeSelets = cart.querySelectorAll('.shapeTypeSelect');
    var cartPrice = 0;
    shapeSelets.forEach(sel => {
        var price = sel.getAttribute('price');
        if (price == null || price == 'undefined') {
            price = 0
            cartPrice += parseInt(price);

        }
        cartPrice += parseInt(price);
    })
    return cartPrice;


}


function CheckDeleteBtnDisabled() {
    var checkCartIdArr = [];
    //step1: 所有checkbox找出來foreach找有被checked的box.value

    var checkboxs = document.querySelectorAll('.valid-list > input[type="checkbox"]');

    checkboxs.forEach(box => {
        if (box.checked) {
            //step2: 加入cartIds陣列[]
            checkCartIdArr.push(parseInt(box.value));
        }

    })
    //console.log(checkCartIdArr)
    if (checkCartIdArr.length == 0) {

    }
}

//設定單獨刪除選擇項目的方法
function deleteBtnClick() {
    var checkCartIdArr = [];
    //step1: 所有checkbox找出來foreach找有被checked的box.value

    var checkboxs = document.querySelectorAll('.valid-list > input[type="checkbox"]'); 

    checkboxs.forEach(box => {
        if (box.checked) {
            //step2: 加入cartIds陣列[]
            checkCartIdArr.push(parseInt(box.value));
        }
        
    })
    //console.log(checkCartIdArr)
    if (checkCartIdArr.length == 0) {

    }

    //step3: 陣列[]foreach執行刪除API
    checkCartIdArr.forEach(cartId  => {
        DeleteCartItem(cartId)
        //console.log(`CartId: ${cartId}，被刪除`)
        var checkbox = document.querySelector(`input[value="${cartId}"]`);
        /*console.log(checkbox)*/
        let li = checkbox.parentElement;
        cartItemUl.removeChild(li);
    })
    

    //step4: 刪除後，如果購物車內沒有商品，則顯示無商品
    var itemList = document.querySelectorAll('.valid-list');
    if (itemList.length == 0) {
        //<li class="null-item flex-wrap align-items-center justify-content-center">
        //    <div class="order-null">
        //        <img src="~/images/no order.png" alt="" class="order-null-img">
    //            <p>您的購物車空無一物</p>
    //            <a asp-route="Search" asp-route-searchinput="Recommend" class="pawsday-btn">立馬搜尋保姆</a>
        //    </div>
        //</li>
        var ul = document.querySelector('.cart-content .valid-item-list');
        console.log(ul)
        let li = document.createElement('li');
        li.classList.add('null-item', 'flex-wrap', 'align-items-center', 'justify-content-center');
        let div = document.createElement('div');
        div.classList.add('order-null');
        
        let img = document.createElement('img');
        img.classList.add('order-null-img');
        img.src = "/images/no order.png";
        console.log(img)

        let p = document.createElement('p');
        p.innerText = '您的購物車空無一物';

        let a = document.createElement('a');
        a.classList.add('pawsday-btn');
        a.innerText = '立馬搜尋保姆';
        a.setAttribute('asp-route', 'Search');
        a.setAttribute('asp-route-searchinput', 'Recommend');

        div.append(img);
        console.log(div)
        div.append(p);
        console.log(div)
        div.append(a);
        console.log(div)
        li.append(div);
        console.log(li)
        ul.append(li)
        console.log(ul)


    }



    //step5: 刪除之後，更新全選的數量
    var reloadCartList = document.querySelectorAll('.valid-list');
    var itemQuan = document.getElementById('itemQuan')
    itemQuan.innerText = `(${reloadCartList.length})`

    selectedIndexList = [] //單選的index歸零
    totalProductQuan = 0;
    productQuantity.innerText = `0 件商品合計`;
    finalTotalPrice.innerText = 0;
}

//刪除全部過期商品的方法
function deleteAllExpiredItem() {

    var expiredItemList = document.querySelector('.expired-item-list');

    //呼叫API刪除
    var items = expiredItemList.querySelectorAll('.cart-item');
    items.forEach(item => {
        var cartId = parseInt(item.querySelector('.hidden-for-cartId').value);

        DeleteCartItem(cartId);
        console.log(`CartId: ${cartId}，被刪除`)

    })

    

    //外觀部分處理
    expiredItemList.innerHTML = '';


    let li = document.createElement('li');
    li.classList.add('null-item', 'flex-wrap', 'align-items-center', 'justify-content-center');
    let div = document.createElement('div');
    div.classList.add('order-null');

    let img = document.createElement('img');
    img.classList.add('order-null-img');
    img.src = "/images/no order.png";
    

    let p = document.createElement('p');
    p.innerText = '無過期項目';

    div.append(img, p)
    li.append(div)
    expiredItemList.append(li)




}


//API 得到組合價錢
async function GetPetUnitPrice(cartId, productId, petTypeId, shapeTypeId) {
    let url = `/api/ShoppingCartWebApi/GetPetPrice?cartId=${cartId}&productId=${productId}&petType=${petTypeId}&shapeType=${shapeTypeId}`;

    const res = await fetch(url);
    //console.log(`Fetch回來： ${res}`)
    const json = await res.json();
    //console.log(`轉為JSON: ${json}`)

    return json;

}


//API 刪除所選cartItem
function DeleteCartItem(cartId) {
    var source = [];
    source.push(cartId)

    let url = `/api/ShoppingCartWebApi/Delete?cartId=${cartId}`

    fetch(url, {
        method: 'DELETE',
        body: JSON.stringify(source),
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    })
        .then(response => response)
        .then(result => {
            console.log(result.body)
        })
        .catch(ex => {
            console.log(ex);
        })

}

//API Update資料庫寵物types
function UpdateDBPetInfos(cartId, cartDetailId, petType, shapeType) { //cartId=1 cartDetailId [1,2]


    var source = [];

    var data = {
        CartId: cartId,
        CartDetailId: cartDetailId,
        PetType: petType,
        ShapeType: shapeType,
    }
    //console.log(data);
    source.push(data);


    let url = `/api/ShoppingCartWebApi/Update`;

    fetch(url, {
        method: 'PUT',
        body: JSON.stringify(source),
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    })

        .then(response => response)
        .then(result => {
            //console.log(result)
        })
        .catch(ex => {
            console.log(ex);
        })


}


//最後送出表單時候，抓取總訂單金額
function GetFinalTotalPrice() {
    var finalPrice = document.querySelector('.hidden-for-finalTotalPrice');
    finalPrice.value = finalTotalPrice.innerText;

}

function switchPetType(petType) {
    switch (petType) {
        case '狗狗':
            return 0;
        case '貓咪':
            return 1;
    }
}
function switchShapeType(shapeType) {
    switch (shapeType) {
        case '迷你型(5kg以下)':
            return 0;
        case '小型(5~10kg以下)':
            return 1;
        case '中型(10~20kg以下)':
            return 2;
        case '大型(20~40kg以下)':
            return 3;
        case '超大型(20kg以上)':
            return 4;
        default:
            return null;
    }
}

