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

import { AuthProvider } from './Authentication/AuthContext';

export default function App() {

    return (
        <AuthProvider>
            <div className='flex flex-col'>

                <Router>
                    <NavBar />

                    <Routes>
                        <Route exact path='/' element={<HomePage />} />
                        <Route path='/overons' element={<OverOns />} />
                        <Route path='/voorstelling/:showID' element={<Voorstelling />} />
                        <Route path='/winkelmand' element={<WinkelMand />} />
                        <Route path='/voorstellingen' element={<Voorstellingen />} />
                        <Route path='/login' element={<Login />} />
                        <Route path='/register' element={<Register />} />
                    </Routes>

                    <Footer />
                </Router>
            </div>
        </AuthProvider>
    )
}