import React from "react";
import { useEffect } from "react";

const API_URL = 'https://localhost:7224/api'

const App = () => {

    const searchCars = async (title) => {
        const response = await fetch(`${API_URL}/car?model=${title}`);
        const data = await response.json();

        console.log(data);
    } 

    useEffect(() => {
        searchCars('S');
    }, []);

    return (
        <h1>App</h1>
    );
}

export default App;