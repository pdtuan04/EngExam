import React, { useState, useEffect } from 'react';
import { API_URL } from "../../../config";

const CreateExamPage = () => {
    // ---------------- STATE: DROPDOWNS LẤY TỪ API ----------------
    const [categories, setCategories] = useState([]);
    const [topics, setTopics] = useState([]);
    
    const [isLoadingData, setIsLoadingData] = useState(true);
    const [isSaving, setIsSaving] = useState(false);

    // ---------------- STATE: THÔNG TIN CHUNG BÀI THI ----------------
    const [examInfo, setExamInfo] = useState({
        title: "",
        durationInMinutes: 10,
        examCategoryId: "",
        description: ""
    });

    // ---------------- STATE: DANH SÁCH CÂU HỎI & ĐÁP ÁN ----------------
    // 🔥 ĐÃ BỎ TEMP_ID: State giờ đây sạch sẽ, y hệt JSON payload gửi lên Server
    const [questions, setQuestions] = useState([
        {
            content: "",
            score: 5.0,
            questionTypes: 1, 
            topicId: "",
            explanation: "",
            answers: [
                { content: "", isCorrect: true },
                { content: "", isCorrect: false },
                { content: "", isCorrect: false },
                { content: "", isCorrect: false }
            ]
        }
    ]);

    // ---------------- LẤY DỮ LIỆU DROPDOWN TỪ API ----------------
    useEffect(() => {
        const fetchDropdownData = async () => {
            try {
                const [catRes, topicRes] = await Promise.all([
                    fetch(`${API_URL}/ExamCategory`, { credentials: "include" }),
                    fetch(`${API_URL}/topic`, { credentials: "include" })
                ]);

                if (catRes.ok) {
                    const catJson = await catRes.json();
                    setCategories(catJson.data || []);
                }
                if (topicRes.ok) {
                    const topicJson = await topicRes.json();
                    setTopics(topicJson.data || []);
                }
            } catch (err) {
                console.error("Lỗi lấy dữ liệu Dropdown:", err);
            } finally {
                setIsLoadingData(false);
            }
        };

        fetchDropdownData();
    }, []);

    // ---------------- CÁC HÀM XỬ LÝ CÂU HỎI ----------------
    const handleAddQuestion = () => {
        setQuestions([
            ...questions,
            {
                content: "",
                score: 5.0,
                questionTypes: 1,
                topicId: "",
                explanation: "",
                answers: [
                    { content: "", isCorrect: true },
                    { content: "", isCorrect: false },
                ]
            }
        ]);
    };

    const handleRemoveQuestion = (qIndex) => {
        if (window.confirm("Bạn có chắc muốn xóa câu hỏi này?")) {
            setQuestions(questions.filter((_, index) => index !== qIndex));
        }
    };

    const handleQuestionChange = (qIndex, field, value) => {
        const newQs = [...questions];
        newQs[qIndex][field] = value;
        setQuestions(newQs);
    };

    // ---------------- CÁC HÀM XỬ LÝ ĐÁP ÁN ----------------
    const handleAddAnswer = (qIndex) => {
        const newQs = [...questions];
        newQs[qIndex].answers.push({ content: "", isCorrect: false });
        setQuestions(newQs);
    };

    const handleRemoveAnswer = (qIndex, aIndex) => {
        const newQs = [...questions];
        newQs[qIndex].answers = newQs[qIndex].answers.filter((_, index) => index !== aIndex);
        setQuestions(newQs);
    };

    const handleAnswerChange = (qIndex, aIndex, value) => {
        const newQs = [...questions];
        newQs[qIndex].answers[aIndex].content = value;
        setQuestions(newQs);
    };

    const handleSetCorrectAnswer = (qIndex, aIndex) => {
        const newQs = [...questions];
        newQs[qIndex].answers.forEach((ans, idx) => {
            ans.isCorrect = (idx === aIndex);
        });
        setQuestions(newQs);
    };

    // ---------------- GỬI DỮ LIỆU LÊN SERVER (SUBMIT) ----------------
    const handleSubmit = async (e) => {
        e.preventDefault();
        
        if (questions.length === 0) {
            alert("Bài thi phải có ít nhất 1 câu hỏi!"); return;
        }

        // 🔥 VÌ ĐÃ BỎ TEMP_ID, PAYLOAD BÂY GIỜ RẤT GỌN:
        const payload = {
            title: examInfo.title,
            durationInMinutes: Number(examInfo.durationInMinutes),
            examCategoryId: examInfo.examCategoryId,
            description: examInfo.description,
            // Ép kiểu các trường số để chắc chắn Backend nhận đúng
            questions: questions.map(q => ({
                ...q,
                score: Number(q.score),
                questionTypes: Number(q.questionTypes),
            }))
        };

        setIsSaving(true);
        try {
            const response = await fetch(`${API_URL}/exam`, {
                method: "POST",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(payload)
            });

            const result = await response.json();

            if (response.ok && result.success) {
                alert("Tạo bài thi thành công!");
                window.location.reload(); 
            } else {
                alert(`Lỗi tạo bài thi: ${result.message}`);
            }
        } catch (err) {
            console.error(err);
            alert("Lỗi kết nối tới máy chủ.");
        } finally {
            setIsSaving(false);
        }
    };

    if (isLoadingData) {
        return <div className="text-center mt-5"><span className="spinner-border text-primary"></span> Đang tải dữ liệu...</div>;
    }

    return (
        <div className="container-fluid px-4 pt-4 pb-5">
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h2 className="fw-bold m-0 text-primary">Tạo Bài Thi Mới (Exam)</h2>
                <button type="button" className="btn btn-success fw-bold px-4 shadow-sm" onClick={handleSubmit} disabled={isSaving}>
                    {isSaving ? <span className="spinner-border spinner-border-sm me-2"></span> : <i className="fas fa-save me-2"></i>}
                    Lưu Bài Thi
                </button>
            </div>

            <form onSubmit={handleSubmit}>
                <div className="row">
                    {/* ================= CỘT TRÁI: THÔNG TIN CHUNG ================= */}
                    <div className="col-lg-4 mb-4">
                        <div className="card shadow-sm border-0 rounded-4 sticky-top" style={{ top: '80px' }}>
                            <div className="card-header bg-white pt-4 pb-2 border-bottom-0">
                                <h5 className="fw-bold text-dark"><i className="fas fa-info-circle text-primary me-2"></i>Thông tin bài thi</h5>
                            </div>
                            <div className="card-body">
                                <div className="mb-3">
                                    <label className="form-label fw-bold">Tiêu đề bài thi <span className="text-danger">*</span></label>
                                    <input type="text" className="form-control" required placeholder="VD: TOEIC Reading 2026..." 
                                        value={examInfo.title} onChange={(e) => setExamInfo({...examInfo, title: e.target.value})} />
                                </div>

                                <div className="mb-3">
                                    <label className="form-label fw-bold">Danh mục (Category) <span className="text-danger">*</span></label>
                                    <select className="form-select" required 
                                        value={examInfo.examCategoryId} onChange={(e) => setExamInfo({...examInfo, examCategoryId: e.target.value})}>
                                        <option value="" disabled>-- Chọn danh mục --</option>
                                        {categories.map(cat => (
                                            <option key={cat.id} value={cat.id}>{cat.name}</option>
                                        ))}
                                    </select>
                                </div>

                                <div className="mb-3">
                                    <label className="form-label fw-bold">Thời gian làm bài (Phút) <span className="text-danger">*</span></label>
                                    <input type="number" className="form-control" required min="1" 
                                        value={examInfo.durationInMinutes} onChange={(e) => setExamInfo({...examInfo, durationInMinutes: e.target.value})} />
                                </div>

                                <div className="mb-3">
                                    <label className="form-label fw-bold">Mô tả ngắn</label>
                                    <textarea className="form-control" rows="3" placeholder="Mô tả mục đích bài thi..."
                                        value={examInfo.description} onChange={(e) => setExamInfo({...examInfo, description: e.target.value})}></textarea>
                                </div>
                            </div>
                        </div>
                    </div>

                    {/* ================= CỘT PHẢI: DANH SÁCH CÂU HỎI ================= */}
                    <div className="col-lg-8">
                        {questions.map((q, qIndex) => (
                            // 🔥 SỬ DỤNG qIndex ĐỂ LÀM KEY THAY VÌ TEMP_ID
                            <div key={qIndex} className="card shadow-sm border-0 rounded-4 mb-4 border-start border-4 border-primary">
                                <div className="card-header bg-light d-flex justify-content-between align-items-center pt-3 pb-3">
                                    <h6 className="fw-bold m-0 fs-5">Câu hỏi {qIndex + 1}</h6>
                                    <button type="button" className="btn btn-sm btn-outline-danger" onClick={() => handleRemoveQuestion(qIndex)} title="Xóa câu hỏi này">
                                        <i className="fas fa-trash-alt"></i>
                                    </button>
                                </div>
                                
                                <div className="card-body">
                                    <div className="row mb-3">
                                        <div className="col-md-8">
                                            <label className="form-label fw-bold text-muted small">Thuộc chủ đề (Topic) <span className="text-danger">*</span></label>
                                            <select className="form-select form-select-sm" required
                                                value={q.topicId} onChange={(e) => handleQuestionChange(qIndex, 'topicId', e.target.value)}>
                                                <option value="" disabled>-- Chọn Topic --</option>
                                                {topics.map(t => (
                                                    <option key={t.id} value={t.id}>{t.name}</option>
                                                ))}
                                            </select>
                                        </div>
                                        <div className="col-md-4">
                                            <label className="form-label fw-bold text-muted small">Điểm số <span className="text-danger">*</span></label>
                                            <input type="number" step="0.5" className="form-control form-control-sm" required
                                                value={q.score} onChange={(e) => handleQuestionChange(qIndex, 'score', e.target.value)} />
                                        </div>
                                    </div>

                                    <div className="mb-4">
                                        <label className="form-label fw-bold text-primary">Nội dung câu hỏi <span className="text-danger">*</span></label>
                                        <textarea className="form-control fs-5" rows="2" required placeholder="Nhập nội dung câu hỏi..."
                                            value={q.content} onChange={(e) => handleQuestionChange(qIndex, 'content', e.target.value)}></textarea>
                                    </div>

                                    <div className="mb-4 ps-3 border-start border-2 border-success">
                                        <label className="form-label fw-bold text-success mb-3"><i className="fas fa-list-ul me-2"></i>Các đáp án (Chọn đáp án đúng)</label>
                                        
                                        {q.answers.map((ans, aIndex) => (
                                            // 🔥 SỬ DỤNG aIndex ĐỂ LÀM KEY THAY VÌ TEMP_ID
                                            <div key={aIndex} className="input-group mb-2">
                                                <div className="input-group-text bg-white" title="Chọn làm đáp án đúng">
                                                    <input className="form-check-input mt-0" type="radio" 
                                                        name={`correctAnswer_q${qIndex}`} 
                                                        checked={ans.isCorrect} 
                                                        onChange={() => handleSetCorrectAnswer(qIndex, aIndex)} 
                                                        style={{ cursor: 'pointer', width: '20px', height: '20px' }}
                                                    />
                                                </div>
                                                <input type="text" className={`form-control ${ans.isCorrect ? 'border-success bg-success bg-opacity-10 fw-bold' : ''}`} required placeholder={`Đáp án ${aIndex + 1}...`}
                                                    value={ans.content} onChange={(e) => handleAnswerChange(qIndex, aIndex, e.target.value)} />
                                                <button type="button" className="btn btn-outline-danger" onClick={() => handleRemoveAnswer(qIndex, aIndex)}>
                                                    <i className="fas fa-times"></i>
                                                </button>
                                            </div>
                                        ))}

                                        <button type="button" className="btn btn-sm btn-outline-success mt-2" onClick={() => handleAddAnswer(qIndex)}>
                                            <i className="fas fa-plus me-1"></i> Thêm đáp án
                                        </button>
                                    </div>

                                    <div className="mb-2">
                                        <label className="form-label fw-bold text-muted small">Giải thích đáp án (Tùy chọn)</label>
                                        <textarea className="form-control form-control-sm" rows="2" placeholder="Giải thích vì sao chọn đáp án này..."
                                            value={q.explanation} onChange={(e) => handleQuestionChange(qIndex, 'explanation', e.target.value)}></textarea>
                                    </div>

                                </div>
                            </div>
                        ))}

                        <div className="text-center mb-5 mt-4">
                            <button type="button" className="btn btn-lg btn-outline-primary fw-bold px-5 rounded-pill shadow-sm" onClick={handleAddQuestion}>
                                <i className="fas fa-plus-circle me-2"></i> THÊM CÂU HỎI MỚI
                            </button>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    );
};

export default CreateExamPage;