import React, {Dispatch, useState} from 'react';
import PageTitle from "../../components/PageTitile/PageTitle";
import HorizontalLine from "../../components/HorizontalLine/HorizontalLine";
import {Button, Col, Input, InputGroup, InputGroupText} from "reactstrap";
import ButtonWithSpinner from "../../components/ButtonWithSpinner/ButtonWithSpinner";
import * as api from "../../api/apiCalls";
import {Action} from "../../redux/authenticationReducer";
import {useDispatch} from "react-redux";


const Settings = () => {
    const [ongoingLogoutAllSessionsApiCall, setOngoingLogoutAllSessionsApiCall] = useState<boolean>(false);
    const dispatch: Dispatch<Action> = useDispatch()


    const onClickLogoutAllSessions = () => {
        setOngoingLogoutAllSessionsApiCall(true);
        api.logoutAllSessions()
            .finally(() => {
                setOngoingLogoutAllSessionsApiCall(false);
                dispatch({type:"logout"});
            });
    }

    return (
        <div>
            <PageTitle title="Settings"/>
            <div className="px-3 pt-3 my-2" id="log-out-of-all-sessions">
                <h6 className="fw-bold">Log out of all sessions</h6>
                <small className="text-muted">Active sessions may continue to remain active for up to 15
                    minutes.</small>
                <br/>
                <Col md="8" lg="6" xl="5" xxl="4" className="mt-2">
                    <div className="d-flex aligns-items-center justify-content-center mt-2 mb-2">
                        <ButtonWithSpinner onClick={onClickLogoutAllSessions} content="Log me out" disabled={false}
                                           ongoingApiCall={ongoingLogoutAllSessionsApiCall}/>
                    </div>
                </Col>
                <HorizontalLine/>
            </div>
            <div className="px-3 pt-1 my-2" id="change-password">
                <h6 className="fw-bold">Change password</h6>
                <small className="text-muted">You can change your password - not to be confused with master
                    password.</small>
                <Col md="8" lg="6" xl="5" xxl="4" className="mt-2">
                    <InputGroup>
                        <InputGroupText style={{minWidth: "209px"}}>
                            Current password:
                        </InputGroupText>
                        <Input/>
                    </InputGroup>
                    <InputGroup className="mt-1">
                        <InputGroupText style={{minWidth: "209px"}}>
                            New password:
                        </InputGroupText>
                        <Input/>
                    </InputGroup>
                    <InputGroup className="mt-1">
                        <InputGroupText style={{minWidth: "209px"}}>
                            Repeat new password:
                        </InputGroupText>
                        <Input/>
                    </InputGroup>
                    <div className="d-flex aligns-items-center justify-content-center mb-2">
                        <Button className="mt-2" size="sm">Change</Button>
                    </div>
                </Col>
                <HorizontalLine/>
            </div>
        </div>
    );
};

export default Settings;