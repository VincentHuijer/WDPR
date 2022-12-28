import React, { useState } from "react";
import PasswordChecklist from "react-password-checklist"
import ReCAPTCHA from "react-google-recaptcha"

export default function Register() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState("")
    const [passwordAgain, setPasswordAgain] = useState("");
    const [firstName, setFirstName] = useState('');
    const [LastName, setLastName] = useState('');
    const [geboorteDatum, setGeboortedatum] = useState('');
    const [geslacht, setGeslacht] = useState('');


    const handleSubmit = (e) => {
        e.preventDefault();
        console.log(email);
    }

    return (
        <div className="auth-form-container w-full mt-32 pb-24">
            <p className="text-5xl font-extrabold w-full text-center pb-6 mt-16">REGISTREREN</p>


            <div className="w-3/12 bg-appSuperLightWhite m-auto rounded-2xl pb-6">
                <form className="register-form" onSubmit={handleSubmit}>

                    <div className="flex flex-col gap-1 pt-2">
                        <div className="flex w-10/12 m-auto justify-between">
                            <div className="flex flex-col pt-3 pr-2 w-1/2">
                                <label className="font-bold" htmlFor="firstName">Voornaam</label>
                                <input className="focus:outline-none h-8 rounded-lg px-2" value={firstName} name="firstName" onChange={(e) => setFirstName(e.target.value)} id="FirstName" placeholder="First Name" />
                            </div>

                            <div className="flex flex-col pt-3 pl-2 w-1/2">
                                <label className="font-bold" htmlFor="LastName">Achternaam</label>
                                <input className="focus:outline-none h-8 rounded-lg px-2" value={LastName} name="name" onChange={(e) => setLastName(e.target.value)} id="LastName" placeholder="Last Name" />
                            </div>
                        </div>

                        <div className="flex w-10/12 m-auto justify-between">
                            <div className="flex flex-col pt-3 pr-2 w-1/2">
                                <label className="font-bold" htmlFor="geboortedatum">Geboortedatum</label>
                                <input className="focus:outline-none h-8 rounded-lg px-2" value={geboorteDatum} onChange={(e) => setGeboortedatum(e.target.value)} type="date" placeholder="******" id="geboortedatum" name="geboortedatum" />
                            </div>

                            <div className="flex flex-col pt-3 pl-2 w-1/2">
                                <label className="font-bold" htmlFor="geslacht">Geslacht</label>
                                <input className="focus:outline-none h-8 rounded-lg px-2" value={geslacht} onChange={(e) => setGeslacht(e.target.value)} type="geslacht" placeholder="MAN" id="geslacht" name="geslacht" />
                            </div>
                        </div>


                        <div className="flex flex-col w-10/12 m-auto pt-3">
                            <label className="font-bold" htmlFor="email">E-mail</label>
                            <input className="focus:outline-none h-8 rounded-lg px-2" value={email} onChange={(e) => setEmail(e.target.value)} type="email" placeholder="naam@gmail.com" id="email" name="email" />
                        </div>


                        <div className="flex flex-col w-10/12 m-auto pt-3">
                            <label className="font-bold" htmlFor="password">Wachtwoord</label>
                            <input className="focus:outline-none h-8 rounded-lg px-2" value={password} onChange={(e) => setPassword(e.target.value)} type="password" placeholder="Uw wachtwoord" id="Password" name="password" />
                        </div>

                        <div className="flex flex-col w-10/12 m-auto pt-3">
                            <label className="font-bold" htmlFor="Password Again">Herhaal wachtwoord</label>
                            <input className="focus:outline-none h-8 rounded-lg px-2" value={passwordAgain} onChange={(e) => setPasswordAgain(e.target.value)} type="password" placeholder="Opnieuw wachtwoord" id="Password2" name="password2" />
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
                                    match: "niet overeen!",
                                }}
                            />
                        </div>

                        <div className="w-full flex justify-center mt-4">
                            <ReCAPTCHA
                                sitekey="6LeLJKwjAAAAANBwpUkGvUJz357ariQ7vhAglkl3"
                            />
                        </div>

                        <div className="w-10/12 m-auto mt-6 hover:cursor-default">
                            <button type="submit" className="hover:cursor-pointer w-full border-2 text-xl border-appRed bg-appRed text-white px-3 py-1 rounded-xl font-extrabold">REGISTREREN</button>
                        </div>

                    </div>
                </form>
            </div>
        </div>
    )
}