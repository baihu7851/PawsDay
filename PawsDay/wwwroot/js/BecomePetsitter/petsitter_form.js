//Modal
const leaveRemind = document.querySelector("#leave-remind")
const imgExceed = document.querySelector("#img-exceed")

//步驟區域
const step1 = document.querySelector("#petsitter-step1")
const step2 = document.querySelector("#petsitter-step2")
const step3 = document.querySelector("#petsitter-step3")

//步驟按鈕
const step1Next = step1.querySelector(".next-btn")
const step2Next = step2.querySelector(".next-btn")
const step3Next = step3.querySelector(".next-btn")
const step2Previous = step2.querySelector(".previous-btn")
const step3Previous = step3.querySelector(".previous-btn")

//檔案上傳
const idcardFrontInput = document.querySelector("#idcard-front")
const idcardBackInput = document.querySelector("#idcard-back")
const licenseInput = document.querySelector("#license")

//檔案預覽區域
const idcardFrontPreview = document.querySelector("#preview-idcard-front")
const idcardBackPreview = document.querySelector("#preview-idcard-back")
const licensePreview = document.querySelector("#preview-license")

//題目區域
const tempQuiz = document.querySelector("#petsitter-quiz")
const tempAptitude = document.querySelector("#aptitude-test")
const tempAnswer = document.querySelector("#short-answer")

//必填區域
const step1Required = document.querySelectorAll("#petsitter-step1 [required]")

//契約勾選區域
const readCheck = document.querySelector("input#read")
const agreeCheck = document.querySelector("input#agree")

window.onload = function () {
    //彈出提示
    new bootstrap.Modal(leaveRemind).show()

    //步驟控制
    step1Next.onclick = function () {
        stepControl(step2, step1)
        GoTop()
        //step2驗證
        step2Check()
        GetImageUrl(idcardFrontInput, "IdCardFront")
        GetImageUrl(idcardBackInput, "IdCardBack")
        GetImageUrl(licenseInput, "License")
    }
    step2Next.onclick = function () {
        stepControl(step3, step2)
        GoTop()
    }
    step2Previous.onclick = function () {
        stepControl(step1, step2)
        GoTop()
    }
    step3Previous.onclick = function () {
        stepControl(step2, step3)
        GoTop()
    }

    //合適度測驗資料取得及渲染
    GetTestData()

    //合約確認
    contractCheck()

    //step1驗證
    step1Check()

    //檔案預覽
    previewInput(idcardFrontInput, idcardFrontPreview, 1)
    previewInput(idcardBackInput, idcardBackPreview, 1)
    previewInput(licenseInput, licensePreview, 5)
}

//步驟控制function
function stepControl(addRegion, removeRegion) {
    addRegion.classList.remove("d-none")
    removeRegion.classList.add("d-none")
}

//step1 基本資料驗證function
function step1Check() {
    step1Next.disabled = true
    let reg = /^[A-Za-z]{1}[1-2]{1}[0-9]{8}$/
    let idNumber = document.querySelector("#id-number")

    step1Required.forEach((e) => {
        e.addEventListener("change", () => {
            let required1Count = 0

            step1Required.forEach((e) => {
                if (e.value == "") {
                    e.previousElementSibling.querySelector("span").innerText = "*此題必填"
                    e.classList.add("border", "border-danger", "border-2")
                }
                else {
                    required1Count++
                    if (e == idNumber && !reg.test(idNumber.value)) {
                        e.previousElementSibling.querySelector("span").innerText = "*請輸入正確的身份證字號"
                        e.classList.add("border", "border-danger", "border-2")
                    }
                    else {
                        e.previousElementSibling.querySelector("span").innerText = "*"
                        e.classList.remove("border", "border-danger", "border-2")
                    }
                }
            })
            if (required1Count == step1Required.length && reg.test(idNumber.value)) {
                step1Next.disabled = false
            }
        })
    })
}

//step2 測驗必填驗證function
function step2Check() {
    step2Next.disabled = true
    let step2Required = step2.querySelectorAll(".question")
    let step2Input = step2.querySelectorAll("input")
    let step2Text = step2.querySelectorAll('[type = "text"]')
    //let step2InputRadio = step2.querySelectorAll('[type = "radio"]')
    let step2Choice = step2.querySelectorAll('.choice .question')

    step2Input.forEach(i => {
        i.addEventListener("change", () => {
            let required2Count = 0

            step2Text.forEach(t => {
                if (t.value != "") {
                    required2Count++
                    t.parentNode.querySelector("span").innerText = "*"
                    t.parentNode.classList.remove("border", "border-danger", "border-2")
                }
                else {
                    t.parentNode.querySelector("span").innerText = "*此題必填"
                    t.parentNode.classList.add("border", "border-danger", "border-2")
                }
            })

            step2Choice.forEach(c => {
                let inputs = c.querySelectorAll('input')
                let isFilled = false
                inputs.forEach(i => {
                    if (i.checked) {
                        isFilled = true
                        required2Count++
                    }

                    if (isFilled) {
                        c.querySelector("span").innerText = "*"
                        c.classList.remove("border", "border-danger", "border-2")
                    }
                    else {
                        c.querySelector("span").innerText = "*此題必填"
                        c.classList.add("border", "border-danger", "border-2")
                    }
                })
            })

            if (required2Count == step2Required.length) {
                step2Next.disabled = false
            }
        })
    })
}

