import moment from "moment";
import { Link } from "react-router-dom";

export default function WinkelMandItem({ bestelObject, toPageButton = false, user = {} }) {

    let plekken = bestelObject.stoelen.map(stoel => {
        return `${stoel.y + 1} - ${stoel.x + 1}`
    })

    let totaalPrijs = 0;

    bestelObject.stoelen.map(stoel => {
        totaalPrijs += stoel.prijs
    })

    let dataString;
    if (user != {}) {
        dataString = {
            voornaam: user.voornaam,
            achternaam: user.achternaam,
            stoelen: bestelObject.stoelen,
            totaalPrijs: totaalPrijs,
            showNaam: bestelObject.showNaam.toString(),
            image: bestelObject.showImage,
            datum: moment(bestelObject.datum).format("DD-MM-YYYY HH:MM")
        }
    }

    return (
        <div className="flex relative">
            <div className="posterAspect h-40 w-28 overflow-hidden">
                <img alt="voorstelling" className="h-full" src={bestelObject.showImage} />
            </div>

            <div className="flex flex-col justify-between ml-4">
                <div>
                    <p className="text-2xl font-bold">{bestelObject.showNaam.toString().toUpperCase()}</p>
                    <p className="text-appLightBlack font-bold text-lg"><b>STOELEN:</b> {plekken.join(" / ")}</p>
                    <p className="text-appLightBlack font-bold text-lg"><b>DATUM:</b> {moment(bestelObject.datum).format("DD-MM-YYYY HH:MM")}</p>
                </div>
                <div>
                    <p className="text-appLightBlack font-bold text-lg"><b>TOTAAL:</b> €{totaalPrijs}</p>
                </div>
            </div>

            <div className="flex items-center ">
                {toPageButton && <div className="absolute right-0"><Link to={`/ticket/${btoa(JSON.stringify(dataString))}`} className='border-2 border-appRed bg-appRed text-white px-3 py-1 rounded-xl text-sm lg:text-base font-extrabold mt-2 lg:mt-6'>BEKIJK TICKETS</Link></div>}
            </div>
        </div >
    )
}