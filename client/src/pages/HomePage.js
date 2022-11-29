import { useNavigate } from "react-router-dom";
import Config from "../Config.js";
import {useState} from "react";

function HomePage() {

    const navigate = useNavigate();

    const [roomCode, setRoomCode] = useState("sd");

    async function createGame() {
        // get username by fetching https://randomuser.me/api/ and retrieving response.results.login.username
        let username = await fetch("https://randomuser.me/api/").then(res => res.json()).then(data => data.results[0].login.username);
        const res = await fetch(`${Config.SERVER_URL}/players`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                playerName: username,
            })
        });
        const data = await res.json();

        if (res.status === 200) {
            // put state in local storage
            localStorage.setItem("session", data.Session);
            localStorage.setItem("id", data.Id);
            localStorage.setItem("name", data.Name);
            // redirect to play page
            // destructure data as state
            navigate("/play", {state: {data}});
        } else {
            alert("Error creating game");
        }
    }



    return (
        <>
        <header className="d-flex masthead">
            <div className="container my-auto text-center">
                <h1 className="mb-1">LIFT</h1>
                <h3 className="mb-5"><em>LOSING IS FUN TOO</em></h3>
                <button className="btn btn-primary btn-xl js-scroll-trigger" role="button" onClick={createGame}>Create game</button>
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
        <footer className="footer text-center">
            <div className="container">
                <ul className="list-inline mb-5">
                    <li className="list-inline-item">&nbsp;<a className="link-light social-link rounded-circle"
                                                              href="src/pages/Index#HomePage.js"><i className="icon-social-facebook"></i></a></li>
                    <li className="list-inline-item">&nbsp;<a className="link-light social-link rounded-circle"
                                                              href="src/pages/Index#HomePage.js"><i className="icon-social-twitter"></i></a></li>
                    <li className="list-inline-item">&nbsp;<a className="link-light social-link rounded-circle"
                                                              href="src/pages/Index#HomePage.js"><i className="icon-social-github"></i></a></li>
                </ul>
                <p className="text-muted mb-0 small">Copyright &nbsp;© Brand 2022</p>
            </div>
            <a className="js-scroll-trigger scroll-to-top rounded" href="src/pages/HomePage.js"><i
                className="fa fa-angle-up"></i></a>
        </footer>
        </>
    );
}

export default HomePage;
