import React from 'react';
import PageTitle from "../PageTitile/PageTitle";

interface Props {
    username: string,
}

const Welcome = (props: Props) => {
    let title: string = `Welcome @${props.username}`;

    return (
        <div>
            <PageTitle title={title}/>

        </div>
    );
};

export default Welcome;