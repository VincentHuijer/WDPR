describe("test inloggen", () => {
  const wachtwoord = "Test123!";
  const wachtwoordFout = "fout123!";

  beforeEach(() => {
    cy.visit("http://localhost:3000/login");
  });

  it("logt succesvol in met de juiste email en wachtwoord combinatie", () => {
    cy.get("input[name=email]").type("vin.cent.huijeren@gmail.com");
    cy.get("input[name=wachtwoord]").type(wachtwoord);
    cy.get("button[type=submit]").click();
    cy.get("p").contains("2FA");//volgende stap na het inloggen. We gaan ervan uit dat de 2FA van Google zelf goed genoeg getest is
  });

  it("probeert met niet bestaand account in te loggen", () => {
    cy.get("input[name=email]").type("ikbestaniet@gmail.com");
    cy.get("input[name=wachtwoord]").type(wachtwoord);
    cy.get("button[type=submit]").click();
    
    cy.get("p").contains("Email or password incorrect!");
  
  });

  it("probeert in te loggen met een foute combinatie", () => {
    cy.get("input[name=email]").type("fin.cent.huijeren@gmail.com");
    cy.get("input[name=wachtwoord]").type(wachtwoord);
    cy.get("button[type=submit]").click();

    cy.get("p").contains("Email or password incorrect!");
  });

  it("probeert met verkeerd wachtwoord/email combinatie in te loggen totdat het geblokkeerd raakt (3 pogingen)", () => {
    cy.get("input[name=email]").type("3dvechterlol@gmail.com");
    cy.get("input[name=wachtwoord]").type(wachtwoordFout);
    cy.get("button[type=submit]").click();
    cy.get("button[type=submit]").click();
    cy.get("button[type=submit]").click();

    cy.get("p").contains("User has been blocked because of too many login attempts!"
    );
  });

  it("Voert eerst een foute combinatie uit en vervolgens een juiste combinatie", () => {
    cy.get("input[name=email]").type("fin.cent.huijeren@gmail.com");
    cy.get("input[name=wachtwoord]").type(wachtwoord);
    cy.get("button[type=submit]").click();
    cy.get("p").contains("Email or password incorrect!");
    cy.get("input[name=email]").clear();
    cy.get("input[name=email]").type("vin.cent.huijeren@gmail.com");
    cy.get("button[type=submit]").click();

    cy.get("p").contains("2FA");//volgende stap na het inloggen. We gaan ervan uit dat de 2FA van Google zelf goed genoeg getest is

    
  });
});
