import './App.css';
import HomePage from "./pages/HomePage";
import GamePage from "./pages/GamePage";
import {BrowserRouter, Route, Routes} from "react-router-dom";


function App() {

  return (
      <BrowserRouter>
        <Routes>
            <Route path="/" element={<HomePage/>}/>
            <Route path="/play" element={<GamePage/>}/>
        </Routes>
      </BrowserRouter>
    );
}

export default App;
