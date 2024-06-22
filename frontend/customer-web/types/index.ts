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

export type Categories = {
  id: string
  name: string
  subCategories: Categories[]
}