'use server'

import { PagedResult, Product } from "@/types";
import { getTokenWorkaround } from "./authActions";

export async function getData(params: string): Promise<PagedResult<Product>> {
  const response = await fetch(`http://localhost:6001/search${params}`)
  if (!response.ok) throw new Error('Failed to fetch data')
  return response.json()
}

export async function createProductTest() {
  const token = await getTokenWorkaround()

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

  const res = await fetch('http://localhost:6001/catalog/products', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + token?.access_token
    },
    body: JSON.stringify(data)
  })

  return { status: res.status, message: res.statusText }
}
