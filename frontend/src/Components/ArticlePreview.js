import { Link } from 'react-router-dom';

//changeable items NAME, DESCRIPTION, BUTTON(HIDDEN OR VISIBLE)
export default function ArticlePreview() {
    return (
        <div className="w-11/12 flex justify-between m-auto mt-12 h-full">
            <div className='h-fit pb-1.5 flex flex-col justify-between'>
                <p className="text-4xl font-extrabold">OVER THEATER LAAK</p>
                <p className="mt-3 font-semibold text-appLightBlack">Some really long sentences here<br />
                    that tells something about Theater Laak.<br />
                    Some other sentence here.<br />
                    And something here.</p>


                <div className='mt-6'>
                    <Link to={"/overons"} className='border-2 border-appRed bg-appRed text-white px-3 py-1 rounded-xl font-extrabold'>VERDER LEZEN</Link>
                </div>
            </div>

            <div className='h-64'>
                <div className='h-full overflow-hidden'>
                    <img className='h-full rounded-2xl border-black border-2' src='./media/theaterLaak.jpg' />
                </div>
            </div>
        </div>
    )
}