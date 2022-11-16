import {Link} from "react-router-dom";
import PropTypes from "prop-types";

function Nav(props) {
    return (
        <nav className="navbar navbar-light navbar-expand-md py-3">
            <div className="container">
                <ul className="navbar-nav me-auto">
                    <li className="nav-item"><a className="nav-link active" href="src/components/GuessingGame#">Hi, {props.username}</a></li>
                </ul>
                <Link to={"/"}><button className="btn btn-danger" type="button">Quit</button></Link>
            </div>
        </nav>
    )
}

Nav.propTypes = {
    username: PropTypes.string.isRequired
}

export default Nav;