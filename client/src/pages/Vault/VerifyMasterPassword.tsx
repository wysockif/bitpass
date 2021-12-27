import React, {Dispatch, useState} from 'react';
import {FormGroup, Input, Label, Modal, ModalBody, ModalFooter, ModalHeader} from "reactstrap";
import ButtonWithSpinner from "../../components/ButtonWithSpinner/ButtonWithSpinner";
import {Link} from "react-router-dom";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faLock} from "@fortawesome/free-solid-svg-icons";
import * as api from "../../api/apiCalls";
import {derivativeKey, hashDerivationKey} from "../../security/KeyDerivation";
import {useDispatch, useSelector} from "react-redux";
import {Action, AuthState} from "../../redux/authenticationReducer";

const VerifyMasterPassword = () => {
        const [masterPassword, setMasterPassword] = useState<string>('');
        const [ongoingApiCall, setOngoingApiCall] = useState<boolean>(false);
        const [error, setError] = useState<string>('');
        const reduxState = useSelector((state: AuthState) => state)
        const dispatch: Dispatch<Action> = useDispatch()

        const onChangeMasterPassword = (ev: React.ChangeEvent<HTMLInputElement>) => {
            if (masterPassword !== ev.target.value.trim()) {
                setMasterPassword(ev.target.value.trim());
                setError('');
            }
        }

        const onClickVerify = () => {
            setOngoingApiCall(true);
            const encryptionKey = derivativeKey(masterPassword, reduxState.email);
            const encryptionKeyHash = hashDerivationKey(encryptionKey, reduxState.email);
            api.verifyEncryptionKey({encryptionKeyHash})
                .then(response => {
                    setOngoingApiCall(false);
                    dispatch({
                        type: "verify-master-password",
                        payload: {...reduxState, encryptionKey}
                    })
                })
                .catch(error => {
                        setOngoingApiCall(false);
                        if (error?.response?.data) {
                            setError("Incorrect master password");
                            return;
                        }
                        setError("An error occurred, please try again later")
                    }
                )
        }

        return (
            <Modal isOpen={!reduxState.encryptionKey} toggle={() => {
                return;
            }} centered>
                <ModalHeader toggle={() => {
                    return;
                }}>
                    <FontAwesomeIcon icon={faLock} className="me-2"/>
                    Verify your master password </ModalHeader>
                <ModalBody>
                    <FormGroup>
                        <Label for="password" className="mt-3">
                            Master password:
                        </Label>
                        <Input
                            id="password"
                            name="Password"
                            placeholder="Enter your master password"
                            type="password"
                            value={masterPassword}
                            onChange={onChangeMasterPassword}
                        />
                    </FormGroup>
                    <div style={{fontSize: "10px"}} className="text-muted">Your master password is never sent to the server.
                        Your browser will derive
                        an encryption key from your master password and only the hash of that key is sent to verify. <Link
                            to="/faq" className="fw-bold">Read more</Link></div>

                </ModalBody>
                {error && <div className="text-danger text-center mt-1 mb-3">{error}</div>}
                <ModalFooter>
                    <ButtonWithSpinner onClick={onClickVerify} disabled={masterPassword.length < 8} className=""
                                       content={"Verify"} ongoingApiCall={ongoingApiCall}/>
                </ModalFooter>
            </Modal>
        );
    }
;

export default VerifyMasterPassword;