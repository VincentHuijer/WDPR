import { Link } from "react-router-dom";
import VoorstellingPosterCard from "./VoorstellingPosterCard";

export default function VoorstellingenPreview({data}) {
    return (
        <section className="w-11/12 m-auto mt-20 h-fit">
            {data.length > 0 && <div>
                <h2 className="text-4xl font-extrabold">NU IN THEATER LAAK</h2>
            </div>}

            {/* <div className="flex flex-wrap w-full h-fit justify-between gap-12 mt-6"> */}
            <div className="flex flex-col sm:grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-6 w-full mt-8">
                {(data.slice(0, 5)).map(voorstelling => {
                    return <VoorstellingPosterCard key={Math.random()} name={voorstelling.voorstellingTitel.toUpperCase()} afbeelding={voorstelling.image} voorstellingPagelink={`/voorstelling/${voorstelling.voorstellingId}`} />
                })}
            </div>

            {data.length > 0 && <div className="w-full flex items-center justify-center mt-8">
                <Link to="/voorstellingen" className='border-2 border-appRed bg-appRed text-white px-14 py-2 rounded-xl font-extrabold'>BEKIJK ALLE VOORSTELLINGEN</Link>
            </div>}
        </section>
    )
}