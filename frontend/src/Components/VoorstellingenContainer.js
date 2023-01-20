import VoorstellingPosterCard from "./VoorstellingPosterCard";
import { useState } from "react";

export default function VoorstellingenContainer({ data, states, userRole }) {

    const [search, setsearch] = useState("")

    return (
        <section className="w-11/12 m-auto h-fit mt-40">
            <div className='pb-4 md:pb-8'>
                <h1 className="text-4xl font-extrabold">NU IN THEATER LAAK</h1>
            </div>

            <div className="flex w-full">
                <div className="font-bold">
                    <p>Search</p>
                    <div className="flex h-10 gap-2">
                        <input name="voorstellingInputveld" value={search} onChange={(e) => setsearch(e.target.value)} className="border-appLight border-2 w-fit h-10 px-2 py-1 rounded-md" />
                        <div onClick={() => { states[1](`/titel/${search ? search : ""}`) }} className="bg-appLight w-fit px-2 ml-auto mr-0 h-10 rounded-md font-bold flex items-center cursor-pointer">
                            ZOEKEN
                        </div>
                    </div>
                </div>


                <div className="flex ml-auto mr-0 gap-3">
                    <div className="bg-appLight w-fit px-2 py-1 ml-auto mr-0 rounded-md font-bold mt-4 flex items-center cursor-pointer">
                        <button onClick={() => states[1](states[0] == "?order=prijs" ? "?order=leeftijd" : "?order=prijs")}>{states[0] == "?order=prijs" ? "Prijs - laag naar hoog" : "Leeftijd - laag naar hoog"}</button>
                    </div>
                    <div className="bg-appLight w-fit px-2 py-1 ml-auto mr-0 rounded-md font-bold mt-4 flex items-center cursor-pointer">
                        <button onClick={() => { setsearch(""); states[1]("?order=prijs") }}>RESET FILTERS</button>
                    </div>
                </div>
            </div>

            <div className="flex flex-col sm:grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-6 w-full mt-0 md:mt-8">
                {data.map(voorstelling => {
                    return <VoorstellingPosterCard key={Math.random()} name={voorstelling.voorstellingTitel.toUpperCase()} afbeelding={voorstelling.image} voorstellingPagelink={`/voorstelling/${voorstelling.voorstellingId}`} />
                })}
            </div>
        </section>
    )
}