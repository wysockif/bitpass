import React, {useState} from 'react';
import {Card, CardBody, CardTitle, Col, FormGroup, Input, Label, Row} from "reactstrap";
import ButtonWithSpinner from "../../components/ButtonWithSpinner/ButtonWithSpinner";
import {Link} from "react-router-dom";

const Register = () => {
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
                            <CardTitle><h5>Sign up</h5></CardTitle>
                            <FormGroup>
                                <Label for="email" className="mt-3">
                                    Email:
                                </Label>
                                <Input
                                    id="email"
                                    name="email"
                                    placeholder="Enter your email address"
                                    type="email"
                                />
                                <Label for="username" className="mt-3">
                                    Username:
                                </Label>
                                <Input
                                    id="username"
                                    name="Username"
                                    placeholder="Enter your username"
                                    type="email"
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
                                <Label for="masterPassword" className="mt-3">
                                    Master password:
                                </Label>
                                <Input
                                    id="masterPassword"
                                    name="Master password"
                                    placeholder="Enter your master password"
                                    type="password"
                                />
                            </FormGroup>
                            <div className="text-center">
                                <ButtonWithSpinner onClick={() => setOngoingApiCall(!ongoingApiCall)} disabled={false}
                                                   className="" content="Sign up" ongoingApiCall={ongoingApiCall}/>
                            </div>
                        </CardBody>
                    </Card>
                </Col>
                <small className="text-center mt-2">Already have an account? <Link className="fw-bold" to={'/login'}>Sign in</Link></small>
            </Row>
        </div>
    );
};

export default Register;