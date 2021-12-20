import React from 'react';
import {Button, FormGroup, Input, Label, Modal, ModalBody, ModalFooter, ModalHeader} from "reactstrap";

const LoginModal = (props: any) => {
    return (
        <Modal isOpen={props.loginModal} toggle={props.toggleLoginModal} centered>
            <ModalHeader
                toggle={props.toggleLoginModal}>Sign in</ModalHeader>
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
                    <Label for="password" className="mt-3">
                        Password:
                    </Label>
                    <Input
                        id="password"
                        name="Password"
                        placeholder="Your password"
                        type="password"
                    />
                </FormGroup>
            </ModalBody>
            <ModalFooter>
                <Button color="primary" onClick={props.toggleLoginModal}>Submit</Button>
            </ModalFooter>
        </Modal>
    );
};

export default LoginModal;