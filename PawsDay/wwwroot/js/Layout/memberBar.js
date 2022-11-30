const memberMenu = document.querySelector('#menu-member')
const memberBar = document.querySelector('.menu-button-member')
Menu(memberBar, memberMenu, 'menu-down')

function Menu(bar, menu, menuDown) {
    bar.addEventListener('click', () => {
        menu.classList.toggle(menuDown)
    })
}