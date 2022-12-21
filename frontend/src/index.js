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
import OverOns from './Pages/OverOns';
import Voorstelling from './Pages/Voorstelling';



const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <div className='flex flex-col'>
      <Router>

        <NavBar />

        <Routes>
          <Route exact path='/' element={<HomePage />} />
          <Route path='/overons' element={<OverOns />} />
          <Route path='/voorstelling/:showID' element={<Voorstelling />} />
        </Routes>

        <Footer />
        
      </Router>
    </div>

  </React.StrictMode>
);

reportWebVitals();
