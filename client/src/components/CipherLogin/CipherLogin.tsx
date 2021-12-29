import React, {useState} from 'react';
import {Card, ListGroupItem, Modal, ModalBody, ModalHeader} from "reactstrap";
import HorizontalLine from "../HorizontalLine/HorizontalLine";
import {VaultItem} from "../../pages/Vault/Vault";
import {decryptPassword} from "../../security/Encryption";
import {useSelector} from "react-redux";
import {AuthState} from "../../redux/authenticationReducer";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faClipboard, faClipboardCheck, faExternalLinkAlt, faEye, faEyeSlash} from "@fortawesome/free-solid-svg-icons";

const CipherLogin = (props: VaultItem) => {
    const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
    const [isPasswordCopied, setIsPasswordCopied] = useState<boolean>(false);
    const [isIdentifierCopied, setIsIdentifierCopied] = useState<boolean>(false);
    const [isPasswordRevealed, setIsPasswordRevealed] = useState<boolean>(false);
    const encryptionKey = useSelector((state: AuthState) => state.encryptionKey)

    function onClickItem() {
        setIsPasswordRevealed(false);
        setIsModalOpen(true);
    }

    function revealPassword() {
        if (isPasswordRevealed) {
            return decryptPassword(props.encryptedPassword, encryptionKey);
        }
    }

    const onToggleModal = () => {
        setIsModalOpen(!isModalOpen)
    }

    const copyPasswordToClipBoard = () => {
        const encryptedPassword = decryptPassword(props.encryptedPassword, encryptionKey);
        navigator.clipboard.writeText(encryptedPassword).then();
        setIsPasswordCopied(true);
        setIsIdentifierCopied(false);
    }

    const copyIdentifierToClipBoard = () => {
        navigator.clipboard.writeText(props.identifier).then();
        setIsIdentifierCopied(true);
        setIsPasswordCopied(false);
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
                <ModalHeader toggle={onToggleModal}>
                    {props.url}
                    <a href={props.url} style={{cursor: "pointer"}} target="_blank" rel="noreferrer">
                        <FontAwesomeIcon icon={faExternalLinkAlt} className="ms-2"/>
                    </a>

                </ModalHeader>
                <ModalBody>
                    <div>Username or email:
                        <Card className="ms-2 px-1 d-inline"><code>{props.identifier}</code></Card>
                        <span className="btn btn-outline-secondary py-0 float-end" onClick={copyIdentifierToClipBoard}>
                            {!isIdentifierCopied && <FontAwesomeIcon icon={faClipboard}/>}
                            {isIdentifierCopied && <FontAwesomeIcon icon={faClipboardCheck}/>}
                        </span>
                    </div>
                    <div className="mt-2">Password:
                        {isPasswordRevealed && <Card className="ms-2 px-1 d-inline"><code>
                            {revealPassword()}
                        </code></Card>}
                        {!isPasswordRevealed && <Card className="ms-2 px-1 d-inline"><code>
                            {"*****************"}
                        </code></Card>}
                        <span className="btn btn-outline-secondary py-0 float-end" onClick={copyPasswordToClipBoard}>
                            {!isPasswordCopied && <FontAwesomeIcon icon={faClipboard}/>}
                            {isPasswordCopied && <FontAwesomeIcon icon={faClipboardCheck}/>}
                        </span>
                        <span className="btn btn-outline-secondary py-0 float-end mx-1"
                              onClick={() => setIsPasswordRevealed(!isPasswordRevealed)}>
                            {!isPasswordRevealed && <FontAwesomeIcon icon={faEye}/>}
                            {isPasswordRevealed && <FontAwesomeIcon icon={faEyeSlash}/>}
                        </span>
                    </div>
                </ModalBody>
            </Modal>
        </div>
    );
};

export default CipherLogin;