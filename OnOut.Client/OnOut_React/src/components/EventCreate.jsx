import { useEffect, useState } from 'react'
import { useAuth0 } from '@auth0/auth0-react'
import { useNavigate } from 'react-router-dom'
import trailIcon from '../assets/events-type-trail.svg'
import dressRunIcon from '../assets/events-type-dress-run.svg'
import campoutIcon from '../assets/events-type-campout.svg'
import pubcrawlIcon from '../assets/events-type-pubcrawl.svg'
import hotelIcon from '../assets/events-type-hotel.svg'
import './EventCreate.css'

const eventTypes = [
  { name: 'Trail', icon: trailIcon },
  { name: 'Dress Run', icon: dressRunIcon },
  { name: 'Campout', icon: campoutIcon },
  { name: 'Pubcrawl', icon: pubcrawlIcon },
  { name: 'Hotel', icon: hotelIcon },
]

const stepNames = ['Overview', 'Rego', 'Pricing', 'Review']

function localDateTimeAfter(days) {
  const date = new Date(Date.now() + days * 24 * 60 * 60 * 1000)
  date.setMinutes(date.getMinutes() - date.getTimezoneOffset())
  return date.toISOString().slice(0, 16)
}

function toIsoDate(value) {
  return new Date(value).toISOString()
}

function InputField({ label, children, className = '' }) {
  return (
    <label className={`event-form-field ${className}`}>
      <span>{label}</span>
      {children}
    </label>
  )
}

