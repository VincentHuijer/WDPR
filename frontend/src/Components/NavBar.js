import React, { useState } from 'react';
import {
    Link
} from 'react-router-dom';
import { useAccesToken, useUpdateAccesToken } from '../Authentication/AuthContext';
import Hero from './Hero';


export function NavBar() {
    const accesToken = useAccesToken()
    const logout = useUpdateAccesToken()


    const [openMobileNav, setOpenMobileNav] = useState(false)

    return (
        <header className='w-full bg-white pt-2 fixed top-0 z-50' >
            <div className='w-11/12 flex justify-between items-center m-auto pb-2'>

                <nav onClick={() => setOpenMobileNav(false)} className='flex items-center gap-4'>

                    <Link to="/#"><img alt='Theater Laak Logo' src='/media/tl-logo.png' /></Link>

                    <div className="gap-4 font-extrabold hidden lg:flex">
                        <Link to="/overons">Over Ons</Link>
                        <Link to="/voorstellingen">Alle Voorstellingen</Link>
                        <Link to="/overons#contact">Contact Opnemen</Link>
                    </div>
                </nav>

                <nav className='hidden lg:flex gap-4'>
                    {
                        accesToken == "none" ?
                            (<>
                                <Link to="/login" className='border-2 border-black bg-white px-3 py-1 rounded-xl font-extrabold'>INLOGGEN</Link>
                                <Link to="/register" className='border-2 border-appRed bg-appRed text-white px-3 py-1 rounded-xl font-extrabold'>REGISTREREN</Link>
                            </>)
                            : <p onClick={() => { logout("none"); window.location.href = "/" }} className='border-2 border-black bg-white px-3 py-1 rounded-xl font-extrabold cursor-pointer'>UITLOGGEN</p>}

                </nav>

                <nav className='block lg:hidden'>
                    <svg onClick={() => setOpenMobileNav(!openMobileNav)} width="35" height="35" viewBox="0 0 100 100" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path d="M17.1875 76.5625H82.8125M17.1875 51.5625H82.8125M17.1875 26.5625H82.8125" stroke="#2E2E38" stroke-width="9.375" stroke-linecap="round" stroke-linejoin="round" />
                    </svg>
                </nav>
            </div>

            {openMobileNav && <div onClick={() => setOpenMobileNav(false)}>
                <nav className='flex fixed flex-col bg-white w-full items-center justify-center pb-4 font-bold'>
                    <Link to="/overons">Over Ons</Link>
                    <Link to="/voorstellingen">Alle Voorstellingen</Link>
                    <Link to="/overons#contact">Contact Opnemen</Link>
                    <div className='mt-4 flex gap-2'>
                        {
                            accesToken == "none" ?
                                (<>
                                    <Link to="/login" className='border-2 border-black bg-white px-3 py-1 text-sm rounded-xl font-extrabold'>INLOGGEN</Link>
                                    <Link to="/register" className='border-2 border-appRed bg-appRed text-white px-3 py-1 text-sm rounded-xl font-extrabold'>REGISTREREN</Link>
                                </>)
                                : <p onClick={() => { logout("none"); window.location.href = "/" }} className='border-2 border-black bg-white px-3 py-1 text-sm rounded-xl font-extrabold cursor-pointer'>UITLOGGEN</p>}
                    </div>
                </nav>
            </div>}

            <div className='w-full bg-black h-fit py-1 text-white font-bold flex items-center justify-center'>
                <div className='w-11/12 m-auto text-center'>
                    <p>ALADIN SHOW(30-12-2022) HAS BEEN CANCELLED DUE TO REASON. </p>
                </div>
            </div>
        </header>
    )
}