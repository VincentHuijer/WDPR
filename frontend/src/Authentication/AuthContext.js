import React, { useState, useContext, useEffect } from 'react';

import { useCookies } from "react-cookie"

const AuthContext = React.createContext()
const AccesTokenContext = React.createContext()


export function useAccesToken() {
    return useContext(AuthContext)
}

export function useUpdateAccesToken() {
    return useContext(AccesTokenContext)
}


export function AuthProvider({ children }) {
    const [accesToken, setAccesToken] = useState("none")
    const [atCookie, setAtCookie] = useCookies(['acces_token', 'remember_me']);

    function updateAccesToken(at) {
        const date = new Date();
        date.setDate(date.getDate() + 7);

        if(atCookie.remember_me){
            setAtCookie("acces_token", at, {expires: date})
        }else{
            setAtCookie("acces_token", at)
        }
        setAccesToken(at)
    }

    useEffect(() => {
        if(!atCookie.acces_token){
            updateAccesToken("none")
            return             
        }
        
        updateAccesToken(atCookie.acces_token)
    }, [])


    return (
        <AuthContext.Provider value={accesToken}>
            <AccesTokenContext.Provider value={updateAccesToken}>
                {children}
            </AccesTokenContext.Provider>
        </AuthContext.Provider>
    )
}