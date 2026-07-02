import { useState } from "react";
import { useNavigate, useLocation, Link } from "react-router-dom";
import { useAuth } from "../AuthContext";
import { GoogleLogin } from '@react-oauth/google';
import { API_URL } from "../../config";

function LoginPage() {
    const navigate = useNavigate();
    const location = useLocation();
    const { login } = useAuth();

    const from = location.state?.from?.pathname || "/";

    // --- STATE ĐĂNG NHẬP ---
    const [form, setForm] = useState({
        userName: "",
        password: "",
        rememberMe: true,
    });
    const [loading, setLoading] = useState(false);

    // --- STATE QUÊN MẬT KHẨU ---
    const [view, setView] = useState("login"); // 'login' hoặc 'forgot'
    const [forgotEmail, setForgotEmail] = useState("");
    const [forgotStatus, setForgotStatus] = useState({ loading: false, message: "", isError: false });

    // --------------------------------------------------------
    // CÁC HÀM XỬ LÝ ĐĂNG NHẬP (GIỮ NGUYÊN)
    // --------------------------------------------------------
    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setForm({
            ...form,
            [name]: type === "checkbox" ? checked : value,
        });
    };

    const handleLoginSuccess = async (response) => {
        if (response.ok) {
            try {
                const result = await response.json();
                if (result.success && result.data) {
                    const userData = result.data;

                    login({
                        userId: userData.userId,
                        username: userData.userName,
                        email: userData.email,
                        role: userData.role
                    });

                    if (userData.role.includes("Admin") || userData.role.includes("ADMIN")) {
                        navigate("/admin");
                    } else {
                        navigate(from, { replace: true });
                    }
                } else {
                    alert(result.message || "Đăng nhập thất bại");
                }
            } catch (error) {
                console.error("Parse response error:", error);
                alert("Lỗi hệ thống");
            }
        } else {
            try {
                const errorResult = await response.json();
                alert("Đăng nhập thất bại: " + (errorResult.message || "Lỗi không xác định"));
            } catch {
                alert("Đăng nhập thất bại");
            }
        }
        setLoading(false);
    };

    const handleGoogleSuccess = async (credentialResponse) => {
        setLoading(true);
        try {
            const res = await fetch(
                `${API_URL}/Authenticate/login-google`,
                {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    credentials: "include",
                    body: JSON.stringify(credentialResponse.credential),
                }
            );
            await handleLoginSuccess(res);
        } catch (error) {
            console.error("Lỗi Google Login:", error);
            alert("Không thể kết nối tới Server");
            setLoading(false);
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);

        try {
            const res = await fetch(
                `${API_URL}/Authenticate/login-account`,
                {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    credentials: "include",
                    body: JSON.stringify(form),
                }
            );
            await handleLoginSuccess(res);
        } catch (error) {
            console.error("Lỗi kết nối Server:", error);
            alert("Không thể kết nối tới Server");
            setLoading(false);
        }
    };

    // --------------------------------------------------------
    // HÀM XỬ LÝ QUÊN MẬT KHẨU (THÊM MỚI)
    // --------------------------------------------------------
    const handleForgotSubmit = async (e) => {
        e.preventDefault();
        setForgotStatus({ loading: true, message: "", isError: false });

        try {
            const res = await fetch(`${API_URL}/authenticate/forgot-password`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ email: forgotEmail })
            });

            if (res.ok) {
                setForgotStatus({ 
                    loading: false, 
                    message: "Liên kết khôi phục mật khẩu đã được gửi! Vui lòng kiểm tra hộp thư (và hộp thư rác) của bạn.", 
                    isError: false 
                });
                setForgotEmail(""); // Xóa rỗng ô nhập sau khi gửi thành công
            } else {
                const data = await res.json().catch(() => ({}));
                setForgotStatus({ 
                    loading: false, 
                    message: data.message || "Không thể gửi yêu cầu. Vui lòng kiểm tra lại email.", 
                    isError: true 
                });
            }
        } catch (error) {
            setForgotStatus({ loading: false, message: "Lỗi kết nối tới máy chủ.", isError: true });
        }
    };

    return (
        <div className="container mt-5" style={{ maxWidth: 420 }}>
            <div className="card shadow border-0 p-4 bg-white rounded-4">
                
                {/* ----------------- GIAO DIỆN ĐĂNG NHẬP ----------------- */}
                {view === "login" ? (
                    <div className="animate__animated animate__fadeIn">
                        <h2 className="mb-4 text-center text-primary fw-bold">EngExam Login</h2>

                        <form onSubmit={handleSubmit}>
                            <div className="mb-3 text-start">
                                <label className="form-label fw-bold">Tên đăng nhập</label>
                                <input
                                    className="form-control"
                                    name="userName"
                                    placeholder="Nhập username"
                                    onChange={handleChange}
                                    disabled={loading}
                                    required
                                />
                            </div>

                            <div className="mb-3 text-start">
                                <div className="d-flex justify-content-between align-items-center mb-1">
                                    <label className="form-label fw-bold m-0">Mật khẩu</label>
                                    <button 
                                        type="button" 
                                        className="btn btn-link p-0 text-decoration-none small"
                                        onClick={() => setView("forgot")}
                                        disabled={loading}
                                    >
                                        Quên mật khẩu?
                                    </button>
                                </div>
                                <input
                                    type="password"
                                    className="form-control"
                                    name="password"
                                    placeholder="Nhập mật khẩu"
                                    onChange={handleChange}
                                    disabled={loading}
                                    required
                                />
                            </div>

                            <div className="form-check mb-3 text-start">
                                <input
                                    type="checkbox"
                                    className="form-check-input"
                                    name="rememberMe"
                                    id="rememberMe"
                                    checked={form.rememberMe}
                                    onChange={handleChange}
                                    disabled={loading}
                                />
                                <label className="form-check-label" htmlFor="rememberMe">
                                    Ghi nhớ đăng nhập
                                </label>
                            </div>

                            <button
                                className="btn btn-primary w-100 mb-3 py-2 fw-bold"
                                disabled={loading}
                            >
                                {loading ? (
                                    <><span className="spinner-border spinner-border-sm me-2"></span>Đang xử lý...</>
                                ) : (
                                    "ĐĂNG NHẬP"
                                )}
                            </button>
                        </form>

                        <div className="position-relative my-4">
                            <hr />
                            <span className="position-absolute top-50 start-50 translate-middle bg-white px-2 text-muted small">
                                hoặc đăng nhập bằng
                            </span>
                        </div>

                        <div className="d-flex justify-content-center mb-3">
                            <GoogleLogin
                                onSuccess={handleGoogleSuccess}
                                onError={() => console.log('Google Login Failed')}
                                useOneTap
                                theme="outline"
                                width="300"
                            />
                        </div>

                        <div className="text-center">
                            <span className="text-muted">Chưa có tài khoản? </span>
                            <Link to="/register" className="text-decoration-none fw-bold">
                                Đăng ký ngay
                            </Link>
                        </div>
                    </div>

                ) : (
                    
                /* ----------------- GIAO DIỆN QUÊN MẬT KHẨU ----------------- */
                    <div className="animate__animated animate__fadeIn">
                        <div className="text-center mb-4">
                            <div className="bg-primary bg-opacity-10 text-primary rounded-circle d-inline-flex align-items-center justify-content-center mb-3" style={{ width: '60px', height: '60px' }}>
                                <i className="fa-solid fa-envelope-open-text fs-3"></i>
                            </div>
                            <h3 className="fw-bold text-dark">Khôi phục mật khẩu</h3>
                            <p className="text-muted small">Nhập email của bạn và chúng tôi sẽ gửi liên kết để đặt lại mật khẩu.</p>
                        </div>

                        {forgotStatus.message && (
                            <div className={`alert ${forgotStatus.isError ? 'alert-danger' : 'alert-success'} py-2 small border-0 shadow-sm rounded-3`}>
                                <i className={`fa-solid ${forgotStatus.isError ? 'fa-circle-exclamation' : 'fa-circle-check'} me-2`}></i>
                                {forgotStatus.message}
                            </div>
                        )}

                        <form onSubmit={handleForgotSubmit}>
                            <div className="mb-4 text-start">
                                <label className="form-label fw-bold small">Địa chỉ Email</label>
                                <input
                                    type="email"
                                    className="form-control"
                                    placeholder="Nhập email của bạn"
                                    value={forgotEmail}
                                    onChange={(e) => setForgotEmail(e.target.value)}
                                    disabled={forgotStatus.loading}
                                    required
                                />
                            </div>

                            <button
                                type="submit"
                                className="btn btn-primary w-100 mb-3 py-2 fw-bold"
                                disabled={forgotStatus.loading}
                            >
                                {forgotStatus.loading ? (
                                    <><span className="spinner-border spinner-border-sm me-2"></span>Đang gửi...</>
                                ) : (
                                    "Gửi Liên Kết"
                                )}
                            </button>
                            
                            <button
                                type="button"
                                className="btn btn-light w-100 py-2 fw-bold text-muted border"
                                onClick={() => {
                                    setView("login");
                                    setForgotStatus({ loading: false, message: "", isError: false }); // Xóa lỗi khi quay lại
                                }}
                                disabled={forgotStatus.loading}
                            >
                                <i className="fa-solid fa-arrow-left me-2"></i> Quay lại Đăng nhập
                            </button>
                        </form>
                    </div>
                )}
            </div>
            
            <style>{`
                .animate__animated { animation-duration: 0.4s; }
                .animate__fadeIn { animation-name: fadeIn; }
                @keyframes fadeIn { from { opacity: 0; transform: translateY(-10px); } to { opacity: 1; transform: translateY(0); } }
            `}</style>
        </div>
    );
}

export default LoginPage;