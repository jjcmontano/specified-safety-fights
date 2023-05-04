import axios from "axios";

const AxiosInstance = axios.create({
    baseURL: 'https://localhost:7101/api/',
    timeout: 10000,
});

export default AxiosInstance;