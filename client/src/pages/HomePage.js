import { useNavigate } from "react-router-dom";
import Config from "../Config.js";
import {useState} from "react";

function HomePage() {

    const navigate = useNavigate();

    const [roomCode, setRoomCode] = useState("");

    async function createGame() {
        // get username by fetching https://randomuser.me/api/ and retrieving response.results.login.username
        let username = await fetch("https://randomuser.me/api/").then(res => res.json()).then(data => data.results[0].login.username);
        const res = await fetch(`${Config.SERVER_URL}/lobby`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                playerName: username,
            })
        });


        if (res.status === 200) {
            const data = await res.json();
            // put state in local storage
            localStorage.setItem("session", data.session);
            // redirect to play page
            // destructure data as state
            navigate("/play", {state: {data}});
        } else {
            alert("Error creating game");
        }
    }

    const joinLobby = async (e) => {
        e.preventDefault();
        let username = await fetch("https://randomuser.me/api/").then(res => res.json()).then(data => data.results[0].login.username);
        const res = await fetch(`${Config.SERVER_URL}/lobby/` + roomCode + "/join", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                playerName: username,
            })
        });

        if (res.status === 200) {
            const data = await res.json();
            // put state in local storage
            localStorage.setItem("session", data.session);
            // redirect to play page
            // destructure data as state
            navigate("/play", {state: {data}});
        } else {
            alert("Invalid room code");
        }


    }


    return (
        <>
        <header className="d-flex masthead">
            <div className="container my-auto text-center">
                <h1 className="mb-1">LIFT</h1>
                <h3 className="mb-5"><em>LOSING IS FUN TOO</em></h3>
                <button className="btn btn-primary btn-xl js-scroll-trigger" role="button" onClick={createGame}>Create lobby</button>
                <form className="form-inline d-flex justify-content-center">
                    <div className="form-group">
                        <label className="sr-only" htmlFor="joinGame">Game Code</label>
                        <input className="form-control" type="text" id="joinGame" placeholder="Game Code"
                               onChange={e => setRoomCode(e.target.value)} value={roomCode.toUpperCase()} />
                        <button className="btn btn-primary" type="submit" onClick={joinLobby}>Join</button>
                    </div>

                </form>
                <div className="overlay"></div>
            </div>
        </header>
            <div className="text-center text-sm-left">
                <p>
                    To create a new game lobby, click the "Create a new lobby" button. You
                    will be given a game code that you can share with other players to invite
                    them to join your lobby.
                </p>
                <p>
                    To join an existing game lobby, enter the game code in the input box and
                    click the "Join lobby" button.
                </p>
            </div>
        </>
    );
}

export default HomePage;
