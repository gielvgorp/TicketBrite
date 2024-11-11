import { Route, Routes } from 'react-router-dom'
import './App.css'
import NavigationBar from './components/NavigationBar/NavigationBar'
import Index from './pages'
import Events from './pages/Events'
import EventInfo from './pages/EventInfo'
import Authentication from './pages/Authentication'
import { useState } from 'react'
import ShoppingCart from './pages/ShoppingCart'
import Profile from './pages/Profile'
import ProtectedRoute from './pages/ProtectedRoute'
import { AuthProvider } from './AuthContext'
import DashboardPage from './pages/Dashboard'
import PaymentSuccess from './pages/PaymentSuccess'

function App() {
  const [showNav, setShowNav] = useState(true);
  return (
    <AuthProvider>
     { showNav && <NavigationBar /> }
     <Routes>
      <Route path="/" element={<Index />} />
      <Route path="/Events" element={<Events />} />
      <Route path="/Events/:id" element={<Events />} />
      <Route path="/Event/:id" element={<EventInfo />} />
      <Route path="/Shopping-cart" element={<ShoppingCart />} />
      <Route path="/Payment-success" element={<PaymentSuccess tickets={[]} purchaseId=''  />} />
      <Route element={<ProtectedRoute />}>
        <Route path="/Profile" element={<Profile />} />
        <Route path="/Organisatie/Dashboard/:eventId" element={<DashboardPage />} />
      </Route>
      
        {/* Route zonder :id parameter */}
      <Route path="/Authenticatie" element={<Authentication setShowNav={(value) => setShowNav(value)} />} />

    {/* Route met :id parameter */}
    <Route path="/Authenticatie/:id" element={<Authentication setShowNav={(value) => setShowNav(value)} />} />
    </Routes>
    </AuthProvider>
  )
}

export default App
