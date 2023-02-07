# People Portal

# Tecnologies
- .NET 7 Minimal WEB API
- MSSQL Database

# Running the application
- Make sure that .NET 7 sdk (preferebly latest one) is installed on machine
1. Launch app using .net cli
- Open People.API in Terminal/PowerShell/CMD 
- Run command dotnet restore
- Run command dotnet run
2. Using Visual Studio 2022
- Open Project in Visual Studio
- Select desired profile to run
3. Using docker
- Make sure docker is installed on machine
- Open People API in Terminal/PowerShell/CMD 
- Run command docker build -t peopleapi .
- Run command docker run -it --rm -p 5000:80 --name peopleapi peopleapi

# Storage
1. Make sure sql server is running on machine
2. Change connection string in People API appsetings.json to point your local instance
3. DB creationg is automatic, but in every run all tables are truncateded and seeded, unless DB_CLEANUP_OPTIONS is set to no action in Properties/launchSettings.json

# Usage
1. Use swagger to send requests
2. import People API.postman_collection.json into postman and send requests