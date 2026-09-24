
/* =========================================================
   APPLICATION MENU
   ========================================================= */

(function () {

    "use strict";


    /* =====================================================
       INITIALIZE MENU
       ===================================================== */

    function initializeMenu() {

        const menuContainer =
            document.querySelector(".main-menu");


        if (!menuContainer) {

            return;

        }


        /* =================================================
           EXPAND / COLLAPSE MENU
           ================================================= */

        const menuToggles =
            menuContainer.querySelectorAll(".menu-toggle");


        menuToggles.forEach(function (toggle) {


            toggle.addEventListener("click", function (event) {

                event.preventDefault();

                event.stopPropagation();


                const parent =
                    toggle.closest(".has-children");


                if (!parent) {

                    return;

                }


                /*
                 * Toggle current menu.
                 */

                const isOpen =
                    parent.classList.contains("open");


                if (isOpen) {

                    closeMenu(parent);

                }
                else {

                    openMenu(parent);

                }

            });

        });


        /* =================================================
           ACTIVE MENU
           ================================================= */

        setActiveMenu();


    }


    /* =====================================================
       OPEN MENU
       ===================================================== */

    function openMenu(menuItem) {

        if (!menuItem) {

            return;

        }


        menuItem.classList.add("open");


        const submenu =
            menuItem.querySelector(":scope > .submenu");


        if (submenu) {

            submenu.style.display = "block";

        }

    }


    /* =====================================================
       CLOSE MENU
       ===================================================== */

    function closeMenu(menuItem) {

        if (!menuItem) {

            return;

        }


        menuItem.classList.remove("open");


        const submenu =
            menuItem.querySelector(":scope > .submenu");


        if (submenu) {

            submenu.style.display = "none";


            /*
             * Close all child menus also.
             */

            const childMenus =
                submenu.querySelectorAll(".has-children");


            childMenus.forEach(function (child) {

                child.classList.remove("open");


                const childSubmenu =
                    child.querySelector(":scope > .submenu");


                if (childSubmenu) {

                    childSubmenu.style.display = "none";

                }

            });

        }

    }


    /* =====================================================
       ACTIVE MENU
       ===================================================== */

    function setActiveMenu() {

        const currentPath =
            window.location.pathname
                .toLowerCase()
                .replace(/\/+$/, "");


        const menuLinks =
            document.querySelectorAll(
                ".main-menu a.menu-link"
            );


        let activeLink = null;


        menuLinks.forEach(function (link) {

            const href =
                link.getAttribute("href");


            if (!href) {

                return;

            }


            /*
             * Ignore javascript links and # links.
             */

            if (href === "#" ||
                href.toLowerCase().startsWith("javascript:")) {

                return;

            }


            /*
             * Convert relative URL into absolute URL.
             */

            let linkUrl;


            try {

                linkUrl =
                    new URL(href, window.location.origin);

            }
            catch (error) {

                return;

            }


            const linkPath =
                linkUrl.pathname
                    .toLowerCase()
                    .replace(/\/+$/, "");


            /*
             * Compare URLs.
             */

            if (linkPath === currentPath) {

                activeLink = link;

            }

        });


        /*
         * Apply active menu.
         */

        if (activeLink) {

            activeLink.classList.add("active");


            /*
             * Open all parent menus.
             */

            openParentMenus(activeLink);

        }

    }


    /* =====================================================
       OPEN PARENT MENUS
       ===================================================== */

    function openParentMenus(element) {

        let parent =
            element.parentElement;


        while (parent) {


            if (parent.classList &&
                parent.classList.contains("has-children")) {

                openMenu(parent);

            }


            parent =
                parent.parentElement;

        }

    }


    /* =====================================================
       CLOSE OTHER ROOT MENUS
       ===================================================== */

    function closeOtherRootMenus(currentMenu) {

        const mainMenu =
            document.querySelector(".menu-tree");


        if (!mainMenu) {

            return;

        }


        const rootMenus =
            mainMenu.querySelectorAll(
                ":scope > li.has-children"
            );


        rootMenus.forEach(function (menu) {

            if (menu !== currentMenu) {

                closeMenu(menu);

            }

        });

    }


    /* =====================================================
       DOM READY
       ===================================================== */

    if (document.readyState === "loading") {

        document.addEventListener(
            "DOMContentLoaded",
            initializeMenu
        );

    }
    else {

        initializeMenu();

    }


})();
