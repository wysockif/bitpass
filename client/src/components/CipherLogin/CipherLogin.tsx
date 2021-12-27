import React, {useState} from 'react';
import {ListGroupItem, Modal, ModalBody, ModalHeader} from "reactstrap";
import HorizontalLine from "../HorizontalLine/HorizontalLine";
import {VaultItem} from "../../pages/Vault/Vault";
import {decryptPassword} from "../../security/Encryption";
import {useSelector} from "react-redux";
import {AuthState} from "../../redux/authenticationReducer";

const CipherLogin = (props: VaultItem) => {
    const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
    const [isPasswordRevealed, setIsPasswordRevealed] = useState<boolean>(false);
    const encryptionKey = useSelector((state: AuthState) => state.encryptionKey)


    function onClickItem() {
        setIsModalOpen(true);
    }

    function revealPassword() {
        if (isPasswordRevealed) {
            return decryptPassword(props.encryptedPassword, encryptionKey);
        }
    }

    return (
        <div>
            <div className="p-2" style={{cursor: "pointer"}} onClick={onClickItem}>
                <ListGroupItem action className="rounded">
                    <h6 className="fw-bold">{props.url}</h6>
                    <HorizontalLine/>
                    <div><small className="text-muted">username or email: <span
                        className="fw-bold">{props.identifier}</span></small></div>
                    <div><small className="text-muted">password: <span
                        className="fw-bold">*****************</span></small>
                    </div>
                </ListGroupItem>
            </div>
            <Modal isOpen={isModalOpen} toggle={() => setIsModalOpen(!isModalOpen)} centered>
                <ModalHeader toggle={() => setIsModalOpen(!isModalOpen)}>{props.url}</ModalHeader>
                <ModalBody>
                    <div>Username or email: {props.identifier}</div>
                    <div>Password:
                        {!isPasswordRevealed && <span style={{cursor: "pointer"}} onClick={() => setIsPasswordRevealed(true)}>
                            Click here to reveal
                        </span>}
                        {isPasswordRevealed && <span>
                            {revealPassword()}
                        </span>}
                    </div>
                </ModalBody>
            </Modal>
        </div>
    );
};

export default CipherLogin;