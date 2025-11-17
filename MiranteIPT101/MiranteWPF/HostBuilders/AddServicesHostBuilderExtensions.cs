using LibraryDomain.Commands;
using LibraryDomain.Queries;
using LibraryFramework;
using LibraryFramework.Commands;
using LibraryFramework.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiranteWPF.Stores;
using MiranteWPF.ViewModels;

namespace MiranteWPF.HostBuilders;

public static class AddServicesHostBuilderExtensions
{
    public static IHostBuilder AddServices(this IHostBuilder host)
    {
        host.ConfigureServices((context, services) =>
        {
            var connectionString = context.Configuration.GetConnectionString("LibraryDatabase")
                ?? throw new InvalidOperationException("Connection string 'LibraryDatabase' not found.");

            services.AddSingleton(new LibraryDbContextFactory(connectionString));
            services.AddSingleton<ILibraryCommandService, LibraryCommandService>();
            services.AddSingleton<ILibraryQueryService, LibraryQueryService>();
            services.AddSingleton<LibraryStore>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>(s => new MainWindow
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });
        });

        return host;
    }
}
