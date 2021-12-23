import React from 'react';
import {Button, Spinner} from "reactstrap";

interface ButtonWithSpinnerProps {
    onClick: (ev: any) => void,
    disabled: boolean,
    className?: string,
    content: string,
    ongoingApiCall: boolean
}

const ButtonWithSpinner = (props: ButtonWithSpinnerProps) => {
    return (
        <Button
            style={{minWidth: "200px"}}
            variant="outline-light"
            onClick={props.onClick}
            disabled={props.disabled}
            className={props.className}
        >
            {props.content}
            {props.ongoingApiCall && <Spinner animation="border" size="sm" role="status" className="ms-1">
               Loading...
            </Spinner>}
        </Button>
    );
};
export default ButtonWithSpinner;