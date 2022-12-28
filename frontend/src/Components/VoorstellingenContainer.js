import VoorstellingPosterCard from "./VoorstellingPosterCard";

export default function VoorstellingenContainer() {
    return (
        <div className="w-11/12 m-auto h-fit mt-40">
            <div className='pb-8'>
                <p className="text-4xl font-extrabold">NU IN THEATER LAAK</p>
            </div>

            <div className="flex flex-wrap gap-4 w-full h-fit justify-between">
                <VoorstellingPosterCard name={"Aladin"} afbeelding={"/media/AladinShow.png"} voorstellingPagelink={"/voorstelling/Aladin"} />
                <VoorstellingPosterCard name={"Soldaat van Oranje"} afbeelding={"/media/SoldaatVanOranje.png"} voorstellingPagelink={"/voorstelling/SoldaatVanOranje"} />
                <VoorstellingPosterCard name={"Sneeuwwitje"} afbeelding={"/media/Sneeuwwitje.png"} voorstellingPagelink={"/voorstelling/Sneeuwwitje"} />
                <VoorstellingPosterCard name={"American Psycho"} afbeelding={"/media/AmericanPsycho.png"} voorstellingPagelink={"/voorstelling/AmericanPsycho"} />
                <VoorstellingPosterCard name={"Johan Cruijff"} afbeelding={"/media/JohanCruiff.png"} voorstellingPagelink={"/voorstelling/JohanCruiff"} />

                <VoorstellingPosterCard name={"Aladin"} afbeelding={"/media/AladinShow.png"} voorstellingPagelink={"/voorstelling/Aladin"} />
                <VoorstellingPosterCard name={"Soldaat van Oranje"} afbeelding={"/media/SoldaatVanOranje.png"} voorstellingPagelink={"/voorstelling/SoldaatVanOranje"} />
                <VoorstellingPosterCard name={"Sneeuwwitje"} afbeelding={"/media/Sneeuwwitje.png"} voorstellingPagelink={"/voorstelling/Sneeuwwitje"} />
                <VoorstellingPosterCard name={"American Psycho"} afbeelding={"/media/AmericanPsycho.png"} voorstellingPagelink={"/voorstelling/AmericanPsycho"} />
                <VoorstellingPosterCard name={"Johan Cruijff"} afbeelding={"/media/JohanCruiff.png"} voorstellingPagelink={"/voorstelling/JohanCruiff"} />
                <VoorstellingPosterCard name={"Johan Cruijff"} afbeelding={"/media/JohanCruiff.png"} voorstellingPagelink={"/voorstelling/JohanCruiff"} />
                <VoorstellingPosterCard name={"Johan Cruijff"} afbeelding={"/media/JohanCruiff.png"} voorstellingPagelink={"/voorstelling/JohanCruiff"} />
            </div>

        </div>
    )
}