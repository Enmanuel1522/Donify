import { useEffect, useState } from 'react'
import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  ResponsiveContainer,
  PieChart,
  Pie,
  Cell,
  Legend,
  LineChart,
  Line,
} from 'recharts'
import { getDonors } from '../services/donorsService'
import { getDonations } from '../services/donationsService'
import { getCampaigns } from '../services/campaignsService'
import { getCategories } from '../services/categoriesService'

const STATUS_COLORS = {
  Completed: '#16a34a',
  Pending: '#eab308',
  Cancelled: '#dc2626',
}

const CAMPAIGN_STATUS_LABELS = {
  Active: 'Activa',
  Finished: 'Finalizada',
  Cancelled: 'Cancelada',
}

function StatCard({ label, value, sublabel, accent = 'text-blue-900' }) {
  return (
    <div className="bg-white rounded-xl shadow-sm border p-5">
      <p className="text-sm text-gray-500 mb-1">{label}</p>
      <p className={`text-3xl font-bold ${accent}`}>{value}</p>
      {sublabel && <p className="text-xs text-gray-400 mt-1">{sublabel}</p>}
    </div>
  )
}

function Dashboard() {
  const [donors, setDonors] = useState([])
  const [donations, setDonations] = useState([])
  const [campaigns, setCampaigns] = useState([])
  const [categories, setCategories] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(null)

  useEffect(() => {
    Promise.all([getDonors(), getDonations(), getCampaigns(), getCategories()])
      .then(([donorsRes, donationsRes, campaignsRes, categoriesRes]) => {
        setDonors(donorsRes.data)
        setDonations(donationsRes.data)
        setCampaigns(campaignsRes.data)
        setCategories(categoriesRes.data)
      })
      .catch(() => setError('No se pudo cargar el dashboard'))
      .finally(() => setLoading(false))
  }, [])

  if (loading) return <p>Cargando dashboard...</p>
  if (error) return <p className="text-red-600">{error}</p>

  // --- Métricas generales ---
  const totalDonors = donors.length
  const activeDonors = donors.filter((d) => d.isActive).length
  const totalDonated = donations
    .filter((d) => d.status === 'Completed')
    .reduce((sum, d) => sum + Number(d.amount), 0)
  const activeCampaigns = campaigns.filter((c) => c.status === 'Active').length
  const totalGoal = campaigns.reduce((sum, c) => sum + Number(c.goalAmount), 0)
  const totalCollected = campaigns.reduce((sum, c) => sum + Number(c.collectedAmount), 0)
  const overallProgress = totalGoal > 0 ? ((totalCollected / totalGoal) * 100).toFixed(1) : 0

  // --- Datos para gráfica de progreso por campaña ---
  const campaignChartData = campaigns.map((c) => ({
    name: c.name.length > 15 ? c.name.slice(0, 15) + '…' : c.name,
    Meta: Number(c.goalAmount),
    Recaudado: Number(c.collectedAmount),
  }))

  // --- Datos para gráfica de estado de donaciones (pie) ---
  const statusCounts = donations.reduce((acc, d) => {
    acc[d.status] = (acc[d.status] || 0) + 1
    return acc
  }, {})
  const statusChartData = Object.entries(statusCounts).map(([status, count]) => ({
    name: status,
    value: count,
  }))

  // --- Datos para gráfica de donaciones por mes ---
  const monthlyMap = {}
  donations.forEach((d) => {
    const date = new Date(d.donatedAt)
    const key = `${date.getFullYear()}-${String(date.getMonth() + 1).padStart(2, '0')}`
    monthlyMap[key] = (monthlyMap[key] || 0) + Number(d.amount)
  })
  const monthlyChartData = Object.entries(monthlyMap)
    .sort(([a], [b]) => a.localeCompare(b))
    .map(([month, total]) => ({ month, total }))

  return (
    <div>
      <h1 className="text-2xl font-bold mb-6">Dashboard</h1>

      {/* Tarjetas de totales */}
      <div className="grid gap-4 md:grid-cols-4 mb-8">
        <StatCard
          label="Donantes"
          value={totalDonors}
          sublabel={`${activeDonors} activos`}
        />
        <StatCard
          label="Total donado"
          value={`$${totalDonated.toLocaleString()}`}
          sublabel="Donaciones completadas"
          accent="text-green-700"
        />
        <StatCard
          label="Campañas activas"
          value={activeCampaigns}
          sublabel={`de ${campaigns.length} totales`}
        />
        <StatCard
          label="Progreso general"
          value={`${overallProgress}%`}
          sublabel={`$${totalCollected.toLocaleString()} de $${totalGoal.toLocaleString()}`}
          accent="text-blue-700"
        />
      </div>

      <div className="grid gap-6 lg:grid-cols-2 mb-6">
        {/* Progreso por campaña */}
        <div className="bg-white rounded-xl shadow-sm border p-5">
          <h2 className="font-semibold mb-4">Meta vs. recaudado por campaña</h2>
          <ResponsiveContainer width="100%" height={280}>
            <BarChart data={campaignChartData}>
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="name" tick={{ fontSize: 11 }} />
              <YAxis tick={{ fontSize: 11 }} />
              <Tooltip />
              <Legend />
              <Bar dataKey="Meta" fill="#93c5fd" radius={[4, 4, 0, 0]} />
              <Bar dataKey="Recaudado" fill="#1e3a8a" radius={[4, 4, 0, 0]} />
            </BarChart>
          </ResponsiveContainer>
        </div>

        {/* Estado de donaciones */}
        <div className="bg-white rounded-xl shadow-sm border p-5">
          <h2 className="font-semibold mb-4">Donaciones por estado</h2>
          <ResponsiveContainer width="100%" height={280}>
            <PieChart>
              <Pie
                data={statusChartData}
                dataKey="value"
                nameKey="name"
                cx="50%"
                cy="50%"
                outerRadius={90}
                label={(entry) => entry.name}
              >
                {statusChartData.map((entry, index) => (
                  <Cell
                    key={index}
                    fill={STATUS_COLORS[entry.name] || '#94a3b8'}
                  />
                ))}
              </Pie>
              <Tooltip />
              <Legend />
            </PieChart>
          </ResponsiveContainer>
        </div>
      </div>

      {/* Donaciones por mes */}
      <div className="bg-white rounded-xl shadow-sm border p-5">
        <h2 className="font-semibold mb-4">Monto donado por mes</h2>
        <ResponsiveContainer width="100%" height={260}>
          <LineChart data={monthlyChartData}>
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="month" tick={{ fontSize: 11 }} />
            <YAxis tick={{ fontSize: 11 }} />
            <Tooltip formatter={(value) => `$${value.toLocaleString()}`} />
            <Line
              type="monotone"
              dataKey="total"
              stroke="#1e3a8a"
              strokeWidth={2}
              dot={{ r: 4 }}
            />
          </LineChart>
        </ResponsiveContainer>
      </div>
    </div>
  )
}

export default Dashboard
