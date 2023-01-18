import React, { useEffect, useState } from 'react'
import { useSearchParams } from 'react-router-dom';

const DoneerPagina = () => {

    const [searchParams, setSearchParams] = useSearchParams();

    const [token, setToken] = useState()

    const [hoeveelheid, sethoeveelheid] = useState(0.00)
    const [message, setmessage] = useState("")

    useEffect(() => {
        let tokenData = searchParams.get("token")
        setToken(tokenData)
    }, [searchParams.get("token")])

    async function doDonatie() {
        await fetch("https://ikdoneer.azurewebsites.net/api/donatie", {
            method: "POST",
            headers: { Authentication: token },
            body: JSON.stringify({
                Doel: 1234,
                Hoeveelheid: parseFloat(hoeveelheid),
                Tekst: message
            })
        })
    }


    return (
        <div className='w-11/12 m-auto mt-40'>
            <div className='text-center h-screen'>
                <div className='flex flex-col'>
                    <div className='flex gap-4'>
                        <p>Kies uw bedrag</p>
                        <input type={'number'} value={hoeveelheid} onChange={(e) => sethoeveelheid(e.target.value)} placeholder='€ 0,00' />
                    </div>

                    <div className='flex gap-4'>
                        <p>Uw Bericht:</p>
                        <input value={message} onChange={(e) => setmessage(e.target.value)} placeholder='Uw Bericht' />
                    </div>
                </div>

                <div className='w-fit ml-0 mr-auto'>
                    <button className='border-2 border-appRed bg-appRed text-white px-3 py-1 text-sm rounded-xl font-extrabold mt-4 mb-4' onClick={() => { doDonatie() }}>DONEER</button>
                </div>
            </div>
        </div>
    )
}

export default DoneerPagina