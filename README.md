# Business Partners API
***note** The name myfirstProject was a default name on my vs and changing the name created errors. </br>
I left the name but in the middle fixing it, but this is not my first project!! </br>

This project provides a Web API for managing business partners. </br>
It is built using C# with .NET 6 and SQL Server. The API supports various operations, </br>
including login, retrieving items and business partners, adding, updating, and deleting documents </br>

# Features
**Login**: Authenticate users and generate a valid secret key for further operations. </br>
**ReadItems**: Retrieve items from the database with optional filtering by any field. </br>
**ReadBusinessPartners**: Retrieve business partners from the database with optional filtering by any field. </br>
**AddDocument**: Create new documents in the SaleOrders or PurchaseOrders tables along with their respective lines and comments. </br>
**UpdateDocument**: Update existing documents in the SaleOrders or PurchaseOrders tables along with their respective lines and comments. </br>
**DeleteDocument**: Delete existing documents from the SaleOrders or PurchaseOrders tables based on the document type. </br>
**GetDocument**: Retrieve a specific document from the SaleOrders or PurchaseOrders tables based on the document type and ID. </br>

# Prerequisites
.NET 6 SDK </br>
SQL Server </br>
Visual Studio or Visual Studio Code (optional) </br>

# Getting Started
1. Clone the repository: </br>
Copy code : </br>
git clone [https://github.com/your-username/business-partners-api.git](https://github.com/EsBien/BusinessPartners-.git) </br>
2. Open the project in your preferred IDE (e.g., Visual Studio) or use the command line. </br>
3. Configure the database connection by updating the appsettings.json file with your SQL Server connection details. </br>
4. Run the project to start the API server. The Swagger UI should be accessible at http://localhost:5000/swagger. </br>
5. Use the Swagger UI or any API testing tool (e.g., Postman) to interact with the API endpoints. </br>

# Error Handling
This project includes error handling middleware that catches exceptions and returns appropriate error responses. </br>
Errors are logged using NLog, and the details can be found in the log files. </br>

# Authentication and Authorization
To perform certain operations, authentication is required.</br>
The API generates a valid secret key upon successful login, which needs to be included in subsequent requests as a token in the Authorization header. </br>

# Validation and Defaults
The API enforces several validations and applies default values for certain fields. </br>
These include checking document types, active business partners, document line presence, active items, and more. </br>

# Database Migration
The project uses Entity Framework Core for database interaction. </br>
To set up the initial database schema and seed data, run the following command in the Package Manager Console: </br>
"Server=<yourConnectionStrings>;Database=<dbName>;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=Yes;" </br>
