# Powerplant
The API endpoint will help to decide how much electricity various power plants should generate based on the expected energy demand (load) and the cost of different types of fuel. The goal is to meet the energy demand efficiently and at the lowest cost.

## Technologies used

- .NET 6
    - ASP.NET WebAPI
    - Newtonsoft.Json

- SOLID Principal

 ProductionPlanService is responsible for calculating the production plan. 
 The ProductionPlanController depends on this service through an interface IProductionPlanService. 
 This adheres to the Dependency Inversion Principle, as high-level modules (controller) depend on abstractions (interfaces) rather than concrete implementations. 
 The service is responsible for the single responsibility of calculating the production plan, following the Single Responsibility Principle. 
 The actual calculation logic is implemented in the CalculateProduction method within the service.

- Hosting
    - Docker (with compose)
      
## Architecture Overview
## The Powerplant API is developed using .Net Core Web API with Docker support.
<p align="center">
    <img alt="read before" src="https://user-images.githubusercontent.com/59339276/274673347-4a4be765-df48-4233-9b57-0e7d2e7811c3.png" />
</p>

## Steps to Run the Project
You can run the Powerplant project on any operating system. **Make sure you have installed docker in your environment.** ([Get Docker Installation](https://docs.docker.com/get-docker/))

Clone Powerplant repository and navigate to the **/Root** folder of the application where docker-compose.yaml file is located and then:

### If you want to run the Powerplant application in your Docker enviroment:

```
docker-compose up
```

### If you want to build the local images and run the Powerplant application in your Docker enviroment:

```
docker-compose -f docker-compose.yaml up --build
```

### You should see something like this when you run Docker Compose 

<p align="center">
    <img alt="read before" src="https://github.com/tanveerdotnet/powerplant/assets/59339276/4ddbd24d-fe63-4cd2-ac48-83a754c48ba5" />
</p>

### The application is configure to run on Port 8888 as below 

<p align="center">
    <img alt="read before" src="https://github.com/tanveerdotnet/powerplant/assets/59339276/b41f58e3-e4fa-4dad-8273-3bc7e832b34e.png" />
</p>


### If you want Visual Studio with F5 and debug experience:

You will need:

- Docker
- You will need at least Visual Studio 2022 and .NET 6.
- The latest SDK and tools can be downloaded from https://dot.net/core
- Right click on the "Powerplan" project and select "Set as Startup Project and hit F5

---
