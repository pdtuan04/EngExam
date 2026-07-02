import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { API_URL } from "../../../config";
const ExamHistoryPage = () => {
    const navigate = useNavigate();
    const [history, setHistory] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const [pagination, setPagination] = useState({
        currentPage: 1, totalPages: 1, pageSize: 5, totalCount: 0, hasPrevious: false, hasNext: false,
    });

    const fetchHistory = async (page = 1) => {
        setLoading(true);
        try {
            const response = await fetch(`${API_URL}/examresult/paginated-user-exam-result/?PageIndex=${page}&PageSize=${pagination.pageSize}`, {
                credentials: "include" 
            });

            if (response.status === 401) {
                navigate('/login');
                return;
            }

            const result = await response.json();
            if (result.success) {
                setHistory(result.data.items);
                setPagination({
                    currentPage: result.data.currentPage,
                    totalPages: result.data.totalPages,
                    pageSize: result.data.pageSize,
                    totalCount: result.data.totalCount,
                    hasPrevious: result.data.hasPrevious,
                    hasNext: result.data.hasNext,
                });
            } else {
                setError(result.message);
            }
        } catch (err) {
            setError('Lỗi kết nối tới máy chủ');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchHistory(1);
    }, []);

    const formatDate = (dateString) => {
        const date = new Date(dateString);
        return date.toLocaleString('vi-VN');
    };

    return (
        <div className="container mt-5 pt-4">
            <h2 className="fw-bold mb-4">Lịch sử làm bài</h2>

            {loading && <div className="text-center my-4"><div className="spinner-border text-primary"></div></div>}
            {error && <div className="alert alert-danger">{error}</div>}

            {!loading && history.length === 0 ? (
                <div className="text-center text-muted mt-5 p-5 bg-light rounded">
                    <h5>Bạn chưa có lịch sử làm bài nào.</h5>
                </div>
            ) : (
                <div className="card shadow-sm border-0">
                    <div className="table-responsive">
                        <table className="table table-hover align-middle mb-0">
                            <thead className="table-light">
                                <tr>
                                    <th>STT</th>
                                    <th>Thời gian hoàn thành</th>
                                    <th>Điểm số</th>
                                    <th>Hành động</th>
                                </tr>
                            </thead>
                            <tbody>
                                {history.map((item, index) => (
                                    <tr key={item.id}>
                                        <td>{(pagination.currentPage - 1) * pagination.pageSize + index + 1}</td>
                                        <td>{formatDate(item.completeAt)}</td>
                                        <td><span className="badge bg-success fs-6">{item.score} Điểm</span></td>
                                        <td>
                                            <button 
                                                className="btn btn-sm btn-outline-primary"
                                                onClick={() => navigate(`/exam-history/${item.id}`)}
                                            >
                                                Xem chi tiết
                                            </button>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                </div>
            )}

            {/* 👇 Đã xóa điều kiện ẩn, thanh phân trang sẽ luôn hiển thị */}
            <div className="d-flex justify-content-center mt-4 mb-5">
                <button 
                    className="btn btn-outline-secondary me-2" 
                    disabled={!pagination.hasPrevious} 
                    onClick={() => fetchHistory(pagination.currentPage - 1)}
                >
                    Trang trước
                </button>
                
                <span className="align-self-center mx-3 fw-semibold">
                    Trang {pagination.currentPage} / {pagination.totalPages} 
                    <span className="text-muted ms-1 fw-normal">(Tổng: {pagination.totalCount})</span>
                </span>
                
                <button 
                    className="btn btn-outline-secondary ms-2" 
                    disabled={!pagination.hasNext} 
                    onClick={() => fetchHistory(pagination.currentPage + 1)}
                >
                    Trang sau
                </button>
            </div>
        </div>
    );
};

export default ExamHistoryPage;