import React, {useState} from 'react';
import {Card, CardBody, CardTitle, Col, FormGroup, Input, Label, Row} from "reactstrap";
import ButtonWithSpinner from "../../components/ButtonWithSpinner/ButtonWithSpinner";
import {Link} from "react-router-dom";

const RequestResetPassword = () => {
    const [ongoingApiCall, setOngoingApiCall] = useState<boolean>(false);

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
                                />
                            </FormGroup>
                            <div className="text-center">
                                <ButtonWithSpinner onClick={() => setOngoingApiCall(!ongoingApiCall)} disabled={false}
                                                   className="" content="Request" ongoingApiCall={ongoingApiCall}/>
                            </div>
                        </CardBody>
                    </Card>
                </Col>
                <small className="text-center mt-2"><Link className="fw-bold" to={'/login'}>Return to login</Link></small>
            </Row>
        </div>
    );
};

export default RequestResetPassword;