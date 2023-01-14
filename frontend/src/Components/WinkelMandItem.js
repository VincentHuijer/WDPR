import moment from "moment";

export default function WinkelMandItem({ bestelObject }) {

    let plekken = bestelObject.stoelen.map(stoel => {
        return `${stoel.y + 1} - ${stoel.x + 1}`
    })

    let totaalPrijs  = 0;

    bestelObject.stoelen.map(stoel => {
        totaalPrijs += stoel.prijs
    })


    return (
        <div className="flex">
            <div className="posterAspect h-40 w-28 overflow-hidden">
                <img alt="voorstelling" className="h-full" src={bestelObject.showImage} />
            </div>

            <div className="flex flex-col justify-between ml-4">
                <div>
                    <p className="text-2xl font-bold">{bestelObject.showNaam.toString().toUpperCase()}</p>
                    <p className="text-appLightBlack font-bold text-lg">PLEKKEN: {plekken.join(" / ")}</p>
                    <p className="text-appLightBlack font-bold text-lg">DATUM: {moment(bestelObject.datum).format("DD-MM-YYYY")}</p>
                </div>
                <div>
                    <p className="text-appLightBlack font-bold text-lg">TOTAAL: €{totaalPrijs}</p>
                </div>
            </div>
        </div>
    )
}