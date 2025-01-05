using BlueSkyApp.Components.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSkyApp.Components;

partial class AppShell : Component
{
    public override VisualNode Render()
    {
        return Shell(
            FlyoutItem(
                Tab("Home", "home_black.png",
                    ShellContent("Discover", () => new DiscoverPage()),
                    ShellContent("Following", () => new FollowingPage())
                ),
                Tab("Search", "search_black.png",
                    ShellContent("Search", ()=> new SearchPage())
                )
            )
            
            .FlyoutDisplayOptions(MauiControls.FlyoutDisplayOptions.AsMultipleItems)

        )
        ;
    }
}
