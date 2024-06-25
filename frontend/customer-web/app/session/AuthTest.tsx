'use client'

import React, { useState } from 'react'
import { createProductTest } from '../actions/productActions'
import { Button } from 'flowbite-react'

export default function AuthTest() {
  const [loading, setLoading] = useState(false)
  const [result, setResult] = useState<any>()

  function Create() {
    setResult(undefined)
    setLoading(true)
    createProductTest()
      .then(res => setResult(res))
      .finally(() => setLoading(false))
  }
  
  return (
    <div className='flex items-center gap-4'>
      <Button outline isProcessing={loading} onClick={Create}>
        Test auth
      </Button>
      <div>
        {JSON.stringify(result, null, 2)}
      </div>
    </div>
  )
}
