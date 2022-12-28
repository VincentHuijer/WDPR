import React, { useState } from "react";
import { Link } from "react-router-dom";

export default function Login() {

    const [email, setEmail] = useState("");
    const [wachtwoord, setWachtwoord] = useState("");

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log(email);
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
                                onChange={(e) => setEmail(e.target.value)}
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
                                onChange={(e) => setWachtwoord(e.target.value)}
                                type="password"
                                placeholder=" *********"
                                id="wachtwoord"
                                name="wachtwoord"
                            />
                        </div>
                        
                        <div className="w-10/12 m-auto mt-1 justify-start flex gap-2 pt-3 hover:cursor-default">
                            <input className="h-5 w-5 hover:cursor-pointer" type="checkbox" />
                            <p className="font-bold">Onthoud mij</p>
                        </div>

                        <div className="w-10/12 m-auto mt-6 hover:cursor-default">
                            <button type="submit" className="hover:cursor-pointer w-full border-2 text-xl border-appRed bg-appRed text-white px-3 py-1 rounded-xl font-extrabold">INLOGGEN</button>
                        </div>

                        <div className="w-10/12 m-auto text-center flex justify-center mt-2">
                            <p className="w-fit flex font-bold">Nog geen account? <Link to={"/register"}><p className="text-appBlue ml-1.5">Registreer hier</p></Link>.</p>
                        </div>
                    </div>
                </form>
            </div>

        </div>
    );
}