import {createStore} from 'redux';
import * as api from "../api/apiCalls";
import authenticationReducer, {AuthState} from "./authenticationReducer";

const notLoggedInUser: AuthState = {
    email: '',
    userId: 0,
    username: '',
    isLoggedIn: false,
    accessToken: '',
    encryptionKey: '',
    refreshToken: ''
}

export const configureStore = () => {
    let loadedUserFromLocalStorage = checkIfUserDataAreStoredInLocalStorage();
    const state = loadedUserFromLocalStorage ? {...loadedUserFromLocalStorage} : {...notLoggedInUser};
    const store = createStore(authenticationReducer, state);

    store.subscribe(() => {
        localStorage.setItem('bitpass-user', JSON.stringify(store.getState()));
        const {isLoggedIn, accessToken} = store.getState();
        api.setAuthHeader({isLoggedIn, accessToken});
    });

    return store;
};

const checkIfUserDataAreStoredInLocalStorage = (): AuthState | null => {
    let loadedUserFromLocalStorage = localStorage.getItem('bitpass-user');
    if (loadedUserFromLocalStorage) {
        try {
            const temp: AuthState = JSON.parse(loadedUserFromLocalStorage);
            api.setAuthHeader({isLoggedIn: true, accessToken: temp.accessToken});
            return temp;
        } catch {
            return null;
        }
    }
    return null;
}