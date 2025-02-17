using BlueSkyApp.Components.Home;
using BlueSkyApp.Components.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSkyApp.Components;

class AppWindowState
{
    public bool LoggedIn { get; set; }
}


partial class AppWindow : Component<AppWindowState>
{
    public override VisualNode Render()
    {
        return Window(
            new MainPage()
        );
    }

    ContentPage RenderFlyoutMenu()
    {
        return ContentPage("MauiReactor Bluesky app"
            

        );
    }

    void Login()
    {
        SetState(s => s.LoggedIn = true);
    }
}
