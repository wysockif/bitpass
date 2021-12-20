import React from 'react';
import {FormGroup, Input, Label, Modal, ModalBody, ModalFooter, ModalHeader} from "reactstrap";

const RegisterModal = (props: any) => {
    return (
        <Modal isOpen={props.registerModal} toggle={props.toggleRegisterModal} centered>
            <ModalHeader
                toggle={props.toggleRegisterModal}>Sign up</ModalHeader>
            <ModalBody>
                <FormGroup>
                    <Label for="email" className="mt-3">
                        Email:
                    </Label>
                    <Input
                        id="email"
                        name="email"
                        placeholder="Your email address"
                        type="email"
                    />
                    <Label for="email" className="mt-3">
                        Username:
                    </Label>
                    <Input
                        id="username"
                        name="Username"
                        placeholder="Your username"
                        type="email"
                    />
                    <Label for="password" className="mt-3">
                        Password:
                    </Label>
                    <Input
                        id="password"
                        name="Password"
                        placeholder="Your password"
                        type="password"
                    />
                    <Label for="masterPassword" className="mt-3">
                        Master password:
                    </Label>
                    <Input
                        id="masterPassword"
                        name="Master password"
                        placeholder="Your master password"
                        type="password"
                    />
                </FormGroup>
            </ModalBody>
            <ModalFooter>

            </ModalFooter>
        </Modal>
    );
};

export default RegisterModal;