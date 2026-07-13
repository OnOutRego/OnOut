import { Routes, Route, Link } from 'react-router-dom'
import Landing from './components/Landing'
import HasherProfile from './components/HasherProfile'
import './App.css'

function App() {
  return (
    <div className="min-h-screen">
      <nav className="p-4">
        <Link to="/" className="text-purple-600 font-semibold">OnOut</Link>
      </nav>

      <Routes>
        <Route path="/" element={<Landing />} />
        <Route path="/profile" element={<HasherProfile />} />
      </Routes>
    </div>
  )
}

export default App
