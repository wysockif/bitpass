import React from 'react';
import PageTitle from "../PageTitile/PageTitle";
import HorizontalLine from "../HorizontalLine/HorizontalLine";
import {Button} from "reactstrap";

const Settings = () => {
    return (
        <div>
            <PageTitle title="Settings"/>
            <div className="p-3">
                <h6>Change password</h6>
                Bla bla bla bla
                <br/>
                <Button>Hi</Button>

                <HorizontalLine/>
            </div>
        </div>
    );
};

export default Settings;