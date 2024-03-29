export default function CheckGegevens(
  firstName,
  lastName,
  password,
  passwordAgain
) {
  const date = new Date();

  console.log(password);
  console.log(passwordAgain);
  if (firstName.length > 0 && firstName.length< 2) {
    return "Voornaam te kort";
  }

  if (firstName.length === 0) {
    return "Voer een voornaam in";
  }

  if (firstName.length > 20) {
    return "Voornaam te lang";
  }

  if (!firstName.match(/^[A-Za-z]+$/)) {
    return "voornaam mag geen bijzondere tekens bevatten";
  }

  if (lastName.length > 0 && lastName.length < 2) {
    return "Achternaam te kort";
  }

  if (lastName.length === 0) {
    return "Voer een achternaam in";
  }

  if (lastName.length > 20) {
    return "Achternaam te lang";
  }

  if (!lastName.match(/^[A-Za-z]+$/)) {
    return "Achternaam mag geen bijzondere tekens bevatten";
  }
  if (password.length === 0) {
    return "Voer een wachtwoord in";
  }

  if (password.length > 0 && password.length < 8) {
    return "Wachtwoord te kort";
  }
  if(password.length > 20){
    return "Wachtwoord te lang";
  }
  if (password !== passwordAgain) {
    return "Wachtwoorden komt niet overeen";
  }
  if (password.match(/^[!@#$%^&*]+$/)) {
    return "Wachtwoord moet een speciaal teken bevatten";
  }
  if (password.match([/0-9/] < 1)) {
    return "Wachtwoord moet een nummer bevatten";
  }
  if (password.match([/A-Z/] < 1)) {
    return "Wachtwoord moet een hoofdletter bevatten";
  }
  return true;
}

//email check staat al in registreren of het een disposable email is en of het email al in gebruik is