//step3 契約確認function
function contractCheck() {
    step3Next.disabled = true

    readCheck.addEventListener("change", () => {
        if (readCheck.checked == true && agreeCheck.checked == true) {
            step3Next.disabled = false
        }
    })
    agreeCheck.addEventListener("change", () => {
        if (readCheck.checked == true && agreeCheck.checked == true) {
            step3Next.disabled = false
        }
    })
}

//檔案預覽function
function previewInput(input, previewArea, limit) {
    input.addEventListener("change", function () {
        previewArea.innerHTML = ""
        if (input.files.length <= limit) {
            for (let i = 0; i < input.files.length; i++) {
                let image = document.createElement("img")
                let file = input.files[i]
                let reader = new FileReader()
                reader.onload = function (event) {
                    image.src = event.target.result
                }
                previewArea.append(image)
                reader.readAsDataURL(file)
            }
        } else {
            var noImage = document.createElement("i")
            noImage.classList.add("fa-solid", "fa-image")
            previewArea.append(noImage)
            //彈出錯誤提示
            new bootstrap.Modal(imgExceed).show()
        }
    })
}

//題目讀取function
function GetTestData() {
    let url = "/api/PetsitterTestWebApi"
    fetch(url, {
        method: "Get",
    })
        .then((res) => {
            return res.json()
        })
        .then((res) => {
            initTest(res)
        })
        .catch()
}

//題目初始化function
function initTest(data) {
    let dataQuiz = data.find((d) => d.TestType == "Quiz").Content
    let dataAptitude = data.find((d) => d.TestType == "Aptitude").Content
    let dataAnswer = data.find((d) => d.TestType == "Answers").Content
    let typeQuiz = "Quiz"
    let typeAptitude = "Aptitude"

    dataQuiz.forEach((q, index) => {
        document.querySelector("section#quiz").append(createQuestion(tempQuiz, q, index, typeQuiz))
    })

    dataAptitude.forEach((q, index) => {
        document
            .querySelector("section#aptitude")
            .append(createQuestion(tempAptitude, q, index, typeAptitude))
    })

    dataAnswer.forEach((q) => {
        document
            .querySelector("section.short-answer")
            .append(createShortAnswer(tempAnswer, q))
    })
}

//選擇題-題目渲染function
function createQuestion(temp, item, index, type) {
    let clone = temp.content.cloneNode(true)
    let requiredMark = '<span class="text-danger">*</span>'
    let options = clone.querySelectorAll(".option")

    clone.querySelector(".question p").innerHTML =
        item.QuestionText + requiredMark
    createOption(options, item, index, type)

    return clone
}

//選擇題-選項渲染function
function createOption(area, item, index, type) {
    for (let i = 0; i < area.length; i++) {
        area[
            i
        ].innerHTML = `<input type="radio" name="${type}[${index}]" value="${item.Answer[i].Value}" id="${item.Answer[i].Id}" required/><label for="${item.Answer[i].Id}">${item.Answer[i].Text}</label>`
    }
}

//簡答題渲染function
function createShortAnswer(temp, item) {
    let clone = temp.content.cloneNode(true)
    let requiredMark = '<span class="text-danger">*</span>'

    let label = clone.querySelector("label")
    label.innerHTML = item.QuestionText + requiredMark
    label.setAttribute("for", item.QuestionId)

    let input = clone.querySelector("input[type = text]")
    input.id = item.QuestionId
    input.name = "Answer"
    input.value = ""
    input.setAttribute("placeholder", "請在此處作答")
    input.required = true

    //使用hidden回傳QuestionId
    let hiddenInput = document.createElement("input")
    hiddenInput.type = "hidden"
    hiddenInput.name = "QuestionId"
    hiddenInput.value = item.QuestionId.replace("Q", "")
    clone.append(hiddenInput)

    return clone
}

//上傳Cloidinary取得連結function
function GetImageUrl(inputArea, inputName) {
    let formData = new FormData();

    for (let i = 0; i < inputArea.files.length; i++) {
        formData.append("file", inputArea.files[i])
    }

    let url = "/api/UploadImage"
    fetch(url, {
        method: 'POST',
        body: formData,
    })
        .then(res => {
            return res.json()
        })
        .then(res => {
            let inputParent = inputArea.parentNode
            let inputHidden = inputParent.querySelectorAll('[type = "hidden"]')

            //清除已經存在的hidden，避免資料變更後傳入錯誤url
            if (inputHidden.length != 0) {
                for (let i = 0; i < inputHidden.length; i++) {
                    inputParent.removeChild(inputHidden[i])
                }
            }

            //設定hidden的name及value
            if (res.data.length == 1) {
                let inputSendUrl = document.createElement("input")
                inputSendUrl.type = "hidden"
                inputSendUrl.value = res.data
                inputSendUrl.name = inputName

                inputParent.append(inputSendUrl)
            }
            else {
                res.data.forEach((data, index) => {
                    let inputSendUrl = document.createElement("input")
                    inputSendUrl.type = "hidden"
                    inputSendUrl.value = data
                    inputSendUrl.name = `${inputName}[${index}]`

                    inputParent.append(inputSendUrl)
                })
            }
        })
        .catch()
}

//返回頁首function
function GoTop() {
    if (document.documentElement.scrollTop > 0 || window.pageYOffset>0) {
        window.scrollTo(0, 0)
    }
}