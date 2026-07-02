import { BrowserRouter, Routes, Route, Outlet } from 'react-router-dom';
import Header from './components/layouts/Header';
import Footer from './components/layouts/Footer';
import AdminLayout from './components/layouts/AdminLayout';
import ExamCategoryPage from './components/pages/ExamCategoryListPage';
import ExamListPage from './components/pages/ExamListPage'
import DoExamPage from './components/pages/DoExamPage';
import RegisterPage from './components/pages/RegisterPage';
import LoginPage from './components/pages/LoginPage';
import { AuthProvider } from './components/AuthContext';
import ProtectedRoute from './components/ProtectedRoute';
import HomePage from './components/pages/HomePage'; 
import TopicListPage from './components/pages/user/TopicListPage';
import PracticeListPage from './components/pages/user/PracticeListPage';
import PracticePage from './components/pages/user/PracticePage';
import ExamHistoryPage from './components/pages/user/ExamHistoryPage';
import ExamResultDetailPage from './components/pages/user/ExamResultDetailPage';
import FlashcardPage from './components/pages/user/FlashcardPage';
import FlashcardDetailPage from './components/pages/user/FlashcardDetailPage';
import TheoryListPage from './components/pages/user/TheoryListPage';
import TheoryDetailPage from './components/pages/user/TheoryDetailPage';

import AdminProtectedRoute from './components/AdminProtectedRoute';
import AdminDashboard from './components/pages/admin/AdminDashboard';
import CourseManagePage from './components/pages/admin/CourseManagePage';
import ExamManagePage from './components/pages/admin/ExamManagePage';
import CreateExamPage from './components/pages/admin/CreateExamPage';
import TopicManagePage from './components/pages/admin/TopicManagePage';
import ExamCategoryManagePage from './components/pages/admin/ExamCategoryManagePage';
import ResetPasswordPage from './components/pages/ResetPasswordPage';
function About() {
    return <h1>About Page</h1>;
}

function App() {
    return (
        <AuthProvider>
            <BrowserRouter>
                <Routes>
                    <Route element={<><Header /><Outlet /><Footer /></>}>
                        <Route path="/register" element={<RegisterPage />} />
                        <Route path="/login" element={<LoginPage />} />
                        <Route path="/reset-password" element={<ResetPasswordPage />} />
                        <Route path="/" element={<HomePage />} />
                        <Route path="/about" element={<About />} />
                        <Route path="/exam-categories" element={<ExamCategoryPage />} />
                        <Route path="/exam-categories/:categoryId/exams" element={<ExamListPage />} />
                        <Route path="/topic-practice" element={<TopicListPage />} />
                        <Route path="/topic-practice/:topicId" element={<PracticeListPage />} />
                        <Route path="/practice/:practiceId" element={<PracticePage />} />
                        <Route path="/theory" element={<TheoryListPage />} />
                        <Route path="/theory/:id" element={<TheoryDetailPage />} />
                        <Route path="/flashcards" element={<ProtectedRoute><FlashcardPage /></ProtectedRoute>} />
                        <Route path="/flashcard/:id" element={<ProtectedRoute><FlashcardDetailPage /></ProtectedRoute>} />
                        <Route path="/exam-categories/:categoryId/exams/:examId" element={<ProtectedRoute><DoExamPage /></ProtectedRoute>} />
                        <Route path="/practice/:practiceId" element={<ProtectedRoute><PracticePage /></ProtectedRoute>} />
                        <Route path="/exam-history" element={<ProtectedRoute><ExamHistoryPage /></ProtectedRoute>} />
                        <Route path="/exam-history/:resultId" element={<ProtectedRoute><ExamResultDetailPage /></ProtectedRoute>} />
                    </Route>
                    <Route 
                        path="/admin" 
                        element={
                            <AdminProtectedRoute>
                                <AdminLayout />
                            </AdminProtectedRoute>
                        }
                    >
                        <Route index element={<AdminDashboard />} />
                        <Route path="courses" element={<CourseManagePage />} />
                        <Route path="exam" element={<ExamManagePage />} />
                        <Route path="exams/create" element={<CreateExamPage />} />
                        <Route path="topic" element={<TopicManagePage />} />\
                        <Route path="examcategory" element={<ExamCategoryManagePage />} />
                    </Route>
                </Routes>
            </BrowserRouter>
        </AuthProvider>
    )
}

export default App;