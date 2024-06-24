'use client'

import React, { useEffect, useState } from 'react'
import { shallow } from 'zustand/shallow';
import queryString from 'query-string'
import AppPagination from '../components/AppPagination';
import { getData } from '../actions/productActions';
import { useParamsStore } from '../hooks/useParamsStore';
import { PagedResult, Product } from '@/types';
import ProductFilters from './ProductFilters';
import ProductCard from './ProductCard';

export default function ProductListings() {
  const [data, setData] = useState<PagedResult<Product>>()
  const params = useParamsStore(state => ({
    searchTerm: state.searchTerm,
    pageNumber: state.pageNumber,
    pageSize: state.pageSize,
    orderBy: state.orderBy
  }), shallow)
  const setParams = useParamsStore(state => state.setParams)
  const formattedQuery = queryString.stringifyUrl({url: '', query: params})

  function setPageNumber(pageNumber: number) {
    setParams({pageNumber})
  }

  useEffect(() => {
    getData(formattedQuery).then(data => {
      setData(data)
    })
  }, [formattedQuery])

  if (!data) return <h3>Loading...</h3>

  return (
    <>
      <ProductFilters />
      <div className='grid grid-cols-2 md:grid-cols-4 gap-6'>
        {data.results.map(product => (
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
  )
}
