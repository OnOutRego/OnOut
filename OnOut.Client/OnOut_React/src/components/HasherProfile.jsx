import { useState } from 'react'
import { useAuth0 } from '@auth0/auth0-react'

export default function HasherProfile() {
  const { isAuthenticated, user, getAccessTokenSilently } = useAuth0()
  const [form, setForm] = useState({ name: '', email: '', homeCity: '', homeState: '', homeCountry: '' })
  const [loading, setLoading] = useState(false)

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
    setForm({ ...form, [e.target.name]: e.target.value })
  }

  async function onSubmit(e) {
    e.preventDefault()
    setLoading(true)
    try {
      const token = await getAccessTokenSilently()
      const apiBase = import.meta.env.VITE_API_BASE_URL || ''
      const payload = {
        UserId: user?.sub,
        Name: form.name,
        Email: form.email || user?.email,
        HomeCity: form.homeCity,
        HomeState: form.homeState,
        HomeCountry: form.homeCountry,
        DietaryChoiceId: '00000000-0000-0000-0000-000000000000' // placeholder
      }

      const res = await fetch(`${apiBase}/api/hashers`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(payload),
      })

      if (!res.ok) {
        const text = await res.text()
        throw new Error(`${res.status} ${text}`)
      }

      const createdId = await res.json()
      alert('Hasher profile created: ' + createdId)
    } catch (err) {
      console.error(err)
      alert('Failed to create profile: ' + err.message)
    } finally {
      setLoading(false)
    }
  }

  return (
    <main className="min-h-screen flex items-center justify-center p-6">
      <form onSubmit={onSubmit} className="w-full max-w-md bg-white dark:bg-slate-800 rounded-xl shadow p-6">
        <h2 className="text-xl font-semibold mb-4 text-gray-900 dark:text-gray-100">Create your hasher profile</h2>

        <label className="block mb-3">
          <div className="text-sm text-gray-600 mb-1">Full name</div>
          <input name="name" value={form.name} onChange={onChange} className="w-full rounded-md border px-3 py-2" />
        </label>

        <label className="block mb-3">
          <div className="text-sm text-gray-600 mb-1">Email</div>
          <input name="email" value={form.email} onChange={onChange} className="w-full rounded-md border px-3 py-2" />
        </label>

        <label className="block mb-3">
          <div className="text-sm text-gray-600 mb-1">Home city</div>
          <input name="homeCity" value={form.homeCity} onChange={onChange} className="w-full rounded-md border px-3 py-2" />
        </label>

        <label className="block mb-3">
          <div className="text-sm text-gray-600 mb-1">Home state</div>
          <input name="homeState" value={form.homeState} onChange={onChange} className="w-full rounded-md border px-3 py-2" />
        </label>

        <label className="block mb-4">
          <div className="text-sm text-gray-600 mb-1">Home country</div>
          <input name="homeCountry" value={form.homeCountry} onChange={onChange} className="w-full rounded-md border px-3 py-2" />
        </label>

        <div className="flex justify-end">
          <button disabled={loading} type="submit" className="bg-purple-600 hover:bg-purple-700 text-white px-4 py-2 rounded-md">
            {loading ? 'Saving…' : 'Save profile'}
          </button>
        </div>
      </form>
    </main>
  )
}
