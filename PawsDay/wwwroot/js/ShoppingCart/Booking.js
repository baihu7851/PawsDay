//變數
let bookNameOK = false;
let bookTelOK = false;
let bookEmailOK = false;

//DOM
let invoiceEmail = document.getElementById('invoice-email');

var selectPetGroups = document.querySelectorAll('.selectpet-group'); //處理選擇預設寵物清單
var invoiceInputs = document.querySelector('.hidden-invoice');//隱藏or打開的Invoice輸入框

var checkInvoice = document.querySelector('#checkInvoice');
const allContinueBtn = document.querySelectorAll('.continue-btn');
const payBtn = document.querySelector('#payBtn');

let allItems = document.querySelectorAll('.eachItem');

const bookerNameInput = document.querySelector('.firstname-input input');
const bookerTelInput = document.querySelector('.phone-input input');
const bookerEmailInput = document.querySelector('.email-input input');

const ecpayBtn = document.getElementById('ecpayBtn')
const newebpayBtn = document.getElementById('newebpayBtn')
const blockToBtn = document.getElementById('blockToBtn')

let bookNameErrorMsg = document.querySelector('.bookerNameErrorMsg');
let bookerTelErrorMsg = document.querySelector('.bookerTelErrorMsg');
let bookerEmailErrorMsg = document.querySelector('.bookerEmailErrorMsg');

let bookInfoIsOK = false;
let petInfoAreOK = false;

let memberInfoArr = MemberInfos.toString().split(',');
let memberName = memberInfoArr[0];
let memberTel = memberInfoArr[1];
let memberEmail = memberInfoArr[2];
let memberAddress = memberInfoArr[3];



//Window.onload
window.onload = () => {

    SetPaymentType()//設定最後付款方式

    GetDefaultPet();//設定下拉選單預設會員寵物

    SetSameUserInfo();//設定同會員按鈕

    DisplayInvoiceInfo();//展開統編輸入框

    DisplayTypeValue(); //將petTypeId跟shapeId丟進hiddenInput

    GetPetCharacters();
    GetSwitchTypes();
    SetCheckMarks();

    //一開始把所有按鈕都disabled
    DisabledAllBtns();

    //一開始設定所有continueBtn點擊後value=1
    ClickContinueBtnChangeValue();

    //顯示訂購人資料驗證訊息:不得空白
    SetDefaultErrorMsg();
    //設定輸入的時候變化
    SetBookerAreaValidation(); //訂購人資料

    //迴圈寵物驗證
    SetAllPetsInfoValidation();
};

//Function

function SetPaymentType() {
    ecpayBtn.addEventListener('click', () => {
        payBtn.setAttribute('formaction', '/ShoppingCart/FromBookingToECPay')
    })

    //formaction = "/ShoppingCart/FromBookingToECPay"

    newebpayBtn.addEventListener('click', () => {
        payBtn.setAttribute('formaction', '/ShoppingCart/FromBookingToNewebPay')
    })


    blockToBtn.addEventListener('click', () => {
        payBtn.setAttribute('formaction', '/ShoppingCart/FromBookingToCoinPay')
    })


}


//前端驗證begin
function ClickContinueBtnChangeValue() {
    
    allContinueBtn.forEach(btn => {
        btn.addEventListener('click', () => {
            btn.value = '1';
            CheckCanPayBtn();
        })
    })
}


function DisabledAllBtns() {
    allContinueBtn.forEach(btn => {
        btn.disabled = true;
        btn.style.backgroundColor = 'var(--clr-bg-g)'
    })
    payBtn.disabled = true
    payBtn.style.backgroundColor = 'var(--clr-bg-g)'
}

function CheckCanPayBtn() {
    let continueBtns = document.querySelectorAll('.continue-btn');
    let valueArr = [];
    continueBtns.forEach(btn => {
        valueArr.push(btn.value)
    })
    if (valueArr.some(x => x == '0')) {

        payBtn.disabled = true;
        payBtn.style.backgroundColor = 'var(--clr-bg-g)'
    }
    else {
        payBtn.disabled = false;
        payBtn.style.backgroundColor = 'var(--clr-border)'
    }


}

function SetDefaultErrorMsg() {
    
    bookNameErrorMsg.innerText = '不得為空白';
    
    bookerTelErrorMsg.innerText = '不得為空白';
    
    bookerEmailErrorMsg.innerText = '不得為空白';
    

}

