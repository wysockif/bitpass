import React, {useState} from 'react';
import PageTitle from "../PageTitile/PageTitle";
import {Button, Col, Form, FormGroup, Input, Label, ListGroupItem} from "reactstrap";


const Generator = () => {
    const [lowercase, setLowerCase] = useState<boolean>(true);
    const [uppercase, setUppercase] = useState<boolean>(true);
    const [digits, setDigits] = useState<boolean>(true);
    const [specialCharacters, setSpecialCharacters] = useState<boolean>(true);
    const [length, setLength] = useState<number>(18);
    const [generatedPassword, setGeneratedPassword] = useState<string>('');
    const [copied, setCopied] = useState<boolean>(false);


    const generatePassword = (ev: React.FormEvent<HTMLButtonElement>) => {
        ev.preventDefault();
        setCopied(false);

        if (!lowercase && !uppercase && !digits && !specialCharacters) {
            setGeneratedPassword('');
            return;
        }
        let alphabet = '';
        if (lowercase) {
            alphabet += 'abcdefghijklmnopqrstuvwxyz';
        }
        if (uppercase) {
            alphabet += 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
        }
        if (digits) {
            alphabet += "0123456789";
        }
        if (specialCharacters) {
            alphabet += "!@#$%^&*";
        }

        let password = '';
        for (var i = 0; i < length; i++) {
            password += alphabet.charAt(Math.floor(Math.random() * alphabet.length));
        }
        setGeneratedPassword(password);
    }

    const copyToClipBoard = () => {
        setCopied(true);
        navigator.clipboard.writeText(generatedPassword).then();
    }

    return (
        <div>
            <PageTitle title="Password generator"/>
            <div className="m-4 mt-4">
                <Form className="ms-2">
                    <FormGroup check inline>
                        <Input checked={lowercase} type="checkbox"
                               onChange={(event) => setLowerCase(event.target.checked)}/>
                        <Label check>
                            a-z
                        </Label>
                    </FormGroup>
                    <FormGroup check inline>
                        <Input checked={uppercase} type="checkbox"
                               onChange={(event) => setUppercase(event.target.checked)}/>
                        <Label check>
                            A-Z
                        </Label>
                    </FormGroup>
                    <FormGroup check inline>
                        <Input checked={digits} type="checkbox" onChange={(event) => setDigits(event.target.checked)}/>
                        <Label check>
                            0-9
                        </Label>
                    </FormGroup>
                    <FormGroup check inline>
                        <Input checked={specialCharacters} type="checkbox"
                               onChange={(event) => setSpecialCharacters(event.target.checked)}/>
                        <Label check>
                            !@#$%^&*
                        </Label>
                    </FormGroup>
                </Form>
                <Col md="6" xl="4" className="ms-2">
                    <Label check>
                        Length: {length}
                    </Label>
                    <Input type="range" className="form-range" id="customRange1" min="8" max="64" step="1"
                           defaultValue="14" onChange={(ev) => setLength(parseInt(ev.target.value))}></Input>
                </Col>
                <Button size="sm" color="primary" className="ms-2 mt-2"
                        onClick={(ev) => generatePassword(ev)}>Generate</Button>

                {generatedPassword && <div>
                    <div className="mt-3 p-2 d-inline-flex" onClick={() => copyToClipBoard()}
                         style={{cursor: "pointer"}}>
                        <ListGroupItem action className="rounded">
                            {!copied &&
                            <span style={{fontSize: "10px"}} className="text-muted">Click to copy to clipboard</span>}
                            {copied &&
                            <span style={{fontSize: "10px"}} className="text-muted">Copied to clipboard!</span>}
                            <div>
                                <h5><code className="text-danger">{generatedPassword}</code></h5>
                            </div>
                        </ListGroupItem>
                    </div>
                </div>}
            </div>
        </div>
    )
}

export default Generator;