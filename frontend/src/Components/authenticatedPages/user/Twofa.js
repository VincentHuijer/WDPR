import React, { useState, useEffect } from 'react'

import host from '../../apiURL'

export default function Twofa({ at, didSetup }) {

  const [code, setCode] = useState("")

  const [responseData, setresponseData] = useState([])

  useEffect(() => {
    const getData = async () => {
      try {
        await fetch(`${host}/api/Klant/setup2fa`, {
          method: 'POST',
          mode: 'cors',
          headers: {
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({ "AccessToken": at }),
        }).then(async res => {
          let response = await res.json()
          setresponseData(response)
        });
      } catch {
        await fetch(`${host}/api/Medewerker/setup2fa`, {
          method: 'POST',
          mode: 'cors',
          headers: {
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({ "AccessToken": at }),
        }).then(async res => {
          let response = await res.json()
          setresponseData(response)
        });
      }
    }

    getData()
  }, [])



  async function codeChange(code) {
    console.log(code);
    let codeLength = code.split("").length

    if (codeLength == 6) {
      await fetch(`${host}/api/Klant/use2fa`, {
        method: 'POST',
        mode: 'cors',
        headers: {
          'Access-Control-Allow-Origin': '*',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ "AccessToken": at, "key": code }),
      }).then(async res => {
        if (res.status == 200) {
          window.location.href = "/"
        }
      });
      await fetch(`${host}/api/Medewerker/use2fa`, {
        method: 'POST',
        mode: 'cors',
        headers: {
          'Access-Control-Allow-Origin': '*',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ "AccessToken": at, "key": code }),
      }).then(async res => {
        if (res.status == 200) {
          window.location.href = "/"
        }
      });
    }

  }



  return (
    <>
      <p className="text-5xl font-extrabold w-full text-center pb-6 mt-16">2FA {!didSetup && "SETUP"}</p>

      <div className="w-3/12 bg-appSuperLightWhite m-auto rounded-2xl pb-6">
        <div className='w-11/12 m-auto pt-2'>
          {!didSetup ? <p>Scan de onderstaande code of voer het in via uw authenticatie app op uw telefoon.</p> : <p>Vul uw 2FA code in.</p>}
          {responseData.length >= 1 && <img className='m-auto mt-4' src={responseData[0]} />}
          {responseData >= 1 && <p className='text-center m-auto mt-1'>{responseData[1]}</p>}
        </div>

        <div className='w-11/12 m-auto'>
          <input
            className="focus:outline-none h-8 rounded-lg px-2 w-full mt-4"
            value={code}
            onChange={(e) => { codeChange(e.target.value); setCode(e.target.value) }}
            type="number"
            placeholder="Uw 2FA Code"
            id="code"
            name="code"
          />
        </div>
      </div>
    </>
  )
}