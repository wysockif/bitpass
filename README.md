# BitPass
[![Website shields.io](https://img.shields.io/website-up-down-green-red/http/shields.io.svg)](https://bitpass-app.herokuapp.com)

<a href="https://bitpass-app.herokuapp.com" target="_blank">https://bitpass-app.herokuapp.com</a>

<a href="https://bitpass-server.herokuapp.com" target="_blank">https://bitpass-server.herokuapp.com</a>

### General Information

Bitpass is an open source password manager opereting on a zero-knowledge security model. 

The project consists of a RESTful web service with Spring Boot & (responsive) single page application with React.

![Vault](img/Vault.png)


---
### Used Technologies

1. Database:
- `PostgreSQL 13.2`
- `Heroku PostgreSQL 13.5`

2. Back-end:
- `C# 9`
- `.NET 5`

3. Front-end:
- `Typescript 4.1.2`
- `React.js 17.0.2`

4. DevOps and deploymend:
- `Heroku`
- `Docker 20.10.11`
- `Docker Compose 2.2.1`

---

### Security
![Diagram](img/diagram.svg)

---
### Requirements

To run the application locally you need:

* `Docker 19.03.13`;
* `Docker-compose  1.25.0`;

Supported browsers:
<p float="left">
    <img src="https://imgur.com/3C4iKO0.png" width="32" height="32">
    <img src="https://imgur.com/ihXsdDO.png" width="32" height="32">
    <img src="https://imgur.com/vMcaXaw.png" width="32" height="32">
    <img src="https://imgur.com/nSJ9htU.png" width="32" height="32">
    <img src="https://imgur.com/ENbaWUu.png" width="32" height="32">
    <img src="https://imgur.com/z8yjLZ2.png" width="32" height="32">
</p>


The project uses the latest version (17.0.2) of React. You can refer to the  <a href="https://reactjs.org/docs/react-dom.html#browser-support" target="_blank">React documentation</a> for more information about supported browsers.

---
### Setup & usage

To use the deployed app <a href="https://bitpass-app.herokuapp.com" target="_blank">click here</a>.

To run it locally follow the instructions:

0. Clone this repo to your desktop:

    ```sh
    git clone git@github.com:wysockif/noticeboard.git
    ```
1. Database:

    Change your directory to `bitpass/database` and enter:

    ```sh
    docker-compose up database
    ```

    It will set up a database (PostgreSQL) server on port 5432.

2. Back-end:

    Create `appsettings.Development.json` file as in <a href="https://github.com/wysockif/bitpass/blob/main/server/src/Api/appsettings.Example.json" target="_blank">appsettings.Example.json</a>.

    Change your directory to `bitpass\server\src\Api` and enter:

    ```bash
    dotnet dev-certs https --trust
     ```
    
    then:

    ```bash
    dotnet run
     ```

     It will run the back-end app on port 5001 via https. Trust the certificate in your browser.

3. Front-end: 
    Change your directory to `bitpass\client` and enter:
    ```bash
    yarn install
    ```
    then: 

    ```bash
    yarn start
    ```

    It will run the front-end app on port 3000 via https. Trust the certificate in your browser.

---
### Screenshots

* Sign up

![Vault](img/SignUp.png)

* Sign in

![Vault](img/SignIn.png)

* Request reset your password

![Vault](img/RequestResetPassword.png)

* Verify your master password

![Vault](img/VerifyMasterPassword.png)

* Add a new item

![Vault](img/AddNewItem.png)

* Vault

![Vault](img/Vault.png)

* Reveal the password

![Vault](img/RevealingPassword.png)

* Password generator

![Vault](img/PasswordGenerator.png)

* Active sessions

![Vault](img/ActiveSessions.png)

* Account activities

![Vault](img/AccountActivities.png)

* Settings

![Vault](img/Settings.png)

---
### Motivation & Project Status

Bitpass was created as my student project during the third year of studying Computer Science. It was my individual project for Data Protection in IT Systems subject. The idea of "zero knowledge" policy was inspired by Bitwarden and Lastpass.

The project was completed successfully.

Duration time: 23.11.2021 - 18-01-2022

---
### License
Usage is provided under the [MIT License](http://opensource.org/licenses/mit-license.php). See LICENSE for the full details.
