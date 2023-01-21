import React from 'react'
import { useState, useEffect } from 'react'
import { useAccesToken } from '../../Authentication/AuthContext'
import Loading from '../../Components/Loading'
import VoorstellingenContainer from '../../Components/VoorstellingenContainer'
import ShowEditContainer from './ShowEditContainer'

import host from '../../Components/apiURL'

const ShowInzien = () => {
    const [data, setData] = useState([])
    const [loading, setLoading] = useState(true)

    const [reload, setReload] = useState(0)

    const [titel, setTitel] = useState()
    const [omschrijving, setOmschrijving] = useState()
    const [image, setImage] = useState()
    const AccessToken = useAccesToken();
    async function getVoorstellingen() {
        await fetch(`${host}/api/voorstelling/getvoorstellingen?order=prijs`)
            .then(res => res.json())
            .then(async data => {
                setData(data)
                setLoading(false)
            })
    }

    async function addVoorstelling() {
        await fetch(`${host}/api/voorstelling/AddVoorstelling`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                Titel: titel,
                Omschrijving: omschrijving,
                Image: image,
                AccessToken: AccessToken 
            })
        }).then(() => {
            setTimeout(() => {
                setReload(reload + 1)
            }, 500);
        })
    }


    useEffect(() => {
        getVoorstellingen()
    }, [])

    useEffect(() => {
        getVoorstellingen()
    }, [reload])

    useEffect(() => {
        if(AccessToken=="none") return
      }, [AccessToken])

    if (loading) {
        return <div className='mt-40 font-bold text-center w-full'>VOORSTELLINGEN LADEN</div>
    }

    return (
        <div className='mt-40'>
            <div className='w-11/12 m-auto'>
                <p className='text-3xl font-bold'>ALLE VOORSTELLINGEN</p>

                <div className='py-4 rounded-md mt-3'>
                    <p className='text-xl font-bold'>Voorstelling Toevoegen</p>
                    <div className='flex gap-2 mt-2'>
                        <input value={titel} onChange={(e) => setTitel(e.target.value)} className='border-2 border-black rounded-md pl-2' placeholder='Titel' name="VoorstellingTitel" />
                        <input value={omschrijving} onChange={(e) => setOmschrijving(e.target.value)} className='border-2 border-black rounded-md pl-2' placeholder='Omschrijving' name="VoorstellingOmschrijving"/>
                        <input value={image} onChange={(e) => setImage(e.target.value)} className='border-2 border-black rounded-md pl-2' placeholder='Poster URL' name="VoorstellingPlaatje"/>

                        <button onClick={() => { addVoorstelling() }} className='bg-appRed text-white font-bold rounded-md px-2' name="VoorstellingToevoegenKnop">VOORSTELLING TOEVOEGEN</button>
                    </div>
                </div>

                <div className='mt-6 flex flex-col gap-4 divide-y-4'>
                    {data.map(voorstelling => {
                        return <ShowEditContainer voorstelingData={voorstelling} />
                    })}
                </div>
            </div>
        </div>
    )
}

export default ShowInzien