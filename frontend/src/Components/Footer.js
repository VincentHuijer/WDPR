import copyToClipboard from "../scripts/CopyToClickboard"

export default function Footer() {
    return (
        <footer className='w-full bg-black text-white pt-2 pb-8 mt-24 bottom-0' >
            <div className='w-11/12 flex flex-col gap-4 lg:gap-0 lg:flex-row items-start justify-between lg:items-center m-auto'>
                <div className="mb-auto mt-2">
                    <p className="font-bold text-xl ">LINKS</p>
                    <div className="flex flex-col mt-2 text-appLight">
                        <a href="/overons">Over Ons</a>
                        <a href="/kalender">Openingstijden & drukte</a>
                        <a href="/voorstellingen">Alle Voorstellingen</a>
                        <a href="/login">Inloggen</a>
                        <a href="/register">Registreren</a>
                    </div>
                </div>
                <div className="mb-auto mt-0">
                    <p className="font-bold text-xl mt-2"> CONTACT</p>
                    <div className="flex flex-col mt-2 text-appLight">
                        <button className="flex flex-col" type='button' id='btn' onClick={() => { copyToClipboard("+31 6 123 456 78") }}>Telnmr: +31 6 123 456 78</button>
                        <a href="mailto:info@laaktheater.nl">email: info@laaktheater.nl</a>
                        <a className="w-64">Adres: Ferrandweg 4-T, 2523 XT Den Haag</a>
                    </div>
                </div>

                <div className="mb-auto mt-2" >
                    <p className="font-bold text-xl">DONEER</p>
                    <div className="flex flex-col mt-0 text-appLight">
                        <a href="http://localhost:3000/Toegang?url=https%3A%2F%2Ftheater-laak.netlify.appl%2F"> Ondersteun het Theater door te doneren</a>
                        <button className='border-2 w-fit mt-2 border-appRed bg-appRed text-white px-3 py-1 rounded-xl font-extrabold' onClick={() => window.location.href = "https://ikdoneer.azurewebsites.net/Toegang?url=https://theaterlaakgroep2klas5.azurewebsites.net/api/donatie/permission"}> Klik hier om te doneren!</button>

                    </div>
                </div>


                <div className="mb-auto mt-2">
                    <h3 className="font-bold text-xl">NIEUWSBRIEF</h3>
                    <div className="mt-2 flex flex-col gap-2 text-appLight">
                        <p>Meld je aan voor onze nieuwsbrief!</p>
                        <input className='border-2 border-White bg-appInputBlack text-white px-3 py-1 rounded-xl font-extrabold' type="email" placeholder="Vul hier uw E-mailadres in" />
                        <button onClick={() => { }} className='border-2 w-fit mt-2 border-appRed bg-appRed text-white px-3 py-1 rounded-xl font-extrabold'>Meld je hier aan voor de nieuwsbrief!</button>
                    </div>
                </div>
            </div>
        </footer>
    )
}