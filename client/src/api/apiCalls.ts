import axios from 'axios';
import {
    getRefreshTokenFromLocalStorage,
    updateAccessTokenInLocalStorage,
    updateRefreshTokenInLocalStorage
} from "../tokens/tokenService";

const apiUrl = 'http://localhost:5000';

axios.interceptors.response.use(
    (res) => {
        return res;
    },
    async (err) => {
        if (err.config.url !== "api/accounts/login" && err.response) {
            if (err.response.status === 401 && !err.config._retry) {
                err.config._retry = true;
                const response = await axios.post(apiUrl + "/api/accounts/refresh-access-token", {refreshToken: getRefreshTokenFromLocalStorage()});
                if (!response.data) {
                    localStorage.removeItem('bitpass-user');
                    return Promise.reject(err);
                }
                const newAccessToken = response.data.accessToken;
                const newRefreshToken = response.data.refreshToken;
                updateAccessTokenInLocalStorage(newAccessToken);
                updateRefreshTokenInLocalStorage(newRefreshToken);
                axios.defaults.headers.common['Authorization'] = `Bearer ${newAccessToken}`;
                err.config.headers['Authorization'] = `Bearer ${newAccessToken}`;
                return axios(err.config);
            }
        }
    });

export const setAuthHeader = (req: { isLoggedIn: boolean, accessToken: string }) => {
    if (req) {
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

export const logout = (logoutRequest: { refreshToken: string }) => {
    return axios.post(apiUrl + '/api/accounts/logout', logoutRequest)
}

export const verifyEmailAddress = (verifyEmailAddressRequest: { username: string; token: string }) => {
    return axios.post(apiUrl + '/api/accounts/verify-email-address', verifyEmailAddressRequest);
}

export const requestResetPassword = (requestResetPasswordRequest: { identifier: string }) => {
    return axios.post(apiUrl + '/api/accounts/request-password-reset', requestResetPasswordRequest);
}

export const verifyEncryptionKey = (verifyEncryptionKeyRequest: { encryptionKeyHash: string }) => {
    return axios.post(apiUrl + '/api/accounts/verify-encryption-key', verifyEncryptionKeyRequest);
}

export const getVault = () => {
    return axios.get(apiUrl + '/api/vault')
}

export const addVaultItem = (addVaultItemRequest: {
    encryptionKeyHash: string, websiteUrl: string, encryptedPassword: string, identifier: string
}) => {
    return axios.post(apiUrl + '/api/vault', addVaultItemRequest);
}