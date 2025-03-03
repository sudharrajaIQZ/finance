/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
      "./src/**/*.{html,ts,tsx,jsx,js}",
      "./components/**/*.{html,ts,tsx,jsx,js}",
    ],
    theme: {
      extend: {
        colors: {
          primary: "#1E40AF",
          secondary: "#9333EA",
          accent: "#14B8A6",
        },
        fontFamily: {
          sans: ["Inter", "sans-serif"],
        },
        fontSize: {
          "15": "15px"
        },
        borderRadius: {
          xl: "1rem",
        },
      },
    },
    plugins: [],
    darkMode: "class",
  };
