import ArticlePreview from "../Components/ArticlePreview";
import Hero from "../Components/Hero";
import VoorstellingenPreview from "../Components/VoorstellingenPreview";

export default function HomePage(){
    return(
        <div className="w-full mt-32">
            <Hero />
            <ArticlePreview />
            <VoorstellingenPreview />
        </div>
    )
}