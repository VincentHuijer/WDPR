import { Link } from "react-router-dom";
import VoorstellingPosterCard from "./VoorstellingPosterCard";

export default function VoorstellingenPreview() {
    return (
        <div className="w-11/12 m-auto mt-20 h-fit">
            <div className='pb-8'>
                <p className="text-4xl font-extrabold">NU IN THEATER LAAK</p>
            </div>

            <div className="flex w-full h-fit justify-between">
                <VoorstellingPosterCard name={"Aladin"} afbeelding={"/media/AladinShow.png"} voorstellingPagelink={"/voorstelling/Aladin"}/>  
                <VoorstellingPosterCard name={"Marry Poppins"}afbeelding={"/media/SoldaatVanOranje.png"}voorstellingPagelink={"/voorstelling/SoldaatVanOranje"}/>
                <VoorstellingPosterCard name ={"Sneeuwwitje"}afbeelding={"/media/Sneeuwwitje.png"}voorstellingPagelink={"/voorstelling/Sneeuwwitje"}/>
                <VoorstellingPosterCard name ={"American Psycho"}afbeelding={"/media/AmericanPsycho.png"}voorstellingPagelink={"/voorstelling/AmericanPsycho"}/>
                <VoorstellingPosterCard name={"Johan Cruijff"}afbeelding={"/media/JohanCruiff.png"}voorstellingPagelink={"/voorstelling/JohanCruiff"}/>
            </div>

            <div className="w-full flex items-center justify-center mt-8">
                <Link to="/voorstellingen" className='border-2 border-appRed bg-appRed text-white px-14 py-2 rounded-xl font-extrabold'>BEKIJK ALLE VOORSTELLINGEN</Link>
            </div>
        </div>
    )
}