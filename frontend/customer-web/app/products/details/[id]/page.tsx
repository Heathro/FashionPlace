import React from 'react'
import { getProduct } from '@/app/actions/productActions'
import Heading from '@/app/components/Heading'

export default async function ProductDetails({params}: {params: {id: string}}) {
  const product = await getProduct(params.id)

  return (
    <div className='grid grid-cols-2 gap-6'>

      <div className='w-full rounded-lg overflow-hidden'>
        <img src={product.variants[0].imageUrl} alt={product.model} />
      </div>

      <div>
        <Heading title={product.brand} subtitle={product.model} invert/>
      </div>

    </div>

  )
}
