import React from 'react';
import {Button, FormGroup, Input, Label, Modal, ModalBody, ModalFooter, ModalHeader} from "reactstrap";

interface AddItemModalProps {
    isOpen: boolean
    toggle: () => void
}

const AddItemModal = (props: AddItemModalProps) => {
    return (
        <Modal isOpen={props.isOpen} toggle={props.toggle} centered>
            <ModalHeader
                toggle={props.toggle}>Add a new item</ModalHeader>
            <ModalBody>
                <FormGroup>
                    <Label for="website" className="mt-3">
                        Website URL:
                    </Label>
                    <Input
                        id="website"
                        name="website"
                        placeholder="Enter website URL"
                        type="text"
                    />
                    <Label for="identifier" className="mt-3">
                        Email or username:
                    </Label>
                    <Input
                        id="identifier"
                        name="identifier"
                        placeholder="Enter email address or username"
                        type="text"
                    />
                    <Label for="password" className="mt-3">
                        Password:
                    </Label>
                    <Input
                        id="password"
                        name="Password"
                        placeholder="Enter password"
                        type="password"
                    />
                </FormGroup>
            </ModalBody>
            <ModalFooter>
                <Button onClick={props.toggle}>Add</Button>
            </ModalFooter>
        </Modal>
    );
};

export default AddItemModal;