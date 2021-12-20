import {useState} from "react";
import SideBar from "./components/Sidebar/Sidebar";
import {BrowserRouter, Navigate, Route, Routes} from "react-router-dom";
import {Col, Row} from "reactstrap";
import Generator from "./pages/Generator/Generator";
import Vault from "./components/Vault/Vault";
import Settings from "./pages/Settings/Settings";
import Faq from "./pages/Faq/Faq";
import Contact from "./pages/Contact/Contact";
import Sessions from "./pages/Sessions/Sessions";
import AccountActivity from "./pages/Activity/AccountActivity";
import Register from "./pages/Register/Register";
import Login from "./pages/Login/Login";

function App() {
    const [sidebarIsOpen, setSidebarOpen] = useState(true);
    const toggleSidebar = () => setSidebarOpen(!sidebarIsOpen);

    let userLoggedIn = false;

    return (
        <BrowserRouter>
            <div>
                {!userLoggedIn && <Routes>
                    <Route path="/login" element={<Login/>}/>
                    <Route path="/register" element={<Register/>}/>
                    <Route path="/*" element={<Navigate to="/login"/>}/>
                </Routes>}
                {userLoggedIn && <Row className="m-0">
                    <SideBar toggle={toggleSidebar} isOpen={sidebarIsOpen}/>
                    <Col lg="10" md="9" sm="8" className="p-0">
                        <Routes>
                            <Route path="/vault" element={<Vault/>}/>
                            <Route path="/generator" element={<Generator/>}/>
                            <Route path="/settings" element={<Settings/>}/>
                            <Route path="/faq" element={<Faq/>}/>
                            <Route path="/contact" element={<Contact/>}/>
                            <Route path="/active-sessions" element={<Sessions/>}/>
                            <Route path="/last-activity" element={<AccountActivity/>}/>
                            <Route path="/*" element={<Navigate to="/vault"/>}/>
                        </Routes>
                    </Col>
                </Row>}
            </div>
        </BrowserRouter>
    );
}

export default App;
