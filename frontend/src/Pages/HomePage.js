import ArticlePreview from "../Components/ArticlePreview";
import Hero from "../Components/Hero";
import VoorstellingenPreview from "../Components/VoorstellingenPreview";



export default function HomePage() {

    return (
        <div className="w-full mt-28">
            <Hero />
            <main>
                <ArticlePreview text={"Theater het Laak is opgericht in 2012 en heeft zich van een klein theater enorm uitgebreid"} />
                <VoorstellingenPreview />
            </main>
        </div>
    )
}