function SetBookerAreaValidation() {
    bookerNameInput.addEventListener('keyup', () => {
        let val = bookerNameInput.value;
        //console.log(val)
        let spec = /^[A-z\u4e00-\u9fa5]*$/
        if (!spec.test(val)) {
            bookNameErrorMsg.innerText = '不得輸入特殊符號 / 數字'
            bookNameOK = false;
        }
        else if (val == '') {
            bookNameErrorMsg.innerText = '不得為空白';
            bookNameOK = false;
        }
        else {
            bookNameErrorMsg.innerText = ''
            bookNameOK = true;
        }

        //檢查是否可繼續
        CheckBookInfoContinue();
        CheckCanPayBtn()
    })
    bookerTelInput.addEventListener('keyup', () => {
        let val = bookerTelInput.value;
        //console.log(val)
        let spec = /^[0-9]*$/
        if (!spec.test(val)) {
            bookerTelErrorMsg.innerText = '請輸入數字'
            bookTelOK = false;
        }
        else if (val == '') {
            bookerTelErrorMsg.innerText = '不得為空白';
            bookTelOK = false;
        }
        else if (val.length != 10) {

            bookerTelErrorMsg.innerText = '輸入長度須為10'
            bookTelOK = false;
        }
        else {
            bookerTelErrorMsg.innerText = ''
            bookTelOK = true;
        }

        //檢查是否可繼續
        CheckBookInfoContinue()
        CheckCanPayBtn()

    })
    bookerEmailInput.addEventListener('keyup', () => {
        let val = bookerEmailInput.value;
        //
        invoiceEmail.value = val;
        let spec = /^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$/
        if (!spec.test(val)) {
            bookerEmailErrorMsg.innerText = 'Email格式有誤'
            bookEmailOK = false;
        }
        else if (val == '') {
            bookerEmailErrorMsg.innerText = '不得為空白';
            bookEmailOK = false;
        }

        else {
            bookerEmailErrorMsg.innerText = ''
            bookEmailOK = true;
        }

        //檢查是否可繼續
        CheckBookInfoContinue()
        CheckCanPayBtn()
    })
}

function CheckBookInfoContinue() {
    let bookerContinueBtn = document.querySelector('.bookerBlock .continue-btn');
    if (bookNameOK && bookTelOK && bookEmailOK) {
        bookerContinueBtn.disabled = false;
        bookerContinueBtn.style.backgroundColor = 'var(--clr-border)'
    }
    else {
        bookerContinueBtn.disabled = true;
        bookerContinueBtn.style.backgroundColor = 'var(--clr-bg-g)'
        bookerContinueBtn.value = '0';
        payBtn.disabled = true;
        payBtn.style.backgroundColor = 'var(--clr-bg-g)'

    }

}

