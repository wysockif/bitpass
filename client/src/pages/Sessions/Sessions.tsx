import React, {useEffect, useState} from 'react';
import PageTitle from "../../components/PageTitile/PageTitle";
import {Button, Spinner, Table} from "reactstrap";
import {Link} from "react-router-dom";
import SingleSession from "./SingleSession";
import * as api from "../../api/apiCalls";

export interface SessionItem {
    id: number,
    expirationUnixTimestamp: number,
    browserName: string
    osName: string,
    ipAddress: string,
}

const Sessions = () => {
    const [sessions, setSessions] = useState<SessionItem[]>([]);
    const [componentDidMount, setComponentDidMount] = useState<boolean>(false);
    const [error, setError] = useState<string>('');
    const [ongoingApiCall, setOngoingApiCall] = useState<boolean>(true);

    useEffect(() => {
        if (!componentDidMount) {
            setError('');
            setOngoingApiCall(true);
            api.getActiveSessions()
                .then(response => {
                    setComponentDidMount(true);
                    setOngoingApiCall(false);
                    setSessions(response.data.items);
                })
                .catch(() => {
                    setOngoingApiCall(false)
                    setError("An error occurred, please try again later");
                });
        }
    }, [componentDidMount, sessions]);

    return (
        <div>
            <PageTitle title="Your active sessions"/>
            <div className="m-2">
                {ongoingApiCall && !error &&
                <div className="text-center mt-3"><Spinner animation="border" size="sm" role="status">
                    Loading...
                </Spinner></div>}
                {!ongoingApiCall && <Table striped>
                    <thead>
                    <tr>
                        <th>IP address</th>
                        <th>OS</th>
                        <th>Browser</th>
                    </tr>
                    </thead>
                    <tbody>
                    {sessions.map(s =>
                        <SingleSession
                            key={s.id}
                            id={s.id}
                            expirationUnixTimestamp={s.expirationUnixTimestamp}
                            browserName={s.browserName}
                            osName={s.osName}
                            ipAddress={s.ipAddress}
                        />)}
                    </tbody>
                </Table>}
                <div className="d-flex aligns-items-center justify-content-center">
                    <div className="m-1">See unfamiliar session?</div>
                    <Button size="sm" className="ms-1"><Link to={"/settings#log-out-of-all-sessions"}>Log out of all
                        sessions</Link></Button>
                </div>
            </div>
        </div>
    );
};

export default Sessions;