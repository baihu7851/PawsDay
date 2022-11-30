let countySelect = document.getElementById('county');
let districtSelect = document.getElementById('district');

countySelect.addEventListener("change", CountyChange);
function CountyChange(event) {
    let countyText = countySelect.selectedOptions[0].text;
    districtSelect.disabled = false;
    districtSelect.innerHTML = '';
    let district = document.createElement('option');
    district.text = '-- 區 --';
    district.setAttribute('selected', '')
    district.value ="districtAll"
    districtSelect.add(district, null);

    let countyArray = filterAreajs.filter(item => item.County == countyText);
    countyArray.forEach((item, index) => {
        let district = document.createElement('option');
        district.value = item.District;
        district.text = item.District;
        districtSelect.add(district);
    });
}



// 是否收藏
let productId;
let memberId = MemberIdjs
let cards = document.querySelectorAll(".commodity-card")
cards.forEach(card => {
	if (memberId != 0) {
		let collect = card.querySelector(".fa-heart")
		collect.addEventListener("click", function (event) {
			productId = card.querySelector(".product-id").value
			if (collect.classList.contains("fa-solid")) {
				collect.classList.add("fa-regular")
				collect.classList.remove("fa-solid")
				SearchDeleteCollect()
			} else {
				collect.classList.add("fa-solid")
				collect.classList.remove("fa-regular")
				SearchCreateCollect()
			}
		})
    }
})

function SearchCreateCollect() {
	data = {
		MemberId: memberId,
		ProductId: productId
	}

	let url = '/api/ProductWebApi/CreateCollect'

	fetch(url, {
		method: 'POST',
		body: JSON.stringify(data),
		headers: new Headers({
			'Content-Type': 'application/json'
		})
	})
		.then(res => res.json())
		.then(response => {
			console.log(response)
		})
		.catch(ex => {
			console.log(ex)
		})
}

function SearchDeleteCollect() {
	data = {
		MemberId: memberId,
		ProductId: productId
	}

	let url = '/api/ProductWebApi/DeleteCollect'

	fetch(url, {
		method: 'DELETE',
		body: JSON.stringify(data),
		headers: new Headers({
			'Content-Type': 'application/json'
		})
	})
		.then(res => res.json())
		.then(response => {
			console.log(response)
		})
		.catch(ex => {
			console.log(ex)
		})
}

//篩選顯示
let filterSelected = filtersjs
getCountyValue(countySelect, filtersjs[0], districtSelect, filtersjs[1])
function getCountyValue(countyFilter, countySelected, districtFilter, districtSelected) {
	countyFilter.value = countySelected
	CountyChange(event)
	if (countyFilter.value != "countyAll") {
		districtFilter.value = districtSelected
	}
}

var dayRadio = document.getElementsByName("day");
getRadioValue(dayRadio, filtersjs[2])
var timeRadio = document.getElementsByName("time");
getRadioValue(timeRadio, filtersjs[3])
var serviceRadio = document.getElementsByName("service");
getRadioValue(serviceRadio, filtersjs[4])
var petRadio = document.getElementsByName("pet");
getRadioValue(petRadio, filtersjs[5])

function getRadioValue(filter, selected) {
	filter.forEach(x => {
		if (x.value == selected) {
			x.checked = true
        }
    })
}


//排序顯示
let sort = document.querySelectorAll(".sort-btn")
switch (sortjs) {
	case "Popular":
		sort[1].classList.add("sort-this");
		break;
	case "Evaluation":
		sort[2].classList.add("sort-this");
		break;
	case "PriceHigh":
		sort[3].classList.add("sort-this");
		break;
	case "PriceLow":
		sort[4].classList.add("sort-this");
		break;
	default:
		sort[0].classList.add("sort-this");
		break;
} 