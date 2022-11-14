import './App.css';
import Home from "./pages/Home";
import Game from "./pages/Game";
import {BrowserRouter, Route, Routes} from "react-router-dom";


function App() {

  return (
      <BrowserRouter>
        <Routes>
            <Route path="/" element={<Home/>}/>
            <Route path="/play" element={<Game/>}/>
        </Routes>
      </BrowserRouter>
    );
}

export default App;
