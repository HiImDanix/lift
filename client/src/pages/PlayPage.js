import PropTypes from "prop-types";
import {useState} from "react";
import Game from "../components/Game";

function PlayPage(props) {

    const [displayName, setDisplayName] = useState("Anonymous");
    const [gameStarted, setGameStarted] = useState(false);

    function startGame() {
        setGameStarted(true);
    }

    if (gameStarted) {
        return (
            <Game displayName={displayName} />
        );
    } else {
        return (
            <>
                <h1>Lobby</h1>
                <button className={"btn btn-primary"} onClick={startGame}>Start game</button>
            </>
        )
    }
}

PlayPage.propTypes = {
    startGame: PropTypes.func
}

export default PlayPage;