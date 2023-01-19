Feature: Authorisatie

Scenario: AdminFunctieMetAdminRol
    Given een medewerker met rol Admin
    And een bepaalde functie vereist rol Admin
    When medewerker met rol Admin deze functie probeert uit te voeren
    Then moet true teruggeven worden als waarde

Scenario: AdminFunctieMetMedewerkerRol
    Given een medewerker met rol Medewerker
    And een bepaalde functie vereist rol Admin
    When medewerker met rol Medewerker deze functie probeert uit te voeren
    Then moet false teruggeven worden als waarde

Scenario: AdminFunctieMetKlantRol
    Given een klant met rol Klant
    And een bepaalde functie vereist rol Admin
    When klant met rol Klant deze functie probeert uit te voeren
    Then moet false teruggeven worden als waarde 

Scenario: AdminFunctieMetArtiestRol
    Given een klant met rol Artiest
    And een bepaalde functie vereist rol Admin
    When klant met rol Artiest deze functie probeert uit te voeren
    Then moet false teruggeven worden als waarde