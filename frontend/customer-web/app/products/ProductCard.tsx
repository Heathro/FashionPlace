import React from 'react'
import ProductImage from './ProductImage'
import { Product, Variant } from '@/types';

type Props = {
  product: Product
}

export default function ProductCard({ product }: Props) {
  const variant = product.variants.find(v => v.quantity > 0);
  if (!variant) return null;

  const sizes = Array.from(new Set(product.variants.map((variant: Variant) => variant.size)));

  const originalPrice = variant.price
  const discount = variant.discount
  const discountedPrice = originalPrice * (1 - discount / 100)
  const formattedOriginalPrice = originalPrice.toLocaleString('en-GB', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2
  });
  const formattedDiscountedPrice = discountedPrice.toLocaleString('en-GB', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2
  });

  return (
    <a href='#' className='group'>
      <ProductImage imageUrl={variant.imageUrl} />
      <div>

        <p className='text-sm text-gray-600 mt-2'>{product.brand}</p>

        <p className='text-2xl'>{product.model}</p>

        <p className='text-sm text-gray-600 mt-2'>
          {sizes.join(', ')}
        </p>

        {discount > 0 ? (
          <div className='flex gap-3'>
            <p className='text-2xl text-red-500'>£{formattedDiscountedPrice}</p>
            <p className='text-2xl line-through'>£{formattedOriginalPrice}</p>
          </div>
        ) : (
          <p className='text-2xl'>£{formattedOriginalPrice}</p>
        )}

      </div>
    </a>
  )
}
