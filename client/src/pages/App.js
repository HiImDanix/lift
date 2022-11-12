import logo from '../logo.svg';
import '../App.css';
import {useState} from "react";
import Home from "./Home";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import GuessingGame from "./GuessingGame";

function App() {

  return (
      <BrowserRouter>
        <Routes>
            <Route path="/" element={<Home/>}/>
            <Route path="/play" element={<GuessingGame/>}/>
        </Routes>
      </BrowserRouter>
    );
}

export default App;
