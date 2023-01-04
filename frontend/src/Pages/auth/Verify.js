import React, { useState, useEffect } from "react"
import { useSearchParams } from "react-router-dom";

export default function Verify() {

    const [searchParams, setSearchParams] = useSearchParams();

    useEffect(() => {
        let email = searchParams.get("email")
        let token = searchParams.get("token")

        if (!email || !token) {
            window.location.href = "/"
        }

        verifyUser(email, token)
    }, [])

    const verifyUser = async (email, token) => {
        let verifyToken = {
            "Email": email.toLowerCase(),
            "Token": token,
        }


        await fetch("https://localhost:7253/api/klant/verifieer", {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(verifyToken),
        }).then(async res => {
            console.log(res.status);
            if (res.status == 200) {
                setTimeout(() => {
                    window.location.href = "/login"
                    return
                }, 1500);
            }
        });
    }

    return (
        <div className="auth-form-container w-full mt-32 pb-24">
            <div className="w-11/12 m-auto text-center">
                <h1>Uw E-mailadres is succesvol geverifieerd!</h1>
            </div>
        </div>
    )
}