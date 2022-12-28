export default function ArtiestProfile({name, picture,role}) {
    return (
        <div className="w-fit h-fit">
            <img className="rounded-3xl border-2 border-black h-48 w-48" src={picture} />
            <p className="font-bold text-xl text-appLightBlack w-48 text-center">{name}</p>
            <p className="font-bold text-sm text-appLightBlack w-48 text-center">{role}</p>
        </div>
    )
}