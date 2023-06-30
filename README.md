# Business Partners API
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
