import logo from './logo.svg';
import './App.css';
import FrontPage from './Frontpage';
import { BrowserRouter as Router,Routes,Route} from "react-router-dom";
import React from "react";
import ReactDOM from 'react-dom';
 


export default function Routing() {
  return (
    <Router>
      <Routes>
        
          <Route exact path="/" index element={<FrontPage />} />
          <Route path="/OverOns" element={<FrontPage/>} />
          <Route path="/Tickets" element={<FrontPage />} />
          
      </Routes>
    </Router>
  );
}
const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<Routing/>, document.getElementById('root'));

// function App() {
//   return (
//     <div className="App">
//       <header className="App-header">
//         <img src={logo} className="App-logo" alt="logo" />
//         <p>
//           Edit <code>src/App.js</code> and save to reload.
//         </p>
//         <a
//           className="App-link"
//           href="https://reactjs.org"
//           target="_blank"
//           rel="noopener noreferrer"
//         >
//           Learn React
//         </a>
//       </header>
//     </div>
//   );
// }

// export default App;
