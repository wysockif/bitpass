import React, {useEffect, useState} from 'react';
import {Link, useParams} from "react-router-dom";
import * as api from "../../api/apiCalls";
import {Spinner} from "reactstrap";
import {faSignOutAlt} from "@fortawesome/free-solid-svg-icons";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";


const VerifyEmailAddress = () => {
    const [ongoingApiCall, setOngoingApiCall] = useState<boolean>(false);
    const [error, setError] = useState<string>('');
    const [message, setMessage] = useState<string>('Email verified!');
    const {token, username} = useParams();

    useEffect(() => {
        if (token && username) {
            setOngoingApiCall(true);
            setMessage('');
            setError('');
            api.verifyEmailAddress({username, token})
                .then(r => {
                    setOngoingApiCall(false)
                    setMessage('Email verified!');
                })
                .catch(error => {
                    setOngoingApiCall(false);
                    if (error?.response?.data?.errors?.Username || error?.response.data?.errors?.Token) {
                        setError("Invalid link");
                    } else if (error.response?.data) {
                        setError(error.response?.data);
                    } else {
                        setError("An error occurred, please try again later")
                    }
                })
        }
    }, [token, username]);

    return (
        <div>
            <div className="sidebar-header text-center py-4 myModal">
                <h3><Link to="/login">Bitpass app</Link></h3>
            </div>
            <div className="text-center mt-3">
                {error && <div>
                    <div className="text-danger">{error}</div>
                    <small className="mt-2 text-muted"><Link to="/request-email-verification">Click here to send email
                        verification link again</Link></small>
                </div>}
                {ongoingApiCall && !error && <Spinner animation="border" size="sm" role="status" className="ms-1">
                    Loading...
                </Spinner>}
                {message && <div>
                    <div className="text-success">{message}</div>
                    <small className="mt-2 text-muted"><Link to="/login">
                        <FontAwesomeIcon icon={faSignOutAlt} className="me-1"/>
                        Click here to login</Link></small>
                </div>}
            </div>
        </div>
    );
};

export default VerifyEmailAddress;