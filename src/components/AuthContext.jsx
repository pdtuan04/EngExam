import { createContext, useContext, useState } from "react";
import { API_URL } from "../config";
const AuthContext = createContext();

export function AuthProvider({ children }) {
    const [user, setUser] = useState(() => {
        const savedUser = localStorage.getItem("user");
        return savedUser ? JSON.parse(savedUser) : null;
    });
    const login = (userData) => {
        setUser(userData);
        localStorage.setItem("user", JSON.stringify(userData));
    };
    const logout = () => {
        setUser(null);
        localStorage.clear();
        fetch(`${API_URL}/Authenticate/logout`, {
            method: "POST",
            credentials: "include",
        }).catch(err => console.log("Lỗi đăng xuất:", err));
    };
    const isAuth = user !== null;
    const isAdmin = user !== null && user.role && user.role.includes("Admin"); 

    return (
        <AuthContext.Provider value={{ user, login, logout, isAuth, isAdmin }}>
            {children}
        </AuthContext.Provider>
    );
}
export const useAuth = () => useContext(AuthContext);