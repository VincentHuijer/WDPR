import { Link } from "react-router-dom";
import VoorstellingPosterCard from "./VoorstellingPosterCard";

export default function VoorstellingenPreview() {
    return (
        <div className="w-11/12 m-auto mt-20 h-fit">
            <div className='pb-8'>
                <p className="text-4xl font-extrabold">NU IN THEATER LAAK</p>
            </div>

            <div className="flex w-full h-fit justify-between">
                <VoorstellingPosterCard />
                <VoorstellingPosterCard />
                <VoorstellingPosterCard />
                <VoorstellingPosterCard />
                <VoorstellingPosterCard />
            </div>

            <div className="w-full flex items-center justify-center mt-6">
                <Link to="/voorstellingen" className='border-2 border-appRed bg-appRed text-white px-3 py-1 rounded-xl font-extrabold'>BEKIJK ALLE VOORSTELLINGEN</Link>
            </div>
        </div>
    )
}