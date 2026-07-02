import React, { useState, useEffect } from 'react';
import { API_URL } from "../../../config";

const TopicManagePage = () => {
    // ---------------- STATE QUẢN LÝ DỮ LIỆU BẢNG ----------------
    const [topics, setTopics] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    // Phân trang & Tìm kiếm
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [pageSize, setPageSize] = useState(10); // Mặc định 10 theo API của bạn
    const [totalCount, setTotalCount] = useState(0);
    const [searchQuery, setSearchQuery] = useState("");

    // ---------------- STATE QUẢN LÝ MODAL (POPUP) ----------------
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    
    // Gộp Add và Edit vào chung 1 Modal
    const [showFormModal, setShowFormModal] = useState(false);
    const [isEditMode, setIsEditMode] = useState(false);

    const [selectedTopic, setSelectedTopic] = useState(null);
    const [isActionLoading, setIsActionLoading] = useState(false);

    // State cho Form dữ liệu
    const initialFormState = { id: "", name: "", description: "" };
    const [formData, setFormData] = useState(initialFormState);

    // ---------------- API: LẤY DANH SÁCH TOPIC (PAGINATED) ----------------
    const fetchTopics = async (page = 1, size = 10) => {
        setLoading(true);
        try {
            const response = await fetch(`${API_URL}/topic/paginated?PageIndex=${page}&PageSize=${size}`, {
                method: "GET",
                credentials: "include",
                headers: { "Content-Type": "application/json" }
            });

            if (response.ok) {
                const result = await response.json();
                if (result.success && result.data) {
                    setTopics(result.data.items || []);
                    setCurrentPage(result.data.currentPage || 1);
                    setTotalPages(result.data.totalPages || 1);
                    setTotalCount(result.data.totalCount || 0);
                }
            } else {
                setError("Cannot load topics.");
            }
        } catch (err) {
            setError("Server connection error.");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchTopics(currentPage, pageSize);
    }, [currentPage, pageSize]);

    // ---------------- HANDLERS: MỞ MODAL ----------------
    const handleAddClick = () => {
        setFormData(initialFormState);
        setIsEditMode(false);
        setShowFormModal(true);
    };

    const handleEditClick = (topic) => {
        // Topic đơn giản nên ta lấy luôn dữ liệu từ dòng hiện tại trên bảng, không cần gọi API chi tiết
        setFormData({
            id: topic.id,
            name: topic.name,
            description: topic.description
        });
        setIsEditMode(true);
        setShowFormModal(true);
    };

    const handleDeleteClick = (topic) => {
        setSelectedTopic(topic);
        setShowDeleteModal(true);
    };

    // ---------------- SUBMIT FORM (GỘP CHUNG POST & PUT) ----------------
    const submitForm = async (e) => {
        e.preventDefault();
        setIsActionLoading(true);

        const method = isEditMode ? "PUT" : "POST";
        const url = isEditMode ? `${API_URL}/topic/${formData.id}` : `${API_URL}/topic`;

        try {
            const response = await fetch(url, {
                method: method,
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    name: formData.name,
                    description: formData.description
                })
            });

            const result = await response.json();

            if (response.ok && result.success) {
                setShowFormModal(false);
                
                if (isEditMode) {
                    // 🔥 PUT: Cập nhật dòng hiện tại bằng cục data trả về
                    setTopics(prev => prev.map(t => t.id === formData.id ? result.data : t));
                } else {
                    // 🔥 POST: Thêm phần tử mới lên đầu mảng
                    if (currentPage === 1) {
                        setTopics(prev => [result.data, ...prev].slice(0, pageSize));
                        setTotalCount(prev => {
                            const newCount = prev + 1;
                            setTotalPages(Math.ceil(newCount / pageSize));
                            return newCount;
                        });
                    } else {
                        // Nếu đang ở trang khác, lùi về trang 1
                        setTimeout(() => setCurrentPage(1), 600); 
                    }
                }
            } else {
                alert(`Failed to ${isEditMode ? 'update' : 'add'} topic: ${result.message}`);
            }
        } catch (err) {
            console.error("Lỗi:", err);
            alert("Server connection error.");
        } finally {
            setIsActionLoading(false);
        }
    };

    // ---------------- XÓA DỮ LIỆU (CQRS OPTIMISTIC DELETE) ----------------
    const confirmDeleteTopic = async () => {
        setIsActionLoading(true);
        try {
            const response = await fetch(`${API_URL}/topic/${selectedTopic.id}`, {
                method: "DELETE",
                credentials: "include",
            });

            if (response.ok) {
                setShowDeleteModal(false);
                setTopics(prev => prev.filter(t => t.id !== selectedTopic.id));
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
                alert("Failed to delete topic.");
            }
        } catch (err) {
            alert("Server connection error.");
        } finally {
            setIsActionLoading(false);
        }
    };

    // ---------------- LỌC DỮ LIỆU HIỂN THỊ TRÊN CLIENT ----------------
    const filteredTopics = topics.filter(topic => 
        topic.name.toLowerCase().includes(searchQuery.toLowerCase()) || 
        (topic.description && topic.description.toLowerCase().includes(searchQuery.toLowerCase()))
    );

    const startEntry = (currentPage - 1) * pageSize + 1;
    const endEntry = Math.min(currentPage * pageSize, totalCount);

    return (
        <div className="container-fluid px-4 pt-4">
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h2 className="fw-bold m-0 text-primary">Topic Management</h2>
                <button className="btn btn-primary fw-bold shadow-sm" onClick={handleAddClick}>
                    <i className="fas fa-plus me-2"></i> Add Topic
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
                                onChange={(e) => {
                                    setPageSize(Number(e.target.value));
                                    setCurrentPage(1);
                                }}
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
                                    type="text" 
                                    className="form-control" 
                                    placeholder="Search topic..." 
                                    value={searchQuery}
                                    onChange={(e) => setSearchQuery(e.target.value)}
                                />
                            </div>
                        </div>
                    </div>

                    {/* TABLE */}
                    {loading ? (
                        <div className="text-center my-5">
                            <div className="spinner-border text-primary" role="status"></div>
                            <p className="mt-2 text-muted">Loading data...</p>
                        </div>
                    ) : (
                        <div className="table-responsive">
                            <table className="table table-bordered table-hover align-middle">
                                <thead className="table-light text-center">
                                    <tr>
                                        <th style={{ width: '5%' }}>No.</th>
                                        <th style={{ width: '30%' }}>Name</th>
                                        <th style={{ width: '50%' }}>Description</th>
                                        <th style={{ width: '15%' }}>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {filteredTopics.length > 0 ? (
                                        filteredTopics.map((topic, index) => (
                                            <tr key={topic.id}>
                                                <td className="text-center text-muted">
                                                    {(currentPage - 1) * pageSize + index + 1}
                                                </td>
                                                <td className="fw-bold text-dark">{topic.name}</td>
                                                <td className="text-muted">{topic.description}</td>
                                                <td className="text-center">
                                                    <button className="btn btn-sm btn-primary me-2" onClick={() => handleEditClick(topic)} title="Edit">
                                                        <i className="fas fa-edit"></i>
                                                    </button>
                                                    <button className="btn btn-sm btn-danger" onClick={() => handleDeleteClick(topic)} title="Delete">
                                                        <i className="fas fa-trash-alt"></i>
                                                    </button>
                                                </td>
                                            </tr>
                                        ))
                                    ) : (
                                        <tr><td colSpan="4" className="text-center text-muted py-4">No entries found.</td></tr>
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

            {/* MODAL THÊM VÀ SỬA (ADD & EDIT) */}
            {showFormModal && (
                <>
                    <div className="modal fade show d-block" tabIndex="-1" style={{ backgroundColor: 'rgba(0,0,0,0.6)', zIndex: 1060 }}>
                        <div className="modal-dialog modal-dialog-centered">
                            <div className="modal-content shadow-lg border-0">
                                <div className="modal-header bg-primary text-white">
                                    <h5 className="modal-title fw-bold">
                                        <i className={`fas fa-${isEditMode ? 'edit' : 'plus-circle'} me-2`}></i>
                                        {isEditMode ? 'Update Topic' : 'Add New Topic'}
                                    </h5>
                                    <button type="button" className="btn-close btn-close-white" onClick={() => setShowFormModal(false)}></button>
                                </div>
                                
                                <div className="modal-body p-4 bg-light">
                                    <form id="topicForm" onSubmit={submitForm}>
                                        <div className="mb-3">
                                            <label className="form-label fw-bold">Topic Name <span className="text-danger">*</span></label>
                                            <input type="text" className="form-control fw-bold fs-5" value={formData.name} onChange={(e) => setFormData({...formData, name: e.target.value})} placeholder="Enter topic name..." required />
                                        </div>
                                        <div className="mb-3">
                                            <label className="form-label fw-bold">Description</label>
                                            <textarea className="form-control" rows="4" value={formData.description} onChange={(e) => setFormData({...formData, description: e.target.value})} placeholder="Brief summary of the topic..." required></textarea>
                                        </div>
                                    </form>
                                </div>
                                <div className="modal-footer bg-light border-top-0">
                                    <button type="button" className="btn btn-secondary px-4" onClick={() => setShowFormModal(false)} disabled={isActionLoading}>Cancel</button>
                                    <button type="submit" form="topicForm" className="btn btn-primary px-4 fw-bold" disabled={isActionLoading}>
                                        {isActionLoading ? <><span className="spinner-border spinner-border-sm me-2"></span>Saving...</> : <><i className="fas fa-save me-2"></i>{isEditMode ? 'Save Changes' : 'Create Topic'}</>}
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="modal-backdrop fade show"></div>
                </>
            )}

            {/* DELETE MODAL */}
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
                                    <p>Are you sure you want to delete topic: <strong>{selectedTopic?.name}</strong>?</p>
                                </div>
                                <div className="modal-footer border-0 pt-0">
                                    <button type="button" className="btn btn-light" onClick={() => setShowDeleteModal(false)}>Cancel</button>
                                    <button type="button" className="btn btn-danger" onClick={confirmDeleteTopic} disabled={isActionLoading}>
                                        {isActionLoading ? <span className="spinner-border spinner-border-sm"></span> : "Delete"}
                                    </button>
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

export default TopicManagePage;