import axios from 'axios';
import {RegisterUserDto} from "./RegisterUserDto";

const apiUrl = 'http://localhost:5000/api';

export const setAuthHeader = (isLoggedIn: boolean, accessToken: string) => {
    if (isLoggedIn) {
        axios.defaults.headers.common['Authorization'] = `Bearer ${accessToken}`;
    } else {
        delete axios.defaults.headers.common['Authorization'];
    }
}

export const register = (createUserDto: RegisterUserDto) => {
    return axios.post(apiUrl + '/accounts/register', createUserDto);
};