import { useState, useEffect } from "react"

export default function Seat({ type = 5, row, seat, addStoel, deleteStoel }) {


    const [selected, setSelected] = useState(false)
    const [tempType, setTempType] = useState(type)

    // 1 = VIP
    // 2 = GEHANDICAPT
    // 3 = EERSTERANGS
    // 4 = TWEEDERANGS
    // 7 = DERDERANGS
    // 6 = GESELECTEERD
    // 5 = GERESERVEERD

    const [svg, setSvg] = useState()

    function changeTickets(state){
        if(state){
            addStoel(`${row}-${seat}`)
        }else{
            deleteStoel(`${row}-${seat}`)
        }
    }

    useEffect(() => {

        if (selected) {
            setTempType(6)
        } else {
            setTempType(type)
        }


        if (tempType === 2) {
            setSvg(
                <button aria-label={`Rij ${row + 1} Stoel ${seat + 1} type gehandicapte plek`}>
                    <svg onClick={() => {setSelected(!selected); changeTickets(!selected)}} className="hover:cursor-pointer" width="30" height="30" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <rect x="0.5" y="0.5" width="39" height="39" rx="7.5" fill="#FF0000" stroke="#FF0000" />
                        <path d="M24.5 25L26.15 27.2C24.9 29.6 22.3875 31.25 19.5 31.25C15.3625 31.25 12 27.8875 12 23.75C12 20.7125 13.825 18.125 16.4375 16.9375L16.7 19.65C15.3875 20.55 14.5 22.0375 14.5 23.75C14.5 26.5125 16.7375 28.75 19.5 28.75C21.825 28.75 23.7625 27.15 24.325 25H24.5ZM31.4375 25.1375L29.875 25.9125L26.375 21.25H20.6375L20.3875 18.75H24.5V16.25H20.125L19.75 12.5C21.0125 12.35 22 11.3 22 10C22 9.33696 21.7366 8.70107 21.2678 8.23223C20.7989 7.76339 20.163 7.5 19.5 7.5C18.837 7.5 18.2011 7.76339 17.7322 8.23223C17.2634 8.70107 17 9.33696 17 10V10.125L18.375 23.75H25.125L29.125 29.0875L32.5625 27.375L31.4375 25.1375Z" fill="white" />
                    </svg>
                </button>
            )
        } else if (tempType === 3) {
            setSvg(
                <button aria-label={`Rij ${row + 1} Stoel ${seat + 1} type 1e rang`}>
                    <svg onClick={() => {setSelected(!selected); changeTickets(!selected)}} width="30" height="30" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <rect x="0.5" y="0.5" width="39" height="39" rx="7.5" fill="#FFFC3B" stroke="black" />
                    </svg>
                </button>
            )
        } else if (tempType === 4) {
            setSvg(
                <button aria-label={`Rij ${row + 1} Stoel ${seat + 1} type 2e rang`}>
                    <svg onClick={() => {setSelected(!selected); changeTickets(!selected)}} width="30" height="30" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <rect x="0.5" y="0.5" width="39" height="39" rx="7.5" fill="#FE9628" stroke="black" />
                    </svg>
                </button>
            )
        } else if (tempType === 5) {
            setSvg(
                <button aria-label={`Rij ${row + 1} Stoel ${seat + 1} type 3e rang`}>
                    <svg onClick={() => {setSelected(!selected); changeTickets(!selected)}} width="30" height="30" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <rect x="0.5" y="0.5" width="39" height="39" rx="7.5" fill="#FE0C1C" stroke="black" />
                    </svg>
                </button>
            )
        }
        else if (tempType === 6) {
            setSvg(
                <button aria-label={`Rij ${row + 1} Stoel ${seat + 1} u heeft deze stoel geselecteerd.`}>
                    <svg onClick={() => {setSelected(!selected); changeTickets(!selected)}} className="hover:cursor-pointer" width="30" height="30" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <rect x="0.5" y="0.5" width="39" height="39" rx="7.5" fill="black" stroke="black" />
                        <rect x="4" y="4" width="32" height="32" rx="4.5" fill="#EFEFEF" stroke="black" />
                        <rect x="11" y="10.5" width="18" height="18" rx="4.5" fill="black" stroke="black" />
                    </svg>
                </button>
            )
        }

        else if (tempType === 7) {
            setSvg(
                <svg width="30" height="30" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect x="0.5" y="0.5" width="39" height="39" rx="7.5" fill="#EDEDED" stroke="black" />
                    <path d="M12.7071 29.2929C12.3166 29.6834 11.6834 29.6834 11.2929 29.2929L10.7071 28.7071C10.3166 28.3166 10.3166 27.6834 10.7071 27.2929L17.2929 20.7071C17.6834 20.3166 17.6834 19.6834 17.2929 19.2929L10.7071 12.7071C10.3166 12.3166 10.3166 11.6834 10.7071 11.2929L11.2929 10.7071C11.6834 10.3166 12.3166 10.3166 12.7071 10.7071L19.2929 17.2929C19.6834 17.6834 20.3166 17.6834 20.7071 17.2929L27.2929 10.7071C27.6834 10.3166 28.3166 10.3166 28.7071 10.7071L29.2929 11.2929C29.6834 11.6834 29.6834 12.3166 29.2929 12.7071L22.7071 19.2929C22.3166 19.6834 22.3166 20.3166 22.7071 20.7071L29.2929 27.2929C29.6834 27.6834 29.6834 28.3166 29.2929 28.7071L28.7071 29.2929C28.3166 29.6834 27.6834 29.6834 27.2929 29.2929L20.7071 22.7071C20.3166 22.3166 19.6834 22.3166 19.2929 22.7071L12.7071 29.2929Z" fill="black" />
                </svg>
            )
        }
    }, [selected, tempType])

    return svg
}