import React, { useEffect, useState } from 'react'
import { useAccesToken } from '../../Authentication/AuthContext'
import host from '../../Components/apiURL'

const Medewerkers = () => {
  const accesToken = useAccesToken()

  const [data, setData] = useState()
  const [loading, setloading] = useState(true)

  const [voornaam, setvoornaam] = useState()
  const [achternaam, setAchternaam] = useState()
  const [email, setEmail] = useState()
  const [wachtwoord, setWachtwoord] = useState()

  const [reload, setReload] = useState(0)



  useEffect(() => {
    getData()
  }, [accesToken])

  useEffect(() => {
    getData()
  }, [reload])


  async function getData() {
    setloading(true)
    await fetch(`${host}/api/medewerker/getmedewerkers`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        AccessToken: accesToken
      })
    }).then(response => response.json())
      .then(data => {

        setloading(false)
        setData(data)
      })
  }

  async function addMedewerker() {

    await fetch(`${host}/api/medewerker/addmedewerker`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        "Voornaam": voornaam,
        "Achternaam": achternaam,
        "Email": email,
        "Wachtwoord": await sha256(wachtwoord),
        "AccessToken": accesToken
      })
    }).then(() => {
      setReload(reload + 1)
    })
  }


  async function verwijderMedewerker(medewerkerID) {
    await fetch(`${host}/api/medewerker/VerwijderMedewerker`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        "Id": medewerkerID,
        AccessToken: accesToken
      })
    }).then(() => {
      setReload(reload + 1)
    })
  }

  async function sha256(message) {
    const msgBuffer = new TextEncoder().encode(message);

    const hashBuffer = await crypto.subtle.digest('SHA-256', msgBuffer);

    const hashArray = Array.from(new Uint8Array(hashBuffer));

    const hashHex = hashArray.map(b => b.toString(16).padStart(2, '0')).join('');
    return hashHex;
  }

  return (
    <div className='mt-40'>
      <div className='w-11/12 m-auto'>
        <p className='text-3xl font-bold'>MEDEWERKERS PORTAAL</p>

        <div>
          <p className='font-bold text-2xl mt-6'>MEDEWERKER TOEVOEGEN</p>
          <div className='flex gap-2 mt-2 flex-wrap'>
            <input value={voornaam} onChange={(e) => setvoornaam(e.target.value)} className='border-2 border-black rounded-md pl-2' placeholder='Voornaam' />
            <input value={achternaam} onChange={(e) => setAchternaam(e.target.value)} className='border-2 border-black rounded-md pl-2' placeholder='Achternaam' />
            <input value={email} onChange={(e) => setEmail(e.target.value)} className='border-2 border-black rounded-md pl-2' placeholder='Email' />
            <input type="password" value={wachtwoord} onChange={(e) => setWachtwoord(e.target.value)} className='border-2 border-black rounded-md pl-2' placeholder='Wachtwoord' />

            <button onClick={() => { addMedewerker() }} className='bg-appRed text-white font-bold rounded-md px-2'>MEDEWERKER TOEVOEGEN</button>
          </div>
        </div>

        <div>
          <p className='font-bold text-2xl mt-6'>ALLE MEDEWERKERS</p>
          {!loading && <div className='flex flex-wrap gap-4'>
            {data.map(user => {
              return (
                <div className='bg-appLight text-black font-bold w-fit rounded-md px-2 py-1'>
                  <div>
                    <p>{user.voornaam}</p>
                    <p>{user.achternaam}</p>
                    <p>{user.email}</p>
                    <p>{user.rolnaam}</p>
                  </div>

                  <div className='mt-2'>
                    <p onClick={() => verwijderMedewerker(user.id)} className='bg-appRed text-white font-bold rounded-md px-2 w-fit cursor-pointer'>VERWIJDEREN</p>
                  </div>
                </div>
              )
            })}
          </div>}
        </div>
      </div>
    </div >
  )
}

export default Medewerkers