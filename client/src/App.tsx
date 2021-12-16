import {useState} from "react";
import SideBar from "./components/Sidebar/Sidebar";
import {BrowserRouter, Navigate, Route, Routes} from "react-router-dom";
import {Col, Row} from "reactstrap";
import Generator from "./components/Generator/Generator";
import Vault from "./components/Vault/Vault";
import Settings from "./components/Settings/Settings";
import Faq from "./components/Faq/Faq";
import Contact from "./components/Contact/Contact";
import Sessions from "./components/Sessions/Sessions";
import AccountActivity from "./components/Activity/AccountActivity";

function App() {
    const [sidebarIsOpen, setSidebarOpen] = useState(true);
    const toggleSidebar = () => setSidebarOpen(!sidebarIsOpen);


    return (
        <BrowserRouter>
            <div>
                <Row className="m-0">
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
                </Row>
            </div>
        </BrowserRouter>
    );
}

export default App;
