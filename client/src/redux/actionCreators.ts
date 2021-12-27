import {Action, AuthState} from "./authenticationReducer";
import {Dispatch} from "react";

export const login = (user: AuthState) => {
    return (dispatch: Dispatch<Action>) => {
        dispatch({
            type: "login",
            payload: user
        });
    };
};

export const logout = () => {
    return (dispatch: Dispatch<Action>) => {
        dispatch({
            type: "logout"
        });
    };
};