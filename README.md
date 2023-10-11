# Share2Connect

## Introduction 

Backend (API) of a Campus information sharing mobile application. The aim of this group project was to make communication and socialization among campus students easier. 

## Prerequisites

The client runs using npm and the api uses nuget packages. Api packages are already present in the
repository therefore no installation is required. However, some packages might be out-of-date and will need some update

To run the project follow the instructions below

## Install Node packages

You need NodeJS to run the command below. [Download NodeJS](https://nodejs.org/en/)

```bash
npm install 
```

## Run Client project

```bash
npm run dev
```

## Run Server [API]

1. Open Visual Studio
2. Open The Package manager console
3. Change connection string to your localhost server
4. Apply migrations to your database using the command below.

```bash
update-database 
```

This will create all necessary database tables using the initial migrations in the project.

5. Now build and run the solution. Your server should be up and running

## Technologies
<div id="badges">
  <img src="https://img.shields.io/badge/-C Sharp-green" />
  <img src="https://img.shields.io/badge/-.Net Core 6" />
  <img src="https://img.shields.io/badge/-Entity Framework" />
  <img src="https://img.shields.io/badge/-JWT" />
  <img src="https://img.shields.io/badge/-MS SQL Server" />
</div>