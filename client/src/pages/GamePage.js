import GuessingGame from "../components/GuessingGame";
import ScoreboardPage from "./game/ScoreboardPage";
import {useState} from "react";
import GameOverPage from "./GameOverPage";

const GamePageEnum = {
    QUESTION: 0,
    SCOREBOARD: 1,
    GAME_OVER: 2
}

function GamePage() {

    const [page, setPage] = useState(GamePageEnum.QUESTION);
    const [round, setRound] = useState(0);
    const [maxRounds, setMaxRounds] = useState(2);

    function nextRound() {
        setRound(round + 1);
        setPage(GamePageEnum.QUESTION);
    }

    function roundFinished() {
        if (round > maxRounds) {
            setPage(GamePageEnum.GAME_OVER);
        } else {
            setPage(GamePageEnum.SCOREBOARD);
        }
    }

    switch (page) {
        case GamePageEnum.QUESTION:
            return <GuessingGame setPage={setPage} roundFinished={roundFinished}/>
        case GamePageEnum.SCOREBOARD:
            // asynchronous call to start next round after 5 seconds
            setTimeout(nextRound, 5000);
            return <ScoreboardPage />;
        case GamePageEnum.GAME_OVER:
            return <GameOverPage />;
        default:
            return <div>Unknown page</div>;
    }
}

export {GamePageEnum};
export default GamePage;