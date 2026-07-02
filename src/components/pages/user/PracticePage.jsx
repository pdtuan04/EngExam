import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { useAuth } from "../../AuthContext";
import { API_URL } from "../../../config";

function PracticePage() {
    const { practiceId } = useParams();
    const navigate = useNavigate();
    const { logout } = useAuth();

    const [practice, setPractice] = useState(null);
    const [loading, setLoading] = useState(true);
    
    const [answeredState, setAnsweredState] = useState({});
    const [textInputs, setTextInputs] = useState({});
    useEffect(() => {
        const fetchPractice = async () => {
            try {
                const response = await fetch(`${API_URL}/practice/${practiceId}`, {
                    credentials: "include"
                });

                if (response.status === 401) {
                    alert("Phiên làm việc hết hạn!");
                    logout();
                    navigate("/login");
                    return;
                }

                const json = await response.json();
                if (json.success) {
                    setPractice(json.data);
                }
            } catch (error) {
                console.error("Lỗi tải bài luyện tập:", error);
            } finally {
                setLoading(false);
            }
        };

        fetchPractice();
    }, [practiceId, navigate, logout]);
    const handleCheckChoice = (question, selectedAnswer) => {
        if (answeredState[question.id]?.isAnswered) return;

        setAnsweredState(prev => ({
            ...prev,
            [question.id]: {
                isAnswered: true,
                isCorrect: selectedAnswer.isCorrect,
                selectedId: selectedAnswer.id
            }
        }));
    };

    const handleCheckFillBlank = (question) => {
        if (answeredState[question.id]?.isAnswered) return;

        const userText = textInputs[question.id] || "";
        const correctAnswer = question.answers.find(a => a.isCorrect)?.content || "";
        const isCorrect = userText.trim().toLowerCase() === correctAnswer.trim().toLowerCase();

        setAnsweredState(prev => ({
            ...prev,
            [question.id]: {
                isAnswered: true,
                isCorrect: isCorrect,
                userText: userText,
                correctText: correctAnswer
            }
        }));
    };

    if (loading) return <div className="text-center mt-5 pt-5"><div className="spinner-border text-success"></div><p>Đang tải bài luyện tập...</p></div>;
    if (!practice) return <h3 className="text-center mt-5 pt-5 text-danger">Không tìm thấy bài luyện tập!</h3>;

    return (
        <div className="container mt-5 pt-5 mb-5">
            <div className="text-center mb-5">
                <h2 className="fw-bold text-success">Luyện Tập: {practice.title}</h2>
                <p className="text-muted fs-5">{practice.description}</p>
            </div>

            {practice.questions?.map((q, index) => {
                const status = answeredState[q.id];
                const isAnswered = status?.isAnswered;

                return (
                    <div key={q.id} className={`card shadow-sm mb-4 border-${isAnswered ? (status.isCorrect ? 'success' : 'danger') : 'light'}`}>
                        <div className="card-body">
                            <h5 className="fw-bold mb-3">Câu {index + 1}: {q.content}</h5>

                            {/* HIỂN THỊ ẢNH NẾU CÓ */}
                            {q.imageUrl && (
                                <img 
                                    src={q.imageUrl.startsWith("http") ? q.imageUrl : `${API_URL}/${q.imageUrl}`} 
                                    alt="Question content" 
                                    className="img-fluid rounded mb-3" 
                                    style={{ maxHeight: '200px' }}
                                />
                            )}

                            {/* KIỂU 0: TRẮC NGHIỆM */}
                            {q.questionTypes === 0 && (
                                <div className="ps-3">
                                    {q.answers.map(ans => {
                                        // Tô màu xanh cho câu đúng, màu đỏ cho câu chọn sai
                                        let labelClass = "form-check-label w-100 p-2 rounded cursor-pointer ";
                                        if (isAnswered) {
                                            if (ans.isCorrect) labelClass += "bg-success text-white fw-bold";
                                            else if (status.selectedId === ans.id && !ans.isCorrect) labelClass += "bg-danger text-white";
                                        }

                                        return (
                                            <div className="form-check mb-2" key={ans.id}>
                                                <input
                                                    className="form-check-input mt-2" 
                                                    type="radio" 
                                                    name={`question-${q.id}`} 
                                                    disabled={isAnswered}
                                                    checked={status?.selectedId === ans.id}
                                                    onChange={() => handleCheckChoice(q, ans)}
                                                />
                                                <label className={labelClass}>{ans.content}</label>
                                            </div>
                                        );
                                    })}
                                </div>
                            )}

                            {/* KIỂU 1: ĐIỀN TỪ */}
                            {q.questionTypes === 1 && (
                                <div className="mt-2 d-flex gap-2">
                                    <input 
                                        type="text" 
                                        className={`form-control ${isAnswered ? (status.isCorrect ? 'is-valid' : 'is-invalid') : 'border-success'}`} 
                                        placeholder="Nhập đáp án của bạn..." 
                                        disabled={isAnswered}
                                        value={textInputs[q.id] || ''}
                                        onChange={(e) => setTextInputs({...textInputs, [q.id]: e.target.value})} 
                                    />
                                    {!isAnswered && (
                                        <button className="btn btn-success" onClick={() => handleCheckFillBlank(q)}>
                                            Kiểm tra
                                        </button>
                                    )}
                                </div>
                            )}

                            {/* HIỂN THỊ GIẢI THÍCH (Chỉ hiện sau khi đã trả lời) */}
                            {isAnswered && (
                                <div className={`alert mt-3 mb-0 ${status.isCorrect ? 'alert-success' : 'alert-danger'}`}>
                                    <h6 className="fw-bold">
                                        {status.isCorrect ? "🎉 Chính xác!" : "❌ Rất tiếc, chưa đúng!"}
                                    </h6>
                                    {/* Với câu điền từ bị sai, nhắc lại đáp án đúng cho user biết */}
                                    {q.questionTypes === 1 && !status.isCorrect && (
                                        <p className="mb-1">Đáp án đúng là: <strong>{status.correctText}</strong></p>
                                    )}
                                    <hr className="my-2"/>
                                    <p className="mb-0"><strong>💡 Giải thích:</strong> {q.explanation}</p>
                                </div>
                            )}
                        </div>
                    </div>
                );
            })}
        </div>
    );
}

export default PracticePage;