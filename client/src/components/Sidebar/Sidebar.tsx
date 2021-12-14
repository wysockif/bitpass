import React from "react";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {
    faCog,
    faExclamationCircle,
    faKey,
    faPaperPlane,
    faQuestion,
    faShieldAlt
} from "@fortawesome/free-solid-svg-icons";
import {Nav, NavItem, NavLink} from "reactstrap";
import classNames from "classnames";
import {Link} from "react-router-dom";

import SubMenu from "./SubMenu";

const SideBar = (props: any) => (
    <div className={classNames("sidebar", {"is-open": props.isOpen})}>
        <div className="sidebar-header">
            <h3>Bitpass app</h3>
        </div>
        <div className="side-menu">
            <Nav vertical className="list-group pb-3">
                <NavItem>
                    <NavLink tag={Link} to={"/vault"} className="menu-item">
                        <FontAwesomeIcon icon={faShieldAlt} className="me-2"/>
                        Vault
                    </NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} to={"/generator"} className="menu-item">
                        <FontAwesomeIcon icon={faKey} className="me-2"/>
                        Generator
                    </NavLink>
                </NavItem>
                <SubMenu title="Activity" icon={faExclamationCircle} items={submenus[0]}/>
                <NavItem>
                    <NavLink tag={Link} to={"/settings"} className="menu-item">
                        <FontAwesomeIcon icon={faCog} className="me-2"/>
                        Settings
                    </NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} to={"/faq"} className="menu-item">
                        <FontAwesomeIcon icon={faQuestion} className="me-2"/>
                        FAQ
                    </NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} to={"/contact"} className="menu-item">
                        <FontAwesomeIcon icon={faPaperPlane} className="me-2"/>
                        Contact
                    </NavLink>
                </NavItem>
            </Nav>
        </div>
    </div>
);

const submenus = [
    [
        {
            title: "Last Activity",
            target: "last-Activity",
        },
        {
            title: "Active sessions",
            target: "active-sessions",
        },
    ],
];

export default SideBar;
