import React, {useEffect, useState} from 'react';
import PageTitle from "../../components/PageTitile/PageTitle";
import {Button, Spinner, Table} from "reactstrap";
import {Link} from "react-router-dom";
import SingleActivity from "./SingleActivity";
import * as api from "../../api/apiCalls";

export interface ActivityItem {
    createdAt: Date,
    osName: string,
    browserName: string
    activityType: string,
    ipAddress: string,
}

const AccountActivity = () => {
    const [activities, setActivities] = useState<ActivityItem[]>([]);
    const [componentDidMount, setComponentDidMount] = useState<boolean>(false);
    const [error, setError] = useState<string>('');
    const [ongoingApiCall, setOngoingApiCall] = useState<boolean>(true);

    useEffect(() => {
        if (!componentDidMount) {
            setError('');
            setOngoingApiCall(true);
            api.getAccountActivities()
                .then(response => {
                    setComponentDidMount(true);
                    setOngoingApiCall(false);
                    setActivities(response.data.items);
                })
                .catch(() => {
                    setOngoingApiCall(false)
                    setError("An error occurred, please try again later");
                });
        }
    }, [componentDidMount, activities]);

    return (
        <div>
            <PageTitle title="Your account activity from the last 14 days"/>

            <div className="m-2">
                {ongoingApiCall && !error && <div className="text-center mt-3"><Spinner animation="border" size="sm" role="status">
                    Loading...
                </Spinner></div>}
                {!ongoingApiCall && <Table striped>
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
                    {activities.length === 0 && <tr>
                        <th scope="row">Loading...</th>
                        <td>Loading...</td>
                        <td>Loading...</td>
                        <td>Loading...</td>
                        <td>Loading...</td>
                    </tr>}
                    {activities.map(a =>
                        <SingleActivity
                            key={a.createdAt.toLocaleString()}
                            activityType={a.activityType}
                            browserName={a.browserName}
                            ipAddress={a.ipAddress}
                            createdAt={a.createdAt}
                            osName={a.osName}
                        />)}
                    </tbody>
                </Table>}
                <div className="d-flex aligns-items-center justify-content-center">
                    <div className="m-1">See unfamiliar activity?</div>
                    <Button size="sm" className="ms-1"><Link to={"/settings#change-password"}>Change password</Link></Button>
                </div>

            </div>
        </div>
    );
};

export default AccountActivity;