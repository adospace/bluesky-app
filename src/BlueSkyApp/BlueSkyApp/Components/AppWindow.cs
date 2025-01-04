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
            State.LoggedIn ? 
            new AppShell() 
            : 
            new LoginPage().OnLoggedIn(Login)
            );
    }

    void Login()
    {
        SetState(s => s.LoggedIn = true);
    }
}
