import React, {Dispatch, useState} from 'react';
import {Card, CardBody, CardTitle, Col, FormGroup, Input, Label, Row} from "reactstrap";
import ButtonWithSpinner from "../../components/ButtonWithSpinner/ButtonWithSpinner";
import {Link} from "react-router-dom";
import * as api from "../../api/apiCalls";
import {useDispatch} from "react-redux";
import {Action} from "../../redux/authenticationReducer";


const Login = () => {
    const [ongoingApiCall, setOngoingApiCall] = useState<boolean>(false);
    const [identifier, setIdentifier] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [fieldErrors, setFieldErrors] = useState<any>([]);
    const [error, setError] = useState<string>('');
    const dispatch: Dispatch<Action> = useDispatch()


    const onClickLogin = () => {
        setFieldErrors([]);
        setError('')
        setOngoingApiCall(true);
        api.login({identifier: identifier, password: password})
            .then(response => {
                setOngoingApiCall(false)
                setIdentifier('');
                setPassword('');
                const {username, accessToken, refreshToken, userId, email} = response.data;
                dispatch({
                    type:"login",
                    payload: {isLoggedIn: true, accessToken, refreshToken, username, userId, email, encryptionKey: ""}})
                api.setAuthHeader({isLoggedIn: true, accessToken: response.data.accessToken})
            })
            .catch(error => {
            setOngoingApiCall(false);
            if (error?.response?.data?.errors) {
                setFieldErrors(error.response.data.errors);
                return;
            } else if (error.response?.data){
                setError(error.response.data);
            } else {
                setError("An error occurred, please try again later")
            }
        });
    }

    const onChangePassword = (ev: React.ChangeEvent<HTMLInputElement>) => {
        if (password !== ev.target.value.trim()) {
            const err = {...fieldErrors};
            delete err.Password;
            setFieldErrors(err);
            setPassword(ev.target.value.trim());
        }
    }

    const onChangeIdentifier = (ev: React.ChangeEvent<HTMLInputElement>) => {
        if (identifier !== ev.target.value.trim()) {
            const err = {...fieldErrors};
            delete err.Username;
            setFieldErrors(err);
            setIdentifier(ev.target.value.trim());
        }
    }

    return (
        <div>
            <div className="sidebar-header text-center py-4 myModal">
                <h3><Link to="/login">Bitpass app</Link></h3>
            </div>
            <Row className="justify-content-center mt-3">
                <Col xl="8" sm="10">
                    <Card>
                        <CardBody>
                            <CardTitle><h5>Sign in</h5></CardTitle>
                            <FormGroup>
                                <Label for="identifier" className="mt-3">
                                    Email or username:
                                </Label>
                                <Input
                                    id="identifier"
                                    name="identifier"
                                    placeholder="Enter your email address or username"
                                    type="text"
                                    value={identifier}
                                    onChange={onChangeIdentifier}
                                />
                                {fieldErrors.Identifier && <div className="text-danger mt-1">{fieldErrors.Identifier}</div>}
                                <Label for="password" className="mt-3">
                                    Password:
                                </Label>
                                <Input
                                    id="password"
                                    name="Password"
                                    placeholder="Enter your password"
                                    type="password"
                                    value={password}
                                    onChange={onChangePassword}
                                />
                                {fieldErrors.Password && <div className="text-danger mt-1">{fieldErrors.Password}</div>}
                            </FormGroup>
                            {error && <div className="text-danger text-center mt-1 mb-3">{error}</div>}

                            <div className="text-center">
                                <ButtonWithSpinner onClick={() => onClickLogin()} disabled={false}
                                                   className="" content="Sign in" ongoingApiCall={ongoingApiCall}/>
                            </div>
                        </CardBody>
                    </Card>
                </Col>
                <small className="text-center mt-2">Forgot your password? <Link className="fw-bold" to={'/request-reset-password'}>Request reset password</Link></small>
                <small className="text-center mt-2">Don't have an account yet? <Link className="fw-bold" to={'/register'}>Sign up</Link></small>
            </Row>
        </div>
    );
};

export default Login;