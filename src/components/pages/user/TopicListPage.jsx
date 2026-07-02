import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { API_URL } from "../../../config";
const TopicListPage = () => {
  const navigate = useNavigate();
  
  // Quản lý state cho dữ liệu
  const [topics, setTopics] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  // Quản lý state cho phân trang
  const [pagination, setPagination] = useState({
    currentPage: 1,
    totalPages: 1,
    pageSize: 9, // Nên để số chia hết cho 3 (ví dụ 9) để hiển thị grid 3 cột cho đẹp
    totalCount: 0,
    hasPrevious: false,
    hasNext: false,
  });

  // Hàm gọi API lấy dữ liệu
  const fetchTopics = async (page = 1) => {
    setLoading(true);
    setError(null);
    
    try {
      const response = await fetch(`${API_URL}/topic/paginated?PageIndex=${page}&PageSize=${pagination.pageSize}`);
      
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }

      const result = await response.json();

      if (result.success) {
        setTopics(result.data.items);
        setPagination({
          currentPage: result.data.currentPage,
          totalPages: result.data.totalPages,
          pageSize: result.data.pageSize,
          totalCount: result.data.totalCount,
          hasPrevious: result.data.hasPrevious,
          hasNext: result.data.hasNext,
        });
      } else {
        setError(result.message || 'Lỗi khi tải dữ liệu');
      }
    } catch (err) {
      setError('Đã xảy ra lỗi khi kết nối tới server.');
      console.error("Fetch error:", err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchTopics(1);
  }, []);

  // Xử lý chuyển trang
  const handlePageChange = (newPage) => {
    fetchTopics(newPage);
  };

  // Xử lý khi click vào 1 Card Chủ đề
  const handleTopicClick = (topicId) => {
    // Chuyển hướng sang trang chi tiết luyện tập và truyền ID của topic lên URL
    navigate(`/topic-practice/${topicId}`);
  };

  return (
    <div className="container mt-5 pt-4">
      <div className="row mb-4 text-center">
        <h2 className="fw-bold text-primary">Danh sách Chủ đề Luyện Tập</h2>
        <p className="text-muted">Chọn một chủ đề bên dưới để bắt đầu làm bài tập</p>
      </div>

      {/* Hiển thị trạng thái Loading hoặc Error */}
      {loading && (
        <div className="d-flex justify-content-center my-5">
          <div className="spinner-border text-primary" role="status">
            <span className="visually-hidden">Loading...</span>
          </div>
        </div>
      )}
      
      {error && (
        <div className="alert alert-danger text-center" role="alert">
          {error}
        </div>
      )}

      {/* Hiển thị danh sách Topic dạng Grid Cards */}
      {!loading && !error && topics.length === 0 ? (
        <div className="text-center text-muted my-5">
          <h5>Chưa có chủ đề nào được tạo.</h5>
        </div>
      ) : (
        <>
          <div className="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
            {topics.map((topic, index) => (
              <div className="col" key={topic.id || index}>
                <div 
                  className="card h-100 shadow-sm border-0 topic-card"
                  style={{ cursor: 'pointer', transition: 'transform 0.2s, box-shadow 0.2s' }}
                  onClick={() => handleTopicClick(topic.id)}
                  // Thêm hiệu ứng hover bằng React events (nếu bạn không dùng file CSS ngoài)
                  onMouseEnter={(e) => {
                    e.currentTarget.style.transform = 'translateY(-5px)';
                    e.currentTarget.classList.add('shadow');
                  }}
                  onMouseLeave={(e) => {
                    e.currentTarget.style.transform = 'translateY(0)';
                    e.currentTarget.classList.remove('shadow');
                  }}
                >
                  <div className="card-body d-flex flex-column">
                    <div className="mb-3 text-primary">
                      {/* Có thể thêm icon ở đây nếu muốn */}
                      <i className="bi bi-book fs-1"></i> 
                    </div>
                    <h5 className="card-title fw-bold">{topic.name}</h5>
                    <p className="card-text text-muted flex-grow-1">
                      {topic.description || 'Không có mô tả cho chủ đề này.'}
                    </p>
                    <div className="mt-auto d-flex justify-content-between align-items-center">
                      <span className="badge bg-light text-primary border border-primary">
                        Bắt đầu luyện tập
                      </span>
                    </div>
                  </div>
                </div>
              </div>
            ))}
          </div>

          {/* Phân trang (Bootstrap Pagination) */}
          <div className="d-flex justify-content-center mt-5">
            <nav aria-label="Page navigation">
              <ul className="pagination">
                <li className={`page-item ${!pagination.hasPrevious ? 'disabled' : ''}`}>
                  <button 
                    className="page-link" 
                    onClick={() => handlePageChange(pagination.currentPage - 1)}
                  >
                    Trang trước
                  </button>
                </li>
                
                <li className="page-item disabled">
                  <span className="page-link text-dark">
                    Trang {pagination.currentPage} / {pagination.totalPages}
                  </span>
                </li>

                <li className={`page-item ${!pagination.hasNext ? 'disabled' : ''}`}>
                  <button 
                    className="page-link" 
                    onClick={() => handlePageChange(pagination.currentPage + 1)}
                  >
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

export default TopicListPage;