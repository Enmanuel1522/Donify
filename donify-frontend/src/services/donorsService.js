import api from './api'

export const getDonors = () => api.get('/donor')
export const getDonorById = (id) => api.get(`/donor/${id}`)
export const createDonor = (data) => api.post('/donor', data)
export const updateDonor = (id, data) => api.put(`/donor/${id}`, data)
export const deleteDonor = (id) => api.delete(`/donor/${id}`)
