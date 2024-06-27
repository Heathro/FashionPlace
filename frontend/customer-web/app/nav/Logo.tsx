'use client'

import React from 'react'
import { usePathname, useRouter } from 'next/navigation'
import { useParamsStore } from '../hooks/useParamsStore'
import { IoShirtOutline } from 'react-icons/io5'

export default function Logo() {
  const router = useRouter()
  const pathName = usePathname()
  const reset = useParamsStore(state => state.reset)

  function doReset() {
    if (pathName !== '/') router.push('/')
    reset()
  }

  return (
    <div
      onClick={doReset}
      className='
        pl-6
        cursor-pointer
        flex
        items-center
        gap-2
        text-3xl
        font-semibold
        text-fuchsia-500
      '
    >
      <IoShirtOutline size={34} />
      Fashion Place
    </div>
  )
}