export default function EventCreate() {
  const { getAccessTokenSilently } = useAuth0()
  const navigate = useNavigate()
  const [kennels, setKennels] = useState([])
  const [step, setStep] = useState(0)
  const [showIntro, setShowIntro] = useState(true)
  const [loadingKennels, setLoadingKennels] = useState(true)
  const [saving, setSaving] = useState(false)
  const [error, setError] = useState('')
  const [bannerPreview, setBannerPreview] = useState('')
  const [form, setForm] = useState({
    name: '',
    eventTypeName: 'Trail',
    kennelId: '',
    description: '',
    locationName: '',
    trailTime: localDateTimeAfter(7),
    maxRegos: '',
    regoDeadline: localDateTimeAfter(6),
    regoPrice: '0',
    hasEarlyRego: false,
    earlyRegoPrice: '',
    earlyRegoDeadline: localDateTimeAfter(5),
    hasGimmes: false,
    gimmes: '',
    bannerImage: '',
  })

  useEffect(() => {
    let cancelled = false

    async function loadKennels() {
      try {
        const token = await getAccessTokenSilently()
        const apiBase = import.meta.env.VITE_API_BASE_URL || ''
        const response = await fetch(`${apiBase}/api/kennels`, {
          headers: { Authorization: `Bearer ${token}` },
        })
        if (!response.ok) throw new Error(`Unable to load kennels (${response.status}).`)
        const data = await response.json()
        if (!cancelled) setKennels(data)
      } catch (loadError) {
        if (!cancelled) setError(loadError.message || 'Unable to load hosting kennels.')
      } finally {
        if (!cancelled) setLoadingKennels(false)
      }
    }

    loadKennels()
    return () => { cancelled = true }
  }, [getAccessTokenSilently])

  function updateField(event) {
    const { name, value, type, checked } = event.target
    setForm(current => ({ ...current, [name]: type === 'checkbox' ? checked : value }))
  }

  function validateCurrentStep() {
    if (step === 0) {
      if (!form.name.trim() || !form.eventTypeName || !form.locationName.trim() || !form.trailTime) {
        setError('Add an event name, type, start date, and location to continue.')
        return false
      }
      if (new Date(form.trailTime) <= new Date()) {
        setError('The event start must be in the future.')
        return false
      }
    }
    if (step === 1) {
      if (!form.regoDeadline || new Date(form.regoDeadline) > new Date(form.trailTime)) {
        setError('Registration must close on or before the event starts.')
        return false
      }
    }
    if (step === 2) {
      if (Number(form.regoPrice) < 0 || (form.hasEarlyRego && Number(form.earlyRegoPrice) < 0)) {
        setError('Prices cannot be negative.')
        return false
      }
    }
    setError('')
    return true
  }

  function nextStep() {
    if (validateCurrentStep()) setStep(current => Math.min(current + 1, stepNames.length - 1))
  }

  async function onBannerChange(event) {
    const file = event.target.files?.[0]
    if (!file) return
    if (!file.type.startsWith('image/')) {
      setError('Choose an image file for the event banner.')
      return
    }
    if (file.size > 5 * 1024 * 1024) {
      setError('Event banner images must be smaller than 5 MB.')
      return
    }

    const dataUrl = await new Promise((resolve, reject) => {
      const reader = new FileReader()
      reader.onload = () => resolve(reader.result)
      reader.onerror = () => reject(new Error('Could not read the selected image.'))
      reader.readAsDataURL(file)
    }).catch(readError => {
      setError(readError.message)
      return ''
    })

    if (dataUrl) {
      setBannerPreview(dataUrl)
      setForm(current => ({ ...current, bannerImage: dataUrl.split(',')[1] || '' }))
      setError('')
    }
  }

  async function onSubmit(event) {
    event.preventDefault()
    if (!validateCurrentStep()) return

    setSaving(true)
    setError('')
    try {
      const token = await getAccessTokenSilently()
      const apiBase = import.meta.env.VITE_API_BASE_URL || ''
      const deadline = toIsoDate(form.regoDeadline)
      const payload = {
        name: form.name.trim(),
        eventTypeName: form.eventTypeName,
        description: form.description.trim(),
        locationName: form.locationName.trim(),
        trailTime: toIsoDate(form.trailTime),
        kennelId: form.kennelId || null,
        maxRegos: Number(form.maxRegos || 0),
        hasGimmes: form.hasGimmes,
        gimmes: form.hasGimmes
          ? form.gimmes.split(/[\n,]/).map(item => item.trim()).filter(Boolean)
          : [],
        hasEarlyRego: form.hasEarlyRego,
        earlyRegoDeadline: form.hasEarlyRego ? toIsoDate(form.earlyRegoDeadline) : deadline,
        earlyRegoPrice: form.hasEarlyRego ? Number(form.earlyRegoPrice || 0) : 0,
        regoPrice: Number(form.regoPrice || 0),
        regoDeadline: deadline,
        primaryImage: form.bannerImage,
        bannerImage: form.bannerImage,
      }
      const response = await fetch(`${apiBase}/api/events`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(payload),
      })

      if (!response.ok) {
        const responseText = await response.text()
        try {
          const problem = JSON.parse(responseText)
          throw new Error(Object.values(problem.errors || {}).flat().join(' ') || problem.title || `Could not create event (${response.status}).`)
        } catch (parseError) {
          if (parseError instanceof SyntaxError) throw new Error(responseText || `Could not create event (${response.status}).`)
          throw parseError
        }
      }

      navigate('/events', { replace: true })
    } catch (submitError) {
      setError(submitError.message || 'Could not create event.')
      setStep(3)
    } finally {
      setSaving(false)
    }
  }

  return (
    <main className="event-create-page">
      <header className="event-create-status" aria-hidden="true">
        <span>9:30</span><span className="event-create-notch" /><span>◆ ▰</span>
      </header>
      <div className="event-create-progress" aria-label={`Step ${step + 1} of 4`}>
        {stepNames.map((name, index) => (
          <div className={`event-step-indicator ${index <= step ? 'is-current' : ''}`} key={name}>
            <span>{index + 1}</span>
            <small>{name}</small>
          </div>
        ))}
      </div>

      <form className="event-create-form" onSubmit={onSubmit}>
        {step === 0 && (
          <section className="event-form-panel">
            <h1>Overview</h1>
            <label className={`event-banner-upload ${bannerPreview ? 'has-image' : ''}`}>
              {bannerPreview && <img className="event-banner-preview" src={bannerPreview} alt="Selected event banner preview" />}
              <span className="event-upload-icon" aria-hidden="true">↥</span>
              <strong>{bannerPreview ? 'Change Banner Image' : 'Upload Banner Images'}</strong>
              <span className="event-upload-button">Browse Files</span>
              <input accept="image/*" onChange={onBannerChange} type="file" />
            </label>

            <InputField label="Event Name*">
              <input autoComplete="off" maxLength={120} name="name" onChange={updateField} placeholder="Name" required value={form.name} />
            </InputField>

            <fieldset className="event-type-fieldset">
              <legend>Event Type*</legend>
              <div className="event-type-grid">
                {eventTypes.map(type => (
                  <button
                    aria-pressed={form.eventTypeName === type.name}
                    className={`event-type-option ${form.eventTypeName === type.name ? 'is-selected' : ''}`}
                    key={type.name}
                    onClick={() => setForm(current => ({ ...current, eventTypeName: type.name }))}
                    type="button"
                  >
                    <img src={type.icon} alt="" />
                    <span>{type.name}</span>
                  </button>
                ))}
              </div>
            </fieldset>

            <InputField label="Hosting Kennel">
              <select disabled={loadingKennels} name="kennelId" onChange={updateField} value={form.kennelId}>
                <option value="">{loadingKennels ? 'Loading kennels…' : 'No hosting kennel'}</option>
                {kennels.map(kennel => <option key={kennel.id} value={kennel.id}>{kennel.name}</option>)}
              </select>
              {!loadingKennels && !kennels.length && <small>You can create this event without a hosting kennel.</small>}
            </InputField>

            <InputField label="Description">
              <textarea maxLength={5000} name="description" onChange={updateField} placeholder="Tell people about your event" rows={4} value={form.description} />
            </InputField>

            <InputField label="Location*">
              <input autoComplete="street-address" maxLength={240} name="locationName" onChange={updateField} placeholder="City, State or address" required value={form.locationName} />
            </InputField>

            <InputField label="Start Date / Time*">
              <input name="trailTime" onChange={updateField} required type="datetime-local" value={form.trailTime} />
            </InputField>
          </section>
        )}

        {step === 1 && (
          <section className="event-form-panel">
            <h1>Rego Settings</h1>
            <InputField label="Rego Deadline*">
              <input max={form.trailTime} name="regoDeadline" onChange={updateField} required type="datetime-local" value={form.regoDeadline} />
            </InputField>
            <InputField label="Rego Cap">
              <input min="0" name="maxRegos" onChange={updateField} placeholder="Unlimited" type="number" value={form.maxRegos} />
            </InputField>
            <label className="event-toggle-row">
              <span>Enable early registration</span>
              <input checked={form.hasEarlyRego} name="hasEarlyRego" onChange={updateField} type="checkbox" />
            </label>
            <label className="event-toggle-row">
              <span>Include gimme options</span>
              <input checked={form.hasGimmes} name="hasGimmes" onChange={updateField} type="checkbox" />
            </label>
            {form.hasGimmes && (
              <InputField label="Gimme Options">
                <textarea name="gimmes" onChange={updateField} placeholder="One option per line" rows={4} value={form.gimmes} />
              </InputField>
            )}
          </section>
        )}

        {step === 2 && (
          <section className="event-form-panel">
            <h1>Pricing</h1>
            <InputField label="Price">
              <input min="0" name="regoPrice" onChange={updateField} step="0.01" type="number" value={form.regoPrice} />
            </InputField>
            {form.hasEarlyRego && (
              <div className="event-form-subpanel">
                <h2>Early Registration</h2>
                <InputField label="Early Price">
                  <input min="0" name="earlyRegoPrice" onChange={updateField} step="0.01" type="number" value={form.earlyRegoPrice} />
                </InputField>
                <InputField label="Early Price Deadline">
                  <input max={form.regoDeadline} name="earlyRegoDeadline" onChange={updateField} type="datetime-local" value={form.earlyRegoDeadline} />
                </InputField>
              </div>
            )}
          </section>
        )}

        {step === 3 && (
          <section className="event-form-panel event-review-panel">
            <h1>Review Event</h1>
            {error && <p className="event-form-error" role="alert">{error}</p>}
            <dl>
              <div><dt>Name</dt><dd>{form.name || 'Not set'}</dd></div>
              <div><dt>Event type</dt><dd>{form.eventTypeName}</dd></div>
              <div><dt>Hosting kennel</dt><dd>{kennels.find(kennel => kennel.id === form.kennelId)?.name || 'None'}</dd></div>
              <div><dt>When</dt><dd>{form.trailTime ? new Date(form.trailTime).toLocaleString() : 'Not set'}</dd></div>
              <div><dt>Where</dt><dd>{form.locationName || 'Not set'}</dd></div>
              <div><dt>Registration deadline</dt><dd>{form.regoDeadline ? new Date(form.regoDeadline).toLocaleString() : 'Not set'}</dd></div>
              <div><dt>Price</dt><dd>{Number(form.regoPrice) > 0 ? `$${Number(form.regoPrice).toFixed(2)}` : 'Free'}</dd></div>
            </dl>
            <p className="event-form-note">Wait-list, payment methods, accommodations, and organizer roles aren’t stored by the current event API.</p>
          </section>
        )}

        {step < 3 && error && <p className="event-form-error" role="alert">{error}</p>}
      </form>

      <footer className="event-create-actions">
        {step === 0 ? (
          <button className="event-secondary-button" onClick={() => navigate('/events')} type="button">Cancel</button>
        ) : (
          <button className="event-secondary-button" onClick={() => { setError(''); setStep(current => current - 1) }} type="button">‹ Prev</button>
        )}
        {step < 3 ? (
          <button className="event-primary-button" onClick={nextStep} type="button">Next <span aria-hidden="true">›</span></button>
        ) : (
          <button className="event-primary-button" disabled={saving || loadingKennels} onClick={onSubmit} type="button">{saving ? 'Creating…' : 'Create Event'}</button>
        )}
      </footer>

      {showIntro && (
        <div className="event-intro-backdrop">
          <section aria-labelledby="event-intro-title" aria-modal="true" className="event-intro-dialog" role="dialog">
            <h1 id="event-intro-title">Create New Event</h1>
            <p>You’ll be guided through a few steps to set up your event. This takes about 2–3 minutes. You can save and finish later.</p>
            <div className="event-intro-actions">
              <button className="event-primary-button" onClick={() => setShowIntro(false)} type="button">Continue</button>
              <button className="event-secondary-button" onClick={() => navigate('/events')} type="button">Cancel</button>
            </div>
          </section>
        </div>
      )}
    </main>
  )
}