using BlueSkyApp.Resources.Styles;
using BlueSkyApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSkyApp.Components.Profile;

class LoginPageState
{
    public string Handle { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; } = true;

    public bool IsLoggingIn { get; set; }

    public bool IsInitializing { get; set; } = true;
}

partial class LoginPage : Component<LoginPageState>
{
    [Prop]
    Action? _onLoggedIn;

    [Inject]
    IBlueSkyService _blueSkyService;

    protected override void OnMounted()
    {
        base.OnMounted();
    }

    public override VisualNode Render()
    {
        return ContentPage(            
            Grid(
                Grid("64,*,Auto", "*",
                    RenderHeader(),

                    RenderBody(),

                    RenderFooter()
                )
                .BackgroundColor(ApplicationTheme.Colors.Semantic.BgCanvas)
                .Opacity(State.IsLoggingIn ? 0.1 : 1.0)
                .IsEnabled(!State.IsLoggingIn)
                .IsVisible(!State.IsInitializing)
                .GridRowSpan(3),

                VStack(spacing: 16,
                    ActivityIndicator()
                        .Height(60)
                        .Width(60)
                        .GridRowSpan(3)
                        .IsVisible(State.IsLoggingIn || State.IsInitializing)
                        .IsRunning(true),

                    Label(State.IsLoggingIn ? "Logging in..." : "Initializing...")
                        .ThemeKey(ApplicationTheme.Selector.Typo.LabelMdRegular)
                        .TextColor(ApplicationTheme.Colors.Semantic.FgMuted)
                )
                .Center()
            )
        )
        .OnAppearing(Initialize);
    }

    Button RenderFooter()
    {
        return Button("Log in")
            .ThemeKey(ApplicationTheme.Selector.Buttons.Primary)
            .IsEnabled(() => !string.IsNullOrWhiteSpace(State.Handle) && !string.IsNullOrWhiteSpace(State.Password))
            .Margin(16, 24)
            .GridRow(2)
            .OnClicked(Login);
    }

    static Grid RenderHeader()
    {
        return Grid(
            Label("Log in")
                .ThemeKey(ApplicationTheme.Selector.Typo.LabelLgBold)
                .TextColor(ApplicationTheme.Colors.Semantic.FgBase)
                .HorizontalTextAlignment(TextAlignment.Center)
                .VerticalTextAlignment(TextAlignment.Center)
            )
            .GridRow(0);
    }

    VStack RenderBody()
    {
        return VStack(spacing: 16,
            ApplicationTheme.TextField("Handler", State.Handle, newText => SetState(s => s.Handle = newText, false), placeholder: "@adospace.bsky.social"),

            ApplicationTheme.TextField("App Password", State.Password, newText => SetState(s => s.Password = newText, false), isPassword: true),

            Grid(
                Label("Remember sign in details")
                    .ThemeKey(ApplicationTheme.Selector.Typo.BodyMd)
                    .TextColor(ApplicationTheme.Colors.Semantic.FgSubtle)
                    .VerticalTextAlignment(TextAlignment.Center)
                    ,

                ApplicationTheme.Switch(State.RememberMe, newState => SetState(s => s.RememberMe = newState)).HEnd()
            )
        )
        .GridRow(1)
        .Margin(16);
    }

    async Task Initialize()
    {
        var result = await _blueSkyService.InitializeAsync();

        SetState(s => s.IsInitializing = false);

        if (result)
        {
            _onLoggedIn?.Invoke();
        }
    }

    async Task Login()
    {
        SetState(s => s.IsLoggingIn = true);

        //await Task.Delay(12000);

        var result = await _blueSkyService.LoginAsync(State.Handle, State.Password, State.RememberMe);

        SetState(s => s.IsLoggingIn = false);

        if (result)
        {
            _onLoggedIn?.Invoke();
        }
    }
}
