import { useEffect, useState } from "react"
import Loading from "../Components/Loading"
import WinkelMandItem from "../Components/WinkelMandItem"
import WinkelMandTekstItem from "../Components/WinkelMandTekstItem"
import { useCookies } from "react-cookie"
import host from "../Components/apiURL"

import { useNavigate } from "react-router-dom";


export default function WinkelMand() {
    const navigate = useNavigate();
    const [atCookie, setAtCookie] = useCookies(['acces_token', 'remember_me']);
    const [bestelObject, setBestelObject] = useState()

    const [isLoading, setIsLoading] = useState(true)

    const [totaalPrijs, setTotaalPrijs] = useState(0)

    const [betalingPage, setBetalingPage] = useState()

    useEffect(() => {
        getBestelling()
    }, [])

    async function getBestelling() {
        setIsLoading(true)
        fetch(`${host}/api/Bestelling/activebestelling`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept-Type": "application/json"
            },
            body: JSON.stringify({
                "AccessToken": atCookie.acces_token
            })
        }).then(response => response.json()).then(data => {
            if (data.status == 404 || !data || data.length <= 0) return


            setIsLoading(false)
            let total = 0;
            data.map(show => {
                show.stoelen.map(stoel => {
                    total += stoel.prijs
                })
            })

            setTotaalPrijs(total)
            setBestelObject(data)
        })
    }

    async function Betaal() {
        await fetch(`${host}/api/Bestelling/bestelling`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept-Type": "application/json"
            },
            body: JSON.stringify({
                "AccessToken": atCookie.acces_token
            })
        }).then(response => response.json()).then(data => {
            console.log(data)
            let bestelInfo = {
                "id": data.bestellingId,
                "prijs": data.totaalbedrag
            }
            const formData = new URLSearchParams();
            formData.append("amount", bestelInfo.prijs)
            formData.append("reference", bestelInfo.id)
            formData.append("url", `${host}/api/Betaling`)
            fetch("https://fakepay.azurewebsites.net", {
                method: "POST",
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded",
                    "Accept-Type": "application/x-www-form-urlencoded"
                },
                body: formData
            }).then(response => response.text()).then(data => {
                setBetalingPage(data)
            })
        })
    }

    async function emptyCart() {
        fetch(`${host}/api/bestelling/verwijderBestelling`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept-Type": "application/json"
            },
            body: JSON.stringify({
                "AccessToken": atCookie.acces_token
            })
        }).then(() => {
            navigate("/");
        })
    }


    return (
        <div id="main-winkelmand">
            {!isLoading ? <div>
                <div className="w-full mt-40">
                    <div className="w-11/12 m-auto">
                        <p className="text-4xl font-extrabold">MIJN WINKELMAND</p>
                    </div>


                    {betalingPage && <div className="h-screen w-screen bg-white absolute m-auto z-50 top-0" dangerouslySetInnerHTML={{ __html: betalingPage }}></div>}

                    <div className="w-11/12 m-auto h-fit mt-8">
                        <div className="flex gap-12">
                            <div className="bg-appSuperLightWhite w-4/6 h-fit min-h-min rounded-2xl pb-5">
                                {/* CART CONTAINER */}
                                <div className="flex flex-col gap-9 mx-5 mt-5 ">
                                    {/* CART ITEMS */}

                                    {bestelObject.map(bestelObject => {
                                        return <WinkelMandItem bestelObject={bestelObject} />
                                    })}

                                </div>
                            </div>

                            <div className="bg-appSuperLightWhite w-2/6 h-fit min-h-min rounded-2xl">
                                <div className="mx-5 flex flex-col gap-4 divide-y-2 divide-black">
                                    <div className="flex flex-col mt-2">
                                        {bestelObject.map(bestelObject => {
                                            return <WinkelMandTekstItem bestelObject={bestelObject} />
                                        })}
                                    </div>


                                    {/* VOUCHER INPUT */}
                                    <div className="pt-2 pb-5">
                                        <p className="font-bold text-xl text-appLightBlack">TOTAAL: €{totaalPrijs}</p>
                                        <button name="afrekenKnop" onClick={() => { Betaal() }} className='border-2 border-appRed bg-appRed text-white px-3 py-1 text-xl rounded-xl font-extrabold mt-3 w-full'>AFREKENEN</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div className="mt-10">
                            <p onClick={() => { emptyCart() }} className='cursor-pointer w-fit border-2 border-appRed bg-appRed text-white px-3 py-1 text-base rounded-xl font-extrabold mt-3'>WINEKLMAND LEEGMAKEN</p>
                        </div>
                    </div>
                </div>
            </div> : <Loading text={"WINKELMAND LADEN"} />
            }
        </div>
    )
}