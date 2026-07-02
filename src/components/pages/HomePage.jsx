import { Link } from "react-router-dom";
import { useAuth } from "../AuthContext";

function HomePage() {
    const { isAuth, user } = useAuth();

    return (
        <div className="bg-light min-vh-100 pb-5">
            {/* HERO SECTION */}
            <div className="container pt-5 mt-4 text-center">
                <div className="row justify-content-center">
                    <div className="col-lg-8">
                        <h1 className="display-4 fw-bold text-primary mb-3">
                            Chinh Phục Tiếng Anh Cùng EngExam Ver 1.3
                        </h1>
                        <p className="lead text-muted mb-5">
                            Nền tảng luyện thi thông minh giúp bạn đánh giá năng lực, 
                            cải thiện ngữ pháp và từ vựng qua từng bài kiểm tra. 
                            Biết kết quả ngay lập tức kèm lời giải thích chi tiết!
                        </p>
                        
                        {isAuth ? (
                            <div>
                                <h5 className="mb-3">Chào mừng trở lại, <span className="text-success">{user?.userName}</span>!</h5>
                                <Link to="/exam-categories" className="btn btn-primary btn-lg px-5 py-3 shadow-sm rounded-pill fw-bold">
                                    Vào Phòng Thi Ngay 🚀
                                </Link>
                            </div>
                        ) : (
                            <div>
                                <Link to="/login" className="btn btn-primary btn-lg px-5 py-3 me-3 shadow-sm rounded-pill fw-bold">
                                    Đăng Nhập
                                </Link>
                                <Link to="/register" className="btn btn-outline-primary btn-lg px-5 py-3 shadow-sm rounded-pill fw-bold">
                                    Đăng Ký Miễn Phí
                                </Link>
                            </div>
                        )}
                    </div>
                </div>
            </div>

            {/* FEATURES SECTION */}
            <div className="container mt-5 pt-5">
                <div className="row g-4 text-center">
                    {/* Feature 1 */}
                    <div className="col-md-4">
                        <div className="card h-100 border-0 shadow-sm rounded-4 p-4 hover-effect">
                            <div className="card-body">
                                <div className="display-4 mb-3">📚</div>
                                <h4 className="fw-bold">Đề Thi Đa Dạng</h4>
                                <p className="text-muted">
                                    Hệ thống cung cấp hàng trăm đề thi từ cơ bản đến nâng cao, 
                                    bao gồm cả trắc nghiệm và điền từ.
                                </p>
                            </div>
                        </div>
                    </div>

                    {/* Feature 2 */}
                    <div className="col-md-4">
                        <div className="card h-100 border-0 shadow-sm rounded-4 p-4 hover-effect">
                            <div className="card-body">
                                <div className="display-4 mb-3">⚡</div>
                                <h4 className="fw-bold">Chấm Điểm Tức Thì</h4>
                                <p className="text-muted">
                                    Không cần chờ đợi. Ngay khi bấm nộp bài, bạn sẽ biết ngay tổng điểm 
                                    và câu nào mình làm sai.
                                </p>
                            </div>
                        </div>
                    </div>

                    {/* Feature 3 */}
                    <div className="col-md-4">
                        <div className="card h-100 border-0 shadow-sm rounded-4 p-4 hover-effect">
                            <div className="card-body">
                                <div className="display-4 mb-3">💡</div>
                                <h4 className="fw-bold">Giải Thích Chi Tiết</h4>
                                <p className="text-muted">
                                    Sai ở đâu, học ở đó. Mỗi câu hỏi đều kèm theo lời giải thích cặn kẽ 
                                    giúp bạn lấp đầy lỗ hổng kiến thức.
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            {/* CALL TO ACTION BOTTOM */}
            {!isAuth && (
                <div className="container mt-5 pt-5 text-center">
                    <div className="bg-white p-5 rounded-4 shadow-sm border-top border-primary border-4">
                        <h3 className="fw-bold mb-3">Bạn đã sẵn sàng để bắt đầu?</h3>
                        <p className="text-muted mb-4">Tạo tài khoản chỉ trong 1 phút và trải nghiệm toàn bộ tính năng.</p>
                        <Link to="/register" className="btn btn-success btn-lg px-5 rounded-pill">
                            Tạo Tài Khoản Ngay
                        </Link>
                    </div>
                </div>
            )}
            
            <style>{`
                .hover-effect {
                    transition: transform 0.3s ease, box-shadow 0.3s ease;
                }
                .hover-effect:hover {
                    transform: translateY(-5px);
                    box-shadow: 0 .5rem 1rem rgba(0,0,0,.15)!important;
                }
            `}</style>
        </div>
    );
}

export default HomePage;