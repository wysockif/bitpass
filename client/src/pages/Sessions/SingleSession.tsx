import React from 'react';
import {SessionItem} from "./Sessions";

const SingleSession = (props: SessionItem) => {
    return (
        <tr>
            <td>{props.ipAddress}</td>
            <td>{props.osName}</td>
            <td>{props.browserName}</td>
        </tr>
    );
};

export default SingleSession;