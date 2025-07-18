/** @type {import('tailwindcss').Config} */
module.exports = {
    darkMode: ["selector"],
    content: [
        "./Pages/**/*.{html,cshtml,razor,js}",
        "./Views/**/*.{html,cshtml,razor,js}",
        "./node_modules/flowbite/**/*.js",
    ],
    theme: {
        extend: {
            container: {
                center: true,
                padding: "1rem",
            },
            colors: {
                primary: {
                    "50": "#eff6ff",
                    "100": "#dbeafe",
                    "200": "#bfdbfe",
                    "300": "#93c5fd",
                    "400": "#60a5fa",
                    "500": "#3b82f6",
                    "600": "#2563eb",
                    "700": "#1d4ed8",
                    "800": "#1e40af",
                    "900": "#1e3a8a",
                    "950": "#172554"
                },
                error: "#FB9898",
            },
        },
    },
    plugins: [
        require("@tailwindcss/typography"),
        require("@tailwindcss/aspect-ratio"),
        require("flowbite/plugin")({
            charts: false,
            forms: true,
            tooltips: true,
        }),
        require("flowbite-typography"),
    ],
};
