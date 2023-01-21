import React, { useEffect, useState } from 'react'
import { useAccesToken } from '../../Authentication/AuthContext'
import host from '../../Components/apiURL'

const Groepen = () => {
    const accesToken = useAccesToken()

    const [data, setData] = useState()
    const [loading, setloading] = useState(true)


    const [groepsnaam, setGroepsnaam] = useState()
    const [omschrijving, setOmschrijving] = useState()

    const [reload, setReload] = useState(0)

    useEffect(() => {
        getData()
    }, [accesToken])

    useEffect(() => {
        getData()
    }, [reload])

    async function getData() {
        setloading(true)
        await fetch(`${host}/api/groep/groepen`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                AccessToken: accesToken
            })
        }).then(response => response.json())
            .then(data => {
                setloading(false)
                setData(data)
            })
    }

    async function addGroep() {

        await fetch(`${host}/api/medewerker/addmedewerker`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                "Groepsnaam": groepsnaam,
                "Omschriving": omschrijving,
                "AccessToken": accesToken
            })
        }).then(() => {
            setReload(reload + 1)
        })
    }


    async function verwijderGroep(groepId) {
        await fetch(`${host}/api/medewerker/VerwijderMedewerker`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                "Id": groepId,
                AccessToken: accesToken
            })
        }).then(() => {
            setReload(reload + 1)
        })
    }


    return (
        <div className='mt-40'>
            <div className='w-11/12 m-auto'>
                <p className='text-3xl font-bold'>GROEPEN PORTAAL</p>

                <div>
                    <p className='font-bold text-2xl mt-6'>GROEP TOEVOEGEN</p>
                    <div className='flex gap-2 mt-2 flex-wrap'>
                        <input value={groepsnaam} onChange={(e) => setGroepsnaam(e.target.value)} className='border-2 border-black rounded-md pl-2' placeholder='Voornaam' />
                        <input value={omschrijving} onChange={(e) => setOmschrijving(e.target.value)} className='border-2 border-black rounded-md pl-2' placeholder='Achternaam' />

                        <button onClick={() => { addGroep() }} className='bg-appRed text-white font-bold rounded-md px-2'>GROEP TOEVOEGEN</button>
                    </div>
                </div>

                <div>
                    <p className='font-bold text-2xl mt-6'>ALLE GROEPEN</p>
                    {!loading && <div className='flex flex-wrap gap-4'>
                        {data.map(user => {
                            return (
                                <div className='bg-appLight text-black font-bold w-fit rounded-md px-2 py-1'>
                                    <div>
                                        <p>{user.voornaam}</p>
                                        <p>{user.achternaam}</p>
                                        <p>{user.email}</p>
                                        <p>{user.rolnaam}</p>
                                    </div>

                                    <div className='mt-2'>
                                        <p onClick={() => verwijderGroep(user.id)} className='bg-appRed text-white font-bold rounded-md px-2 w-fit cursor-pointer'>VERWIJDEREN</p>
                                    </div>
                                </div>
                            )
                        })}
                    </div>}
                </div>
            </div>
        </div >
    )
}

export default Groepen