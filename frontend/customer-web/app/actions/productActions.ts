'use server'

import { PagedResult, Product } from "@/types";

export async function getData(params: string): Promise<PagedResult<Product>> {
  const response = await fetch(`http://localhost:6001/search${params}`);
  if (!response.ok) throw new Error('Failed to fetch data');
  return response.json();
}