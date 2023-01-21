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
                        {/* <a> Telnmr: +31 6 123 456 78</a> */}
                        <button className="flex flex-col" type='button' id='btn' onClick={() => { copyToClipboard("+31 6 123 456 78") }}>Telnmr: +31 6 123 456 78</button>
                        <a href="mailto:info@laaktheater.nl">email: info@laaktheater.nl</a>
                        <a className="w-64">Adres: Ferrandweg 4-T, 2523 XT Den Haag</a>
                    </div>
                </div>

                {/* <div className="mb-auto mt-0">
                    <p className="font-bold text-xl mt-2">SOCIAL MEDIA</p>
                    <div className="flex mt-2 gap-2 text-appLight">
                        <svg width="28" height="27" viewBox="0 0 28 27" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M24.3801 0C25.1757 0 25.9388 0.316071 26.5014 0.87868C27.064 1.44129 27.3801 2.20435 27.3801 3V24C27.3801 24.7957 27.064 25.5587 26.5014 26.1213C25.9388 26.684 25.1757 27 24.3801 27H3.38005C2.5844 27 1.82134 26.684 1.25873 26.1213C0.696121 25.5587 0.380051 24.7957 0.380051 24V3C0.380051 2.20435 0.696121 1.44129 1.25873 0.87868C1.82134 0.316071 2.5844 0 3.38005 0H24.3801ZM23.6301 23.25V15.3C23.6301 14.0031 23.1149 12.7593 22.1978 11.8423C21.2808 10.9252 20.037 10.41 18.7401 10.41C17.4651 10.41 15.9801 11.19 15.2601 12.36V10.695H11.0751V23.25H15.2601V15.855C15.2601 14.7 16.1901 13.755 17.3451 13.755C17.902 13.755 18.4362 13.9763 18.83 14.3701C19.2238 14.7639 19.4451 15.2981 19.4451 15.855V23.25H23.6301ZM6.20006 8.34001C6.8684 8.34001 7.50938 8.07451 7.98197 7.60192C8.45456 7.12932 8.72006 6.48835 8.72006 5.82001C8.72006 4.425 7.59506 3.285 6.20006 3.285C5.52773 3.285 4.88294 3.55208 4.40754 4.02749C3.93213 4.50289 3.66505 5.14768 3.66505 5.82001C3.66505 7.21501 4.80506 8.34001 6.20006 8.34001ZM8.28506 23.25V10.695H4.13005V23.25H8.28506Z" fill="#D1D1D1" />
                        </svg>

                        <svg width="32" height="26" viewBox="0 0 32 26" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M31.3801 3C30.2251 3.525 28.9801 3.87 27.6901 4.035C29.0101 3.24 30.0301 1.98 30.5101 0.465C29.2651 1.215 27.8851 1.74 26.4301 2.04C25.2451 0.750001 23.5801 0 21.6901 0C18.165 0 15.285 2.88 15.285 6.43501C15.285 6.94501 15.345 7.44001 15.45 7.90501C10.11 7.63501 5.35504 5.07 2.19003 1.185C1.63503 2.13 1.32003 3.24 1.32003 4.41C1.32003 6.64501 2.44503 8.62501 4.18503 9.75001C3.12003 9.75001 2.13003 9.45001 1.26003 9.00001V9.04501C1.26003 12.165 3.48003 14.775 6.42004 15.36C5.47613 15.6183 4.48518 15.6543 3.52503 15.465C3.93244 16.7437 4.73034 17.8626 5.80656 18.6644C6.88278 19.4662 8.18321 19.9106 9.52504 19.935C7.25048 21.7357 4.43103 22.709 1.53003 22.695C1.02003 22.695 0.510031 22.665 3.05176e-05 22.605C2.85003 24.435 6.24004 25.5 9.87004 25.5C21.6901 25.5 28.1851 15.69 28.1851 7.18501C28.1851 6.90001 28.1851 6.63 28.1701 6.34501C29.4301 5.445 30.5101 4.305 31.3801 3Z" fill="#D1D1D1" />
                        </svg>

                        <svg width="30" height="30" viewBox="0 0 30 30" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M8.70001 0H21.3C26.1 0 30 3.9 30 8.70001V21.3C30 23.6074 29.0834 25.8203 27.4519 27.4519C25.8203 29.0834 23.6074 30 21.3 30H8.70001C3.9 30 0 26.1 0 21.3V8.70001C0 6.39262 0.916605 4.17974 2.54817 2.54817C4.17974 0.916605 6.39262 0 8.70001 0ZM8.40001 3C6.96784 3 5.59433 3.56893 4.58163 4.58163C3.56893 5.59433 3 6.96784 3 8.40001V21.6C3 24.585 5.415 27 8.40001 27H21.6C23.0322 27 24.4057 26.4311 25.4184 25.4184C26.4311 24.4057 27 23.0322 27 21.6V8.40001C27 5.415 24.585 3 21.6 3H8.40001ZM22.875 5.25C23.3723 5.25 23.8492 5.44755 24.2008 5.79918C24.5525 6.15081 24.75 6.62772 24.75 7.12501C24.75 7.62229 24.5525 8.0992 24.2008 8.45083C23.8492 8.80246 23.3723 9.00001 22.875 9.00001C22.3777 9.00001 21.9008 8.80246 21.5492 8.45083C21.1976 8.0992 21 7.62229 21 7.12501C21 6.62772 21.1976 6.15081 21.5492 5.79918C21.9008 5.44755 22.3777 5.25 22.875 5.25ZM15 7.50001C16.9891 7.50001 18.8968 8.29018 20.3033 9.69671C21.7098 11.1032 22.5 13.0109 22.5 15C22.5 16.9891 21.7098 18.8968 20.3033 20.3033C18.8968 21.7098 16.9891 22.5 15 22.5C13.0109 22.5 11.1032 21.7098 9.69671 20.3033C8.29018 18.8968 7.50001 16.9891 7.50001 15C7.50001 13.0109 8.29018 11.1032 9.69671 9.69671C11.1032 8.29018 13.0109 7.50001 15 7.50001ZM15 10.5C13.8065 10.5 12.6619 10.9741 11.818 11.818C10.9741 12.6619 10.5 13.8065 10.5 15C10.5 16.1935 10.9741 17.3381 11.818 18.182C12.6619 19.0259 13.8065 19.5 15 19.5C16.1935 19.5 17.3381 19.0259 18.182 18.182C19.0259 17.3381 19.5 16.1935 19.5 15C19.5 13.8065 19.0259 12.6619 18.182 11.818C17.3381 10.9741 16.1935 10.5 15 10.5Z" fill="#D1D1D1" />
                        </svg>
                    </div>
                </div> */}

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