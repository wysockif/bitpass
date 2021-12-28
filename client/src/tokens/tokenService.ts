import {AuthState} from "../redux/authenticationReducer";

export const getRefreshTokenFromLocalStorage = () => {
    let loadedUserFromLocalStorage = localStorage.getItem('bitpass-user');
    if (loadedUserFromLocalStorage) {
        try {
            const temp: AuthState = JSON.parse(loadedUserFromLocalStorage);
            return temp.refreshToken;
        } catch {
            return null;
        }
    }
}

export const updateAccessAndRefreshTokensInLocalStorage = (newAccessToken: string, newRefreshToken: string) => {
    let loadedUserFromLocalStorage = localStorage.getItem('bitpass-user');
    if (loadedUserFromLocalStorage) {
        try {
            const loaded: AuthState = JSON.parse(loadedUserFromLocalStorage);
            loaded.accessToken = newAccessToken;
            loaded.refreshToken = newRefreshToken;
            localStorage.setItem('bitpass-user', JSON.stringify(loaded));
        } catch {
            return null;
        }
    }
}



