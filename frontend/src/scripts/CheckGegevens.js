export default function CheckGegevens(
  firstName,
  lastName,
  password,
  passwordAgain
) {
  const date = new Date();

  console.log(password);
  console.log(passwordAgain);
  if (firstName.length < 2) {
    return "Voornaam te kort";
  }

  if (firstName.length > 15) {
    return "Voornaam te lang";
  }

  if (!firstName.match(/^[A-Za-z]+$/)) {
    return "voornaam mag geen bijzondere tekens bevatten";
  }

  if (lastName.length < 2) {
    return "achternaam te kort";
  }

  if (lastName.length > 15) {
    return "achternaam te lang";
  }

  if (!lastName.match(/^[A-Za-z]+$/)) {
    return "achternaam mag geen bijzondere tekens bevatten";
  }

  if (password.length < 8) {
    return "Wachtwoord te kort";
  }
  if (password !== passwordAgain) {
    return "wachtwoorden komt niet overeen";
  }
  if (password.match(/^[!@#$%^&*]+$/)) {
    return "wachtwoord moet een speciaal teken bevatten";
  }
  if (password.match([/0-9/] < 1)) {
    return "wachtwoord moet een nummer bevatten";
  }
  if (password.match([/A-Z/] < 1)) {
    return "Wachtwoord moet een hoofdletter bevatten";
  }
  return true;
}

//email check staat al in registreren of het een disposable email is en of het email al in gebruik is
