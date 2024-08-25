import { useEffect, useState } from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
function App() {
    const [tasks, setTasks] = useState([]);

    const showTasks = async () => {
        const response = await fetch("api/task/lista");
        if (response.ok) {
            const data = await response.json();
            setTasks(data);
        }
        else {
            console.log("status code" + response.status)
        }
    }

    useEffect(() => {
        showTasks();
    }, []);
    
    return (
        <div>
            <h1 id="tableLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    ); 


}

export default App;