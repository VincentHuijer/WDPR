import { Link } from "react-router-dom";

export default function VoorstellingPosterCard({name, afbeelding, voorstellingPagelink}) {
    return (

        <div className="">
            <Link to={voorstellingPagelink}>
                <div className="rounded-2xl posterAspect">
                    <img alt={name} className="h-96 rounded-2xl" src={afbeelding} />
                </div>
            </Link>
            <p className="font-bold mt-1">{name.toUpperCase()}</p>
            <div className="font-bold mt-1 text-appLightBlack">
                <Link to={voorstellingPagelink}>MEER INFO</Link>
            </div>
        </div>
    )
}