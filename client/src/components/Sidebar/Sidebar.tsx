import React, {Dispatch} from "react";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {
    faCog,
    faExclamationCircle,
    faKey,
    faPaperPlane,
    faQuestion,
    faShieldAlt,
    faSignOutAlt
} from "@fortawesome/free-solid-svg-icons";
import {Col, Nav, NavItem, NavLink} from "reactstrap";
import classNames from "classnames";
import {Link} from "react-router-dom";
import * as api from "../../api/apiCalls";
import SubMenu from "./SubMenu";
import {useDispatch, useSelector} from "react-redux";
import {Action, AuthState} from "../../redux/authenticationReducer";

const SideBar = (props: any) => {
    const refreshToken = useSelector((state: AuthState) => state.refreshToken);
    const dispatch: Dispatch<Action> = useDispatch();

    const onClickLogoutButton = () => {
        api.logout({refreshToken})
            .finally(() => {
                api.deleteAuthHeader();
                dispatch({type: "logout"});
            });
    }

    return (
        <Col lg="2" md="3" sm="4" className={classNames("sidebar", {"is-open": props.isOpen})}>
            <div className="sidebar-header mt-4 text-center mb-3">
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
                <div className="text-center logout-button ms-1 px-2 py-1" onClick={onClickLogoutButton}>
                    <small>
                        <FontAwesomeIcon icon={faSignOutAlt} className="me-2"/>
                        Log out
                    </small>
                </div>
            </div>
        </Col>
    );
}

const submenus = [
    [
        {
            title: "Last Activity",
            target: "last-activity",
        },
        {
            title: "Active sessions",
            target: "active-sessions",
        },
    ],
];

export default SideBar;
