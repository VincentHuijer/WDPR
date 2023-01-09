import VoorstellingPosterCard from "./VoorstellingPosterCard";

export default function VoorstellingenContainer() {
    return (
        <section className="w-11/12 m-auto h-fit mt-40">
            <div className='pb-4 md:pb-8'>
                <h2 className="text-4xl font-extrabold">NU IN THEATER LAAK</h2>
            </div>

            <div className="flex flex-col sm:grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-6 w-full mt-0 md:mt-8">
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
        </section>
    )
}