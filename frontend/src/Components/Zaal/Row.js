import Seat from './Seat'

export default function Row({ nmr, seats, bg, addStoel, deleteStoel }) {

    return (
        <div className={"flex gap-2 " + (bg && "bg-gray-200")}>
            <p className="font-bold text-xl w-8 flex justify-center items-center">{nmr + 1}</p>
            <div className="flex gap-2 w-full justify-center">
                {seats.map((seat, i) => {
                    return <Seat addStoel={addStoel} deleteStoel={deleteStoel} row={nmr} key={`${nmr}-${i}`} seat={i} type={seat} />
                })}
            </div>
        </div>
    )
}