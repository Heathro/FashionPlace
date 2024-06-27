import { createWithEqualityFn } from "zustand/traditional"
import { shallow } from "zustand/shallow"
import { PagedResult, Product } from "@/types"

type State = {
  products: Product[],
  totalCount: number,
  pageCount: number
}

type Products = {
  setData: (data: PagedResult<Product>) => void
}

const initialState: State = {
  products: [],
  totalCount: 0,
  pageCount: 0
}

export const useProductsStore = createWithEqualityFn<State & Products>()((set) => ({

  ...initialState,

  setData: (data: PagedResult<Product>) => {
    set(() => ({
      products: data.results,
      totalCount: data.totalCount,
      pageCount: data.pageCount
    }))
  }

}), shallow)
