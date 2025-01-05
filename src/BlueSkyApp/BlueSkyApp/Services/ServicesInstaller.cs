using BlueSkyApp.Services.Implementation;
using CommunityToolkit.Maui;
using MauiReactor.HotReload;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Storage;
using ReactorData.Maui;
using ReactorData.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSkyApp.Services;

static class ServicesInstaller
{
    const string _dbPath = "bluesky.db";

    public static MauiAppBuilder ConfigureAppServices(this MauiAppBuilder builder)
    {
        builder.Services.AddServices();


        builder.UseReactorData(services =>
        {
            services.AddReactorData(
                connectionStringOrDatabaseName: $"Data Source={_dbPath}"//,
                //configure: _ => _.Model<Todo>()
                );
        });


        return builder;
    }

    [ComponentServices]
    public static void AddServices(this IServiceCollection services)
    {
#if DEBUG
        services.AddLogging(configure => configure.AddDebug().AddFilter(level => level >= LogLevel.Debug));
#endif
        services.AddSingleton(sp => SecureStorage.Default);
        services.AddSingleton<IBlueSkyService, BlueSkyService>();

    }
}
