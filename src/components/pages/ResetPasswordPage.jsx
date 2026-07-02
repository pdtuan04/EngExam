import React, { useState } from 'react';
import { useSearchParams, useNavigate, Link } from 'react-router-dom';
import { API_URL } from "../../config"; // Chỉnh lại đường dẫn config cho đúng

const ResetPasswordPage = () => {
    const [searchParams] = useSearchParams();
    const navigate = useNavigate();
    
    // Tự động lấy email và token từ thanh URL
    const emailUrl = searchParams.get('email');
    const tokenUrl = searchParams.get('token');

    const [newPassword, setNewPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [status, setStatus] = useState({ loading: false, error: null, success: false });

    // Rào chắn bảo vệ: Nếu người dùng tự gõ /reset-password mà không có token
    if (!emailUrl || !tokenUrl) {
        return (
            <div className="container mt-5 pt-5 text-center">
                <div className="alert alert-danger d-inline-block px-5 shadow-sm">
                    <h4 className="alert-heading"><i className="fa-solid fa-triangle-exclamation me-2"></i>Liên kết không hợp lệ</h4>
                    <p className="mb-0">Link khôi phục mật khẩu bị thiếu hoặc đã hỏng. Vui lòng yêu cầu cấp lại link mới từ trang Đăng nhập.</p>
                </div>
                <div className="mt-4">
                    <Link to="/login" className="btn btn-primary rounded-pill px-4 fw-bold shadow-sm">Về trang Đăng nhập</Link>
                </div>
            </div>
        );
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        
        if (newPassword !== confirmPassword) {
            setStatus({ ...status, error: "Mật khẩu xác nhận không khớp!" });
            return;
        }

        if (newPassword.length < 6) {
            setStatus({ ...status, error: "Mật khẩu phải có ít nhất 6 ký tự!" });
            return;
        }

        setStatus({ loading: true, error: null, success: false });

        try {
            const res = await fetch(`${API_URL}/authenticate/reset-password`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                
                body: JSON.stringify({
                    email: emailUrl,
                    resetCode: tokenUrl,
                    newPassword: newPassword
                })
            });

            if (res.ok) {
                setStatus({ loading: false, error: null, success: true });
                // Hiển thị thành công 2 giây rồi tự đá sang trang Login
                setTimeout(() => navigate('/login'), 2000);
            } else {
                const data = await res.json();
                setStatus({ loading: false, error: data.message || "Khôi phục mật khẩu thất bại. Token có thể đã hết hạn.", success: false });
            }
        } catch (err) {
            setStatus({ loading: false, error: "Lỗi kết nối tới máy chủ.", success: false });
        }
    };

    return (
        <div className="container mt-5 pt-5">
            <div className="row justify-content-center">
                <div className="col-md-6 col-lg-5">
                    <div className="card shadow-lg border-0 rounded-4">
                        <div className="card-body p-5">
                            <div className="text-center mb-4">
                                <div className="bg-primary bg-opacity-10 text-primary rounded-circle d-inline-flex align-items-center justify-content-center mb-3" style={{ width: '60px', height: '60px' }}>
                                    <i className="fa-solid fa-unlock-keyhole fs-3"></i>
                                </div>
                                <h3 className="fw-bold text-dark">Đặt Lại Mật Khẩu</h3>
                                <p className="text-muted small">Tạo mật khẩu mới cho tài khoản <strong className="text-primary">{emailUrl}</strong></p>
                            </div>

                            {status.success ? (
                                <div className="alert alert-success text-center border-0 shadow-sm rounded-3 animation-fade-in">
                                    <i className="fa-solid fa-circle-check fs-2 text-success mb-2 d-block"></i>
                                    <strong>Thành công!</strong> Đổi mật khẩu hoàn tất.<br/>
                                    Đang tự động chuyển hướng đến trang Đăng nhập...
                                </div>
                            ) : (
                                <form onSubmit={handleSubmit}>
                                    {status.error && (
                                        <div className="alert alert-danger py-2 small border-0 shadow-sm rounded-3">
                                            <i className="fa-solid fa-circle-exclamation me-2"></i>{status.error}
                                        </div>
                                    )}

                                    <div className="mb-3">
                                        <label className="form-label fw-semibold text-dark small">Mật khẩu mới</label>
                                        <div className="input-group input-group-merge rounded-3 shadow-sm border">
                                            <span className="input-group-text bg-white border-0"><i className="fa-solid fa-lock text-muted"></i></span>
                                            <input 
                                                type="password" 
                                                className="form-control border-0 ps-0" 
                                                placeholder="Nhập mật khẩu mới"
                                                value={newPassword}
                                                onChange={(e) => setNewPassword(e.target.value)}
                                                required
                                            />
                                        </div>
                                    </div>

                                    <div className="mb-4">
                                        <label className="form-label fw-semibold text-dark small">Xác nhận mật khẩu</label>
                                        <div className="input-group input-group-merge rounded-3 shadow-sm border">
                                            <span className="input-group-text bg-white border-0"><i className="fa-solid fa-lock text-muted"></i></span>
                                            <input 
                                                type="password" 
                                                className="form-control border-0 ps-0" 
                                                placeholder="Nhập lại mật khẩu mới"
                                                value={confirmPassword}
                                                onChange={(e) => setConfirmPassword(e.target.value)}
                                                required
                                            />
                                        </div>
                                    </div>

                                    <button 
                                        type="submit" 
                                        className="btn btn-primary w-100 fw-bold rounded-pill shadow-sm py-2" 
                                        disabled={status.loading}
                                    >
                                        {status.loading ? <span className="spinner-border spinner-border-sm me-2"></span> : <i className="fa-solid fa-floppy-disk me-2"></i>}
                                        Lưu Mật Khẩu Mới
                                    </button>
                                </form>
                            )}
                        </div>
                    </div>
                </div>
            </div>

            <style>{`
                .input-group-merge:focus-within { border-color: #0d6efd !important; box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.15) !important; }
                .input-group-merge input:focus { box-shadow: none; border-color: transparent; }
                .animation-fade-in { animation: fadeIn 0.4s ease-in-out; }
                @keyframes fadeIn { from { opacity: 0; transform: translateY(-10px); } to { opacity: 1; transform: translateY(0); } }
            `}</style>
        </div>
    );
};

export default ResetPasswordPage;