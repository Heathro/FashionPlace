import React from 'react'
import NoContent from '@/app/components/NoContent'

export default function Page({ searchParams }: { searchParams: { callbackUrl: string } }) {
  return (
    <NoContent
      title='You need to be logged in to do that'
      subtitle='Sign in to continue'
      showLogin
      callbackUrl={searchParams.callbackUrl}
    />
  )
}
