'use client'

import React, { useEffect, useState } from 'react'
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr'
import { useAiStore } from '../hooks/useAiStore'
import { FieldValues, useForm } from 'react-hook-form'
import { Message, MessageThread } from '../types'

export default function Chat() {
  const [connection, setConnection] = useState<HubConnection | null>(null)
  const messageThread = useAiStore(state => state.messageThread)
  const setMessageThread = useAiStore(state => state.setMessageThread)
  const addMessage = useAiStore(state => state.addMessage)
  const { register, handleSubmit, reset, formState: { errors } } = useForm()

  function onSubmit(data: FieldValues) {
    connection?.invoke('SendMessage', data.message)
    reset()
  }

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl('http://localhost:6001/ai')
      .withAutomaticReconnect()
      .build()
    setConnection(newConnection)
  }, [])

  useEffect(() => {
    if (connection) {
      connection.start()
        .then(() => {
          console.log('\n======>>>>>> Connected to AI hub\n')

          connection.on('ReceiveMessageThread', (messageThread: MessageThread) => {
            console.log('\n======>>>>>> ReceiveMessageThread: ' + messageThread.connectionId + '\n')
            setMessageThread(messageThread)
          })
          connection.on('ReceiveMessage', (message: Message) => {
            console.log('\n======>>>>>> ReceiveMessage: ' + message.content + '\n')
            addMessage(message)
          })
        }).catch(error => {
          console.log('\n======>>>>>> Connection to AI failed: ' + error.message + '\n')
        })
    }

    return () => {
      connection?.stop()
    }
  }, [connection])

  return (
    <>
      {messageThread && (
        <>
          <div>
            {messageThread.messages.map((message) => (
              <div key={message.id}>
                {message.content}
              </div>
            ))}
          </div>
          
          <form onSubmit={handleSubmit(onSubmit)}>
            <input 
              type="text"
              {...register('message', { required: true })}
              placeholder="Type your message"
            />
            {errors.message && <span>This field is required</span>}
            <button type="submit">Send</button>
          </form>
        </>
      )}
  </>
  )
}
