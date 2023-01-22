const emailSuccesvol = "vin.centhuijeren@gmail.com";
const wachtwoord = "Test123!";

describe("Medewerker voorstelling tests ", () => { //Dit is een test voor de medewerker dingen UPDATE DITUPDATE DITUPDATE DITUPDATE DITUPDATE DITUPDATE DITUPDATE DIT
  before(() => {
    cy.visit("http://localhost:3000/login/");
  });



  it("Probeer een voorstelling toe te voegen", () => {
    cy.get("input[name=email]").type("test@gmail.com");
    cy.get("input[name=wachtwoord]").type("test");
    cy.get("button[type=submit]").click();
    cy.wait(2000); //Cypress gaat naar medewerker voordat de login is afgerond
    cy.visit("http://localhost:3000/medewerker");
    cy.get("input[name=VoorstellingTitel]").type("2Dummies1car");
    cy.get("input[name=VoorstellingOmschrijving]").type("Testers");
    cy.get("input[name=VoorstellingPlaatje]").type("https://media.tenor.com/tMEM  ZWKRkN0AAAAC/thumbs-up-men.gif");
    cy.contains("VOORSTELLING TOEVOEGEN").click();
    // cy.get("button[name=VoorstellingToevoegenKnop]").click();
    cy.wait(2000); 

  });


  it("Probeer een show aan een voorstelling toe te voegen", () => {
    cy.visit("http://localhost:3000/login/");
    cy.get("input[name=email]").type("test@gmail.com");
    cy.get("input[name=wachtwoord]").type("test");
    cy.get("button[type=submit]").click();
    cy.wait(2000); //Cypress gaat naar medewerker voordat de login is afgerond
    cy.visit("http://localhost:3000/medewerker");
    var randomDag = Math.floor(Math.random() * 9) + 1//We testen of het op verschillende dagen
    var randomMaand = Math.floor(Math.random() * 9) + 1 //We testen of het op verschillende maanden
    var randomJaar = Math.floor(Math.random() * (9 - 3 +1) + 3) //We testen of het op verschillende jaren
    
    cy.get("input[name=2Dummies1carDatumInput]").type("202" + randomJaar + "-0" + randomMaand + "-0" + randomDag);
    cy.get("input[name=2Dummies1carzaalInput]").type(1);
    cy.get("button[name=2Dummies1cartoevoegButton]").click();
    cy.wait(2000); 

  });


it("Probeert de voorstelling Test Dummy te zoeken in de zoekbalk", () => { 
  cy.visit("http://localhost:3000/voorstellingen/");
  cy.get(`input[name=voorstellingInputveld]`).type("2Dummies1car");
  cy.contains("ZOEKEN").click();
  cy.contains("MEER INFO").click();
  cy.wait(2000); //laden van shows duurt een seconde.
  cy.contains("2DUMMIES1CAR");


});




});

describe("Klant bestelt een stoel bij een voorstelling ", () => { //Dit is een test voor de klant dingen
it("reserveer een stoel", () => { 
  cy.visit("http://localhost:3000/login/");
  cy.get("input[name=email]").type(emailSuccesvol); 
  cy.get("input[name=wachtwoord]").type(wachtwoord);
  cy.get("button[type=submit]").click();
  cy.wait(2000); //de website heeft een seconde nodig om het te verwerken

  cy.visit("http://localhost:3000/voorstellingen/");
  cy.get(`input[name=voorstellingInputveld]`).type("2Dummies1car");
  cy.contains("ZOEKEN").click();
  cy.contains("MEER INFO").click();

  cy.get(`[aria-label="Rij 1 Stoel 14 type 2e rang"]`).click();
  cy.get("button[name=bestelButton]").click();
  cy.wait(2000);
  cy.visit("http://localhost:3000/winkelmand");
  cy.wait(2000); //laden van winkelmand duurt een seconde.
  cy.get("button[name=afrekenKnop]").click();
});
});

  describe("Medewerker verwijdert voorstelling ", () => { //Dit is een test voor de klant dingen

it("Probeer een show aan een voorstelling te verwijderen", () => {
  cy.visit("http://localhost:3000/login/");
  cy.get("input[name=email]").type("test@gmail.com");
  cy.get("input[name=wachtwoord]").type("test");
  cy.get("button[type=submit]").click();
  cy.wait(2000); //Cypress gaat naar medewerker voordat de login is afgerond
  cy.visit("http://localhost:3000/medewerker");
  cy.get("button[name=2Dummies1carverwijderButton]").click();
});
});

