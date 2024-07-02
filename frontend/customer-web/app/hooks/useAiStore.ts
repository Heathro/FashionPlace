import { shallow } from "zustand/shallow"
import { createWithEqualityFn } from "zustand/traditional"
import { Message, MessageThread } from "../types"

type State = {
  messageThread: MessageThread
}

type Actions = {
  setMessageThread: (messageThread: MessageThread) => void
  addMessage: (message: Message) => void
}

const initialState: State = {
  messageThread: {
    id: '',
    connectionId: '',
    messages: []
  } as MessageThread
}

export const useAiStore = createWithEqualityFn<State & Actions>()((set) => ({

  ...initialState,

  setMessageThread: (data: MessageThread) => {
    set({messageThread: data})
  },

  addMessage: (value: Message) => {
    set(state => ({
      messageThread: {
        ...state.messageThread,
        messages: [...state.messageThread.messages, value]
      }
    }))
  }

}), shallow)
