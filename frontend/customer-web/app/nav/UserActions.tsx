'use client'

import React from 'react'
import Link from 'next/link'
import { signOut } from 'next-auth/react'
import { Button, Dropdown } from 'flowbite-react'
import { GoHeart } from 'react-icons/go'
import { GoPersonFill } from 'react-icons/go'
import { SlBasket } from 'react-icons/sl'

export default function UserActions() {
  return (
    <div className='flex items-center gap-2'>
      <Dropdown
        inline
        label={<GoPersonFill size={34} className="text-fuchsia-500" />}
        className='bg-black border-black'
      >
        <Dropdown.Item
          className='
            text-white text-lg
            hover:text-black hover:bg-fuchsia-500
            focus:text-black focus:bg-fuchsia-500
          '
        >
          <Link href=''>
            Account
          </Link>
        </Dropdown.Item>

        <Dropdown.Item
          className='
            text-white text-lg
            hover:text-black hover:bg-fuchsia-500
            focus:text-black focus:bg-fuchsia-500
          '
        >
          <Link href='/session'>
            Session (dev)
          </Link>
        </Dropdown.Item>

        <Dropdown.Item
          onClick={() => signOut({ callbackUrl: '/' })}
          className='
            text-rose-500 text-lg
            hover:text-black hover:bg-fuchsia-500
            focus:text-black focus:bg-fuchsia-500
          '
        >
          Sign out
        </Dropdown.Item>
      </Dropdown>

      <Link href='/session' className='pr-6'>
        <GoHeart size={34} className='text-fuchsia-500' />
      </Link>

      <Link href='/session' className='pr-6'>
        <SlBasket size={34} className='text-fuchsia-500' />
      </Link>
    </div>
  )
}
