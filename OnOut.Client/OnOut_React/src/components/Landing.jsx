import { useEffect } from 'react'
import { useAuth0 } from '@auth0/auth0-react'

export default function Landing() {
  const { loginWithRedirect, isAuthenticated, user } = useAuth0()

  useEffect(() => {
    // If the user is already authenticated, you might redirect to profile.
    // We'll leave navigation to the app routing (Auth0 can return to /profile).
  }, [isAuthenticated])

  return (
    <main className="min-h-screen flex items-center justify-center bg-gradient-to-b from-white to-gray-50 p-6">
      <div className="w-full max-w-sm bg-white dark:bg-slate-800 rounded-2xl shadow-lg p-6">
        <div className="flex flex-col items-center gap-4">
          <div className="w-24 h-24 rounded-full bg-purple-100 flex items-center justify-center">
            {/* Placeholder for logo - provide image for a nicer header */}
            <span className="text-2xl font-semibold text-purple-600">OnOut</span>
          </div>

          <h1 className="text-2xl font-semibold text-gray-900 dark:text-gray-100">Welcome to OnOut</h1>
          <p className="text-sm text-gray-600 dark:text-gray-300 text-center">Quickly sign in to create your hasher profile and get started.</p>

          <div className="w-full">
            <button
              onClick={() => loginWithRedirect({ authorizationParams: { redirect_uri: `${window.location.origin}/profile` } })}
              className="w-full bg-purple-600 hover:bg-purple-700 text-white py-3 rounded-lg font-medium"
            >
              Continue with Auth0
            </button>
          </div>

          <small className="text-xs text-gray-500">You will be prompted to create a hasher profile after login.</small>
        </div>
      </div>
    </main>
  )
}
