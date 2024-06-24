import React from 'react'
import Logo from './Logo';
import Searchbar from './SearchBar';

export default function NavBar() {
  return (
    <header className='sticky top-0 z-50 flex justify-between bg-black p-5 items-center text-white shadow-md'>

      <Logo />

      <Searchbar />

      <div>Login</div>

    </header>
  )
}
