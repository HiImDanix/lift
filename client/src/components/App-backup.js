import logo from '../logo.svg';
import '../App.css';
import {useState} from "react";

function App() {
  const [name, setName] = useState('');
  const [responseGreeting, setResponseGreeting] = useState('');

  async function handleSubmit(event) {
    event.preventDefault();
    const data = new FormData(event.target);
    const value = data.get('name');
    const response = await fetch('https://localhost:7031/Greetings/' + value);
    if (response.ok) {
        const greeting = await response.text();
        setResponseGreeting(greeting);
    }
  }

  return (
    <div className="App">
      {responseGreeting && <h1>{responseGreeting}</h1>}
        <form onSubmit={handleSubmit}>
            <label htmlFor="name">Your name</label>
            <input id="name" name="name" type="text" />
            <button>Submit</button>
        </form>
    </div>
    );
}

export default App;
