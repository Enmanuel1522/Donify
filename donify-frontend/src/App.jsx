import { BrowserRouter, Routes, Route } from 'react-router-dom'
import MainLayout from './layouts/MainLayout'
import Donors from './pages/Donors'
import Donations from './pages/Donations'
import Campaigns from './pages/Campaigns'
import Categories from './pages/Categories'
import Dashboard from './pages/Dashboard'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<MainLayout />}>
          <Route path="/" element={<Dashboard />} />
          <Route path="/donantes" element={<Donors />} />
          <Route path="/donaciones" element={<Donations />} />
          <Route path="/campanas" element={<Campaigns />} />
          <Route path="/categorias" element={<Categories />} />
        </Route>
      </Routes>
    </BrowserRouter>
  )
}

export default App



