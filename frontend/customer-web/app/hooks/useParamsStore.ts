import { createWithEqualityFn } from "zustand/traditional"
import { shallow } from "zustand/shallow"

type State = {
  searchTerm: string
  pageNumber: number
  pageSize: number
  orderBy: string
  filterBy: string
  pageCount: number
  searchValue: string
}

type Actions = {
  setParams: (params: Partial<State>) => void
  reset: () => void
  setSearchValue: (value: string) => void
}

const initialState: State = {
  searchTerm: '',
  pageNumber: 1,
  pageSize: 12,
  orderBy: '',
  filterBy: '',
  pageCount: 1,
  searchValue: ''
}

export const useParamsStore = createWithEqualityFn<State & Actions>()((set) => ({

  ...initialState,

  setParams: (newParams: Partial<State>) => {
    set((state) => {
      if (newParams.pageNumber) {
        return {...state, pageNumber: newParams.pageNumber}
      } else {
        return {...state, ...newParams, pageNumber: 1}
      }
    })
  },

  reset: () => {
    set(initialState)
  },

  setSearchValue: (value: string) => {
    set({searchValue: value})
  }
  
}), shallow)
