import VoorstellingPosterCard from "./VoorstellingPosterCard";

export default function VoorstellingenContainer({data}) {
    return (
        <section className="w-11/12 m-auto h-fit mt-40">
            <div className='pb-4 md:pb-8'>
                <h1 className="text-4xl font-extrabold">NU IN THEATER LAAK</h1>
            </div>

            <div className="flex flex-col sm:grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-6 w-full mt-0 md:mt-8">
                {data.map(voorstelling => {
                    return  <VoorstellingPosterCard key={Math.random()} name={voorstelling.voorstellingTitel.toUpperCase()} afbeelding={voorstelling.image} voorstellingPagelink={`/voorstelling/${voorstelling.voorstellingId}`} />
                })}
            </div>
        </section>
    )
}