import GuessingGame from "../components/GuessingGame";
import Scoreboard from "../components/Scoreboard";
import {useState} from "react";

const GamePage = {
    QUESTION: 0,
    SCOREBOARD: 1,
    GAME_OVER: 2
}

function Game() {

    const [page, setPage] = useState(GamePage.QUESTION);

    switch (page) {
        case GamePage.QUESTION:
            return <GuessingGame setPage={setPage}/>;
        case GamePage.SCOREBOARD:
            return <Scoreboard  />;
        case GamePage.GAME_OVER:
            return <div>Game Over</div>;
    }
}

export {GamePage};
export default Game;