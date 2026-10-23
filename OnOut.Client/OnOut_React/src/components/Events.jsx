import { useEffect, useMemo, useState } from 'react'
import { useAuth0 } from '@auth0/auth0-react'
import { useNavigate } from 'react-router-dom'
import searchIcon from '../assets/events-search.svg'
import filterIcon from '../assets/events-filter.svg'
import plusIcon from '../assets/events-plus.svg'
import locationIcon from '../assets/events-location.svg'
import kennelLogo from '../assets/events-kennel-logo.png'
import homeIcon from '../assets/home-nav-home.svg'
import eventsIcon from '../assets/home-nav-events.svg'
import kennelsIcon from '../assets/home-nav-kennels.svg'
import profileIcon from '../assets/home-nav-profile.svg'
import orangeCard from '../assets/events-card-orange.jpg'
import greenCard from '../assets/events-card-green.jpg'
import redCard from '../assets/events-card-red.jpg'
import blueCard from '../assets/events-card-blue.jpg'
import './Events.css'

const cardPhotos = [orangeCard, greenCard, redCard, blueCard]

function formatPrice(value) {
  const price = Number(value || 0)
  return price > 0
    ? new Intl.NumberFormat(undefined, { style: 'currency', currency: 'USD', maximumFractionDigits: 0 }).format(price)
    : 'Free'
}

function getEventLocation(event) {
  const kennel = event.eventKennel
  if (kennel?.city) return [kennel.city, kennel.state].filter(Boolean).join(', ')
  return event.location?.name || 'Location to be announced'
}

function EventCard({ event, index, onSelect }) {
  const date = event.trailTime ? new Date(event.trailTime) : null
  const validDate = date && !Number.isNaN(date.getTime())
  const image = event.bannerImage || event.primaryImage
  const imageSource = image ? `data:image/jpeg;base64,${image}` : cardPhotos[index % cardPhotos.length]
  const kennel = event.eventKennel?.name || 'On Out event'

  return (
    <button className={`events-card events-card-${index % cardPhotos.length}`} onClick={onSelect} type="button">
      <img className="events-card-photo" src={imageSource} alt="" />
      <div className="events-card-tint" />
      <div className="events-card-copy">
        <h2>{event.name}</h2>
        <div className="events-card-meta">
          <span>
            <img className="events-kennel-logo" src={kennelLogo} alt="" />
            {kennel}
          </span>
          <span>
            <img className="events-location-icon" src={locationIcon} alt="" />
            {getEventLocation(event)}
          </span>
        </div>
      </div>
      <div className="events-card-price-date">
        <time className="events-date-badge" dateTime={validDate ? date.toISOString() : undefined}>
          <span>{validDate ? date.toLocaleString(undefined, { month: 'short' }) : 'TBD'}</span>
          <strong>{validDate ? date.getDate() : '--'}</strong>
        </time>
        <strong>{formatPrice(event.regoPrice)}</strong>
      </div>
    </button>
  )
}

