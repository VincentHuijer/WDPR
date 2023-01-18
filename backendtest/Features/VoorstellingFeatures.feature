Feature: Voorstelling

Scenario: VoorstellingAanmaken
    Given er komt een nieuwe voorstelling oudejaarsconferentie in het theater
    When een admin de voorstelling toevoegd
    Then moet de voorstelling in de database staan

Scenario: VoorstellingOphalenMetSortering
    Given GetVoorstellingen/leeftijd/{age} bestaat
    When sortering is ascending
    Then moet de voorstellingen in de database opgehaald worden in de juiste volgorde
