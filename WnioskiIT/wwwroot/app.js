"use strict";

window.blazorApp = {
    init: function (dotNetObj) {
        // Restore saved theme
        const saved = localStorage.getItem("wnioskiIT-theme");
        const prefersDark = window.matchMedia("(prefers-color-scheme: dark)").matches;
        const theme = saved || (prefersDark ? "dark" : "light");
        document.documentElement.dataset.theme = theme;
    },

    setTheme: function (theme) {
        document.documentElement.dataset.theme = theme;
        try { localStorage.setItem("wnioskiIT-theme", theme); } catch (e) {}
    }
};

// Apply theme on load before Blazor initialises (prevents flash)
(function () {
    const saved = localStorage.getItem("wnioskiIT-theme");
    const prefersDark = window.matchMedia("(prefers-color-scheme: dark)").matches;
    document.documentElement.dataset.theme = saved || (prefersDark ? "dark" : "light");
})();