function SetAllPetsInfoValidation() {
    let allItems = document.querySelectorAll('.eachItem');
    allItems.forEach(item => {
        let continueBtn = item.querySelector('.continue-btn');
        let addressInput = item.querySelector('.addressInput');
        let addressErrorMsg = item.querySelector('.addressErrorMsg');
        addressErrorMsg.innerText = '不得為空';
        let contactNameInput = item.querySelector('.contactNameInput');
        let contactNameErrorMsg = item.querySelector('.contactNameErrorMsg');
        contactNameErrorMsg.innerText = '不得為空';
        let contactTelInput = item.querySelector('.contactTelInput');
        let contactTelErrorMsg = item.querySelector('.contactTelErrorMsg');
        contactTelErrorMsg.innerText = '不得為空';

        let addressOK = false;
        let contactNameOK = false;
        let contactTelOK = false;

        let petAllOK = false;

        //服務地址驗證begin
        addressInput.addEventListener('keyup', () => {
            let val = addressInput.value;
            let spec = /^[\u4e00-\u9fa5\0-9]*$/
            if (val == '') {

                addressErrorMsg.innerText = '不可留空'
                addressOK = false;
            }

            else if (!spec.test(val)) {

                addressErrorMsg.innerText = '請輸入中文+數字'
                addressOK = false;
            }
            else {

                addressErrorMsg.innerText = ''
                addressOK = true;
            }

            //檢查是否可繼續
            if (addressOK && contactNameOK && contactTelOK && petAllOK) {
                continueBtn.disabled = false;
                continueBtn.style.backgroundColor = 'var(--clr-border)'
                bookInfoIsOK = true;

            }
            else {
                continueBtn.disabled = true;
                continueBtn.style.backgroundColor = 'var(--clr-bg-g)'
                continueBtn.value = '0';
                payBtn.disabled = true;
                payBtn.style.backgroundColor = 'var(--clr-bg-g)'
                bookInfoIsOK = false;
            }
            CheckCanPayBtn()
        });
        //服務地址驗證end

        //聯絡姓名驗證begin
        contactNameInput.addEventListener('keyup', () => {
            let val = contactNameInput.value;
            let spec = /^[A-z\u4e00-\u9fa5]*$/
            if (val == '') {

                contactNameErrorMsg.innerText = '不可留空'
                contactNameOK = false;
            }

            else if (!spec.test(val)) {

                contactNameErrorMsg.innerText = '不得輸入特殊符號 / 數字'
                contactNameOK = false;
            }
            else {

                contactNameErrorMsg.innerText = ''
                contactNameOK = true;
            }

            //檢查是否可繼續
            if (addressOK && contactNameOK && contactTelOK && petAllOK) {
                continueBtn.disabled = false;
                continueBtn.style.backgroundColor = 'var(--clr-border)'
                bookInfoIsOK = true;

            }
            else {
                continueBtn.disabled = true;
                continueBtn.style.backgroundColor = 'var(--clr-bg-g)'
                continueBtn.value = '0';
                payBtn.disabled = true;
                payBtn.style.backgroundColor = 'var(--clr-bg-g)'
                bookInfoIsOK = false;
            }
            CheckCanPayBtn()
        });
        //聯絡姓名驗證ends

        //聯絡電話驗證begin
        contactTelInput.addEventListener('keyup', () => {
            let val = contactTelInput.value;
            let spec = /^[0-9]*$/
            if (val == '') {

                contactTelErrorMsg.innerText = '不可留空'
                contactTelOK = false;
            }

            else if (!spec.test(val)) {

                contactTelErrorMsg.innerText = '請輸入數字'
                contactTelOK = false;
            }
            else if (val.length != 10) {
                contactTelErrorMsg.innerText = '輸入長度須為10'
                contactTelOK = false;
            }
            else {

                contactTelErrorMsg.innerText = ''
                contactTelOK = true;
            }

            //檢查是否可繼續
            if (addressOK && contactNameOK && contactTelOK && petAllOK) {
                continueBtn.disabled = false;
                continueBtn.style.backgroundColor = 'var(--clr-border)'
                bookInfoIsOK = true;

            }
            else {
                continueBtn.disabled = true;
                continueBtn.style.backgroundColor = 'var(--clr-bg-g)'
                continueBtn.value = '0';
                payBtn.disabled = true;
                payBtn.style.backgroundColor = 'var(--clr-bg-g)'
                bookInfoIsOK = false;
            }
            CheckCanPayBtn()
        });
        //聯絡電話驗證ends

        ///寵物迴圈以下
        let petOKArr = []

        let petInputGroup = item.querySelectorAll('.petinfo-inputs');
        petInputGroup.forEach((pet, index) => {
            petOKArr[index] = false;
        })

        let petNameOK = false;
        let petYearOK = false;
        let petLigationOK = false;
        let petVaccineOK = false;


        petInputGroup.forEach((pet, index) => {


            let petNameInput = pet.querySelector('.petNameInput');
            let petYearInput = pet.querySelector('.petYearInput');

            let vaccineLabels = pet.querySelectorAll('.vaccine-input .radio-option label');
            let vaccineInputs = pet.querySelectorAll('.vaccine-input .radio-option input');

            let ligationLabels = pet.querySelectorAll('.ligation-input .radio-option label');
            let ligationInputs = pet.querySelectorAll('.ligation-input .radio-option input');

            
            let petNameErrorMsg = pet.querySelector('.petNameErrorMsg');
            let petYearErrorMsg = pet.querySelector('.petYearErrorMsg');
            let petVaccineErrorMsg = pet.querySelector('.petVaccineErrorMsg');
            let petLigationErrorMsg = pet.querySelector('.petLigationErrorMsg');

            petNameErrorMsg.innerText = '不得為空白';
            petYearErrorMsg.innerText = '不得為空白';
            petVaccineErrorMsg.innerText = '必須勾選"是"或"否"';
            petLigationErrorMsg.innerText = '必須勾選"是"或"否"';

            petNameInput.addEventListener('keyup', () => {
                let val = petNameInput.value;
                let spec = /^[A-z\u4e00-\u9fa5]*$/
                if (val == '') {

                    petNameErrorMsg.innerText = '不可留空'
                    petNameOK = false;
                }

                else if (!spec.test(val)) {

                    petNameErrorMsg.innerText = '不得輸入特殊符號 / 數字'
                    petNameOK = false;
                }
                else {

                    petNameErrorMsg.innerText = ''
                    petNameOK = true;
                }

                //檢查是否可繼續
                if (petNameOK && petYearOK && petLigationOK && petVaccineOK) {
                    petOKArr[index] = true;
                    //console.log(petOKArr)
                    
                    if (petOKArr.every(x => x === true)) {

                        petAllOK = true;

                    }
                    else {
                        petAllOK = false;
                    }

                }
                else {
                    petOKArr[index] = false;
                    //console.log(petOKArr)

                    petAllOK = false;
                    //console.log(petAllOK)
                }
                //檢查是否可繼續
                if (addressOK && contactNameOK && contactTelOK && petAllOK) {
                    continueBtn.disabled = false;
                    continueBtn.style.backgroundColor = 'var(--clr-border)'
                }
                else {
                    continueBtn.disabled = true;
                    continueBtn.style.backgroundColor = 'var(--clr-bg-g)'
                    payBtn.disabled = true;
                    payBtn.style.backgroundColor = 'var(--clr-bg-g)'
                    continueBtn.value = '0';
                }
                CheckCanPayBtn()

            });
            petYearInput.addEventListener('keyup', () => {
                let val = petYearInput.value;
                let spec = /^[0-9]*$/
                if (val == '') {

                    petYearErrorMsg.innerText = '不可留空'
                    petYearOK = false;
                }

                else if (!spec.test(val)) {

                    petYearErrorMsg.innerText = '請輸入數字'
                    petYearOK = false;
                }
                else if (val.length != 4) {
                    petYearErrorMsg.innerText = '輸入長度須為4'
                    petYearOK = false;
                }
                else {

                    petYearErrorMsg.innerText = ''
                    petYearOK = true;
                }

                //檢查是否可繼續
                if (petNameOK && petYearOK && petLigationOK && petVaccineOK) {
                    petOKArr[index] = true;
                    //console.log(petOKArr)

                    
                    if (petOKArr.every(x => x === true)) {

                        petAllOK = true;

                    }
                    else {
                        petAllOK = false;
                    }



                }
                else {
                    petOKArr[index] = false;
                    //console.log(petOKArr)

                    petAllOK = false;
                    //console.log(petAllOK)
                }


                //檢查是否可繼續
                if (addressOK && contactNameOK && contactTelOK && petAllOK) {
                    continueBtn.disabled = false;
                    continueBtn.style.backgroundColor = 'var(--clr-border)';
                }
                else {
                    continueBtn.disabled = true;
                    continueBtn.style.backgroundColor = 'var(--clr-bg-g)'

                    continueBtn.value = '0';
                    payBtn.disabled = true;
                    payBtn.style.backgroundColor = 'var(--clr-bg-g)'
                }
                CheckCanPayBtn()

            });

            vaccineLabels.forEach(label => {
                label.addEventListener('click', () => {
                    petVaccineOK = true;
                    petVaccineErrorMsg.innerText = '';
                    //檢查是否可繼續
                    if (petNameOK && petYearOK && petLigationOK && petVaccineOK) {
                        petOKArr[index] = true;
                        //console.log(petOKArr)

                        
                        if (petOKArr.every(x => x === true)) {

                            petAllOK = true;

                        }
                        else {
                            petAllOK = false;
                        }


                    }
                    else {
                        petOKArr[index] = false;
                        //console.log(petOKArr)

                        petAllOK = false;
                        //console.log(petAllOK)
                    }


                    //檢查是否可繼續
                    if (addressOK && contactNameOK && contactTelOK && petAllOK) {
                        continueBtn.disabled = false;
                        continueBtn.style.backgroundColor = 'var(--clr-border)';
                    }
                    else {
                        continueBtn.disabled = true;
                        continueBtn.style.backgroundColor = 'var(--clr-bg-g)'

                        continueBtn.value = '0';
                        payBtn.disabled = true;
                        payBtn.style.backgroundColor = 'var(--clr-bg-g)'
                    }
                    CheckCanPayBtn()
                });
            })
            vaccineInputs.forEach(input => {
                input.addEventListener('click', () => {
                    petVaccineErrorMsg.innerText = '';
                    petVaccineOK = true;

                    //檢查是否可繼續
                    if (petNameOK && petYearOK && petLigationOK && petVaccineOK) {
                        petOKArr[index] = true;
                        //console.log(petOKArr)

                        if (petOKArr.every(x => x === true)) {

                            petAllOK = true;

                        }
                        else {
                            petAllOK = false;
                        }
                    }
                    else {
                        petOKArr[index] = false;
                        //console.log(petOKArr)

                        petAllOK = false;
                        //console.log(petAllOK)
                    }


                    //檢查是否可繼續
                    if (addressOK && contactNameOK && contactTelOK && petAllOK) {
                        continueBtn.disabled = false;
                        continueBtn.style.backgroundColor = 'var(--clr-border)';
                    }
                    else {
                        continueBtn.disabled = true;
                        continueBtn.style.backgroundColor = 'var(--clr-bg-g)'

                        continueBtn.value = '0';
                        payBtn.disabled = true;
                        payBtn.style.backgroundColor = 'var(--clr-bg-g)'
                    }
                    CheckCanPayBtn()

                })



            })

            ligationLabels.forEach(label => {
                label.addEventListener('click', () => {
                    petLigationOK = true;
                    petLigationErrorMsg.innerText = '';
                    //檢查是否可繼續
                    if (petNameOK && petYearOK && petLigationOK && petVaccineOK) {
                        petOKArr[index] = true;
                        //console.log(petOKArr)

                        if (petOKArr.every(x => x === true)) {

                            petAllOK = true;

                        }
                        else {
                            petAllOK = false;
                        }
                    }
                    else {
                        petOKArr[index] = false;
                        //console.log(petOKArr)

                        petAllOK = false;
                        //console.log(petAllOK)
                    }


                    //檢查是否可繼續
                    if (addressOK && contactNameOK && contactTelOK && petAllOK) {
                        continueBtn.disabled = false;
                        continueBtn.style.backgroundColor = 'var(--clr-border)';
                    }
                    else {
                        continueBtn.disabled = true;
                        continueBtn.style.backgroundColor = 'var(--clr-bg-g)'

                        continueBtn.value = '0';
                        payBtn.disabled = true;
                        payBtn.style.backgroundColor = 'var(--clr-bg-g)'
                    }
                    CheckCanPayBtn()
                });
            })
            ligationInputs.forEach(input => {
                input.addEventListener('click', () => {
                    petLigationErrorMsg.innerText = '';
                    petLigationOK = true;

                    //檢查是否可繼續
                    if (petNameOK && petYearOK && petLigationOK && petVaccineOK) {
                        petOKArr[index] = true;
                        //console.log(petOKArr)

                        if (petOKArr.every(x => x === true)) {

                            petAllOK = true;

                        }
                        else {
                            petAllOK = false;
                        }
                    }
                    else {
                        petOKArr[index] = false;
                        //console.log(petOKArr)

                        petAllOK = false;
                        //console.log(petAllOK)
                    }


                    //檢查是否可繼續
                    if (addressOK && contactNameOK && contactTelOK && petAllOK) {
                        continueBtn.disabled = false;
                        continueBtn.style.backgroundColor = 'var(--clr-border)';
                    }
                    else {
                        continueBtn.disabled = true;
                        continueBtn.style.backgroundColor = 'var(--clr-bg-g)'

                        continueBtn.value = '0';
                        payBtn.disabled = true;
                        payBtn.style.backgroundColor = 'var(--clr-bg-g)'
                    }
                    CheckCanPayBtn()

                })

            })

        })

        ///設定同會員

        //服務地址
        let sameServiceAddress = item.querySelector('.sameuser-service-address'); //服務地址的同會員按鈕
        let sameServiceAddressCheckbox = sameServiceAddress.querySelector('input'); //服務地址的checkbox
        sameServiceAddress.addEventListener('click', () => {

            if (sameServiceAddressCheckbox.checked) {
                //sameServiceAddressCheckbox.checked = true;

                addressInput.value = memberAddress;
                

                //以下要修改
                addressErrorMsg.innerText = ''
                addressOK = true;

            }
            else {
                //sameServiceAddressCheckbox.checked = false;
                addressInput.value = '';
                

                //以下要修改
                addressErrorMsg.innerText = '不可留空'
                addressOK = false;

            }

            //檢查是否可繼續
            if (addressOK && contactNameOK && contactTelOK && petAllOK) {
                continueBtn.disabled = false;
                continueBtn.style.backgroundColor = 'var(--clr-border)'
                bookInfoIsOK = true;

            }
            else {
                continueBtn.disabled = true;
                continueBtn.style.backgroundColor = 'var(--clr-bg-g)'
                continueBtn.value = '0';
                payBtn.disabled = true;
                payBtn.style.backgroundColor = 'var(--clr-bg-g)'
                bookInfoIsOK = false;
            }
            CheckCanPayBtn()
        });

        //聯絡人資訊
        let sameUserContact = item.querySelector('#contactUser'); //聯繫人資訊的同會員按鈕
        let sameContactCheckbox = sameUserContact.querySelector('input');//聯繫人資訊的checkbox
        sameUserContact.addEventListener('click', () => {
            if (sameContactCheckbox.checked) {
                //sameContactCheckbox.checked = true;

                contactNameInput.value = memberName;
                

                contactTelInput.value = memberTel;
                



                //以下要修改
                contactNameErrorMsg.innerText = ''
                contactNameOK = true;
                contactTelErrorMsg.innerText = ''
                contactTelOK = true;


            }
            else {
                //sameContactCheckbox.checked = false;

                contactNameInput.value = '';
                

                contactTelInput.value = '';
                
               

                //以下要修改
                contactNameErrorMsg.innerText = '不可留空'
                contactNameOK = false;
                contactTelErrorMsg.innerText = '不可留空'
                contactTelOK = false;

            }

            //檢查是否可繼續
            if (addressOK && contactNameOK && contactTelOK && petAllOK) {
                continueBtn.disabled = false;
                continueBtn.style.backgroundColor = 'var(--clr-border)'
                bookInfoIsOK = true;

            }
            else {
                continueBtn.disabled = true;
                continueBtn.style.backgroundColor = 'var(--clr-bg-g)'
                continueBtn.value = '0';
                payBtn.disabled = true;
                payBtn.style.backgroundColor = 'var(--clr-bg-g)'
                bookInfoIsOK = false;
            }
            CheckCanPayBtn()



        });

        //寵物資訊
        let PetsInfoArea = item.querySelectorAll('.petinfo-inputs');
        PetsInfoArea.forEach((onePet,index) => {
            //每一隻寵物的資訊填寫區
            let defaultPetDropDown = onePet.querySelector('.dropdown-menu');
            let defaultPetNames = defaultPetDropDown.querySelectorAll('.pet-item');
            let noDefaultOption = defaultPetDropDown.querySelector('.default-item');

            let petNameInput = onePet.querySelector('.petNameInput');
            let petSexSelect = onePet.querySelector('.petSexSelect');
            let petYearInput = onePet.querySelector('.petYearInput');
            let petMonthInput = onePet.querySelector('.petMonthInput');
            let ligationRadioSelect = onePet.querySelector('.ligation-radio-select');
            let vaccineRadioSelect = onePet.querySelector('.vaccine-radio-select');

            let petNameErrorMsg = onePet.querySelector('.petNameErrorMsg');
            let petYearErrorMsg = onePet.querySelector('.petYearErrorMsg');
            let petVaccineErrorMsg = onePet.querySelector('.petVaccineErrorMsg');
            let petLigationErrorMsg = onePet.querySelector('.petLigationErrorMsg');

            noDefaultOption.addEventListener('click', () => {
                petNameInput.value = '';
                petSexSelect.querySelector(`option[value="true"]`).selected = true;  //
                petYearInput.value = ''; //
                petMonthInput.value = ''; //
                ligationRadioSelect.querySelectorAll(`input`).forEach(input => {
                    input.checked = false;
                })
                vaccineRadioSelect.querySelectorAll(`input`).forEach(input => {
                    input.checked = false;
                })

                petNameErrorMsg.innerText = '不可留空'
                petNameOK = false;
                petYearErrorMsg.innerText = '不可留空'
                petYearOK = false;
                petVaccineErrorMsg.innerText = '不可留空'
                petVaccineOK = false;
                petLigationErrorMsg.innerText = '不可留空'
                petLigationOK = false;

                petOKArr[index] = false;
                

                petAllOK = false;
                continueBtn.disabled = true;
                continueBtn.style.backgroundColor = 'var(--clr-bg-g)'

                continueBtn.value = '0';
                payBtn.disabled = true;
                payBtn.style.backgroundColor = 'var(--clr-bg-g)'
                CheckCanPayBtn()


            });

            defaultPetNames.forEach((petName, count) => {
                petName.addEventListener('click', () => {
                    


                    let defaultPetName = MemberPets[count].PetName;
                    console.log(`petName: ${defaultPetName}`)
                    petNameInput.value = defaultPetName; //

                    let defaultPetSex = MemberPets[count].PetSex;
                    console.log(`sex: ${defaultPetSex}`);
                    petSexSelect.querySelector(`option[value="${defaultPetSex}"]`).selected = true;  //
                    

                    let defaultPetYear = MemberPets[count].BirthYear;
                    console.log(`petYear: ${defaultPetYear}`);
                    petYearInput.value = defaultPetYear; //

                    let defaultPetMonth = MemberPets[count].BirthMonth;
                    console.log(`petMonth: ${defaultPetMonth}`);
                    petMonthInput.value = defaultPetMonth; //

                    let defaultPetLigation = MemberPets[count].Ligation;
                    console.log(`petLigation: ${defaultPetLigation}`)
                    ligationRadioSelect.querySelector(`input[value="${defaultPetLigation}"]`).checked = true;

                    let defaultPetVaccine = MemberPets[count].Vaccine;
                    console.log(`petVaccine: ${defaultPetVaccine}`)
                    vaccineRadioSelect.querySelector(`input[value="${defaultPetVaccine}"]`).checked = true;

                    //前端驗證
                    petNameErrorMsg.innerText = ''
                    petNameOK = true;
                    petYearErrorMsg.innerText = ''
                    petYearOK = true;
                    petVaccineErrorMsg.innerText = '';
                    petVaccineOK = true;
                    petLigationErrorMsg.innerText = '';
                    petLigationOK = true;

                    //檢查是否可繼續
                    if (petNameOK && petYearOK && petLigationOK && petVaccineOK) {
                        petOKArr[index] = true;
                        //console.log(petOKArr)

                        if (petOKArr.every(x => x === true)) {

                            petAllOK = true;

                        }
                        else {
                            petAllOK = false;
                        }
                    }
                    else {
                        petOKArr[index] = false;
                        //console.log(petOKArr)

                        petAllOK = false;
                        //console.log(petAllOK)
                    }


                    //檢查是否可繼續
                    if (addressOK && contactNameOK && contactTelOK && petAllOK) {
                        continueBtn.disabled = false;
                        continueBtn.style.backgroundColor = 'var(--clr-border)';
                    }
                    else {
                        continueBtn.disabled = true;
                        continueBtn.style.backgroundColor = 'var(--clr-bg-g)'

                        continueBtn.value = '0';
                        payBtn.disabled = true;
                        payBtn.style.backgroundColor = 'var(--clr-bg-g)'
                    }
                    CheckCanPayBtn()



                });



            });


        })


    })
}

