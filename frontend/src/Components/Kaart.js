export default function Kaart() {
    return (
        <article id="contact" className="w-11/12 flex flex-col gap-4 justify-between m-auto mt-20 h-full">
            <h2 className="text-4xl font-extrabold">HOE KUNT U ONS BEREIKEN?</h2>

            <div className="w-full h-1/3 flex flex-col md:flex-row mdfle-row justify-between items-start overflow-hidden">
                <div className="flex flex-col gap-0">
                    <p className="font-semibold text-appLightBlack"><span className="font-bold">Adres:</span> Ferrandweg 4-T, 2523 XT Den Haag</p>
                    <p className="font-semibold text-appLightBlack"><span className="font-bold">Email:</span> contact@theaterlaak.com</p>
                    <p className="font-semibold text-appLightBlack"><span className="font-bold">Telnmr:</span> +31 6 12 345 678</p>
                </div>

                <div className="mt-2 lg:md-0 w-full md:w-1/2">
                    <img alt="Google Maps Kaart locatie Theater Laak Adres: Ferrandweg 4-T, 2523 XT Den Haag" className="h-full w-full" src="/media/Kaart.png" />
                </div>
            </div>
        </article>
    )
}