export default function CheckGegevens(firstName, lastName, geboorteDatum) {
    const date = new Date();
    if (firstName.length < 2) {
        return "Voornaam te kort";
    }

    if (firstName.length > 15) {
        return "Voornaam te lang";
    }

    if (!firstName.match(/^[A-Za-z]+$/)) {
        return "voornaam mag geen bijzondere tekens bevatten"
    }


    if (lastName.length < 2) {
        return "achternaam te kort";
    }


    if (lastName.length > 15) {
        return "achternaam te lang";
    }

    if (!lastName.match(/^[A-Za-z]+$/)) {
        return "achternaam mag geen bijzondere tekens bevatten"
    }

    if (geboorteDatum > date) {
        return "Ongeldige geboortedatum";
    }

    return "";
};