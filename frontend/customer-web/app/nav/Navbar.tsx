import React from 'react'
import { IoShirtOutline } from "react-icons/io5";

export default function Navbar() {
  return (
    <header className='
      sticky top-0 z-50 flex justify-between bg-black p-5 items-center text-white shadow-md
    '>
      <div className='
        flex items-center gap-2 text-3xl font-semibold text-fuchsia-500
      '>
        <IoShirtOutline size={34} />
        Fashion Place
      </div>
      <div>Search</div>
      <div>Login</div>
    </header>
  )
}
