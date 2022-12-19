export default function Menubalk() {
    return (
      <header>
        <link rel="stylesheet" type="text/css" href="/PretparkWebsite.css" />
        <title>Welkom bij de Efteling!</title>
        <div id="Menubalk">
          <a href="/"> {/*front page */}
            <img
              src={require('./Afbeeldingen/TheaterLogo.png')} alt="Terug naar het hoofdmenu"
            ></img>
          </a>
          <div>
            <a href="overons" className="nav">
              Over Ons
            </a>
          </div>
          <div>
            <a href="Alle Voorstellingen" className="nav">
                Alle Voorstellingen
            </a>
          </div>
          <div>
            <a href="Contact Opnemen" className="nav">
            Contact Opnemen
            </a>
          </div>
          <div>
            <a href="Bestel Tickets!" className="nav">
              Bestel nu tickets!
            </a>
          </div>
          <div>
            <a href="Inloggen" className="nav">
              Inloggen
            </a>
          </div>
          <div>
            <a href="Registreren" className="nav">
              Registreren
            </a>
          </div>
        </div>
      </header>
    );
  }
  