import ScoreboardPage from "./game/ScoreboardPage";
import PropTypes from "prop-types";
import {useState} from "react";

function Scoreboard(props) {
    console.log("wtf - " + props.scoreboard);
    return (
        <>
            <div className="table-responsive">
                <table className="table">
                    <thead>
                    <tr>
                        <th className="text-center">Position</th>
                        <th className="text-start">Name</th>
                        <th>Score</th>
                    </tr>
                    </thead>
                    <tbody>
                    {
                        props.scoreboard.scores.map((score, index) => {
                            return (
                                <tr key={index} {...(score.player.name === props.displayName ? {className: "text-bg-warning"} : {})}>
                                    <td className="text-center">{score.position}</td>
                                    <td className="text-start">{score.player.name}</td>
                                    <td>{score.score}</td>
                                </tr>
                            );
                        })
                    }
                    </tbody>
                </table>
            </div>
        <div className="btn-toolbar">
            <div className="btn-group d-flex flex-grow-0 flex-fill justify-content-center align-items-center mx-auto"
                 role="group">
                <button className="btn btn-primary active" type="button">1</button>
                <button className="btn btn-primary" type="button">2</button>
            </div>
            <div className="btn-group" role="group"></div>
        </div>
    </>
    )
}

Scoreboard.propTypes = {
    displayName: PropTypes.string.isRequired,
    // scoreboard contains array of scores
    // scoreboard: PropTypes.shape({
    //     scores: PropTypes.arrayOf(PropTypes.shape({
    //         position: PropTypes.number,
    //         username: PropTypes.string,
    //         score: PropTypes.number
    //     }))
    // })
}

export default Scoreboard;