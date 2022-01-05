import axios from 'axios';
import {getRefreshTokenFromLocalStorage, updateAccessAndRefreshTokensInLocalStorage} from "../tokens/tokenService";
import {store} from "../index";

const apiUrl = 'http://localhost:5000';

axios.interceptors.response.use(
    (res) => {
        return res;
    },
    async (err) => {
        if (!err.config.url.includes("login")
            && !err.config.url.includes("refresh-access-token")
            && !err.config.url.includes("register")
            && !err.config.url.includes("verify-email-address")
            && err.response) {
            if (err.response.status === 401 && !err.config._retry) {
                err.config._retry = true;

                const oldRefreshToken = getRefreshTokenFromLocalStorage();

                if (!oldRefreshToken) {
                    localStorage.removeItem('bitpass-user');
                    store.dispatch({type: "logout"});
                    return Promise.reject(err);
                }

                try {
                    console.log("old " + oldRefreshToken)
                    const response = await axios.post(apiUrl + "/api/accounts/refresh-access-token", {refreshToken: oldRefreshToken});
                    const newAccessToken = response.data.accessToken;
                    const newRefreshToken = response.data.refreshToken;
                    console.log("new " + newRefreshToken)
                    updateAccessAndRefreshTokensInLocalStorage(newAccessToken, newRefreshToken);
                    axios.defaults.headers.common['Authorization'] = `Bearer ${newAccessToken}`;
                    err.config.headers['Authorization'] = `Bearer ${newAccessToken}`;
                    if (err.config.url.includes("logout")) {
                        err.config.body = JSON.stringify({refreshToken: newRefreshToken})
                        err.config.data = JSON.stringify({refreshToken: newRefreshToken})
                    }
                } catch (e) {
                    localStorage.removeItem('bitpass-user');
                    store.dispatch({type: "logout"});
                    return Promise.reject(err);
                }

                return axios(err.config);
            } else {
                return Promise.reject(err);
            }
        } else {
            return Promise.reject(err);
        }
    });

export const setAuthHeader = (req: { isLoggedIn: boolean, accessToken: string }) => {
    if (req.accessToken) {
        axios.defaults.headers.common['Authorization'] = `Bearer ${req.accessToken}`;
    }
}

export const deleteAuthHeader = () => {
    delete axios.defaults.headers.common['Authorization'];
}

export const register = (registerUserRequest: { password: string; encryptionKeyHash: string; email: string; username: string }) => {
    return axios.post(apiUrl + '/api/accounts/register', registerUserRequest);
};

export const login = (loginUserRequest: { password: string; identifier: string }) => {
    return axios.post(apiUrl + '/api/accounts/login', loginUserRequest);
};

export const logout = () => {
    const refreshToken = getRefreshTokenFromLocalStorage();
    return axios.post(apiUrl + '/api/accounts/logout', {refreshToken})
}

export const verifyEmailAddress = (verifyEmailAddressRequest: { username: string; token: string }) => {
    return axios.post(apiUrl + '/api/accounts/verify-email-address', verifyEmailAddressRequest);
}

export const requestResetPassword = (requestResetPasswordRequest: { identifier: string }) => {
    return axios.post(apiUrl + '/api/accounts/request-password-reset', requestResetPasswordRequest);
}

export const requestEmailVerification = (requestEmailVerificationRequest: { identifier: string }) => {
    return axios.post(apiUrl + '/api/accounts/request-email-verification', requestEmailVerificationRequest);
}

export const resetPassword = (resetPasswordRequest: { username: string, resetPasswordToken: string, newPassword: string }) => {
    return axios.post(apiUrl + '/api/accounts/reset-password', resetPasswordRequest);
}

export const verifyEncryptionKey = (verifyEncryptionKeyRequest: { encryptionKeyHash: string }) => {
    return axios.post(apiUrl + '/api/accounts/verify-encryption-key', verifyEncryptionKeyRequest);
}

export const getVault = () => {
    return axios.get(apiUrl + '/api/vault');
}

export const addVaultItem = (addVaultItemRequest: {
    encryptionKeyHash: string, websiteUrl: string, encryptedPassword: string, identifier: string
}) => {
    return axios.post(apiUrl + '/api/vault', addVaultItemRequest);
}

export const getAccountActivities = () => {
    return axios.get(apiUrl + '/api/accounts/activities');
}

export const getActiveSessions = () => {
    return axios.get(apiUrl + '/api/accounts/active-sessions');
}

export const logoutAllSessions = () => {
    return axios.post(apiUrl + '/api/accounts/logout-all-sessions');
}

export const changePassword = (changePasswordRequest: { newPassword: string, oldPassword: string }) => {
    return axios.post(apiUrl + '/api/accounts/change-password', changePasswordRequest);
}