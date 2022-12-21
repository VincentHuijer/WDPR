import React, { useState } from "react";

export const Register = (props) => {
    const [email, setEmail] = useState('');
    const [wachtwoord, setWachtwoord] = useState('');
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
            <label htmlFor="wachtwoord">wachtwoord</label>
            <input value={wachtwoord} onChange={(e) => setWachtwoord(e.target.value)} type="wachtwoord" placeholder="Supergeheimwachtwoord123#" id="wachtwoord" name="wachtwoord" />
            <button type="registreer">Log In</button>
        </form>
        <button className="link-btn" onClick={() => props.onFormSwitch('login')}>Al een account? Log hier in.</button>
    </div>
    )
}