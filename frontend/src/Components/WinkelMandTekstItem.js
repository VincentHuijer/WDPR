export default function WinkelMandTekstItem({bestelObject}){

    let totaalPrijs  = 0;

    bestelObject.stoelen.map(stoel => {
        totaalPrijs += stoel.prijs
    })

    
    return (
        <div className="flex flex-col gap-4 py-1">
            <div className="flex flex-col gap-0">
                <p className="font-bold text-xl text-appLightBlack">{bestelObject.showNaam.toString().toUpperCase()} ({bestelObject.stoelen.length}X)</p>
                <p className="leading-4 font-bold text-appLightBlack text-lg">€{totaalPrijs}</p>
            </div>
        </div>
    )
}