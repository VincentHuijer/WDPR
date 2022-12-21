import {
    Link
} from 'react-router-dom';

export default function Hero() {
    return (
        <div className="w-full">
            <div className="relative flex items-center justify-center overflow-hidden">
                <img className="h-full w-full" src="/media/heroImage.png" />

                <div className="w-11/12 flex flex-col justify-center items-start m-auto absolute text-white">
                    <p className="font-bold text-5xl">THEATER LAAK<br />SOMETHING SOMETHING</p>
                    <p className="mt-2 font-semibold">Some really long sentence here about theater Laak</p>

                    <Link to="/voorstellingen" className='border-2 border-appRed bg-appRed text-white px-3 py-1 rounded-xl font-extrabold mt-6'>BEKIJK ALLE VOORSTELLINGEN</Link>
                </div>
            </div>

        </div>
    )
}