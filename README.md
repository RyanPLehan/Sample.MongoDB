# Sample.MongoDB
## Sample C# application to help learn the basics of MongoDB as the data repository

## **Please Note: This sample application is still a work in progress!  This message will be removed once it is completed.**

#### Prior to using this application, I highly recommend the following
- [MongoDB University](https://learn.mongodb.com/) - General educational content.
- [MongoDB Training](https://learn.mongodb.com/catalog) - Provides learning paths for specific content (ie C#).
- [MongoDB Developer](https://www.mongodb.com/developer/) - Developer Center for language of your choice.

---

### Prerequisites
1. This is designed to use the [sample databases](https://www.mongodb.com/developer/products/atlas/atlas-sample-datasets/) that is provided by Mongo.  
   
2. If you are not using Mongo Atlas, you may need to install the following:
    -  MongoDB - For local installation, use the [Community Edition](https://www.mongodb.com/products/self-managed/community-edition).
    -  [Compass](https://www.mongodb.com/products/tools/compass) - GUI for MongoDB.
    -  [Mongosh](https://www.mongodb.com/docs/mongodb-shell/install/) - Mongo Shell for command line interaction to MongoDB.           
       Compass has Mongosh builtin
    -  Mongo [Database Tools](https://www.mongodb.com/try/download/database-tools) - Command line utility tools for working with a MongoDB deployment (ie, restore sample database to local instance).  

3. When connecting to a MongoDB you can use your network credentials (implicit) or specific user/password (explicit).   
   For explicit credentials, do the following to create sample users:
    -  Using Mongosh (CLI or within Compass), connect to database server.  
    - Switch to the **admin** database.  
        `use admin`
    -  Run the following command to create a user with Read/Write access.   
       ```
       db.createUser
       (
        {
            user: "sampleRW",
            pwd: "sampleRW_Password",
            customData : { description: "Special user with Read/Write role for Sample.MongoDB application"},
            roles: [
                { role: "readWrite", db: "sample_airbnb" },
                { role: "readWrite", db: "sample_analytics" },
                { role: "readWrite", db: "sample_geospatial" },
                { role: "readWrite", db: "sample_guides" },
                { role: "readWrite", db: "sample_mflix" },
                { role: "readWrite", db: "sample_restaurants" },
                { role: "readWrite", db: "sample_supplies" },
                { role: "readWrite", db: "sample_training" },
                { role: "readWrite", db: "sample_weatherdata" }
            ]
        }
       )
       ```   
    -  Run the following command to create a user with Read only access.   
       ```
       db.createUser
       (
        {
            user: "sampleRO",
            pwd: "sampleRO_Password",
            customData : { description: "Special user with Read only role for Sample.MongoDB application"},
            roles: [
                { role: "read", db: "sample_airbnb" },
                { role: "read", db: "sample_analytics" },
                { role: "read", db: "sample_geospatial" },
                { role: "read", db: "sample_guides" },
                { role: "read", db: "sample_mflix" },
                { role: "read", db: "sample_restaurants" },
                { role: "read", db: "sample_supplies" },
                { role: "read", db: "sample_training" },
                { role: "read", db: "sample_weatherdata" }
            ]
        }
       )
       ```   
    -  Run the following command to verify that the two users were created.   
       `db.system.users.find({user: /sample*/}, {_id : 1, user : 1, customData : 1})`  
       Your output should be as follows:  
       ```   
       {
         _id: 'admin.sampleRO',
         user: 'sampleRO',
         customData: {
            description: 'Special user with Read only role for Sample.MongoDB application'
         }
       }
       {
         _id: 'admin.sampleRW',
         user: 'sampleRW',
         customData: {
            description: 'Special user with Read/Write role for Sample.MongoDB application'
         }
       }
       ```  


---

### Sample.MongoDB C# Application
The C# application was written using .Net Core 8.  


#### It includes the following:
- Dependency Injection  
- Console Logging  
- Use of the native .Net MongoDB Driver   
   - As of 2023, Mongo released an Entity Framework driver.   
     However, it does not support the full capability as the native .Net MongoDB driver.  
     Thus it is not used within this application.


#### The following patterns are applied
- Command and Query Responsibility Segregation (CQRS)  
- Mediator for request/handler using MediatR  
  This allows for a vertical slicing instead of the layered approached
- Repository pattern


#### The solution breakdown as follows:
- Sample.MongoDB.Console - Console application (main entry point)  
- Sample.MongoDB.Domain - Domain/Core logic  
- Sample.MongoDB.Infrastructure - Logic for all MongoDB related interaction


#### The Repository and Mediator pattern breakdown as follows:
- Informational - Operations for querying of Server, Database, or Collection information (ie list of databases or indexes).
- Management - Operations for *simple* CRUD operations (Retrieval by primary key only).
- Search - Operations for specialized queries to retrieve data that would be outside of *simple* retrieval by primary key.
- Aggregation - Operations for specialized *aggregation* queries where the Mongo Aggregration Pipeline is used.  


#### Directory structure breakdown:
- Each sample database will have it's own directory as follows:
   - Name of the directory will **exclude** the prefix of *sample_*
   - Repository pattern directory breakdown structure as follows:  
     Management/Airbnb  
     Search/Airbnb  
   - Mediator pattern (Request/Handler) directory breakdown structure as follows:  
     Management/Airbnb/Requests  
     Management/Airbnb/Handlers  
     Search/Airbnb/Requests  
     Search/Airbnb/Handlers  
   - Models (POCO classes) directory breakdown structure as follows:  
     **Note:** Each directory will house all the models needed for that database  
     Models/Airbnb  
     Models/WeatherData


