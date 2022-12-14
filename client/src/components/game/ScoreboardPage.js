import Scoreboard from "../../components/Scoreboard";
import PropTypes from "prop-types";
import Countdown from "react-countdown";

function ScoreboardPage(props) {
    return (
        <>

            <h1 className="text-uppercase fw-bold text-center">Scoreboard</h1>
            <Countdown className={"text-center d-block"} date={props.scoreboardEndTime} />
            <Scoreboard displayName={props.displayName} scoreboardEndTime={props.scoreboardEndTime} scoreboard={props.scoreboard} />
        </>
    );
}

ScoreboardPage.propTypes = {
    displayName: PropTypes.string.isRequired,
    scoreboardEndTime: PropTypes.number.isRequired,
    // scoreboard: PropTypes.shape({
    //     scores: PropTypes.arrayOf(PropTypes.shape({
    //         position: PropTypes.number,
    //         username: PropTypes.string,
    //         score: PropTypes.number
    //     }))
    // })
}

export default ScoreboardPage;