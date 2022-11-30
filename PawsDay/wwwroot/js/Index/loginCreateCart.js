if (Number(MemberIdjs) != 0 && Cookies.get('pawsdayCarts') != null) {
    LoginCreateCart()
}

function LoginCreateCart() {
    let carts = JSON.parse(Cookies.get('pawsdayCarts'))
    carts.forEach(cart => {
        data = {
            MemberId: Number(MemberIdjs),
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