import React, {useState} from 'react';
import {Button} from "reactstrap";
import LoginModal from "./LoginModal";
import RegisterModal from "./RegisterModal";

const Auth = () => {
    const [registerModal, setRegisterModal] = useState<boolean>(false);
    const [loginModal, setLoginModal] = useState<boolean>(false);
    const toggleRegisterModal = () => setRegisterModal(!registerModal);
    const toggleLoginModal = () => setLoginModal(!loginModal);

    return (
        <div className="first-page supreme-container">
            <div className="sidebar-header text-center py-4 myModal">
                <h3>Bitpass app</h3>
            </div>
            <div className="text-light">Welcome to Bitpass</div>

            <div className="text-center">
                <Button className="mx-1 px-3" color="danger" onClick={toggleRegisterModal}>Sign up</Button>
                <Button className="mx-1 px-3" color="danger" onClick={toggleLoginModal}>Sign in</Button>
            </div>

            <RegisterModal toggleRegisterModal={toggleRegisterModal} registerModal={registerModal} />
            <LoginModal toggleLoginModal={toggleLoginModal} loginModal={loginModal}/>

        </div>
    );
};

export default Auth;