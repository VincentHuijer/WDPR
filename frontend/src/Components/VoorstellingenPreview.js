import { Link } from "react-router-dom";
import VoorstellingPosterCard from "./VoorstellingPosterCard";

export default function VoorstellingenPreview() {
    return (
        <div className="w-11/12 m-auto mt-20 h-fit">
            <div>
                <p className="text-4xl font-extrabold">NU IN THEATER LAAK</p>
            </div>

            <div className="flex flex-wrap w-full h-fit justify-between gap-12 mt-6">
                <VoorstellingPosterCard name={"Aladin"} afbeelding={"/media/AladinShow.png"} voorstellingPagelink={"/voorstelling/Aladin"} />
                <VoorstellingPosterCard name={"Soldaat van Oranje"} afbeelding={"/media/SoldaatVanOranje.png"} voorstellingPagelink={"/voorstelling/SoldaatVanOranje"} />
                <VoorstellingPosterCard name={"Sneeuwwitje"} afbeelding={"/media/Sneeuwwitje.png"} voorstellingPagelink={"/voorstelling/Sneeuwwitje"} />
                <VoorstellingPosterCard name={"American Psycho"} afbeelding={"/media/AmericanPsycho.png"} voorstellingPagelink={"/voorstelling/AmericanPsycho"} />
                <VoorstellingPosterCard name={"Johan Cruijff"} afbeelding={"/media/JohanCruiff.png"} voorstellingPagelink={"/voorstelling/JohanCruiff"} />
            </div>

            <div className="w-full flex items-center justify-center mt-8">
                <Link to="/voorstellingen" className='border-2 border-appRed bg-appRed text-white px-14 py-2 rounded-xl font-extrabold'>BEKIJK ALLE VOORSTELLINGEN</Link>
            </div>
        </div>
    )
}