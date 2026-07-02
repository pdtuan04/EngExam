import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { API_URL } from "../../../config";
const PracticeListPage = () => {
  // Lấy topicId từ URL
  const { topicId } = useParams();
  const navigate = useNavigate();

  // Quản lý state
  const [practices, setPractices] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  // Quản lý phân trang
  const [pagination, setPagination] = useState({
    currentPage: 1,
    totalPages: 1,
    pageSize: 9, // Số chia hết cho 3 để lên grid cho đẹp
    totalCount: 0,
    hasPrevious: false,
    hasNext: false,
  });

  // Hàm gọi API
  const fetchPractices = async (page = 1) => {
    setLoading(true);
    setError(null);
    
    try {
      // Đã cập nhật đúng Endpoint API mới của bạn
      const url = `${API_URL}/practice/paginated-topic?PageIndex=${page}&PageSize=${pagination.pageSize}&topicId=${topicId}`;
      const response = await fetch(url);
      
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }

      const result = await response.json();

      if (result.success) {
        setPractices(result.data.items);
        setPagination({
          currentPage: result.data.currentPage,
          totalPages: result.data.totalPages,
          pageSize: result.data.pageSize,
          totalCount: result.data.totalCount,
          hasPrevious: result.data.hasPrevious,
          hasNext: result.data.hasNext,
        });
      } else {
        setError(result.message || 'Lỗi khi tải dữ liệu bài tập');
      }
    } catch (err) {
      setError('Đã xảy ra lỗi khi kết nối tới server.');
      console.error("Fetch error:", err);
    } finally {
      setLoading(false);
    }
  };

  // Gọi API mỗi khi component render HOẶC khi topicId trên URL thay đổi
  useEffect(() => {
    if (topicId) {
      fetchPractices(1);
    }
  }, [topicId]);

  const handlePageChange = (newPage) => {
    fetchPractices(newPage);
  };

  const handleStartPractice = (practiceId) => {
    navigate(`/practice/${practiceId}`);
  };

  return (
    <div className="container mt-5 pt-4">
      {/* Tiêu đề & Nút quay lại */}
      <div className="d-flex justify-content-between align-items-center mb-4">
        <div>
          <h2 className="fw-bold text-success mb-0">Danh sách Bài Luyện Tập</h2>
          <p className="text-muted mt-1">Hoàn thành các bài tập để nắm vững chủ đề này</p>
        </div>
        <button 
          className="btn btn-outline-secondary"
          onClick={() => navigate('/topic-practice')}
        >
          <i className="bi bi-arrow-left me-2"></i> Quay lại Chủ đề
        </button>
      </div>

      {/* Hiệu ứng Loading */}
      {loading && (
        <div className="d-flex justify-content-center my-5">
          <div className="spinner-border text-success" role="status">
            <span className="visually-hidden">Loading...</span>
          </div>
        </div>
      )}
      
      {/* Thông báo lỗi */}
      {error && (
        <div className="alert alert-danger text-center shadow-sm" role="alert">
          <i className="bi bi-exclamation-triangle-fill me-2"></i> {error}
        </div>
      )}

      {/* Danh sách Cards Bài tập */}
      {!loading && !error && practices.length === 0 ? (
        <div className="text-center text-muted my-5 p-5 bg-light rounded-3 border">
          <i className="bi bi-folder-x display-1 mb-3 text-secondary"></i>
          <h5>Chưa có bài luyện tập nào trong chủ đề này.</h5>
          <p>Vui lòng quay lại sau nhé!</p>
        </div>
      ) : (
        <>
          <div className="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
            {practices.map((practice) => (
              <div className="col" key={practice.id}>
                <div className="card h-100 shadow-sm border-0 border-top border-success border-4">
                  <div className="card-body d-flex flex-column">
                    {/* Chú ý: Đã đổi thành practice.title theo đúng JSON của bạn */}
                    <h5 className="card-title fw-bold text-dark">{practice.title}</h5>
                    <p className="card-text text-muted flex-grow-1 mt-2">
                      {practice.description || 'Không có mô tả chi tiết.'}
                    </p>
                    
                    <button 
                      className="btn btn-success mt-4 w-100 fw-semibold"
                      onClick={() => handleStartPractice(practice.id)}
                    >
                      Vào làm bài <i className="bi bi-play-circle ms-1"></i>
                    </button>
                  </div>
                </div>
              </div>
            ))}
          </div>

          {/* Phân trang (Pagination) */}
          <div className="d-flex justify-content-center mt-5">
            <nav aria-label="Page navigation">
              <ul className="pagination">
                <li className={`page-item ${!pagination.hasPrevious ? 'disabled' : ''}`}>
                  <button className="page-link text-success" onClick={() => handlePageChange(pagination.currentPage - 1)}>
                    Trang trước
                  </button>
                </li>
                <li className="page-item disabled">
                  <span className="page-link text-dark bg-light">
                    Trang {pagination.currentPage} / {pagination.totalPages}
                  </span>
                </li>
                <li className={`page-item ${!pagination.hasNext ? 'disabled' : ''}`}>
                  <button className="page-link text-success" onClick={() => handlePageChange(pagination.currentPage + 1)}>
                    Trang tiếp
                  </button>
                </li>
              </ul>
            </nav>
          </div>
        </>
      )}
    </div>
  );
};

export default PracticeListPage;