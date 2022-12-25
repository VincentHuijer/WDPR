
import React, { useState } from "react";

export const Login = (props) => {
  const [email, setEmail] = useState("");
  const [wachtwoord, setWachtwoord] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log(email);
  };

  return (
    <div className="auth-form-container">
      <h2>Login</h2>
      <form className="login-form" onSubmit={handleSubmit}>
        <label htmlFor="email">email</label>
        <input
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          type="email"
          placeholder=" Uw emailadres"
          id="email"
          name="email"
        />
        <label htmlFor="wachtwoord">wachtwoord</label>
        <input
          value={wachtwoord}
          onChange={(e) => setWachtwoord(e.target.value)}
          type="wachtwoord"
          placeholder=" *********"
          id="wachtwoord"
          name="wachtwoord"
        />
        <button type="submit">Log In</button>
      </form>
      <button
        className="link-btn"
        onClick={() => props.onFormSwitch("register")}
      >
        Nog geen account? registreer hier.
      </button>
    </div>
  );
};