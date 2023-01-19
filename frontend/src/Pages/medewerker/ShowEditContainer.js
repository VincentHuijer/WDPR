import moment from 'moment'
import React, { useEffect, useState } from 'react'
import { useAccesToken } from '../../Authentication/AuthContext'
import { Link } from 'react-router-dom'


const ShowEditContainer = ({ voorstelingData }) => {

    const [data, setData] = useState(voorstelingData)
    const [loading, setLoading] = useState(true)

    const [reload, setReload] = useState(0)

    const AccessToken = useAccesToken();
    const [date, setdate] = useState()
    const [zaalNummer, setZaalNummer] = useState()

    useEffect(() => {
        getVoorstellingData()
    }, [])

    useEffect(() => {
        getVoorstellingData()
    }, [reload])

    useEffect(() => {
        if (AccessToken == "none") return
    }, [AccessToken])

    async function getVoorstellingData() {
        setLoading(true)
        setData([])
        await fetch(`https://localhost:7253/api/show/GetShows/${data.voorstellingId}`)
            .then(res => res.json())
            .then(voorstellingData => {
                let tempObject = data
                tempObject.shows = [...voorstellingData.shows]

                setData(tempObject)
                setLoading(false)
            })
    }

    async function addShow() {
        await fetch(`https://localhost:7253/api/show/AddShow`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                Zaalnummer: Number(zaalNummer),
                StartDatum: date,
                VoorstellingId: Number(data.voorstellingId)
            })
        }).then(() => {
            setTimeout(() => {
                setReload(reload + 1)
            }, 500);
        })
    }

    async function verwijderShow() {
        await fetch(`https://localhost:7253/api/voorstelling/verwijdervoorstelling/${Number(data.voorstellingId)}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ "AccessToken": AccessToken })
        })
    }

    if (loading) {
        return <div className='mt-40 font-bold text-center w-full'>VOORSTELLINGEN LADEN</div>
    }


    return (
        <div className='pt-4'>
            <p className='text-2xl font-bold'>{data.voorstellingTitel}</p>

            <div className='mt-2'>
                {(data.shows || data.shows.length > 0) && <p className='text-xl font-bold'>Shows:</p>}
                <div className='w-full flex flex-wrap gap-2 mt-2'>
                    {(data.shows || data.shows.length > 0) && data.shows.map(show => {
                        return <Link to={`/medewerker/show/${show.showId}`} className='flex gap-2'>
                            <button className='bg-appLight px-2 py-1 rounded-full'>Zaal: <b>{show.zaalnummer}</b> Datum: <b>{moment(show.datum).format("DD-MM-YYYY HH:MM")}</b></button>
                        </Link>
                    })}
                </div>
            </div>

            <div className='mt-2'>
                <p className='text-xl font-bold'>Show Toevoegen</p>
                <div className='flex flex-col gap-2'>
                    <div>
                        <p>Datum:</p>
                        <input value={date} onChange={(e) => setdate(e.target.value)} className='border-2 border-black rounded-md' placeholder='Datum' type="date" />
                    </div>
                    <div>
                        <p>Zaalnummer:</p>
                        <input value={zaalNummer} onChange={(e) => setZaalNummer(e.target.value)} className='border-2 border-black rounded-md pl-2' placeholder='Zaal' type="Number" />
                    </div>
                </div>

                <div className='flex gap-2 mt-4'>
                    <button onClick={() => { addShow() }} className='bg-appRed text-white font-bold rounded-md px-2 py-1 mt-2'>TOEVOEGEN</button>
                    <button onClick={() => { verwijderShow() }} className='bg-white border-2 border-black text-black font-bold rounded-md px-2 py-1 mt-2'>VOORSTELLING VERWIJDEREN</button>
                </div>
            </div>
        </div>
    )
}

export default ShowEditContainer