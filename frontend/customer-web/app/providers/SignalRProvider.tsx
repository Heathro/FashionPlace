'use client'

import { ReactNode, useEffect, useState } from 'react'
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr'
import { Product } from '../types'

type Props = {
  children: ReactNode
}

export default function SignalRProvider({children}: Props) {
  const [connection, setConnection] = useState<HubConnection | null>(null)

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl('http://localhost:6001/notification')
      .withAutomaticReconnect()
      .build()

    setConnection(newConnection)
  }, [])

  useEffect(() => {
    if (connection) {
      connection.start()
        .then(() => {
          console.log('\n\n======>>>>>> Connected to notification hub\n\n')

          connection.on('ProductAdded', (product: Product) => {
            console.log('\n\n======>>>>>> Product Added: ' + product.id + '\n\n')
          })
        }).catch(error => {
          console.log('\n\n======>>>>>> Connection failed: ' + error.message + '\n\n')
        })
    }

    return () => {
      connection?.stop()
    }
  }, [connection])

  return (
    children
  )
}
