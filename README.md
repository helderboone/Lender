# Lender

## About this Project

The idea of the App is:

_"Application to manage your games which got borrowed by your friends
"._

Email-me: helder.boone@gmail.com

Connect with me at [LinkedIn](https://www.linkedin.com/in/helder-boone-493a7784).

## Functionalities

- Login
	- Loggin

- Game 
	- Create a game and can select a photo
    - Edit your game
    - List games

- Friend 
	- Create a friend and can select a photo
    - Edit your friend
    - List friend

- Loan 
	- Select a game and friend to lend a game
  - List your game loans

## Getting Started

### Installing

**Cloning the Repository**

```
$ git clone https://github.com/helderboone/Lender.git

$ cd Lender
```

**Installing dependencies**

```
$ cd Lender.API && dotnet build
```

```
$ dotnet run
```

```
$ cd Lender-SPA && npm install
```

```
$ npm start
```


## Docker support
    Make sure both docker engine and docker-compose are installed

```
$ git clone https://github.com/helderboone/Lender.git

$ cd Lender/docker

$ docker-compose up -d
```
	
## Built with

### Backend
- [NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) - Install .NET Core 3.1 
- [MediatR](https://github.com/jbogard/MediatR) - MediatR
- [AutoMapper](https://automapper.org/) - AutoMapper
- [EntityFrameworkCore](https://docs.microsoft.com/pt-br/ef/core/) - EntityFramework Core 3.1
- [JWT Token](https://jwt.io/) - JWT
- [Identity](https://docs.microsoft.com/pt-br/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-3.1) - Identity


### Frontend
- [Angular](https://angular.io//) - Create Angular app

## Support tools

- [cloudinary](https://cloudinary.com/) - Store the Images

## Disclaimer
 - The default user and password below to access the application
 
```
email: joao@email.com
password: P@ssw0rd123
```