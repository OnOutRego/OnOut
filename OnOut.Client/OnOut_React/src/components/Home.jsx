import { useEffect, useState } from 'react'
import { useAuth0 } from '@auth0/auth0-react'
import { useNavigate } from 'react-router-dom'
import brandLogo from '../assets/home-brand.svg'
import notificationIcon from '../assets/home-notification.svg'
import eventLocationIcon from '../assets/home-location.svg'
import homeIcon from '../assets/home-nav-home.svg'
import eventsIcon from '../assets/home-nav-events.svg'
import kennelsIcon from '../assets/home-nav-kennels.svg'
import profileIcon from '../assets/home-nav-profile.svg'
import redEventFallback from '../assets/home-event-red.jpg'
import blueEventFallback from '../assets/home-event-blue.jpg'
import kennelFallback from '../assets/home-kennel.jpg'
import redEventMark from '../assets/home-event-mark-red.svg'
import blueEventMark from '../assets/home-event-mark-blue.svg'
import kennelLogo from '../assets/home-kennel-logo.png'
import './Home.css'

function getImageSource(image, fallback) {
  if (!image) return fallback
  if (typeof image === 'string' && image.length > 0) {
    return image.startsWith('data:') ? image : `data:image/jpeg;base64,${image}`
  }
  return fallback
}

function locationScore(item, profile, event = false) {
  const city = profile.homeCity?.trim().toLocaleLowerCase()
  const state = profile.homeState?.trim().toLocaleLowerCase()
  const country = profile.homeCountry?.trim().toLocaleLowerCase()
  const location = event ? item.location?.name : ''
  const kennel = event ? item.eventKennel : item
  const values = [location, kennel?.city, kennel?.state, kennel?.country]
    .filter(Boolean)
    .join(' ')
    .toLocaleLowerCase()

  if (!city && !state && !country) return 1
  if (city && values.includes(city)) return 3
  if (state && values.includes(state)) return 2
  if (country && values.includes(country)) return 1
  return 0
}

function EventCard({ event, index }) {
  const date = event.trailTime ? new Date(event.trailTime) : null
  const hasValidDate = date && !Number.isNaN(date.getTime())
  const fallback = index % 2 === 0 ? redEventFallback : blueEventFallback
  const image = getImageSource(event.bannerImage || event.primaryImage, fallback)
  const locationName = event.eventKennel?.city
    ? `${event.eventKennel.city}${event.eventKennel.state ? `, ${event.eventKennel.state}` : ''}`
    : event.location?.name || 'Location to be announced'
  const kennelName = event.eventKennel?.name || 'On Out event'
  const price = Number(event.regoPrice) > 0
    ? new Intl.NumberFormat(undefined, { style: 'currency', currency: 'USD', maximumFractionDigits: 0 }).format(event.regoPrice)
    : 'Free'

  return (
    <article className={`home-event-card ${index % 2 === 0 ? 'home-event-card-red' : 'home-event-card-blue'}`}>
      <img className="home-event-photo" src={image} alt="" />
      <div className="home-event-tint" />
      <img className="home-event-mark" src={index % 2 === 0 ? redEventMark : blueEventMark} alt="" />
      <div className="home-event-info">
        <h3>{event.name}</h3>
        <div className="home-event-meta">
          <span className="home-event-kennel">
            <img src={kennelLogo} alt="" />
            <span>{kennelName}</span>
          </span>
          <span className="home-event-location">
            <img src={eventLocationIcon} alt="" />
            <span>{locationName}</span>
          </span>
        </div>
      </div>
      <div className="home-event-date-price">
        <div className="home-date-badge">
          <span>{hasValidDate ? date.toLocaleString(undefined, { month: 'short' }) : 'TBD'}</span>
          <strong>{hasValidDate ? date.getDate() : '--'}</strong>
        </div>
        <strong className="home-event-price">{price}</strong>
      </div>
    </article>
  )
}

function KennelCard({ kennel }) {
  const image = getImageSource(kennel.bannerImage || kennel.primaryImage, kennelFallback)
  const location = [kennel.city, kennel.state].filter(Boolean).join(', ')
  const type = ['Fixed', 'Travel', 'Virtual'][kennel.type] || 'Kennel'

  return (
    <article className="home-kennel-card">
      <img className="home-kennel-photo" src={image} alt="" />
      <div className="home-kennel-shade" />
      <div className="home-kennel-info">
        <h3>{kennel.name}</h3>
        <p className="home-kennel-location">
          <img src={eventLocationIcon} alt="" />
          <span>{location || kennel.country || 'Location not listed'}</span>
        </p>
        <div className="home-kennel-tags">
          <span>{type}</span>
          {kennel.childFriendly && <span>Family friendly</span>}
        </div>
        <span className="home-kennel-members">{Number(kennel.numMembers || 0)}+ members</span>
      </div>
    </article>
  )
}

