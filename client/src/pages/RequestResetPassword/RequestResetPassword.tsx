import React, {useState} from 'react';
import {Card, CardBody, CardTitle, Col, FormGroup, Input, Label, Row} from "reactstrap";
import ButtonWithSpinner from "../../components/ButtonWithSpinner/ButtonWithSpinner";
import {Link} from "react-router-dom";
import * as api from "../../api/apiCalls";

const RequestResetPassword = () => {
    const [ongoingApiCall, setOngoingApiCall] = useState<boolean>(false);
    const [identifier, setIdentifier] = useState<string>('');
    const [fieldErrors, setFieldErrors] = useState<any>([]);
    const [error, setError] = useState<string>('');
    const [message, setMessage] = useState<string>('');

    const onClickRequestResetPassword = () => {
        setIdentifier('');
        setOngoingApiCall(true);
        setError('');
        setFieldErrors('');
        setMessage('')

        api.requestResetPassword({identifier})
            .then(response => {
                setMessage('Check your email inbox and reset your password')
                setIdentifier('');
                setOngoingApiCall(false);
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
            })
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
                            <CardTitle><h5>Request reset password</h5></CardTitle>
                            <small className="text-muted">Requests per hour are limited</small>

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
                            </FormGroup>
                            {message && <div className="text-center mb-2">
                                <div className="text-success">{message}</div>
                                <div style={{fontSize: "12px"}} className="text-muted mt-2">
                                    If you do not see the email in a few minutes, check your spam folder.<br/>
                                    The number of requests per hour is limited. <br/>
                                    Only the last sent link is valid (for 15 minutes).
                                </div>
                            </div>}
                            {error && <div className="text-danger text-center mt-1 mb-3">{error}</div>}
                            <div className="text-center">
                                <ButtonWithSpinner onClick={onClickRequestResetPassword} disabled={!identifier}
                                                   className="" content="Request" ongoingApiCall={ongoingApiCall}/>
                            </div>
                        </CardBody>
                    </Card>
                </Col>
                <small className="text-center mt-2"><Link className="fw-bold" to={'/login'}>Return to
                    login</Link></small>
            </Row>
        </div>
    );
};

export default RequestResetPassword;