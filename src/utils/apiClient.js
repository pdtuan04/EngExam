import { API_URL } from "../config";
const BASE_URL = `${API_URL}`; 

async function customFetch(endpoint, options = {}) {
    const url = `${BASE_URL}${endpoint}`;
    const defaultOptions = {
        headers: {
            "Content-Type": "application/json",
            ...options.headers,
        },
        credentials: "include",
        ...options,
    };
    if (defaultOptions.body && typeof defaultOptions.body === "object") {
        defaultOptions.body = JSON.stringify(defaultOptions.body);
    }

    try {
        const response = await fetch(url, defaultOptions);
        if (response.status === 401) {
            console.warn("Phiên đăng nhập hết hạn hoặc chưa đăng nhập!");
            localStorage.clear();
            window.location.href = "/login";
            return Promise.reject("Unauthorized"); 
        }
        const data = await response.json();

        if (!response.ok) {
            return Promise.reject(data);
        }

        return data;

    } catch (error) {
        console.error(`Lỗi gọi API [${endpoint}]:`, error);
        throw error;
    }
}
export const apiClient = {
    get: (endpoint) => customFetch(endpoint, { method: "GET" }),
    
    post: (endpoint, body) => customFetch(endpoint, { method: "POST", body }),
    
    put: (endpoint, body) => customFetch(endpoint, { method: "PUT", body }),
    
    delete: (endpoint) => customFetch(endpoint, { method: "DELETE" }),
};