export default function Home({ profile }) {
  const { getAccessTokenSilently } = useAuth0()
  const navigate = useNavigate()
  const [events, setEvents] = useState([])
  const [kennels, setKennels] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')

  useEffect(() => {
    let cancelled = false

    async function loadHomeData() {
      setLoading(true)
      setError('')

      try {
        const token = await getAccessTokenSilently()
        const apiBase = import.meta.env.VITE_API_BASE_URL || ''
        const headers = { Authorization: `Bearer ${token}` }
        const [eventsResponse, kennelsResponse] = await Promise.all([
          fetch(`${apiBase}/api/events`, { headers }),
          fetch(`${apiBase}/api/kennels`, { headers }),
        ])

        if (!eventsResponse.ok) throw new Error(`Unable to load events (${eventsResponse.status}).`)
        if (!kennelsResponse.ok) throw new Error(`Unable to load kennels (${kennelsResponse.status}).`)

        const [eventItems, kennelItems] = await Promise.all([
          eventsResponse.json(),
          kennelsResponse.json(),
        ])

        if (!cancelled) {
          setEvents(eventItems)
          setKennels(kennelItems)
        }
      } catch (loadError) {
        if (!cancelled) setError(loadError.message || 'Unable to load your home feed.')
      } finally {
        if (!cancelled) setLoading(false)
      }
    }

    loadHomeData()
    return () => { cancelled = true }
  }, [getAccessTokenSilently])

  const now = Date.now()
  const upcomingEvents = events
    .filter(event => {
      const timestamp = new Date(event.trailTime).getTime()
      return Number.isFinite(timestamp) && timestamp >= now && locationScore(event, profile, true) > 0
    })
    .sort((left, right) => new Date(left.trailTime) - new Date(right.trailTime))
  const suggestedKennels = kennels
    .map(kennel => ({ kennel, score: locationScore(kennel, profile) }))
    .filter(item => item.score > 0)
    .sort((left, right) => right.score - left.score || (right.kennel.numMembers || 0) - (left.kennel.numMembers || 0))
    .map(item => item.kennel)

  function navigateToSection(section) {
    if (section === 'events') {
      navigate('/events')
      return
    }
    if (section === 'profile') {
      navigate('/profile')
      return
    }
    if (section === 'home') {
      window.scrollTo({ top: 0, behavior: 'smooth' })
      return
    }
    document.getElementById(`home-${section}`)?.scrollIntoView({ behavior: 'smooth', block: 'start' })
  }

  return (
    <div className="home-page">
      <header className="home-header">
        <div className="home-header-topline" aria-hidden="true">
          <span>9:30</span>
          <span className="home-status-notch" />
          <span className="home-status-signals"><i /><i /></span>
        </div>
        <div className="home-header-main">
          <img className="home-brand-logo" src={brandLogo} alt="On Out" />
          <img className="home-notification-icon" src={notificationIcon} alt="" />
        </div>
      </header>

      <main className="home-content">
        <section className="home-section" id="home-events" aria-labelledby="upcoming-events-heading">
          <h2 id="upcoming-events-heading">Upcoming Events</h2>
          {loading ? (
            <p className="home-state">Loading events…</p>
          ) : error ? (
            <p className="home-state" role="alert">{error}</p>
          ) : upcomingEvents.length ? (
            <div className="home-horizontal-rail" aria-label="Upcoming events near your location">
              {upcomingEvents.map((event, index) => <EventCard key={event.id} event={event} index={index} />)}
            </div>
          ) : (
            <p className="home-state">No upcoming events match your saved location.</p>
          )}
        </section>

        <section className="home-section" id="home-kennels" aria-labelledby="suggested-kennels-heading">
          <h2 id="suggested-kennels-heading">Suggested Kennels</h2>
          {loading ? (
            <p className="home-state">Loading kennels…</p>
          ) : error ? (
            <p className="home-state" role="alert">{error}</p>
          ) : suggestedKennels.length ? (
            <div className="home-horizontal-rail home-kennel-rail" aria-label="Kennels near your location">
              {suggestedKennels.map(kennel => <KennelCard key={kennel.id} kennel={kennel} />)}
            </div>
          ) : (
            <p className="home-state">No nearby kennels match your saved location.</p>
          )}
        </section>
      </main>

      <nav className="home-bottom-nav" aria-label="Main navigation">
        {[
          ['home', 'Home', homeIcon],
          ['events', 'Events', eventsIcon],
          ['kennels', 'Kennels', kennelsIcon],
          ['profile', 'Profile', profileIcon],
        ].map(([section, label, icon]) => (
          <button
            aria-current={section === 'home' ? 'page' : undefined}
            className={`home-nav-item ${section === 'home' ? 'is-active' : ''}`}
            key={section}
            onClick={() => navigateToSection(section)}
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