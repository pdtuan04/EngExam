import { NavLink, useNavigate } from "react-router-dom";
import { useAuth } from "../AuthContext";

function Header() {
    const { isAuth, logout } = useAuth();
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate("/login");
    };

    return (
        <nav className="navbar navbar-expand-md navbar-dark bg-dark fixed-top">
            <div className="container-fluid">
                <NavLink className="navbar-brand" to="/">
                    EngExam
                </NavLink>
                <button
                    className="navbar-toggler"
                    type="button"
                    data-bs-toggle="collapse"
                    data-bs-target="#mainNavbar"
                >
                    <span className="navbar-toggler-icon"></span>
                </button>

                {/* 🔥 COLLAPSE PHẢI CÓ ID */}
                <div className="collapse navbar-collapse" id="mainNavbar">
                    {/* LEFT */}
                    <ul className="navbar-nav me-auto">
                        <li className="nav-item">
                            <NavLink to="/" className="nav-link">
                                Trang chủ
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink to="/exam-categories" className="nav-link">
                                Thi Thử
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink to="/topic-practice" className="nav-link">
                                Luyện Tập
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink to="/flashcards" className="nav-link">
                                Flashcards
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink to="/theory" className="nav-link">
                                Lý thuyết
                            </NavLink>
                        </li>
                    </ul>

                    {/* RIGHT */}
                    <ul className="navbar-nav ms-auto">
                        {!isAuth ? (
                            <>
                                <li className="nav-item">
                                    <NavLink to="/login" className="nav-link">
                                        Đăng nhập
                                    </NavLink>
                                </li>
                                <li className="nav-item">
                                    <NavLink to="/register" className="nav-link">
                                        Đăng ký
                                    </NavLink>
                                </li>
                            </>
                        ) : (
                            <li className="nav-item dropdown">
                                <a
                                    className="nav-link dropdown-toggle"
                                    href="#"
                                    data-bs-toggle="dropdown"
                                >
                                    <img
                                        src="https://ui-avatars.com/?name=User"
                                        className="rounded-circle"
                                        width="20"
                                        height="20"
                                    />
                                </a>
                                <ul className="dropdown-menu dropdown-menu-end">
                                    <li>
                                        <button className="dropdown-item">
                                            Tài khoản
                                        </button>
                                    </li>
                                    <li>
                                        <button 
                                            className="dropdown-item text-primary" 
                                            onClick={() => navigate("/exam-history")}
                                        >
                                            <i className="bi bi-clock-history me-2"></i> Lịch sử thi
                                        </button>
                                    </li>
                                    <li>
                                        <button
                                            className="dropdown-item"
                                            onClick={handleLogout}
                                        >
                                            Đăng xuất
                                        </button>
                                    </li>
                                </ul>
                            </li>
                        )}
                    </ul>
                </div>
            </div>
        </nav>
    );
}

export default Header;
    