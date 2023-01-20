import Row from "../Components/Zaal/Row";
import DatePicker from 'react-datepicker'
import "react-datepicker/dist/react-datepicker.css";
import moment from 'moment';

import { useState, useEffect } from "react";
import { useParams } from "react-router";
import Seat from "../Components/Zaal/Seat";

import ArtiestProfile from "../Components/ArtiestProfile";

import { useAccesToken } from "../Authentication/AuthContext";
import Loading from "../Components/Loading";
import StoelenLoading from "../Components/StoelenLoading";

import host from "../Components/apiURL";

export default function Voorstelling() {
  const accesToken = useAccesToken();
  const { id } = useParams();

  const [data, setData] = useState([])
  const [loading, setLoading] = useState(true)

  const [seats, setSeats] = useState([]);

  const [types, setTypes] = useState([])

  const [kaartjes, setKaartjes] = useState([])

  const [startDate, setStartDate] = useState();
  const [allowedDates, setAllowedDate] = useState();

  const [showList, setShowList] = useState([])
  const [showId, setShowId] = useState()

  const [stoelenLoading, setStoelenLoading] = useState(true)

  const [lowest, setLowest] = useState(100)
  const [highest, setHighest] = useState(-100)

  const [totaalPrijs, setTotaalPrijs] = useState(0)

  const [bestelLoading, setBestelLoading] = useState(false)


  const [userData, setUserData] = useState()
  const [role, setRole] = useState()


  useEffect(() => {
    getVoorstellingData()
  }, [])



  //fetch STOEL DATA
  useEffect(() => {

    let showId;
    showList.map(showLink => {
      if (moment(showLink.datum).format("DD-MM-YYYY") == moment(startDate).format("DD-MM-YYYY")) showId = showLink.showID
    })

    if (!showId) {
      console.log("something went wrong");
      return
    }

    setStoelenLoading(true)
    setKaartjes(oldArr => [])
    setBestelLoading(false)

    fetch(`${host}/api/zaal/GetShowStoelenVoorstelling/${showId}`).then(response => response.json()).then(data => {

      let seatsList = []
      data.map(row => {
        let seats = []
        row.map(async seat => {
          seats.push(seat)

          await setPrice(true, seat.prijs)
          await setPrice(false, seat.prijs)
        })

        seatsList.push(seats)
      })

      setStoelenLoading(false)
      setSeats(seatsList)
      setShowId(showId)
    })
  }, [startDate])

  async function setPrice(type, price) {
    return new Promise((resolve, reject) => {
      if (type) {
        if (price < lowest) {
          setLowest(price)
        }
      } else {
        if (price > highest) {
          setHighest(price)
        }
      }
      resolve(true)
    })
  }


  async function getVoorstellingData() {
    await fetch(`${host}/api/show/GetShows/${id}`)
      .then(res => res.json())
      .then(data => {
        if (data.status == 404) return

        let showLinks = data.shows.map(show => {
          return {
            "datum": show.datum,
            "showID": show.showId
          }

        })

        setShowList(showLinks)

        let allowedDates = []

        data.shows.sort(function (a, b) { return new Date(a.datum).getTime() - new Date(b.datum).getTime() });
        data.shows.map(show => {
          let newDate = moment(new Date(show.datum)).format("DD-MM-YYYY")
          allowedDates.push(newDate.toString())
        })

        setData(data)
        setStartDate(data.shows[0].datum)
        setStartDate(new Date(data.shows[0].datum))
        setAllowedDate(allowedDates)
        setLoading(false)
      })
  }

  function Bestel() {
    if (accesToken == "none") {
      window.location.href = "/login";
    }

    let bestelBody = {
      "ShowId": showId,
      "StoelIds": kaartjes.map(kaart => { return kaart.stoelID.toString() }),
      "AccessToken": accesToken
    }

    fetch(`${host}/api/Bestelling/nieuwebestelling`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Accept-Type": "application/json"
      },
      body: JSON.stringify(bestelBody)
    }).then(response => {
      console.log(response.text);
      window.location.href = "/winkelmand"
    })
    setBestelLoading(true)
  }


  useEffect(() => {
    let newArr = []

    seats.map(row => {
      newArr = [...newArr, ...[...new Set(row)]]
    })

    setTypes([...new Set(newArr)])
  }, [seats])

  useEffect(() => {
    let count = 0;
    kaartjes.map(kaart => {
      count += kaart.prijs
    })
    setTotaalPrijs(count)
  }, [kaartjes])


  function addStoel(stoelData) {
    setKaartjes(oldArray => [...oldArray, stoelData])
  }

  function deleteStoel(stoelData) {
    let id = stoelData.stoelID
    let arr = kaartjes.filter(kaart => kaart.stoelID != id)
    setKaartjes([...arr])
  }

  return (
    <div className="w-full mt-40">
      {!loading ? <div className="w-11/12 m-auto">
        <section className="flex justify-between h-1/7">
          <div className="w-full xl:w-2/7 h-fit">
            <div>
              <h1 className="text-4xl font-extrabold">{data.voorstelling.voorstellingTitel.toUpperCase()}</h1>
              <p className="font-bold text-appLightBlack">
                {allowedDates[0]} tot {allowedDates[allowedDates.length - 1]}
              </p>
            </div>

            <div className="mt-3">
              <p className="text-black font-bold">Leeftijd</p>
              <p className="text-appLightBlack font-bold">{data.voorstelling.leeftijd}+</p>
            </div>

            <div className="mt-2">
              <p className="text-black font-bold">Prijs</p>
              {!stoelenLoading ? <p className="text-appLightBlack font-bold">€{lowest} - €{highest}</p> : <p className="text-appLightBlack font-bold">Prijs Laden...</p>}
            </div>

            <div className="mt-6">
              <p className="text-black font-bold">Beschrijving</p>
              <p className="text-appLightBlack font-bold">
                {data.voorstelling.omschrijving}
              </p>
            </div>
          </div>

          <div className="hidden xl:block">
            <div className="flex flex-col gap-8 items-end  overflow-hidden">
              <img
                className="border-2 border-black h-96 bg-black rounded-2xl"
                src={data.voorstelling.image}
                alt="Aladin Poster Disney"
              />
            </div>
          </div>
        </section>

        <section className="mt-8">
          <div>
            <h2 className="text-4xl font-extrabold">KAARTJES BESTELLEN</h2>
          </div>

          <div className="flex gap-8 items-center mt-6">
            <p className="font-bold text-lg text-black w-56">
              SELECTEER UW DATUM
            </p>
            <div>
              <DatePicker
                className="bg-appSuperLightWhite h-9 text-center rounded-2xl"
                filterDate={(d) => allowedDates.includes(moment(d).format('DD-MM-YYYY'))}
                placeholderText={"DD-MM-YYYY"}
                selected={startDate}
                onChange={(date) => setStartDate(date)}

              />
            </div>
          </div>

          <div className="mt-6">

            {!stoelenLoading && <div className="w-96 flex flex-col lg:flex-row lg:mt-6 lg:mb-7 gap-2 pointer-events-none">
              {types.includes(3) && <div className="flex min-w-max items-center gap-2">
                <Seat type={3} />
                <p className="font-bold">= 1ᵉ RANG</p>
              </div>}

              {types.includes(4) && <div className="flex min-w-max items-center gap-2">
                <Seat type={4} />
                <p className="font-bold">= 2ᵉ RANG</p>
              </div>}

              {types.includes(5) && <div className="flex min-w-max items-center gap-2">
                <Seat type={5} />
                <p className="font-bold">= 3ᵉ RANG</p>
              </div>}

              {types.includes(2) && <div className="flex min-w-max items-center gap-2">
                <Seat type={2} />
                <p className="font-bold">= GEHANDICAPT</p>
              </div>}

              {types.includes(6) && <div className="flex min-w-max items-center gap-2">
                <Seat type={6} />
                <p className="font-bold">= GESELECTEERD</p>
              </div>}

              {types.includes(7) && <div className="flex min-w-max items-center gap-2">
                <Seat type={7} />
                <p className="font-bold">= GERESERVEERD</p>
              </div>}
            </div>}

            {/* ZAAL MATRIX */}
            <div className={"mt-6 w-full md:w-fit flex flex-col h-min gap-8 items-start " + (!stoelenLoading && "overflow-x-scroll md:overflow-x-hidden")}>
              <div className="flex flex-col w-fit gap-2 text-center">
                {!stoelenLoading ? <>
                  {seats.map((row, i) => {
                    return <Row addStoel={addStoel} deleteStoel={deleteStoel} bg={i % 2 == 0} key={`${i}-${showId}`} nmr={i} seats={row} />;
                  })}
                </> : <StoelenLoading />}

                <div className="flex w-full justify-center">
                  {!stoelenLoading && <p className="ml-20 mt-2 font-bold text-appLightBlack">
                    PODIUM
                  </p>}
                </div>
              </div>
            </div>

            {kaartjes.length > 0 && <div className="flex flex-col gap-2">
              <p className="font-bold text-2xl mt-4">Tickets:</p>
              {kaartjes.map(kaart => {
                return (
                  <div className="bg-appLight rounded-full text-center py-1 px-3 w-fit" key={`Kaartje: Stoel ${kaart.x + 1} Rij ${kaart.y + 1} ${Math.random()}`}>
                    <p>Prijs: <b>€{kaart.prijs}</b> Stoel: <b>{kaart.x + 1}</b> Rij: <b>{kaart.y + 1}</b></p>
                  </div>
                )
              })}

              <p><span className="font-bold">Totaal:</span> €{totaalPrijs}</p>
            </div>}
          </div>

          {(!stoelenLoading && kaartjes.length > 0 && kaartjes.length <= 10) && <button name="bestelButton"
            onClick={() => { if (!bestelLoading) Bestel() }}
            className={"hover:cursor-pointer border-2 w-fit border-appRed bg-appRed text-white px-3 py-1 rounded-xl font-extrabold mt-6 " + (bestelLoading && "opacity-50 hover:cursor-wait")}
          >
            TOEVOEGEN AAN WINKELMAND
          </button>}

          {kaartjes.length > 10 && <p className="text-2xl text-appRed">Sorry, U kunt maximaal 10 kaartjes bestellen.</p>}
        </section>

        <section className="mt-20 h-fit">
          <div>
            <h2 className="text-4xl font-extrabold">ARTIESTEN</h2>
          </div>

          <div className="flex flex-wrap justify-between md:justify-start gap-y-6 md:gap-12 w-full mt-6">
            <ArtiestProfile
              name={"Will Smith"}
              picture={"/media/WillSmith.png"}
              role={"Protagonist"}
            />
            <ArtiestProfile
              name={"Jessie"}
              picture={"/media/Person.png"}
              role={"Antagonist"}
            />
            <ArtiestProfile
              name={"Elizabeth"}
              picture={"/media/Person.png"}
              role={"Bijfiguur"}
            />
            <ArtiestProfile
              name={"Bo"}
              picture={"/media/Person.png"}
              role={"Bijfiguur"}
            />
            <ArtiestProfile
              name={"Malenia"}
              picture={"/media/Person.png"}
              role={"Bijfiguur"}
            />
            <ArtiestProfile
              name={"Andrea"}
              picture={"/media/Person.png"}
              role={"Bijfiguur"}
            />
          </div>
        </section>
      </div> : <Loading text={"VOORSTELLING LADEN"} />}
    </div>
  );
}
