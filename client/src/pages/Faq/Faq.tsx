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
                            No.
                        </CardText>
                    </CardBody>
                </Card>
            </div>
        </div>
    );
};

export default Faq;