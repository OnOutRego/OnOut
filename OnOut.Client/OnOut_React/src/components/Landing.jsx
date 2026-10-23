import { useEffect, useRef, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { useAuth0 } from '@auth0/auth0-react'
import Home from './Home'
import './Landing.css'

export default function Landing() {
  const navigate = useNavigate()
  const { loginWithRedirect, logout, getAccessTokenSilently, isAuthenticated, isLoading, user } = useAuth0()
  const [mode, setMode] = useState('login')
  const [email, setEmail] = useState('')
  const [profileStatus, setProfileStatus] = useState('idle')
  const [profile, setProfile] = useState(null)
  const [error, setError] = useState('')
  const profileRequest = useRef(null)

  useEffect(() => {
    if (!isAuthenticated) {
      profileRequest.current = null
      return
    }

    let cancelled = false

    if (!profileRequest.current) {
      profileRequest.current = (async () => {
        const token = await getAccessTokenSilently()
        const apiBase = import.meta.env.VITE_API_BASE_URL || ''
        const response = await fetch(`${apiBase}/api/auth/me`, {
          headers: { Authorization: `Bearer ${token}` },
        })

        if (!response.ok) {
          throw new Error(`Unable to load your account (${response.status}).`)
        }

        return response.json()
      })()
    }

    setProfileStatus('checking')
    setError('')
    profileRequest.current
      .then(profile => {
        if (cancelled) return
        if (profile.needsProfileCompletion) {
          navigate('/profile', { replace: true })
          return
        }

        setProfile(profile)
        setProfileStatus('ready')
      })
      .catch(requestError => {
        profileRequest.current = null
        if (!cancelled) {
          setError(requestError.message || 'Unable to load your account.')
          setProfileStatus('error')
        }
      })
    return () => { cancelled = true }
  }, [getAccessTokenSilently, isAuthenticated, navigate])

  async function continueWithAuth(event) {
    event.preventDefault()
    setError('')

    try {
      await loginWithRedirect({
        authorizationParams: {
          screen_hint: mode,
          ...(email.trim() ? { login_hint: email.trim() } : {}),
        },
      })
    } catch (loginError) {
      setError(loginError.message || 'Unable to start sign in.')
    }
  }

  if (isLoading || (isAuthenticated && !['ready', 'error'].includes(profileStatus))) {
    return <main className="auth-state" aria-live="polite">Loading your account…</main>
  }

  if (isAuthenticated) {
    if (profileStatus === 'ready') {
      return <Home profile={profile} />
    }

    return (
      <main className="auth-state">
        <img className="auth-state-logo" src="/onout-logo.svg" alt="On Out" />
        <h1>{profileStatus === 'ready' ? `Welcome${user?.name ? `, ${user.name}` : ''}` : 'We could not load your profile'}</h1>
        {error && <p className="auth-error" role="alert">{error}</p>}
        <div className="auth-state-actions">
          {profileStatus === 'error' && (
            <button className="auth-primary-button" onClick={() => window.location.reload()} type="button">Try again</button>
          )}
          <button
            className="auth-secondary-button"
            onClick={() => logout({ logoutParams: { returnTo: window.location.origin } })}
            type="button"
          >
            Sign out
          </button>
        </div>
      </main>
    )
  }

  return (
    <main className="auth-page">
      <header className="auth-header">
        <div className="auth-header-inner">
          <img className="auth-logo" src="/onout-logo.svg" alt="On Out" />
          <button
            className="auth-mode-button"
            onClick={() => setMode(current => current === 'signup' ? 'login' : 'signup')}
            type="button"
          >
            {mode === 'signup' ? 'Sign In' : 'Sign Up'}
          </button>
        </div>
      </header>

      <section className="auth-content">
        <p className="auth-intro">Welcome to On Out, built by hashers for hashers!</p>
        <p className="auth-description">
          On Out supports events as well as regular trails. {mode === 'signup' ? 'Sign up' : 'Sign in'} to get started.
        </p>

        <form className="auth-form" onSubmit={continueWithAuth}>
          <label className="auth-field-label" htmlFor="auth-email">
            {mode === 'signup' ? 'Sign up' : 'Sign in'} with your email address
          </label>
          <input
            autoComplete="email"
            className="auth-email-input"
            id="auth-email"
            onChange={event => setEmail(event.target.value)}
            placeholder="Email"
            required
            type="email"
            value={email}
          />
          {error && <p className="auth-error" role="alert">{error}</p>}
          <button className="auth-primary-button auth-next-button" type="submit">
            Next <span aria-hidden="true">›</span>
          </button>
        </form>
      </section>
    </main>
  )
}
