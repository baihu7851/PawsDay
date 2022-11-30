const searchMenu = document.querySelector('#menu-search')
const searchBar = document.querySelector('.menu-button-search')

const memberMenu = document.querySelector('#menu-member')
const memberBar = document.querySelector('.menu-button-member')

Menu(searchBar, searchMenu, 'menu-down', memberMenu)
Menu(memberBar, memberMenu, 'menu-down', searchMenu)

function Menu(bar, menu, menuDown, othermenu) {
    bar.addEventListener('click', () => {
        menu.classList.toggle(menuDown)
        othermenu.classList.remove(menuDown)
    })
}