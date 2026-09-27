/* =========================================================
   RESIZABLE SIDEBAR
   ========================================================= */

(function () {

    "use strict";

    const DEFAULT_WIDTH = 280;
    const MIN_WIDTH = 180;
    const MAX_WIDTH = 450;

    const STORAGE_KEY = "CIPLTMS_SIDEBAR_WIDTH";

    let sidebar = null;
    let resizer = null;

    let isResizing = false;


    /* =====================================================
       INITIALIZE
       ===================================================== */

    function initializeSidebarResizer() {

        sidebar = document.querySelector(".sidebar");
        resizer = document.querySelector(".sidebar-resizer");

        if (!sidebar || !resizer) {
            return;
        }

        restoreSidebarWidth();

        resizer.addEventListener(
            "mousedown",
            startResize
        );

        resizer.addEventListener(
            "dblclick",
            resetSidebarWidth
        );
    }


    /* =====================================================
       START RESIZE
       ===================================================== */

    function startResize(event) {

        event.preventDefault();

        isResizing = true;

        document.body.style.cursor = "col-resize";
        document.body.style.userSelect = "none";

        document.addEventListener(
            "mousemove",
            resizeSidebar
        );

        document.addEventListener(
            "mouseup",
            stopResize
        );
    }


    /* =====================================================
       RESIZE SIDEBAR
       ===================================================== */

    function resizeSidebar(event) {

        if (!isResizing || !sidebar) {
            return;
        }

        let newWidth = event.clientX;

        if (newWidth < MIN_WIDTH) {
            newWidth = MIN_WIDTH;
        }

        if (newWidth > MAX_WIDTH) {
            newWidth = MAX_WIDTH;
        }

        setSidebarWidth(newWidth);
    }


    /* =====================================================
       STOP RESIZE
       ===================================================== */

    function stopResize() {

        if (!isResizing) {
            return;
        }

        isResizing = false;

        document.body.style.cursor = "";
        document.body.style.userSelect = "";

        document.removeEventListener(
            "mousemove",
            resizeSidebar
        );

        document.removeEventListener(
            "mouseup",
            stopResize
        );

        saveSidebarWidth();
    }


    /* =====================================================
       SET SIDEBAR WIDTH
       ===================================================== */

    function setSidebarWidth(width) {

        if (!sidebar) {
            return;
        }

        sidebar.style.width = width + "px";
        sidebar.style.minWidth = width + "px";
    }


    /* =====================================================
       SAVE WIDTH
       ===================================================== */

    function saveSidebarWidth() {

        if (!sidebar) {
            return;
        }

        const width = parseInt(
            window.getComputedStyle(sidebar).width,
            10
        );

        if (!isNaN(width)) {

            localStorage.setItem(
                STORAGE_KEY,
                width.toString()
            );
        }
    }


    /* =====================================================
       RESTORE WIDTH
       ===================================================== */

    function restoreSidebarWidth() {

        const savedWidth =
            localStorage.getItem(STORAGE_KEY);

        if (!savedWidth) {
            setSidebarWidth(DEFAULT_WIDTH);
            return;
        }

        let width = parseInt(
            savedWidth,
            10
        );

        if (isNaN(width)) {
            width = DEFAULT_WIDTH;
        }

        if (width < MIN_WIDTH) {
            width = MIN_WIDTH;
        }

        if (width > MAX_WIDTH) {
            width = MAX_WIDTH;
        }

        setSidebarWidth(width);
    }


    /* =====================================================
       RESET WIDTH
       ===================================================== */

    function resetSidebarWidth(event) {

        event.preventDefault();

        setSidebarWidth(DEFAULT_WIDTH);

        localStorage.setItem(
            STORAGE_KEY,
            DEFAULT_WIDTH.toString()
        );
    }


    /* =====================================================
       PAGE LOAD
       ===================================================== */

    if (document.readyState === "loading") {

        document.addEventListener(
            "DOMContentLoaded",
            initializeSidebarResizer
        );

    } else {

        initializeSidebarResizer();
    }

})();