import { useEffect, useState } from "react"
import Loading from "../Components/Loading"
import VoorstellingenContainer from "../Components/VoorstellingenContainer"

export default function Voorstellingen() {
    const [data, setData] = useState([])
    const [loading, setLoading] = useState(true)

    const [orderState, setOrderState] = useState("?order=prijs")


    async function getVoorstellingen() {
        await fetch(`https://localhost:7253/api/voorstelling/getvoorstellingen${orderState}`)
            .then(res => res.json())
            .then(data => {
                if (data.status == 404) return
                setData(data)
                setLoading(false)
            })
    }

    useEffect(() => {
        getVoorstellingen()
    }, [orderState])

    return (
        <>
            {!loading ? <VoorstellingenContainer states={[orderState, setOrderState]} data={data} /> : <Loading text={"VOORSTELLINGEN LADEN"} />}
        </>
    )
}