import React, {useState} from 'react';
import PageTitle from "../../components/PageTitile/PageTitle";
import {Button} from "reactstrap";
import AddItemModal from "./AddItemModal";
import VerifyMasterPassword from "./VerifyMasterPassword";

const Vault = () => {
    const [isAddItemModalOpen, setAddItemModalOpen] = useState<boolean>(false);
    const [isMasterPasswordVerified, setMasterPasswordVerified] = useState<boolean>(false);
    const toggleAddItemModal = () => setAddItemModalOpen(!isAddItemModalOpen);

    return (
        <div>
            <PageTitle title="Your vault"/>
            <VerifyMasterPassword isOpen={!isMasterPasswordVerified} toggle={() => console.log()} />
            <AddItemModal isOpen={isAddItemModalOpen} toggle={toggleAddItemModal}/>

            <div className="m-3">
                <div className="text-end"><Button className="py-1" onClick={toggleAddItemModal}>Add an item</Button></div>
            </div>
        </div>
    );
};

export default Vault;