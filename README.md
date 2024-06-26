# HRLeaveManagement

## Design Patterns in use

### Singleton

Setting logger instance into the static services
![image](https://github.com/rescatado182/HRLeaveManagement.Clean/assets/3597508/c80ec5a9-08e6-4a1e-9f9d-482729d47e5c)

Using logger in a singleton
![image](https://github.com/rescatado182/HRLeaveManagement.Clean/assets/3597508/e6b3bf1b-93f4-49c0-8d92-32568ef681a4)


### Adapter

 In this example, the LoggerAdapter<T> class adapt the IAppLogger<T> of the logger nuget external package.
 and from InfrastructureServicesRegistration class through ConfigureInfrastructureServices static method register the service.
 
 Creating through factory
![image](https://github.com/rescatado182/HRLeaveManagement.Clean/assets/3597508/b8b4df94-fdf3-4fe2-a87f-072936f88232)


 This allows the application using the logger instance on Application layer facilitating easy integration with minimal code. 
![image](https://github.com/rescatado182/HRLeaveManagement.Clean/assets/3597508/6be4a7ae-f306-47b7-8a56-644c907e827e)

