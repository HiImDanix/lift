import Scoreboard from "../../components/Scoreboard";
import PropTypes from "prop-types";

function GameOverPage(props) {
    return (
        <>
            <div className={"section_spacing"}>
                <h1 className="text-center">Congratulations!</h1>
                <div className="mt-5">
                    <div className="row gx-0">
                        <div className="col d-flex justify-content-end align-items-end">
                            <div className="bg-secondary d-flex justify-content-center game-over-pedestial-bar t-place-bar">
                                <div className="position-relative"><strong className="position-absolute translate-middle-x game-over-pedestial-username">n00bhunter69</strong></div>
                            </div>
                        </div>
                        <div className="col d-flex justify-content-center">
                            <div className="bg-warning d-flex justify-content-center game-over-pedestial-bar f-place-bar">
                                <div className="position-relative"><strong className="position-absolute translate-middle-x game-over-pedestial-username">ProGamer25</strong></div>
                            </div>
                        </div>
                        <div className="col d-flex d-md-flex justify-content-start align-items-end">
                            <div className="bg-secondary d-flex justify-content-center game-over-pedestial-bar s-place-bar">
                                <div className="position-relative"><strong className="position-absolute translate-middle-x game-over-pedestial-username">HardBoiledEgg</strong></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="container fixed-bottom mb-3">
                    <div className="btn-group d-flex justify-content-center" role="group">
                        <button className="btn btn-primary" type="button">Back to Lobby<i
                            className="fas fa-arrow-left ms-2"></i></button>
                        <button className="btn btn-light" type="button">Play Again<i className="fas fa-redo ms-2"></i>
                        </button>
                        <button className="btn btn-secondary" type="button">Share my score<i
                            className="fas fa-share-alt ms-2"></i></button>
                    </div>
                </div>
            </div>
            <div className={"section_spacing last_section"}>
                <Scoreboard displayName={props.displayName} scoreboardEndTime={props.scoreboardEndTime} scoreboard={props.scoreboard} />/>
                <Scoreboard displayName={props.displayName} scoreboardEndTime={props.scoreboardEndTime} scoreboard={props.scoreboard} />
            </div>

        </>
    );
}

GameOverPage.propTypes = {
    displayName: PropTypes.string.isRequired,
    scoreboardEndTime: PropTypes.number.isRequired
}

export default GameOverPage;