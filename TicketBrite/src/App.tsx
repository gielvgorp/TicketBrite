import { Route, Routes } from 'react-router-dom'
import './App.css'
import NavigationBar from './components/NavigationBar/NavigationBar'
import Index from './pages'
import Events from './pages/Events'

function App() {
  return (
    <>
     <NavigationBar />
     <Routes>
      <Route path="/" element={<Index />} />
      <Route path="/Events" element={<Events />} />
    </Routes>
    </>
  )
}

export default App
