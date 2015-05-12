# webapi-setup-template
A simple template for a setup class for WebApi projects. The intention is that all site setup and config is managed through classes dervived from this (apart from the generation of the IDependencyResolver) which would be used both to configure the HttpConfiguration object from GlobalConfiguration.Configuration property, and a HttpConfiguration object created for the purpose of end to end in memory testing using something like the [WebApi.Testing](https://github.com/jchannon/WebAPI.Testing) project. 

To enable the end to end in memory testing scenario, you would create a class that built the IDependencyResolver instance. You would write this class so that dependencies on types that write to database or external services are configured thorugh virtual methods. For the in memory testing you would create a derivative of this class and override the methods for building external services so that you can specify TestDoubles and modify their behaviour for different tests. For example:

```csharp
public class DependencyResolverBuilder
{
  public IDependencyResolver Build()
  {
    var container = new MyContainer();
    ConfigureRepositories(container)
    
    return new MyDependencyResolver(container);
  }
  
  protected virtual void ConfigureRepositories(MyContainer container)
  {
    container.Register<IMyDataRepository, MySqlDbDataRepository>()
  }
}

public class TestingDependencyResolverBuilder : DependencyResolverBuilder
{
  public readonly MyMockRepository MockRepo;

  public TestingDependencyResolverBuilder(MyMockRepository mockRepo)
  {
    MockRepo = mockRepo;
  }
  
  protected override void ConfigureRepositories(MyContainer container)
  {
    container.RegisterInstance<IMyDataRepository>(MockRepo);
  }
}
```

The DependencyResolver builder can expose a property for your Test Double so you can inspect what happened to it if it is a Spy or Mock. 
