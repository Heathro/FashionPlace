'use client'

import React from 'react'
import { signIn } from 'next-auth/react'
import { GoPerson } from 'react-icons/go'

export default function LoginButton() {
  return (
    <button
      onClick={() => signIn('id-server', { callbackUrl: '/' }, { prompt: 'login' })}
      className='pr-6'
    >
      <GoPerson size={34} className='text-fuchsia-500' />
    </button>
  )
}
