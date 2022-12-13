import Countdown from "react-countdown";
import {useEffect, useState} from "react";
import PropTypes from "prop-types";
import Config from "../../Config";


function GuessingGame(props) {

    const gameID = props.gameID;
    const gameQuestionID = props.gameQuestionID;
    const question = props.gameData.questionText;
    const image = props.gameData.imagePath;

    // TODO: Make answer available only server-side
    const answers = props.gameData.answers;
    // Note: With the current implementation, the correct answer is always the first answer in the array
    const correctAnswer = answers.find(answer => answer.isCorrect);

    const [selectedAnswer, setSelectedAnswer] = useState(null);
    const [selectingAnswer, setSelectingAnswer] = useState(false);

    const canAnswer = () => {
        // Check existence of selected answer
        for (let i = 0; i < props.playerAnswers.length; i++) {
            if (props.playerAnswers[i].player.id === props.myID && props.playerAnswers[i].gameQuestionId === gameQuestionID) {
                return false;
            }
        }
        // Check if answer has been selected (client-side)
        if (!selectingAnswer && selectedAnswer == null) {
            return true;
        }
        return false;
    }

    // Handle answer selection. If correct, score a point. Then, end the round
    function selectAnswer(answer) {
        if (canAnswer()) {
            setSelectingAnswer(true);
            // Send to server as POST with fetch api
            fetch(`${Config.SERVER_URL}/games/${gameID}/answers`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.getItem('session')}`
                },
                body: JSON.stringify({
                        gameQuestionID: gameQuestionID,
                        answer: answer.answerText
                    }
                )}).then(response => {
                if (response.ok) {
                    setSelectedAnswer(answer.id);
                }
            }).catch(error => {
                console.log("Error sending answer to server: " + error);
            });
            setSelectingAnswer(false);
        }
    }

    useEffect(() => {
        // Check if answer has been selected (server-side)
        if (selectedAnswer == null) {
            for (let i = 0; i < props.playerAnswers.length; i++) {
                if (props.playerAnswers[i].player.id === props.myID && props.playerAnswers[i].gameQuestionId === gameQuestionID) {
                    // Mark answer as selected
                    setSelectedAnswer(props.playerAnswers[i].answer.id);
                }
            }
        }
    })


    function determineAnswerClass(answer) {
        // If answer is not null, show correct answer btn-success, incorrect btn-danger,
        // and if the one selected is not correct, show btn-secondary.
        // Otherwise, show btn-primary
        if (selectedAnswer) {
            if (answer.id === correctAnswer.id) {
                return "btn-success";
            }
            if (selectedAnswer === answer.id && answer.id !== correctAnswer) {
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
                {answers.map((answer) => {
                    return (
                        <div className="col-6 col-md-6 text-center" key={answer.id}>
                            <button
                                // If answer is selected, show correct answer green, wrong answer red. If not selected, show primary button
                                className={"btn btn-lg text-center border rounded-0 w-100 " + determineAnswerClass(answer)}
                                type="button" onClick={() => selectAnswer(answer)}>
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
    }),
    gameID: PropTypes.number,
    gameQuestionID: PropTypes.number,
    playerAnswers: PropTypes.arrayOf(PropTypes.shape({
        answer: PropTypes.arrayOf(PropTypes.shape({
            answerText: PropTypes.string,
            isCorrect: PropTypes.bool
        })),
        answeredTime: PropTypes.number,
        id: PropTypes.number,
        gameQuestionId: PropTypes.number,
        question: PropTypes.arrayOf(PropTypes.shape({
            id: PropTypes.number,
            questionText: PropTypes.string,
            imagePath: PropTypes.string,
            answers: PropTypes.arrayOf(PropTypes.shape({
            answerText: PropTypes.string,
            isCorrect: PropTypes.bool
            }))
        })),
        player: PropTypes.arrayOf(PropTypes.shape({
            id: PropTypes.number,
            name: PropTypes.string
        }))
    })),
    myID: PropTypes.number
};

export default GuessingGame;