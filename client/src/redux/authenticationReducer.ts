import {Reducer} from "react";

export interface AuthState {
    isLoggedIn: boolean,
    encryptionKey: string,
    accessToken: string,
    refreshToken: string
    userId: number,
    username: string,
    email: string
}

export interface Action {
    type: string,
    payload?: AuthState
}

export const initialState: AuthState = {
    email: '',
    userId: 0,
    username: '',
    accessToken: '',
    refreshToken: '',
    isLoggedIn: false,
    encryptionKey: ''
}

const authenticationReducer: Reducer<any, Action> = (state: AuthState = initialState, action: Action) => {
    switch (action.type) {
        case 'login':
            return {...action.payload};
        case 'verify-master-password':
            return {...action.payload};
        case 'logout':
            return {...initialState};
        default:
            return state;
    }
}

export default authenticationReducer;