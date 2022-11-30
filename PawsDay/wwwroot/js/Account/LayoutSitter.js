// 宣告

// DOM


// BootStrap Modal Control object


// window.onload

getNowUrl();


// function

function getNowUrl() {
    let thisIsRouteUrl = location.pathname;

    Cookies.set('thisIsNowUrl', thisIsRouteUrl, { path: '/' });
}

