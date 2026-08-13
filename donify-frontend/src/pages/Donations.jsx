import { useEffect, useState } from 'react'
import {
  getDonations,
  createDonation,
  updateDonation,
  deleteDonation,
} from '../services/donationsService'
import { getDonors } from '../services/donorsService'
import Modal from '../components/Modal'
import Button from '../components/Button'
import Input from '../components/Input'
import ConfirmDialog from '../components/ConfirmDialog'

const emptyForm = {
  donorId: '',
  amount: '',
  type: 'Monetaria',
  description: '',
  paymentMethod: 'Transferencia',
  status: 'Pending',
}

const statusColors = {
  Pending: 'bg-yellow-100 text-yellow-800',
  Completed: 'bg-green-100 text-green-800',
  Cancelled: 'bg-red-100 text-red-800',
}

function Donations() {
  const [donations, setDonations] = useState([])
  const [donors, setDonors] = useState([])
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
    Promise.all([getDonations(), getDonors()])
      .then(([donationsRes, donorsRes]) => {
        setDonations(donationsRes.data)
        setDonors(donorsRes.data)
      })
      .catch(() => setError('No se pudieron cargar las donaciones'))
      .finally(() => setLoading(false))
  }

  useEffect(() => {
    loadAll()
  }, [])

  const donorName = (donorId) => {
    const donor = donors.find((d) => d.id === donorId)
    return donor ? `${donor.firstName} ${donor.lastName}` : `Donante #${donorId}`
  }

  const openCreateModal = () => {
    setEditingId(null)
    setForm(emptyForm)
    setFormError(null)
    setIsModalOpen(true)
  }

  const openEditModal = (donation) => {
    setEditingId(donation.id)
    setForm({
      donorId: donation.donorId,
      amount: donation.amount,
      type: donation.type,
      description: donation.description || '',
      paymentMethod: donation.paymentMethod,
      status: donation.status,
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
      donorId: Number(form.donorId),
      amount: Number(form.amount),
      type: form.type,
      description: form.description,
      paymentMethod: form.paymentMethod,
      status: form.status,
      donatedAt: editingId
        ? donations.find((d) => d.id === editingId)?.donatedAt
        : new Date().toISOString(),
    }

    try {
      if (editingId) {
        await updateDonation(editingId, payload)
      } else {
        await createDonation(payload)
      }
      setIsModalOpen(false)
      loadAll()
    } catch (err) {
      setFormError('No se pudo guardar la donación. Verifica los datos.')
    } finally {
      setSaving(false)
    }
  }

  const handleDelete = async () => {
    try {
      await deleteDonation(deleteTarget.id)
      setDeleteTarget(null)
      loadAll()
    } catch (err) {
      setError('No se pudo eliminar la donación')
    }
  }

  if (loading) return <p>Cargando donaciones...</p>

  return (
    <div>
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold">Donaciones</h1>
        <Button onClick={openCreateModal}>+ Nueva donación</Button>
      </div>

      {error && <p className="text-red-600 mb-4">{error}</p>}

      <div className="grid gap-3">
        {donations.map((d) => (
          <div
            key={d.id}
            className="border p-4 rounded-lg bg-white shadow-sm flex justify-between items-center"
          >
            <div>
              <p className="font-semibold">${Number(d.amount).toFixed(2)}</p>
              <p className="text-gray-600 text-sm">{donorName(d.donorId)}</p>
              <p className="text-gray-400 text-xs">
                {d.paymentMethod} — {new Date(d.donatedAt).toLocaleDateString()}
              </p>
            </div>
            <div className="flex items-center gap-2">
              <span
                className={`px-3 py-1 rounded-full text-xs font-medium ${
                  statusColors[d.status] || 'bg-gray-100 text-gray-800'
                }`}
              >
                {d.status}
              </span>
              <Button variant="secondary" onClick={() => openEditModal(d)}>
                Editar
              </Button>
              <Button variant="danger" onClick={() => setDeleteTarget(d)}>
                Eliminar
              </Button>
            </div>
          </div>
        ))}
        {donations.length === 0 && (
          <p className="text-gray-500">No hay donaciones registradas todavía.</p>
        )}
      </div>

      <Modal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        title={editingId ? 'Editar donación' : 'Nueva donación'}
      >
        <form onSubmit={handleSubmit}>
          <div className="mb-4">
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Donante
            </label>
            <select
              name="donorId"
              value={form.donorId}
              onChange={handleChange}
              required
              className="w-full border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
            >
              <option value="">Selecciona un donante</option>
              {donors.map((donor) => (
                <option key={donor.id} value={donor.id}>
                  {donor.firstName} {donor.lastName}
                </option>
              ))}
            </select>
          </div>

          <Input
            label="Monto"
            name="amount"
            type="number"
            step="0.01"
            value={form.amount}
            onChange={handleChange}
            required
          />

          <div className="mb-4">
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Método de pago
            </label>
            <select
              name="paymentMethod"
              value={form.paymentMethod}
              onChange={handleChange}
              className="w-full border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
            >
              <option value="Transferencia">Transferencia</option>
              <option value="Tarjeta">Tarjeta</option>
              <option value="Efectivo">Efectivo</option>
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
              <option value="Pending">Pendiente</option>
              <option value="Completed">Completada</option>
              <option value="Cancelled">Cancelada</option>
            </select>
          </div>

          <Input
            label="Descripción"
            name="description"
            value={form.description}
            onChange={handleChange}
          />

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
        message={`¿Seguro que quieres eliminar esta donación de $${deleteTarget?.amount}? Esta acción no se puede deshacer.`}
      />
    </div>
  )
}

export default Donations
