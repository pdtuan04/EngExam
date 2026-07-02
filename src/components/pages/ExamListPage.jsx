import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { API_URL } from "../../config";
function ExamListPage() {
    const { categoryId } = useParams();
    const [exams, setExams] = useState([]);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();
    useEffect(() => {
        fetch(`${API_URL}/Exam/exam-list-${categoryId}`)
            .then(res => res.json())
            .then(json => {
                setExams(json.data);
                setLoading(false);
            });
    }, [categoryId]);

    if (loading) return <p className="text-center mt-5">Đang tải bài kiểm tra...</p>;

    return (
        <div className="container mt-5 pt-4">
            <h2 className="mb-4">Danh sách bài kiểm tra</h2>

            <div className="row">
                {exams.map(exam => (
                    <div className="col-md-6 mb-4" key={exam.id}>
                        <div className="card h-100 shadow-sm">
                            <div className="card-body">
                                <h5 className="card-title">{exam.title}</h5>
                                <p className="card-text">{exam.description}</p>

                                <button
                                    className="btn btn-primary"
                                    onClick={() => navigate(
                                        `/exam-categories/${categoryId}/exams/${exam.id}`
                                    )}
                                >
                                    Làm bài
                                </button>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default ExamListPage;
