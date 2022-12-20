import './index.css';
import React from 'react';
import ReactDOM from 'react-dom/client';
import reportWebVitals from './reportWebVitals';
import {
  BrowserRouter as Router,
  Routes,
  Route
} from 'react-router-dom';

import HomePage from './Pages/HomePage';
import { NavBar } from './Components/NavBar';
import Footer from './Components/Footer';



const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <div className='flex flex-col'>
      <Router>

        <NavBar />

        <Routes>
          <Route exact path='/' element={<HomePage />} />
        </Routes>

        <Footer />
        
      </Router>
    </div>

  </React.StrictMode>
);

reportWebVitals();
