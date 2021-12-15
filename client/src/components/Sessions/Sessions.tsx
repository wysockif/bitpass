import React from 'react';
import PageTitle from "../PageTitile/PageTitle";
import {Button, Table} from "reactstrap";

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
                        <th>Close session</th>
                    </tr>
                    </thead>
                    <tbody>
                    <tr>
                        <td>96.12.23.42</td>
                        <td>Windows 10</td>
                        <td>Chrome 96</td>
                        <th><Button className="px-3 py-1">Close</Button></th>
                    </tr>
                    <tr>
                        <td>86.12.22.49</td>
                        <td>Windows 11</td>
                        <td>Firefox 106</td>
                        <th><Button className="px-3 py-1">Close</Button></th>

                    </tr>
                    <tr>
                        <td>86.12.22.42</td>
                        <td>MacOS 12.1</td>
                        <td>Safari 9.1</td>
                        <th><Button className="px-3 py-1">Close</Button></th>
                    </tr>
                    </tbody>
                </Table>
            </div>
        </div>
    );
};

export default Sessions;