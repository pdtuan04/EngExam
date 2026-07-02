import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom'; // Dùng để chuyển trang
import { API_URL } from "../../../config";

const ExamManagePage = () => {
    const navigate = useNavigate();

    // ---------------- STATE QUẢN LÝ DỮ LIỆU BẢNG ----------------
    const [exams, setExams] = useState([]);
    const [categories, setCategories] = useState([]); // Lưu danh mục để map Tên thay vì ID
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    // Phân trang & Tìm kiếm
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [totalCount, setTotalCount] = useState(0);
    const [searchQuery, setSearchQuery] = useState("");

    // ---------------- STATE QUẢN LÝ MODAL VIEW & DELETE ----------------
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [showViewModal, setShowViewModal] = useState(false);
    const [selectedExam, setSelectedExam] = useState(null);
    const [isActionLoading, setIsActionLoading] = useState(false);

    // ---------------- API: LẤY DANH SÁCH CATEGORY (ĐỂ MAP TÊN) ----------------
    const fetchCategories = async () => {
        try {
            const response = await fetch(`${API_URL}/ExamCategory`, {
                method: "GET",
                credentials: "include",
            });
            if (response.ok) {
                const result = await response.json();
                if (result.success) setCategories(result.data || []);
            }
        } catch (err) {
            console.error("Lỗi lấy danh mục:", err);
        }
    };

    // ---------------- API: LẤY DANH SÁCH BÀI THI (PAGINATED) ----------------
    const fetchExams = async (page = 1, size = 5) => {
        setLoading(true);
        try {
            const response = await fetch(`${API_URL}/exam/paginated?PageIndex=${page}&PageSize=${size}`, {
                method: "GET",
                credentials: "include",
                headers: { "Content-Type": "application/json" }
            });

            if (response.ok) {
                const json = await response.json();
                if (json.success && json.data) {
                    // 🔥 Lấy đúng cấu trúc JSON bạn cung cấp (json.data.items)
                    setExams(json.data.items || []);
                    setCurrentPage(json.data.currentPage || 1);
                    setTotalPages(json.data.totalPages || 1);
                    setTotalCount(json.data.totalCount || 0);
                }
            } else {
                setError("Cannot load exams.");
            }
        } catch (err) {
            setError("Server connection error.");
        } finally {
            setLoading(false);
        }
    };

    // Chạy 1 lần lúc mở trang để nạp Category
    useEffect(() => {
        fetchCategories();
    }, []);

    // Chạy mỗi khi đổi trang
    useEffect(() => {
        fetchExams(currentPage, pageSize);
    }, [currentPage, pageSize]);


    // ---------------- HANDLERS ----------------
    const handleAddClick = () => {
        // Chuyển hướng sang trang Tạo Bài thi xịn sò vừa làm lúc nãy
        navigate('/admin/exams/create'); 
    };

    const handleEditClick = (id) => {
        // Chuyển hướng sang trang Sửa Bài thi (Cùng là trang Create nhưng truyền ID)
        navigate(`/admin/exams/edit/${id}`);
    };

    const handleViewClick = async (id) => {
        try {
            const response = await fetch(`${API_URL}/exam/${id}`, { credentials: "include" });
            if (response.ok) {
                const result = await response.json();
                setSelectedExam(result.data); // Lấy cục data chi tiết
                setShowViewModal(true);
            }
        } catch (err) {
            alert("Lỗi lấy chi tiết bài thi!");
        }
    };

    const handleDeleteClick = (exam) => {
        setSelectedExam(exam);
        setShowDeleteModal(true);
    };

    // ---------------- API: DELETE (CQRS OPTIMISTIC UPDATE) ----------------
    const confirmDeleteExam = async () => {
        setIsActionLoading(true);
        try {
            const response = await fetch(`${API_URL}/exam/${selectedExam.id}`, {
                method: "DELETE",
                credentials: "include",
            });

            if (response.ok) {
                setShowDeleteModal(false);
                // CQRS: Tự xóa khỏi mảng
                setExams(prev => prev.filter(e => e.id !== selectedExam.id));
                setTotalCount(prev => {
                    const newCount = prev - 1;
                    const newTotalPages = Math.ceil(newCount / pageSize) || 1;
                    setTotalPages(newTotalPages);

                    if (currentPage > newTotalPages && currentPage > 1) {
                        setCurrentPage(newTotalPages);
                    }
                    return newCount;
                });
            } else {
                alert("Failed to delete exam.");
            }
        } catch (err) {
            alert("Server connection error.");
        } finally {
            setIsActionLoading(false);
        }
    };

    // ---------------- TIỆN ÍCH ----------------
    const getCategoryName = (id) => {
        const cat = categories.find(c => c.id === id);
        return cat ? cat.name : <span className="text-danger small">Không rõ</span>;
    };

    const formatDate = (dateStr) => {
        if (!dateStr || dateStr.startsWith("0001")) return "Chưa cập nhật";
        return new Date(dateStr).toLocaleDateString('vi-VN');
    };

    // Lọc tìm kiếm
    const filteredExams = exams.filter(exam => 
        exam.title.toLowerCase().includes(searchQuery.toLowerCase()) || 
        (exam.description && exam.description.toLowerCase().includes(searchQuery.toLowerCase()))
    );

    const startEntry = (currentPage - 1) * pageSize + 1;
    const endEntry = Math.min(currentPage * pageSize, totalCount);

    return (
        <div className="container-fluid px-4 pt-4">
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h2 className="fw-bold m-0 text-primary">Exam Management</h2>
                <button className="btn btn-primary fw-bold shadow-sm" onClick={handleAddClick}>
                    <i className="fas fa-plus me-2"></i> Add Exam
                </button>
            </div>

            <div className="card shadow-sm border-0 rounded-4 mb-4">
                <div className="card-body">
                    {error && <div className="alert alert-danger">{error}</div>}

                    {/* TOP CONTROLS */}
                    <div className="row mb-3 align-items-center">
                        <div className="col-md-6 d-flex align-items-center">
                            <select 
                                className="form-select form-select-sm w-auto d-inline-block"
                                value={pageSize}
                                onChange={(e) => { setPageSize(Number(e.target.value)); setCurrentPage(1); }}
                            >
                                <option value="5">5</option>
                                <option value="10">10</option>
                                <option value="15">15</option>
                                <option value="50">50</option>
                            </select>
                            <span className="ms-2 text-muted">entries per page</span>
                        </div>
                        <div className="col-md-6 d-flex justify-content-md-end mt-2 mt-md-0">
                            <div className="input-group input-group-sm" style={{ maxWidth: '250px' }}>
                                <span className="input-group-text bg-white"><i className="fas fa-search"></i></span>
                                <input 
                                    type="text" className="form-control" placeholder="Search..." 
                                    value={searchQuery} onChange={(e) => setSearchQuery(e.target.value)}
                                />
                            </div>
                        </div>
                    </div>

                    {/* TABLE */}
                    {loading ? (
                        <div className="text-center my-5">
                            <div className="spinner-border text-primary"></div>
                            <p className="mt-2 text-muted">Loading data...</p>
                        </div>
                    ) : (
                        <div className="table-responsive">
                            <table className="table table-bordered table-hover align-middle">
                                <thead className="table-light text-center">
                                    <tr>
                                        <th style={{ width: '5%' }}>No.</th>
                                        <th style={{ width: '30%' }}>Exam Title</th>
                                        <th style={{ width: '20%' }}>Category</th>
                                        <th style={{ width: '10%' }}>Duration</th>
                                        <th style={{ width: '15%' }}>Created At</th>
                                        <th style={{ width: '20%' }}>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {filteredExams.length > 0 ? (
                                        filteredExams.map((exam, index) => (
                                            <tr key={exam.id}>
                                                <td className="text-center text-muted">
                                                    {(currentPage - 1) * pageSize + index + 1}
                                                </td>
                                                <td className="fw-bold text-dark">{exam.title}</td>
                                                <td className="text-center">
                                                    <span className="badge bg-secondary">
                                                        {getCategoryName(exam.examCategoryId)}
                                                    </span>
                                                </td>
                                                <td className="text-center fw-bold text-primary">
                                                    {exam.durationInMinutes} mins
                                                </td>
                                                <td className="text-center text-muted">
                                                    {formatDate(exam.createdAt)}
                                                </td>
                                                <td className="text-center">
                                                    <button className="btn btn-sm btn-info text-white me-1" onClick={() => handleViewClick(exam.id)} title="View">
                                                        <i className="fas fa-eye"></i>
                                                    </button>
                                                    <button className="btn btn-sm btn-primary me-1" onClick={() => handleEditClick(exam.id)} title="Edit">
                                                        <i className="fas fa-edit"></i>
                                                    </button>
                                                    <button className="btn btn-sm btn-danger" onClick={() => handleDeleteClick(exam)} title="Delete">
                                                        <i className="fas fa-trash-alt"></i>
                                                    </button>
                                                </td>
                                            </tr>
                                        ))
                                    ) : (
                                        <tr><td colSpan="6" className="text-center text-muted py-4">No entries found.</td></tr>
                                    )}
                                </tbody>
                            </table>
                        </div>
                    )}

                    {/* PAGINATION */}
                    {!loading && (
                        <div className="d-flex flex-column flex-md-row justify-content-between align-items-center mt-3">
                            <div className="small text-muted mb-2 mb-md-0">
                                Showing {totalCount === 0 ? 0 : startEntry} to {endEntry} of {totalCount} entries
                            </div>
                            <ul className="pagination pagination-sm m-0">
                                <li className={`page-item ${currentPage === 1 ? 'disabled' : ''}`}>
                                    <button className="page-link" onClick={() => setCurrentPage(1)}><i className="fas fa-angle-double-left"></i></button>
                                </li>
                                <li className={`page-item ${currentPage === 1 ? 'disabled' : ''}`}>
                                    <button className="page-link" onClick={() => setCurrentPage(prev => Math.max(prev - 1, 1))}><i className="fas fa-angle-left"></i></button>
                                </li>
                                {[...Array(totalPages)].map((_, i) => (
                                    <li key={i} className={`page-item ${currentPage === i + 1 ? 'active' : ''}`}>
                                        <button className="page-link" onClick={() => setCurrentPage(i + 1)}>{i + 1}</button>
                                    </li>
                                ))}
                                <li className={`page-item ${currentPage === totalPages || totalPages === 0 ? 'disabled' : ''}`}>
                                    <button className="page-link" onClick={() => setCurrentPage(prev => Math.min(prev + 1, totalPages))}><i className="fas fa-angle-right"></i></button>
                                </li>
                                <li className={`page-item ${currentPage === totalPages || totalPages === 0 ? 'disabled' : ''}`}>
                                    <button className="page-link" onClick={() => setCurrentPage(totalPages)}><i className="fas fa-angle-double-right"></i></button>
                                </li>
                            </ul>
                        </div>
                    )}
                </div>
            </div>

            {/* ======================= MODALS ======================= */}
            
            {/* 1. DELETE MODAL */}
            {showDeleteModal && (
                <>
                    <div className="modal fade show d-block" tabIndex="-1">
                        <div className="modal-dialog modal-dialog-centered">
                            <div className="modal-content">
                                <div className="modal-header bg-danger text-white">
                                    <h5 className="modal-title"><i className="fas fa-exclamation-triangle me-2"></i> Confirm Delete</h5>
                                    <button type="button" className="btn-close btn-close-white" onClick={() => setShowDeleteModal(false)}></button>
                                </div>
                                <div className="modal-body">
                                    <p>Are you sure you want to delete exam: <strong>{selectedExam?.title}</strong>?</p>
                                </div>
                                <div className="modal-footer border-0 pt-0">
                                    <button type="button" className="btn btn-light" onClick={() => setShowDeleteModal(false)}>Cancel</button>
                                    <button type="button" className="btn btn-danger" onClick={confirmDeleteExam} disabled={isActionLoading}>
                                        {isActionLoading ? <span className="spinner-border spinner-border-sm"></span> : "Delete"}
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="modal-backdrop fade show"></div>
                </>
            )}

            {/* 2. VIEW MODAL (Xem nhanh số lượng câu hỏi) */}
            {showViewModal && selectedExam && (
                <>
                    <div className="modal fade show d-block" tabIndex="-1">
                        <div className="modal-dialog modal-lg modal-dialog-centered modal-dialog-scrollable">
                            <div className="modal-content">
                                <div className="modal-header bg-info text-white">
                                    <h5 className="modal-title"><i className="fas fa-info-circle me-2"></i> Exam Details</h5>
                                    <button type="button" className="btn-close btn-close-white" onClick={() => setShowViewModal(false)}></button>
                                </div>
                                <div className="modal-body">
                                    <h4 className="fw-bold text-center text-primary">{selectedExam.title}</h4>
                                    <div className="d-flex justify-content-center gap-3 mb-4 mt-3">
                                        <span className="badge bg-secondary fs-6 px-3 py-2"><i className="fas fa-folder me-2"></i>{getCategoryName(selectedExam.examCategoryId)}</span>
                                        <span className="badge bg-success fs-6 px-3 py-2"><i className="fas fa-clock me-2"></i>{selectedExam.durationInMinutes} mins</span>
                                        <span className="badge bg-primary fs-6 px-3 py-2"><i className="fas fa-question-circle me-2"></i>{selectedExam.questions?.length || 0} Questions</span>
                                    </div>
                                    <p className="text-muted text-center bg-light p-3 rounded">{selectedExam.description || "No description provided."}</p>
                                    
                                    <hr className="my-4" />
                                    <h5 className="fw-bold"><i className="fas fa-list-ol me-2"></i>Preview Questions:</h5>
                                    
                                    {selectedExam.questions?.map((q, i) => (
                                        <div key={q.id} className="card border-0 bg-light mb-3">
                                            <div className="card-body">
                                                <p className="fw-bold mb-2">Q{i + 1}: {q.content}</p>
                                                <div className="ps-3">
                                                    {q.answers?.map((ans, j) => (
                                                        <div key={ans.id} className={`small ${ans.isCorrect ? 'text-success fw-bold' : 'text-muted'}`}>
                                                            {String.fromCharCode(65 + j)}. {ans.content}
                                                            {ans.isCorrect && <i className="fas fa-check-circle ms-2"></i>}
                                                        </div>
                                                    ))}
                                                </div>
                                            </div>
                                        </div>
                                    ))}
                                </div>
                                <div className="modal-footer border-0 bg-light">
                                    <button type="button" className="btn btn-secondary px-4" onClick={() => setShowViewModal(false)}>Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="modal-backdrop fade show"></div>
                </>
            )}

        </div>
    );
};

export default ExamManagePage;