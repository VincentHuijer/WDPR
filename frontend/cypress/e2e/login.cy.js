describe("test inloggen", () => {
  const emailSuccesvol = "vin.cent.huijeren@gmail.com";
  const emailBlokkeren = "3dvechterlol@gmail.com"
  const wachtwoord = "Test123!";
  const wachtwoordFout = "fout123!";

  beforeEach(() => {
    cy.visit("http://localhost:3000/login");
  });

  it("logt succesvol in met de juiste email en wachtwoord combinatie", () => {   
    cy.get("input[name=email]").type(emailSuccesvol);
    cy.get("input[name=wachtwoord]").type(wachtwoord);
    cy.get("button[type=submit]").click();
  });

  it("probeert met niet bestaand account in te loggen", () => {
    cy.get("input[name=email]").type("ikbestaniet@gmail.com");
    cy.get("input[name=wachtwoord]").type(wachtwoord);
    cy.get("button[type=submit]").click();
  });

    it("probeert met verkeerd wachtwoord/email combinatie in te loggen totdat het geblokkeerd raakt (3 pogingen)", () => {
      cy.get("input[name=email]").type(emailBlokkeren);
      cy.get("input[name=wachtwoord]").type(wachtwoordFout);
      cy.get("button[type=submit]").click();
      cy.get("button[type=submit]").click();
      cy.get("button[type=submit]").click();

      cy.get("p").contains("User has been blocked because of too many login attempts!");
    });
});
