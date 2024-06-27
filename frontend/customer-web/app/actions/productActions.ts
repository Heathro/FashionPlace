'use server'

import { fetchWrapper } from "@/lib/fetchWrapper";
import { PagedResult, Product } from "@/types";

export async function getProducts(query: string): Promise<PagedResult<Product>> {
  return await fetchWrapper.get(`search${query}`)
}

export async function getProduct(id: string): Promise<Product> {
  return await fetchWrapper.get(`catalog/products/${id}`)
}

export async function createProductTest() {
  const data = {
    "description": "test",
    "brand": "test",
    "model": "test",
    "categories": [
      {
        "newCategories": [
          "test"
        ],
        "parentCategoryId": null
      }
    ],
    "variants": [
      {
        "color": "test",
        "size": "test",
        "price": 1,
        "discount": 1,
        "quantity": 1,
        "imageUrl": "https://images.pexels.com/photos/17931250/pexels-photo-17931250/free-photo-of-blue-trainers-for-runners.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
      }
    ],
    "specifications": [
      {
        "type": "test",
        "value": "test"
      }
    ]
  }

  return await fetchWrapper.post('catalog/products', data)
}
