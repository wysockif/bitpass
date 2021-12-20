import React from 'react';
import PageTitle from "../../components/PageTitile/PageTitle";
import {Button, Table} from "reactstrap";
import {Link} from "react-router-dom";

const Sessions = () => {
    return (
        <div>
            <PageTitle title="Your active sessions"/>
            <div className="m-2">
                <Table striped>
                    <thead>
                    <tr>
                        <th>IP address</th>
                        <th>OS</th>
                        <th>Browser</th>
                    </tr>
                    </thead>
                    <tbody>
                    <tr>
                        <td>96.12.23.42</td>
                        <td>Windows 10</td>
                        <td>Chrome 96</td>
                    </tr>
                    <tr>
                        <td>86.12.22.49</td>
                        <td>Windows 11</td>
                        <td>Firefox 106</td>

                    </tr>
                    <tr>
                        <td>86.12.22.42</td>
                        <td>MacOS 12.1</td>
                        <td>Safari 9.1</td>
                    </tr>
                    </tbody>
                </Table>
                <div className="d-flex aligns-items-center justify-content-center">
                    <div className="m-1">See unfamiliar session?</div>
                    <Button size="sm" className="ms-1"><Link to={"/settings#log-out-of-all-sessions"}>Log out of all sessions</Link></Button>
                </div>
            </div>
        </div>
    );
};

export default Sessions;