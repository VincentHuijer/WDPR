import React, { useState } from "react";
import PasswordChecklist from "react-password-checklist"
import ReCAPTCHA from "react-google-recaptcha"
import { ReactDOM } from "react";
import onChange from "../scripts/Captcha";

export const Register = (props) => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState("")
	const [passwordAgain, setPasswordAgain] = useState("");
    const [firstName, setFirstName] = useState('');
    const [LastName, setLastName] = useState('');
    const [geboorteDatum, setGeboortedatum] = useState('');


    const handleSubmit = (e) => {
        e.preventDefault();
        console.log(email);
    }

    const onChange = () => {};

    return (
        <div className="auth-form-container">
            {/* e.target.value is voor het element dat het event triggered. Ind it geval de label */}
            <h2>Register</h2>
        <form className="register-form" onSubmit={handleSubmit}>

            <label htmlFor="firstName">Voornaam</label>
            <input value={firstName} name="firstName" onChange={(e) => setFirstName(e.target.value)} id="FirstName" placeholder="First Name" />

            <label htmlFor="LastName">Achternaam</label>
            <input value={LastName} name="name" onChange={(e) => setLastName(e.target.value)} id="LastName" placeholder="Last Name" /> 

            <label htmlFor="email">email</label>
            <input value={email} onChange={(e) => setEmail(e.target.value)}type="email" placeholder="naam@gmail.com" id="email" name="email" />

            <label htmlFor="geboortedatum">geboorteDatum</label>
            <input value={geboorteDatum} onChange={(e) => setGeboortedatum(e.target.value)}type="geboortedatum" placeholder="******" id="geboortedatum" name="geboortedatum" />

            <label htmlFor="password">Wachtwoord</label>
            <input value={password} onChange={(e) => setPassword(e.target.value)} type="password" placeholder="Uw wachtwoord" id="Password" name="password" />

            <label htmlFor="Password Again">Opnieuw wachtwoord</label>
			<input value={passwordAgain} onChange={(e) => setPasswordAgain(e.target.value)} type="password" placeholder="Opnieuw wachtwoord" id="Password2" name="password2"/>  
			{/* <input type="password" onChange={e => setPasswordAgain(e.target.value)}></input>   */}
            
            <ReCAPTCHA
    sitekey="6LeLJKwjAAAAANBwpUkGvUJz357ariQ7vhAglkl3"
    onChange={onChange}
  />
    


            <button type="registreer">Log In</button>
           <PasswordChecklist
				rules={["minLength","specialChar","number","capital","match"]}
				minLength={8}
				value={password}
				valueAgain={passwordAgain}
				messages={{
					minLength: "Minimaal 8 karakters!",
					specialChar: "Een special karakter!",
					number: "Een nummer!",
					capital: "Een hoofdletter!",
					match: "niet overeen!",
				}}
			/>
        </form>

        <button className="link-btn" onClick={() => props.onFormSwitch('login')}>Al een account? Log hier in.</button>
    </div>
    )
}