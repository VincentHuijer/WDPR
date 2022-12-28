import WinkelMandItem from "../Components/WinkelMandItem"
import WinkelMandTekstItem from "../Components/WinkelMandTekstItem"

export default function WinkelMand() {
    return (
        <div className="w-full mt-40">
            <div className="w-11/12 m-auto">
                <p className="text-4xl font-extrabold">MIJN WINKELMAND</p>
            </div>

            <div className="w-11/12 m-auto h-fit mt-8">
                <div className="flex gap-12">
                    <div className="bg-appSuperLightWhite w-4/6 h-fit min-h-min rounded-2xl pb-5">
                        {/* CART CONTAINER */}
                        <div className="flex flex-col gap-9 mx-5 mt-5 ">
                            {/* CART ITEMS */}
                            <WinkelMandItem />
                            <WinkelMandItem />
                            <WinkelMandItem />
                        </div>
                    </div>

                    <div className="bg-appSuperLightWhite w-2/6 h-fit min-h-min rounded-2xl">
                        <div className="mx-5 flex flex-col gap-4 divide-y-2 divide-black">
                            <div className="flex flex-col mt-2">
                                <WinkelMandTekstItem />
                                <WinkelMandTekstItem />
                                <WinkelMandTekstItem />
                            </div>


                            {/* VOUCHER INPUT */}
                            <div className="pt-2 pb-5">
                                <p className="font-bold text-xl text-appLightBlack">TOTAAL: €267</p>
                                <button className='border-2 border-appRed bg-appRed text-white px-3 py-1 text-xl rounded-xl font-extrabold mt-3 w-full'>AFREKENEN</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}