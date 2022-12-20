import ArticlePreview from "../Components/ArticlePreview";
import Hero from "../Components/Hero";

export default function HomePage(){
    return(
        <div className="w-full mt-28">
            <Hero />
            <ArticlePreview />
        </div>
    )
}