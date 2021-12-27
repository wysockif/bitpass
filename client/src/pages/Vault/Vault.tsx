import React, {useEffect, useState} from 'react';
import PageTitle from "../../components/PageTitile/PageTitle";
import {Button, Col, Row} from "reactstrap";
import AddItemModal from "./AddItemModal";
import * as api from "../../api/apiCalls";
import VerifyMasterPassword from "./VerifyMasterPassword";
import CipherLogin from "../../components/CipherLogin/CipherLogin";

export interface VaultItem {
    id: number
    url: string,
    encryptedPassword: string,
    identifier: string
}

const Vault = () => {
    const [isAddItemModalOpen, setAddItemModalOpen] = useState<boolean>(false);
    const toggleAddItemModal = () => setAddItemModalOpen(!isAddItemModalOpen);
    const [vault, setVault] = useState<VaultItem[]>([]);
    const [componentDidMount, setComponentDidMount] = useState<boolean>(false);

    useEffect(() => {
        if (!componentDidMount) {
            api.getVault()
                .then(response => {
                    console.log("response");
                    console.log(response);
                    setComponentDidMount(true);
                    setVault(response.data.items);
                })
                .catch(error => {
                    console.log("error")
                    console.log(error)
                });
        }
    }, [componentDidMount, vault]);

    return (
        <div>
            <PageTitle title="Your vault"/>
            <VerifyMasterPassword/>
            <AddItemModal isOpen={isAddItemModalOpen} toggle={toggleAddItemModal}
                          afterAdd={() => setComponentDidMount(false)}
            />
            <Row className="m-3">
                <Col sm="9" className="mt-3">
                    {vault.length === 0 && <div className="text-center">Your vault is empty</div>}
                    {vault.length > 0 && <div>
                        {vault.map(item => (<CipherLogin
                            key={item.id}
                            id={item.id}
                            url={item.url}
                            encryptedPassword={item.encryptedPassword}
                            identifier={item.identifier}
                        />))}
                    </div>}
                </Col>
                <Col sm="3">
                    <div><Button className=" mt-4 py-1" onClick={toggleAddItemModal}>Add an item</Button></div>
                </Col>
            </Row>

        </div>
    );
};

export default Vault;