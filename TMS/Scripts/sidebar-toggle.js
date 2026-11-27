
(function () {

    "use strict";

    const STORAGE_KEY = "CIPLTMS_SIDEBAR_COLLAPSED";

    function initializeSidebarToggle() {

        const layout = document.querySelector(".app-layout");
        const sidebar = document.getElementById("appSidebar");
        const button = document.getElementById("sidebarToggle");

        if (!layout || !sidebar || !button) {
            console.error("Sidebar toggle: Required elements not found.");
            return;
        }

        function setCollapsed(collapsed) {

            layout.classList.toggle(
                "sidebar-collapsed",
                collapsed
            );

            button.setAttribute(
                "aria-expanded",
                String(!collapsed)
            );

            button.setAttribute(
                "aria-label",
                collapsed ? "Show sidebar" : "Hide sidebar"
            );

            button.setAttribute(
                "title",
                collapsed ? "Show sidebar" : "Hide sidebar"
            );

            try {
                localStorage.setItem(
                    STORAGE_KEY,
                    String(collapsed)
                );
            }
            catch (error) {
                console.warn("Unable to save sidebar state.", error);
            }
        }

        // Restore previous state
        let isCollapsed = false;

        try {
            isCollapsed =
                localStorage.getItem(STORAGE_KEY) === "true";
        }
        catch (error) {
            console.warn("Unable to restore sidebar state.", error);
        }

        setCollapsed(isCollapsed);

        // Toggle sidebar on button click
        button.addEventListener("click", function () {

            const currentlyCollapsed =
                layout.classList.contains("sidebar-collapsed");

            setCollapsed(!currentlyCollapsed);

        });

        console.log("Sidebar toggle initialized successfully");
    }

    if (document.readyState === "loading") {

        document.addEventListener(
            "DOMContentLoaded",
            initializeSidebarToggle
        );

    }
    else {

        initializeSidebarToggle();

    }

})();
