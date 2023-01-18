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
import Bedankt from './Pages/Bedankt';
import Bestellingen from './Pages/user/Bestellingen';
import FourOOOFour from './Components/FourOOOFour';
import Ticket from './Pages/Ticket';
import DoneerPagina from './Pages/DoneerPagina';
import WachtwoordResetten from './Pages/user/WachtwoordResetten';
import ShowInzien from './Pages/medewerker/ShowInzien';

export default function App() {

    return (
        <AuthProvider>
            {/* add rotating to this class */}
            <div className='flex flex-col '>

                <Router>


                    <Routes>
                        <Route exact path='/' element={<Wrapper><HomePage /></Wrapper>} />
                        <Route path='/overons' element={<Wrapper><OverOns /></Wrapper>} />
                        <Route path='/voorstelling/:id' element={<Wrapper><Voorstelling /></Wrapper>} />
                        <Route path='/winkelmand' element={<Wrapper><WinkelMand /> </Wrapper>} />
                        <Route path='/voorstellingen' element={<Wrapper><Voorstellingen /> </Wrapper>} />
                        <Route path='/login' element={<Wrapper><Login /> </Wrapper>} />
                        <Route path='/register' element={<Wrapper><Register /> </Wrapper>} />
                        <Route path='/verify' element={<Wrapper><Verify /> </Wrapper>} />
                        <Route path='/betalen' element={<Wrapper><Betalen /> </Wrapper>} />
                        <Route path='/bedankt' element={<Wrapper><Bedankt /> </Wrapper>} />
                        <Route path='/donatie' element={<Wrapper><DoneerPagina /></Wrapper>} />
                        <Route path='/user/resetwachtwoord' element={<Wrapper><WachtwoordResetten /></Wrapper>} />
                        <Route path='/medewerker' element={<Wrapper><ShowInzien/></Wrapper>}/>
                        <Route path='/user/bestellingen' element={<Wrapper><Bestellingen /> </Wrapper>} />

                        <Route path='/ticket/:data' element={<Ticket />} />
                        <Route path='*' element={<FourOOOFour />} />
                    </Routes>

                </Router>
            </div>
        </AuthProvider>
    )
}

const Wrapper = ({ children }) => {
    return (
        <>
            <NavBar />
            {children}
            <Footer />
        </>
    )
}