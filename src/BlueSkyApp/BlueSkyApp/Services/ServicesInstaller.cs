using BlueSkyApp.Services.Implementation;
using CommunityToolkit.Maui;
using MauiReactor.HotReload;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSkyApp.Services;

static class ServicesInstaller
{
    public static MauiAppBuilder ConfigureAppServices(this MauiAppBuilder builder)
    {
        builder.Services.AddServices();
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
