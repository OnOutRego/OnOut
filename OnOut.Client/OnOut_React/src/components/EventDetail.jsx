import { useEffect, useState } from 'react'
import { useAuth0 } from '@auth0/auth0-react'
import { useNavigate, useParams } from 'react-router-dom'
import locationIcon from '../assets/events-location.svg'
import kennelLogo from '../assets/events-kennel-logo.png'
import fallbackImage from '../assets/events-card-green.jpg'
import './EventDetail.css'

function displayPrice(value) {
  const amount = Number(value || 0)
  return amount > 0
    ? new Intl.NumberFormat(undefined, { style: 'currency', currency: 'USD', maximumFractionDigits: 0 }).format(amount)
    : 'Free'
}

export default function EventDetail() {
  const { eventId } = useParams()
  const navigate = useNavigate()
  const { getAccessTokenSilently } = useAuth0()
  const [event, setEvent] = useState(null)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')

  useEffect(() => {
    let cancelled = false

    async function loadEvent() {
      try {
        const token = await getAccessTokenSilently()
        const apiBase = import.meta.env.VITE_API_BASE_URL || ''
        const response = await fetch(`${apiBase}/api/events/${eventId}`, {
          headers: { Authorization: `Bearer ${token}` },
        })
        if (!response.ok) throw new Error(`Unable to load this event (${response.status}).`)
        const item = await response.json()
        if (!cancelled) setEvent(item)
      } catch (loadError) {
        if (!cancelled) setError(loadError.message || 'Unable to load this event.')
      } finally {
        if (!cancelled) setLoading(false)
      }
    }

    loadEvent()
    return () => { cancelled = true }
  }, [eventId, getAccessTokenSilently])

  const image = event?.bannerImage || event?.primaryImage
  const imageSource = image ? `data:image/jpeg;base64,${image}` : fallbackImage
  const location = event?.eventKennel?.city
    ? [event.eventKennel.city, event.eventKennel.state].filter(Boolean).join(', ')
    : event?.location?.name || 'Location to be announced'
  const date = event?.trailTime ? new Date(event.trailTime) : null

  return (
    <main className="event-detail-page">
      <header className="event-detail-header">
        <button aria-label="Back to events" className="event-detail-back" onClick={() => navigate('/events')} type="button">‹</button>
        <span>Event</span>
        <span className="event-detail-header-spacer" />
      </header>

      {loading ? (
        <p className="event-detail-state">Loading event…</p>
      ) : error ? (
        <p className="event-detail-state" role="alert">{error}</p>
      ) : event && (
        <>
          <div className="event-detail-hero">
            <img src={imageSource} alt="" />
            {date && !Number.isNaN(date.getTime()) && (
              <time dateTime={date.toISOString()} className="event-detail-date">
                <span>{date.toLocaleString(undefined, { month: 'short' })}</span>
                <strong>{date.getDate()}</strong>
              </time>
            )}
          </div>

          <article className="event-detail-content">
            <header className="event-detail-title-row">
              <div>
                <h1>{event.name}</h1>
                <div className="event-detail-meta">
                  <span><img src={kennelLogo} alt="" />{event.eventKennel?.name || 'On Out event'}</span>
                  <span><img src={locationIcon} alt="" />{location}</span>
                </div>
              </div>
              <div className="event-detail-price">
                <span>{displayPrice(event.regoPrice)}</span>
                <small>{event.regoDeadline ? `Rego closes ${new Date(event.regoDeadline).toLocaleDateString()}` : 'Registration details'}</small>
              </div>
            </header>

            <section className="event-detail-description">
              <h2>Description</h2>
              <p>{event.description || 'No description has been added yet.'}</p>
            </section>

            <dl className="event-detail-facts">
              <div><dt>Event type</dt><dd>{event.eventType?.name || 'Event'}</dd></div>
              {date && !Number.isNaN(date.getTime()) && <div><dt>Starts</dt><dd>{date.toLocaleString()}</dd></div>}
              {event.maxRegos > 0 && <div><dt>Registration cap</dt><dd>{event.maxRegos}</dd></div>}
            </dl>
          </article>

          <footer className="event-detail-actions">
            <p>Online registration isn’t available yet.</p>
            <button disabled type="button">Rego</button>
          </footer>
        </>
      )}
    </main>
  )
}