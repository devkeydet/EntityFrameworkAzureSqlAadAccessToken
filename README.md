# EntityFrameworkAzureSqlAadAccessToken

## Overview
This is a sample ASP.NET MVC 5 web app, capable of being deployed to Azure App Service.  It demonstrates using Entity Framework 6 to connect to an Azure SQL Database using Azure Active Directory Authentication:
https://docs.microsoft.com/en-us/azure/sql-database/sql-database-aad-authentication

Specifically, it uses the token-based authentication approach describe in the article above.  This sample is based on both the guidance in the article above and this sample:
…and concepts in this sample code:
https://blogs.msdn.microsoft.com/sqlsecurity/2016/02/09/token-based-authentication-support-for-azure-sql-db-using-azure-ad-auth/

The key thing I focused on in the sample was to ensure the least amount of impact to an existing Etity Framework 6 codebase.  

The BlogContext class encapsulates everything necessary to get the access token from Azure AD and attach it to the underlying SqlConnection before the connection is opened.  It’s done in such a way that any code that theoretically already used BlogContext would just continue to work without modification.  The TokenHelper class does the work of getting the token from Azure AD based on values in the config file.  No username/password used.

## Geting Started

This code was authored with Visual Studio 2017.  In order to run it, you need to create two new files: **PrivateAppSettings.config** and **PrivateConnectionStrings.config**.  These files should be based on the sample **PrivateAppSettings.template.config** and **PrivateConnectionStrings.template.config** files and in the same location.  After you have done that, you should be able open the solution in Visual Studio without errors.  Next, you need to modify the values in those new files using your corresponding values.  If you have provided valid values, the app should build and run. 