export default function Events() {
  const { getAccessTokenSilently } = useAuth0()
  const navigate = useNavigate()
  const [events, setEvents] = useState([])
  const [search, setSearch] = useState('')
  const [selectedType, setSelectedType] = useState('All')
  const [view, setView] = useState('list')
  const [showFilters, setShowFilters] = useState(false)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')

  useEffect(() => {
    let cancelled = false

    async function loadEvents() {
      setLoading(true)
      setError('')
      try {
        const token = await getAccessTokenSilently()
        const apiBase = import.meta.env.VITE_API_BASE_URL || ''
        const response = await fetch(`${apiBase}/api/events`, {
          headers: { Authorization: `Bearer ${token}` },
        })
        if (!response.ok) throw new Error(`Unable to load events (${response.status}).`)
        const data = await response.json()
        if (!cancelled) setEvents(data)
      } catch (loadError) {
        if (!cancelled) setError(loadError.message || 'Unable to load events.')
      } finally {
        if (!cancelled) setLoading(false)
      }
    }

    loadEvents()
    return () => { cancelled = true }
  }, [getAccessTokenSilently])

  const eventTypes = useMemo(() => [
    'All',
    ...new Set(events.map(event => event.eventType?.name).filter(Boolean)),
  ], [events])

  const filteredEvents = useMemo(() => {
    const searchTerm = search.trim().toLocaleLowerCase()
    return [...events]
      .filter(event => selectedType === 'All' || event.eventType?.name === selectedType)
      .filter(event => {
        if (!searchTerm) return true
        return [event.name, event.description, event.eventType?.name, event.eventKennel?.name, getEventLocation(event)]
          .filter(Boolean)
          .some(value => value.toLocaleLowerCase().includes(searchTerm))
      })
      .sort((left, right) => new Date(left.trailTime) - new Date(right.trailTime))
  }, [events, search, selectedType])

  function navigateTo(section) {
    if (section === 'events') return
    if (section === 'profile') navigate('/profile')
    else navigate('/')
  }

  return (
    <div className="events-page">
      <header className="events-header">
        <div className="events-status-bar" aria-hidden="true">
          <span>9:30</span><span className="events-notch" /><span className="events-status-icons">◆ ▰</span>
        </div>
        <div className="events-search-row">
          <label className="events-search">
            <span className="sr-only">Search events</span>
            <input value={search} onChange={event => setSearch(event.target.value)} placeholder="Search" />
            <img src={searchIcon} alt="" />
          </label>
          <button
            aria-expanded={showFilters}
            aria-label="Filter events"
            className={`events-filter-button ${showFilters ? 'is-open' : ''}`}
            onClick={() => setShowFilters(value => !value)}
            type="button"
          >
            <img src={filterIcon} alt="" />
          </button>
        </div>
        <div className="events-view-tabs" role="tablist" aria-label="Event view">
          <button aria-selected={view === 'list'} onClick={() => setView('list')} role="tab" type="button">List</button>
          <button aria-selected={view === 'map'} onClick={() => setView('map')} role="tab" type="button">Map</button>
        </div>
      </header>

      {showFilters && (
        <div className="events-filter-panel">
          <label htmlFor="event-type-filter">Event type</label>
          <select id="event-type-filter" value={selectedType} onChange={event => setSelectedType(event.target.value)}>
            {eventTypes.map(type => <option key={type} value={type}>{type}</option>)}
          </select>
          <button className="events-clear-filter" onClick={() => setSelectedType('All')} type="button">Clear</button>
        </div>
      )}

      <main className="events-content">
        {loading ? (
          <p className="events-state">Loading events…</p>
        ) : error ? (
          <p className="events-state" role="alert">{error}</p>
        ) : view === 'map' ? (
          <div className="events-map-empty">
            <h1>Map view</h1>
            <p>Events don’t have map coordinates yet. The current location field is text-only.</p>
            <button onClick={() => setView('list')} type="button">Return to list</button>
          </div>
        ) : filteredEvents.length ? (
          <div className="events-list" aria-label="Events">
            {filteredEvents.map((event, index) => (
              <EventCard key={event.id} event={event} index={index} onSelect={() => navigate(`/events/${event.id}`)} />
            ))}
          </div>
        ) : (
          <div className="events-state">
            <p>{search || selectedType !== 'All' ? 'No events match your search and filters.' : 'No events yet.'}</p>
            <button onClick={() => navigate('/events/create')} type="button">Create an event</button>
          </div>
        )}
      </main>

      <button
        aria-label="Create event"
        className="events-create-button"
        onClick={() => navigate('/events/create')}
        type="button"
      >
        <img src={plusIcon} alt="" />
      </button>

      <nav className="home-bottom-nav events-bottom-nav" aria-label="Main navigation">
        {[
          ['home', 'Home', homeIcon],
          ['events', 'Events', eventsIcon],
          ['kennels', 'Kennels', kennelsIcon],
          ['profile', 'Profile', profileIcon],
        ].map(([section, label, icon]) => (
          <button
            aria-current={section === 'events' ? 'page' : undefined}
            className={`home-nav-item ${section === 'events' ? 'is-active' : ''}`}
            key={section}
            onClick={() => navigateTo(section)}
            type="button"
          >
            <img src={icon} alt="" />
            <span>{label}</span>
          </button>
        ))}
      </nav>
    </div>
  )
}