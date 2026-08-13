import { Outlet, NavLink } from 'react-router-dom'

const navItems = [
  { to: '/', label: 'Dashboard', icon: '◈', end: true },
  { to: '/donantes', label: 'Donantes', icon: '◐' },
  { to: '/donaciones', label: 'Donaciones', icon: '◆' },
  { to: '/campanas', label: 'Campañas', icon: '◉' },
  { to: '/categorias', label: 'Categorías', icon: '▤' },
]

function MainLayout() {
  return (
    <div className="min-h-screen flex bg-bone">
      <aside className="w-64 bg-forest text-bone flex flex-col shrink-0">
        <div className="px-6 py-6 border-b border-white/10">
          <h1 className="font-display text-2xl font-semibold tracking-tight">
            Donify
          </h1>
          <p className="text-xs text-sage mt-1 tracking-wide uppercase">
            Gestión de donaciones
          </p>
        </div>

        <nav className="flex-1 px-3 py-6 space-y-1">
          {navItems.map((item) => (
            <NavLink
              key={item.to}
              to={item.to}
              end={item.end}
              className={({ isActive }) =>
                `flex items-center gap-3 px-3 py-2.5 rounded-lg text-sm font-medium transition-colors ${
                  isActive
                    ? 'bg-gold text-forest-dark'
                    : 'text-bone/80 hover:bg-white/10 hover:text-bone'
                }`
              }
            >
              <span className="text-base">{item.icon}</span>
              {item.label}
            </NavLink>
          ))}
        </nav>

        <div className="px-6 py-4 border-t border-white/10 text-xs text-sage">
          InnovaCode © 2026
        </div>
      </aside>

      <main className="flex-1 p-8 overflow-y-auto">
        <Outlet />
      </main>
    </div>
  )
}

export default MainLayout
