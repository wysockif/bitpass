import React from 'react';

interface Props {
    title: string,
}

const PageTitle = (props: Props) => {
    return (
        <div className="py-4 page-title" style={{backgroundColor: "#39667C"}}>
            <h5 className="text-light ms-4">{props.title}</h5>
        </div>
    );
};

export default PageTitle;