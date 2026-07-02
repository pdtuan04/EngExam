import React, { useState, useEffect } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom'; 
import { API_URL } from "../../../config";
import { useAuth } from "../../AuthContext"; 

const TheoryDetailPage = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const { user } = useAuth(); 
    
    // --- STATE BÀI HỌC ---
    const [course, setCourse] = useState(null);
    const [loading, setLoading] = useState(true);

    // --- STATE BÌNH LUẬN GỐC ---
    const [comments, setComments] = useState([]);
    const [commentPage, setCommentPage] = useState(1);
    const [totalCount, setTotalCount] = useState(0); 
    const [hasMoreComments, setHasMoreComments] = useState(false);
    const [newCommentText, setNewCommentText] = useState("");
    const [isSubmitting, setIsSubmitting] = useState(false);

    // --- STATE TRẢ LỜI BÌNH LUẬN (REPLIES) ---
    const [repliesData, setRepliesData] = useState({});
    const [activeReplyBoxId, setActiveReplyBoxId] = useState(null); 
    const [replyText, setReplyText] = useState("");

    // 🔥 ĐÃ XÓA HÀM getImageUrl VÌ BACKEND ĐÃ TRẢ FULL URL

    // 1. TẢI CHI TIẾT BÀI HỌC
    useEffect(() => {
        fetch(`${API_URL}/course/${id}`)
            .then(res => res.json())
            .then(result => {
                // Trích xuất chính xác object chứa bài học từ cục data trả về
                const actualCourse = result.data !== undefined ? result.data : result;
                setCourse(actualCourse);
                setLoading(false);
            })
            .catch(err => {
                console.error("Lỗi lấy chi tiết bài học:", err);
                setLoading(false);
            });
    }, [id]);

    // 2. TẢI DANH SÁCH BÌNH LUẬN GỐC
    const fetchComments = async (page = 1, append = false) => {
        try {
            const res = await fetch(`${API_URL}/comment/course/${id}?pageIndex=${page}&pageSize=5`, {
                credentials: "include"
            });
            if (res.ok) {
                const data = await res.json();

                if (data.items && data.items.length > 0) {
                    if (append) {
                        setComments(prev => {
                            const existingIds = new Set(prev.map(c => c.id));
                            const uniqueNewItems = data.items.filter(c => !existingIds.has(c.id));
                            return [...prev, ...uniqueNewItems];
                        });
                    } else {
                        setComments(data.items);
                    }
                }
                
                setHasMoreComments(data.hasNext);
                setCommentPage(Number(data.currentPage));
                setTotalCount(data.totalCount || 0);
            }
        } catch (err) {
            console.error("Lỗi lấy bình luận:", err);
        }
    };

    useEffect(() => {
        if (course) fetchComments(1, false);
    }, [course]);

    // 3. TẢI PHẢN HỒI (REPLIES)
    const fetchReplies = async (parentId, page = 1, append = false) => {
        try {
            const res = await fetch(`${API_URL}/comment/${parentId}/replies?pageIndex=${page}&pageSize=5`, {
                credentials: "include"
            });
            if (res.ok) {
                const data = await res.json();
                setRepliesData(prev => {
                    const currentItems = prev[parentId]?.items || [];
                    let newItems = data.items || [];
                    if (append) {
                        const existingIds = new Set(currentItems.map(c => c.id));
                        const uniqueNewItems = newItems.filter(c => !existingIds.has(c.id));
                        newItems = [...currentItems, ...uniqueNewItems];
                    }

                    return {
                        ...prev,
                        [parentId]: {
                            items: newItems,
                            page: Number(data.currentPage), 
                            hasMore: data.hasNext,
                            show: true 
                        }
                    };
                });
            }
        } catch (err) {
            console.error("Lỗi tải phản hồi:", err);
        }
    };

    const toggleReplies = (parentId) => {
        const currentData = repliesData[parentId];
        if (currentData && currentData.show) {
            setRepliesData(prev => ({ ...prev, [parentId]: { ...currentData, show: false } }));
        } else {
            if (!currentData?.items || currentData.items.length === 0) {
                fetchReplies(parentId, 1, false);
            } else {
                setRepliesData(prev => ({ ...prev, [parentId]: { ...currentData, show: true } }));
            }
        }
    };

    // 4. GỬI BÌNH LUẬN (CQRS)
    const submitComment = async (parentId = null, rootId = null) => {
        if (!user) { navigate("/login"); return; }
        const content = parentId ? replyText : newCommentText;
        if (!content.trim()) return;

        setIsSubmitting(true);
        try {
            const res = await fetch(`${API_URL}/comment`, {
                method: "POST",
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ parentId, courseId: id, content })
            });

            if (res.status === 401) { navigate("/login"); return; }

            if (res.ok) {
                const newCommentObj = await res.json(); 

                if (!parentId) {
                    setComments(prev => [newCommentObj, ...prev]);
                    setNewCommentText("");
                    setTotalCount(prev => prev + 1); 
                } else {
                    setRepliesData(prev => {
                        const existingReplies = prev[rootId]?.items || [];
                        const newItems = [...existingReplies, newCommentObj];
                        
                        newItems.sort((a, b) => a.path.localeCompare(b.path));

                        return {
                            ...prev,
                            [rootId]: {
                                ...prev[rootId],
                                items: newItems,
                                show: true
                            }
                        };
                    });
                    
                    setComments(prev => prev.map(c => c.id === rootId ? { ...c, replyCount: c.replyCount + 1 } : c));
                    setReplyText("");
                    setActiveReplyBoxId(null);
                }
            }
        } finally {
            setIsSubmitting(false);
        }
    };

    // --- UI RENDERERS ---
    if (loading) return <div className="text-center mt-5 pt-5"><div className="spinner-border text-primary"></div></div>;
    if (!course) return (
        <div className="container mt-5 pt-5 text-center">
            <div className="alert alert-warning d-inline-block px-5 shadow-sm">Bài học không tồn tại.</div><br />
            <Link to="/theory" className="btn btn-primary mt-3 rounded-pill px-4 shadow-sm"><i className="bi bi-arrow-left me-2"></i>Quay lại danh sách</Link>
        </div>
    );

    return (
        <div className="container mt-5 pt-4 mb-5">
            <div className="row justify-content-center">
                <div className="col-lg-9">
                    {/* NAV & BANNER */}
                    <nav aria-label="breadcrumb" className="mb-4">
                        <Link to="/theory" className="btn btn-sm btn-light rounded-pill mb-3 px-3 shadow-sm border fw-semibold text-muted hover-primary">
                            <i className="fa-solid fa-arrow-left me-2"></i>Quay lại
                        </Link>
                    </nav>

                    {/* 🔥 SỬ DỤNG TRỰC TIẾP URL TỪ DB (Có Optional Chaining) */}
                    {course?.imageUrl && (
                        <div className="mb-4 rounded-4 shadow-sm overflow-hidden border">
                            <img 
                                src={course.imageUrl} 
                                className="img-fluid w-100" 
                                alt={course?.name} 
                                style={{ maxHeight: '420px', objectFit: 'cover' }} 
                                onError={(e) => {
                                    e.target.onerror = null;
                                    e.target.src = "https://placehold.co/800x420?text=Image+Error";
                                }}
                            />
                        </div>
                    )}

                    <h1 className="fw-bolder mb-3 display-5 text-dark">{course?.name}</h1>
                    <div className="p-4 bg-light rounded-4 mb-5 border-start border-4 border-primary shadow-sm">
                        <p className="lead text-secondary m-0 fw-medium">{course?.description}</p>
                    </div>

                    <div className="theory-rich-content text-dark" dangerouslySetInnerHTML={{ __html: course?.content || "" }} />

                    <hr className="my-5 border-secondary opacity-25" />

                    {/* ================= BÌNH LUẬN UI MỚI ================= */}
                    <div className="comments-section bg-white p-4 p-md-5 rounded-4 shadow-sm border">
                        <div className="d-flex align-items-center mb-4 pb-2 border-bottom">
                            <h4 className="fw-bold m-0 text-dark"><i className="fa-solid fa-comments text-primary me-2"></i>Bình luận ({totalCount})</h4>
                        </div>

                        {/* KHUNG NHẬP BÌNH LUẬN GỐC */}
                        <div className="d-flex gap-3 mb-5">
                            <div className="avatar-circle flex-shrink-0">
                                {user ? user.name?.charAt(0).toUpperCase() : <i className="fa-solid fa-user"></i>}
                            </div>
                            <div className="flex-grow-1">
                                <div className="comment-input-wrapper">
                                    <textarea 
                                        className="form-control comment-textarea" 
                                        rows="2" 
                                        placeholder="Để lại bình luận của bạn..."
                                        value={newCommentText}
                                        onChange={(e) => setNewCommentText(e.target.value)}
                                        onClick={() => { if (!user) navigate("/login"); }}
                                    ></textarea>
                                </div>
                                {newCommentText.trim() && (
                                    <div className="d-flex justify-content-end mt-3 animation-fade-in">
                                        <button type="button" className="btn btn-light rounded-pill px-4 me-2 fw-semibold text-muted" onClick={() => setNewCommentText("")}>Hủy</button>
                                        <button type="button" className="btn btn-primary rounded-pill px-4 fw-bold shadow-sm" onClick={() => submitComment(null, null)} disabled={isSubmitting}>
                                            {isSubmitting ? <span className="spinner-border spinner-border-sm"></span> : <><i className="fa-solid fa-paper-plane me-2"></i>Đăng</>}
                                        </button>
                                    </div>
                                )}
                            </div>
                        </div>

                        {/* DANH SÁCH BÌNH LUẬN */}
                        {comments.length === 0 ? (
                            <div className="text-center py-5">
                                <div className="text-muted opacity-50 mb-3"><i className="fa-regular fa-comment-dots display-1"></i></div>
                                <h5 className="fw-semibold text-secondary">Chưa có bình luận nào</h5>
                                <p className="text-muted small">Hãy là người đầu tiên chia sẻ suy nghĩ của bạn!</p>
                            </div>
                        ) : (
                            <div className="comments-list">
                                {comments.map((comment) => (
                                    <div key={comment.id} className="comment-thread mb-4">
                                        {/* BÌNH LUẬN GỐC */}
                                        <div className="d-flex gap-3">
                                            <div className="avatar-circle-sm flex-shrink-0 bg-secondary">
                                                {comment.userName ? comment.userName.charAt(0).toUpperCase() : "U"}
                                            </div>
                                            
                                            <div className="flex-grow-1">
                                                <div className="comment-bubble">
                                                    <div className="mb-1">
                                                        <span className="fw-bold text-dark me-2">{comment.userName || "Người dùng ẩn danh"}</span>
                                                        <span className="text-muted small"><i className="fa-regular fa-clock me-1"></i>Vừa xong</span>
                                                    </div>
                                                    <div className="text-dark" style={{ whiteSpace: 'pre-wrap', lineHeight: "1.6" }}>{comment.content}</div>
                                                </div>

                                                {/* NÚT TƯƠNG TÁC */}
                                                <div className="d-flex align-items-center gap-4 mt-2 ms-2">
                                                    <button type="button" className="btn-action text-muted" onClick={() => {
                                                        if (!user) navigate("/login");
                                                        else setActiveReplyBoxId(activeReplyBoxId === comment.id ? null : comment.id);
                                                    }}>
                                                        <i className="fa-solid fa-reply me-1"></i> Phản hồi
                                                    </button>
                                                    {comment.replyCount > 0 && (
                                                        <button type="button" className="btn-action text-primary fw-semibold" onClick={() => toggleReplies(comment.id)}>
                                                            {repliesData[comment.id]?.show ? <><i className="fa-solid fa-angle-up me-1"></i> Ẩn phản hồi</> : <><i className="fa-solid fa-angle-down me-1"></i> Xem {comment.replyCount} phản hồi</>}
                                                        </button>
                                                    )}
                                                </div>

                                                {/* Ô NHẬP TRẢ LỜI (CHO ROOT COMMENT) */}
                                                {activeReplyBoxId === comment.id && (
                                                    <div className="d-flex gap-2 mt-3 animation-fade-in">
                                                        <div className="avatar-circle-xs bg-primary flex-shrink-0 mt-1">{user?.name?.charAt(0).toUpperCase()}</div>
                                                        <div className="flex-grow-1">
                                                            <div className="comment-input-wrapper-sm">
                                                                <textarea className="form-control border-0 bg-transparent" rows="1" autoFocus placeholder={`Phản hồi bình luận của ${comment.userName}...`} value={replyText} onChange={(e) => setReplyText(e.target.value)}></textarea>
                                                            </div>
                                                            <div className="d-flex justify-content-end gap-2 mt-2">
                                                                <button type="button" className="btn btn-sm btn-light rounded-pill px-3 fw-semibold text-muted" onClick={() => {setActiveReplyBoxId(null); setReplyText("");}}>Hủy</button>
                                                                <button type="button" className="btn btn-sm btn-primary rounded-pill px-3 fw-bold shadow-sm" disabled={!replyText.trim() || isSubmitting} onClick={() => submitComment(comment.id, comment.id)}>Gửi</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                )}

                                                {/* DANH SÁCH PHẢN HỒI (REPLIES - TRẢI PHẲNG) */}
                                                {repliesData[comment.id]?.show && (
                                                    <div className="replies-wrapper mt-3 ms-2 ps-3 border-start border-2">
                                                        {repliesData[comment.id].items.map((reply) => {
                                                            let replyToName = null;
                                                            if (reply.parentId !== comment.id) {
                                                                const parentReply = repliesData[comment.id].items.find(r => r.id === reply.parentId);
                                                                if (parentReply) replyToName = parentReply.userName || "Khách";
                                                            }

                                                            return (
                                                                <div key={reply.id} className="d-flex gap-2 mb-3">
                                                                    <div className="avatar-circle-xs bg-info flex-shrink-0 mt-1">
                                                                        {reply.userName ? reply.userName.charAt(0).toUpperCase() : "R"}
                                                                    </div>
                                                                    <div className="flex-grow-1">
                                                                        <div className="reply-bubble">
                                                                            {/* TÊN Ở DÒNG TRÊN */}
                                                                            <div className="fw-bold text-dark small mb-1">{reply.userName || "Khách"}</div>
                                                                            
                                                                            {/* TAG VÀ NỘI DUNG Ở DÒNG DƯỚI */}
                                                                            <div className="text-dark small" style={{ whiteSpace: 'pre-wrap', lineHeight: "1.5" }}>
                                                                                {replyToName && (
                                                                                    <span className="mention-tag me-1 fw-bold">@{replyToName}</span>
                                                                                )}
                                                                                {reply.content}
                                                                            </div>
                                                                        </div>
                                                                        
                                                                        <div className="mt-1 ms-2">
                                                                            <button type="button" className="btn-action text-muted small" onClick={() => {
                                                                                if (!user) navigate("/login");
                                                                                else setActiveReplyBoxId(activeReplyBoxId === reply.id ? null : reply.id);
                                                                            }}>
                                                                                Trả lời
                                                                            </button>
                                                                        </div>

                                                                        {/* Ô NHẬP TRẢ LỜI (CHO REPLY CON) */}
                                                                        {activeReplyBoxId === reply.id && (
                                                                            <div className="d-flex gap-2 mt-2 animation-fade-in">
                                                                                 <div className="avatar-circle-xs bg-primary flex-shrink-0 mt-1">{user?.name?.charAt(0).toUpperCase()}</div>
                                                                                <div className="flex-grow-1">
                                                                                    <div className="comment-input-wrapper-sm">
                                                                                        <textarea className="form-control border-0 bg-transparent" rows="1" autoFocus placeholder={`Phản hồi @${reply.userName}...`} value={replyText} onChange={(e) => setReplyText(e.target.value)}></textarea>
                                                                                    </div>
                                                                                    <div className="d-flex justify-content-end gap-2 mt-2">
                                                                                        <button type="button" className="btn btn-sm btn-light rounded-pill px-3 fw-semibold text-muted" onClick={() => {setActiveReplyBoxId(null); setReplyText("");}}>Hủy</button>
                                                                                        <button type="button" className="btn btn-sm btn-primary rounded-pill px-3 fw-bold shadow-sm" disabled={!replyText.trim() || isSubmitting} onClick={() => submitComment(reply.id, comment.id)}>Gửi</button>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        )}
                                                                    </div>
                                                                </div>
                                                            );
                                                        })}
                                                        
                                                        {/* Nút Tải Thêm Phản Hồi */}
                                                        {repliesData[comment.id].hasMore && (
                                                            <button type="button" className="btn btn-link text-decoration-none text-primary fw-semibold p-0 mt-1 mb-2 d-flex align-items-center" onClick={() => fetchReplies(comment.id, repliesData[comment.id].page + 1, true)}>
                                                                <i className="fa-solid fa-arrow-turn-down me-2" style={{transform: "scaleY(-1)"}}></i> Tải thêm phản hồi...
                                                            </button>
                                                        )}
                                                    </div>
                                                )}
                                            </div>
                                        </div>
                                    </div>
                                ))}

                                {/* Nút tải thêm Bình luận Gốc */}
                                {hasMoreComments && (
                                    <div className="text-center mt-5 border-top pt-4">
                                        <button 
                                            type="button" 
                                            className="btn btn-outline-secondary rounded-pill px-5 fw-bold hover-primary" 
                                            onClick={() => fetchComments(commentPage + 1, true)} 
                                        >
                                            Tải thêm bình luận <i className="fa-solid fa-chevron-down ms-2"></i>
                                        </button>
                                    </div>
                                )}
                            </div>
                        )}
                    </div>
                </div>
            </div>

            {/* CSS Tùy chỉnh làm đẹp Component */}
            <style>{`
                /* Article Content Styling */
                .theory-rich-content { font-size: 1.1rem; }
                .theory-rich-content img { max-width: 100%; height: auto; border-radius: 12px; margin: 25px 0; box-shadow: 0 4px 15px rgba(0,0,0,0.1); }
                .theory-rich-content table { width: 100%; border-collapse: collapse; margin: 20px 0; }
                .theory-rich-content table td, .theory-rich-content table th { border: 1px solid #dee2e6; padding: 12px; }
                .theory-rich-content table th { background-color: #f8f9fa; }

                /* Avatars */
                .avatar-circle { width: 48px; height: 48px; border-radius: 50%; background: linear-gradient(135deg, #0d6efd, #0dcaf0); color: white; display: flex; align-items: center; justify-content: center; font-size: 1.25rem; font-weight: bold; box-shadow: 0 2px 5px rgba(13,110,253,0.3); }
                .avatar-circle-sm { width: 40px; height: 40px; border-radius: 50%; background: linear-gradient(135deg, #6c757d, #adb5bd); color: white; display: flex; align-items: center; justify-content: center; font-size: 1rem; font-weight: bold; }
                .avatar-circle-xs { width: 32px; height: 32px; border-radius: 50%; color: white; display: flex; align-items: center; justify-content: center; font-size: 0.85rem; font-weight: bold; }

                /* Input Textareas */
                .comment-input-wrapper { background-color: #f8f9fa; border: 1px solid #e9ecef; border-radius: 1rem; overflow: hidden; transition: all 0.2s ease-in-out; }
                .comment-input-wrapper:focus-within { background-color: #fff; border-color: #0d6efd; box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.15); }
                .comment-textarea { background: transparent; resize: none; box-shadow: none !important; padding: 12px 16px; }
                
                .comment-input-wrapper-sm { background-color: #f8f9fa; border: 1px solid #dee2e6; border-radius: 1rem; overflow: hidden; }
                .comment-input-wrapper-sm:focus-within { border-color: #0d6efd; background-color: #fff; }

                /* Bubbles */
                .comment-bubble { background-color: #f8f9fa; border-radius: 0 1rem 1rem 1rem; padding: 12px 16px; display: inline-block; min-width: 60%; }
                .reply-bubble { background-color: #f1f3f5; border-radius: 0 1rem 1rem 1rem; padding: 10px 14px; display: inline-block; }

                /* Action Buttons */
                .btn-action { background: none; border: none; font-size: 0.85rem; font-weight: 600; padding: 0; transition: color 0.2s; }
                .btn-action:hover { color: #0d6efd !important; text-decoration: underline; }

                /* Tags & Tree Line */
                .mention-tag { color: #0d6efd; background-color: rgba(13, 110, 253, 0.1); padding: 2px 8px; border-radius: 12px; font-size: 0.85rem; display: inline-block; margin-bottom: 2px;}
                .replies-wrapper { border-color: #dee2e6 !important; }

                /* Utils */
                .hover-primary:hover { background-color: #0d6efd; color: white !important; border-color: #0d6efd !important; }
                .animation-fade-in { animation: fadeIn 0.3s ease-in-out; }
                @keyframes fadeIn { from { opacity: 0; transform: translateY(-5px); } to { opacity: 1; transform: translateY(0); } }
            `}</style>
        </div>
    );
};

export default TheoryDetailPage;