//前端驗證end

//預設會員訂購人資料
function SetSameUserInfo() {

    //「訂購同會員」

        var sameUserBooker = document.querySelector('#sameuser-booker'); //按下的區塊
        

        var bookerInputs = document.querySelectorAll('.parent-info > .inputarea > input'); //要放入的input集合
        var checkBoxSameBooker = document.querySelector('#bookerUser');
        sameUserBooker.addEventListener('change', () => {
            if (checkBoxSameBooker.checked) {
                /*checkBoxSameBooker.checked = true;*/


                bookerNameInput.value = memberName;
                
                bookerTelInput.value = memberTel;
                
                bookerEmailInput.value = memberEmail;
                
                

                invoiceEmail.value = memberEmail;

                
                bookNameErrorMsg.innerText = ''
                bookNameOK = true;
                bookerTelErrorMsg.innerText = ''
                bookTelOK = true;
                bookerEmailErrorMsg.innerText = ''
                bookEmailOK = true;

                //檢查是否可繼續
                CheckBookInfoContinue()

            }
            else {
                //checkBoxSameBooker.checked = false;
                bookerInputs.forEach((input, index) => {
                    input.value = '';
                    
                    input.disabled = false;

                })

                invoiceEmail.value = ''

                bookNameErrorMsg.innerText = '不得為空白';
                bookNameOK = false;

                bookerTelErrorMsg.innerText = '不得為空白';
                bookTelOK = false;
                bookerEmailErrorMsg.innerText = '不得為空白';
                bookEmailOK = false;
                //檢查是否可繼續
                CheckBookInfoContinue()
            }
        });


   

     
     
    
    
}

