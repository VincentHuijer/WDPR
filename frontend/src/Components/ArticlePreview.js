import { Link } from 'react-router-dom';

//changeable items NAME, DESCRIPTION, BUTTON(HIDDEN OR VISIBLE)
export default function ArticlePreview({text, overonspagina = false}) {
    return (
        <article className="w-11/12 flex flex-col md:flex-row justify-between m-auto mt-12 h-full">
            <div className='h-fit pb-1.5 flex flex-col justify-between'>
                <h2 className="text-2xl lg:text-4xl font-extrabold">OVER THEATER LAAK</h2>
                <p className="mt-3 font-semibold text-appLightBlack w-full md:w-3/4 ">{text}<br /></p>

                {!overonspagina && <div className='mt-6'>
                    <Link to={"/overons"} className='border-2 border-appRed bg-appRed text-white px-3 py-1 rounded-xl font-extrabold'>VERDER LEZEN</Link>
                </div>}

            </div>

            <div className='h-64 mt-8 md:mt-0'>
                <div className='h-full w-96 overflow-hidden'>
                    <img alt='theater laak' className='h-full rounded-2xl border-black border-2' src='./media/theaterLaak.jpg' />
                </div>
            </div>
        </article>
    )
}