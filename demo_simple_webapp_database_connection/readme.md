# Simple MVC Core Application with EF Core (Code First)

Simple MVC Core application that uses Entity Framework Code First to create a connection and simple interaction with an Azure SQL Managed Instance. You could use it to prove a concept showing how to connect a regular application to Managed Instances under a specific VNet on Azure.

## Pre-requisites

In order to run this application properly, you will need:

**.NET Core 2.2 (+)**<br />
Download and install: [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)

## Usage

Do navigate to project's root directory and from there run the following.

```bash
dotnet restore
dotnet run
```

Then, go to your prefered browser and type the following. You should be able to see a warning from your browser telling you that there is a issue with the certificate of that website. This is perfectly normal, once we don't have a valid certificate for this app. Just bypass the message and "Go to the website".
```
https://localhost:5001/
```

If you want to use it connected with Azure SQL Managed Instance, please go ahead and look at the links below. There are some technical aspects you need to go through to get there.

* SQL Database Manage Instances Capabilities: [https://docs.microsoft.com/en-us/azure/sql-database/sql-database-managed-instance](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-managed-instance)

* How to create a SQL Managed Instance: [https://docs.microsoft.com/en-us/azure/sql-database/sql-database-managed-instance-get-started](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-managed-instance-get-started)

* How to restore an existing database to SQL Managed Instance: [https://docs.microsoft.com/en-us/azure/sql-database/sql-database-managed-instance-get-started-restore](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-managed-instance-get-started-restore)

* How to configure a VPN P2S from your administrative machine to Managed Instance: [https://docs.microsoft.com/en-us/azure/sql-database/sql-database-managed-instance-configure-p2s](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-managed-instance-configure-p2s)

Enjoy!
