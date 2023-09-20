/** @type {import('tailwindcps').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {},
  },
  plugins: [
    require("daisyui")
  ],
  daisyui: {
    themes: [
      {
        dark: {
          ...require("daisyui/src/theming/themes")["[data-theme=dark]"],
          "base-content": "#ecf9fb",
          "secondary-content": "#6d737b",
          "base-100": "#16181c",
          accent: "#1bd96a",
          "accent-focus": "#1bd9b6",
        },
      },
      'light'
    ],
  }
}
// main text - #ecf9fb
// secondary text - #6d737b, #9abac5, #949ca5, #969ea7
// #1bd96a
// not checked/checked #434956,#393e49
