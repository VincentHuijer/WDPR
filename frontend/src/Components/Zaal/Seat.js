import { useState, useEffect } from "react"

export default function Seat({ type = 5 }) {


    const [selected, setSelected] = useState(false)
    const [tempType, setTempType] = useState(type)

    //1 = VIP
    //2 = GEHANDICAPT
    //3 = STANDAARD
    //4 = GESELECTEERD
    //5 = GERESERVEERD

    const [svg, setSvg] = useState()

    useEffect(() => {

        if (selected) {
            setTempType(4)
        } else {
            setTempType(type)
        }

        if (tempType === 1) {
            setSvg(
                <svg onClick={() => setSelected(!selected)} className="hover:cursor-pointer" width="30" height="30" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect x="0.5" y="0.5" width="39" height="39" rx="7.5" fill="#FAFF1E" stroke="black" />
                </svg>
            )
        } else if (tempType === 2) {
            setSvg(
                <svg onClick={() => setSelected(!selected)} className="hover:cursor-pointer" width="30" height="30" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect x="0.5" y="0.5" width="39" height="39" rx="7.5" fill="#FF0000" stroke="#FF0000" />
                    <path d="M24.5 25L26.15 27.2C24.9 29.6 22.3875 31.25 19.5 31.25C15.3625 31.25 12 27.8875 12 23.75C12 20.7125 13.825 18.125 16.4375 16.9375L16.7 19.65C15.3875 20.55 14.5 22.0375 14.5 23.75C14.5 26.5125 16.7375 28.75 19.5 28.75C21.825 28.75 23.7625 27.15 24.325 25H24.5ZM31.4375 25.1375L29.875 25.9125L26.375 21.25H20.6375L20.3875 18.75H24.5V16.25H20.125L19.75 12.5C21.0125 12.35 22 11.3 22 10C22 9.33696 21.7366 8.70107 21.2678 8.23223C20.7989 7.76339 20.163 7.5 19.5 7.5C18.837 7.5 18.2011 7.76339 17.7322 8.23223C17.2634 8.70107 17 9.33696 17 10V10.125L18.375 23.75H25.125L29.125 29.0875L32.5625 27.375L31.4375 25.1375Z" fill="white" />
                </svg>
            )
        } else if (tempType === 3) {
            setSvg(
                <svg onClick={() => setSelected(!selected)} className="hover:cursor-pointer" width="30" height="30" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect x="0.5" y="0.5" width="39" height="39" rx="7.5" fill="#FF0000" stroke="#FF0000" />
                </svg>
            )
        } else if (tempType === 4) {
            setSvg(
                <svg onClick={() => setSelected(!selected)} className="hover:cursor-pointer" width="30" height="30" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect x="0.5" y="0.5" width="39" height="39" rx="7.5" fill="black" stroke="black" />
                    <rect x="4" y="4" width="32" height="32" rx="4.5" fill="#EFEFEF" stroke="black" />
                    <rect x="11" y="10.5" width="18" height="18" rx="4.5" fill="black" stroke="black" />
                </svg>
            )
        } else {
            setSvg(
                <svg width="30" height="30" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect x="0.5" y="0.5" width="39" height="39" rx="7.5" fill="#EDEDED" stroke="black" />
                </svg>
            )
        }
    }, [selected,tempType])

    return svg
}