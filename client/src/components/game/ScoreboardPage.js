import Scoreboard from "../../components/Scoreboard";
import PropTypes from "prop-types";
import Countdown from "react-countdown";

function ScoreboardPage(props) {
    return (
        <>

            <h1 className="text-uppercase fw-bold text-center">Scoreboard</h1>
            <Countdown className={"text-center d-block"} date={Date.now() + props.scoreboardDurationMs} onComplete={props.scoreboardFinished} />
            <Scoreboard score={props.score} />
        </>
    );
}

ScoreboardPage.propTypes = {
    scoreboardFinished: PropTypes.func.isRequired,
    scoreboardDurationMs: PropTypes.number.isRequired,
    score: PropTypes.number.isRequired
}

export default ScoreboardPage;