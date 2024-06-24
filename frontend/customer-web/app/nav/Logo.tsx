'use client'

import React from 'react'
import { useParamsStore } from '../hooks/useParamsStore'
import { IoShirtOutline } from 'react-icons/io5'

export default function Logo() {
  const reset = useParamsStore(state => state.reset)

  return (
    <div
      onClick={reset}
      className='cursor-pointer flex items-center gap-2 text-3xl font-semibold text-fuchsia-500'
    >
      <IoShirtOutline size={34} />
      Fashion Place
    </div>
  )
}
