import './index.css';
import React from 'react';
import ReactDOM from 'react-dom/client';
import reportWebVitals from './reportWebVitals';
import {Helmet} from "react-helmet";

import App from './App';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    
      <Helmet htmlAttributes={{lang: 'nl'}}></Helmet>
      <App />

  </React.StrictMode>
);

reportWebVitals();
