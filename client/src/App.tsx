import {useEffect, useState} from "react";
import {derivativeKey, hashDerivationKey} from "./security/DerivationKey";
import {decryptPassword, encryptPassword} from "./security/Encryption";

function App() {
    const [key, setKey] = useState<string>("");
    const [hash, setHash] = useState<string>("");
    const [encryptedMessage, setEncryptedMessage] = useState<string>("");
    const [decryptedMessage, setDecryptedMessage] = useState<string>("");

    useEffect(() => {
        setKey(derivativeKey("Password", "salt"));
    }, []);

    useEffect(() => {
        if (key) {
            setHash(hashDerivationKey(key, "salt"));
            setEncryptedMessage(encryptPassword("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", key));
        }
    }, [key]);

    useEffect(() => {
        if (key && encryptedMessage) {
            setDecryptedMessage(decryptPassword(encryptedMessage, key));
        }
    }, [encryptedMessage, key]);

    return (
        <div className="App">
            <div>{key}</div>
            <div>{hash}</div>
            <div>{encryptedMessage}</div>
            <div>{decryptedMessage}</div>
        </div>
    );
}

export default App;
