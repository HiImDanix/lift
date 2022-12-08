import PropTypes from "prop-types";
import Countdown from "react-countdown";

function SecondsCountdownRenderer({ seconds }) {
    return <span>{seconds}</span>;
}

function InstructionsGuessingGame(props) {
    return (
        <>
            <h1 className="text-uppercase fw-bold text-center">Instructions</h1>
            <p className="text-center">You will be shown a logo. You must guess the name of the company that owns the logo.</p>
            <p className="text-center">You will have {props.timeLimit} seconds to guess the name of the company.</p>
            <p className="text-center">The game will start in <Countdown date={props.startTime}
                                                                         renderer={SecondsCountdownRenderer} /> seconds
            </p>
            <p className="text-center">Have fun!</p>
        </>
    );
}

InstructionsGuessingGame.propTypes = {
    timeLimit: PropTypes.number.isRequired,
    startTime: PropTypes.number.isRequired
}


export default InstructionsGuessingGame;