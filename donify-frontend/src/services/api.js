import axios from 'axios'

const api = axios.create({
  baseURL: 'http://localhost:5000/api', // ajusta si tu API usa otro puerto o prefijo
  headers: {
    'Content-Type': 'application/json',
  },
})

export default api

