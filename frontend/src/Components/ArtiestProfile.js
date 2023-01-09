export default function ArtiestProfile({ name, picture, role }) {
    return (
        <div className="w-full artistCard aspect-square">
            <div className="aspect-square overflow-hidden rounded-3xl border-2 border-black flex justify-start items-center">
                <img className="w-full" src={picture} />
            </div>
            <p className="font-bold text-xl text-appLightBlack w-11/12 m-auto">{name}</p>
            <p className="font-bold text-sm text-appLightBlack w-11/12 m-auto">{role}</p>
        </div>
    )
}