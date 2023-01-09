import Row from "../Components/Zaal/Row";

import { useState, useEffect } from "react";
import Seat from "../Components/Zaal/Seat";

import ArtiestProfile from "../Components/ArtiestProfile";

import { useAccesToken } from "../Authentication/AuthContext";

export default function Voorstelling() {
  const [zaalLayout, setZaalLayout] = useState()

  // 1 = VIP
  // 2 = GEHANDICAPT
  // 3 = EERSTERANGS
  // 4 = TWEEDERANGS
  // 7 = DERDERANGS
  // 6 = GESELECTEERD
  // 5 = GERESERVEERD

  const accesToken = useAccesToken();
  const [seats, setSeats] = useState([]);

  const [types, setTypes] = useState([])

  // 6 = 4
  // 7 = 5

  useEffect(() => {
    setZaalLayout(1)
  }, [])

  useEffect(() => {
    if (zaalLayout == 1) { //240 stoelen.
      setSeats([
        [5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5],
        [5, 5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5],
        [5, 5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5],
        [5, 5, 5, 5, 4, 4, 4, 7, 7, 7, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5],
        [5, 5, 5, 5, 4, 4, 4, 4, 7, 7, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5],
        [5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5],
        [5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 5, 5, 4, 7, 7, 7, 7, 4, 4, 4, 5, 5, 5],
        [5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 7, 7, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 7, 7, 7, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 7, 5, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 7, 7, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 5, 5, 4, 4, 7, 7, 7, 7, 4, 4, 5, 5, 5],
        [5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 5, 5, 4, 7, 7, 4, 4, 4, 4, 4, 5, 5, 5],
        [4, 4, 3, 3, 3, 7, 7, 7, 3, 3, 3, 3, 4, 4],
        [3, 3, 2, 7, 7, 2, 2, 2, 2, 3],
      ]);
    }

    else if (zaalLayout == 2) { //180 stoelen.
      setSeats([
        [4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4],
        [4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4],
        [4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4],
        [4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4],
        [4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4],
        [4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4],
        [4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4],
        [4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4],
        [4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4],
        [4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4],
        [3, 3, 3, 3, 3, 3, 3, 3, 3, 3],
      ]);
    }

    else if (zaalLayout == 3) { //90 stoelen.
      setSeats([
        [4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4],
        [4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4],
        [4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4],
        [4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4],
        [4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4],
        [3, 3, 3, 3, 3, 3, 3, 3, 3, 3],
        [3, 3, 3, 3, 3, 3, 3, 3, 3, 3],
      ]);
    }
    else if (zaalLayout == 6) { //440 stoelen.
      setSeats([
        [5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5],
        [5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5],
        [5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5],
        [5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5],
        [5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5],
        [5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5],
        [5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5],
        [5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5],
        [5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5],
        [3, 3, 3, 3, 3, 3, 3, 3, 3, 3],
        [3, 3, 3, 3, 3, 3, 3, 3, 3, 3],
        [3, 3, 3, 3, 3, 3, 3, 3, 3, 3],
        [3, 3, 3, 3, 3, 3, 3, 3, 3, 3],
        [3, 3, 3, 3, 3, 3],
        [3, 3, 3, 3],
      ]);
    }

    else if (zaalLayout == 7) { //30 stoelen. ruimte layout voor kleine voorstellingen/workshops
      setSeats([
        [3, 3, 3, 3, 3, 3, 3, 3, 3, 3],
        [3, 3, 3, 3, 3, 3, 3, 3, 3, 3],
        [3, 3, 3, 3, 3, 3, 3, 3, 3, 3],
      ])
    }

    else { //geen zaal. Mockup
      setSeats([
        [5, 5, 4, 4, 4, 4, 5, 5],
        [5, 4, 4, 3, 3, 3, 4, 5],
        [3, 3, 3, 0, 3, 3, 0, 0],
        [3, 0, 0, 0, 0, 0, 3, 3],
        [2, 7, 3, 3, 7, 3, 3, 2],
        [2, 1, 1, 1, 1, 1, 2],
        [1, 1, 7, 7, 7, 1, 1],
      ]);
    }
  }, [zaalLayout]);

  useEffect(() => {
    let newArr = []

    seats.map(row => {
      // console.log([...new Set(row)]);
      newArr = [...newArr, ...[...new Set(row)]]
    })

    setTypes([...new Set(newArr)])
  }, [seats])


  function addToCart() {
    if (accesToken == "none") {
      window.location.href = "/login";
    }
  }

  //FETCH THE MATRIX AND SAVE IT WITH THE setSeats FUNCTION

  return (
    <div className="w-full mt-40">
      <div className="w-11/12 m-auto">
        <section className="flex justify-between h-1/7">
          <div className="w-full xl:w-2/7 h-fit">
            <div>
              <h2 className="text-4xl font-extrabold">SOME VOORSTELLING NAAM</h2>
              <p className="font-bold text-appLightBlack">
                20-12-2022 tot 03-04-2023
              </p>
            </div>

            <div className="mt-3">
              <p className="text-black font-bold">Leeftijd</p>
              <p className="text-appLightBlack font-bold">5+</p>
            </div>

            <div className="mt-2">
              <p className="text-black font-bold">Prijs</p>
              <p className="text-appLightBlack font-bold">€ 7, -</p>
            </div>

            <div className="mt-6">
              <p className="text-black font-bold">Beschrijving</p>
              <p className="text-appLightBlack font-bold">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
                eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut
                enim ad minim veniam, quis nostrud exercitation ullamco laboris
                nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor
                in reprehenderit in voluptate velit esse cillum dolore.
              </p>
            </div>

            {/* {!bestelPopup && <div onClick={() => setbestelPopup(true)} className='hover:cursor-pointer border-2 w-fit border-appRed bg-appRed text-white px-3 py-1 rounded-xl font-extrabold mt-6'>
                            KAARTJES BESTELLEN
                        </div>} */}

            {/* ONLY ENABLE THIS WHEN "KAARTJES BESTELLEN" HAS BEEN CLICKED */}


          </div>

          <div className="hidden xl:block">
            <div className="flex flex-col gap-8 items-end  overflow-hidden">
              <img
                className="border-2 border-black h-96 bg-black rounded-2xl"
                src="/media/AladinShow.png"
                alt="AladinFoto"
              />
            </div>
          </div>
        </section>

        <section className="mt-8">
          <div>
            <h2 className="text-4xl font-extrabold">KAARTJES BESTELLEN</h2>
          </div>

          <div className="flex gap-8 items-center mt-6">
            <p className="font-bold text-lg text-black">
              SELECTEER UW DATUM
            </p>
            <input
              className="border-2 border-appSuperLightWhite bg-appSuperLightWhite text-appLightBlack px-3 py-1 rounded-xl font-extrabold"
              type={"date"}
            />
          </div>

          <div className="mt-6">
            <div>
              <p className="font-bold text-lg text-black">
                KIES UW PLEKKEN (2x Tickets)
              </p>
            </div>

            <div className="w-96 flex flex-col lg:flex-row lg:mt-6 lg:mb-7 gap-2 pointer-events-none">
              {types.includes(1) && <div className="flex min-w-max items-center gap-2 ">
                <Seat type={1} />
                <p className="font-bold">= VIP</p>
              </div>}

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
            </div>

            {/* ZAAL MATRIX */}
            <div className="mt-6 w-full overflow-x-scroll md:overflow-x-hidden md:w-fit flex flex-col h-min gap-8 items-start">
              <div className="flex flex-col w-fit gap-2 text-center">
                {seats.map((row, i) => {
                  return <Row bg={i % 2 == 0} key={Math.random()} nmr={i} seats={row} />;
                })}

                <div className="flex w-full justify-center">
                  <p className="ml-20 mt-2 font-bold text-appLightBlack">
                    PODIUM
                  </p>
                </div>
              </div>
            </div>
          </div>

          <div
            onClick={() => {
              addToCart();
            }}
            className="hover:cursor-pointer border-2 w-fit border-appRed bg-appRed text-white px-3 py-1 rounded-xl font-extrabold mt-6"
          >
            TOEVOEGEN AAN WINKELMAND
          </div>
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
      </div>
    </div>
  );
}
