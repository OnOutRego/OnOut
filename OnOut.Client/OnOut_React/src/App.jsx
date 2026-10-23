import { Routes, Route } from 'react-router-dom'
import Landing from './components/Landing'
import HasherProfile from './components/HasherProfile'
import Events from './components/Events'
import EventCreate from './components/EventCreate'
import EventDetail from './components/EventDetail'
import './App.css'

function App() {
  return (
    <div className="min-h-screen">
      <Routes>
        <Route path="/" element={<Landing />} />
        <Route path="/profile" element={<HasherProfile />} />
        <Route path="/events" element={<Events />} />
        <Route path="/events/create" element={<EventCreate />} />
        <Route path="/events/:eventId" element={<EventDetail />} />
      </Routes>
    </div>
  )
}

export default App
