'use client'

import React, { useEffect, useState } from 'react'
import { getData } from '../actions/productActions';
import AppPagination from '../components/AppPagination';
import { Product } from '@/types';
import ProductCard from './ProductCard';

export default function ProductListings() {
  const [products, setProducts] = useState<Product[]>([])
  const [pageCount, setPageCount] = useState(0)
  const [pageNumber, setPageNumber] = useState(1)

  useEffect(() => {
    getData(pageNumber).then(data => {
      setProducts(data.results)
      setPageCount(data.pageCount)
    })
  }, [pageNumber])

  if (products.length === 0) return <h3>Loading...</h3>

  return (
    <>
      <div className='grid grid-cols-4 gap-6'>
        {products.map(product => (
          <ProductCard product={product} key={product.id} />
        ))}
      </div>
      <div className='flex justify-center mt-4'>
        <AppPagination currentPage={pageNumber} pageCount={pageCount} pageChanged={setPageNumber} />
      </div>    
    </>
  )
}
