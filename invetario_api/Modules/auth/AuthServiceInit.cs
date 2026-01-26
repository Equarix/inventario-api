using System;

namespace invetario_api.Modules.auth;

public class AuthServiceInit : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public AuthServiceInit(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
        await authService.registerAdmin();

        Console.WriteLine("Admin user initialized");
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
