# Web Forms Dependency Injection

This solution demonstrates dependency injection in Web Forms using the `SimpleInjectorPageHandlerFactory` described by [Steven van Deursen](https://twitter.com/dot_net_junkie) on his blog: https://cuttingedge.it/blogs/steven/pivot/entry.php?id=81.

## How To Use

1. Open up [`WebFormsDi.sln`](WebFormsDi.sln) in Visual Studio.
2. Run in IIS Express.
3. Observe that the messages are generated using the [`InjectedMessageService`](WebFormsDi/Models/InjectedMessageService.cs) through dependency injection rather than the [`DefaultMessageService`](WebFormsDi/Models/DefaultMessageService.cs) passed in through the default constructors.
