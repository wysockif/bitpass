import React, {Dispatch, useState} from 'react';
import PageTitle from "../../components/PageTitile/PageTitle";
import HorizontalLine from "../../components/HorizontalLine/HorizontalLine";
import {Col, Label} from "reactstrap";
import ButtonWithSpinner from "../../components/ButtonWithSpinner/ButtonWithSpinner";
import * as api from "../../api/apiCalls";
import {Action} from "../../redux/authenticationReducer";
import {useDispatch} from "react-redux";
import PasswordInput from "../../components/PasswordInput/PasswordInput";
import {validatePassword} from "../../utils/validatePassword";


const Settings = () => {
    const [ongoingLogoutAllSessionsApiCall, setOngoingLogoutAllSessionsApiCall] = useState<boolean>(false);
    const [ongoingChangePasswordApiCall, setOngoingChangePasswordApiCall] = useState<boolean>(false);
    const dispatch: Dispatch<Action> = useDispatch()
    const [oldPassword, setOldPassword] = useState<string>('');
    const [newPassword, setNewPassword] = useState<string>('');
    const [fieldErrors, setFieldErrors] = useState<any>([]);
    const [error, setError] = useState<string>('');
    const [newPasswordError, setNewPasswordError] = useState<string>('');


    const onClickLogoutAllSessions = () => {
        setOngoingLogoutAllSessionsApiCall(true);
        api.logoutAllSessions()
            .finally(() => {
                setOngoingLogoutAllSessionsApiCall(false);
                dispatch({type: "logout"});
            });
    }

    const onClickChangePassword = () => {
        setFieldErrors([]);
        setError('')
        setOngoingChangePasswordApiCall(true);
        api.changePassword({oldPassword, newPassword})
            .then(() => {
                setOngoingChangePasswordApiCall(false);
                dispatch({type: "logout"});
            })
            .catch(error => {
                setOngoingChangePasswordApiCall(false);
                if (error?.response?.data?.errors) {
                    setFieldErrors(error.response.data.errors);
                    return;
                } else if (error.response?.data) {
                    setError(error.response.data);
                } else {
                    setError("An error occurred, please try again later")
                }
            });
    }

    const onChangeNewPassword = (ev: React.ChangeEvent<HTMLInputElement>) => {
        if (newPassword !== ev.target.value.trim()) {
            const err = {...fieldErrors};
            delete err.Password;
            setFieldErrors(err);
            setNewPassword(ev.target.value.trim());
            if (validatePassword(ev.target.value.trim()) || ev.target.value.trim() === '') {
                setNewPasswordError('');
            } else {
                setNewPasswordError('Password must contain at least 8 characters: at least one uppercase character, one lowercase character, " +\n' +
                    '                    "one number, one special character and must not contain any white character.');
            }
        }
    }

    const onChangeOldPassword = (ev: React.ChangeEvent<HTMLInputElement>) => {
        if (oldPassword !== ev.target.value.trim()) {
            const err = {...fieldErrors};
            delete err.Password;
            setFieldErrors(err);
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
                    {fieldErrors.OldPassword &&
                    <div className="text-danger mt-1">{fieldErrors.OldPassword}</div>}
                    <Label for="new-password" className="mt-3">
                        New password:
                    </Label>
                    <PasswordInput password={newPassword} onChangePassword={onChangeNewPassword}
                                   placeholder={"Enter your new password"} name={"new-password"} id={"new-password"}/>
                    {fieldErrors.NewPassword &&
                    <div className="text-danger mt-1">{fieldErrors.NewPassword}</div>}
                    {newPasswordError &&
                    <div className="text-danger mt-1">{newPasswordError}</div>}
                    {error && <div className="text-danger text-center mt-1 mb-3">{error}</div>}

                    <div className="d-flex aligns-items-center justify-content-center mb-2 mt-2">
                        <ButtonWithSpinner onClick={onClickChangePassword} content="Change"
                                           disabled={!newPassword || !oldPassword || newPasswordError !== ''}
                                           ongoingApiCall={ongoingChangePasswordApiCall}/>
                    </div>
                </Col>
                <HorizontalLine/>
            </div>
        </div>
    );
};

export default Settings;