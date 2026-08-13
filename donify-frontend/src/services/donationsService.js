import api from './api'

export const getDonations = () => api.get('/donation')
export const getDonationById = (id) => api.get(`/donation/${id}`)
export const createDonation = (data) => api.post('/donation', data)
export const updateDonation = (id, data) => api.put(`/donation/${id}`, data)
export const deleteDonation = (id) => api.delete(`/donation/${id}`)