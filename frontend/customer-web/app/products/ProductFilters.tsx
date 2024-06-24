import React from 'react'
import { Button } from 'flowbite-react'
import { useParamsStore } from '../hooks/useParamsStore'
import { AiOutlineSortAscending, AiOutlineSortDescending } from 'react-icons/ai'

const orderOptions = [
  {
    label: 'Discount £ (High to Low)',
    icon: AiOutlineSortDescending,
    value: 'discount-amount'
  },
  {
    label: 'Discount % (High To Low)',
    icon: AiOutlineSortDescending,
    value: 'discount-procent'
  },
  {
    label: 'Price (Low To High)',
    icon: AiOutlineSortAscending,
    value: 'price-asc'
  },
  {
    label: 'Price (High To Low)',
    icon: AiOutlineSortDescending,
    value: 'price-desc'
  },
  {
    label: 'Brand (A To Z)',
    icon: AiOutlineSortAscending,
    value: 'brand-asc'
  },
  {
    label: 'Brand (Z To A)',
    icon: AiOutlineSortDescending,
    value: 'brand-desc'
  }
]

export default function ProductFilters() {
  const setParams = useParamsStore(state => state.setParams)
  const orderBy = useParamsStore(state => state.orderBy)

  return (
    <div className='flex justify-between items-center mb-4'>

      <div>
        <span className='uppercase text-sm text-gray-500 mr-2'>Sort</span>
        <Button.Group>
          {orderOptions.map(({label, icon: Icon, value}) => (
            <Button
              key={value}
              onClick={() => setParams({orderBy: value})}
              color={`${orderBy === value ? 'red' : 'gray'}`}
            >
              <Icon className='mr-3 h-4 w-4' />
              {label}
            </Button>
          ))}
        </Button.Group>
      </div>

    </div>
  )
}
