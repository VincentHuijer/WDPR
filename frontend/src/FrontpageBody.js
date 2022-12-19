export default function FrontpageBody() {
  return (
    <div>
      <div id="FrontpageAfbeelding">
      {/* <img src={require("./Voorstelling.png")} alt="Voorstelling"></img> */}
      </div>

      <article>
        <div className="bestelText">
        </div>

        <div className="Voorbeeldshow">
          <h2>Marry Poppins</h2>
          {/* <img src={require("./MarryPoppins.png")} alt="Marry Poppins"></img> */}
        </div>

        <div className="Voorbeeldshow">
          <h2>Soldaat van Oranje</h2>
          {/* <img src={require("./SoldaatVanOranje.png")} alt="Soldaat Van Oranje"></img> */}
        </div>
      </article>
    </div>
  );
}
