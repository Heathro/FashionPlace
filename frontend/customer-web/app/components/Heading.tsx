import React from 'react';

type Props = {
	title: string;
	subtitle?: string;
	center?: boolean;
	invert?: boolean;
}

export default function Heading({ title, subtitle, center, invert }: Props) {
	return (
		<div className={center ? 'text-center' : 'text-start'}>

			{invert ? (
				<>
					<div className='font-light text-neutral-500'>
						{title}
					</div>
					<div className='text-2xl font-bold mt-2'>
						{subtitle}
					</div>
				</>
			) : (
				<>
					<div className='text-2xl font-bold'>
						{title}
					</div>
					<div className='font-light text-neutral-500 mt-2'>
						{subtitle}
					</div>
				</>
			)}


		</div>
	)
}
