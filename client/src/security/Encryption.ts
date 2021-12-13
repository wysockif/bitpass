import {AES, enc} from "crypto-js";

export const encryptPassword = (message: string, key: string): string => {
    return AES.encrypt(message, key).toString();
}

export const decryptPassword = (message: string, key: string): string => {
    const code = AES.decrypt(message, key);
    return code.toString(enc.Utf8);
}