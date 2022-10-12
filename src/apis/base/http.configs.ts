import axios from "axios";

let config = {
    baseURL: "https://widemapp-api.constack.co/",
    timeout: 5000,
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    }
};

const http = axios.create(config);

export default http;