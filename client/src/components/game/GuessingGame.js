import Countdown from "react-countdown";
import {useState} from "react";
import PropTypes from "prop-types";


function GuessingGame(props) {

    // Hardcoded data for now. Will be fetched from server later
    const [gameData, setGameData] = useState({
        "question": "What is this logo?",
        "image": "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2f/Google_2015_logo.svg/1200px-Google_2015_logo.svg.png",
        "answers": [
            "Google",
            "Facebook",
            "Twitter",
            "Instagram"
        ],
        "correctAnswer": "Google"
    });

    const [startTime, setStartTime] = useState(Date.now());
    const [question, setQuestion] = useState(gameData.question);
    const [image, setImage] = useState(gameData.image);
    const [answers, setAnswers] = useState(gameData.answers);
    const [correctAnswer, setCorrectAnswer] = useState(gameData.correctAnswer);
    const [selectedAnswer, setSelectedAnswer] = useState(null);
    const [selectedAnswerTime, setSelectedAnswerTime] = useState(null);

    const CORRECT_ANSWER_DURATION = 2000;

    // Handle answer selection. If correct, score a point. Then, end the round
    function selectAnswer(answer) {
        setSelectedAnswerTime(Date.now());
        setSelectedAnswer(answer);
        if (selectedAnswer === correctAnswer) {
            props.scorePoint();
        }
        // Wait 2 second before ending the round
        setTimeout(props.roundFinished, CORRECT_ANSWER_DURATION);
    }



    return (
        <>
            <div className="container d-flex flex-column flex-fill justify-content-center">
                <div className="d-flex flex-column flex-fill justify-content-center align-items-center"><img
                    className="mb-3" src={image} height="200" />
                    <h1>{question}</h1>
                    {selectedAnswerTime === null ? (
                        <Countdown date={startTime + props.roundDurationMs} onComplete={props.roundFinished} />
                    ) : (
                        <Countdown date={selectedAnswerTime + CORRECT_ANSWER_DURATION} onComplete={props.roundFinished} />
                    )}

                </div>
            </div>
            <div className="row gx-0 gy-0 justify-content-center align-items-center">
                {answers.map((answer, index) => {
                    return (
                        <div className="col-6 col-md-6 text-center" key={index}>
                            <button
                                // If answer is selected, show correct answer green, wrong answer red. If not selected, show primary button
                                className={"btn btn-lg text-center border rounded-0 w-100" + (selectedAnswer != null ? (answer === correctAnswer ? " btn-success" : " btn-danger") : " btn-primary")}
                                type="button" onClick={() => selectAnswer(answer)}>
                                {answer}
                            </button>
                        </div>
                    );
                })}

            </div>
        </>
    );
}

GuessingGame.propTypes = {
    roundFinished: PropTypes.func.isRequired,
    scorePoint: PropTypes.func.isRequired,
    roundDurationMs: PropTypes.number.isRequired,
}

export default GuessingGame;