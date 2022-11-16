import { Link } from "react-router-dom";

function HomePage() {
    return (
        <div id="page-top"><a className="menu-toggle rounded" href="src/pages/Index#HomePage.js"><i
            className="fa fa-bars"></i></a>
        <nav className="navbar navbar-light navbar-expand" id="sidebar-wrapper">
            <div className="container">
                <button data-bs-toggle="collapse" className="navbar-toggler d-none" data-bs-target="#"></button>
                <div className="collapse navbar-collapse">
                    <ul className="navbar-nav sidebar-nav" id="sidebar-nav">
                        <li className="nav-item sidebar-brand"><a className="nav-link active js-scroll-trigger"
                                                                  href="src/pages/HomePage.js">Brand</a></li>
                        <li className="nav-item sidebar-nav-item"><a className="nav-link js-scroll-trigger"
                                                                     href="src/pages/HomePage.js">HomePage</a></li>
                        <li className="nav-item sidebar-nav-item"><a className="nav-link js-scroll-trigger"
                                                                     href="src/pages/HomePage.js">About</a></li>
                        <li className="nav-item sidebar-nav-item"><a className="nav-link js-scroll-trigger"
                                                                     href="src/pages/HomePage.js">Services</a></li>
                        <li className="nav-item sidebar-nav-item"><a className="nav-link js-scroll-trigger"
                                                                     href="src/pages/HomePage.js">Portfolio</a></li>
                        <li className="nav-item sidebar-nav-item"><a className="nav-link js-scroll-trigger"
                                                                     href="src/pages/HomePage.js">Contact</a></li>
                    </ul>
                </div>
            </div>
        </nav>
        <header className="d-flex masthead">
            <div className="container my-auto text-center">
                <h1 className="mb-1">LIFT</h1>
                <h3 className="mb-5"><em>LOSING IS FUN TOO</em></h3>
                <Link className="btn btn-primary btn-xl js-scroll-trigger" role="button" to={"/play"}>Play</Link>
                <div className="overlay"></div>
            </div>
        </header>
        <section id="about" className="content-section bg-light">
            <div className="container text-center">
                <div className="row">
                    <div className="col-lg-10 mx-auto"><a className="btn btn-dark btn-xl js-scroll-trigger"
                                                          role="button" href="src/pages/HomePage.js">What
                        We Offer</a></div>
                </div>
            </div>
        </section>
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
        </div>
    );
}

export default HomePage;
