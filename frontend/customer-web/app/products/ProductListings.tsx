'use client'

import React, { useEffect, useState } from 'react'
import { shallow } from 'zustand/shallow'
import queryString from 'query-string'
import AppPagination from '../components/AppPagination'
import { getProducts } from '../actions/productActions'
import { useParamsStore } from '../../hooks/useParamsStore'
import ProductFilters from './ProductFilters'
import ProductCard from './ProductCard'
import { useProductsStore } from '../../hooks/useProductsStore'
import NoContent from '../components/NoContent'

export default function ProductListings() {
  const [loading, setLoading] = useState(true)
  const params = useParamsStore(state => ({
    searchTerm: state.searchTerm,
    pageNumber: state.pageNumber,
    pageSize: state.pageSize,
    orderBy: state.orderBy,
    filterBy: state.filterBy
  }), shallow)
  const setParams = useParamsStore(state => state.setParams)
  const data = useProductsStore(state => ({
    products: state.products,
    totalCount: state.totalCount,
    pageCount: state.pageCount
  }), shallow)
  const setData = useProductsStore(state => state.setData)
  const formattedQuery = queryString.stringifyUrl({url: '', query: params})

  function setPageNumber(pageNumber: number) {
    setParams({pageNumber})
  }

  useEffect(() => {
    getProducts(formattedQuery).then(data => {
      setData(data)
      setLoading(false)
    })
  }, [formattedQuery])

  if (loading) return <h3>Loading...</h3>

  return (
    <>
      <ProductFilters />

      {data.totalCount === 0 ? (
        
        <NoContent
          title='No products found'
          subtitle='Try changing filters or search term'
          showReset
        />

      ) : (
        <>

          <div className='grid grid-cols-2 md:grid-cols-4 gap-6'>
            {data.products.map(product => (
              <ProductCard product={product} orderBy={params.orderBy} key={product.id} />
            ))}
          </div>

          <div className='flex justify-center mt-4'>
            <AppPagination 
              currentPage={params.pageNumber}
              pageCount={data.pageCount}
              pageChanged={setPageNumber}
            />
          </div>

        </>
      )}         
    </>
  )
}
