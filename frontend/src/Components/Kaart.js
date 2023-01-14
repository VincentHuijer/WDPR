export default function Kaart() {
    return (
        <article id="contact" className="w-11/12 flex flex-col gap-4 justify-between m-auto mt-20 h-full">
            <h2 className="text-4xl font-extrabold">HOE KUNT U ONS BEREIKEN?</h2>

            <div className="w-full h-1/3 flex flex-col md:flex-row mdfle-row justify-between items-start overflow-hidden">
                <div className="flex flex-col gap-0">
                    <p className="font-semibold text-appLightBlack"><span className="font-bold">Adres:</span> Ferrandweg 4-T, 2523 XT Den Haag</p>
                    <p className="font-semibold text-appLightBlack"><span className="font-bold">Email:</span> contact@theaterlaak.com</p>
                    <p className="font-semibold text-appLightBlack"><span className="font-bold">Telnmr:</span> +31 6 12 345 678</p>
                </div>

                <div className="mt-2 lg:md-0 w-full md:w-1/2 border-2 border-black rounded-2xl overflow-hidden">
                    <iframe  src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2453.2645688889215!2d4.3125697!3d52.0567061!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x47c5b6c3a09032e5%3A0x9dd219c920c8ee15!2sFerrandweg%204T%2C%202523%20XT%20Den%20Haag!5e0!3m2!1snl!2snl!4v1673736171391!5m2!1snl!2snl" width="800" height="300" loading="lazy" referrerPolicy="no-referrer-when-downgrade"></iframe>
                </div>
            </div>
        </article>
    )
}