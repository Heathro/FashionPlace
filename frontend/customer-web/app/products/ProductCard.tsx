import React from 'react'
import formatPrice from '../helpers/formatters';
import { Product, Variant } from '@/types';
import ProductImage from './ProductImage'

type Props = {
  product: Product
  orderBy: string
}

export default function ProductCard({ product, orderBy }: Props) {
  let variant: Variant | undefined;
  let discountedPrice: number | undefined;
  switch (orderBy) {
    case 'discount-amount':
      variant = product.variants.find(v => v.id === product.discountAmountHighest.id);
      discountedPrice = product.discountAmountHighest.value;
      break;
    case 'discount-procent':
      variant = product.variants.find(v => v.id === product.discountPercentHighest.id);
      discountedPrice = product.discountPercentHighest.value;
      break;
    case 'price-desc':
      variant = product.variants.find(v => v.id === product.discountedPriceHighest.id);
      discountedPrice = product.discountedPriceHighest.value;
      break;
    default:
      variant = product.variants.find(v => v.id === product.discountedPriceLowest.id);
      discountedPrice = product.discountedPriceLowest.value;
      break;
  }
  if (!variant || !discountedPrice) return;

  const sizes = Array.from(new Set(product.variants.map((variant: Variant) => variant.size)));

  return (
    <a href='#' className='group'>
      <ProductImage imageUrl={variant.imageUrl} />
      <div>
        <p className='text-sm sm:text-base md:text-sm text-gray-600 mt-1 sm:mt-2'>{product.brand}</p>
        <p className='text-lg sm:text-xl md:text-2xl mt-1 sm:mt-2'>{product.model}</p>
        <p className='text-sm sm:text-base md:text-sm text-gray-600 mt-1 sm:mt-2'>{sizes.join(', ')}</p>
      
        {variant.discount > 0 ? (
          <div className='flex gap-2 sm:gap-3 mt-1 sm:mt-2'>
            <p className='text-lg sm:text-xl md:text-2xl text-red-500'>£{formatPrice(discountedPrice)}</p>
            <p className='text-lg sm:text-xl md:text-2xl line-through'>£{formatPrice(variant.price)}</p>
          </div>
        ) : (
          <p className='text-lg sm:text-xl md:text-2xl mt-1 sm:mt-2'>£{formatPrice(variant.price)}</p>
        )}
      </div>
    </a>
  )
}
