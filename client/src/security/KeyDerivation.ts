import {Buffer} from "buffer";
import {pbkdf2Sync} from "pbkdf2";

export const derivativeKey = (password: string, salt: string, iterations: number = 100200): string => {
    const passwordBuffer = Buffer.from(password, "utf-8");
    const saltBuffer = Buffer.from(salt, "utf-8")
    const derivationKey = pbkdf2Sync(passwordBuffer, saltBuffer, iterations, 128, 'sha256');
    return derivationKey.toString("base64");
}

export const hashDerivationKey = (key: string, salt: string): string => {
    const keyBuffer = Buffer.from(key, "base64");
    const saltBuffer = Buffer.from(salt, "utf-8");
    const derivationKey = pbkdf2Sync(keyBuffer, saltBuffer, 1, 256, 'sha256');
    return derivationKey.toString("base64");
}