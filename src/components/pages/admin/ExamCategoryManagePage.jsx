import React, { useState, useEffect } from 'react';
import { API_URL } from "../../../config";

const ExamCategoryManagePage = () => {
    // ---------------- STATE QUẢN LÝ DỮ LIỆU BẢNG ----------------
    const [categories, setCategories] = useState([]); // Mảng gốc lưu TOÀN BỘ dữ liệu từ API
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    // Phân trang & Tìm kiếm (Client-side)
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [searchQuery, setSearchQuery] = useState("");

    // ---------------- STATE QUẢN LÝ MODAL (POPUP) ----------------
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    
    // Gộp Add và Edit vào chung 1 Modal
    const [showFormModal, setShowFormModal] = useState(false);
    const [isEditMode, setIsEditMode] = useState(false);

    const [selectedCategory, setSelectedCategory] = useState(null);
    const [isActionLoading, setIsActionLoading] = useState(false);
    const [uploadingImage, setUploadingImage] = useState(false);

    // State cho Form dữ liệu
    const initialFormState = { id: "", name: "", description: "", imageUrl: "" };
    const [formData, setFormData] = useState(initialFormState);

    // ---------------- HÀM TIỆN ÍCH HIỂN THỊ ẢNH ----------------
    const getFullImageUrl = (path) => {
        if (!path) return null;
        if (path.startsWith('http')) return path;
        const cleanPath = path.startsWith('/') ? path : `/${path}`;
        return `${API_URL}${cleanPath}`;
    };

    // ---------------- API: LẤY TOÀN BỘ DANH SÁCH (CHỈ GỌI 1 LẦN) ----------------
    const fetchCategories = async () => {
        setLoading(true);
        try {
            const response = await fetch(`${API_URL}/ExamCategory`, {
                method: "GET",
                credentials: "include",
                headers: { "Content-Type": "application/json" }
            });

            if (response.ok) {
                const result = await response.json();
                if (result.success) {
                    setCategories(result.data || []);
                }
            } else {
                setError("Cannot load exam categories.");
            }
        } catch (err) {
            setError("Server connection error.");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchCategories();
    }, []);

    // ---------------- THUẬT TOÁN LỌC VÀ PHÂN TRANG (CLIENT-SIDE) ----------------
    // 1. Lọc theo tên hoặc mô tả
    const filteredCategories = categories.filter(cat => 
        cat.name.toLowerCase().includes(searchQuery.toLowerCase()) || 
        (cat.description && cat.description.toLowerCase().includes(searchQuery.toLowerCase()))
    );

    // 2. Tính toán phân trang từ mảng đã lọc
    const totalCount = filteredCategories.length;
    const totalPages = Math.ceil(totalCount / pageSize) || 1;

    // 3. Cắt mảng (slice) để lấy đúng số phần tử cho trang hiện tại
    const paginatedCategories = filteredCategories.slice(
        (currentPage - 1) * pageSize, 
        currentPage * pageSize
    );

    // 4. Nếu xóa phần tử cuối cùng của trang làm currentPage vượt quá totalPages, tự lùi trang
    useEffect(() => {
        if (currentPage > totalPages && totalPages > 0) {
            setCurrentPage(totalPages);
        }
    }, [totalPages, currentPage]);


    // ---------------- HANDLERS: MỞ MODAL ----------------
    const handleAddClick = () => {
        setFormData(initialFormState);
        setIsEditMode(false);
        setShowFormModal(true);
    };

    const handleEditClick = (category) => {
        setFormData({
            id: category.id,
            name: category.name,
            description: category.description || "",
            imageUrl: category.imageUrl || ""
        });
        setIsEditMode(true);
        setShowFormModal(true);
    };

    const handleDeleteClick = (category) => {
        setSelectedCategory(category);
        setShowDeleteModal(true);
    };

    // ---------------- UPLOAD ẢNH ----------------
    const handleImageUpload = async (e) => {
        const file = e.target.files[0];
        if (!file) return;

        setUploadingImage(true);
        const data = new FormData();
        data.append('file', file); 

        try {
            const response = await fetch(`${API_URL}/uploadmedia/upload-images`, {
                method: 'POST',
                body: data
            });
            const result = await response.json();

            if (response.ok && result.success) {
                setFormData(prev => ({ ...prev, imageUrl: result.data }));
            } else {
                alert(`Upload failed: ${result.message}`);
            }
        } catch (err) {
            alert("Server connection error during upload.");
        } finally {
            setUploadingImage(false);
        }
    };

    // ---------------- SUBMIT FORM (GỘP CHUNG POST & PUT) ----------------
    const submitForm = async (e) => {
        e.preventDefault();
        setIsActionLoading(true);

        const method = isEditMode ? "PUT" : "POST";
        const url = isEditMode ? `${API_URL}/ExamCategory/${formData.id}` : `${API_URL}/ExamCategory`;

        try {
            const response = await fetch(url, {
                method: method,
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    name: formData.name,
                    description: formData.description,
                    imageUrl: formData.imageUrl
                })
            });

            const result = await response.json();

            if (response.ok && result.success) {
                setShowFormModal(false);
                
                if (isEditMode) {
                    // 🔥 PUT CQRS Update: Sửa phần tử trong mảng
                    setCategories(prev => prev.map(c => c.id === formData.id ? result.data : c));
                } else {
                    // 🔥 POST CQRS Update: Ném dữ liệu mới lên đầu mảng
                    setCategories(prev => [result.data, ...prev]);
                    // Đá về trang 1 và clear thanh search (nếu có) để xem dòng mới thêm
                    setCurrentPage(1);
                    setSearchQuery("");
                }
            } else {
                alert(`Failed to ${isEditMode ? 'update' : 'add'} category: ${result.message}`);
            }
        } catch (err) {
            console.error("Lỗi:", err);
            alert("Server connection error.");
        } finally {
            setIsActionLoading(false);
        }
    };

    // ---------------- XÓA DỮ LIỆU (CQRS OPTIMISTIC DELETE) ----------------
    const confirmDeleteCategory = async () => {
        setIsActionLoading(true);
        try {
            const response = await fetch(`${API_URL}/ExamCategory/${selectedCategory.id}`, {
                method: "DELETE",
                credentials: "include",
            });

            if (response.ok) {
                setShowDeleteModal(false);
                // CQRS Update: Lọc bỏ thẳng phần tử ra khỏi mảng gốc
                setCategories(prev => prev.filter(c => c.id !== selectedCategory.id));
            } else {
                alert("Failed to delete category.");
            }
        } catch (err) {
            alert("Server connection error.");
        } finally {
            setIsActionLoading(false);
        }
    };

    const startEntry = (currentPage - 1) * pageSize + 1;
    const endEntry = Math.min(currentPage * pageSize, totalCount);

    return (
        <div className="container-fluid px-4 pt-4">
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h2 className="fw-bold m-0 text-primary">Exam Category Management</h2>
                <button className="btn btn-primary fw-bold shadow-sm" onClick={handleAddClick}>
                    <i className="fas fa-plus me-2"></i> Add Category
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
                                    placeholder="Search category..." 
                                    value={searchQuery}
                                    onChange={(e) => {
                                        setSearchQuery(e.target.value);
                                        setCurrentPage(1); // Gõ tìm kiếm là tự nhảy về trang 1
                                    }}
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
                                        <th style={{ width: '15%' }}>Image</th>
                                        <th style={{ width: '30%' }}>Category Name</th>
                                        <th style={{ width: '35%' }}>Description</th>
                                        <th style={{ width: '15%' }}>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {paginatedCategories.length > 0 ? (
                                        paginatedCategories.map((category, index) => (
                                            <tr key={category.id}>
                                                <td className="text-center text-muted">
                                                    {(currentPage - 1) * pageSize + index + 1}
                                                </td>
                                                <td className="text-center">
                                                    <img 
                                                        src={getFullImageUrl(category.imageUrl)} 
                                                        alt={category.name} 
                                                        className="rounded shadow-sm border"
                                                        style={{ width: "60px", height: "45px", objectFit: "cover" }}
                                                        onError={(e) => { e.target.src = "https://via.placeholder.com/60x45?text=No+Img" }}
                                                    />
                                                </td>
                                                <td className="fw-bold text-dark">{category.name}</td>
                                                <td className="text-muted">{category.description}</td>
                                                <td className="text-center">
                                                    <button className="btn btn-sm btn-primary me-2" onClick={() => handleEditClick(category)} title="Edit">
                                                        <i className="fas fa-edit"></i>
                                                    </button>
                                                    <button className="btn btn-sm btn-danger" onClick={() => handleDeleteClick(category)} title="Delete">
                                                        <i className="fas fa-trash-alt"></i>
                                                    </button>
                                                </td>
                                            </tr>
                                        ))
                                    ) : (
                                        <tr><td colSpan="5" className="text-center text-muted py-4">No entries found.</td></tr>
                                    )}
                                </tbody>
                            </table>
                        </div>
                    )}

                    {/* PAGINATION */}
                    {!loading && totalPages > 0 && (
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
                                <li className={`page-item ${currentPage === totalPages ? 'disabled' : ''}`}>
                                    <button className="page-link" onClick={() => setCurrentPage(prev => Math.min(prev + 1, totalPages))}><i className="fas fa-angle-right"></i></button>
                                </li>
                                <li className={`page-item ${currentPage === totalPages ? 'disabled' : ''}`}>
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
                        <div className="modal-dialog modal-lg modal-dialog-centered">
                            <div className="modal-content shadow-lg border-0">
                                <div className="modal-header bg-primary text-white">
                                    <h5 className="modal-title fw-bold">
                                        <i className={`fas fa-${isEditMode ? 'edit' : 'plus-circle'} me-2`}></i>
                                        {isEditMode ? 'Update Exam Category' : 'Add New Category'}
                                    </h5>
                                    <button type="button" className="btn-close btn-close-white" onClick={() => setShowFormModal(false)}></button>
                                </div>
                                
                                <div className="modal-body p-4 bg-light">
                                    <form id="categoryForm" onSubmit={submitForm}>
                                        <div className="row">
                                            {/* CỘT TRÁI: ẢNH BÌA */}
                                            <div className="col-md-4">
                                                <div className="mb-3">
                                                    <label className="form-label fw-bold">Category Image</label>
                                                    <div className="card border-1 text-center p-3 mb-2" style={{ borderStyle: 'dashed' }}>
                                                        {formData.imageUrl ? (
                                                            <div className="position-relative">
                                                                <img src={getFullImageUrl(formData.imageUrl)} alt="preview" className="img-fluid rounded" style={{ maxHeight: '180px', objectFit: 'cover' }} />
                                                                <button type="button" className="btn btn-sm btn-danger position-absolute top-0 end-0 m-1" onClick={() => setFormData({...formData, imageUrl: ""})}>
                                                                    <i className="fas fa-times"></i>
                                                                </button>
                                                            </div>
                                                        ) : (
                                                            <div className="text-muted py-4">
                                                                <i className="fas fa-image fs-1 mb-2"></i>
                                                                <p className="mb-0 small">No image</p>
                                                            </div>
                                                        )}
                                                    </div>
                                                    <input 
                                                        type="file" 
                                                        className="form-control form-control-sm" 
                                                        accept="image/png, image/jpeg, image/jpg, image/webp" 
                                                        onChange={handleImageUpload} 
                                                        disabled={uploadingImage}
                                                    />
                                                    {uploadingImage && <small className="text-primary mt-1 d-block"><i className="fas fa-spinner fa-spin me-1"></i>Uploading...</small>}
                                                </div>
                                            </div>

                                            {/* CỘT PHẢI: INFO */}
                                            <div className="col-md-8">
                                                <div className="mb-3">
                                                    <label className="form-label fw-bold">Category Name <span className="text-danger">*</span></label>
                                                    <input type="text" className="form-control fw-bold fs-5" value={formData.name} onChange={(e) => setFormData({...formData, name: e.target.value})} placeholder="Enter category name..." required />
                                                </div>
                                                <div className="mb-3">
                                                    <label className="form-label fw-bold">Description</label>
                                                    <textarea className="form-control" rows="5" value={formData.description} onChange={(e) => setFormData({...formData, description: e.target.value})} placeholder="Brief summary of the category..." required></textarea>
                                                </div>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                                <div className="modal-footer bg-light border-top-0">
                                    <button type="button" className="btn btn-secondary px-4" onClick={() => setShowFormModal(false)} disabled={isActionLoading}>Cancel</button>
                                    <button type="submit" form="categoryForm" className="btn btn-primary px-4 fw-bold" disabled={isActionLoading}>
                                        {isActionLoading ? <><span className="spinner-border spinner-border-sm me-2"></span>Saving...</> : <><i className="fas fa-save me-2"></i>{isEditMode ? 'Save Changes' : 'Create Category'}</>}
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
                                    <p>Are you sure you want to delete category: <strong>{selectedCategory?.name}</strong>?</p>
                                </div>
                                <div className="modal-footer border-0 pt-0">
                                    <button type="button" className="btn btn-light" onClick={() => setShowDeleteModal(false)}>Cancel</button>
                                    <button type="button" className="btn btn-danger" onClick={confirmDeleteCategory} disabled={isActionLoading}>
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

export default ExamCategoryManagePage;