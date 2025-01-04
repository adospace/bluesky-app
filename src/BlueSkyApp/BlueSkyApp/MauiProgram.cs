using BlueSkyApp.Components;
using BlueSkyApp.Components.Profile;
using BlueSkyApp.Resources.Styles;
using BlueSkyApp.Services;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;

namespace BlueSkyApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiReactorApp<AppWindow>(app => app.UseTheme<ApplicationTheme>())
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionToolkit()
            .ConfigureAppServices()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("PlusJakartaSans-Regular.ttf", "PlusJakartaSansRegular");
                fonts.AddFont("PlusJakartaSans-SemiBold.ttf", "PlusJakartaSansSemiBold");
                fonts.AddFont("PlusJakartaSans-Medium.ttf", "PlusJakartaSansMedium");
                fonts.AddFont("PlusJakartaSans-Bold.ttf", "PlusJakartaSansBold");
            });

        CustomizeNativeControls();

        return builder.Build();
    }

    static void CustomizeNativeControls()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
        });
    }
}
