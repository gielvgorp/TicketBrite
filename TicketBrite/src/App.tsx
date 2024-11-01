import { Route, Routes } from 'react-router-dom'
import './App.css'
import NavigationBar from './components/NavigationBar/NavigationBar'
import Index from './pages'
import Events from './pages/Events'
import EventInfo from './pages/EventInfo'
import Authentication from './pages/Authentication'
import { useState } from 'react'
import Profile from './pages/Profile'
import ProtectedRoute from './pages/ProtectedRoute'
import { AuthProvider } from './AuthContext'

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
      <Route element={<ProtectedRoute />}>
        <Route path="/Profile" element={<Profile />} />
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
