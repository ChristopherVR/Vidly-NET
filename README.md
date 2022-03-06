<p align="center"> 
  <img src="MoveShowcaseSPA/ClientApp/public/logo512.png" alt="React Logo" width="80px" height="80px">
</p>
<h1 align="center"> Movie System React Domain Driven Design Example </h1>
<h3 align="center"> A combination of libraries and frameworks integrated to showcase using React as the frontend and .NET as the backend </h3>  

</br>

<!-- TABLE OF CONTENTS -->
<h2 id="table-of-contents"> :book: Table of Contents</h2>

<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li><a href="#about-the-project"> ➤ About The Project</a></li>
    <li><a href="#prerequisites"> ➤ Prerequisites</a></li>
    <li><a href="#folder-structure"> ➤ Folder Structure</a></li>
    <li><a href="#setup"> ➤ Setup</a></li>
    <li><a href="#config"> ➤ Config</a></li>
  </ol>
</details>

![-----------------------------------------------------](https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png)

<!-- ABOUT THE PROJECT -->
<h2 id="about-the-project"> :pencil: About The Project</h2>

<p align="justify"> 
  This project aims to integrate different frameworks and libraries used (Create React App template with TypeScript and Microsoft's eShopContainer DDD project) to demonstrate a movie system database with basic CRUD functionality folowing code-with-mosh Mastering React course.
</p>

![-----------------------------------------------------](https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png)

<!-- PREREQUISITES -->
<h2 id="prerequisites"> :fork_and_knife: Prerequisites</h2>

[![made-with-react](https://img.shields.io/badge/-Made%20with%20React-blue)](https://reactjs.org/docs/create-a-new-react-app.html) <br>
[![Made with-dot-net](https://img.shields.io/badge/-Made%20with%20.NET-purple)](https://dotnet.microsoft.com/en-us/) <br>
[![build status][buildstatus-image]][buildstatus-url]

[buildstatus-image]: https://github.com/ChristopherVR/MovieSystem-React-DDD-Example/blob/main/.github/workflows/badge.svg
[buildstatus-url]: https://github.com/ChristopherVR/MovieSystem-React-DDD-Example/actions

<!--This project is written mainly in C# and JavaScript programming languages. <br>-->
The following open source packages are used in this project:
* Dapper
* gRPC
* .NET 6
* React
* Create-React-App
* React-Toastify
* Bootstrap
* MDB

![-----------------------------------------------------](https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png)

<!-- :paw_prints:-->
<!-- FOLDER STRUCTURE -->
<h2 id="folder-structure"> :cactus: Folder Structure</h2>
    ├───github
│   └───workflows
├───MoveShowcaseSPA
│   ├───ClientApp
│   │   ├───public
│   │   └───src
│   │       ├───components
│   │       │   └───common
│   │       ├───context
│   │       ├───interfaces
│   │       ├───services
│   │       └───utils
│   ├───Controllers
│   ├───Enums
│   ├───Models
│   ├───Pages
│   ├───Properties
│   ├───Protos
│   ├───Services
│   └───wwwroot
│       ├───css
│       └───js
├───Services
│   └───MovieSystem
│       ├───MovieSystem.API
│       │   ├───Application
│       │   │   ├───Behaviors
│       │   │   ├───Commands
│       │   │   ├───DomainEventHandlers
│       │   │   │   └───UserCreated
│       │   │   ├───Queries
│       │   │   └───Validations
│       │   ├───Infrastructure
│       │   │   ├───AutofacModules
│       │   │   └───Options
│       │   ├───Migrations
│       │   ├───Properties
│       │   ├───Protos
│       │   └───Services
│       ├───MovieSystem.Domain
│       │   ├───AggregatesModel
│       │   │   ├───GenreAggregate
│       │   │   ├───MovieAggregate
│       │   │   └───UserAggregate
│       │   ├───Events
│       │   ├───Exceptions
│       │   └───SeedWork
│       ├───MovieSystem.Infrastructure
│       │   ├───EntityConfigurations
│       │   ├───Extensions
│       │   └───Repositories
│       └───MovieSystem.UnitTests
└───Tests
    └───MoveShowcaseWeb.Tests

![-----------------------------------------------------](https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png)

<!-- SETUP -->
<h2 id="setup"> :floppy_disk: Setup</h2>
<p> 
Clone the git repo and install dependencies.

Navigate to the ClientApp for the React-app:
```
npm ci
```

You can then run following scripts for local development

```
npm run build  // builds the React app 

npm test  // run unit tests

npm lint  // check for any linting issues

```

Navigate to the MoveShowcaseSPA folder:
```
dotnet run watch // builds the project and enables hot-reload
```

Navigate to the Services\MovieSystem\MovieSystem.API folder
```
dotnet run watch // builds the project and enables hot-reload
```

</p>

![-----------------------------------------------------](https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png)

<!-- ROADMAP -->
<h2 id="config"> :dart: Config</h2>

<p align="justify"> 
For the MovieShowcaseSPA Web project the following app settings can be configured:

* **BypassAuthentication**: indicates whether authentication should be bypassed. Default user credentials will be used. Possible values are `true` or `false`.
* **Key**: Indicates the Auth Key for the JWT Token based Authentication that will be used. This is a required value and needs to be 128 bits long.
* **Audience**: Indicates the Audience and Issuer that will be used to valid JWT tokens. Example `http://localhost:7235/`.
* **Services:MovieSystem**: Indicates the URL for the MovieSystem gRPC client. Example `http://moviesystemapi`
* **Services:ReactAppURL**: Indicates the URL where the React-app is hosted. This is used for setting CORS policy. Example `http://localhost:7235/`.

</p>

![-----------------------------------------------------](https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png)

