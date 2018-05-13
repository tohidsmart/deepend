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

### Projects Structure 
![](https://github.com/tohidsmart/deepend/blob/master/strcuture.JPG)

### Integration Tests Result
![](https://github.com/tohidsmart/deepend/blob/master/test.JPG)

# How To Run This project locally 

1. First Publish the "deepend.db" project to SQL Server 
2. Right click on the project and Select Publish 
3. Click on Edit Button and Select the desired Server 
4. Give a name to database 
5. Click Publish 
6. After Publish is successful, please replace the connection string values in "Web.Config" of "deepend.api" project  at line 14 
   - Assign to  "Data Source" the value of SQL server name 
   -  Assign to "Initial Catalog" the name of the database you provided in step 4 
   - Assign to "User Id" and "Password" the user name and Password of your SQL server 
7. Start the Project in Visual Studio 

# NOTE:
If you wish to run the Integrations test, you have to copy the same connectionstring from "deepend.api" into App.Config of "deepend.test" project


### When the project starts, it will load the main page like below 

### Main Page 
This page will display all the cheques in the database ordere by data and time descending 

![](https://github.com/tohidsmart/deepend/blob/master/Home.JPG)

## Add New Cheque 
By clicking on Add new Button ,you will be redirected to another page which you can create a new cheque 

![](https://github.com/tohidsmart/deepend/blob/master/addnew.JPG)

## view Cheque Details 
By clicking on any Cheque amount in the main page table , you will be redirected to view cheque detail page. It will show the cheque amount in letters with some extra details 

![](https://github.com/tohidsmart/deepend/blob/master/details.JPG)

