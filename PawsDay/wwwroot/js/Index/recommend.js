const RC = document.querySelector('.recommend-container')
const moreRC = document.querySelector('.recommend-more')
const IconRC = document.querySelector('.recommend-arrow')
const moreSecondRC = document.querySelector('.recommend-more-second')
const iconSecondRC = document.querySelector('.recommend-arrow-second')
More(moreRC, RC, IconRC, "recommend-more")
More(moreSecondRC, RC, iconSecondRC, "recommend-more-second")

function More(arrow, container, icon, classname) {
    arrow.addEventListener('click', () => {
        container.classList.toggle(classname)
        icon.classList.toggle("fa-angle-right")
        icon.classList.toggle("fa-angle-left")
    })
}

// 是否收藏
let collectProduct, isCollect, collectProductid;
let collectMemberId = MemberIdjs
let recommendCards = document.querySelectorAll(".recommend-card")
recommendCards.forEach((card,index) => {
	if (collectMemberId != 0) {
		collectProductid = recommendCards[index].querySelector(".product-id").value
		let collect = card.querySelector(".fa-heart")
		isCollect = GetCollect(Number(collectMemberId), Number(collectProductid))
		isCollect.then((result) => {
			if (result == true) {
				collect.classList.add("fa-solid")
				collect.classList.remove("fa-regular")
			}
			else {
				collect.classList.add("fa-regular")
				collect.classList.remove("fa-solid")
			}
		})
		
		collect.addEventListener("click", function (event) {
			collectProduct = card.querySelector(".product-id").value
			if (collect.classList.contains("fa-solid")) {
				collect.classList.add("fa-regular")
				collect.classList.remove("fa-solid")
				DeleteCollect()
			} else {
				collect.classList.add("fa-solid")
				collect.classList.remove("fa-regular")
				CreateCollect()
			}
		})
    }
})
async function GetCollect(memberid, productid) {
	let url = `/api/IndexWebApi/GetCollect?memberId=${memberid}&productId=${productid}`
	const res = await fetch(url);
	const json = await res.json();
	return json;
}


function CreateCollect() {
	data = {
		MemberId: collectMemberId,
		ProductId: collectProduct
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
			
		})
		.catch(ex => {
			console.log(ex)
		})
	collectTrue()
}

function DeleteCollect() {
	data = {
		MemberId: collectMemberId,
		ProductId: collectProduct
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
			
		})
		.catch(ex => {
			console.log(ex)
		})
	collectFlase()
}

// API取得是否有收藏GetCollect
function collectTrue() {
	recommendCards.forEach(otherscard => {
		let otherscollect = otherscard.querySelector(".fa-heart")
		if (otherscard.querySelector(".product-id").value == collectProduct) {
			otherscollect.classList.add("fa-solid")
			otherscollect.classList.remove("fa-regular")
		}
	})
}
function collectFlase() {
	recommendCards.forEach(otherscard => {
		let otherscollect = otherscard.querySelector(".fa-heart")
		if (otherscard.querySelector(".product-id").value == collectProduct) {
			otherscollect.classList.remove("fa-solid")
			otherscollect.classList.add("fa-regular")
		}
	})
}