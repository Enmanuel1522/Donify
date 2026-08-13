import { useEffect, useState } from 'react'
import {
  getDonors,
  createDonor,
  updateDonor,
  deleteDonor,
} from '../services/donorsService'
import Modal from '../components/Modal'
import Button from '../components/Button'
import Input from '../components/Input'
import ConfirmDialog from '../components/ConfirmDialog'

const emptyForm = {
  firstName: '',
  lastName: '',
  donorType: 'Individual',
  email: '',
  phone: '',
  isActive: true,
}

function Donors() {
  const [donors, setDonors] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(null)

  const [isModalOpen, setIsModalOpen] = useState(false)
  const [editingId, setEditingId] = useState(null)
  const [form, setForm] = useState(emptyForm)
  const [saving, setSaving] = useState(false)
  const [formError, setFormError] = useState(null)

  const [deleteTarget, setDeleteTarget] = useState(null)

  const loadDonors = () => {
    setLoading(true)
    getDonors()
      .then((res) => setDonors(res.data))
      .catch(() => setError('No se pudieron cargar los donantes'))
      .finally(() => setLoading(false))
  }

  useEffect(() => {
    loadDonors()
  }, [])

  const openCreateModal = () => {
    setEditingId(null)
    setForm(emptyForm)
    setFormError(null)
    setIsModalOpen(true)
  }

  const openEditModal = (donor) => {
    setEditingId(donor.id)
    setForm({
      firstName: donor.firstName || '',
      lastName: donor.lastName || '',
      donorType: donor.donorType || 'Individual',
      email: donor.email || '',
      phone: donor.phone || '',
      isActive: donor.isActive,
    })
    setFormError(null)
    setIsModalOpen(true)
  }

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target
    setForm((prev) => ({ ...prev, [name]: type === 'checkbox' ? checked : value }))
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    setSaving(true)
    setFormError(null)

    const payload = {
      id: editingId || 0,
      ...form,
      registeredAt: editingId
        ? donors.find((d) => d.id === editingId)?.registeredAt
        : new Date().toISOString(),
    }

    try {
      if (editingId) {
        await updateDonor(editingId, payload)
      } else {
        await createDonor(payload)
      }
      setIsModalOpen(false)
      loadDonors()
    } catch (err) {
      setFormError('No se pudo guardar el donante. Verifica los datos.')
    } finally {
      setSaving(false)
    }
  }

  const handleDelete = async () => {
    try {
      await deleteDonor(deleteTarget.id)
      setDeleteTarget(null)
      loadDonors()
    } catch (err) {
      setError('No se pudo eliminar el donante')
    }
  }

  if (loading) return <p>Cargando donantes...</p>

  return (
    <div>
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold">Donantes</h1>
        <Button onClick={openCreateModal}>+ Nuevo donante</Button>
      </div>

      {error && <p className="text-red-600 mb-4">{error}</p>}

      <div className="grid gap-3">
        {donors.map((d) => (
          <div
            key={d.id}
            className="border p-4 rounded-lg bg-white shadow-sm flex justify-between items-center"
          >
            <div>
              <p className="font-semibold">
                {d.firstName} {d.lastName}{' '}
                <span className="text-xs font-normal text-gray-500">
                  ({d.donorType})
                </span>
              </p>
              <p className="text-gray-600 text-sm">{d.email}</p>
              {d.phone && <p className="text-gray-400 text-xs">{d.phone}</p>}
            </div>
            <div className="flex items-center gap-2">
              <span
                className={`text-xs px-2 py-1 rounded-full ${
                  d.isActive
                    ? 'bg-green-100 text-green-800'
                    : 'bg-gray-100 text-gray-600'
                }`}
              >
                {d.isActive ? 'Activo' : 'Inactivo'}
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
        {donors.length === 0 && (
          <p className="text-gray-500">No hay donantes registrados todavía.</p>
        )}
      </div>

      <Modal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        title={editingId ? 'Editar donante' : 'Nuevo donante'}
      >
        <form onSubmit={handleSubmit}>
          <Input
            label="Nombre"
            name="firstName"
            value={form.firstName}
            onChange={handleChange}
            required
          />
          <Input
            label="Apellido"
            name="lastName"
            value={form.lastName}
            onChange={handleChange}
            required
          />
          <div className="mb-4">
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Tipo de donante
            </label>
            <select
              name="donorType"
              value={form.donorType}
              onChange={handleChange}
              className="w-full border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
            >
              <option value="Individual">Individual</option>
              <option value="Corporativo">Corporativo</option>
            </select>
          </div>
          <Input
            label="Email"
            name="email"
            type="email"
            value={form.email}
            onChange={handleChange}
            required
          />
          <Input
            label="Teléfono"
            name="phone"
            value={form.phone}
            onChange={handleChange}
          />
          <label className="flex items-center gap-2 mb-4 text-sm text-gray-700">
            <input
              type="checkbox"
              name="isActive"
              checked={form.isActive}
              onChange={handleChange}
            />
            Donante activo
          </label>

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
        message={`¿Seguro que quieres eliminar a "${deleteTarget?.firstName} ${deleteTarget?.lastName}"? Esta acción no se puede deshacer.`}
      />
    </div>
  )
}

export default Donors
