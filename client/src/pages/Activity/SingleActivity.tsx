import React from 'react';
import {ActivityItem} from "./AccountActivity";

const SingleActivity = (props: ActivityItem) => {

    return (
        <tr>
            <th scope="row">{new Date(props.createdAt).toLocaleString()}</th>
            <td>{props.activityType}</td>
            <td>{props.ipAddress}</td>
            <td>{props.osName}</td>
            <td>{props.browserName}</td>
        </tr>
    );
};

export default SingleActivity;