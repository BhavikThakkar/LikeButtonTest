CORE Structure 


LikeTest.Services 
	Services 
	AutoMapperResolver
	
LikeTest.API
	Controllers
	Installers
		CORS
		DB
		Security
		Services
		Swagger
	Configuration
	Mappers
	HTTPServices


LikeTest.Data
	ModelBuilder
Repositories
 

LikeTest.Domain
	IRepositories 
	IServices 
	DB
	Model
	Entities 
		Entity1
			EntityResource
		
EntityQuery
			EntityQueryResource
			EntitySaveResource

			EntityResponse
			EntityValidator
	Generic
		Query
		Resource
		Response
LikeTest.Services 
	
Project Purpose 
This project is about writing Business entities services - for DB operations it will use created repositories. 

Project Dependencies
	Data
	Domain 

Input 
	Services need repositories to interact with data and unit of work to commit operation. 

Access
	Services 
	Repositories 
	Entity 
DB

Major Components
Services - Services need repositories to interact with data and unit of work to commit operation.

Mapper Resolver - Resolvers are needed to deal with Custom data type modeling in automapper, specifically one to one relationship.



LikeTest.Domain 
	
Project Purpose 
This project is about managing models for query, response, entity, repositories, services etc.

Project Dependencies
	Domain 
Input 
	NA
Access
	Entity 
DB
Major Components
DB- these are the models identical to database tables.

IRepositories - Interface of repositories project needs for each entity. So every entity will have one repository defined, however to reduce the same code, repositories are extended with base repositories - all basic features will be written in generic -> base repository. 

IServices - Interface of service project needs for each entity. So every entity will have one service defined.

Model - All Schema models (POCO) defined within it categorised mainly in entities and generic. For each entities there will be basic POCOS generated for different purposes like 
Query - This is basic filter criteria  design for any entity which will be used by service to fetch data. 
Query Resource - This is scope which allows the user to specify filter criteria for any entity. Prior controller passes query to services, it converts resources in the actual internal class. 
Entity Resource - this is a model of database entity.
Entity Response - this is a possible response supported by each entity on various possibilities. 
Entity Validator - this is class which can validate entity attributes along with values prior it gets sent to services. 
Entity Save Resource - this class will allow users to pass values to insert / update.

LikeTest.Data 
	
Project Purpose 
This project is about writing repositories & entity builders - where all validation & db constraints can be placed.

Project Dependencies
	Domain 

Input 
	Data needs access to DB POCO created under domain project along with specification being mentioned in the interface of the repository for each entity. 
Access
	API

Major Components
Entity Builder - fluent syntax of every table creation along with constraints like indexes, primary key, foreign key, relationship, attribute validation etc. 

Repositories - each entity holds one repository class extended by base repository for basic operations, having specific repositories for each entity give scope to extend any services for specific entities if needed.




LikeTest.API 
	
Project Purpose 
This project is about writing a start up definition of project, instantiating services and writing public facing services via controller.

Project Dependencies
	Domain 
	Data
	Service

Input 
	API needs access to Domain, Data & services to give POCO structure to API mainly responsible for getting input from the user, processing it via Services and return response via domain defined responses. 
Access
	Any Client. (Angular / react / jQuey / Vue / razor)

Major Components
Controller - public interface class with rest syntax defining well input and output structure. 

Hub- Socket communication like signalr classes - to notify real time communication 

	Installers - Initialization logic for each service and injecting it into app startup containers. 
	
	Mapper - Conversion between Internal & External entities including input & output services.

Migrations - Create DB on the basis of defined entities in domain class. 


LikeTest.Web 
	
Project Purpose 
This project is about writing a UI of web applications which can consume APIs underneath to communicate with services. 

Project Dependencies
	--

Input 
	--
Access
	--
Major Components
Depends on framework used but mainly front end services & views. 







Like Test Specific 

How to run Project:

From Solution set multiple projects in start, as it will require api as well as web both running to work.




Modify Appsettings.Json for Connection string - MYSQL 



Access API with Swagger 



Access Web 







How Like Mechanism is working:

We can have any front end framework rendering UI, currently i have taken a simple .NET Core Razor page with blank div.





It's referencing several javascript libraries like jQuery,signalr & like.js. First 2 libraries are third party libraries where last one is for main custom logic like we wrote. 

IN Like.Js 

First it sets up a connection with API Signalr Endpoint, if it fails due to any reason in the first attempt then it will keep trying to connect every 5 seconds, this is common practise for any websocket connection.
It calls endpoint getlist to get post data along with like count and render UI with list of posts. THIS IS NOT THE BEST WAY TO RENDER UI, ANY TEMPLATING SERVICE SHALL BE USED. but for time being as it's a small set of data i have used direct jquery and static html.
It has 2 access points of Hub
Subscriber - updateLikeCount
To get updates from the server after any access of PostLikes , this will make a new count for all clients who are on that page without refreshing the UI. 
Invoke - like
To Update Likes on Post.
