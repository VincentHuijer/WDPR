import Seat from './Seat'

export default function Row({ nmr, seats, bg }) {

    return (
        <div className={"flex gap-2 " + (bg && "bg-gray-200")}>
            <p className="font-bold text-xl w-8 flex justify-center items-center">{nmr + 1}</p>
            <div className="flex gap-2 w-full justify-center">
                {seats.map(seat => {
                    return <Seat key={Math.random()} type={seat} />
                })}
            </div>
        </div>
    )
}