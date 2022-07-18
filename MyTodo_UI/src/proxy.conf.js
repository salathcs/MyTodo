const PROXY_CONFIG = [
  {
    context: [
      "/api/users",
    ],
    target: "https://localhost:7088",
    secure: false
  },
  {
    context: [
      "/api/todos",
    ],
    target: "https://localhost:7096",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
