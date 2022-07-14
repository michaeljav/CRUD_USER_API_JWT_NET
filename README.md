
**Exercise Instructions**

For this exercise, we would require .net 4.7.2 WebAPI2 framework.

Deliver a restful service that provides a registration,

1. login 
1. crud services for a user. 
1. Fields: User will have to support 
   1. first name, last name, phone, email, and address of street, city, state, zip.
1. We wish to see correct use of 
   1. http verbs,
   1. ` `validation,
   1. ` `exception handling, 
   1. jwt, 
   1. and. user responses for successful and error responses 
   1. Create code structure for 
      1. controllers,
      1. ` `logic,
      1. ` `and data, 
1. and unit tests for at least the logic. 

To submit this exercise, please create a free repository on GitHub and share the link to this repository with us. You can create an account via this link: <https://github.com/michaeljav/NS804.git>



**Solution:**

**TECHNICAL SPECIFICATIONS FOR DEVELOPMENT**

1. Data Base
   1. Sql Server Management Studio v17.4
   1. Microsoft SQL Server 2016 (RTM) - 13.0.1601.5 (X64)   Apr 29 2016 23:23:58   Copyright (c) Microsoft Corporation  Enterprise Edition (64-bit) on Windows Server 2016 Standard 6.3 <X64> (Build 14393: ) (Hypervisor) 


1. Rest API 
   1. Visual Studio Community 2017 Version 15.9.23
   1. Framework: .NET 4.7.2 WebAPI2
   1. EntityFramework 6
   1. EntityFrameworkCore.Tools 5.0.8
   1. Tokens.Jwt, Version=6.12.2.0
   1. Postman collection  examples v2.1









**STEPS TO START THE PROJECT**

1. **Data Base**
   1. Run the following Scripts
      1. Script  to create Data Base, Table, and Insert data for user testing: 1.DataBaseScript \ CreateDataBaseWithDataExample.sql
      1. **“User”**: mjavier |  “**Password”**: 187b7b2514961e1141b6eef7f70f355c
      1. **“Note”**: The password is encrypted with MD5
1. **Rest API** 
   1. Insert Database Connection Credentials (“**Server, User, Password”**) in API, located in file “**Web.config”** in the tag “**connectionStrings”** . 
   1. Run Rest API service located in folder: **…\** **WebAPI2**
   1. EndPoint List.- Can be tested by POSTMAN application
      1. **GET,**  List of registered users. you don't need to authenticate with jwt token: http://localhost:1202/api/user/all
      1. **GET,**  Search for a user by id. you don't need to authenticate with jwt token: <http://localhost:1202/api/user/1>
      1. **POST,**  Create a user. you don't need to authenticate with jwt token: http://localhost:1202/api/user
         1. **Body** à  **raw:** 

{



`  `"use\_UserName":  "mjavier",

`  `"use\_Password": "187b7b2514961e1141b6eef7f70f355c",

`  `"use\_FirstName": "Michael",

`  `"use\_LastName": "Javier Mota",

`  `"use\_Phone": null,

`  `"use\_email": null,

`  `"use\_AddressOfStreet": null,

`  `"use\_City": null,

`  `"use\_State": null,

`  `"use\_Zip": null,

`  `"use\_IsActive": true

}

1. **POST,** Perform authentication to obtain the **TOKEN JWT** : <http://localhost:1202/api/login/authenticate>
   1. **Body** à  **raw:** 

{

`    `"username": "mjavier",

`    `"password": "187b7b2514961e1141b6eef7f70f355c"

}

1. **PUT,**  to modify the user. you need to authenticate with jwt token: http://localhost:1202/api/user/6
   1. **Id User …** api/user/<6> 
   1. **Body** à  **raw:** 

{



`  `"use\_UserName":  "mjavier",

`  `"use\_Password": "e10adc3949ba59abbe56e057f20f883e",

`  `"use\_FirstName": "Modificado",

`  `"use\_LastName": "Amador",

`  `"use\_Phone": 232323,

`  `"use\_email": "michaeljaviermota@gmail.com",

`  `"use\_AddressOfStreet": "street",

`  `"use\_City": "city",

`  `"use\_State": "state",

`  `"use\_Zip": "zip",

`  `"use\_IsActive": true

}

1. Headers
   1. “**KEY**”: Authorization     “**VALUE**”:<TOKEN GENERADO EJ: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VJZCI6IjMiLCJuYmYiOjE2MjgyNjA4NTIsImV4cCI6MTYyOTk4ODg1MiwiaWF0IjoxNjI4MjYwODUyfQ.NYwVx56O9fcOb159pYHN6\_h5Xr6OrE183H4CyJ1vtIQ>

1. **DELETE,** Delete User: http://localhost:1202/api/user/6
   1. **Id User …** api/user/<6> 
   1. Headers
      1. “**KEY**”: Authorization     “**VALUE**”:<TOKEN GENERADO EJ: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VJZCI6IjMiLCJuYmYiOjE2MjgyNjA4NTIsImV4cCI6MTYyOTk4ODg1MiwiaWF0IjoxNjI4MjYwODUyfQ.NYwVx56O9fcOb159pYHN6\_h5Xr6OrE183H4CyJ1vtIQ>



