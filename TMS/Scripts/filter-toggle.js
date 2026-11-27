
(function () {
    "use strict";

    const STORAGE_KEY = "CIPLTMS_FILTER_COLLAPSED";

    function initializeFilterToggle() {
        const filterPanel = document.querySelector(".filter-grid-container");
        const button = document.getElementById("filterToggle");

        if (!filterPanel || !button) {
            return;
        }

        // Find the common layout containing filter and GridView
        const layout = filterPanel.closest(".page-layout");

        if (!layout) {
            console.error("Filter toggle: .page-layout not found.");
            return;
        }

        // Show button only on pages with filters
        button.style.display = "flex";

        function setCollapsed(collapsed) {
            layout.classList.toggle("filter-collapsed", collapsed);

            button.setAttribute("aria-expanded", String(!collapsed));
            button.setAttribute(
                "aria-label",
                collapsed ? "Show filters" : "Hide filters"
            );
            button.setAttribute(
                "title",
                collapsed ? "Show filters" : "Hide filters"
            );

            try {
                localStorage.setItem(STORAGE_KEY, String(collapsed));
            } catch (error) {
                console.warn("Unable to save filter state.", error);
            }
        }

        // Restore previous state
        let isCollapsed = false;

        try {
            isCollapsed = localStorage.getItem(STORAGE_KEY) === "true";
        } catch (error) {
            console.warn("Unable to restore filter state.", error);
        }

        setCollapsed(isCollapsed);

        button.addEventListener("click", function () {
            const currentlyCollapsed =
                layout.classList.contains("filter-collapsed");

            setCollapsed(!currentlyCollapsed);
        });

        console.log("Filter toggle initialized successfully");
    }

    if (document.readyState === "loading") {
        document.addEventListener(
            "DOMContentLoaded",
            initializeFilterToggle
        );
    } else {
        initializeFilterToggle();
    }
})();
