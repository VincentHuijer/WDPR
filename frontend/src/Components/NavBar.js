import React, { useState, useEffect } from 'react';
import {
    Link
} from 'react-router-dom';
import { useAccesToken, useUpdateAccesToken } from '../Authentication/AuthContext';
import Hero from './Hero';


import host from './apiURL';

export function NavBar() {
    const accesToken = useAccesToken()
    const logout = useUpdateAccesToken()

    const [openSettings, setOpenSettings] = useState()

    const [openMobileNav, setOpenMobileNav] = useState(false)
    const [resetState, setResetState] = useState(false)

    const [userData, setUserData] = useState()

    const [mijnAccountTab, setMijnAccountTab] = useState(false)

    useEffect(() => {
        getUserData()
    }, [accesToken])

    async function getUserData() {

        if (!accesToken) return;
        if (accesToken == "none") return;


        try {
            await fetchData(`${host}/api/klant/klant/by/at`)
        } catch {
            await fetchData(`${host}/api/medewerker/medewerker/by/at`)
        }
    }

    async function fetchData(url) {
        await fetch(url, {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                "AccessToken": accesToken
            }),
        }).then(response => response.json()).then(data => {
            setUserData(data)
        })
    }

    async function resetRequest() {
        setResetState(true)
        await fetchData(`${host}/api/klant/request/passwordreset/${userData.email}`)
    }

    async function overalUitloggen() {
        try {
            await fetchData(`${host}/api/klant/logoutall`)
            logout("none")
        } catch {
            await fetchData(`${host}/api/medewerker/logoutall`)
            logout("none")
        }
    }

    return (
        <header className='w-full bg-white pt-2 fixed top-0 z-50' >
            <div className='w-11/12 flex justify-between items-center m-auto pb-2'>

                <nav onClick={() => setOpenMobileNav(false)} className='flex items-center gap-4'>

                    <Link to="/#"><img alt='Theater Laak Logo en Home page knop' src='/media/tl-logo.png' /></Link>

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
                            :
                            <div className='flex items-center gap-2'>
                                <div className='relative'>
                                    <svg className='cursor-pointer' onClick={() => { setOpenSettings(!openSettings) }} width="35" height="35" viewBox="0 0 100 100" fill="none" xmlns="http://www.w3.org/2000/svg">
                                        <path d="M50.0003 50C45.417 50 41.4934 48.3681 38.2295 45.1042C34.9656 41.8403 33.3337 37.9167 33.3337 33.3334C33.3337 28.75 34.9656 24.8264 38.2295 21.5625C41.4934 18.2986 45.417 16.6667 50.0003 16.6667C54.5837 16.6667 58.5073 18.2986 61.7712 21.5625C65.035 24.8264 66.667 28.75 66.667 33.3334C66.667 37.9167 65.035 41.8403 61.7712 45.1042C58.5073 48.3681 54.5837 50 50.0003 50ZM16.667 83.3334V71.6667C16.667 69.3056 17.2753 67.1347 18.492 65.1542C19.7059 63.1764 21.3198 61.6667 23.3337 60.625C27.6392 58.4722 32.0142 56.857 36.4587 55.7792C40.9031 54.7042 45.417 54.1667 50.0003 54.1667C54.5837 54.1667 59.0975 54.7042 63.542 55.7792C67.9864 56.857 72.3614 58.4722 76.667 60.625C78.6809 61.6667 80.2948 63.1764 81.5087 65.1542C82.7253 67.1347 83.3337 69.3056 83.3337 71.6667V83.3334H16.667Z" fill="black" />
                                    </svg>
                                    {openSettings && <div className='absolute mt-3 py-2 pb-4 bg-white border-black border-2 rounded-2xl w-72 float-right right-0'>
                                        <div className='text-left px-4'>
                                            <p className='font-bold'>Welkom {userData.voornaam} {userData.achternaam}</p>
                                        </div>

                                        <div className='flex flex-col divide-y-2 px-4 mt-4 divide-black font-semibold'>
                                            <div >
                                                <p onClick={() => { setMijnAccountTab(!mijnAccountTab) }} className='cursor-pointer'>Mijn Account</p>
                                                {mijnAccountTab && <div className='text-sm'>
                                                    <button onClick={() => resetRequest()} className='cursor-pointer'>{!resetState ? "Wachtwoord Resetten" : "Email Verzonden"}</button>
                                                    <br />
                                                    <p onClick={() => { overalUitloggen() }} className='cursor-pointer'>Overal Uitloggen</p>
                                                </div>}
                                            </div>

                                            {userData.rolNaam == "Admin" && <Link className='cursor-pointer' to="/admin">Admin Page</Link>}
                                            {userData.rolNaam == "Artiest" && <Link className='cursor-pointer' to="/mijngroep">Mijn Groep</Link>}
                                            {userData.rolNaam == "Medewerker" && <Link className='cursor-pointer' to="/medewerker">Medewerker Portaal</Link>}

                                            {["Klant", "Artiest", "Donateur"].includes(userData.rolNaam) &&
                                                <>
                                                    <Link onClick={() => { setOpenSettings(!openSettings) }} className='cursor-pointer' to="/user/bestellingen" name="Bestellingen">Bestellingen</Link>
                                                    <Link onClick={() => { setOpenSettings(!openSettings) }} className='cursor-pointer' to="/winkelmand" name="Winkelmand   ">Winkelmand</Link>
                                                </>}
                                        </div>

                                        <div className='px-4 text-center mt-6'>
                                            <p onClick={() => { logout("none"); window.location.href = "/" }} className='border-2 border-black bg-white px-3 py-1 text-sm rounded-xl font-extrabold cursor-pointer'>UITLOGGEN</p>
                                        </div>
                                    </div>}
                                </div>

                            </div>}
                    {/* <p onClick={() => { logout("none"); window.location.href = "/" }} className='border-2 border-black bg-white px-3 py-1 rounded-xl font-extrabold cursor-pointer'>UITLOGGEN</p>} */}

                </nav>

                <nav className='block lg:hidden'>
                    <svg onClick={() => setOpenMobileNav(!openMobileNav)} width="35" height="35" viewBox="0 0 100 100" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path d="M17.1875 76.5625H82.8125M17.1875 51.5625H82.8125M17.1875 26.5625H82.8125" stroke="#2E2E38" strokeWidth="9.375" strokeLinecap="round" strokeLinejoin="round" />
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
                                :
                                <>
                                    <p onClick={() => { logout("none"); window.location.href = "/" }} className='border-2 border-black bg-white px-3 py-1 text-sm rounded-xl font-extrabold cursor-pointer'>UITLOGGEN</p>
                                </>
                        }
                    </div>
                </nav>
            </div>}

            <div className='w-full bg-black h-fit py-1 text-white font-bold flex items-center justify-center'>
                <div className='w-11/12 m-auto text-center'>
                    <p>ALADIN SHOW(30-12-2022) HAS BEEN CANCELLED DUE TO REASON.</p>
                </div>
            </div>
        </header>
    )
}