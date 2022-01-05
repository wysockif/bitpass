import React, {useState} from 'react';
import {Input} from "reactstrap";
import {InputType} from "reactstrap/types/lib/Input";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faEye, faEyeSlash} from "@fortawesome/free-solid-svg-icons";


const PasswordInput = (props: {
    password: string, onChangePassword: (ev: React.ChangeEvent<HTMLInputElement>) => void, id: string, name: string, placeholder: string
}) => {
    const [isPasswordShown, setIsPasswordShown] = useState<boolean>(false);
    let type: InputType = isPasswordShown ? "text" : "password";

    const onClickEyeIcon = () => {
        setIsPasswordShown(!isPasswordShown);
    }
    return (
        <div>
            <Input type={type} onChange={props.onChangePassword} id={props.id} placeholder={props.placeholder}
                   name={props.name} value={props.password}/>
            <span onClick={onClickEyeIcon} className="text-muted ms-1 mt-1" style={{cursor: "pointer", fontSize: "12px"}}>
                {!isPasswordShown && <span>Show password: <FontAwesomeIcon className="ms-1 mt-1" icon={faEye}/></span>}
                {isPasswordShown &&
                <span>Hide password: <FontAwesomeIcon className="ms-1 mt-1" icon={faEyeSlash}/></span>}
            </span>
        </div>
    );
};

export default PasswordInput;