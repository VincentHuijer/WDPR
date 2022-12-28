import React, { useState } from "react";
import { Link } from "react-router-dom";

import { getCookie, setCookie } from "../scripts/Cookies";


export default function Login() {

    const [email, setEmail] = useState("");
    const [wachtwoord, setWachtwoord] = useState("");
    const [rememberMe, setRememberMe] = useState(false);

    const [errorMessage, setErrorMessage] = useState("")

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!email || !wachtwoord) return;

        let loginBody = {
            "Email": email.toLowerCase(),
            "Wachtwoord": wachtwoord
        }


        await fetch("https://localhost:7253/api/klant/login", {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(loginBody),
        }).then(async res => {

            if (rememberMe) {
                setCookie("session", Math.random().toString(), 7)
            } else {
                setCookie("session", Math.random().toString())
            }

            if (res.status == 200) {
                window.location.href = "/"
                return
            }

            let errorMessage = await res.text()
            setErrorMessage(errorMessage)
        });
    };


    return (
        <div className="auth-form-container w-full mt-32 pb-24">

            <p className="text-5xl font-extrabold w-full text-center pb-6 mt-16">INLOGGEN</p>

            <div className="w-3/12 bg-appSuperLightWhite m-auto rounded-2xl pb-6">
                <form className="login-form " onSubmit={handleSubmit}>

                    <div className="flex flex-col gap-1 pt-2">
                        <div className="flex flex-col w-10/12 m-auto pt-3">
                            <label className="font-bold" htmlFor="email">E-mail</label>
                            <input
                                className="focus:outline-none h-8 rounded-lg px-2"
                                value={email}
                                onChange={(e) => { setEmail(e.target.value); setErrorMessage("") }}
                                type="email"
                                placeholder=" Uw emailadres"
                                id="email"
                                name="email"
                            />
                        </div>

                        <div className="flex flex-col w-10/12 m-auto pt-3">
                            <label className="font-bold" htmlFor="wachtwoord">Wachtwoord</label>
                            <input
                                className="focus:outline-none h-8 rounded-lg px-2"
                                value={wachtwoord}
                                onChange={(e) => { setWachtwoord(e.target.value); setErrorMessage("") }}
                                type="password"
                                placeholder="*********"
                                id="wachtwoord"
                                name="wachtwoord"
                            />
                        </div>

                        <div className="w-10/12 m-auto mt-1 justify-start flex gap-2 pt-3 hover:cursor-default">
                            <input className="h-5 w-5 hover:cursor-pointer" value={rememberMe} onChange={(e) => { setRememberMe(e.target.checked) }} type="checkbox" />
                            <p className="font-bold">Onthoud mij</p>
                        </div>

                        {errorMessage != "" && <div className="w-10/12 m-auto mt-4">
                            <p className="font-bold text-appRed">{errorMessage}</p>
                        </div>}


                        <div className="w-10/12 m-auto mt-4 hover:cursor-default">
                            <button type="submit" className="hover:cursor-pointer w-full border-2 text-xl border-appRed bg-appRed text-white px-3 py-1 rounded-xl font-extrabold">INLOGGEN</button>
                        </div>

                        <div className="w-10/12 m-auto text-center flex justify-center mt-2">
                            <p className="w-fit flex font-bold">Nog geen account? <Link to={"/register"} className="text-appBlue ml-1.5">Registreer hier</Link>.</p>
                        </div>
                    </div>
                </form>
            </div>

        </div>
    );
}