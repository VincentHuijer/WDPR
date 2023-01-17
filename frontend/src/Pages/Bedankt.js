import React from 'react'
import { Link } from 'react-router-dom'

const Bedankt = () => {
    return (
        <div className="auth-form-container w-full mt-32 pb-24">
            <div className="w-11/12 m-auto text-center mt-4 pb-12">
                <img className='h-60 m-auto mt-10' src='/media/applause.gif' />
                <h1 className='text-3xl font-bold mt-6'>Bedankt voor uw aankoop!</h1>

                <div className='mt-6'>
                    <Link to="/user/bestellingen" className='border-2 border-appRed bg-appRed text-white px-3 py-1 rounded-xl text-sm lg:text-base font-extrabold mt-2 lg:mt-6'>BEKIJK MIJN BESTELLINGEN</Link>
                </div>

            </div>
        </div>
    )
}

export default Bedankt