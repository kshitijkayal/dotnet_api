import React from 'react'

interface Props {
    companyName: string,
    ticker: string,
    price: number
}

const Card: React.FC<Props> = ({companyName, ticker, price}: Props) : JSX.Element=> {
  return (
    <div className='card'>
      <img src="https://unsplash.com/photos/a-person-swimming-in-the-ocean-with-a-camera-NhWxAIs61MM" alt="Image" />
      <div className='details'>
        <h2>{companyName} ({ticker})</h2>
        <p>${price}</p>
      </div>
      <div className='info'>
        <p>iuhdiushwafkjwnf</p>
      </div>
    </div>
  );
}

export default Card
