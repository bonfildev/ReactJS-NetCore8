import { useEffect, useState } from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
function App() {
    const [tasks, setTasks] = useState([]);
    //7.- Crear useState descripcion
    const [descripcion, setDescripcion] = useState("");

    //2.- Metodo Obtener
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
    //3.- Metodo convertir fecha
    const formatDate = (string) => {
        let options = { year: 'numeric', month: 'long', day: 'numeric' };
        let fecha = new Date(string).toLocaleDateString("es-PE", options);
        let hora = new Date(string).toLocaleTimeString();
        return fecha + " | " + hora
    }

    //4.- Insertar datos
    useEffect(() => {
        showTasks();
    }, []);

    //8.- Guardar NOTA
    const guardarTarea = async (e) => {
        console.log("el log es " + descripcion);
        e.preventDefault()

        const response = await fetch("api/task/guardar", {
            method: "PUT",
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify({ descripcion: descripcion })
        })

        if (response.ok) {
            setDescripcion("");
            await showTasks();
        }
    }
    //10 Cerrar Tarea
    const cerrarTarea = async (id) => {

        const response = await fetch("api/task/Cerrar/" + id, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
        })

        if (response.ok)
            await showTasks();
    }

    function buttonCerrar(id, finished) { 
        if (finished  == false) { 
            return <button type="button" className="btn btn-sm btn-outline-danger"
                        /*11.- Cerrar Tarea*/
                        onClick={() => cerrarTarea(id)}>
                        Cerrar
                    </button>;
        }
        else "";
    }

    return (
        /*Lista*/ 
        <div className="container bg-dark p-4 vh-100">
            <h2 className="text-white">Lista de tareas</h2>
            <div className="row">
                <div className="col-sm-12">
                    <form onSubmit={guardarTarea}>

                        <div className="input-group"> 
                            <input type="text" className="form-control"
                                placeholder="Ingrese descripcion"
                                value={descripcion}
                                onChange={(e) => setDescripcion(e.target.value)} />

                            <button className="btn btn-success">Agregar</button>
                        </div>

                    </form>
                </div>
            </div>

            <div className="row mt-4">
                <div className="col-sm-12"></div>
                <div className="list-group">
                    {
                        tasks.map(
                            (item) => (
                                <div key={item.idtask}  className="list-group-item list-group-item-action">
                                    <h5 className="text-primary">{item.description}</h5>
                                    <div className="d-flex justify-content-between">
                                        <small className="text-muted">{formatDate(item.registerDate)}</small>
                                        {buttonCerrar(item.idtask, item.finished)}
                                    </div>
                                </div>
                            )
                        )
                    }
                </div>
            </div>
        </div>
    ); 
}

export default App;