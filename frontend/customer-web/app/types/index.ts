export type PagedResult<T> = {
  results: T[]
  pageCount: number
  totalCount: number
}

export type Product = {
  id: string
  description: string
  brand: string
  model: string
  productCategories: ProductCategory[]
  variants: Variant[]
  specifications: Specification[]
  discountAmountHighest: DiscountData
  discountPercentHighest: DiscountData
  discountedPriceLowest: DiscountData
  discountedPriceHighest: DiscountData
  searchString: string
}

export type ProductCategory = {
  id: string
  categories: string[]
}

export type Variant = {
  id: string
  color: string
  size: string
  price: number
  discount: number
  quantity: number
  imageUrl: string
}

export type Specification = {
  type: string
  value: string
}

export type DiscountData = {
  id: string
  value: number
}

export type Categories = {
  id: string
  name: string
  subCategories: Categories[]
}

export type MessageThread = {
  id: string,
  connectionId: string,
  messages: Message[]
}

export type Message = {
  id: string,
  isUser: boolean,
  content: string
}
