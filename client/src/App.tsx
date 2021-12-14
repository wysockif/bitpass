import {useState} from "react";
import SideBar from "./components/Sidebar/Sidebar";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import {Container} from "reactstrap";
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
            <div className="App wrapper">
                <SideBar toggle={toggleSidebar} isOpen={sidebarIsOpen}/>
                <Container fluid className="px-0">
                    <Routes>
                        <Route path="/vault" element={<Vault/>}/>
                        <Route path="/generator" element={<Generator/>}/>
                        <Route path="/settings" element={<Settings/>}/>
                        <Route path="/faq" element={<Faq/>}/>
                        <Route path="/contact" element={<Contact/>}/>
                        <Route path="/active-sessions" element={<Sessions/>}/>
                        <Route path="/last-activity" element={<AccountActivity/>}/>
                        <Route path="*" element={<Vault/>} />
                        {/*<*/}
                        {/*/!*<Route path="/" element={<Welcome username={"wysockif"}/>}/>*!/*/}
                    </Routes>

                </Container>

                {/*<Content toggleSidebar={toggleSidebar} sidebarIsOpen={sidebarIsOpen} />*/}
            </div>
        </BrowserRouter>
    );
}

export default App;
