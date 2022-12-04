import PropTypes from "prop-types";
import {useEffect, useState} from "react";
import Game from "../components/Game";
import { useLocation, useNavigate } from "react-router-dom";
import Nav from "../components/Nav";
import {HubConnectionBuilder} from "@microsoft/signalr";
import Config from "../Config";

function PlayPage() {
    const navigate = useNavigate();
    const location = useLocation();
    const state = location.state;

    // Validate if user should be able to access this page
    const [validating, setValidating] = useState(true);

    // Signal R connection
    const [ connection, setConnection ] = useState(null);

    // Lobby state from create/join
    const [displayName, setDisplayName] = useState(state?.data.name);
    const [myId, setMyId] = useState(state?.data.id);
    const [session, setSession] = useState(state?.data.session); // The JWT session token

    // Lobby state - retrieved from server
    const [lobbyID, setLobbyID] = useState(state?.data.room.id);
    const [lobbyCode, setLobbyCode] = useState(state?.data.room.code);
    const [players, setPlayers] = useState(state?.data.room.players);
    const [gameStarted, setGameStarted] = useState(false);

    // Validate if user should be able to access this page, else redirect to home
    useEffect(() => {
        if (state === null) {
            return navigate("/");
        }
        // Retrieve the lobby state from the server
        getLobbyState();
        setValidating(false);
    }, []);

    // Signal R connection
    useEffect(() => {
        // connect with bearer token
        const newConnection = new HubConnectionBuilder()
            .withUrl(`${Config.SERVER_URL}/hubs/game`, {
                accessTokenFactory: () => session
            })
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);
    }, []);


    useEffect(() => {
        if (connection) {
            connection.start()
                .then(result => {
                    console.log('Connected!');

                    connection.on('PlayerJoined', player => {
                        console.log('Player joined', player);
                        setPlayers([...players, player]);
                    });
                })
                .catch(e => console.log('Connection failed: ', e));
        }
    }, [connection]);

    // Methods

    const getLobbyState = () => {
        fetch(`${Config.SERVER_URL}/lobby/${lobbyID}`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",

            }
        }).then(res => res.json())
            .then(data => {
                setLobbyCode(data.code);
                setPlayers(data.players);
            });
    };


    function startGame() {
        alert("Gameplay is simulated. No actual game will be played.");
        setGameStarted(true);
    }


    // Render
    if (validating) {
        return <div>Validating...</div>
    } else
    {
        if (gameStarted) {
            return (
                <Game displayName={displayName} />
            );
        } else {
            return (
                <>
                    <Nav username={displayName} />
                    <div className="container">
                        <h1 className={"text-center"}>Lobby</h1>
                        <h2>Your name: {displayName}</h2>
                        <h2>Game code: {lobbyCode}</h2>
                        <h2>Players:</h2>
                        <ul>
                            {players.map((player) => (
                                <li key={"player-" + player.id}>{player.name}</li>
                            ))}
                        </ul>
                        <div className="text-center">
                            <button className={"btn btn-primary btn-lg"} onClick={startGame}>Start game</button>
                        </div>
                    </div>
                </>
            )
        }
    }

}

PlayPage.propTypes = {
    startGame: PropTypes.func
}

export default PlayPage;