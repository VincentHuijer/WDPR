import ArticlePreview from "../Components/ArticlePreview";
import Hero from "../Components/Hero";
import VoorstellingenPreview from "../Components/VoorstellingenPreview";
import { useState, useEffect } from "react";


export default function HomePage() {

    const [data, setData] = useState([])
    const [loading, setLoading] = useState(true)

    useEffect(() => {
        getVoorstellingData()
    }, [])

    async function getVoorstellingData() {
        await fetch(`https://localhost:7253/api/voorstelling/getVoorstellingen`)
            .then(res => res.json())
            .then(data => {
                setData(data)
                setLoading(false)
            })
    }

    return (
        <div className="w-full mt-28">
            <Hero />
            <main>
                <ArticlePreview text={"Theater het Laak is opgericht in 2012 en heeft zich van een klein theater enorm uitgebreid"} />
                <VoorstellingenPreview data={data} />
            </main>
        </div>
    )
}