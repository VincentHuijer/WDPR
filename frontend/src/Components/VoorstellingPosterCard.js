import { Link } from "react-router-dom";

export default function VoorstellingPosterCard({name, afbeelding, voorstellingPagelink}) {
    return (

        <div className="">
            <Link to={"/voorstelling/123"}>
                <div className="rounded-2xl posterAspect">
<<<<<<< Updated upstream
                    <img className="h-96" src="/media/AladinShow.png" />
=======
                    <img className="h-96" src={afbeelding} />
>>>>>>> Stashed changes
                </div>
            </Link>
            <p className="font-bold mt-1">{name}</p>
            <div className="font-bold mt-1">
                <Link to={voorstellingPagelink}>MEER INFO</Link>
            </div>
        </div>
    )
}