import React, { useState } from "react";
import { Link } from "react-router-dom";
import { setCookie } from "../scripts/Cookies";

import host from "../Components/apiURL";
import Twofa from "../Components/authenticatedPages/user/Twofa";

export default function Login() {

    const [email, setEmail] = useState("");
    const [wachtwoord, setWachtwoord] = useState("");
    const [rememberMe, setRememberMe] = useState(false);

    const [errorMessage, setErrorMessage] = useState("")

    const [isAuth, setIsAuth] = useState(false)

    const [didSetup2FA, setdidSetup2FA] = useState(true)
    const [isVerified, setIsVerified] = useState(true)

    const [at, setAt] = useState(null)


    const handleKeyDown = (event) => {
        if (event.key === 'Enter') {
            if (email == "" && wachtwoord == "") return

            console.log('do validate')
            handleSubmit()
        }
    }

    function saveAuthCookie(at, remember) {
        setCookie("remember_me", remember, 7)
        setCookie("acces_token", at)
    }

    const handleSubmit = async (e) => {
        if (e) {
            e.preventDefault();
        }

        if (!email || !wachtwoord) return;

        let hashedPassword = await sha256(wachtwoord)

        let loginBody = {
            "Email": email.toLowerCase(),
            "Wachtwoord": hashedPassword
        }


        try {
            await klantLogin(loginBody)
        } catch {
            await medewerkerLogin(loginBody)
        }
    };

    async function medewerkerLogin(loginBody) {
        await fetch(`${host}/api/medewerker/login`, {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(loginBody),
        }).then(async res => {

            if (res.status == 200) {
                let userData = await res.json();
                console.log(userData);

                if (userData.accessToken.token) {
                    saveAuthCookie(userData.accessToken.token.toString(), rememberMe)
                    setAt(userData.accessToken.token.toString())
                }

                setIsAuth(true)
                setIsVerified(true)
                setdidSetup2FA(true)


                if (!userData.twoFactorAuthSetupComplete) {
                    //SHOW 2FA VERIFY
                    setdidSetup2FA(false)
                    console.log("2FA IS NOT SETUP");
                }

                return
            }


            let errorMessage = await res.text()

            if (errorMessage.toLowerCase() == "user not verified!") {
                setIsVerified(false)

            }

            setErrorMessage(errorMessage)
        });
    }

    async function klantLogin(loginBody) {
        return new Promise(async (resolve, reject) => {
            await fetch(`${host}/api/klant/login`, {
                method: 'POST',
                mode: 'cors',
                headers: {
                    'Access-Control-Allow-Origin': '*',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(loginBody),
            }).then(async res => {

                if (res.status == 200) {
                    let userData = await res.json();
                    console.log(userData);

                    if (userData.accessToken.token) {
                        saveAuthCookie(userData.accessToken.token.toString(), rememberMe)
                        setAt(userData.accessToken.token.toString())
                    }

                    setIsAuth(true)

                    if (!userData.isVerified) {
                        //SHOW EMAIL VERIFY
                        setIsVerified(false)
                        console.log("EMAIL IS NOT VERIFIED");
                    }

                    if (!userData.twoFactorAuthSetupComplete) {
                        //SHOW 2FA VERIFY
                        setdidSetup2FA(false)
                        console.log("2FA IS NOT SETUP");
                    }

                    resolve()
                    return
                }

                reject()


                let errorMessage = await res.text()

                if (errorMessage.toLowerCase() == "user not verified!") {
                    setIsVerified(false)

                }

                setErrorMessage(errorMessage)
            });
        })
    }

    async function sha256(message) {
        const msgBuffer = new TextEncoder().encode(message);

        const hashBuffer = await crypto.subtle.digest('SHA-256', msgBuffer);

        const hashArray = Array.from(new Uint8Array(hashBuffer));

        const hashHex = hashArray.map(b => b.toString(16).padStart(2, '0')).join('');
        return hashHex;
    }


    return (
        <div className="auth-form-container w-full mt-32 pb-24">

            {isVerified ? <div>
                {!isAuth && <h1 className="text-5xl font-extrabold w-11/12 m-auto md:w-full text-left md:text-center pb-6 mt-16">INLOGGEN</h1>}

                {!isAuth ? <div className="w-11/12 md:w-6/12 lg:w-4/12 bg-appSuperLightWhite m-auto rounded-2xl pb-6">
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
                                    onKeyDown={handleKeyDown}
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
                                    onKeyDown={handleKeyDown}
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
                                <p className="w-fit flex flex-col md:flex-row font-bold">Nog geen account? <Link to={"/register"} className="text-appBlue ml-1.5">Registreer hier.</Link></p>
                            </div>
                        </div>
                    </form>
                </div> : <div> <Twofa at={at} didSetup={didSetup2FA} /></div>}
            </div> :
                <div>
                    <p className="text-5xl font-extrabold w-full text-center pb-6 mt-16">YOU ARE NOT VERIFIED</p>
                    <div className="w-11/12 m-auto text-center">
                        <p>Please verify your email first. Check your email and click on the url.</p>
                    </div>
                </div>}
        </div>
    );
}