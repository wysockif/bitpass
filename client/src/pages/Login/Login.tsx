import React, {useState} from 'react';
import {Card, CardBody, CardTitle, Col, FormGroup, Input, Label, Row} from "reactstrap";
import ButtonWithSpinner from "../../components/ButtonWithSpinner/ButtonWithSpinner";
import {Link} from "react-router-dom";

const Login = () => {
    const [ongoingApiCall, setOngoingApiCall] = useState<boolean>(false);


    return (
        <div>
            <div className="sidebar-header text-center py-4 myModal">
                <h3>Bitpass app</h3>
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
                                />
                                <Label for="password" className="mt-3">
                                    Password:
                                </Label>
                                <Input
                                    id="password"
                                    name="Password"
                                    placeholder="Enter your password"
                                    type="password"
                                />
                            </FormGroup>
                            <div className="text-center">
                                <ButtonWithSpinner onClick={() => setOngoingApiCall(!ongoingApiCall)} disabled={false}
                                                   className="" content="Sign in" ongoingApiCall={ongoingApiCall}/>
                            </div>
                        </CardBody>
                    </Card>
                </Col>
                <small className="text-center mt-2">Don't have an account yet? <Link className="fw-bold" to={'/register'}>Sign up</Link></small>
            </Row>
        </div>
    );
};

export default Login;