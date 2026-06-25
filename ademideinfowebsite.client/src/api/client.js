import axios from "axios";
 

export const apiClient = axios.create({
    baseURL: import.meta.env.VITE_API_BASE_URL ?? "http://localhost:3000/api", // Replace with your API base URL
    headers: {
        "Content-Type": "application/json",
    },
}); 

apiClient.interceptors.request.use((config) => { 
    const token = localStorage.getItem("AUTH_TOKEN_KEY");
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

apiClient.interceptors.response.use((response) => {
        return response;
},
    (error) => {
        if (error.response?.status === 401) {
            localStorage.removeItem("AUTH_TOKEN_KEY");
            window.location.href = '/login';
        }
        return Promise.reject(error);
    }
);

//unwrap the data from the response object
export const unwrapResponse = (response) =>
{
    if (response && response.data) {
        return response.data;
    }
    return response;
}