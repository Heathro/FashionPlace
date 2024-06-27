import React from 'react'
import { Button } from 'flowbite-react'
import { useParamsStore } from '../hooks/useParamsStore'


const orderOptions = [
  { label: 'Discount £ (High to Low)', value: 'discount-amount' },
  { label: 'Discount % (High To Low)', value: 'discount-percent' },
  { label: 'Price (Low To High)', value: 'price-asc' },
  { label: 'Price (High To Low)', value: 'price-desc' },
  { label: 'Brand (A To Z)', value: 'brand-asc' },
  { label: 'Brand (Z To A)', value: 'brand-desc' }
]

const filterOptions = [
  { label: 'White', value: 'White' },
  { label: 'Black', value: 'Black' },
  { label: 'Gold', value: 'Gold' },
  { label: 'Red', value: 'Red' },
  { label: 'Blue', value: 'Blue' },
  { label: 'Yellow', value: 'Yellow' }
]

export default function ProductFilters() {
  const setParams = useParamsStore(state => state.setParams)
  const orderBy = useParamsStore(state => state.orderBy)
  const filterBy = useParamsStore(state => state.filterBy)

  return (
    <div className='items-center mb-4'>

      <div>
        <span className='uppercase text-sm text-gray-500 mr-2'>Sort</span>
        <Button.Group>
          {orderOptions.map(({label, value}) => (
            <Button
              key={value}
              onClick={() => setParams({orderBy: value})}
              color={`${orderBy === value ? 'red' : 'gray'}`}
            >
              {label}
            </Button>
          ))}
        </Button.Group>
      </div>

      <div>
        <span className='uppercase text-sm text-gray-500 mr-2'>Color</span>
        <Button.Group>
          {filterOptions.map(({label, value}) => (
            <Button
              key={value}
              onClick={() => setParams({filterBy: value})}
              color={`${filterBy === value ? 'red' : 'gray'}`}
            >
              {label}
            </Button>
          ))}
        </Button.Group>
      </div>

    </div>
  )
}
