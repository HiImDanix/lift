import ScoreboardPage from "./game/ScoreboardPage";
import PropTypes from "prop-types";
import {useState} from "react";

function Scoreboard(props) {


    // Hardcoded data
    const [scoreboardData, setScoreboardData] = useState({
        "scores": [
            {
                "position": 1,
                "username": "ProGamer25",
                "score": 100
            },
            {
                "position": 2,
                "username": "HardBoiledEgg",
                "score": 50
            },
            {
                "position": 3,
                "username": "n00bhunter69",
                "score": 50
            },
            {
                "position": 4,
                "username": "TheRealSlimShady",
                "score": 25
            },
            {
                "position": 5,
                "username": "Haxx0r",
                "score": 25
            },
            {
                "position": 6,
                "username": props.displayName,
                "score": 0
            }
        ]

    });


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
                        scoreboardData.scores.map((score, index) => {
                            return (
                                <tr key={index} {...(score.username === props.displayName ? {className: "text-bg-warning"} : {})}>
                                    <td className="text-center">{score.position}</td>
                                    <td className="text-start">{score.username}</td>
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
    displayName: PropTypes.string.isRequired
}

export default Scoreboard;