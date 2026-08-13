import { useEffect, useState } from 'react'
import {
  getCampaigns,
  createCampaign,
  updateCampaign,
  deleteCampaign,
} from '../services/campaignsService'
import { getCategories } from '../services/categoriesService'
import Modal from '../components/Modal'
import Button from '../components/Button'
import Input from '../components/Input'
import ConfirmDialog from '../components/ConfirmDialog'

// CampaignStatus enum en el backend: Active=0, Finished=1, Cancelled=2
const statusOptions = [
  { value: 'Active', label: 'Activa' },
  { value: 'Finished', label: 'Finalizada' },
  { value: 'Cancelled', label: 'Cancelada' },
]


const statusStyles = {
  Active: 'bg-green-100 text-green-800',
  Finished: 'bg-blue-100 text-blue-800',
  Cancelled: 'bg-red-100 text-red-800',
}


const emptyForm = {
  name: '',
  description: '',
  goalAmount: '',
  collectedAmount: '0',
  startDate: '',
  endDate: '',
  status: 'Active',
  categoryId: '',
}

function Campaigns() {
  const [campaigns, setCampaigns] = useState([])
  const [categories, setCategories] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(null)

  const [isModalOpen, setIsModalOpen] = useState(false)
  const [editingId, setEditingId] = useState(null)
  const [form, setForm] = useState(emptyForm)
  const [saving, setSaving] = useState(false)
  const [formError, setFormError] = useState(null)

  const [deleteTarget, setDeleteTarget] = useState(null)

  const loadAll = () => {
    setLoading(true)
    Promise.all([getCampaigns(), getCategories()])
      .then(([campaignsRes, categoriesRes]) => {
        setCampaigns(campaignsRes.data)
        setCategories(categoriesRes.data)
      })
      .catch(() => setError('No se pudieron cargar las campañas'))
      .finally(() => setLoading(false))
  }

  useEffect(() => {
    loadAll()
  }, [])

  const categoryName = (categoryId) => {
    const cat = categories.find((c) => c.id === categoryId)
    return cat ? cat.name : `Categoría #${categoryId}`
  }

  const toDateInput = (isoString) => (isoString ? isoString.split('T')[0] : '')

  const openCreateModal = () => {
    setEditingId(null)
    setForm(emptyForm)
    setFormError(null)
    setIsModalOpen(true)
  }

  const openEditModal = (campaign) => {
    setEditingId(campaign.id)
    setForm({
      name: campaign.name,
      description: campaign.description || '',
      goalAmount: campaign.goalAmount,
      collectedAmount: campaign.collectedAmount,
      startDate: toDateInput(campaign.startDate),
      endDate: toDateInput(campaign.endDate),
      status: campaign.status,
      categoryId: campaign.categoryId,
    })
    setFormError(null)
    setIsModalOpen(true)
  }

  const handleChange = (e) => {
    const { name, value } = e.target
    setForm((prev) => ({ ...prev, [name]: value }))
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    setSaving(true)
    setFormError(null)

    const payload = {
      id: editingId || 0,
      name: form.name,
      description: form.description,
      goalAmount: Number(form.goalAmount),
      collectedAmount: Number(form.collectedAmount),
      startDate: new Date(form.startDate).toISOString(),
      endDate: new Date(form.endDate).toISOString(),
      status: form.status,
      categoryId: Number(form.categoryId),
    }

    try {
      if (editingId) {
        await updateCampaign(editingId, payload)
      } else {
        await createCampaign(payload)
      }
      setIsModalOpen(false)
      loadAll()
    } catch (err) {
      setFormError('No se pudo guardar la campaña. Verifica los datos.')
    } finally {
      setSaving(false)
    }
  }

  const handleDelete = async () => {
    try {
      await deleteCampaign(deleteTarget.id)
      setDeleteTarget(null)
      loadAll()
    } catch (err) {
      setError('No se pudo eliminar la campaña')
    }
  }

  if (loading) return <p>Cargando campañas...</p>

  return (
    <div>
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold">Campañas</h1>
        <Button onClick={openCreateModal}>+ Nueva campaña</Button>
      </div>

      {error && <p className="text-red-600 mb-4">{error}</p>}

      <div className="grid gap-4 md:grid-cols-2">
        {campaigns.map((c) => {
          const progress =
            c.goalAmount > 0
              ? Math.min((c.collectedAmount / c.goalAmount) * 100, 100)
              : 0
          return (
            <div key={c.id} className="border p-4 rounded-lg bg-white shadow-sm">
              <div className="flex justify-between items-start mb-1">
                <h2 className="font-semibold text-lg">{c.name}</h2>
                <span
                  className={`text-xs px-2 py-1 rounded-full ${statusStyles[c.status]}`}
                >
                  {statusOptions.find((s) => s.value === c.status)?.label}
                </span>
              </div>
              <p className="text-gray-500 text-xs mb-2">{categoryName(c.categoryId)}</p>
              <p className="text-gray-600 text-sm mb-3">{c.description}</p>

              <div className="w-full bg-gray-200 rounded-full h-2 mb-1">
                <div
                  className="bg-blue-600 h-2 rounded-full"
                  style={{ width: `${progress}%` }}
                />
              </div>
              <p className="text-sm text-gray-600 mb-3">
                ${Number(c.collectedAmount).toLocaleString()} de $
                {Number(c.goalAmount).toLocaleString()}
              </p>

              <p className="text-xs text-gray-400 mb-3">
                {new Date(c.startDate).toLocaleDateString()} —{' '}
                {new Date(c.endDate).toLocaleDateString()}
              </p>

              <div className="flex justify-end gap-2">
                <Button variant="secondary" onClick={() => openEditModal(c)}>
                  Editar
                </Button>
                <Button variant="danger" onClick={() => setDeleteTarget(c)}>
                  Eliminar
                </Button>
              </div>
            </div>
          )
        })}
        {campaigns.length === 0 && (
          <p className="text-gray-500">No hay campañas registradas todavía.</p>
        )}
      </div>

      <Modal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        title={editingId ? 'Editar campaña' : 'Nueva campaña'}
      >
        <form onSubmit={handleSubmit}>
          <Input
            label="Nombre"
            name="name"
            value={form.name}
            onChange={handleChange}
            required
          />
          <Input
            label="Descripción"
            name="description"
            value={form.description}
            onChange={handleChange}
          />
          <Input
            label="Meta ($)"
            name="goalAmount"
            type="number"
            step="0.01"
            value={form.goalAmount}
            onChange={handleChange}
            required
          />
          <Input
            label="Recaudado ($)"
            name="collectedAmount"
            type="number"
            step="0.01"
            value={form.collectedAmount}
            onChange={handleChange}
          />
          <div className="grid grid-cols-2 gap-3">
            <Input
              label="Fecha inicio"
              name="startDate"
              type="date"
              value={form.startDate}
              onChange={handleChange}
              required
            />
            <Input
              label="Fecha fin"
              name="endDate"
              type="date"
              value={form.endDate}
              onChange={handleChange}
              required
            />
          </div>

          <div className="mb-4">
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Categoría
            </label>
            <select
              name="categoryId"
              value={form.categoryId}
              onChange={handleChange}
              required
              className="w-full border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
            >
              <option value="">Selecciona una categoría</option>
              {categories.map((cat) => (
                <option key={cat.id} value={cat.id}>
                  {cat.name}
                </option>
              ))}
            </select>
          </div>

          <div className="mb-4">
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Estado
            </label>
            <select
              name="status"
              value={form.status}
              onChange={handleChange}
              className="w-full border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
            >
              {statusOptions.map((s) => (
                <option key={s.value} value={s.value}>
                  {s.label}
                </option>
              ))}
            </select>
          </div>

          {formError && <p className="text-red-500 text-sm mb-4">{formError}</p>}

          <div className="flex justify-end gap-3">
            <Button
              type="button"
              variant="secondary"
              onClick={() => setIsModalOpen(false)}
            >
              Cancelar
            </Button>
            <Button type="submit" disabled={saving}>
              {saving ? 'Guardando...' : 'Guardar'}
            </Button>
          </div>
        </form>
      </Modal>

      <ConfirmDialog
        isOpen={!!deleteTarget}
        onClose={() => setDeleteTarget(null)}
        onConfirm={handleDelete}
        message={`¿Seguro que quieres eliminar la campaña "${deleteTarget?.name}"? Esta acción no se puede deshacer.`}
      />
    </div>
  )
}

export default Campaigns
