import axios from 'axios';

const apiUrl = 'http://localhost:5000';


export const setAuthHeader = (req: {isLoggedIn: boolean, accessToken: string}) => {
    if (req) {
        axios.defaults.headers.common['Authorization'] = `Bearer ${req.accessToken}`;
    } else {
        delete axios.defaults.headers.common['Authorization'];
    }
}


export const register = (registerUserRequest: { password: string; encryptionKeyHash: string; email: string; username: string }) => {
    return axios.post(apiUrl + '/api/accounts/register', registerUserRequest);
};

export const login = (loginUserRequest: { password: string; identifier: string }) => {
    return axios.post(apiUrl + '/api/accounts/login', loginUserRequest);
};

export const verifyEmailAddress = (verifyEmailAddressRequest: {username: string; token: string}) => {
    return axios.post(apiUrl + '/api/accounts/verify-email-address', verifyEmailAddressRequest);
}

export const requestResetPassword = (requestResetPasswordRequest: {identifier: string}) => {
    return axios.post(apiUrl + '/api/accounts/request-password-reset', requestResetPasswordRequest);
}

export const verifyEncryptionKey = (verifyEncryptionKeyRequest: {encryptionKeyHash: string}) => {
    return axios.post(apiUrl + '/api/accounts/verify-encryption-key', verifyEncryptionKeyRequest);
}
