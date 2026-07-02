import React, { useState, useEffect } from 'react';
import { useParams, useNavigate, Link } from 'react-router-dom';
import { API_URL } from "../../../config";
const FlashcardDetailPage = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const [flashcard, setFlashcard] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    // Mode: 'list' hoặc 'learn'
    const [viewMode, setViewMode] = useState('list');
    const [currentIndex, setCurrentIndex] = useState(0); 
    const [isFlipped, setIsFlipped] = useState(false);   

    // State cho Modal thêm từ mới
    const [showAddModal, setShowAddModal] = useState(false);
    const [newWord, setNewWord] = useState({ text: "", meaning: "" });

    const fetchFlashcardDetail = async () => {
        setLoading(true);
        try {
            const response = await fetch(`${API_URL}/FlashCard/${id}`, {
                method: "GET",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                cache: "no-store"
            });

            if (response.status === 401) {
                navigate("/login");
                return;
            }

            if (response.ok) {
                const data = await response.json();
                setFlashcard(data);
            } else {
                setError("Không tìm thấy bộ Flashcard này.");
            }
        } catch (err) {
            setError("Lỗi kết nối tới máy chủ.");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchFlashcardDetail();
    }, [id]);

    // --- API THÊM TỪ MỚI ---
    const handleAddWord = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch(`${API_URL}/word`, {
                method: "POST",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    text: newWord.text,
                    meaning: newWord.meaning,
                    flashCardId: id
                })
            });

            if (response.ok) {
                const addedWord = await response.json();
                // Cập nhật danh sách hiển thị ngay lập tức
                setFlashcard(prev => ({
                    ...prev,
                    words: [...prev.words, addedWord]
                }));
                // Reset form và đóng modal
                setNewWord({ text: "", meaning: "" });
                setShowAddModal(false);
            } else {
                alert("Không thể thêm từ mới.");
            }
        } catch (error) {
            console.error("Lỗi:", error);
        }
    };

    const toggleMemorized = async (wordId, e) => {
        if (e) e.stopPropagation(); 
        try {
            const wordToToggle = flashcard.words.find(w => w.id === wordId);
            const newMemorizedStatus = !wordToToggle.isMemorized;
            const response = await fetch(`${API_URL}/word/${wordId}/memorized`, {
                method: "PATCH",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    id: wordId,
                    isMemorized: newMemorizedStatus,
                    flashCardId: id
                })
            });

            if (response.ok) {
                const result = await response.json();
                if (result.success) {
                    setFlashcard(prev => ({
                        ...prev,
                        words: prev.words.map(w =>
                            w.id === wordId ? { ...w, isMemorized: result.isMemorized } : w
                        )
                    }));
                }
            } else {
                console.error("Cập nhật trạng thái thất bại");
            }
        } catch (error) {
            console.error("Lỗi:", error);
        }
    };

    const handleNext = () => {
        if (currentIndex < flashcard.words.length - 1) {
            setIsFlipped(false); 
            setTimeout(() => setCurrentIndex(prev => prev + 1), 150); 
        }
    };

    const handlePrev = () => {
        if (currentIndex > 0) {
            setIsFlipped(false);
            setTimeout(() => setCurrentIndex(prev => prev - 1), 150);
        }
    };

    if (loading) return <div className="text-center mt-5"><div className="spinner-border text-primary"></div></div>;
    if (error) return <div className="alert alert-danger mt-5 container">{error}</div>;
    if (!flashcard) return null;

    const totalWords = flashcard.words?.length || 0;
    const memorizedWords = flashcard.words?.filter(w => w.isMemorized).length || 0;
    const progressPercent = totalWords > 0 ? Math.round((memorizedWords / totalWords) * 100) : 0;
    const currentWord = flashcard.words?.[currentIndex];

    return (
        <div className="container mt-5 pt-4 mb-5" style={{ maxWidth: "850px" }}>
            <style>
                {`
                    .flashcard-scene { perspective: 1000px; width: 100%; height: 380px; }
                    .flashcard-inner { position: relative; width: 100%; height: 100%; transition: transform 0.6s cubic-bezier(0.4, 0.2, 0.2, 1); transform-style: preserve-3d; cursor: pointer; }
                    .flashcard-inner.is-flipped { transform: rotateX(180deg); }
                    .flashcard-face { position: absolute; width: 100%; height: 100%; backface-visibility: hidden; border-radius: 1.5rem; box-shadow: 0 8px 24px rgba(0,0,0,0.08); display: flex; flex-direction: column; justify-content: center; align-items: center; background-color: white; border: 2px solid #e9ecef; }
                    .flashcard-face.back { transform: rotateX(180deg); background-color: #f8f9fa; border-color: #0d6efd; }
                    .memorize-badge { position: absolute; top: 1.5rem; right: 1.5rem; z-index: 10; }
                `}
            </style>

            {/* Header */}
            <div className="d-flex align-items-center justify-content-between mb-4">
                <Link to="/flashcards" className="btn btn-light shadow-sm rounded-pill px-4 border d-flex align-items-center gap-2">
                    <i className="bi bi-arrow-left"></i> Quay lại
                </Link>
                <h3 className="fw-bold m-0 text-center flex-grow-1">{flashcard.title}</h3>
                <div style={{ width: "115px" }}></div>
            </div>
            
            {/* Thanh tiến độ */}
            <div className="text-center mb-5">
                <div className="progress mx-auto" style={{ height: "10px", maxWidth: "500px", borderRadius: "10px" }}>
                    <div className="progress-bar bg-success" style={{ width: `${progressPercent}%` }}></div>
                </div>
                <small className="text-muted mt-2 d-block fw-medium">Đã thuộc {memorizedWords} / {totalWords} từ</small>
            </div>

            {/* CHẾ ĐỘ DANH SÁCH */}
            {viewMode === 'list' ? (
                <div className="animate__animated animate__fadeIn">
                    <div className="d-flex justify-content-between align-items-center mb-4">
                        <h4 className="fw-bold m-0 text-dark">Danh sách từ vựng</h4>
                        <div className="d-flex gap-2">
                            <button 
                                className="btn btn-outline-primary px-3 rounded-pill fw-bold d-flex align-items-center gap-2"
                                onClick={() => setShowAddModal(true)}
                            >
                                <i className="bi bi-plus-lg"></i> Thêm từ vựng
                            </button>
                            <button 
                                className="btn btn-primary px-4 rounded-pill fw-bold shadow-sm d-flex align-items-center gap-2"
                                onClick={() => setViewMode('learn')}
                                disabled={totalWords === 0}
                            >
                                <i className="bi bi-play-circle-fill"></i> Học ngay
                            </button>
                        </div>
                    </div>
                    
                    {totalWords === 0 ? (
                        <div className="text-center text-muted p-5 bg-light rounded-4 border">
                            <h5>Chưa có từ vựng nào. Hãy bấm "Thêm từ vựng" để bắt đầu!</h5>
                        </div>
                    ) : (
                        <div className="card shadow-sm border-0 rounded-4 overflow-hidden">
                            <div className="list-group list-group-flush">
                                {flashcard.words.map((word, index) => (
                                    <div key={word.id} className="list-group-item d-flex align-items-center justify-content-between p-3 border-bottom">
                                        <div className="d-flex align-items-center gap-4">
                                            <span className="text-muted fw-bold">{index + 1}</span>
                                            <div>
                                                <div className="fw-bold fs-5 text-primary">{word.text}</div>
                                                <div className="text-secondary">{word.meaning}</div>
                                            </div>
                                        </div>
                                        <button 
                                            className={`btn btn-sm rounded-pill px-3 ${word.isMemorized ? 'btn-success' : 'btn-outline-secondary'}`}
                                            onClick={() => toggleMemorized(word.id)}
                                        >
                                            <i className={`bi ${word.isMemorized ? 'bi-check-circle-fill' : 'bi-circle'}`}></i>
                                            <span className="ms-2 d-none d-md-inline">{word.isMemorized ? "Đã thuộc" : "Chưa thuộc"}</span>
                                        </button>
                                    </div>
                                ))}
                            </div>
                        </div>
                    )}
                </div>
            ) : (
                /* CHẾ ĐỘ FLASHCARD */
                <div className="animate__animated animate__zoomIn text-center">
                    <button className="btn btn-link text-decoration-none mb-3 d-flex align-items-center gap-2 mx-auto" onClick={() => setViewMode('list')}>
                        <i className="bi bi-list-ul"></i> Quay lại danh sách
                    </button>
                    
                    <div className="flashcard-scene mx-auto mb-4" style={{ maxWidth: "650px" }}>
                        <div className={`flashcard-inner ${isFlipped ? 'is-flipped' : ''}`} onClick={() => setIsFlipped(!isFlipped)}>
                            <div className="flashcard-face front">
                                <div className="memorize-badge">
                                    <button className={`btn px-4 py-2 fs-6 fw-bold rounded-pill shadow-sm d-flex align-items-center gap-2 ${currentWord.isMemorized ? 'btn-success' : 'btn-light border text-muted'}`} onClick={(e) => toggleMemorized(currentWord.id, e)}>
                                        <i className={`bi fs-5 ${currentWord.isMemorized ? 'bi-check-circle-fill' : 'bi-circle'}`}></i>
                                        {currentWord.isMemorized ? "Đã thuộc" : "Đánh dấu"}
                                    </button>
                                </div>
                                <h1 className="fw-bold text-primary display-4 mb-4">{currentWord.text}</h1>
                                <span className="text-secondary rounded-pill bg-light border px-4 py-2">Nhấn để xem đáp án <i className="bi bi-arrow-repeat ms-1"></i></span>
                            </div>
                            <div className="flashcard-face back">
                                <div className="memorize-badge">
                                    <button className={`btn px-4 py-2 fs-6 fw-bold rounded-pill shadow-sm d-flex align-items-center gap-2 ${currentWord.isMemorized ? 'btn-success' : 'btn-light border text-muted'}`} onClick={(e) => toggleMemorized(currentWord.id, e)}>
                                        <i className={`bi fs-5 ${currentWord.isMemorized ? 'bi-check-circle-fill' : 'bi-circle'}`}></i>
                                        {currentWord.isMemorized ? "Đã thuộc" : "Đánh dấu"}
                                    </button>
                                </div>
                                <h2 className="fw-bold text-success display-5 mb-4">{currentWord.meaning}</h2>
                                <span className="text-primary rounded-pill bg-white border border-primary px-4 py-2">Nhấn để xem câu hỏi <i className="bi bi-arrow-repeat ms-1"></i></span>
                            </div>
                        </div>
                    </div>

                    <div className="d-flex justify-content-center align-items-center gap-3">
                        <button className="btn btn-outline-primary px-4 py-2 fw-bold d-flex align-items-center gap-2" style={{ borderRadius: "12px", minWidth: "120px" }} onClick={handlePrev} disabled={currentIndex === 0}>
                            <i className="bi bi-chevron-left"></i> Trước
                        </button>
                        <div className="fw-bold fs-5 text-dark px-4 py-2 bg-white rounded-3 shadow-sm border text-center" style={{ minWidth: "100px" }}>
                            {currentIndex + 1} / {totalWords}
                        </div>
                        <button className="btn btn-primary px-4 py-2 fw-bold d-flex align-items-center gap-2 justify-content-center" style={{ borderRadius: "12px", minWidth: "120px" }} onClick={handleNext} disabled={currentIndex === totalWords - 1}>
                            Tiếp <i className="bi bi-chevron-right"></i>
                        </button>
                    </div>
                </div>
            )}

            {/* MODAL THÊM TỪ MỚI */}
            {showAddModal && (
                <div className="modal show d-block" tabIndex="-1" style={{ backgroundColor: "rgba(0,0,0,0.5)", zIndex: 1050 }}>
                    <div className="modal-dialog modal-dialog-centered">
                        <div className="modal-content border-0 rounded-4 shadow">
                            <div className="modal-header border-0 pb-0">
                                <h5 className="modal-title fw-bold">Thêm từ vựng mới</h5>
                                <button type="button" className="btn-close" onClick={() => setShowAddModal(false)}></button>
                            </div>
                            <form onSubmit={handleAddWord}>
                                <div className="modal-body">
                                    <div className="mb-3">
                                        <label className="form-label fw-semibold">Từ vựng (Tiếng Anh)</label>
                                        <input 
                                            type="text" 
                                            className="form-control rounded-3" 
                                            placeholder="VD: Jump"
                                            value={newWord.text}
                                            onChange={(e) => setNewWord({...newWord, text: e.target.value})}
                                            required
                                        />
                                    </div>
                                    <div className="mb-3">
                                        <label className="form-label fw-semibold">Ý nghĩa (Tiếng Việt)</label>
                                        <input 
                                            type="text" 
                                            className="form-control rounded-3" 
                                            placeholder="VD: Nhảy"
                                            value={newWord.meaning}
                                            onChange={(e) => setNewWord({...newWord, meaning: e.target.value})}
                                            required
                                        />
                                    </div>
                                </div>
                                <div className="modal-footer border-0">
                                    <button type="button" className="btn btn-light rounded-pill px-4" onClick={() => setShowAddModal(false)}>Hủy</button>
                                    <button type="submit" className="btn btn-primary rounded-pill px-4 fw-bold">Lưu lại</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default FlashcardDetailPage;