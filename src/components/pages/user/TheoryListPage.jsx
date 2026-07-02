import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { API_URL } from "../../../config";

const TheoryListPage = () => {
    const [courses, setCourses] = useState([]);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        fetch(`${API_URL}/course?pageIndex=1&pageSize=10`)
            .then(res => res.json())
            .then(data => {
                // API trả về mảng nằm trong property 'items'
                setCourses(data.items || []);
                setLoading(false);
            })
            .catch(err => {
                console.error("Lỗi lấy danh sách bài học:", err);
                setLoading(false);
            });
    }, []);

    if (loading) return (
        <div className="text-center mt-5 pt-5">
            <div className="spinner-border text-primary"></div>
            <p className="mt-2 text-muted">Đang tải bài học...</p>
        </div>
    );

    return (
        <div className="container mt-5 pt-4 pb-5">
            <div className="text-center mb-5">
                <h2 className="fw-bold text-primary display-6">Thư Viện Lý Thuyết</h2>
                <p className="text-muted">Tổng hợp kiến thức trọng tâm cho kỳ thi EngExam</p>
            </div>

            <div className="row g-4">
                {courses.map(course => (
                    <div className="col-md-6 col-lg-4" key={course.id}>
                        <div className="card h-100 shadow-sm border-0 rounded-4 overflow-hidden card-hover" 
                             style={{ transition: '0.3s' }}>
                            <div style={{ height: '200px' }}>
                                <img 
                                    // 🔥 SỬ DỤNG TRỰC TIẾP URL TỪ BACKEND TRẢ VỀ HOẶC ẢNH MẶC ĐỊNH
                                    src={course.imageUrl || "https://placehold.co/400x250?text=No+Image"} 
                                    className="w-100 h-100 object-fit-cover" 
                                    alt={course.name}
                                    // Xử lý lỗi load ảnh (ví dụ: link ảnh bị die)
                                    onError={(e) => {
                                        e.target.onerror = null;
                                        e.target.src = "https://placehold.co/400x250?text=Image+Error";
                                    }}
                                />
                            </div>
                            <div className="card-body d-flex flex-column">
                                <h5 className="card-title fw-bold text-dark mb-2">{course.name}</h5>
                                <p className="card-text text-muted small flex-grow-1 mb-3">
                                    {course.description}
                                </p>
                                <button 
                                    className="btn btn-primary w-100 rounded-pill fw-bold"
                                    onClick={() => navigate(`/theory/${course.id}`)}
                                >
                                    Xem chi tiết <i className="bi bi-book-half ms-1"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                ))}
            </div>

            <style>{`
                .card-hover:hover {
                    transform: translateY(-5px);
                    box-shadow: 0 10px 20px rgba(0,0,0,0.1) !important;
                }
            `}</style>
        </div>
    );
};

export default TheoryListPage;