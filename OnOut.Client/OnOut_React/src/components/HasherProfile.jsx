import { useEffect, useState } from 'react'
import { useAuth0 } from '@auth0/auth0-react'
import { useNavigate } from 'react-router-dom'

function uniqueSorted(values) {
  return [...new Map(
    values
      .filter(value => typeof value === 'string' && value.trim())
      .map(value => [value.trim().toLocaleLowerCase(), value.trim()]),
  ).values()].sort((left, right) => left.localeCompare(right))
}

export default function HasherProfile() {
  const navigate = useNavigate()
  const { isAuthenticated, user, getAccessTokenSilently } = useAuth0()
  const [form, setForm] = useState({
    hashName: '',
    email: '',
    homeCity: '',
    homeState: '',
    homeCountry: '',
    dietaryChoiceId: '',
  })
  const [dietaryChoices, setDietaryChoices] = useState([])
  const [kennelLocations, setKennelLocations] = useState([])
  const [loading, setLoading] = useState(false)
  const [loadingChoices, setLoadingChoices] = useState(true)
  const [hasherId, setHasherId] = useState('')
  const [error, setError] = useState('')

  useEffect(() => {
    if (!isAuthenticated) {
      return
    }

    async function loadDietaryChoices() {
      try {
        const token = await getAccessTokenSilently()
        const apiBase = import.meta.env.VITE_API_BASE_URL || ''
        const [profileResponse, choicesResponse, locationsResponse] = await Promise.all([
          fetch(`${apiBase}/api/auth/me`, {
            headers: { Authorization: `Bearer ${token}` },
          }),
          fetch(`${apiBase}/api/dietarychoices`, {
            headers: { Authorization: `Bearer ${token}` },
          }),
          fetch(`${apiBase}/api/kennels`, {
            headers: { Authorization: `Bearer ${token}` },
          }).catch(() => null),
        ])

        if (!profileResponse.ok) {
          throw new Error(`Unable to load your profile (${profileResponse.status}).`)
        }
        if (!choicesResponse.ok) {
          throw new Error(`Unable to load dietary choices (${choicesResponse.status}).`)
        }

        const profile = await profileResponse.json()
        setHasherId(profile.hasherId)
        setForm(current => ({
          ...current,
          homeCity: profile.homeCity || current.homeCity,
          homeState: profile.homeState || current.homeState,
          homeCountry: profile.homeCountry || current.homeCountry,
        }))

        const choices = await choicesResponse.json()
        setDietaryChoices(choices)
        if (locationsResponse?.ok) {
          setKennelLocations(await locationsResponse.json())
        }

      } catch (err) {
        console.error('Failed to load profile setup data', err)
        setError(err.message || 'Could not load profile setup.')
      } finally {
        setLoadingChoices(false)
      }
    }

    loadDietaryChoices()
  }, [isAuthenticated, getAccessTokenSilently])

  const countryOptions = uniqueSorted(kennelLocations.map(location => location.country))
  const stateOptions = uniqueSorted(
    kennelLocations
      .filter(location => !form.homeCountry || location.country?.trim().toLocaleLowerCase() === form.homeCountry.trim().toLocaleLowerCase())
      .map(location => location.state),
  )
  const cityOptions = uniqueSorted(
    kennelLocations
      .filter(location => !form.homeCountry || location.country?.trim().toLocaleLowerCase() === form.homeCountry.trim().toLocaleLowerCase())
      .filter(location => !form.homeState || location.state?.trim().toLocaleLowerCase() === form.homeState.trim().toLocaleLowerCase())
      .map(location => location.city),
  )

      if (!isAuthenticated) {
        return (
          <div className="min-h-screen flex items-center justify-center">
            <div className="p-4 text-center">
              <h2 className="text-xl font-semibold">Please sign in</h2>
              <p className="text-sm text-gray-600">You must be signed in to create your hasher profile.</p>
            </div>
          </div>
        )
      }

      function onChange(e) {
        const { name, value } = e.target
        setForm(current => ({
          ...current,
          [name]: value,
          ...(name === 'homeCountry' ? { homeState: '', homeCity: '' } : {}),
          ...(name === 'homeState' ? { homeCity: '' } : {}),
        }))
      }

      async function onSubmit(e) {
        e.preventDefault()
        setLoading(true)
        setError('')
        try {
          const token = await getAccessTokenSilently()
          const apiBase = import.meta.env.VITE_API_BASE_URL || ''
          const res = await fetch(`${apiBase}/api/auth/profile`, {
            method: 'PUT',
            headers: {
              'Content-Type': 'application/json',
              Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify({
              Hasher: {
                Id: hasherId,
                Name: form.hashName,
                HashName: form.hashName,
                Email: form.email || user?.email,
                HomeCity: form.homeCity,
                HomeState: form.homeState,
                HomeCountry: form.homeCountry,
                DietaryChoiceId: form.dietaryChoiceId || null,
                ProfileCompleted: true,
              },
            }),
          })

          if (!res.ok) {
            throw new Error(`${res.status} ${await res.text()}`)
          }

          navigate('/', { replace: true })
        } catch (err) {
          console.error(err)
          setError(err.message || 'Failed to save your profile.')
        } finally {
          setLoading(false)
        }
      }

      return (
        <main className="min-h-screen flex items-center justify-center p-6">
          <form onSubmit={onSubmit} className="w-full max-w-lg bg-white dark:bg-slate-800 rounded-xl shadow p-6 space-y-4">
            <h2 className="text-xl font-semibold text-gray-900 dark:text-gray-100">Create your hasher profile</h2>
            {error && <p role="alert" className="text-sm text-red-700">{error}</p>}

            <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
              <label className="block">
                <div className="text-sm text-gray-600 mb-1">Hash name</div>
                <input
                  name="hashName"
                  value={form.hashName}
                  onChange={onChange}
                  className="w-full rounded-md border px-3 py-2"
                  required
                />
              </label>

              <label className="block">
                <div className="text-sm text-gray-600 mb-1">Email</div>
                <input
                  name="email"
                  type="email"
                  value={form.email}
                  onChange={onChange}
                  className="w-full rounded-md border px-3 py-2"
                  placeholder={user?.email || 'you@example.com'}
                />
              </label>
            </div>

            <div className="grid grid-cols-1 gap-4 sm:grid-cols-3">
              <label className="block">
                <div className="text-sm text-gray-600 mb-1">City</div>
                <input
                  autoComplete="address-level2"
                  list="home-city-options"
                  name="homeCity"
                  value={form.homeCity}
                  onChange={onChange}
                  className="w-full rounded-md border px-3 py-2"
                  placeholder="Search or enter city"
                  required
                />
                <datalist id="home-city-options">
                  {cityOptions.map(city => <option key={city} value={city} />)}
                </datalist>
              </label>

              <label className="block">
                <div className="text-sm text-gray-600 mb-1">State</div>
                <input
                  autoComplete="address-level1"
                  list="home-state-options"
                  name="homeState"
                  value={form.homeState}
                  onChange={onChange}
                  className="w-full rounded-md border px-3 py-2"
                  placeholder="Search or enter state"
                />
                <datalist id="home-state-options">
                  {stateOptions.map(state => <option key={state} value={state} />)}
                </datalist>
              </label>

              <label className="block">
                <div className="text-sm text-gray-600 mb-1">Country</div>
                <input
                  autoComplete="country-name"
                  list="home-country-options"
                  name="homeCountry"
                  value={form.homeCountry}
                  onChange={onChange}
                  className="w-full rounded-md border px-3 py-2"
                  placeholder="Search or enter country"
                  required
                />
                <datalist id="home-country-options">
                  {countryOptions.map(country => <option key={country} value={country} />)}
                </datalist>
              </label>
            </div>

            <label className="block">
              <div className="text-sm text-gray-600 mb-1">Dietary choice</div>
              <select
                name="dietaryChoiceId"
                value={form.dietaryChoiceId}
                onChange={onChange}
                className="w-full rounded-md border px-3 py-2"
                disabled={loadingChoices || !hasherId}
              >
                <option value="">None</option>
                {!loadingChoices && (
                  dietaryChoices.map(choice => (
                    <option key={choice.id} value={choice.id}>
                      {choice.name}
                    </option>
                  ))
                )}
              </select>
              {dietaryChoices.length > 0 && (
                <p className="mt-2 text-sm text-gray-500">{dietaryChoices.find(choice => choice.id === form.dietaryChoiceId)?.description}</p>
              )}
            </label>

            <div className="flex justify-end">
              <button
                disabled={loading || loadingChoices || !hasherId}
                type="submit"
                className="bg-purple-600 hover:bg-purple-700 text-white px-4 py-2 rounded-md disabled:opacity-50"
              >
                {loading ? 'Saving…' : 'Finish profile'}
              </button>
            </div>
          </form>
        </main>
      )
    }
