import Countdown from "react-countdown";
import {useState} from "react";
import PropTypes from "prop-types";


function GuessingGame(props) {
    return (
        <>
            <div className="container d-flex flex-column flex-fill justify-content-center">
                <div className="d-flex flex-column flex-fill justify-content-center align-items-center"><img
                    className="mb-3" src="img/logo_1.jpg" height="200" />
                    <h1>Which brand has this logo?</h1>
                    <Countdown date={Date.now() + props.roundDurationMs} onComplete={props.roundFinished} />
                </div>
            </div>
            <div className="row gx-0 gy-0 justify-content-center align-items-center">
                <div className="col-6 col-md-6 text-center">
                    <button className="btn btn-primary btn-lg text-center border rounded-0 w-100" type="button">Apple
                    </button>
                </div>
                <div className="col-6 col-md-6 text-center">
                    <button className="btn btn-primary btn-lg border rounded-0 w-100" type="button">Amazon</button>
                </div>
                <div className="col-6 col-md-6 text-center">
                    <button className="btn btn-primary btn-lg border rounded-0 w-100" type="button">Tesla</button>
                </div>
                <div className="col-6 col-md-6 text-center">
                    <button className="btn btn-primary btn-lg border rounded-0 border-0 w-100" type="button">BMW</button>
                </div>
            </div>
        </>
    );
}

GuessingGame.propTypes = {
    roundFinished: PropTypes.func.isRequired,
    scorePoint: PropTypes.func.isRequired,
    roundDurationMs: PropTypes.number.isRequired
}

export default GuessingGame;