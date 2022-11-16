function Scoreboard() {
    return (
        <>
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
    </>
    )
}

export default Scoreboard;