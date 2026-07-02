import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { API_URL } from "../../../config";
const ExamResultDetailPage = () => {
    const { resultId } = useParams();
    const navigate = useNavigate();
    const [detail, setDetail] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchDetail = async () => {
            try {
                const response = await fetch(`${API_URL}/examresult/details/${resultId}`, {
                    credentials: "include"
                });
                
                if (response.status === 401) {
                    navigate('/login');
                    return;
                }

                const result = await response.json();
                if (result.success) {
                    setDetail(result.data);
                }
            } catch (err) {
                console.error(err);
            } finally {
                setLoading(false);
            }
        };

        fetchDetail();
    }, [resultId, navigate]);

    if (loading) return <div className="text-center mt-5 pt-5"><div className="spinner-border text-primary"></div></div>;
    if (!detail) return <div className="container mt-5 pt-5 text-center text-danger"><h4>Không tìm thấy kết quả</h4></div>;

    return (
        <div className="container mt-5 pt-4 mb-5">
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h2 className="fw-bold">Chi tiết bài làm</h2>
                <button className="btn btn-secondary" onClick={() => navigate('/exam-history')}>Quay lại Lịch sử</button>
            </div>

            <div className="alert alert-info shadow-sm mb-4">
                <h5 className="mb-2">Ngày nộp bài: <strong>{new Date(detail.completeAt).toLocaleString('vi-VN')}</strong></h5>
                <h4 className="mb-0 text-success fw-bold">Tổng điểm: {detail.totalScore}</h4>
            </div>

            {detail.userAnswers?.map((ans, index) => (
                <div key={index} className={`card shadow-sm mb-4 border-${ans.isCorrect ? 'success' : 'danger'}`}>
                    <div className="card-body">
                        <h5 className="fw-bold mb-3">Câu {index + 1}: {ans.content}</h5>
                        
                        <div className="mb-3 ps-3">
                            {/* DẠNG TRẮC NGHIỆM */}
                            {ans.questionTypes === 0 && ans.options.map((opt, oIndex) => {
                                // Xác định màu sắc hiển thị
                                let boxClass = "border p-2 mb-2 rounded ";
                                if (opt.content === ans.userAnswer) {
                                    boxClass += ans.isCorrect ? "bg-success text-white border-success" : "bg-danger text-white border-danger";
                                } else if (opt.isCorrect) {
                                    boxClass += "bg-success text-white bg-opacity-75 border-success"; // Hiện đáp án đúng bị bỏ lỡ
                                }

                                return (
                                    <div key={oIndex} className={boxClass}>
                                        {opt.content === ans.userAnswer && <i className="bi bi-check2-circle me-2"></i>}
                                        {opt.content}
                                    </div>
                                );
                            })}

                            {/* DẠNG ĐIỀN TỪ */}
                            {ans.questionTypes === 1 && (
                                <div>
                                    <p className="mb-1">Câu trả lời của bạn: 
                                        <span className={`fw-bold ms-2 ${ans.isCorrect ? 'text-success' : 'text-danger'}`}>
                                            {ans.userAnswer}
                                        </span>
                                    </p>
                                    {!ans.isCorrect && (
                                        <p className="mb-1 text-success">
                                            Đáp án đúng: <span className="fw-bold">{ans.options.find(o => o.isCorrect)?.content}</span>
                                        </p>
                                    )}
                                </div>
                            )}
                        </div>

                        <hr />
                        <div className="bg-light p-3 rounded text-muted">
                            <strong><i className="bi bi-lightbulb text-warning me-2"></i>Giải thích: </strong> 
                            {ans.explanation || 'Không có giải thích chi tiết.'}
                        </div>
                    </div>
                </div>
            ))}
        </div>
    );
};

export default ExamResultDetailPage;