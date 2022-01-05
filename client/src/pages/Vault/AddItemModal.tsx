import React, {useState} from 'react';
import {FormGroup, Input, Label, Modal, ModalBody, ModalFooter, ModalHeader} from "reactstrap";
import {useSelector} from "react-redux";
import {AuthState} from "../../redux/authenticationReducer";
import {hashDerivationKey} from "../../security/KeyDerivation";
import {encryptPassword} from "../../security/Encryption";
import * as api from "../../api/apiCalls";
import ButtonWithSpinner from "../../components/ButtonWithSpinner/ButtonWithSpinner";
import PasswordInput from "../../components/PasswordInput/PasswordInput";


interface AddItemModalProps {
    isOpen: boolean
    toggle: () => void
    afterAdd: () => void
}

const AddItemModal = (props: AddItemModalProps) => {
    const [websiteUrl, setWebsiteUrl] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [identifier, setIdentifier] = useState<string>('');
    const [fieldErrors, setFieldErrors] = useState<any>([]);
    const [ongoingApiCall, setOngoingApiCall] = useState<boolean>(false);
    const reduxState = useSelector((state: AuthState) => state);


    const onChangeIdentifier = (ev: React.ChangeEvent<HTMLInputElement>) => {
        if (identifier !== ev.target.value.trim()) {
            const err = {...fieldErrors};
            delete err.Username;
            setFieldErrors(err);
            setIdentifier(ev.target.value.trim());
        }
    }

    const onChangePassword = (ev: React.ChangeEvent<HTMLInputElement>) => {
        if (password !== ev.target.value.trim()) {
            const err = {...fieldErrors};
            delete err.Password;
            setFieldErrors(err);
            setPassword(ev.target.value.trim());
        }
    }

    const onChangeWebsiteUrl = (ev: React.ChangeEvent<HTMLInputElement>) => {
        if (websiteUrl !== ev.target.value.trim()) {
            const err = {...fieldErrors};
            delete err.Password;
            setFieldErrors(err);
            setWebsiteUrl(ev.target.value.trim());
        }
    }

    const onToggle = () => {
        setWebsiteUrl('');
        setIdentifier('');
        setPassword('');
        props.toggle();
    }

    const onClickAddButton = () => {
        setOngoingApiCall(true);
        const encryptionKeyHash = hashDerivationKey(reduxState.encryptionKey, reduxState.email);
        const encryptedPassword = encryptPassword(password, reduxState.encryptionKey);

        api.addVaultItem({identifier, websiteUrl, encryptionKeyHash, encryptedPassword})
            .then(response => {
                props.toggle();
                props.afterAdd();
                setOngoingApiCall(false);
            });
    }

    return (
        <Modal isOpen={props.isOpen} toggle={onToggle} centered>
            <ModalHeader toggle={onToggle}>Add a new item</ModalHeader>
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
                        value={websiteUrl}
                        onChange={onChangeWebsiteUrl}
                    />
                    <Label for="identifier" className="mt-3">
                        Email or username:
                    </Label>
                    <Input
                        id="identifier"
                        name="identifier"
                        placeholder="Enter email address or username"
                        type="text"
                        value={identifier}
                        onChange={onChangeIdentifier}
                    />
                    <Label for="password" className="mt-3">
                        Password:
                    </Label>
                    <PasswordInput
                        id="password"
                        name="Password"
                        placeholder="Enter password"
                        password={password}
                        onChangePassword={onChangePassword}
                    />
                </FormGroup>
            </ModalBody>
            <ModalFooter>
                <ButtonWithSpinner onClick={onClickAddButton} disabled={!websiteUrl || !password || !identifier}
                                   content={"Add"} ongoingApiCall={ongoingApiCall} />
            </ModalFooter>
        </Modal>
    );
};

export default AddItemModal;