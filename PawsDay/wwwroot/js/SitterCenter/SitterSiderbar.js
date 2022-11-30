const currentpage = document.querySelector('.currenttitle').innerText
const ul = document.querySelector('.sitter-function-list')
const sheetnames = ul.querySelectorAll('p')

sheetnames.forEach(sheet => {
    if (sheet.innerText == currentpage) {
        sheet.parentElement.parentElement.classList.add('this-a')
    }
})