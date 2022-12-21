import Row from "../Components/Zaal/Row"

import { useState } from "react"
import Seat from "../Components/Zaal/Seat"

import ArtiestProfile from '../Components/ArtiestProfile'

export default function Voorstelling() {

    const [seats, setSeats] = useState([
        [0, 0, 0, 0, 0, 0, 0, 0],
        [0, 0, 0, 2, 1, 1, 0, 0],
        [0, 2, 2, 0, 2, 2, 0, 0],
        [0, 0, 0, 0, 0, 0, 2, 2],
        [0, 0, 2, 2, 0, 2, 2, 2],
        [0, 0, 0, 0, 0, 0, 0],
        [0, 0, 2, 2, 2, 2],
    ])

    const [bestelPopup, setbestelPopup] = useState(false)

    //FETCH THE MATRIX AND SAVE IT WITH THE setSeats FUNCTION

    return (
        <div className="w-full mt-40">
            <div className="w-11/12 m-auto">
                <div className="flex justify-between h-fit">
                    <div className="w-2/5">
                        <div>
                            <p className="text-4xl font-extrabold">SOME VOORSTELLING NAAM</p>
                            <p className="font-bold text-appLightBlack">20-12-2022 tot 03-04-2023</p>
                        </div>

                        <div className="mt-3">
                            <p className="text-black font-bold">Leeftijd</p>
                            <p className="text-appLightBlack font-bold">7+</p>
                        </div>

                        <div className="mt-2">
                            <p className="text-black font-bold">Prijs</p>
                            <p className="text-appLightBlack font-bold">€ 55, -</p>
                        </div>

                        <div className="mt-4">
                            <p className="text-black font-bold">Beschrijving</p>
                            <p className="text-appLightBlack font-bold">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore.</p>
                        </div>

                        {!bestelPopup && <div onClick={() => setbestelPopup(true)} className='hover:cursor-pointer border-2 w-fit border-appRed bg-appRed text-white px-3 py-1 rounded-xl font-extrabold mt-4'>
                            KAARTJES BESTELLEN
                        </div>}


                        {/* ONLY ENABLE THIS WHEN "KAARTJES BESTELLEN" HAS BEEN CLICKED */}

                        {bestelPopup && <div className="mt-8">
                            <div>
                                <p className="text-4xl font-extrabold">KAARTJES BESTELLEN</p>
                            </div>

                            <div className="flex gap-8 items-center mt-4">
                                <p className="font-bold text-lg text-black">SELECTEER UW DATUM</p>
                                <input className="border-2 border-appSuperLightWhite bg-appSuperLightWhite text-appLightBlack px-3 py-1 rounded-xl font-extrabold" type={"date"} />
                            </div>

                            <div className="mt-4">
                                <div>
                                    <p className="font-bold text-lg text-black">KIES UW PLEKKEN (2x Tickets)</p>
                                </div>

                                {/* ZAAL MATRIX */}
                                <div className="mt-4 w-72 flex h-min gap-8 items-start">
                                    <div className="flex flex-col gap-2 text-center">
                                        {seats.map((row, i) => {
                                            return <Row key={Math.random()} nmr={i} seats={row} />
                                        })}
                                    </div>

                                    <div className="w-96 flex flex-col gap-2">
                                        <div className="flex w-52 items-center gap-2"><Seat type={0} /><p>= NIET BESCHIKBAAR</p></div>
                                        <div className="flex w-52 items-center gap-2"><Seat type={2} /><p>= BESCHIKBAAR</p></div>
                                        <div className="flex w-52 items-center gap-2"><Seat type={1} /><p>= GESELECTEERD</p></div>
                                    </div>
                                </div>

                                <div className="flex w-72 justify-center">
                                    <p className="ml-20 mt-2 font-bold text-appLightBlack">PODIUM</p>
                                </div>
                            </div>

                            <div className='hover:cursor-pointer border-2 w-fit border-appRed bg-appRed text-white px-3 py-1 rounded-xl font-extrabold mt-4'>
                                TOEVOEGEN AAN WINKELMAND
                            </div>

                        </div>}

                    </div>

                    <div>
                        <div className="flex flex-col gap-8 h-5/6 items-end ">
                            <img className="border-2 border-black bg-black rounded-2xl" src="/media/AladinShow.png" />
                        </div>
                    </div>
                </div>


                <div className="mt-20 h-fit">
                    <div>
                        <p className="text-4xl font-extrabold">ARTIESTEN</p>
                    </div>

                    <div className="flex flex-wrap gap-12 w-full mt-4">
                        <ArtiestProfile />
                        <ArtiestProfile />
                        <ArtiestProfile />
                        <ArtiestProfile />
                        <ArtiestProfile />
                        <ArtiestProfile />
                        <ArtiestProfile />
                        <ArtiestProfile />
                        <ArtiestProfile />
                        <ArtiestProfile />
                    </div>
                </div>
            </div>
        </div>
    )
}