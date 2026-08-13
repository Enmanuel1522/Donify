import { useEffect, useState } from 'react'
import {
  getCategories,
  createCategory,
  updateCategory,
  deleteCategory,
} from '../services/categoriesService'
import Modal from '../components/Modal'
import Button from '../components/Button'
import Input from '../components/Input'
import ConfirmDialog from '../components/ConfirmDialog'

const emptyForm = { name: '', description: '' }

function Categories() {
  const [categories, setCategories] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(null)

  const [isModalOpen, setIsModalOpen] = useState(false)
  const [editingId, setEditingId] = useState(null)
  const [form, setForm] = useState(emptyForm)
  const [saving, setSaving] = useState(false)
  const [formError, setFormError] = useState(null)

  const [deleteTarget, setDeleteTarget] = useState(null)

  const loadCategories = () => {
    setLoading(true)
    getCategories()
      .then((res) => setCategories(res.data))
      .catch(() => setError('No se pudieron cargar las categorías'))
      .finally(() => setLoading(false))
  }

  useEffect(() => {
    loadCategories()
  }, [])

  const openCreateModal = () => {
    setEditingId(null)
    setForm(emptyForm)
    setFormError(null)
    setIsModalOpen(true)
  }

  const openEditModal = (category) => {
    setEditingId(category.id)
    setForm({ name: category.name, description: category.description || '' })
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

    const payload = { id: editingId || 0, ...form }

    try {
      if (editingId) {
        await updateCategory(editingId, payload)
      } else {
        await createCategory(payload)
      }
      setIsModalOpen(false)
      loadCategories()
    } catch (err) {
      setFormError('No se pudo guardar la categoría. Verifica los datos.')
    } finally {
      setSaving(false)
    }
  }

  const handleDelete = async () => {
    try {
      await deleteCategory(deleteTarget.id)
      setDeleteTarget(null)
      loadCategories()
    } catch (err) {
      setError('No se pudo eliminar la categoría (puede tener campañas asociadas)')
    }
  }

  if (loading) return <p>Cargando categorías...</p>

  return (
    <div>
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold">Categorías</h1>
        <Button onClick={openCreateModal}>+ Nueva categoría</Button>
      </div>

      {error && <p className="text-red-600 mb-4">{error}</p>}

      <div className="grid gap-3 md:grid-cols-3">
        {categories.map((c) => (
          <div key={c.id} className="border p-4 rounded-lg bg-white shadow-sm">
            <p className="font-semibold">{c.name}</p>
            <p className="text-gray-600 text-sm mb-3">{c.description}</p>
            <div className="flex justify-end gap-2">
              <Button variant="secondary" onClick={() => openEditModal(c)}>
                Editar
              </Button>
              <Button variant="danger" onClick={() => setDeleteTarget(c)}>
                Eliminar
              </Button>
            </div>
          </div>
        ))}
        {categories.length === 0 && (
          <p className="text-gray-500">No hay categorías registradas todavía.</p>
        )}
      </div>

      <Modal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        title={editingId ? 'Editar categoría' : 'Nueva categoría'}
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
        message={`¿Seguro que quieres eliminar la categoría "${deleteTarget?.name}"? Esta acción no se puede deshacer.`}
      />
    </div>
  )
}

export default Categories
