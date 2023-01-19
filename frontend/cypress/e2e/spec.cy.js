/// <reference types="cypress" />

describe("test redirect naar homepage bij ongeldige link", () => {

  beforeEach(() => {
    cy.visit("http://localhost:3000/ditbestaatniet");
  });
it("redirect naar homepage", () => {
  cy.wait(10000) 
  // pause voor 10 seconde. Al is dit rood is dit de correcte manier van testen en brengt het geen conflicts.
cy.location().should((location) => {
    expect(location.href).to.eq("http://localhost:3000/")
  });

});
it("redirect naar homepage door te klikken op de hyperlink", () => {

  cy.contains("homepagina").click();
  cy.location().should((location) => {
    expect(location.href).to.eq("http://localhost:3000/")
  });
});

it("redirect met een 3G connectie simulatie", () => {
}); 
});
