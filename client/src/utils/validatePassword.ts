const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W])[\S].{8,256}$/;

export const validatePassword = (password: string) => {
    return password.match(regex);
}