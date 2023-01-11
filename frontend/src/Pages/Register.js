import React, { useState } from "react";
import PasswordChecklist from "react-password-checklist"
import ReCAPTCHA from "react-google-recaptcha"
import CheckGegevens from "../scripts/CheckGegevens";

import useSound from 'use-sound'
import mySound from '../mario.mp3'

export default function Register() {
    const [playSound] = useSound(mySound)


    const [email, setEmail] = useState('');
    const [password, setPassword] = useState("")
    const [passwordAgain, setPasswordAgain] = useState("");
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [geboorteDatum, setGeboortedatum] = useState('');
    const [geslacht, setGeslacht] = useState('');
    const [errorMessage, setErrorMessage] = useState("");

    const [complete, setComplete] = useState(false)


    const handleSubmit = async (e) => {
        e.preventDefault();

        let checkResponse = CheckGegevens(firstName, lastName, geboorteDatum)

        if(checkResponse.split("").length > 0){
            console.log("Check response: " + checkResponse);
            return
        }

        if (!email || !password || !passwordAgain || !firstName || !lastName) return;


        if(firstName.toLowerCase() == "damion") playSound()

        let loginBody = {
            "Email": email.toLowerCase(),
            "Wachtwoord": password,
            "Voornaam": firstName,
            "Achternaam": lastName,
        }


        await fetch("https://localhost:7253/api/klant/registreer", {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(loginBody),
        }).then(async res => {
            if (res.status === 200) {
                setComplete(true)
            }

            let errorMessage = await res.text()
            setErrorMessage(errorMessage)
        });

    }

    return (
        <div className="auth-form-container w-full mt-32 pb-24">
            {!complete ? <div>
                <h1 className="text-5xl font-extrabold w-11/12 m-auto md:w-full text-left md:text-center pb-6 mt-16">REGISTREREN</h1>


                <div className="w-11/12 md:w-6/12 lg:w-4/12 bg-appSuperLightWhite m-auto rounded-2xl pb-6">
                    <form className="register-form" onSubmit={handleSubmit}>

                        <div className="flex flex-col gap-1 pt-2">
                            <div className="flex w-10/12 m-auto justify-between">
                                <div className="flex flex-col pt-3 pr-2 w-1/2">
                                    <label className="font-bold" htmlFor="firstName">Voornaam</label>
                                    <input className="focus:outline-none h-8 rounded-lg px-2" value={firstName} name="firstName" onChange={(e) => setFirstName(e.target.value)} id="firstName" placeholder="Voornaam" />
                                </div>

                                <div className="flex flex-col pt-3 pl-2 w-1/2">
                                    <label className="font-bold" htmlFor="lastName">Achternaam</label>
                                    <input className="focus:outline-none h-8 rounded-lg px-2" value={lastName} name="name" onChange={(e) => setLastName(e.target.value)} id="lastName" placeholder="Achternaam" />
                                </div>
                            </div>

                            <div className="flex w-10/12 m-auto justify-between">
                                <div className="flex flex-col pt-3 pr-2 w-1/2">
                                    <label className="font-bold" htmlFor="geboortedatum">Geboortedatum</label>
                                    <input className="focus:outline-none h-8 rounded-lg px-2" value={geboorteDatum} min="01-01-1900" onChange={(e) => setGeboortedatum(e.target.value)} type="date" placeholder="dd-mm-yyyy" id="geboortedatum" name="geboortedatum" />
                                </div>

                                <div className="flex flex-col pt-3 pl-2 w-1/2">
                                    <label className="font-bold" htmlFor="geslacht">Geslacht</label>
                                    <input className="focus:outline-none h-8 rounded-lg px-2" value={geslacht} onChange={(e) => setGeslacht(e.target.value)} type="geslacht" placeholder="MAN" id="geslacht" name="geslacht" />
                                </div>
                            </div>


                            <div className="flex flex-col w-10/12 m-auto pt-3">
                                <label className="font-bold" htmlFor="email">E-mail</label>
                                <input className="focus:outline-none h-8 rounded-lg px-2" value={email} onChange={(e) => setEmail(e.target.value)} type="email" placeholder="email@email.com" id="email" name="email" />
                            </div>


                            <div className="flex flex-col w-10/12 m-auto pt-3">
                                <label className="font-bold" htmlFor="password">Wachtwoord</label>
                                <input className="focus:outline-none h-8 rounded-lg px-2" value={password} onChange={(e) => setPassword(e.target.value)} type="password" placeholder="Uw wachtwoord" id="password" name="password" />
                            </div>

                            <div className="flex flex-col w-10/12 m-auto pt-3">
                                <label className="font-bold" htmlFor="repeatpassword">Herhaal wachtwoord</label>
                                <input className="focus:outline-none h-8 rounded-lg px-2" value={passwordAgain} onChange={(e) => setPasswordAgain(e.target.value)} type="password" placeholder="Herhaal Wachtwoord" id="repeatpassword" name="repeatpassword" />
                            </div>



                            <div className="w-10/12 m-auto pt-3">
                                <PasswordChecklist
                                    rules={["minLength", "specialChar", "number", "capital", "match"]}
                                    minLength={8}
                                    value={password}
                                    valueAgain={passwordAgain}
                                    messages={{
                                        minLength: "Minimaal 8 karakters!",
                                        specialChar: "Een special karakter!",
                                        number: "Een nummer!",
                                        capital: "Een hoofdletter!",
                                        match: "Zijn gelijk!",
                                    }}
                                />
                            </div>

                            <div className="w-full flex justify-center mt-4">
                                <ReCAPTCHA
                                    sitekey="6LeLJKwjAAAAANBwpUkGvUJz357ariQ7vhAglkl3"
                                />
                            </div>


                            {errorMessage !== "" && <div className="w-10/12 m-auto mt-4">
                                <p className="font-bold text-appRed">{errorMessage}</p>
                            </div>}

                            <div className="w-10/12 m-auto mt-6 hover:cursor-default">
                                <button type="submit" className="hover:cursor-pointer w-full border-2 text-xl border-appRed bg-appRed text-white px-3 py-1 rounded-xl font-extrabold">REGISTREREN</button>
                            </div>

                        </div>
                    </form>
                </div>
            </div> :
                        // <div className="w-10/12 m-auto mt-6 hover:cursor-default">
                        //     {/* <button type="submit" className="hover:cursor-pointer w-full border-2 text-xl border-appRed bg-appRed text-white px-3 py-1 rounded-xl font-extrabold">REGISTREREN</button> */}
                        //     <button type="submit" className="hover:cursor-pointer w-full border-2 text-xl border-appRed bg-appRed text-white px-3 py-1 rounded-xl font-extrabold" onClick={() =>{CheckGegevens(firstName, lastName, geboorteDatum, validatieCheck)}}>REGISTREREN</button>

                        // </div>

                <div className="w-11/12 m-auto">
                    <div className="text-center">
                        <p className="text-5xl font-extrabold w-full text-center pb-6 mt-16">VERIFIEER UW ACCOUNT</p>
                        {/* ILLUSTRATION HERE */}
                        <p>Klik op de link in uw email om uw account te verifieren.</p>
                    </div>
                </div>
            }
        </div>
    )
}