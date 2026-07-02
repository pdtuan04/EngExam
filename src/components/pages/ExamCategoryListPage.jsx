import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { API_URL } from "../../config";

function ExamCategoryPage() {
    // 1. STATE QUẢN LÝ DỮ LIỆU
    const [categories, setCategories] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    // 2. GỌI API LẤY DỮ LIỆU
    useEffect(() => {
        const fetchExamCategories = async () => {
            try {
                const response = await fetch(`${API_URL}/ExamCategory`);

                if (!response.ok) {
                    throw new Error("Lỗi khi tải danh sách danh mục từ Server");
                }

                const json = await response.json();
                
                // Dựa theo JSON bạn đưa, dữ liệu mảng nằm trong json.data
                if (json.success) {
                    setCategories(json.data);
                }
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchExamCategories();
    }, []);
    const handleGoToExams = (categoryId) => {
        navigate(`/exam-categories/${categoryId}/exams`);
    };
    if (loading) return (
        <div className="text-center mt-5">
            <div className="spinner-border text-primary"></div>
            <p className="mt-2">Đang tải danh mục...</p>
        </div>
    );
    
    if (error) return <h4 className="text-danger text-center mt-5">{error}</h4>;

    return (
        <div className="container mt-5 pt-4 pb-5">
            <div className="text-center mb-5">
                <h2 className="fw-bold text-primary">Danh Mục Luyện Thi</h2>
                <p className="text-muted">Chọn một kỹ năng để bắt đầu bài kiểm tra của bạn</p>
            </div>

            <div className="row g-4">
                {categories.map((cat) => {
                    // Xử lý link ảnh: Nối tên miền Backend với đuôi ảnh trong Database
                    const imageUrl = cat.imageUrl
                        ? `${cat.imageUrl}` 
                        : "https://placehold.co/400x200/eeeeee/999999?text=No+Image";

                    return (
                        <div className="col-md-4" key={cat.id}>
                            <div
                                className="card h-100 shadow-sm border-0 rounded-4 overflow-hidden card-hover"
                                onClick={() => handleGoToExams(cat.id)}
                                style={{ cursor: "pointer", transition: "all 0.3s ease" }}
                            >
                                {/* PHẦN HIỂN THỊ ẢNH */}
                                <div style={{ height: "200px", backgroundColor: "#f8f9fa" }}>
                                    <img 
                                        src={imageUrl} 
                                        alt={cat.name} 
                                        className="w-100 h-100 object-fit-cover"
                                        onError={(e) => {
                                            // DÒNG QUAN TRỌNG NHẤT: Chặn vòng lặp vô tận
                                            e.target.onerror = null; 
                                            // Gắn ảnh mặc định nếu ảnh gốc bị lỗi 404
                                            e.target.src = "https://placehold.co/400x200/eeeeee/999999?text=Image+Not+Found";
                                        }}
                                    />
                                </div>

                                {/* PHẦN NỘI DUNG CHỮ */}
                                <div className="card-body d-flex flex-column">
                                    <h5 className="card-title fw-bold text-dark">{cat.name}</h5>
                                    <p className="card-text text-muted flex-grow-1">
                                        {cat.description}
                                    </p>
                                    
                                    <button className="btn btn-outline-primary w-100 rounded-pill fw-semibold mt-3">
                                        Xem Đề Thi <i className="bi bi-arrow-right ms-1"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                    );
                })}
            </div>

            {/* CSS CHO HIỆU ỨNG HOVER */}
            <style>{`
                .card-hover:hover {
                    transform: translateY(-8px);
                    box-shadow: 0 1rem 3rem rgba(0,0,0,.15) !important;
                }
                .card-hover img {
                    transition: transform 0.4s ease;
                }
                .card-hover:hover img {
                    transform: scale(1.05);
                }
            `}</style>
        </div>
    );
}

export default ExamCategoryPage;