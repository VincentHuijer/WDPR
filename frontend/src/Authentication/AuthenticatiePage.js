import React, { useState } from "react";
import { Login } from "./login";
import { Register } from "./Register";

function AuthenthicatiePage() {
  const [currentForm, setCurrentForm] = useState('login');

  const toggleForm = (formName) => {
    setCurrentForm(formName);
  }

  return (
    <div className="App">
      {
        currentForm === "login" ? <Login onFormSwitch={toggleForm} /> : <Register onFormSwitch={toggleForm} />
      }
    </div>
  );
}

export default AuthenthicatiePage;