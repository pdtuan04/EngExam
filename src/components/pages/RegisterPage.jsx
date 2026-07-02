import { useState } from "react";
import { useNavigate, Link } from "react-router-dom"; // Dùng Link để chuyển trang mượt hơn
import { GoogleLogin } from '@react-oauth/google';
import { useAuth } from "../AuthContext";
import { API_URL } from "../../config";
function RegisterPage() {
    const navigate = useNavigate();
    const { login } = useAuth(); // Lấy hàm login để dùng cho Google

    const [form, setForm] = useState({
        userName: "",
        email: "",
        password: "",
        confirmPassword: "",
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setForm({ ...form, [name]: value });
    };

    // --- XỬ LÝ ĐĂNG KÝ TÀI KHOẢN THƯỜNG ---
    const handleSubmit = async (e) => {
        e.preventDefault();

        // 1. Kiểm tra mật khẩu khớp nhau
        if (form.password !== form.confirmPassword) {
            alert("Mật khẩu xác nhận không khớp!");
            return;
        }

        try {
            const res = await fetch(
                `${API_URL}/Authenticate/register-account`,
                {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    credentials: "include",
                    body: JSON.stringify({
                        userName: form.userName,
                        password: form.password,
                        confirmPassword: form.confirmPassword,
                        email: form.email
                    }),
                }
            );

            if (res.ok) {
                alert("Đăng ký thành công! Vui lòng đăng nhập.");
                navigate("/login"); // Tài khoản thường thì vẫn nên bắt đăng nhập lại 1 lần để bảo mật
            } else {
                const errorText = await res.text();
                alert("Đăng ký thất bại: " + errorText);
            }
        } catch (error) {
            console.error("Lỗi kết nối:", error);
            alert("Không thể kết nối Server");
        }
    };

    // --- XỬ LÝ GOOGLE (Đăng ký = Đăng nhập luôn) ---
    const handleGoogleSuccess = async (credentialResponse) => {
        try {
            // Gọi cùng 1 API với Login, vì Google Login có tính năng:
            // "Nếu chưa có tk -> Tự tạo -> Login"
            // "Nếu có tk rồi -> Login luôn"
            const res = await fetch(
                `${API_URL}/Authenticate/login-google`,
                {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    credentials: "include", // Quan trọng để nhận Cookie
                    body: JSON.stringify(credentialResponse.credential),
                }
            );

            if (res.ok) {
                login(); // Cập nhật trạng thái App là "Đã login"
                navigate("/"); // Vào thẳng trang chủ luôn
            } else {
                alert("Xác thực Google thất bại");
            }
        } catch (error) {
            console.error("Lỗi Google Auth:", error);
        }
    };

    return (
        <div className="container mt-5" style={{ maxWidth: 400 }}>
            <div className="card p-4 shadow border-0">
                <h2 className="text-center mb-4 text-primary fw-bold">Đăng Ký EngExam</h2>

                <form onSubmit={handleSubmit}>
                    <div className="mb-3">
                        <input
                            className="form-control"
                            name="userName"
                            placeholder="Tên đăng nhập"
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="mb-3">
                        <input
                            type="email"
                            className="form-control"
                            name="email"
                            placeholder="Email"
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="mb-3">
                        <input
                            type="password"
                            className="form-control"
                            name="password"
                            placeholder="Mật khẩu"
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="mb-3">
                        <input
                            type="password"
                            className="form-control"
                            name="confirmPassword"
                            placeholder="Nhập lại mật khẩu"
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <button className="btn btn-primary w-100 py-2 fw-bold">
                        ĐĂNG KÝ
                    </button>
                </form>

                {/* Phần Google & Chuyển trang */}
                <div className="position-relative my-4">
                    <hr />
                    <span className="position-absolute top-50 start-50 translate-middle bg-white px-2 text-muted small">
                        hoặc đăng ký bằng
                    </span>
                </div>

                <div className="d-flex justify-content-center mb-3">
                    <GoogleLogin
                        onSuccess={handleGoogleSuccess}
                        onError={() => console.log('Google Failed')}
                        useOneTap
                        theme="outline"
                        width="300"
                    />
                </div>

                <div className="text-center">
                    <span className="text-muted">Đã có tài khoản? </span>
                    <Link to="/login" className="text-decoration-none fw-bold">Đăng nhập ngay</Link>
                </div>
            </div>
        </div>
    );
}

export default RegisterPage;