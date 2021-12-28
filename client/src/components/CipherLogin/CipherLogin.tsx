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
        setIsPasswordRevealed(false);
        setIsModalOpen(true);
    }

    function revealPassword() {
        if (isPasswordRevealed) {
            return " " + decryptPassword(props.encryptedPassword, encryptionKey);
        }
    }

    const onToggleModal = () => {
        setIsModalOpen(!isModalOpen)
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
            <Modal isOpen={isModalOpen} toggle={onToggleModal} centered>
                <ModalHeader toggle={onToggleModal}>{props.url}</ModalHeader>
                <ModalBody>
                    <div>Username or email: <code>{props.identifier}</code>
                        <span className="btn btn-outline-secondary py-0 float-end" onClick={() => {
                            return;
                        }}>
                            copy
                        </span>
                    </div>
                    <div className="mt-2">Password:
                        {isPasswordRevealed && <code>
                            {revealPassword()}
                        </code>}
                        {!isPasswordRevealed && <code>
                            {" *****************"}
                        </code>}
                        <span className="btn btn-outline-secondary py-0 float-end" onClick={() => {
                            return;
                        }}>copy</span>
                        {!isPasswordRevealed &&
                        <span className="btn btn-outline-secondary py-0 float-end mx-1"
                              onClick={() => setIsPasswordRevealed(true)}>
                            reveal
                        </span>}
                    </div>
                </ModalBody>
            </Modal>
        </div>
    );
};

export default CipherLogin;