//預設寵物選項的下拉選單
function GetDefaultPet() {
    selectPetGroups.forEach(group => {
        var btn = group.querySelector('button');
        var pets = group.querySelectorAll('.dropdown-item');
        pets.forEach(pet => {
            pet.addEventListener('click', () => {
                btn.innerText = pet.innerText;
            });
        })
    })
}

//收據若選擇統編，觸發兩個input填寫
function DisplayInvoiceInfo() {
    invoiceInputs.style.display = 'none';
    checkInvoice.addEventListener('click', () => {
        //console.log(checkInvoice.checked)
        if (!checkInvoice.checked) {
            invoiceInputs.style.display = 'none';
        }
        else {
            invoiceInputs.style.display = 'block';

        }
    });
}

function DisplayTypeValue() {
    var targetCartList = document.querySelector('.overview-CartItemList');
    var cartItems = targetCartList.querySelectorAll('.payment-productdetail');

    cartItems.forEach(item => {

        var petSelectList = item.querySelectorAll('.selected-petType');
        petSelectList.forEach(eachPet => {
            var petType = eachPet.querySelector('.petTypeSel').innerText;
            var shapeType = eachPet.querySelector('.shapeTypeSel').innerText;
            var petTypeId = switchPetType(petType);
            var shapeTypeId = switchShapeType(shapeType);

            let hiddenPetTypeInput = eachPet.querySelector('.hidden-for-PetTypeId');
            hiddenPetTypeInput.value = petTypeId;
            
            
            let hiddenShapeTypeInput = eachPet.querySelector('.hidden-for-ShapeTypeId');
            hiddenShapeTypeInput.value = shapeTypeId;
           
        })
    })
}


