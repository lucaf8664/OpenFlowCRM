# Open Flow CRM
Introducing Open Flow CRM, the Open Source CRM designed with simplicity and efficiency at its core.

Open Flow CRM is an ASP.NET application with SQL Server, JWT authentication, and HTTPS using a self-signed certificate. 
This guide will walk you through the steps to set up and run the application.

## Prerequisites

- Docker installed on your machine
- Docker Compose installed on your machine
- Git installed on your machine
- SSL installed on your machine

## Setup

1. **Clone the repository**: Clone this repository to your local machine using `git clone <repository_url>`.

2. **Navigate to the project directory**: Use the command `cd <project_directory>` to navigate to the project directory.

## SQL Server Setup

Set the SQL Server password: In the ./DBImage/DockerFile file, replace `<mssql_password>` with your desired password in the `MSSQL_SA_PASSWORD` environment variable.
Repeat for file ./conf.env

JWT Setup
Set the JWT secret: In the ./conf.env file of the API project, replace <jwt_secret> with your desired JWT secret(at least 32 bytes).

HTTPS Setup
Generate a self-signed certificate: You can generate a self-signed certificate using OpenSSL. Please note that this certificate should only be used for development purposes.
bash
openssl req -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -days 365
openssl pkcs12 -export -out certificate.pfx -inkey key.pem -in cert.pem

now you should replace the password you chose for the certificate in the conf.env -> CERTIFICATE_PASSWORD

## Docker

docker-compose build
docker-compose up

Once reached the login page you will be able to login with admin/admin.