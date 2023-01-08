Feature: Authenticatie

Scenario: RegistreerCorrect
    Given Een gebruiker met emailadres lucad2811@gmail.com heeft nog geen account
    And gebruiker met emailadres lucad2811@gmail.com vult de registratie correct in
    When gebruiker bevestigt de registratie
    Then moet Success teruggeven worden als response