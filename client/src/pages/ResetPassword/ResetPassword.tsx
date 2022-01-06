import React, {useState} from 'react';
import {Link, useParams} from "react-router-dom";
import {Col, FormGroup, Label, Row} from "reactstrap";
import * as api from "../../api/apiCalls";
import ButtonWithSpinner from "../../components/ButtonWithSpinner/ButtonWithSpinner";
import PasswordInput from "../../components/PasswordInput/PasswordInput";
import {validatePassword} from "../../utils/validatePassword";

const ResetPassword = () => {
    const [ongoingApiCall, setOngoingApiCall] = useState<boolean>(false);
    const [error, setError] = useState<string>('');
    const [message, setMessage] = useState<string>('');
    const [fieldErrors, setFieldErrors] = useState<any>([]);
    const [newPassword, setNewPassword] = useState<string>('');
    const {token, username} = useParams();
    const [newPasswordError, setNewPasswordError] = useState<string>('');


    const onClickResetPassword = () => {
        setOngoingApiCall(true);
        setError('');
        setFieldErrors('');
        setMessage('')

        if (!username || !token) {
            setError("Incorrect link")
            return;
        }

        api.resetPassword({username, newPassword, resetPasswordToken: token})
            .then(() => {
                setMessage('Success')
                setNewPassword('');
                setOngoingApiCall(false);
            })
            .catch(error => {
                setMessage('')
                setNewPassword('');
                setOngoingApiCall(false);
                if (error?.response?.data?.errors) {
                    setFieldErrors(error.response.data.errors);
                    return;
                } else if (error.response?.data) {
                    setError(error.response.data);
                } else {
                    setError("An error occurred, please try again later")
                }
            })
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
                setNewPasswordError('Password is too weak.');
            }
        }
    }

    return (
        <div>
            <div className="sidebar-header text-center py-4 myModal">
                <h3><Link to="/login">Bitpass app</Link></h3>
            </div>
            <div className="text-center mt-3">
                <Row className="justify-content-center mt-3">
                    <Col xl="8" sm="10">
                        <FormGroup>
                            <Label for="identifier" className="mt-3">
                                New password:
                            </Label>
                            <PasswordInput
                                id="password"
                                name="password"
                                placeholder="Enter your new password"
                                password={newPassword}
                                onChangePassword={onChangeNewPassword}
                            />
                        </FormGroup>
                    </Col>
                </Row>
                {message && <div>
                    <div className="text-success mb-3">{message}</div>
                </div>}
                {error && <div className="text-danger text-center mt-1 mb-3">{error}</div>}
                {newPasswordError &&
                <div className="text-danger">{newPasswordError}</div>}
                <div className="text-center mt-2">
                    <ButtonWithSpinner onClick={onClickResetPassword} disabled={!newPassword || newPasswordError !== ''}
                                       className="" content="Reset" ongoingApiCall={ongoingApiCall}/>
                </div>
                <div className="mt-2"><small className="text-center"><Link className="fw-bold" to={'/login'}>Return to
                    login</Link></small></div>
            </div>
        </div>
    );
};

export default ResetPassword;