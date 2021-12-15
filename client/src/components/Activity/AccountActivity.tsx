import React from 'react';
import PageTitle from "../PageTitile/PageTitle";
import {Button, Table} from "reactstrap";
import {Link} from "react-router-dom";

const AccountActivity = () => {
    return (
        <div>
            <PageTitle title="Your account activity from the last 28 days"/>
            <div className="m-2">
                <Table striped>
                    <thead>
                    <tr>
                        <th>Date</th>
                        <th>Activity type</th>
                        <th>IP address</th>
                        <th>OS</th>
                        <th>Browser</th>
                    </tr>
                    </thead>
                    <tbody>
                    <tr>
                        <th scope="row">15.12.2021 - 16:13</th>
                        <td>Successful login</td>
                        <td>96.12.23.42</td>
                        <td>Windows 10</td>
                        <td>Chrome 96</td>
                    </tr>
                    <tr>
                        <th scope="row">14.12.2021 - 16:12</th>
                        <td>Failed login</td>
                        <td>86.12.22.49</td>
                        <td>Windows 11</td>
                        <td>Firefox 106</td>
                    </tr>
                    <tr>
                        <th scope="row">14.12.2021 - 16:14</th>
                        <td>Failed login</td>
                        <td>86.12.22.42</td>
                        <td>MacOS 12.1</td>
                        <td>Safari 9.1</td>
                    </tr>
                    </tbody>
                </Table>
                <div className="d-flex aligns-items-center justify-content-center">
                    <div className="m-1">See unfamiliar activity?</div>
                    <Button size="sm" className="ms-1"><Link to={"/settings#change-password"}>Change password</Link></Button>
                </div>

            </div>
        </div>
    );
};

export default AccountActivity;