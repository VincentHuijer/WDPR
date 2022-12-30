import ArticlePreview from "../Components/ArticlePreview";
import Hero from "../Components/Hero";
import VoorstellingenPreview from "../Components/VoorstellingenPreview";

import { useAccesToken } from "../Authentication/AuthContext";

export default function HomePage() {

    const accesToken = useAccesToken()

    return (
        <div className="w-full mt-28">
            <Hero />
            <ArticlePreview text={"Theater het Laak is opgericht in 2012 en heeft zich van een klein theater enorm uitgebreid"} />
            <VoorstellingenPreview />
            <p>awd {accesToken}</p>
        </div>
    )
}