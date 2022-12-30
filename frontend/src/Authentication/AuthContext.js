import React, { useState, useContext } from 'react';

const AuthContext = React.createContext()
const AccesTokenContext = React.createContext()

export function useAccesToken(){
    return useContext(AuthContext)
}

export function useUpdateAccesToken(){
    return useContext(AccesTokenContext)
}


export function AuthProvider({ children }) {
    const [accesToken, setAccesToken] = useState("awdhiuawhdiu-awdiughawd-aiwudbawd")

    function updateAccesToken(at) {
        setAccesToken(at)
    }

    return (
        <AuthContext.Provider value={accesToken}>
            <AccesTokenContext.Provider value={updateAccesToken}>
                {children}
            </AccesTokenContext.Provider>
        </AuthContext.Provider>
    )
}