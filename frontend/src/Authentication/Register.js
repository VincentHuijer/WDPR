import React, { useState } from "react";
import PasswordChecklist from "react-password-checklist"

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

    return (
        <div className="auth-form-container">
            <h2>Register</h2>
        <form className="register-form" onSubmit={handleSubmit}>
            <label htmlFor="firstName">Voornaam</label>
            {/* e.target.value is voor het element dat het event triggered. Ind it geval de label */}
            <input value={firstName} name="firstName" onChange={(e) => setFirstName(e.target.value)} id="FirstName" placeholder="First Name" />
            <label htmlFor="LastName">Full name</label>
            <input value={LastName} name="name" onChange={(e) => setLastName(e.target.value)} id="LastName" placeholder="Last Name" /> 
            <label htmlFor="email">email</label>
            <input value={email} onChange={(e) => setEmail(e.target.value)}type="email" placeholder="naam@gmail.com" id="email" name="email" />
            <label htmlFor="email">email</label>
            <input value={geboorteDatum} onChange={(e) => setGeboortedatum(e.target.value)}type="geboortedatum" placeholder="**" id="geboortedatum" name="geboortedatum" />
            <label htmlFor="password">Password</label>
            <input value={password} onChange={(e) => setPassword(e.target.value)} type="password" placeholder="SupergeheimPassword123#" id="Password" name="password" />
            <label htmlFor="Password Again"> </label>
			<input type="password" onChange={e => setPasswordAgain(e.target.value)}></input>




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