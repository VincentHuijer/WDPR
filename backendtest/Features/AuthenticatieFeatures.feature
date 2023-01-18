Feature: Authenticatie

Scenario: RegistreerCorrect
    Given Een gebruiker met emailadres lucad2811@gmail.com heeft nog geen account
    And gebruiker met emailadres lucad2811@gmail.com vult de registratie correct in
    When gebruiker bevestigt de registratie
    Then moet Success teruggeven worden als response

Scenario: RegistreerDuplicate
    Given Een gebruiker met emailadres lucad2811@gmail.com heeft al een account
    And gebruiker met emailadres lucad2811@gmail.com vult de registratie correct in
    When gebruiker bevestigt de registratie
    Then moet EmailInUseError teruggeven worden als response

Scenario: RegistreerDisposableDomain
    Given Een gebruiker met emailadres test@yopmail.com heeft nog geen account
    And gebruiker met emailadres test@yopmail.com vult de registratie correct in
    When gebruiker bevestigt de registratie
    Then moet DisposableMailError teruggeven worden als response

Scenario: LoginCorrect
    Given Een gebruiker met emailadres lucad2811@gmail.com heeft al een account
    And gebruiker met emailadres lucad2811@gmail.com vult de logingegevens correct in
    And gebruiker met emailadres lucad2811@gmail.com heeft geverifieerd
    And gebruiker met emailadres lucad2811@gmail.com is niet geblokkeerd
    When gebruiker bevestigt login
    Then moet Success teruggeven worden als response

Scenario: LoginInValidCredentials
    Given Een gebruiker met emailadres lucad2811@gmail.com heeft al een account
    And gebruiker met emailadres lucad2811@gmail.com vult de logingegevens incorrect in
    And gebruiker met emailadres lucad2811@gmail.com heeft geverifieerd
    And gebruiker met emailadres lucad2811@gmail.com is niet geblokkeerd
    When gebruiker bevestigt login
    Then moet InvalidCredentialsError teruggeven worden als response

Scenario: LoginBlocked
    Given Een gebruiker met emailadres lucad2811@gmail.com heeft al een account
    And gebruiker met emailadres lucad2811@gmail.com vult de logingegevens correct in
    And gebruiker met emailadres lucad2811@gmail.com heeft geverifieerd
    And gebruiker met emailadres lucad2811@gmail.com is wel geblokkeerd
    When gebruiker bevestigt login
    Then moet UserBlockedError teruggeven worden als response

Scenario: LoginUnverified
    Given Een gebruiker met emailadres lucad2811@gmail.com heeft al een account
    And gebruiker met emailadres lucad2811@gmail.com vult de logingegevens correct in
    And gebruiker met emailadres lucad2811@gmail.com heeft niet geverifieerd
    And gebruiker met emailadres lucad2811@gmail.com is niet geblokkeerd
    When gebruiker bevestigt login
    Then moet NotVerifiedError teruggeven worden als response

Scenario: VerifieerCorrect
    Given Een gebruiker met emailadres lucad2811@gmail.com heeft al een account
    And gebruiker met emailadres lucad2811@gmail.com bezit de juiste token
    And gebruiker met emailadres lucad2811@gmail.com heeft een niet verlopen token
    When gebruiker met emailadres lucad2811@gmail.com bevestigt verificatie
    Then moet Success teruggeven worden als response

Scenario: VerifieerExpiredToken
    Given Een gebruiker met emailadres lucad2811@gmail.com heeft al een account
    And gebruiker met emailadres lucad2811@gmail.com bezit de juiste token
    And gebruiker met emailadres lucad2811@gmail.com heeft een verlopen token
    When gebruiker met emailadres lucad2811@gmail.com bevestigt verificatie
    Then moet ExpiredTokenError teruggeven worden als response

Scenario: VerifieerAlreadyVerified
    Given Een gebruiker met emailadres lucad2811@gmail.com heeft al een account
    And gebruiker met emailadres lucad2811@gmail.com bezit geen token meer
    When gebruiker met emailadres lucad2811@gmail.com bevestigt verificatie
    Then moet AlreadyVerifiedError teruggeven worden als response

Scenario: ResetWachtwoordSucces
    Given Een gebruiker met emailadres lucad2811@gmail.com heeft al een account
    And gebruiker met emailadres lucad2811@gmail.com geeft aan zijn wachtwoord te zijn vergeten
    And gebruiker met emailadres lucad2811@gmail.com bezit de juiste authenticatietoken
    And gebruiker met emailadres lucad2811@gmail.com heeft een niet verlopen authenticatietoken
    When gebruiker met emailadres lucad2811@gmail.com de wachtwoordreset bevestigt
    Then moet Success teruggeven worden als response

Scenario: ResetWachtwoordInvalidToken
    Given Een gebruiker met emailadres lucad2811@gmail.com heeft al een account
    And gebruiker met emailadres lucad2811@gmail.com geeft aan zijn wachtwoord te zijn vergeten
    And gebruiker met emailadres lucad2811@gmail.com bezit niet de juiste authenticatietoken
    And gebruiker met emailadres lucad2811@gmail.com heeft een niet verlopen authenticatietoken
    When gebruiker met emailadres lucad2811@gmail.com de wachtwoordreset bevestigt
    Then moet InvalidTokenError teruggeven worden als response    

Scenario: ResetWachtwoordExpiredToken
    Given Een gebruiker met emailadres lucad2811@gmail.com heeft al een account
    And gebruiker met emailadres lucad2811@gmail.com geeft aan zijn wachtwoord te zijn vergeten
    And gebruiker met emailadres lucad2811@gmail.com bezit de juiste authenticatietoken
    And gebruiker met emailadres lucad2811@gmail.com heeft een verlopen authenticatietoken
    When gebruiker met emailadres lucad2811@gmail.com de wachtwoordreset bevestigt
    Then moet ExpiredTokenError teruggeven worden als response             