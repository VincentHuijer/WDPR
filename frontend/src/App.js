import React, { useState } from 'react';
import {
    BrowserRouter as Router,
    Routes,
    Route
} from 'react-router-dom';

//components
import HomePage from './Pages/HomePage';
import { NavBar } from './Components/NavBar';
import Footer from './Components/Footer';
import OverOns from './Pages/OverOns';
import Voorstelling from './Pages/Voorstelling';
import WinkelMand from './Pages/WinkelMand';
import Voorstellingen from './Pages/Voorstellingen';
import Login from './Pages/Login';
import Register from './Pages/Register';
import Betalen from './Pages/Betalen';
import { AuthProvider } from './Authentication/AuthContext';
import Verify from './Pages/auth/Verify';

export default function App() {

    return (
        <AuthProvider>
            {/* add rotating to this class */}
            <div className='flex flex-col '>

                <Router>
                    <NavBar />

                    <Routes>
                        <Route exact path='/' element={<HomePage />} />
                        <Route path='/overons' element={<OverOns />} />
                        <Route path='/voorstelling/:id' element={<Voorstelling />} />
                        <Route path='/winkelmand' element={<WinkelMand />} />
                        <Route path='/voorstellingen' element={<Voorstellingen />} />
                        <Route path='/login' element={<Login />} />
                        <Route path='/register' element={<Register />} />
                        <Route path='/verify' element={<Verify />} />
                        <Route path='betalen' element={<Betalen />} />
                    </Routes>

                    <Footer />
                </Router>
            </div>
        </AuthProvider>
    )
}