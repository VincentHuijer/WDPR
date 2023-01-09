import {
    Link
} from 'react-router-dom';

export default function Hero() {
    return (
        <div className="w-full pt-7 sm:pt-0">
            <div className="relative flex items-center justify-center overflow-hidden ">
                <img alt='voorpagina foto' className="h-full w-full" src="/media/heroImage.png" />

                <div className="w-11/12 flex flex-col justify-center items-start m-auto absolute text-white">
                    <p className="font-bold text-2xl lg:text-6xl">THEATER LAAK</p>
                    <p className="mt-2 font-bold text-sm lg:text-3xl">WAAR DROMEN WERKELIJKHEID WORDEN</p>
                    <p className="mt-0 font-semibold text-sm lg:text-base">Beleef een van onze vele shows!</p>

                    <Link to="/voorstellingen" className='border-2 border-appRed bg-appRed text-white px-3 py-1 rounded-xl text-sm lg:text-base font-extrabold mt-2 lg:mt-6'>BEKIJK ALLE VOORSTELLINGEN</Link>
                </div>
            </div>

        </div>
    )
}