import React, { useState } from "react";
import { Login } from "./login";
import { Register } from "./Register";

function AuthenthicatiePage(Loginpage) {
  const [currentForm, setCurrentForm] = useState('login');

  const toggleForm = (formName) => {
    setCurrentForm(formName);
  }

  if (Loginpage) return (
    <div className="App">
      { 
      currentForm === "login" ? <Login onFormSwitch={toggleForm} /> : <Register onFormSwitch={toggleForm} />
      }
    </div>
  )
 else{
  currentForm === "login" ? <Register onFormSwitch={toggleForm} /> : <Login onFormSwitch={toggleForm} />

 }
}

export default AuthenthicatiePage;