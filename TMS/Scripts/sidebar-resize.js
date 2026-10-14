
(function () {
    "use strict";

    const DEFAULT_WIDTH = 280;
    const MIN_WIDTH = 200;
    const MAX_WIDTH = 600;

    const STORAGE_KEY = "CIPLTMS_SIDEBAR_WIDTH";

    function initSidebarResize() {

        const sidebar = document.getElementById("appSidebar");
        const handle = document.getElementById("sidebarResizer");

        if (!sidebar || !handle) {
            console.error("Sidebar resize: Required elements not found.");
            return;
        }

        let isDragging = false;
        let startX = 0;
        let startWidth = 0;

        function clampWidth(width) {

            const availableWidth = Math.max(
                MIN_WIDTH,
                window.innerWidth - 320
            );

            return Math.max(
                MIN_WIDTH,
                Math.min(width, MAX_WIDTH, availableWidth)
            );
        }

        function setWidth(width) {

            const newWidth = clampWidth(width);

            document.documentElement.style.setProperty(
                "--sidebar-width",
                newWidth + "px"
            );

            return newWidth;
        }

        function saveWidth(width) {

            try {
                localStorage.setItem(
                    STORAGE_KEY,
                    String(Math.round(width))
                );
            } catch (error) {
                console.warn("Unable to save sidebar width.", error);
            }
        }

        // Restore saved width
        try {

            const savedWidth = localStorage.getItem(STORAGE_KEY);

            if (savedWidth !== null && Number.isFinite(Number(savedWidth))) {
                setWidth(Number(savedWidth));
            } else {
                setWidth(DEFAULT_WIDTH);
            }

        } catch (error) {
            setWidth(DEFAULT_WIDTH);
        }

        // Start dragging
        handle.addEventListener("mousedown", function (event) {

            if (event.button !== 0) return;

            event.preventDefault();

            isDragging = true;

            startX = event.clientX;
            startWidth = sidebar.getBoundingClientRect().width;

            document.body.classList.add("sidebar-resizing");
            handle.classList.add("dragging");

            console.log("Sidebar dragging started");
        });

        // Track movement across entire document
        document.addEventListener("mousemove", function (event) {

            if (!isDragging) return;

            const difference = event.clientX - startX;

            const newWidth = startWidth + difference;

            setWidth(newWidth);
        });

        // Stop dragging
        document.addEventListener("mouseup", function () {

            if (!isDragging) return;

            isDragging = false;

            document.body.classList.remove("sidebar-resizing");
            handle.classList.remove("dragging");

            const finalWidth = sidebar.getBoundingClientRect().width;

            saveWidth(finalWidth);

            console.log("Sidebar width saved:", finalWidth);
        });

        // Double-click to reset
        handle.addEventListener("dblclick", function () {

            const resetWidth = setWidth(DEFAULT_WIDTH);

            saveWidth(resetWidth);

            console.log("Sidebar reset to default width");
        });

        // Keyboard resizing
        handle.addEventListener("keydown", function (event) {

            let width = sidebar.getBoundingClientRect().width;

            if (event.key === "ArrowRight") {
                width += 10;
            } else if (event.key === "ArrowLeft") {
                width -= 10;
            } else if (event.key === "Home") {
                width = DEFAULT_WIDTH;
            } else {
                return;
            }

            event.preventDefault();

            saveWidth(setWidth(width));
        });

        console.log("Sidebar resize initialized successfully");
    }

    if (document.readyState === "loading") {

        document.addEventListener(
            "DOMContentLoaded",
            initSidebarResize
        );

    } else {

        initSidebarResize();
    }

})();
