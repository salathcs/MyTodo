const PROXY_CONFIG = [
  {
    context: [
      "/auth",
    ],
    target: "https://localhost:7272",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
