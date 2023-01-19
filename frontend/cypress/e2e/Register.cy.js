/// <reference types="cypress" />

describe("test register form", () => {
  const firstName = "testVoornaam";
  const lastName = "testAchternaam";
  const email = "test@gmail.com";
  const wachtwoord = "testWachtwoord123!";
  const wachtwoordOpnieuw = "testWachtwoord123!";
  const WachtwoordOpniewOngelijk ="foutWachtwoord123!"
  const disposableemail = "a@0-mail.com";

  beforeEach(() => {
    cy.visit("http://localhost:3000/register");
  });

 

  it("vult het registratieformulier met een ongelijk wachtwoord fout in", () => {
    cy.get("input[name=firstName]").type(firstName);
    cy.get("input[name=name]").type(lastName);
    cy.get("input[name=email]").type(email);
    cy.get("input[name=password]").type(wachtwoord);
    cy.get("input[name=repeatpassword]").type(WachtwoordOpniewOngelijk);
    cy.get("button[type=submit]").click();

    cy.get("p").contains("Wachtwoorden komt niet overeen");
  }
    );

    it("vult het registratieformulier met een zwak wachtwoord in", () => {
      cy.get("input[name=firstName]").type(firstName);
      cy.get("input[name=name]").type(lastName);
      cy.get("input[name=email]").type(email);
      cy.get("input[name=password]").type("Kaas");
      cy.get("input[name=repeatpassword]").type("Kaas");
      cy.get("button[type=submit]").click();
  
      cy.get("p").contains("Wachtwoord te kort");
    }
      );

 it("vult het registratieformulier met een te lang wachtwoord in", () => {
      cy.get("input[name=firstName]").type(firstName);
      cy.get("input[name=name]").type(lastName);
      cy.get("input[name=email]").type(email);
      cy.get("input[name=password]").type("supersterkwachtwoord1234567890");
      cy.get("input[name=repeatpassword]").type("supersterkwachtwoord1234567890");
      cy.get("button[type=submit]").click();
  
      cy.get("p").contains("Wachtwoord te lang");
    }
      );


    it("vult het registratieformulier met een disposable email", () => {
      cy.get("input[name=firstName]").type(firstName);
      cy.get("input[name=name]").type(lastName);
      cy.get("input[name=email]").type(disposableemail); 
      cy.get("input[name=password]").type(wachtwoord);
      cy.get("input[name=repeatpassword]").type(wachtwoordOpnieuw);
      cy.get("button[type=submit]").click();

      cy.get("p").contains("Disposable email used!");  
    }
      );

      it("vult het registratieformulier met een voornaam met nummers", () => { //voornaam/achternaam exact zelfde code
        cy.get("input[name=firstName]").type("23123213");
        cy.get("input[name=name]").type(lastName);
        cy.get("input[name=email]").type(email);
        cy.get("input[name=password]").type(wachtwoord);
        cy.get("input[name=repeatpassword]").type(WachtwoordOpniewOngelijk);
        cy.get("button[type=submit]").click();
    
        cy.get("p").contains("voornaam mag geen bijzondere tekens bevatten");

  });

   it("vult het registratieformulier met een achternaam met maar 1 letter", () => { //voornaam/achternaam exact zelfde code
    cy.get("input[name=firstName]").type(firstName);
    cy.get("input[name=name]").type("a");
    cy.get("input[name=email]").type(email);
    cy.get("input[name=password]").type(wachtwoord);
    cy.get("input[name=repeatpassword]").type(wachtwoordOpnieuw);
    cy.get("button[type=submit]").click();

    cy.get("p").contains("Achternaam te kort");
   });
 it("vult het registratieformulier met een voornaam van meer dan 20 karakters", () => { //voornaam/achternaam exact zelfde code
        cy.get("input[name=firstName]").type("WillemAlexanderDerNederlanden");
        cy.get("input[name=name]").type(lastName);
        cy.get("input[name=email]").type(email);
        cy.get("input[name=password]").type(wachtwoord);
        cy.get("input[name=repeatpassword]").type(wachtwoordOpnieuw);
        cy.get("button[type=submit]").click();
    
        cy.get("p").contains("Voornaam te lang");

});

it("vult het registratieformulier zonder voornaam", () => { //voornaam/achternaam exact zelfde code
  cy.get("input[name=name]").type(lastName);
  cy.get("input[name=email]").type(email);
  cy.get("input[name=password]").type(wachtwoord);
  cy.get("input[name=repeatpassword]").type(wachtwoordOpnieuw);
  cy.get("button[type=submit]").click();

  cy.get("p").contains("Voer een voornaam in");
});

it("vult het registratieformulier succesvol in voor een nog niet bestaande gebruiker", () => {
  cy.get("input[name=firstName]").type(firstName);
  cy.get("input[name=name]").type(lastName);
  const randomEmail = `newuser+${Math.floor(Math.random() * 10000)}@example.com`;
  cy.get('input[name="email"]').type(randomEmail);
  cy.get("input[name=password]").type(wachtwoord);
  cy.get("input[name=repeatpassword]").type(wachtwoordOpnieuw);
  cy.get("button[type=submit]").click();

  cy.get("p").contains("VERIFIEER UW ACCOUNT");
});

it("vult het registratieformulier fout in en vervolgens goed (succesvolle registratie)", () => { //voornaam/achternaam exact zelfde code
  cy.get("input[name=name]").type(lastName);
  const randomEmail = `newuser+${Math.floor(Math.random() * 10000)}@example.com`;
  cy.get('input[name="email"]').type(randomEmail);
  cy.get("input[name=password]").type(wachtwoord);
  cy.get("input[name=repeatpassword]").type(wachtwoordOpnieuw);
  cy.get("button[type=submit]").click();
  cy.get("p").contains("Voer een voornaam in");

  cy.get("input[name=firstName]").type(firstName);
  cy.get("button[type=submit]").click();

  cy.get("p").contains("VERIFIEER UW ACCOUNT");

});




});