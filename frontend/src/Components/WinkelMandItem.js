export default function WinkelMandItem() {
    return (
        <div className="flex">
            <div className="posterAspect">
                <img alt="voorstelling" className="h-40" src="/media/AladinShow.png" />
            </div>

            <div className="flex flex-col justify-between ml-4">
                <div>
                    <p className="text-2xl font-bold">SOME VOORSTELLING NAAM</p>
                    <p className="text-appLightBlack font-bold text-lg">PLEKKEN: 2-1 / 2-2 / 2-3</p>
                    <p className="text-appLightBlack font-bold text-lg">DATUM: 24-12-2022</p>
                </div>
                <div>
                    <p className="text-appLightBlack font-bold text-lg">TOTAAL: €165</p>
                </div>
            </div>
        </div>
    )
}