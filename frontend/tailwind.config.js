/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        appRed: "#FF0000",
        appLight: "#D1D1D1",
        appInputBlack: "#0B0B0B",
        appLightBlack: "#262626",
        appSuperLightWhite: "#EFEFEF",
        appBlue: "#25C2D8"
      }
    },
  },
  plugins: [],
}
