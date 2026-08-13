import api from './api'

export const getCampaigns = () => api.get('/campaign')
export const getCampaignById = (id) => api.get(`/campaign/${id}`)
export const createCampaign = (data) => api.post('/campaign', data)
export const updateCampaign = (id, data) => api.put(`/campaign/${id}`, data)
export const deleteCampaign = (id) => api.delete(`/campaign/${id}`)
