import './App.css';
import HomePage from "./pages/HomePage";
import PlayPage from "./pages/PlayPage";
import {BrowserRouter, Route, Routes} from "react-router-dom";


function App() {

  return (
      <BrowserRouter>
        <Routes>
            <Route path="/" element={<HomePage/>}/>
            <Route path="/play" element={<PlayPage/>}/>
        </Routes>
      </BrowserRouter>
    );
}

export default App;
