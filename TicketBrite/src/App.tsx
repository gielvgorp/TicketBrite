import { Route, Routes } from 'react-router-dom'
import './App.css'
import NavigationBar from './components/NavigationBar/NavigationBar'
import Index from './pages'
import Events from './pages/Events'
import EventInfo from './pages/EventInfo'
import Authentication from './pages/Authentication'
import { useState } from 'react'
import ShoppingCart from './pages/ShoppingCart'

function App() {
  const [showNav, setShowNav] = useState(true);
  return (
    <>
     { showNav && <NavigationBar /> }
     <Routes>
      <Route path="/" element={<Index />} />
      <Route path="/Events" element={<Events />} />
      <Route path="/Events/:id" element={<Events />} />
      <Route path="/Event/:id" element={<EventInfo />} />
      <Route path="/Shopping-cart" element={<ShoppingCart />} />
        {/* Route zonder :id parameter */}
      <Route path="/Authenticatie" element={<Authentication setShowNav={(value) => setShowNav(value)} />} />

    {/* Route met :id parameter */}
    <Route path="/Authenticatie/:id" element={<Authentication setShowNav={(value) => setShowNav(value)} />} />
    </Routes>
    </>
  )
}

export default App
