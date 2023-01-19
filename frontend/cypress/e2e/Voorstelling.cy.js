const emailSuccesvol = "vin.cent.huijeren@gmail.com";
const wachtwoord = "Test123!";

describe("test voorstellingen", () => {
  before(() => {
    cy.visit("http://localhost:3000/voorstellingen/");
  });
it("Probeert de voorstelling Aladin te zoeken in de zoekbalk", () => { 
  cy.get(`input[name=voorstellingInputveld]`).type("Aladin");
  cy.contains("ZOEKEN").click();
  cy.contains("MEER INFO").click();
  cy.location().should((location) => {
    expect(location.href).to.eq("http://localhost:3000/voorstelling/1")
  });
});
});


// describe("test voorstelling", () => {
//   before(() => {
//     cy.visit("http://localhost:3000/voorstelling/1");
//   });

//   it("Probeer te bestellen zonder ingelogt te zijn (redirect naar loginpage)", () => { 
//     cy.get(`[aria-label="Rij 1 Stoel 14 type 2e rang"]`).click();
//     cy.get("button[name=bestelButton]").click();
//   });



//   it("Probeer te bestellen, logt in, geselecteerden stoel zit nogsteeds in winkelmand", () => { //bij een heel slome verbinding aan de database heeft de test meer tijd nodig. (ligt aan gratis versie)
//     cy.get(`[aria-label="Rij 1 Stoel 15 type 2e rang"]`).click();
//     cy.get("button[name=bestelButton]").click();
//     cy.get("input[name=email]").type(emailSuccesvol);
//     cy.get("input[name=wachtwoord]").type(wachtwoord);
//     cy.get("button[type=submit]").click();
//     cy.get("button[name=Winkelmand]").click(); //werkt niet door mobile app in cypress.
//     cy.visit("http://localhost:3000/winkelmand");
//     cy.get("button[name=afrekenKnop]").click();
//   });




// it("reserveer een stoel, terwijl ingelogt", () => { //bij een heel slome verbinding aan de database heeft de test meer tijd nodig. (ligt aan gratis versie)
//     cy.visit("http://localhost:3000/login/");

//   cy.get("input[name=email]").type(emailSuccesvol); 
//   cy.get("input[name=wachtwoord]").type(wachtwoord);
//   cy.get("button[type=submit]").click();

//   cy.visit("http://localhost:3000/voorstelling/1");
//   cy.get(`[aria-label="Rij 1 Stoel 14 type 2e rang"]`).click();
//   cy.get("button[name=bestelButton]").click();
//       // cy.get("button[name=Winkelmand]").click(); //werkt niet door mobile app in cypress.
//   cy.visit("http://localhost:3000/winkelmand");
//   cy.get("button[name=afrekenKnop]").click();
// });
// });


