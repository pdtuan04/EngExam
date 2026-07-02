import React, { useState, useEffect, useRef } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { API_URL } from "../../config";

const DoExamPage = () => {
    const { examId } = useParams();
    const navigate = useNavigate();

    const [examData, setExamData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [submitting, setSubmitting] = useState(false);
    const [error, setError] = useState(null);

    // State lưu đáp án của người dùng: { questionId: { answerId: "...", answerFillInBlank: "..." } }
    const [userAnswers, setUserAnswers] = useState({});
    // Sử dụng useRef để lưu bản sao mới nhất của userAnswers, giúp hàm auto-submit lấy đúng dữ liệu
    const answersRef = useRef(userAnswers);

    // State thời gian (tính bằng giây)
    const [timeLeft, setTimeLeft] = useState(null);

    // 1. Cập nhật ref mỗi khi userAnswers thay đổi
    useEffect(() => {
        answersRef.current = userAnswers;
    }, [userAnswers]);

    // 2. Gọi API lấy đề thi
    useEffect(() => {
        const fetchExam = async () => {
            try {
                const response = await fetch(`${API_URL}/Exam/do-exam/${examId}`, {
                    method: "GET",
                    credentials: "include",
                    headers: { "Content-Type": "application/json" }
                });

                if (response.status === 401) {
                    navigate("/login");
                    return;
                }

                const json = await response.json();
                if (json.success) {
                    setExamData(json.data);
                    // Đổi phút ra giây để đếm ngược
                    setTimeLeft(json.data.durationInMinutes * 60);
                } else {
                    setError("Không thể tải đề thi.");
                }
            } catch (err) {
                setError("Lỗi kết nối tới máy chủ.");
            } finally {
                setLoading(false);
            }
        };

        fetchExam();
    }, [examId, navigate]);

    // 3. Xử lý đồng hồ đếm ngược
    useEffect(() => {
        // Nếu chưa tải xong hoặc đã hết giờ thì không chạy interval
        if (timeLeft === null || timeLeft <= 0 || submitting) return;

        const timer = setInterval(() => {
            setTimeLeft((prev) => {
                if (prev <= 1) {
                    clearInterval(timer);
                    return 0;
                }
                return prev - 1;
            });
        }, 1000);

        return () => clearInterval(timer);
    }, [timeLeft, submitting]);

    // 4. Lắng nghe sự kiện hết giờ để tự động nộp bài
    useEffect(() => {
        if (timeLeft === 0 && !submitting) {
            alert("Đã hết thời gian làm bài! Hệ thống sẽ tự động nộp bài.");
            handleSubmit();
        }
    }, [timeLeft]);

    // 5. Hàm xử lý khi chọn đáp án trắc nghiệm hoặc điền từ
    const handleAnswerChange = (questionId, answerId, textValue = "") => {
        setUserAnswers(prev => ({
            ...prev,
            [questionId]: {
                questionId: questionId,
                answerId: answerId || null, // null nếu là câu điền từ
                answerFillInBlank: textValue
            }
        }));
    };

    // 6. Hàm Nộp bài (Submit)
    const handleSubmit = async (e) => {
        if (e) e.preventDefault();
        
        if (timeLeft > 0 && e) {
            const confirmSubmit = window.confirm("Bạn có chắc chắn muốn nộp bài sớm không?");
            if (!confirmSubmit) return;
        }

        setSubmitting(true);

        // Map object answersRef.current ra thành mảng theo đúng format API yêu cầu
        const formattedAnswers = Object.values(answersRef.current).map(ans => ({
            questionId: ans.questionId,
            answerId: ans.answerId || "00000000-0000-0000-0000-000000000000", // Gửi Guid rỗng nếu không có answerId
            answerFillInBlank: ans.answerFillInBlank || ""
        }));

        const payload = {
            examId: examId,
            userAnswers: formattedAnswers
        };

        try {
            const response = await fetch(`${API_URL}/Exam/submit-exam`, {
                method: "POST",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(payload)
            });

            if (response.ok) {
                // Giả sử API trả về kết quả hoặc ID kết quả, bạn điều hướng sang trang xem kết quả
                // const result = await response.json();
                alert("Nộp bài thành công!");
                navigate("/exam-history"); // Điều hướng về lịch sử thi hoặc trang kết quả chi tiết
            } else {
                alert("Có lỗi xảy ra khi nộp bài.");
                setSubmitting(false);
            }
        } catch (error) {
            console.error("Lỗi nộp bài:", error);
            alert("Lỗi kết nối khi nộp bài.");
            setSubmitting(false);
        }
    };

    // Format giây thành dạng MM:SS
    const formatTime = (seconds) => {
        if (seconds === null) return "00:00";
        const m = Math.floor(seconds / 60).toString().padStart(2, '0');
        const s = (seconds % 60).toString().padStart(2, '0');
        return `${m}:${s}`;
    };

    if (loading) return <div className="text-center mt-5 pt-5"><div className="spinner-border text-primary"></div><p>Đang tải đề thi...</p></div>;
    if (error) return <div className="container mt-5 pt-5 text-center alert alert-danger">{error}</div>;
    if (!examData) return null;

    return (
        <div className="container mt-5 pt-4 mb-5">
            {/* Header Đề thi & Đồng hồ (Sticky để luôn nổi trên cùng khi cuộn) */}
            <div className="sticky-top bg-white pt-3 pb-2 mb-4 border-bottom shadow-sm z-3" style={{ top: '56px' }}>
                <div className="d-flex justify-content-between align-items-center px-3">
                    <div>
                        <h3 className="fw-bold m-0 text-primary">{examData.title}</h3>
                        <p className="text-muted small m-0">{examData.description}</p>
                    </div>
                    <div className={`text-center px-4 py-2 rounded-3 border ${timeLeft <= 60 ? 'bg-danger text-white border-danger animate__animated animate__pulse animate__infinite' : 'bg-light border-secondary'}`}>
                        <div className="small fw-bold text-uppercase opacity-75">Thời gian còn lại</div>
                        <div className="fs-3 fw-bold font-monospace">{formatTime(timeLeft)}</div>
                    </div>
                </div>
            </div>

            {/* Danh sách câu hỏi */}
            <div className="row justify-content-center">
                <div className="col-lg-9">
                    {examData.questions?.map((q, index) => (
                        <div className="card shadow-sm border-0 rounded-4 mb-4" key={q.id}>
                            <div className="card-body p-4">
                                <h5 className="fw-bold mb-3">
                                    <span className="badge bg-primary me-2">Câu {index + 1}</span> 
                                    {q.content}
                                </h5>

                                {/* Render Đáp án tùy theo questionTypes (0: Trắc nghiệm, 1: Điền từ...) */}
                                {q.questionTypes === 0 ? (
                                    <div className="d-flex flex-column gap-2 mt-3 ps-3">
                                        {q.answers?.map(ans => (
                                            <div className="form-check form-check-custom" key={ans.id}>
                                                <input 
                                                    className="form-check-input" 
                                                    type="radio" 
                                                    name={`question_${q.id}`} 
                                                    id={`answer_${ans.id}`} 
                                                    value={ans.id}
                                                    onChange={() => handleAnswerChange(q.id, ans.id, "")}
                                                    checked={userAnswers[q.id]?.answerId === ans.id}
                                                    disabled={submitting || timeLeft === 0}
                                                />
                                                <label className="form-check-label fs-5 ms-2 w-100" htmlFor={`answer_${ans.id}`} style={{ cursor: "pointer" }}>
                                                    {ans.content}
                                                </label>
                                            </div>
                                        ))}
                                    </div>
                                ) : (
                                    // Trường hợp câu hỏi điền khuyết
                                    <div className="mt-3 ps-3">
                                        <input 
                                            type="text" 
                                            className="form-control" 
                                            placeholder="Nhập câu trả lời của bạn..."
                                            value={userAnswers[q.id]?.answerFillInBlank || ""}
                                            onChange={(e) => handleAnswerChange(q.id, null, e.target.value)}
                                            disabled={submitting || timeLeft === 0}
                                        />
                                    </div>
                                )}
                            </div>
                        </div>
                    ))}

                    <div className="text-center mt-5 mb-5">
                        <button 
                            className="btn btn-primary btn-lg px-5 rounded-pill shadow-sm fw-bold" 
                            onClick={handleSubmit}
                            disabled={submitting || timeLeft === 0}
                        >
                            {submitting ? (
                                <><span className="spinner-border spinner-border-sm me-2"></span> Đang nộp bài...</>
                            ) : (
                                <><i className="bi bi-send-check me-2"></i> Nộp Bài Ngay</>
                            )}
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default DoExamPage;