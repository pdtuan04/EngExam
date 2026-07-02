import { Navigate, useLocation } from "react-router-dom";
import { useAuth } from "./AuthContext";

const AdminProtectedRoute = ({ children }) => {
    const { isAuth, isAdmin } = useAuth();
    const location = useLocation();
    if (!isAuth) {
        return <Navigate to="/login" state={{ from: location }} replace />;
    }

    if (!isAdmin) {
        return (
            <div className="container mt-5">
                <div className="alert alert-danger text-center">
                    <h4>⛔ Truy cập bị từ chối</h4>
                    <p>Bạn không có quyền truy cập trang quản trị.</p>
                    <a href="/" className="btn btn-primary">Về trang chủ</a>
                </div>
            </div>
        );
    }

    return children;
};

export default AdminProtectedRoute;