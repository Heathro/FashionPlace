'use client'

import React from 'react';
import Heading from './Heading';
import { Button } from 'flowbite-react';
import { signIn } from 'next-auth/react';
import { useParamsStore } from '../../hooks/useParamsStore';

type Props = {
	title?: string;
	subtitle?: string;
	showReset?: boolean;
	showLogin?: boolean;
	callbackUrl?: string
}

export default function NoContent({
	title = '',
	subtitle = '',
	showReset,
	showLogin,
	callbackUrl
}: Props) {
	const reset = useParamsStore(state => state.reset);

	return (
		<div
			className='
				h-[40vh]
				flex
				flex-col
				gap-2
				justify-center
				items-center
				shadow-2xl
			'
		>
			<Heading title={title} subtitle={subtitle} center />
			<div className='mt-4'>
				{showReset && (
					<button
						onClick={() => reset()}
						className='
						bg-black 
						text-fuchsia-500
							rounded-2xl
							px-4 py-2
							border-4 border-black
							hover:border-4 hover:border-fuchsia-500
							focus:border-4 focus:border-fuchsia-500
						'
					>
						Reset
					</button>
				)}
				{showLogin && (
					<button
						onClick={() => signIn('id-server', { callbackUrl })}
						className='
						bg-black 
						text-fuchsia-500
							rounded-2xl
							px-4 py-2
							border-4 border-black
							hover:border-4 hover:border-fuchsia-500
							focus:border-4 focus:border-fuchsia-500
						'
					>
						Login
					</button>
				)}
			</div>
		</div>
	)
}