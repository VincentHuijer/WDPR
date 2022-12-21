export default function Seat({ type = 0}) {

    //SELECTED
    if (type == 1) {
        return (
            <svg className="hover:cursor-pointer" width="30" height="30" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                <rect x="0.5" y="0.5" width="39" height="39" rx="7.5" fill="#FF0000" stroke="black" />
                <rect x="6" y="6" width="28" height="28" rx="8" fill="black" />
            </svg>
        )

        //AVAILABLE
    } else if (type == 2) {
        return (
            <svg className="hover:cursor-pointer" width="30" height="30" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                <rect x="0.5" y="0.5" width="39" height="39" rx="7.5" fill="#FF0000" stroke="#FF0000" />
            </svg>
        )


        //NOT AVAILABLE 0
    } else {
        return (
            <svg width="30" height="30" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                <rect x="0.5" y="0.5" width="39" height="39" rx="7.5" fill="#EFEFEF" stroke="black" />
            </svg>
        )
    }
}