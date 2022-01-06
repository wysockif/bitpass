import React, {useState} from 'react';
import {
    Card,
    CardBody,
    CardTitle,
    Col,
    FormGroup,
    Input,
    Label,
    Modal,
    ModalBody,
    ModalFooter,
    ModalHeader,
    Row
} from "reactstrap";
import ButtonWithSpinner from "../../components/ButtonWithSpinner/ButtonWithSpinner";
import {Link} from "react-router-dom";
import * as api from "../../api/apiCalls";
import {derivativeKey, hashDerivationKey} from "../../security/KeyDerivation";
import PasswordInput from "../../components/PasswordInput/PasswordInput";
import {validatePassword} from "../../utils/validatePassword";

const Register = () => {
    const [ongoingApiCall, setOngoingApiCall] = useState<boolean>(false);
    const [email, setEmail] = useState<string>('');
    const [username, setUsername] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [masterPassword, setMasterPassword] = useState<string>('');
    const [fieldErrors, setFieldErrors] = useState<any>([]);
    const [error, setError] = useState<string>('');
    const [masterPasswordError, setMasterPasswordError] = useState<string>('');
    const [passwordError, setPasswordError] = useState<string>('');
    const [isMessageDisplayed, setIsMessageDisplayed] = useState<boolean>(false);

    const onClickRegisterButton = () => {
        setFieldErrors([]);
        setError('')
        setOngoingApiCall(true);
        const encryptionKeyHash = hashDerivationKey(derivativeKey(masterPassword, email), email);
        api.register({email, username, password, encryptionKeyHash})
            .then(() => {
                setUsername('');
                setEmail('');
                setPassword('');
                setMasterPassword('');
                setOngoingApiCall(false);
                setIsMessageDisplayed(true);
            })
            .catch(error => {
                setOngoingApiCall(false);
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

    const onChangeEmail = (ev: React.ChangeEvent<HTMLInputElement>) => {
        if (email !== ev.target.value.trim()) {
            const err = {...fieldErrors};
            delete err.Email;
            setFieldErrors(err);
            setEmail(ev.target.value.trim());
        }
    }

    const onChangeUsername = (ev: React.ChangeEvent<HTMLInputElement>) => {
        if (username !== ev.target.value.trim()) {
            const err = {...fieldErrors};
            delete err.Username;
            setFieldErrors(err);
            setUsername(ev.target.value.trim());
        }
    }

    const onChangePassword = (ev: React.ChangeEvent<HTMLInputElement>) => {
        if (password !== ev.target.value.trim()) {
            const err = {...fieldErrors};
            delete err.Password;
            setFieldErrors(err);
            setPassword(ev.target.value.trim());
            if (validatePassword(ev.target.value.trim()) || ev.target.value.trim() === '') {
                setPasswordError('');
                console.log('here')
            } else {
                setPasswordError('Password is too weak.');
            }
        }
    }

    const onChangeMasterPassword = (ev: React.ChangeEvent<HTMLInputElement>) => {
        if (masterPassword !== ev.target.value.trim()) {
            const err = {...fieldErrors};
            delete err.MasterPassword;
            setFieldErrors(err);
            setMasterPassword(ev.target.value.trim());
            if (validatePassword(ev.target.value.trim()) || ev.target.value.trim() === '') {
                setMasterPasswordError('');
            } else {
                setMasterPasswordError('Master password is too weak.');
            }
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
                            <CardTitle><h5>Sign up</h5></CardTitle>
                            <FormGroup>
                                <Label for="email" className="mt-3">
                                    Email:
                                </Label>
                                <Input
                                    id="email"
                                    name="email"
                                    placeholder="Enter your email address"
                                    type="text"
                                    value={email}
                                    onChange={onChangeEmail}
                                />
                                {fieldErrors.Email && <div className="text-danger mt-1">{fieldErrors.Email}</div>}
                                <Label for="username" className="mt-3">
                                    Username:
                                </Label>
                                <Input
                                    id="username"
                                    name="Username"
                                    placeholder="Enter your username"
                                    type="text"
                                    value={username}
                                    onChange={onChangeUsername}
                                />
                                {fieldErrors.Username && <div className="text-danger mt-1">{fieldErrors.Username}</div>}
                                <Label for="password" className="mt-3">
                                    Password:
                                </Label>
                                <PasswordInput
                                    id="password"
                                    name="Password"
                                    placeholder="Enter your password"
                                    password={password}
                                    onChangePassword={onChangePassword}
                                />
                                {fieldErrors.Password && <div className="text-danger mt-1">{fieldErrors.Password}</div>}
                                {passwordError &&
                                <div className="text-danger mt-1">{passwordError}</div>}
                                <Label for="masterPassword" className="mt-3">
                                    Master password:
                                </Label>
                                <PasswordInput
                                    id="masterPassword"
                                    name="Master password"
                                    placeholder="Enter your master password"
                                    password={masterPassword}
                                    onChangePassword={onChangeMasterPassword}
                                />
                                {fieldErrors.EncryptionKeyHash &&
                                <div className="text-danger mt-1">{fieldErrors.EncryptionKeyHash}</div>}
                                {masterPasswordError &&
                                <div className="text-danger mt-1">{masterPasswordError}</div>}
                            </FormGroup>
                            {error && <div className="text-danger text-center mt-1 mb-3">{error}</div>}

                            <div className="text-center">
                                <ButtonWithSpinner onClick={() => onClickRegisterButton()}
                                                   disabled={!username || !email || !password || !masterPassword || masterPasswordError !== '' || passwordError !== ''}
                                                   content="Sign up" ongoingApiCall={ongoingApiCall}/>
                            </div>
                        </CardBody>
                    </Card>
                </Col>
                <small className="text-center mt-2">Already have an account? <Link className="fw-bold" to={'/login'}>Sign
                    in</Link></small>
            </Row>
            <Modal isOpen={isMessageDisplayed} toggle={() => {
                return;
            }} centered>
                <ModalHeader toggle={() => {
                    return;
                }}>
                    Welcome to Bitpass!
                </ModalHeader>
                <ModalBody>
                    Check your email inbox and verify your email address.
                    <div style={{fontSize: "12px"}} className="text-muted mt-2">If you do not see the email in a few
                        minutes, check your spam folder.</div>
                </ModalBody>
                <ModalFooter>
                    <Link to="/login">
                        <ButtonWithSpinner onClick={() => {
                            return;
                        }} disabled={true} content={"Login"} ongoingApiCall={false}/>
                    </Link>
                </ModalFooter>
            </Modal>
        </div>
    );
};

export default Register;