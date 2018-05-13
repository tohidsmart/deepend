##  Welcome to Home of Cheque Application 

This Solution is structured into the following projects

- Business
       deepend.business
- Common
    deepend.common
- Data 
 deepend.data
 deepend.db
 - Entities
 deepend.entity
- API 
deepend.api
- UI 
deepend.ui
- Test
deepend.test
   
  I try to explain the purpose of each project briefly. 
### deepend.Business
It is responsible for calculation and performing business login of the application. In this solution it convert a decimal number ( cheque amount ) to its equivalent presentation.This layer is used by API Layer for data manipulation 
### deeepend.common
It provides common functionality to other projects. In this solution it provide database connection string to other project.
### deepend.data 
This project is the data access layer of the solution and talk to databases directly. It is responsible for simple CRUD functionalities and provides data functionalities to API layer only.
### deepend.db 
This is the database project of the solution. It contains SQL tables and stored procedures. 
  ### deepend.entity 
  This project holds the data model of the application. Entities such as cheques request and response are in this project 
  
 ### deepend.api 
 This is the API layer of the solution which exposes basic functionalities through REST APIs. other clients can subscribes to its endpoint and send or receive the data. It is implemented using ASP.Net Web API 

### deepend.ui 
This is the presentation layer of the application implemented in ASP.Net MVC and bootstrap.This layer consumes the API layer as a client

### deepend.test 
This is the test project of the solution contains Integration test for API, Business and Data components 
