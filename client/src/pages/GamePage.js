import GuessingGame from "../components/GuessingGame";
import ScoreboardPage from "./game/ScoreboardPage";
import {useState} from "react";
import GameOverPage from "./game/GameOverPage";

const GamePageEnum = {
    QUESTION: 0,
    SCOREBOARD: 1,
    GAME_OVER: 2
}
export {GamePageEnum};

function GamePage() {

    const [page, setPage] = useState(GamePageEnum.QUESTION);
    const [round, setRound] = useState(0);
    const [maxRounds, setMaxRounds] = useState(2);
    const [roundDurationMs, setRoundDurationMs] = useState(5000);
    const [scoreboardDurationMs, setScoreboardDurationMs] = useState(3000);
    const [score, setScore] = useState(0);


    // Start a new round
    function nextRound() {
        setRound(round + 1);
        setPage(GamePageEnum.QUESTION);
    }

    // Round finished:
    // - Show scoreboard
    // - If last round, show game over page
    function roundFinished() {
        if (round > maxRounds) {
            setPage(GamePageEnum.GAME_OVER);
        } else {
            setPage(GamePageEnum.SCOREBOARD);
        }
    }

    // Scoreboard screen finished:
    // - Start next round
    function scoreboardFinished() {
        nextRound();
    }

    // Score a point
    function scorePoint() {
        setScore(score + 1);
    }

    switch (page) {
        case GamePageEnum.QUESTION:
            return <GuessingGame roundFinished={roundFinished} scorePoint={scorePoint} roundDurationMs={roundDurationMs} />;
        case GamePageEnum.SCOREBOARD:
            return <ScoreboardPage scoreboardFinished={scoreboardFinished} scoreboardDurationMs={scoreboardDurationMs} score={score} />;
        case GamePageEnum.GAME_OVER:
            return <GameOverPage />;
        default:
            return <div>Unknown page</div>;
    }
}
export default GamePage;