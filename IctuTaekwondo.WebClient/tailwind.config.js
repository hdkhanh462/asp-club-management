/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: ["selector"],
  content: [
    "./Pages/**/*.{html,cshtml,razor,js}",
    "./Views/**/*.{html,cshtml,razor,js}",
  ],
  theme: {
    extend: {
      container:{
        center:true,
        padding: "1rem",
      },
      colors: {
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
