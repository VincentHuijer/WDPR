import { Link } from "react-router-dom";

export default function VoorstellingPosterCard({ name, afbeelding, voorstellingPagelink }) {
    return (

        <div className="w-full">
            <Link to={voorstellingPagelink}>
                <div className="rounded-2xl posterAspect">
                    <img alt={name} className=" w-full rounded-2xl" src={afbeelding} />
                </div>

                <p className="font-bold mt-1 w-11/12 mr-auto">{name.toUpperCase()}</p>
                <div className="font-bold mt-1 text-appLightBlack">
                    MEER INFO
                </div>
            </Link>
        </div>
    )
}