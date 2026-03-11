using LaCheminee.FnB.Adapters.Api;
using LaCheminee.FnB.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddFnbServices();
services.AddSingleton<ConsoleDemoRunner>();

using var provider = services.BuildServiceProvider();
var runner = provider.GetRequiredService<ConsoleDemoRunner>();
await runner.RunAsync();
