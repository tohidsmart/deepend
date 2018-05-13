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
It is responsible for calculation and performing business logic of the application. In this solution it convert a decimal number ( cheque amount ) to its equivalent presentation.This layer is used by API layer for data manipulation 
### deeepend.common
It provides common functionalities to other projects. In this solution, database connection string to other project is provided by this project.
### deepend.data 
This project is the data access layer of the solution and talks to database directly. It is responsible for simple CRUD functionalities and provides data functionalities to API layer only.
### deepend.db 
This is the database project of the solution. It contains SQL table and stored procedures. 
  ### deepend.entity 
  This project holds the data model of the application. Entities such as cheques request and response are in this project 
  
 ### deepend.api 
 This is the API layer of the solution which exposes basic functionalities through REST APIs. other clients can subscribe to its endpoints and send or receive  data. It is implemented using ASP.Net Web API 

### deepend.ui 
This is the presentation layer of the application implemented in ASP.Net MVC and bootstrap.This layer consumes the API layer as a client

### deepend.test 
This is the test project of the solution contains Integration test for API, Business and Data components 


## Best practices and patterns used in this task 
- Dependency injection through constructor and  IOC container. I used SimpleInjector for IOC container 
-  Separation of concerns through multi-layers application patterns 
- Singleton design pattern for memory intensive resources like HttpClient 
- Programming against Interface 
- Single Responsibility through various projects and separate classes which promotes extend-ability  
- ORM for performing CRUD  with database . I used Dapper for the ORM 
- Integration Testing. I used Nunit and NSubsitute for testing framework 
