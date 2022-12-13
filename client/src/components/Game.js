
import {useState} from "react";

import Nav from "./Nav";
import GuessingGame from "./game/GuessingGame";
import ScoreboardPage from "./game/ScoreboardPage";
import GameOverPage from "./game/GameOverPage";
import PropTypes from "prop-types";
import InstructionsGuessingGame from "./game/GuessingGame/InstructionsGuessingGame";
import {useEffect} from "react";

const GameStatus = {
    INSTRUCTIONS: "Instructions",
    PLAYING: "Playing",
    SCOREBOARD: "Scoreboard",
    FINISHED: "Finished"
}
export {GameStatus};

function Game(props) {

    const [gameID, setGameID] = useState(props.id);
    const [status, setStatus] = useState(props.status);
    const [gameType, setGameType] = useState(props.gameType);

    const [startTime, setStartTime] = useState(props.startTime);
    const [totalRounds, setTotalRounds] = useState(props.totalRounds);
    const [roundDurationMs, setRoundDurationMs] = useState(props.roundDurationMs);
    const [scoreboardDurationMs, setScoreboardDurationMs] = useState(props.scoreboardDurationMs);

    const [currentRound, setCurrentRound] = useState(props.currentRound);
    const [currentRoundStartTime, setCurrentRoundStartTime] = useState(props.currentRoundStartTime);
    const [currentGameQuestionID, setCurrentGameQuestionID] = useState(props.currentQuizGameQuestionID);

   // TODO: Game specific. Move to one 'specific game-state' object
    const [scoreboard, setScoreboard] = useState(props.scoreboard);
    const [gameData, setGameData] = useState(props.currentQuestion);
    const [currentQuizGameQuestionID, setCurrentQuizGameQuestionID] = useState(props.currentQuizGameQuestionId)
    const [playerAnswers, setPlayerAnswers] = useState(props.playerAnswers);


    useEffect(() => {
        if (props.connection) {
            props.connection.on('RoundStarted', (game) => {
                console.log('RoundStarted', game);
                setCurrentRound(game.currentRound);
                setCurrentRoundStartTime(game.currentRoundStartTime);
                setStatus(game.status);
                setGameData(game.currentQuestion);
                setCurrentQuizGameQuestionID(game.currentQuizGameQuestionId)
            });

            props.connection.on('RoundFinished', () => {
                setStatus(GameStatus.SCOREBOARD);
            });

            props.connection.on('GameFinished', () => {
                console.log('GameFinished');
                setStatus(GameStatus.FINISHED);
            });
        }
    }, [props.connection]);


    // Start a new round
    // function nextRound() {
    //     setRound(round + 1);
    //     setPage(GamePageEnum.QUESTION);
    // }

    // Round finished:
    // - Show scoreboard
    // - If last round, show game over status
    // function roundFinished() {
    //     if (round >= maxRounds) {
    //         setPage(GamePageEnum.GAME_OVER);
    //     } else {
    //         setPage(GamePageEnum.SCOREBOARD);
    //     }
    // }

    // Scoreboard screen finished:
    // - Start next round
    // function scoreboardFinished() {
    //     nextRound();
    // }

    // Score a point
    // function scorePoint() {
    //     setScore(score + 1);
    // }

    function renderSwitch(status) {
        switch (status) {
            case GameStatus.INSTRUCTIONS:
                if (gameType == "GuessingGame") {
                    return <InstructionsGuessingGame timeLimit={roundDurationMs / 1000} startTime={startTime} />;
                }
            case GameStatus.PLAYING:
                return <GuessingGame currentRoundStartTime={currentRoundStartTime}
                                     roundDurationMs={roundDurationMs}
                                     gameData={gameData} gameID={gameID}
                                     gameQuestionID={currentQuizGameQuestionID}
                                     playerAnswers={playerAnswers}
                                     myID={props.myID}/>;
            case GameStatus.SCOREBOARD:
                return <ScoreboardPage displayName={props.displayName} scoreboardEndTime={currentRoundStartTime + roundDurationMs + scoreboardDurationMs} />;
            case GameStatus.FINISHED:
                return <GameOverPage displayName={props.displayName} scoreboardEndTime={currentRoundStartTime + roundDurationMs + scoreboardDurationMs} />;
            default:
                return <div>Unknown page</div>;
        }
    }

    return (
        <>
                {renderSwitch(status)}
        </>
    );

}

Game.propTypes = {
    displayName: PropTypes.string.isRequired,
    status: PropTypes.string.isRequired,
    gameType: PropTypes.string.isRequired,
    currentRound: PropTypes.number.isRequired,
    totalRounds: PropTypes.number.isRequired,
    roundDurationMs: PropTypes.number.isRequired,
    scoreboardDurationMs: PropTypes.number.isRequired,
    connection: PropTypes.object.isRequired,
    // Optional
    currentQuestion: PropTypes.object,
    scoreboard: PropTypes.array,
    currentQuizGameQuestionID: PropTypes.number,
    startTime: PropTypes.number,
    currentRoundStartTime: PropTypes.number,
    playerAnswers: PropTypes.array,
    myID: PropTypes.number,
}

export default Game;