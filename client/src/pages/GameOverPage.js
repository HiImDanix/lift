import Scoreboard from "../components/Scoreboard";

function GameOverPage() {
    return (
        <>
            <div className={"section_spacing"}>
                <h1 className="text-center">Congratulations!</h1>
                <div className="mt-5">
                    <div className="row gx-0">
                        <div className="col d-flex justify-content-end align-items-end">
                            <div className="bg-primary d-flex justify-content-center game-over-pedestial-bar t-place-bar">
                                <div className="position-relative"><strong className="position-absolute translate-middle-x game-over-pedestial-username">HHHHHHHHHHHHHHHH</strong></div>
                            </div>
                        </div>
                        <div className="col d-flex justify-content-center">
                            <div className="bg-secondary d-flex justify-content-center game-over-pedestial-bar f-place-bar">
                                <div className="position-relative"><strong className="position-absolute translate-middle-x game-over-pedestial-username">BoldDdsdsdds</strong></div>
                            </div>
                        </div>
                        <div className="col d-flex d-md-flex justify-content-start align-items-end">
                            <div className="bg-primary d-flex justify-content-center game-over-pedestial-bar s-place-bar">
                                <div className="position-relative"><strong className="position-absolute translate-middle-x game-over-pedestial-username">BoldDdsdsdds</strong></div>
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
                <Scoreboard />
            </div>

        </>
    );
}

export default GameOverPage;