function Scoreboard() {
    return (
        <div>
            <nav className="navbar navbar-light navbar-expand-md py-3">
                <div className="container"><a className="navbar-brand d-flex align-items-center" href="#"><span
                    className="bs-icon-sm bs-icon-rounded bs-icon-primary d-flex justify-content-center align-items-center me-2 bs-icon"><svg
                    xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" fill="currentColor" viewBox="0 0 16 16"
                    className="bi bi-bezier">
                            <path fill-rule="evenodd"
                                  d="M0 10.5A1.5 1.5 0 0 1 1.5 9h1A1.5 1.5 0 0 1 4 10.5v1A1.5 1.5 0 0 1 2.5 13h-1A1.5 1.5 0 0 1 0 11.5v-1zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1zm10.5.5A1.5 1.5 0 0 1 13.5 9h1a1.5 1.5 0 0 1 1.5 1.5v1a1.5 1.5 0 0 1-1.5 1.5h-1a1.5 1.5 0 0 1-1.5-1.5v-1zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1zM6 4.5A1.5 1.5 0 0 1 7.5 3h1A1.5 1.5 0 0 1 10 4.5v1A1.5 1.5 0 0 1 8.5 7h-1A1.5 1.5 0 0 1 6 5.5v-1zM7.5 4a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1z"></path>
                            <path
                                d="M6 4.5H1.866a1 1 0 1 0 0 1h2.668A6.517 6.517 0 0 0 1.814 9H2.5c.123 0 .244.015.358.043a5.517 5.517 0 0 1 3.185-3.185A1.503 1.503 0 0 1 6 5.5v-1zm3.957 1.358A1.5 1.5 0 0 0 10 5.5v-1h4.134a1 1 0 1 1 0 1h-2.668a6.517 6.517 0 0 1 2.72 3.5H13.5c-.123 0-.243.015-.358.043a5.517 5.517 0 0 0-3.185-3.185z"></path>
                        </svg></span><span>&lt;Guest&gt;</span></a>
                    <button className="btn btn-primary" type="button">Log in</button>
                </div>
            </nav>
            <h1 className="text-uppercase fw-bold text-center">Scoreboard</h1>
            <div className="table-responsive">
                <table className="table">
                    <thead>
                    <tr>
                        <th className="text-center">Position</th>
                        <th className="text-start">Name</th>
                        <th>Score</th>
                    </tr>
                    </thead>
                    <tbody>
                    <tr>
                        <td className="text-center">1</td>
                        <td>ProGamer25</td>
                        <td>9999</td>
                    </tr>
                    <tr className="text-bg-warning">
                        <td className="text-center">2</td>
                        <td>Jackboringname<br /></td>
                        <td>5734</td>
                    </tr>
                    <tr>
                        <td className="text-center">3</td>
                        <td>ISUCKATGAMES</td>
                        <td>4434</td>
                    </tr>
                    <tr>
                        <td className="text-center">4</td>
                        <td>bruhlmao</td>
                        <td>3069</td>
                    </tr>
                    <tr>
                        <td className="text-center">5</td>
                        <td>n00bhunter64</td>
                        <td>2000</td>
                    </tr>
                    <tr>
                        <td className="text-center">6</td>
                        <td>tier3sub</td>
                        <td>654</td>
                    </tr>
                    <tr>
                        <td className="text-center">7</td>
                        <td>aabpiratesfan</td>
                        <td>397</td>
                    </tr>
                    <tr>
                        <td className="text-center">8</td>
                        <td>gamer10</td>
                        <td>1</td>
                    </tr>
                    <tr>
                        <td className="text-center">9</td>
                        <td>randersRULEZ</td>
                        <td>0</td>
                    </tr>
                    </tbody>
                </table>
            </div>
            <div className="btn-toolbar">
                <div className="btn-group d-flex flex-grow-0 flex-fill justify-content-center align-items-center mx-auto"
                     role="group">
                    <button className="btn btn-primary active" type="button">1</button>
                    <button className="btn btn-primary" type="button">2</button>
                </div>
                <div className="btn-group" role="group"></div>
            </div>
        </div>
    );
}

export default Scoreboard;