import { Link } from "react-router-dom";

function HomePage() {
    return (
        <>
        <header className="d-flex masthead">
            <div className="container my-auto text-center">
                <h1 className="mb-1">LIFT</h1>
                <h3 className="mb-5"><em>LOSING IS FUN TOO</em></h3>
                <Link className="btn btn-primary btn-xl js-scroll-trigger" role="button" to={"/play"}>Create game</Link>
                <form className="form-inline d-flex justify-content-center">
                    <div className="form-group">
                        <label className="sr-only" htmlFor="joinGame">Game Code</label>
                        <input className="form-control" type="text" id="joinGame" placeholder="Game Code" />
                        <button className="btn btn-primary" type="submit">Join</button>
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