//設定寵物個性each checkbox checked=true時候，會加入寵物性格
function GetPetCharacters() {
    var petInputsList = document.querySelectorAll('.petinfo-inputs'); //要抓取寵物個性用的
    var hiddenDescriptionInputs = document.querySelectorAll('.pet-characters')

    petInputsList.forEach((pet, count) => {
        if (hiddenDescriptionInputs[count].value == '') {
            var characterArr = []
        }
        else {
            var characterArr = JSON.parse(hiddenDescriptionInputs[count].value);
        }
        
        var characterInput = pet.querySelector('.petcharacter-input .checkbox-option');
        var checkboxs = characterInput.querySelectorAll('input');
        checkboxs.forEach(box => {
            box.addEventListener('click', () => {
                if (box.checked) {
                    characterArr.push(box.value)
                }
                else {
                    var index = characterArr.indexOf(box.value)
                    characterArr.splice(index, 1);
                }

                var characterJson = JSON.stringify(characterArr)
                hiddenDescriptionInputs[count].value = characterJson;

                



            })

        })

    })

}

//將字串的寵物 類型/體型轉換成int  存進hidden的Input，再以Model方式傳出給下個IAction
function GetSwitchTypes() {
    var cartItems = document.querySelectorAll('.accordion-item .cart-item');
    cartItems.forEach(item => {

        var petSel = item.querySelector('.pet-selection');
        var lists = petSel.querySelectorAll('.petInfoList');
        //console.log(lists)
        lists.forEach(li => {
            var petType = li.querySelector('.hidden-eachPetType').value;
            var shapeType = li.querySelector('.hidden-eachShapeType').value;
            var petTypeId = switchPetType(petType);
            var shapeTypeId = switchShapeType(shapeType);
            //console.log(petTypeId)
            //console.log(shapeTypeId)
            var hiddenPetTypeId = li.querySelector('.hidden-eachPetTypeId');
            hiddenPetTypeId.value = petTypeId;
            var hiddenShapeTypeId = li.querySelector('.hidden-eachShapeTypeId');
            hiddenShapeTypeId.value = shapeTypeId;

        })


    })


}

function SetCheckMarks() {
    var items = document.querySelectorAll('.marksItem');
    items.forEach(item => {
        var btn = item.querySelector('.continue-btn');
        //console.log(btn)
        var mark = item.querySelector('.data-checked');
        //console.log(mark)
        //mark.style.display = 'none';
        btn.addEventListener('click', () => {
            //console.log('123')
            mark.style.display = 'block';
        });

    })

    
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
    }
}