import React, {useEffect, useState} from 'react'
import { getCookie, setCookie } from '../scripts/Cookies'

const AuthHandler = ({ children }) => {

    const [sessionToken, setsessionToken] = useState(getCookie("session"))

    const [authenticated, setauthenticated] = useState(false)

    useEffect(() => {
        if(!sessionToken){

        }else{
            setauthenticated(true)
        }
    }, [, sessionToken])
    

    return (
        <>
            <div>{children}</div>
        </>
    )
}

export default AuthHandler