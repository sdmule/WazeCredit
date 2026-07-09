//Singleton Lifetime

//Syntax to Register :     services.AddSingleton<>
//Should be used very carefully.
//Singleton service sends same instance for the life of the application.
//E.g. If you click on all view or link on a website, whenever an instance is requested it will send same object. It will change only when application restarts.


//Scoped Lifetime

//Syntax to Register :     services.AddScoped<>
//Not ideal for multi-threading
//Scoped service sends a new instance for each request.
//E.g.If you click on a view or link for that page load if instance is requested 10 times it will send same object!


//Transient Lifetime

//Syntax to Register :     services.AddTransient<>
//Always try to register a service as transient if unsure.
//Transient service sends a new instance every time it is requested.
//E.g. If you click on a view or link for that page load if instance is requested 10 times it will send 10 different objects!


//TryAddEnumerable()
//TryAddEnumerable() registers a service only if the same implementation type for that service type has not already been registered.
//Add this implementation to the collection only if that exact implementation isn't already present.
//Unlike TryAdd(), it is designed for scenarios where multiple implementations of the same interface are expected.

//TryAddEnumerable() is used when a service type can have multiple implementations. 
//It registers an implementation only if that specific implementation hasn't already been registered for the service type.
//This prevents duplicate registrations while still allowing different implementations of the same interface. 
//It is commonly used by the ASP.NET Core framework for components like authorization handlers, model validators, logging providers, and hosted services where IEnumerable<T> is injected.