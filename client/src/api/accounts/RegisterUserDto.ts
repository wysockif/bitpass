export interface RegisterUserDto {
    username: string,
    email: string,
    password: string,
    "encryptionKeyHash": string
}
