import {useState} from "react";
import SideBar from "./components/Sidebar/Sidebar";
import {BrowserRouter} from "react-router-dom";

function App() {
    const [sidebarIsOpen, setSidebarOpen] = useState(true);
    const toggleSidebar = () => setSidebarOpen(!sidebarIsOpen);


    return (
        <BrowserRouter>
            <div className="App wrapper">
                <SideBar toggle={toggleSidebar} isOpen={sidebarIsOpen} />
                {/*<Content toggleSidebar={toggleSidebar} sidebarIsOpen={sidebarIsOpen} />*/}
            </div>
        </BrowserRouter>
    );
}

export default App;
