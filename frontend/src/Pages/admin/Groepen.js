import React, { useEffect, useState } from 'react'
import { useAccesToken } from '../../Authentication/AuthContext'
import host from '../../Components/apiURL'

const Groepen = () => {
    const accesToken = useAccesToken()

    const [data, setData] = useState()
    const [loading, setloading] = useState(true)


    const [groepsnaam, setGroepsnaam] = useState()
    const [omschrijving, setOmschrijving] = useState()

    const [lidEmail, setLidEmail] = useState()

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
            .then(async data => {
                setloading(true)

                for (let i = 0; i < data.length; i++) {
                    await fetch(`${host}/api/Groep/get/${data[i].groepsId}/leden`, {
                        method: "POST",
                        body: JSON.stringify({ "AccessToken": accesToken }),
                        headers: {
                            "Content-Type": "application/json"
                        }
                    }).then(async response => {
                        data[i]["LEDEN"] = await response.json()

                        if (i == data.length - 1) {
                            setloading(false)
                        }
                    })

                }

                console.log(data);
                setData(data)
            })
    }

    async function addGroep() {

        await fetch(`${host}/api/groep/nieuwegroep`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                "Groepsnaam": groepsnaam,
                "Omschrijving": omschrijving,
                "AccessToken": accesToken
            })
        }).then(() => {
            setReload(reload + 1)
        })
    }


    async function verwijderGroep(groepId) {
        await fetch(`${host}/api/groep/verwijdergroep`, {
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

    async function verwijderLid(persoonId, groepId) {
        await fetch(`${host}/api/groep/verwijderlid`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                GroepsId: groepId.toString(),
                KlantMail: persoonId.toString(),
                AccessToken: accesToken
            })
        }).then(() => {
            setReload(reload + 1)
        })
    }

    async function addLid(groepId) {
        await fetch(`${host}/api/groep/nieuwlid`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                GroepsId: groepId,
                KlantMail: lidEmail,
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
                        <input value={groepsnaam} onChange={(e) => setGroepsnaam(e.target.value)} className='border-2 border-black rounded-md pl-2' placeholder='Naam' />
                        <input value={omschrijving} onChange={(e) => setOmschrijving(e.target.value)} className='border-2 border-black rounded-md pl-2' placeholder='Omschrijving' />

                        <button onClick={() => { addGroep() }} className='bg-appRed text-white font-bold rounded-md px-2'>GROEP TOEVOEGEN</button>
                    </div>
                </div>

                <div>
                    <p className='font-bold text-2xl mt-6'>ALLE GROEPEN</p>
                    {!loading && <div className='flex flex-wrap gap-4'>
                        {data.map(groep => {
                            return (
                                <div className='bg-appLight text-black font-bold w-full rounded-md px-2 py-2'>
                                    <div className='flex gap-6'>
                                        <div>
                                            <p>Naam:</p>
                                            <p>{groep.groepsnaam}</p>
                                        </div>
                                        <div>
                                            <p>Omschrijving:</p>
                                            <p>{groep.omschrijving}</p>
                                        </div>
                                    </div>

                                    <div className='mt-2'>
                                        <p>LEDEN:</p>
                                        <div className='flex flex-wrap gap-6'>
                                            {groep["LEDEN"].map(persoon => {
                                                return <div className='bg-white rounded-md px-2 py-1'>
                                                    <p>Voornaam: {persoon.voornaam}</p>
                                                    <p>Achternaam: {persoon.achternaam}</p>
                                                    <p>Email: {persoon.email}</p>

                                                    <div className='mt-4'>
                                                        <button onClick={() => { verwijderLid(persoon.email, groep.groepsId) }} className='bg-appRed text-white font-bold rounded-md px-2'>LID VERWIJDEREN</button>
                                                    </div>
                                                </div>
                                            })}
                                        </div>
                                    </div>

                                    <div>
                                        <p className='font-bold text-2xl mt-6'>LID TOEVOEGEN</p>
                                        <div className='flex gap-2 mt-2 flex-wrap'>
                                            <input value={lidEmail} onChange={(e) => setLidEmail(e.target.value)} className='border-2 border-black rounded-md pl-2' placeholder='Email' />

                                            <button onClick={() => { addLid(groep.groepsId) }} className='bg-appRed text-white font-bold rounded-md px-2'>LID TOEVOEGEN</button>
                                        </div>
                                    </div>

                                    <div className='mt-4'>
                                        <p onClick={() => verwijderGroep(groep.groepsId)} className='bg-appRed text-white font-bold rounded-md px-2 w-fit cursor-pointer'>GROEP VERWIJDEREN</p>
                                    </div>
                                </div>
                            )
                        })
                        }
                    </div>}
                </div>
            </div>
        </div >
    )
}

export default Groepen