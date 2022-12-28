export default function Kaart() {
    return (
        <div id="contact" className="w-11/12 flex flex-col gap-4 justify-between m-auto mt-20 h-full">
            <p className="text-4xl font-extrabold">HOE KUNT U ONS BEREIKEN?</p>

            <div className="w-full h-1/3 flex justify-between items-start overflow-hidden">
                <div className="flex flex-col gap-0">
                    <p className="font-semibold text-appLightBlack">Adres: Ferrandweg 4-T, 2523 XT Den Haag</p>
                    <p className="font-semibold text-appLightBlack">Email: contact@theaterlaak.com</p>
                    <p className="font-semibold text-appLightBlack">Telnmr: +31 6 12 345 678</p>
                </div>

                <div className="w-1/2">
                    <img alt="Google Maps Kaart" className="h-full w-full" src="/media/Kaart.png" />
                </div>
            </div>
        </div>
    )
}