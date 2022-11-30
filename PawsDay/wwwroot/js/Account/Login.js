// 宣告

// DOM


// window.onload
//window.onload = function () {



const loginRegisterModel = document.querySelector('#login-register');
const loginModel = document.querySelector('#login');
const signUpModel = document.querySelector('#sign-up');
const forgotModel = document.querySelector('#forgot');
const accountMessageModel = document.querySelector('#account-message');
const allModel = document.querySelectorAll('.modal');

loginRegisterModel.addEventListener('hide.bs.modal', function (event) {

    isModalClose(allModel)
    allModel.forEach(x => console.log(x.classList.contains("show")))

})
loginModel.addEventListener('hidden.bs.modal', function (event) {
    isModalClose(allModel)
})
signUpModel.addEventListener('hidden.bs.modal', function (event) {
    isModalClose(allModel)
})
forgotModel.addEventListener('hidden.bs.modal', function (event) {
    isModalClose(allModel)
})
accountMessageModel.addEventListener('hidden.bs.modal', function (event) {
    isModalClose(allModel)
})

// function

function isModalClose(allModel) {


    let boolArray = []
    allModel.forEach((x, index) => {

        let a = x.classList.contains("show");
        boolArray.push(a);
        console.log(a)
    })
    let isClose = boolArray.every(x => x == false)
    console.log(isClose)
    if (isClose) {
        window.location.href = location.origin
    }
    console.log(boolArray)

}






//if (isLoginPop) {
//	let loginRegister = new bootstrap.Modal(document.getElementById('login-register'), {
//		keyboard: false
//	});
//	loginRegister.show();
//}
//if (isLoginEmailPop) {
//	let loginRegister = new bootstrap.Modal(document.getElementById('login'), {
//		keyboard: false
//	});
//	loginRegister.show();
//}
//if (isForgotPop) {
//	let loginRegister = new bootstrap.Modal(document.getElementById('forgot'), {
//		keyboard: false
//	});
//	loginRegister.show();
//}
//if (isSignUpPop) {
//	let loginRegister = new bootstrap.Modal(document.getElementById('sign-up'), {
//		keyboard: false
//	});
//	loginRegister.show();
//}




//if (isLoginPop == false) {
//	let loginRegister = new bootstrap.Modal(document.getElementById('login-register'), {
//		keyboard: false
//	});
//	let login = new bootstrap.Modal(document.getElementById('login'), {
//		keyboard: false
//	});
//	let signUp = new bootstrap.Modal(document.getElementById('sign-up'), {
//		keyboard: false
//	});
//	let forgot = new bootstrap.Modal(document.getElementById('forgot'), {
//		keyboard: false
//	});
//	loginRegister.hide();
//	login.hide();
//	signUp.hide();
//	forgot.hide();
//}









