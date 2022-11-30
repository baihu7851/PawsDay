let box = document.querySelector('.container-pet-information')
let petitems = document.querySelectorAll('[petId]')
let savebtns = document.querySelectorAll('.save-btn')
let cancelbtns = document.querySelectorAll('.pet-cancel')

window.onload = function () {
    cancelbtns.forEach(cancelbtn => {
        cancelbtn.addEventListener('click', (event) => { 
            let temp = event.target.parentElement.parentElement.parentElement
            let id = temp.attributes[1].value
            Delete(temp, id)
        })
    })

    savebtns.forEach(savebtn => {
        savebtn.addEventListener('click', (event) => {
            
            let id = event.path[6].attributes[1].value
            
            Update(id)
            
        })
    })

}


//刪除
function Delete(temp,id) {

    let url = `/api/MemberPetInfoApi/Delete?petid=${id}`

    fetch(url, {
        method: 'DELETE',
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    })
        //.then(res => res) //轉型
        .then(response => {
            if (response.status == 200) {
                box.removeChild(temp);
            }
            
        })
        .catch(ex => {
            console.log(ex)
        })
}


//更新
function Update(id) {
    
    let name = document.getElementById(`petname${id}`).value
    let sex = document.getElementById(`pet-sex${id}`).value
    let type = document.getElementById(`pet-type${id}`).value
    let shape = document.getElementById(`body-type${id}`).value
    let month = document.getElementById(`pet-birth-month${id}`).value
    let year = document.getElementById(`pet-birth-year${id}`).value

    let ligation = document.getElementById(`ligation${id}`).children[0].control.checked
    let vaccine = document.getElementById(`vaccine${id}`).children[0].control.checked

    let disposition = document.querySelectorAll(`#disposition${id}`)
    //let length = document.getElementById(`disposition${id}`).children.length
    let dispositionarray = []
    console.log(disposition)
    
    disposition.forEach(d => {
        console.dir(d)
        if (d.checked)
        {
            dispositionarray.push(d.value)
        }
    })
    console.log(dispositionarray)
    
    let intro = document.getElementById(`pettext${id}`).value

    let data = {
        PetName: name,
        PetSex: sex,
        PetType: type,
        ShapeType: shape,
        BirthMonth: month,
        BirthYear: year,
        Ligation: ligation,
        Vaccine: vaccine,
        Description: intro,

        PetId: id,
        PetDispositionType: dispositionarray
    }
    console.log(data)

    let url = `/api/MemberPetInfoApi/UpdatePetInformation`

    fetch(url, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    })
        .then(res => res.json())
        .then(response => {
            let title = document.getElementById(`petTitle${id}`)
            title.innerText = `${response.petName}`
            //title.innerText = data.PetName //方法2
        })
        .catch(ex => {
            console.log(ex)
        })



}
