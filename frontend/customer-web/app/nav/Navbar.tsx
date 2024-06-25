import React from 'react'
import { getCurrentUser } from '../actions/authActions';
import Logo from './Logo';
import Searchbar from './SearchBar';
import UserActions from './UserActions';
import LoginButton from './LoginButton';

export default async function NavBar() {
  const user = await getCurrentUser()

  return (
    <header className='
      sticky top-0 z-50 flex
      justify-between bg-black p-5
      items-center shadow-md'
    >

      <Logo />

      <Searchbar />

      {user ? (
        <UserActions />
      ) : (
        <LoginButton />
      )}

    </header>
  )
}
