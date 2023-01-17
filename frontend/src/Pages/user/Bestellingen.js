import React, { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import { useAccesToken } from '../../Authentication/AuthContext'
import WinkelMandItem from '../../Components/WinkelMandItem'

const Bestellingen = () => {
    const accesToken = useAccesToken()

    const [data, setData] = useState([])
    const [userData, setUserData] = useState()
    const [loading, setLoading] = useState(true)


    useEffect(() => {
        fetchData()
    }, [accesToken])

    async function fetchData() {
        await fetch("https://localhost:7253/api/Bestelling/getbestelling/by/at", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept-Type": "application/json"
            },
            body: JSON.stringify({
                "AccessToken": accesToken
            })
        }).then(response => response.json()).then(async data => {
            if (data.status == 404 || !data || data.length <= 0) return


            setData(data.reverse())

            await fetch("https://localhost:7253/api/klant/klant/by/at", {
                method: 'POST',
                mode: 'cors',
                headers: {
                    'Access-Control-Allow-Origin': '*',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    "AccessToken": accesToken
                }),
            }).then(response => response.json()).then(data => {
                console.log(data);
                setUserData(data)
                setLoading(false)
            })
        })
    }

    return (
        <div className='w-11/12 m-auto mt-40'>
            <h1 className="text-4xl font-extrabold">MIJN BESTELLINGEN</h1>
            <div className=" w-full m-auto h-fit min-h-min pb-5">
                {/* CART CONTAINER */}
                <div className="flex flex-col gap-6 mt-5">
                    {!loading && data.map(bestelObject => {
                        return <div className="bg-appSuperLightWhite rounded-2xl p-3"><WinkelMandItem user={userData} toPageButton={true} bestelObject={bestelObject} /></div>
                    })}
                </div>
            </div>
        </div>
    )
}

export default Bestellingen