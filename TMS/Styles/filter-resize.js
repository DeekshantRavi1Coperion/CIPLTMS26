(function () {
    "use strict";

    var DEFAULT_WIDTH = 260;
    var MIN_WIDTH = 180;
    var MAX_WIDTH = 600;

    var STORAGE_PREFIX = "CIPLTMS_FILTER_WIDTH_";

    function initializeFilterResize() {

        var layouts = document.querySelectorAll(".page-layout");

        for (var i = 0; i < layouts.length; i++) {
            setupLayout(layouts[i], i);
        }
    }

    function setupLayout(layout, index) {

        var filter = layout.querySelector(".filter-grid-container");
        var handle = layout.querySelector(".filter-resizer");
        var grid = layout.querySelector(".employee-grid-container");

        if (!filter || !handle || !grid) {
            return;
        }

        var storageKey =
            STORAGE_PREFIX +
            window.location.pathname +
            "_" +
            index;

        var isDragging = false;
        var startX = 0;
        var startWidth = 0;

        function clampWidth(width) {

            var availableWidth = layout.clientWidth;

            // Reserve at least 250px for the GridView.
            var maximum = Math.min(
                MAX_WIDTH,
                availableWidth - 250 - handle.offsetWidth
            );

            maximum = Math.max(MIN_WIDTH, maximum);

            return Math.max(
                MIN_WIDTH,
                Math.min(width, maximum)
            );
        }

        function setWidth(width) {

            var appliedWidth = clampWidth(width);

            layout.style.setProperty(
                "--filter-width",
                appliedWidth + "px"
            );

            handle.setAttribute(
                "aria-valuenow",
                Math.round(appliedWidth)
            );

            return appliedWidth;
        }

        function saveWidth(width) {

            try {
                localStorage.setItem(
                    storageKey,
                    String(Math.round(width))
                );
            } catch (error) {
                console.warn("Cannot save filter width.", error);
            }
        }

        // Restore previously saved width.
        try {

            var saved = localStorage.getItem(storageKey);

            if (saved !== null && isFinite(Number(saved))) {
                setWidth(Number(saved));
            } else {
                setWidth(DEFAULT_WIDTH);
            }

        } catch (error) {
            setWidth(DEFAULT_WIDTH);
        }

        // Start dragging.
        handle.addEventListener("mousedown", function (event) {

            if (event.button !== 0) {
                return;
            }

            event.preventDefault();

            isDragging = true;

            startX = event.clientX;
            startWidth = filter.getBoundingClientRect().width;

            handle.classList.add("dragging");
            document.body.classList.add("filter-resizing");
        });

        // Track movement even when the cursor leaves the handle.
        document.addEventListener("mousemove", function (event) {

            if (!isDragging) {
                return;
            }

            var difference = event.clientX - startX;

            setWidth(startWidth + difference);
        });

        // Finish dragging.
        document.addEventListener("mouseup", function () {

            if (!isDragging) {
                return;
            }

            isDragging = false;

            handle.classList.remove("dragging");
            document.body.classList.remove("filter-resizing");

            saveWidth(filter.getBoundingClientRect().width);
        });

        // Double-click to reset.
        handle.addEventListener("dblclick", function () {

            var width = setWidth(DEFAULT_WIDTH);

            saveWidth(width);
        });

        // Keyboard support.
        handle.addEventListener("keydown", function (event) {

            var width = filter.getBoundingClientRect().width;

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

        // Recalculate when the browser is resized.
        window.addEventListener("resize", function () {

            setWidth(filter.getBoundingClientRect().width);
        });
    }

    if (document.readyState === "loading") {

        document.addEventListener(
            "DOMContentLoaded",
            initializeFilterResize
        );

    } else {

        initializeFilterResize();
    }

})();