import React, {Dispatch, useState} from 'react';
import PageTitle from "../../components/PageTitile/PageTitle";
import HorizontalLine from "../../components/HorizontalLine/HorizontalLine";
import {Col, Label} from "reactstrap";
import ButtonWithSpinner from "../../components/ButtonWithSpinner/ButtonWithSpinner";
import * as api from "../../api/apiCalls";
import {Action} from "../../redux/authenticationReducer";
import {useDispatch} from "react-redux";
import PasswordInput from "../../components/PasswordInput/PasswordInput";


const Settings = () => {
    const [ongoingLogoutAllSessionsApiCall, setOngoingLogoutAllSessionsApiCall] = useState<boolean>(false);
    const [ongoingChangePasswordApiCall, setOngoingChangePasswordApiCall] = useState<boolean>(false);
    const dispatch: Dispatch<Action> = useDispatch()
    const [oldPassword, setOldPassword] = useState<string>('');
    const [newPassword, setNewPassword] = useState<string>('');

    const onClickLogoutAllSessions = () => {
        setOngoingLogoutAllSessionsApiCall(true);
        api.logoutAllSessions()
            .finally(() => {
                setOngoingLogoutAllSessionsApiCall(false);
                dispatch({type: "logout"});
            });
    }

    const onClickChangePassword = () => {
        setOngoingChangePasswordApiCall(true);
        api.changePassword({oldPassword, newPassword})
            .finally(() => {
                setOngoingChangePasswordApiCall(false);
                dispatch({type: "logout"});
            });
    }

    const onChangeNewPassword = (ev: React.ChangeEvent<HTMLInputElement>) => {
        if (newPassword !== ev.target.value.trim()) {
            setNewPassword(ev.target.value.trim());
        }
    }

    const onChangeOldPassword = (ev: React.ChangeEvent<HTMLInputElement>) => {
        if (oldPassword !== ev.target.value.trim()) {
            setOldPassword(ev.target.value.trim());
        }
    }

    return (
        <div>
            <PageTitle title="Settings"/>
            <div className="px-3 pt-3 my-2" id="log-out-of-all-sessions">
                <h6 className="fw-bold">Log out of all sessions</h6>
                <small className="text-muted">Active sessions may continue to remain active for up to 1
                    minute.</small>
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
                    <Label for="old-password" className="mt-3">
                        Current password:
                    </Label>
                    <PasswordInput password={oldPassword} onChangePassword={onChangeOldPassword}
                                   placeholder="Enter your current password" name={"old-password"} id={"old-password"}/>
                    <Label for="new-password" className="mt-3">
                        New password:
                    </Label>
                    <PasswordInput password={newPassword} onChangePassword={onChangeNewPassword}
                                   placeholder={"Enter your new password"} name={"new-password"} id={"new-password"}/>
                    <div className="d-flex aligns-items-center justify-content-center mb-2 mt-2">
                        <ButtonWithSpinner onClick={onClickChangePassword} content="Change" disabled={false}
                                           ongoingApiCall={ongoingChangePasswordApiCall}/>
                    </div>
                </Col>
                <HorizontalLine/>
            </div>
        </div>
    );
};

export default Settings;