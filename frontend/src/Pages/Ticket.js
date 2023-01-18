import React, { useState, useEffect } from 'react'
import { Link } from 'react-router-dom';
import { useParams } from "react-router";

const Ticket = () => {
  let { data } = useParams();

  data = JSON.parse(atob(data))

  const [prinButton, setPrinButton] = useState(true)

  useEffect(() => {
    if (!prinButton) {
      window.print();
      setPrinButton(true)
    }
  }, [prinButton])



  return (
    <div className="w-screen px-10 h-screen z-50 bg-white m-auto">
      <h1 className='text-3xl font-bold mt-4'>{data.showNaam.toUpperCase()} TICKET{data.stoelen.length > 1 ? "S" : ""}</h1>

      {prinButton && <div>
        <button className='border-2 border-appRed bg-appRed text-white px-3 py-1 text-sm rounded-xl font-extrabold mt-4 mb-4' onClick={() => { setPrinButton(false); }}>TICKETS UITPRINTEN</button>
      </div>}

      <p className='text-xl font-bold'>Gegevens: </p>
      <p>Voornaam: {data.voornaam}</p>
      <p>Achternaam: {data.achternaam}</p>

      <div className='w-full mt-4'>
        <p className='text-2xl font-bold'>Stoelen</p>
        <div className='flex flex-col gap-6 mt-4'>
          {data.stoelen.map(stoel => {

            let ticketData = {
              Show: data.showNaam,
              StoelRij: stoel.y + 1,
              StoelNummer: stoel.x + 1,
              Rang: stoel.rang,
              Datum: data.datum
            }

            return <div key={Math.random()} className='w-fit px-2 pt-2 pb-3 flex flex-col justify-center'>
              <div className='flex justify-between items-center h-40 gap-2'>
                <div className='rounded-2xl h-full px-2 pt-2'>
                  <p><b>Show:</b> {data.showNaam}</p>
                  <p><b>Rij:</b> {stoel.y + 1}</p>
                  <p><b>Stoel:</b> {stoel.x + 1}</p>
                  <p><b>Rang:</b> {stoel.rang}</p>
                  <p><b>Datum:</b> {data.datum}</p>
                </div>

                <div className='rounded-2xl w-fit h-fit p-2'>
                  <img className='' src={` https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=${JSON.stringify(ticketData)}`} />
                </div>
              </div>
            </div>
          })}
        </div>

        {prinButton && <div className='mt-6'>
          <Link to="/user/bestellingen" className='border-2 border-appRed bg-appRed text-white px-3 py-1 text-sm rounded-xl font-extrabold mt-4 mb-4'>TERUG NAAR MIJN BESTELLINGEN</Link>
        </div>}
      </div>
    </div>
  )
}

export default Ticket