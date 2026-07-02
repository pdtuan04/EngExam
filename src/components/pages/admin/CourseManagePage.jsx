import React, { useState, useEffect } from 'react';
import { Editor } from '@tinymce/tinymce-react';
import { API_URL } from "../../../config";

const CourseManagePage = () => {
    // ---------------- STATE QUẢN LÝ DỮ LIỆU BẢNG ----------------
    const [courses, setCourses] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const [topics, setTopics] = useState([]);

    // Phân trang & Tìm kiếm
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [totalCount, setTotalCount] = useState(0);
    const [searchQuery, setSearchQuery] = useState("");

    // ---------------- STATE QUẢN LÝ MODAL (POPUP) ----------------
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [showViewModal, setShowViewModal] = useState(false);
    
    const [showFormModal, setShowFormModal] = useState(false);
    const [isEditMode, setIsEditMode] = useState(false);

    const [selectedCourse, setSelectedCourse] = useState(null);
    const [isActionLoading, setIsActionLoading] = useState(false);
    const [uploadingImage, setUploadingImage] = useState(false);

    // State cho Form dữ liệu
    const initialFormState = {
        id: "", name: "", description: "", content: "", imageUrl: "", 
        topicId: "" 
    };
    const [formData, setFormData] = useState(initialFormState);

    // ---------------- HÀM TIỆN ÍCH HIỂN THỊ ẢNH ----------------
    const getFullImageUrl = (path) => {
        if (!path) return null;
        if (path.startsWith('http')) return path;
        
        const CLOUDFRONT_DOMAIN = "https://d1klycy9voc7ou.cloudfront.net";
        const cleanPath = path.startsWith('/') ? path : `/${path}`;
        return `${CLOUDFRONT_DOMAIN}${cleanPath}`;
    };

    // ---------------- API: LẤY DANH SÁCH TOPIC ----------------
    const fetchTopics = async () => {
        try {
            const response = await fetch(`${API_URL}/topic`, {
                method: "GET",
                credentials: "include",
            });
            if (response.ok) {
                const result = await response.json();
                if (result.success) {
                    setTopics(result.data || []);
                }
            }
        } catch (err) {
            console.error("Lỗi lấy danh sách topic:", err);
        }
    };

    // ---------------- API: LẤY DANH SÁCH COURSE ----------------
    const fetchCourses = async (page = 1, size = 5) => {
        setLoading(true);
        try {
            const response = await fetch(`${API_URL}/course?pageIndex=${page}&pageSize=${size}`, {
                method: "GET",
                credentials: "include",
                headers: { "Content-Type": "application/json" }
            });

            if (response.ok) {
                const json = await response.json();
                setCourses(json.items || []);
                setCurrentPage(json.currentPage || 1);
                setTotalPages(json.totalPages || 1);
                setTotalCount(json.totalCount || 0);
            } else {
                setError("Cannot load courses.");
            }
        } catch (err) {
            setError("Server connection error.");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchTopics();
    }, []); 

    useEffect(() => {
        fetchCourses(currentPage, pageSize);
    }, [currentPage, pageSize]);

    // ---------------- API: LẤY CHI TIẾT COURSE ----------------
    const fetchCourseDetail = async (id) => {
        try {
            const response = await fetch(`${API_URL}/course/${id}`, {
                method: "GET",
                credentials: "include"
            });
            if (response.ok) return await response.json();
            alert("Cannot fetch course details!");
            return null;
        } catch (err) {
            alert("Error connecting to server!");
            return null;
        }
    };

    // ---------------- HANDLERS: MỞ MODAL ----------------
    const handleAddClick = () => {
        setFormData(initialFormState);
        setIsEditMode(false);
        setShowFormModal(true);
    };

    const handleEditClick = async (id) => {
        const data = await fetchCourseDetail(id);
        if (data) {
            setFormData(data);
            setIsEditMode(true);
            setShowFormModal(true);
        }
    };

    const handleViewClick = async (id) => {
        const data = await fetchCourseDetail(id);
        if (data) {
            setSelectedCourse(data);
            setShowViewModal(true);
        }
    };

    const handleDeleteClick = (course) => {
        setSelectedCourse(course);
        setShowDeleteModal(true);
    };

    // ---------------- UPLOAD ẢNH BÌA BÊN TRÁI ----------------
    const handleImageUpload = async (e) => {
        const file = e.target.files[0];
        if (!file) return;

        setUploadingImage(true);
        const data = new FormData();
        data.append('file', file); 

        try {
            const response = await fetch(`${API_URL}/file/upload-images`, {
                method: 'POST',
                body: data
            });
            const result = await response.json();

            if (response.ok && result.success) {
                setFormData(prev => ({ ...prev, imageUrl: result.data.filePath }));
            } else {
                alert(`Upload failed: ${result.message}`);
            }
        } catch (err) {
            alert("Server connection error during upload.");
        } finally {
            setUploadingImage(false);
        }
    };

    const submitForm = async (e) => {
        e.preventDefault();
        setIsActionLoading(true);

        const method = isEditMode ? "PUT" : "POST";
        const url = isEditMode ? `${API_URL}/course/${formData.id}` : `${API_URL}/course`;

        try {
            const response = await fetch(url, {
                method: method,
                credentials: "include",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    id: formData.id || undefined, 
                    name: formData.name,
                    description: formData.description,
                    content: formData.content,
                    imageUrl: formData.imageUrl,
                    topicId: formData.topicId
                })
            });

            if (response.ok) {
                setShowFormModal(false);
                
                if (isEditMode) {
                    setCourses(prev => prev.map(c => c.id === formData.id ? formData : c));
                } else {
                    const createdCourse = await response.json();
                    
                    if (currentPage === 1) {
                        setCourses(prev => [createdCourse, ...prev].slice(0, pageSize));
                        
                        setTotalCount(prev => {
                            const newCount = prev + 1;
                            setTotalPages(Math.ceil(newCount / pageSize));
                            return newCount;
                        });
                    } else {
                        setTimeout(() => {
                            setCurrentPage(1); 
                        }, 600); 
                    }
                }
            } else {
                alert(`Failed to ${isEditMode ? 'update' : 'add'} course.`);
            }
        } catch (err) {
            console.error("Lỗi:", err);
            alert("Server connection error.");
        } finally {
            setIsActionLoading(false);
        }
    };

    const confirmDeleteCourse = async () => {
        setIsActionLoading(true);
        try {
            const response = await fetch(`${API_URL}/course/${selectedCourse.id}`, {
                method: "DELETE",
                credentials: "include",
            });

            if (response.ok) {
                setShowDeleteModal(false);
                setCourses(prev => prev.filter(c => c.id !== selectedCourse.id));
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
                alert("Failed to delete course.");
            }
        } catch (err) {
            alert("Server connection error.");
        } finally {
            setIsActionLoading(false);
        }
    };

    // ---------------- LỌC DỮ LIỆU HIỂN THỊ ----------------
    const filteredCourses = courses.filter(course => 
        course.name.toLowerCase().includes(searchQuery.toLowerCase()) || 
        course.description.toLowerCase().includes(searchQuery.toLowerCase())
    );

    const startEntry = (currentPage - 1) * pageSize + 1;
    const endEntry = Math.min(currentPage * pageSize, totalCount);

    return (
        <div className="container-fluid px-4 pt-4">
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h2 className="fw-bold m-0 text-primary">Course Management</h2>
                <button className="btn btn-primary fw-bold shadow-sm" onClick={handleAddClick}>
                    <i className="fas fa-plus me-2"></i> Add Course
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
                                    placeholder="Search..." 
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
                                        <th style={{ width: '15%' }}>Image</th>
                                        <th style={{ width: '25%' }}>Name</th>
                                        <th style={{ width: '35%' }}>Description</th>
                                        <th style={{ width: '20%' }}>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {filteredCourses.length > 0 ? (
                                        filteredCourses.map((course, index) => (
                                            <tr key={course.id}>
                                                <td className="text-center text-muted">
                                                    {(currentPage - 1) * pageSize + index + 1}
                                                </td>
                                                <td className="text-center">
                                                    <img 
                                                        src={getFullImageUrl(course.imageUrl)} 
                                                        alt={course.name} 
                                                        className="rounded shadow-sm"
                                                        style={{ width: "60px", height: "45px", objectFit: "cover" }}
                                                        onError={(e) => { e.target.src = "https://via.placeholder.com/60x45?text=No+Image" }}
                                                    />
                                                </td>
                                                <td className="fw-bold text-dark">{course.name}</td>
                                                <td className="text-muted">{course.description}</td>
                                                <td className="text-center">
                                                    <button className="btn btn-sm btn-info text-white me-1" onClick={() => handleViewClick(course.id)} title="View">
                                                        <i className="fas fa-eye"></i>
                                                    </button>
                                                    <button className="btn btn-sm btn-primary me-1" onClick={() => handleEditClick(course.id)} title="Edit">
                                                        <i className="fas fa-edit"></i>
                                                    </button>
                                                    <button className="btn btn-sm btn-danger" onClick={() => handleDeleteClick(course)} title="Delete">
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

            {showFormModal && (
                <>
                    <div className="modal fade show d-block" tabIndex="-1" style={{ backgroundColor: 'rgba(0,0,0,0.6)', zIndex: 1060 }}>
                        <div className="modal-dialog modal-xl modal-dialog-scrollable">
                            <div className="modal-content shadow-lg border-0">
                                <div className="modal-header bg-primary text-white">
                                    <h5 className="modal-title fw-bold">
                                        <i className={`fas fa-${isEditMode ? 'edit' : 'plus-circle'} me-2`}></i>
                                        {isEditMode ? 'Update Course' : 'Add New Course'}
                                    </h5>
                                    <button type="button" className="btn-close btn-close-white" onClick={() => setShowFormModal(false)}></button>
                                </div>
                                
                                <div className="modal-body p-4 bg-light">
                                    <form id="courseForm" onSubmit={submitForm}>
                                        <div className="row">
                                            <div className="col-md-3">
                                                <div className="mb-3">
                                                    <label className="form-label fw-bold">Cover Image</label>
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
                                                                <p className="mb-0 small">No image uploaded</p>
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

                                                <div className="mb-3 mt-4">
                                                    <label className="form-label fw-bold">Topic <span className="text-danger">*</span></label>
                                                    <select 
                                                        className="form-select" 
                                                        value={formData.topicId} 
                                                        onChange={(e) => setFormData({...formData, topicId: e.target.value})} 
                                                        required
                                                    >
                                                        <option value="" disabled>-- Select a Topic --</option>
                                                        {topics.map(topic => (
                                                            <option key={topic.id} value={topic.id}>
                                                                {topic.name}
                                                            </option>
                                                        ))}
                                                    </select>
                                                </div>
                                            </div>

                                            <div className="col-md-9">
                                                <div className="mb-3">
                                                    <label className="form-label fw-bold">Course Name <span className="text-danger">*</span></label>
                                                    <input type="text" className="form-control fw-bold fs-5" value={formData.name} onChange={(e) => setFormData({...formData, name: e.target.value})} placeholder="Enter course name..." required />
                                                </div>
                                                <div className="mb-3">
                                                    <label className="form-label fw-bold">Description <span className="text-danger">*</span></label>
                                                    <textarea className="form-control" rows="2" value={formData.description} onChange={(e) => setFormData({...formData, description: e.target.value})} placeholder="Brief summary of the course..." required></textarea>
                                                </div>
                                                <div className="mb-3">
                                                    <label className="form-label fw-bold">Detailed Content <span className="text-danger">*</span></label>
                                                    
                                                    {/* TRÌNH SOẠN THẢO TINYMCE */}
                                                    <Editor
                                                        apiKey={import.meta.env.VITE_TINYMCE_API_KEY} 
                                                        value={formData.content}
                                                        onEditorChange={(newContent) => setFormData({...formData, content: newContent})}
                                                        init={{
                                                            height: 400,
                                                            menubar: true,
                                                            plugins: [
                                                                'advlist', 'autolink', 'lists', 'link', 'image', 'charmap', 'preview',
                                                                'anchor', 'searchreplace', 'visualblocks', 'code', 'fullscreen',
                                                                'insertdatetime', 'media', 'table', 'code', 'help', 'wordcount'
                                                            ],
                                                            toolbar: 'undo redo | blocks | ' +
                                                                'bold italic forecolor | alignleft aligncenter ' +
                                                                'alignright alignjustify | bullist numlist outdent indent | ' +
                                                                'removeformat | image | help',
                                                            content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:16px }',
                                                            
                                                            // 🔥 SỬA LẠI ĐƯỜNG DẪN API Ở ĐÂY CHUẨN XÁC
                                                            images_upload_handler: (blobInfo, progress) => new Promise(async (resolve, reject) => {
                                                                const data = new FormData();
                                                                data.append('file', blobInfo.blob(), blobInfo.filename());

                                                                try {
                                                                    // 🔥 ĐÃ ĐỔI THÀNH /file/upload-images
                                                                    const response = await fetch(`${API_URL}/file/upload-images`, {
                                                                        method: 'POST',
                                                                        body: data
                                                                    });
                                                                    const result = await response.json();
                                                                    
                                                                    if (response.ok && result.success) {
                                                                        const imgUrl = result.data.fileUrl.startsWith('http') 
                                                                            ? result.data.fileUrl 
                                                                            : `https://${result.data.fileUrl}`;
                                                                        resolve(imgUrl);
                                                                    } else {
                                                                        reject(result.message || 'Lỗi tải ảnh');
                                                                    }
                                                                } catch (err) {
                                                                    reject('Lỗi kết nối tới server');
                                                                }
                                                            })
                                                        }}
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                                <div className="modal-footer bg-light border-top-0">
                                    <button type="button" className="btn btn-secondary px-4" onClick={() => setShowFormModal(false)} disabled={isActionLoading}>Cancel</button>
                                    <button type="submit" form="courseForm" className="btn btn-primary px-4 fw-bold" disabled={isActionLoading}>
                                        {isActionLoading ? <><span className="spinner-border spinner-border-sm me-2"></span>Saving...</> : <><i className="fas fa-save me-2"></i>{isEditMode ? 'Save Changes' : 'Create Course'}</>}
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="modal-backdrop fade show"></div>
                </>
            )}

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
                                    <p>Are you sure you want to delete course: <strong>{selectedCourse?.name}</strong>?</p>
                                </div>
                                <div className="modal-footer border-0 pt-0">
                                    <button type="button" className="btn btn-light" onClick={() => setShowDeleteModal(false)}>Cancel</button>
                                    <button type="button" className="btn btn-danger" onClick={confirmDeleteCourse}>Delete</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="modal-backdrop fade show"></div>
                </>
            )}

            {showViewModal && selectedCourse && (
                <>
                    <div className="modal fade show d-block" tabIndex="-1">
                        <div className="modal-dialog modal-lg modal-dialog-centered">
                            <div className="modal-content">
                                <div className="modal-header bg-info text-white">
                                    <h5 className="modal-title"><i className="fas fa-info-circle me-2"></i> Course Details</h5>
                                    <button type="button" className="btn-close btn-close-white" onClick={() => setShowViewModal(false)}></button>
                                </div>
                                <div className="modal-body">
                                    <div className="text-center mb-3">
                                        <img src={getFullImageUrl(selectedCourse.imageUrl)} alt="Course" className="img-fluid rounded shadow-sm" style={{ maxHeight: "200px" }} />
                                    </div>
                                    <h4 className="fw-bold text-center">{selectedCourse.name}</h4>
                                    <p className="text-muted text-center">{selectedCourse.description}</p>
                                    <hr />
                                    <h6 className="fw-bold">Content:</h6>
                                    <div className="p-3 bg-light rounded" dangerouslySetInnerHTML={{ __html: selectedCourse.content }}></div>
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

export default CourseManagePage;