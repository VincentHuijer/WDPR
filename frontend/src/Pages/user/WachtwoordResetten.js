import React, { useState, useEffect } from 'react'
import PasswordChecklist from "react-password-checklist"
import { useSearchParams } from 'react-router-dom';

const WachtwoordResetten = () => {
    const [searchParams, setSearchParams] = useSearchParams();
    let email = searchParams.get("email")
    let token = searchParams.get("token")

    const [wachtwoord, setWachtwoord] = useState("")
    const [wachtwoordAgain, setWachtwoordAgain] = useState("")

    const [emailState, setEmail] = useState()
    const [at, setAt] = useState()


    useEffect(() => {
        setEmail(email)
        setAt(token)
    }, [email, token])



    async function resetWachtwoord() {
        fetch(`https://localhost:7253/api/klant/complete/passwordreset/${emailState}`, {
            method: 'POST',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                "AuthenticatieToken": at,
                "NieuwWachtwoord": await (sha256(wachtwoord))
            }),
        }).then(response => {
            if (response.status == 200) {
                window.location.href = "/login"
            }
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
        <div className='w-11/12 m-auto mt-40'>
            <div className=''>
                <p className='font-bold text-3xl'>WACHTWOORD RESETTEN</p>

                <div className='flex flex-col gap-2 py-6'>
                    <p>Vul Hier Uw Nieuw Wachtwoord In</p>
                    <input className="focus:outline-none w-fit h-8 px-2 border-black border-2 rounded-md" placeholder='Nieuw Wachtwoord' type="password" value={wachtwoord} onChange={(e) => { setWachtwoord(e.target.value) }} />
                    <input className="focus:outline-none w-fit h-8 px-2 border-black border-2 rounded-md" placeholder='Herhaal Wachtwoord' type="password" value={wachtwoordAgain} onChange={(e) => { setWachtwoordAgain(e.target.value) }} />
                </div>
                <div>
                    <PasswordChecklist
                        rules={["minLength", "specialChar", "number", "capital", "match"]}
                        minLength={8}
                        value={wachtwoord}
                        valueAgain={wachtwoordAgain}
                        messages={{
                            minLength: "Minimaal 8 karakters!",
                            specialChar: "Een special karakter!",
                            number: "Een nummer!",
                            capital: "Een hoofdletter!",
                            match: "Zijn gelijk!",
                        }}
                    />
                </div>

                <button onClick={() => { resetWachtwoord() }} className='border-2 border-appRed bg-appRed text-white px-3 py-1 rounded-xl text-sm lg:text-base font-extrabold mt-2 lg:mt-6'>WACHTWOORD RESETTEN</button>
            </div>
        </div>
    )
}

export default WachtwoordResetten