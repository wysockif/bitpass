import React from 'react';
import PageTitle from "../../components/PageTitile/PageTitle";
import {Card, CardBody, CardText, CardTitle} from "reactstrap";

const Faq = () => {
    return (
        <div>
            <PageTitle title="FAQ"/>
            <div className="m-3">
                <Card>
                    <CardBody>
                        <CardTitle tag="h5">
                            Can Bitpass see your passwords?
                        </CardTitle>
                        <CardText>
                            No. <br/>
                            Bitpass employs a "zero-knowledge" policy. Your passwords, master password and encryption
                            key are never received in plain text on the server - it is possible by encrypting your vault passwords
                            and hashing your encryption key before ever leaving your browser.
                            Having the encrypted passwords without the encryption key prevents the decryption on the
                            server. The encryption key is derived from your master password using Password-Based Key Derivation Function
                            2 (PBKDF2) -
                            it makes it harder for someone to guess your encryption key through a brute-force attack.
                        </CardText>
                    </CardBody>
                </Card>
                <Card className="mt-2">
                    <CardBody>
                        <CardTitle tag="h5">
                            What algorithms uses Bitpass?
                        </CardTitle>
                        <CardText>
                            <ul>
                                <li> <code>AES 256-bit</code> - encryption</li>
                                <li> <code>PBKDF2 (with salt)</code> - deriving encryption key</li>
                                <li> <code>BCrypt (with salt and pepper)</code> - hashing</li>
                            </ul>
                        </CardText>
                    </CardBody>
                </Card>

                <div className="ms-2 mt-2"><small className="text-muted">
                    <a href={"https://github.com/wysockif/bitpass"} target="_blank" rel="noreferrer" >Read more</a>
                </small></div>
            </div>
        </div>
    );
};

export default Faq;