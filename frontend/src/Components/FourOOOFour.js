import React, { useState } from 'react'
import { Link } from 'react-router-dom';

const FourOOOFour = () => {

    const [timer, setTimer] = useState(5)

    setTimeout(() => {
        if (timer == 0) {
            window.location.href = "/"
        } else {
            setTimer(timer - 1)
        }
    }, 1000);

    return (
        <div className='w-11/12 m-auto'>
            <div className='text-center h-screen flex items-center justify-center'>
                <div>
                    <img className='h-56 m-auto' src="/media/404.gif" />
                    <h1 className="text-3xl font-extrabold mt-6">404</h1>
                    <h1 className="text-4xl font-semibold mt-4">PAGINA NIET GEVONDEN</h1>
                    <p>U wordt over <b>{timer}</b> seconden naar de <Link to={"/"} className="underline font-bold" name="redirect">homepagina</Link> gestuurd</p>
                </div>
            </div>
        </div>
    )
}

export default FourOOOFour