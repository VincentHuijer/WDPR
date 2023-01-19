import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { useAccesToken } from '../../../Authentication/AuthContext'

const ShowId = () => {
    const accesToken = useAccesToken()
    const { showId } = useParams()

    const [stoelenLoading, setStoelenLoading] = useState(true)
    const [seats, setSeats] = useState([]);

    const [stoelPopup, setStoelPopup] = useState(false)
    const [stoelPopupData, setStoelPopupData] = useState()

    useEffect(() => {
        if (accesToken == "none") return

        getKaartjes()
    }, [accesToken])

    async function getKaartjes() {
        await fetch(`https://localhost:7253/api/zaal/GetShowStoelen/${showId}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ "AccessToken": accesToken })
        }).then(response => response.json()).then(data => {

            let seatsList = []
            data.map(row => {
                let seats = []
                row.map(async seat => {
                    seats.push(seat)
                })
                seatsList.push(seats)
            })

            setStoelenLoading(false)
            setSeats(seatsList)

        })
    }

    async function openPopup(cardInfo) {
        setStoelPopup(true)
        setStoelPopupData(cardInfo)
        console.log(cardInfo);
    }


    if (stoelenLoading) {
        return <div className='mt-40 font-bold text-center w-full'>SHOW LADEN</div>
    }


    return (
        <div className='mt-40'>
            <div className='w-11/12 m-auto'>
                <p className='text-3xl font-bold'>SHOW INZIEN</p>
                <div className='flex flex-col gap-3'>
                    {seats.map((row, i) => {
                        return <div className='mt-2'>
                            <p className='text-2xl font-semibold'>Rij {i + 1}</p>
                            <div className='flex flex-wrap w-full gap-5 mt-2'>
                                {row.map(stoel => {
                                    let stoelData;

                                    if (stoel.isGereserveerd) {
                                        stoelData = {
                                            x: stoel.x + 1,
                                            y: stoel.y + 1,
                                            prijs: stoel.prijs,
                                            klantInfo: stoel.klantInfo
                                        }
                                    }

                                    return <div onClick={() => { stoel.isGereserveerd && openPopup(stoelData) }} className={'px-2 py-1 rounded-md ' + (stoel.isGereserveerd ? "border-red-600 border-2 cursor-pointer" : "bg-green-200")}>
                                        <p><b>Nmr:</b> {stoel.x + 1}</p>
                                        <p><b>Rang:</b> {stoel.rang}</p>
                                        <p><b>Prijs:</b> €{stoel.prijs.toFixed(2)}</p>
                                    </div>
                                })}
                            </div>
                        </div>
                    })}
                </div>
            </div>

            {stoelPopup && <div onClick={() => (setStoelPopup(false))} className='flex items-center justify-center fixed top-0 left-0 h-full w-full bg-black bg-opacity-25'>
                <div className='w-fit h-fit  opacity-100'>
                    <div className={'px-3 py-2 rounded-md bg-white border-red-600 border-2'}>
                        <p><b>Nmr:</b> {stoelPopupData.x + 1}</p>
                        <p><b>Rang:</b> {stoelPopupData.rang}</p>
                        <p><b>Prijs:</b> €{stoelPopupData.prijs.toFixed(2)}</p>
                        <p><b>Voornaam:</b> {stoelPopupData.klantInfo.voornaam}</p>
                        <p><b>Achternaam:</b> {stoelPopupData.klantInfo.achternaam}</p>
                        <p><b>Email:</b> {stoelPopupData.klantInfo.email}</p>
                    </div>
                </div>
            </div>}
        </div>
    )
}

export default ShowId