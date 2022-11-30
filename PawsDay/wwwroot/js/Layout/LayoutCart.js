let shopping = document.querySelector(".shopping-card")
let shoppingnull = document.querySelector(".shopping-null")
let reset = document.getElementById("shopping-reset")

if (member != 0) {
    if (Cookies.get('pawsdayCarts') != null) {
        LoginCreateCart()
    }
    GetShoppingCart()
    reset.addEventListener("click", GetShoppingCart)
} else {
    NotLoginGetCart()
    reset.addEventListener("click", NotLoginGetCart)
}

//登入後
function GetShoppingCart() {
    shopping.innerHTML = ""
    let url = `/api/LayoutWebApi/GetShoppingCart?memberId=${member}`;
    let shoppingarray = []
    fetch(url)
        .then(res => res.json())
        .then(response => {
            if (response.length != 0) {
                shopping.classList.remove("d-none")
                shoppingnull.classList.add("d-none")
                shoppingarray = response
                shopping.innerHTML = ""
                shoppingarray.forEach((x, index) => {
                    let shoppingitem = document.createElement("div")
                    shoppingitem.innerHTML = `<div class="shopping-item">
					                            <div class="shopping-img">
						                            <img class="item-img" src="${x.image}" alt="">
					                            </div>
					                            <div class="shopping-content">
						                            <h2 class="shopping-title shopping-name" id="shopping-title">${x.sitterName}</h2>
						                            <p class="shopping-title">｜</p>
						                            <h2 class="shopping-title shopping-service-type" id="shopping-service-type">${x.serviceType}</h2>
						                            <p class="shopping-time">
                                                        ${x.serviceDate} ${x.serviceTime}
						                            </p>
						                            <span class="shopping-pettype">
						                            </span>
						                            <span class="card-price">TWD ${x.totalPrice}</span>
					                            </div>
				                            </div>`
                    let pettype = shoppingitem.querySelector(".shopping-pettype")
                    x.cartPets.forEach(y => {
                        let p = document.createElement("p")
                        p.innerText = `${y.petType} - ${y.shapeType}`
                        pettype.append(p)
                    })
                    shopping.append(shoppingitem)
                })
            } else {
                shopping.classList.add("d-none")
                shoppingnull.classList.remove("d-none")
            }
        })
        .catch(ex => {
            console.log(ex)
        })
}


//登入前
function NotLoginGetCart() {
    if (Cookies.get('pawsdayCarts') != null && Cookies.get('pawsdayCarts') != []) {
        let pawsdaycarts = JSON.parse(Cookies.get('pawsdayCarts'))
        let shoppingarray = []
        let url = '/api/LayoutWebApi/NotLoginGetCart'
        let datas = [];
        pawsdaycarts.forEach(x => {
            data = {
                ProductId: x.Id,
                Date: x.Day,
                Times: x.Time,
                PetTypes: x.ShapeTypes
            }
            datas.push(data)
        })
        fetch(url, {
            method: 'POST',
            body: JSON.stringify(datas),
            headers: new Headers({
                'Content-Type': 'application/json'
            })
        })
            .then(res => res.json())
            .then(response => {
                if (response.length != 0) {
                    shopping.classList.remove("d-none")
                    shoppingnull.classList.add("d-none")
                    shoppingarray = response
                    shopping.innerHTML = ""
                    shoppingarray.forEach((x, index) => {
                        let shoppingitem = document.createElement("div")
                        shoppingitem.innerHTML = `<div class="shopping-item">
					                            <div class="shopping-img">
						                            <img class="item-img" src="${x.image}" alt="">
					                            </div>
					                            <div class="shopping-content">
						                            <h2 class="shopping-title shopping-name" id="shopping-title">${x.sitterName}</h2>
						                            <p class="shopping-title">｜</p>
						                            <h2 class="shopping-title shopping-service-type" id="shopping-service-type">${x.serviceType}</h2>
						                            <p class="shopping-time">
                                                        ${x.serviceDate} ${x.serviceTime}
						                            </p>
						                            <span class="shopping-pettype">
						                            </span>
						                            <span class="card-price">TWD ${x.totalPrice}</span>
					                            </div>
				                            </div>`
                        let pettype = shoppingitem.querySelector(".shopping-pettype")
                        x.cartPets.forEach(y => {
                            let p = document.createElement("p")
                            p.innerText = `${y.petType} - ${y.shapeType}`
                            pettype.append(p)
                        })
                        shopping.append(shoppingitem)
                    })
                } else {
                    shopping.classList.add("d-none")
                    shoppingnull.classList.remove("d-none")
                }
            })
            .catch(ex => {
                console.log(ex)
            })
    }
}


function LoginCreateCart() {
    let carts = JSON.parse(Cookies.get('pawsdayCarts'))
    carts.forEach(cart => {
        data = {
            MemberId: Number(member),
            SelectedId: Number(cart.Id),
            SelectedDay: cart.Day,
            SelectedTime: cart.Time,
            SelectedCounty: cart.County,
            SelectedDistrict: cart.District,
            SelectedShapeTypes: cart.ShapeTypes,
            SelectedPrice: '0'
        }

        let url = '/api/ProductWebApi/CreateCart'

        fetch(url, {
            method: 'POST',
            body: JSON.stringify(data),
            headers: new Headers({
                'Content-Type': 'application/json'
            })
        })
            .then(res => res.json())
            .then(response => {
            })
            .catch(ex => {
                console.log(ex)
            })
    })
    GetShoppingCart()
    Cookies.remove('pawsdayCarts')
}