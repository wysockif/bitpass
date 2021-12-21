import React from 'react';
import {FormGroup, Input, Label, Modal, ModalBody, ModalFooter, ModalHeader} from "reactstrap";
import ButtonWithSpinner from "../../components/ButtonWithSpinner/ButtonWithSpinner";
import {Link} from "react-router-dom";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faLock} from "@fortawesome/free-solid-svg-icons";

interface VerifyPasswordModalProps {
    isOpen: boolean
    toggle: () => void
}

const VerifyMasterPassword = (props: VerifyPasswordModalProps) => {
    return (
        <Modal isOpen={props.isOpen} toggle={props.toggle} centered>
            <ModalHeader toggle={props.toggle}>
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
                    />
                </FormGroup>
                <div style={{fontSize: "10px"}} className="text-muted">Your master password is never sent to the server.
                    Your browser will derive
                    an encryption key from your master password and only the hash of that key is sent to verify. <Link
                        to="/faq" className="fw-bold">Read more</Link></div>

            </ModalBody>
            <ModalFooter>
                <ButtonWithSpinner onClick={() => {
                    return;
                }} disabled={false} className="" content={"Verify"} ongoingApiCall={true}/>
            </ModalFooter>
        </Modal>
    );
};

export default VerifyMasterPassword;