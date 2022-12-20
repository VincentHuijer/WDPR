import {
    BrowserRouter as Router,
    Routes,
    Route,
    Link
} from 'react-router-dom';

export function NavBar() {
    return (
        <header className='w-full bg-white pt-2 fixed top-0 z-50' >
            <div className='w-11/12 flex justify-between items-center m-auto'>

                <div className='flex items-center gap-4'>

                        <Link to="/#"><img src='/media/tl-logo.png' /></Link>

                    <div className="flex gap-4 font-extrabold">

                            <Link to="/overons">Over Ons</Link>
                            <Link to="/voorstellingen">Alle Voorstellingen</Link>
                            <Link to="/contact">Contact Opnemen</Link>

                    </div>
                </div>

                <div className='flex gap-4'>

                        <Link to="/login" className='border-2 border-black bg-white px-3 py-1 rounded-xl font-extrabold'>INLOGGEN</Link>
                        <Link to="/register" className='border-2 border-appRed bg-appRed text-white px-3 py-1 rounded-xl font-extrabold'>REGISTREREN</Link>

                </div>
            </div>

            <div className='w-full bg-black h-9 text-white font-bold flex items-center justify-center'>
                <p>ALADIN SHOW(30-12-2022) HAS BEEN CANCELLED DUE TO REASON. </p>
            </div>
        </header>
    )
}