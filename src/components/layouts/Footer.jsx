function Footer() {
    return (
        <div className="container">
            <footer className="d-flex flex-wrap justify-content-between align-items-center py-3 my-4 border-top">
                <div className="d-flex align-items-center">
                    <a href="/" className="me-2 text-body-secondary text-decoration-none">
                        <svg className="bi" width="30" height="24">
                            <use xlinkHref="#bootstrap"></use>
                        </svg>
                    </a>
                    <span className="text-body-secondary text-nowrap">© 2026 Phan Đình Tuấn. Phiên bản thử nghiệm phục vụ mục đích học tập</span>
                </div>

                <ul className="nav list-unstyled d-flex mb-0">
                    <li className="ms-3">
                        <a className="text-body-secondary" href="#">
                            <svg className="bi" width="24" height="24">
                                <use xlinkHref="#twitter"></use>
                            </svg>
                        </a>
                    </li>
                    <li className="ms-3">
                        <a className="text-body-secondary" href="#">
                            <svg className="bi" width="24" height="24">
                                <use xlinkHref="#instagram"></use>
                            </svg>
                        </a>
                    </li>
                    <li className="ms-3">
                        <a className="text-body-secondary" href="#">
                            <svg className="bi" width="24" height="24">
                                <use xlinkHref="#facebook"></use>
                            </svg>
                        </a>
                    </li>
                </ul>
            </footer>
        </div>
    );
}

export default Footer;