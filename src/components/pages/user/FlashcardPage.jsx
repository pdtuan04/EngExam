import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { API_URL } from "../../../config";
const FlashcardPage = () => {
    const navigate = useNavigate();
    const [flashcards, setFlashcards] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const [showModal, setShowModal] = useState(false);
    const [newTitle, setNewTitle] = useState("");
    const [newDescription, setNewDescription] = useState("");

    const fetchFlashcards = async () => {
        setLoading(true);
        try {
            const response = await fetch(`${API_URL}/FlashCard`, {
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
                setFlashcards(data);
            } else {
                setError("Không thể tải danh sách Flashcard.");
            }
        } catch (err) {
            setError("Lỗi kết nối tới máy chủ.");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchFlashcards();
    }, []);

    // --- 1. TẠO FLASHCARD MỚI (TỰ UPDATE STATE) ---
    const handleCreateFlashcard = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch(`${API_URL}/FlashCard`, {
                method: "POST",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    title: newTitle,
                    description: newDescription
                })
            });

            if (response.ok) {
                // Lấy data thật từ backend trả về (đã có ID và UserId)
                const resultData = await response.json(); 
                
                // Đóng modal và reset form
                setShowModal(false);
                setNewTitle("");
                setNewDescription("");

                // 🔥 TỐI ƯU: Nối trực tiếp object từ Backend vào state
                // Điều này giúp giao diện cập nhật ngay lập tức
                setFlashcards(prevCards => [resultData, ...prevCards]); 
                
            } else {
                alert("Tạo flashcard thất bại.");
            }
        } catch (error) {
            console.error("Lỗi:", error);
        }
    };

    // --- 2. XÓA FLASHCARD (TỰ UPDATE STATE) ---
    const handleDeleteFlashcard = async (id, e) => {
        e.stopPropagation();

        if (!window.confirm("Bạn có chắc chắn muốn xóa bộ Flashcard này không?")) return;

        try {
            const response = await fetch(`${API_URL}/FlashCard/${id}`, {
                method: "DELETE",
                credentials: "include",
            });

            if (response.ok) {
                // 🔥 TỐI ƯU: Lọc bỏ ID vừa xóa khỏi state hiện tại mà không fetch lại
                setFlashcards(prevCards => prevCards.filter(fc => fc.id !== id));
            } else {
                alert("Xóa thất bại.");
            }
        } catch (error) {
            console.error("Lỗi khi xóa:", error);
        }
    };

    return (
        <div className="container mt-5 pt-4">
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h2 className="fw-bold m-0 text-dark">Bộ sưu tập Flashcard</h2>
                <button className="btn btn-primary px-4 rounded-pill shadow-sm fw-bold" onClick={() => setShowModal(true)}>
                    <i className="bi bi-plus-circle me-2"></i> Tạo bộ mới
                </button>
            </div>

            {loading && <div className="text-center my-4"><div className="spinner-border text-primary"></div></div>}
            {error && <div className="alert alert-danger shadow-sm">{error}</div>}

            {!loading && flashcards.length === 0 && !error ? (
                <div className="text-center text-muted mt-5 p-5 bg-light rounded-4 border">
                    <i className="bi bi-collection fs-1 mb-3 d-block opacity-25"></i>
                    <h5>Danh sách hiện đang trống</h5>
                    <p>Bắt đầu hành trình học tập bằng cách tạo bộ từ vựng đầu tiên!</p>
                </div>
            ) : (
                <div className="row g-4">
                    {flashcards.map((fc) => (
                        <div className="col-md-4" key={fc.id}>
                            <div className="card h-100 shadow-sm border-0 rounded-4 overflow-hidden card-hover position-relative">
                                {/* Nút Xóa */}
                                <button 
                                    className="btn btn-sm btn-outline-danger position-absolute border-0 rounded-circle" 
                                    style={{ top: "10px", right: "10px", zIndex: 10, width: "32px", height: "32px" }}
                                    onClick={(e) => handleDeleteFlashcard(fc.id, e)}
                                >
                                    <i className="bi bi-x-lg"></i>
                                </button>

                                <div className="card-body p-4 d-flex flex-column pt-5">
                                    <h5 className="card-title text-primary fw-bold mb-2">{fc.title}</h5>
                                    <p className="card-text text-muted small flex-grow-1">{fc.description}</p>
                                    <button 
                                        className="btn btn-primary mt-3 rounded-pill fw-bold"
                                        onClick={() => navigate(`/flashcard/${fc.id}`)}
                                    >
                                        Học ngay <i className="bi bi-arrow-right-short ms-1"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                    ))}
                </div>
            )}

            {/* Modal tạo mới */}
            {showModal && (
                <div className="modal show d-block animate__animated animate__fadeIn" tabIndex="-1" style={{ backgroundColor: "rgba(0,0,0,0.4)", zIndex: 1050 }}>
                    <div className="modal-dialog modal-dialog-centered">
                        <div className="modal-content border-0 rounded-4 shadow-lg">
                            <div className="modal-header border-0 pb-0">
                                <h5 className="modal-title fw-bold">Tạo Flashcard Mới</h5>
                                <button type="button" className="btn-close" onClick={() => setShowModal(false)}></button>
                            </div>
                            <form onSubmit={handleCreateFlashcard}>
                                <div className="modal-body p-4">
                                    <div className="mb-3">
                                        <label className="form-label fw-semibold">Tiêu đề bộ từ vựng</label>
                                        <input 
                                            type="text" 
                                            className="form-control rounded-3" 
                                            placeholder="VD: Từ vựng TOEIC Part 2"
                                            value={newTitle} 
                                            onChange={(e) => setNewTitle(e.target.value)} 
                                            required 
                                        />
                                    </div>
                                    <div className="mb-3">
                                        <label className="form-label fw-semibold">Mô tả ngắn gọn</label>
                                        <textarea 
                                            className="form-control rounded-3" 
                                            rows="3"
                                            placeholder="Mô tả nội dung của bộ flashcard này..."
                                            value={newDescription} 
                                            onChange={(e) => setNewDescription(e.target.value)} 
                                            required 
                                        ></textarea>
                                    </div>
                                </div>
                                <div className="modal-footer border-0 pt-0">
                                    <button type="button" className="btn btn-light rounded-pill px-4 fw-bold" onClick={() => setShowModal(false)}>Hủy</button>
                                    <button type="submit" className="btn btn-primary rounded-pill px-4 fw-bold">Tạo bộ học</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default FlashcardPage;