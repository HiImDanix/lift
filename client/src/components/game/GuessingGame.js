import Countdown from "react-countdown";
import {useState} from "react";
import PropTypes from "prop-types";


function GuessingGame(props) {

    const question = props.gameData.questionText;
    const image = props.gameData.imagePath;
    // TODO: Make answer available only server-side
    const answers = props.gameData.answers;
    // Note: With the current implementation, the correct answer is always the first answer in the array
    const correctAnswer = answers.find(answer => answer.isCorrect);

    const [selectedAnswer, setSelectedAnswer] = useState(null);

    // Handle answer selection. If correct, score a point. Then, end the round
    function selectAnswer(answer) {
        // TODO: Send answer to server
        // If correct, score a point
        setSelectedAnswer(answer);

    }

    function determineAnswerClass(answer) {
        // If answer is not null, show correct answer btn-success, incorrect btn-danger,
        // and if the one selected is not correct, show btn-secondary.
        // Otherwise, show btn-primary
        if (selectedAnswer) {
            if (answer.answerText === correctAnswer.answerText) {
                return "btn-success";
            }
            if (selectedAnswer === answer.answerText && answer.answerText !== correctAnswer) {
                return "btn-secondary";
            }
            return "btn-danger";
        }
        return "btn-primary";

    }

    return (
        <>
            <div className="container d-flex flex-column flex-fill justify-content-center">
                <div className="d-flex flex-column flex-fill justify-content-center align-items-center"><img
                    className="mb-3" src={image} height="200" />
                    <h1>{question}</h1>
                    <Countdown date={props.currentRoundStartTime + props.roundDurationMs} />
                </div>
            </div>
            <div className="row gx-0 gy-0 justify-content-center align-items-center">
                {answers.map((answer, index) => {
                    return (
                        <div className="col-6 col-md-6 text-center" key={index}>
                            <button
                                // If answer is selected, show correct answer green, wrong answer red. If not selected, show primary button
                                className={"btn btn-lg text-center border rounded-0 w-100 " + determineAnswerClass(answer)}
                                type="button" onClick={() => selectAnswer(answer.answerText)}>
                                {answer.answerText}
                            </button>
                        </div>
                    );
                })}

            </div>
        </>
    );
}

GuessingGame.propTypes = {
    currentRoundStartTime: PropTypes.number,
    roundDurationMs: PropTypes.number,
    gameData: PropTypes.shape({
        questionText: PropTypes.string,
        imagePath: PropTypes.string,
        answers: PropTypes.arrayOf(PropTypes.shape({
            answerText: PropTypes.string,
            isCorrect: PropTypes.bool
        }))
    })

}

export default GuessingGame;