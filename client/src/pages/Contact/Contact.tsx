import React, {useState} from 'react';
import PageTitle from "../../components/PageTitile/PageTitle";
import {Card, CardBody, CardText, CardTitle, ListGroupItem} from "reactstrap";

const Contact = () => {
    const email: string = 'bitpass.app@gmail.com';
    const [copied, setCopied] = useState<boolean>(false);

    const copyToClipBoard = () => {
        setCopied(true);
        navigator.clipboard.writeText(email).then();
    }

    return (
        <div>
            <PageTitle title="Contact"/>
            <div className="m-3">
                <Card>
                    <CardBody>
                        <CardTitle tag="h5" className="ms-3">
                            Email address
                        </CardTitle>
                        <CardText tag="div">
                                <div className="ms-1 p-2 d-inline-flex" onClick={() => copyToClipBoard()}
                                     style={{cursor: "pointer"}}>
                                    <ListGroupItem action className="rounded">
                                        {!copied &&
                                        <span style={{fontSize: "10px"}} className="text-muted">Click to copy to clipboard</span>}
                                        {copied &&
                                        <span style={{fontSize: "10px"}}
                                              className="text-muted">Copied to clipboard!</span>}
                                        <div>
                                            <code className="text-danger">{email}</code>
                                        </div>
                                    </ListGroupItem>
                                </div>
                        </CardText>
                        <div className="ms-3">
                            <small className="text-muted">Franciszek Wysocki</small>
                        </div>
                    </CardBody>
                </Card>
            </div>
        </div>
);
};

export default Contact;