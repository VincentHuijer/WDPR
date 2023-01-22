import React, { useState, useEffect } from 'react'
import { useAccesToken } from '../Authentication/AuthContext'

import host from '../Components/apiURL'

const MijnGroep = () => {
  const AccessToken = useAccesToken()
  const [leden, setLeden] = useState([]);
  const [loading, setLoading] = useState(true)
  useEffect(() => {
    if (AccessToken == "none") return
    laadLeden()
  }, [AccessToken])

  async function laadLeden() {
    await fetch(`${host}/api/Groep/groep/by/at`, {
      method: "POST",
      body: JSON.stringify({ "AccessToken": AccessToken }),
      headers: {
        "Content-Type": "application/json"
      }
    }).then(async response => {
      let data = await response.json()
      await fetch(`${host}/api/Groep/get/${data.groepsId}/leden`, {
        method: "POST",
        body: JSON.stringify({ "AccessToken": AccessToken }),
        headers: {
          "Content-Type": "application/json"
        }
      }).then(async response => {
        setLeden(await response.json())
        setLoading(false)
      })
    })
  }
  if (loading) {
    return <p className='mt-40'>is loading</p>
  }
  return (
    <div className='w-11/12 flex flex-row md:flex-row justify-between m-auto h-full mt-40'>
      <div className='flex flex-col'>

        <h1 className='text-2xl lg:text-4xl font-extrabold'>MIJN GROEP</h1>
        <p className='text-2xl lg-text-1xl font-semibold mt-5'>Leden:</p>
        <div className='flex flex-wrap gap-6 font-bold mt-4'>{leden.map((persoon, i) => {
          return (

            <div key={i} className='bg-appLight rounded-md px-2 py-1'>
              <p>Voornaam: {persoon.voornaam}</p>
              <p>Achternaam: {persoon.achternaam}</p>
              <p>Email: {persoon.email}</p>

            </div>
          )
        })}</div>

      </div>
    </div>
  )
}

export default MijnGroep