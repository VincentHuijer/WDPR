/// <reference types="cypress" />

describe("test redirect naar homepage bij ongeldige link", () => {
  beforeEach(() => {
    cy.visit("http://localhost:3000/ditbestaatniet");
  });
  it("redirect naar homepage", { defaultCommandTimeout: 10000 }, () => {
    //defaultCommandTimeout wacht 10 seconde voor het testen van de locatie.
    // Dit is nodig omdat de redirect naar de homepage 5 seconde duurt.
    //Als de redirect niet werkt dan blijft de locatie op http://localhost:3000/ditbestaatniet en faalt de test.
    //Als de redirect wel werkt dan is de locatie na 5 seconde http://localhost:3000/ en slaagt de test.
    cy.location().should((location) => {
      expect(location.href).to.eq("http://localhost:3000/");
    });
  });
  it("redirect naar homepage door te klikken op de hyperlink", () => {
    cy.contains("homepagina").click();
    cy.location().should((location) => {
      expect(location.href).to.eq("http://localhost:3000/");
    });
  });
});
