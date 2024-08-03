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

  const linkify = (text: string) => {
    const guidPattern = /\b[A-Fa-f0-9]{8}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{12}\b/g;  
    return text.replace(guidPattern, (guid) => 
      `<a href="/products/details/${guid}" 
        target="_blank" rel="noopener noreferrer" 
        style="color: blue; text-decoration: underline;"
      >Link</a>`
    );
  };

  return (
    <div className='w-1/2 mx-auto'>
      {messageThread && (
        <>
          <div>
            {messageThread.messages.map((message) => (
              <div
                key={message.id}
                className={`
                  p-2 m-2 rounded
                  ${message.isUser ? 'bg-green-100 text-right' : 'bg-red-100 text-left'}
                `}
                dangerouslySetInnerHTML={{ __html: linkify(message.content) }}
              />
            ))}
          </div>
          
          <form
            className='flex justify-center'
            onSubmit={handleSubmit(onSubmit)}
          >
            <input 
              type="text"
              {...register('message', { required: true })}
              placeholder="Type your message"
            />
            {errors.message && <span>This field is required</span>}
            <button className='ml-4' type="submit">Send</button>
          </form>
        </>
      )}
    </div>
  )
}
