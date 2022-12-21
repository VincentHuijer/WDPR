import { Link } from "react-router-dom";

export default function VoorstellingPosterCard() {
    return (

        <div className="">
            <Link to={"/voorstelling/123"}>
                <div className="rounded-2xl posterAspect">
                    <img className="h-80" src="/media/AladinShow.png" />
                </div>
            </Link>
            <p className="font-bold mt-1">SOME SHOW NAME</p>
            <div className="font-bold mt-1">
                <Link to={"/voorstelling/123"}>MEER INFO</Link>
            </div>
        </div>
    )
}