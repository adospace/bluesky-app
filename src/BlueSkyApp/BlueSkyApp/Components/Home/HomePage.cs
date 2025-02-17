using BlueSkyApp.Components.Shared;
using Syncfusion.Maui.Toolkit.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSkyApp.Components.Home;

enum HomePageView
{
    Discover,

    Following
}

class HomePageState
{
    public HomePageView View { get; set; }

}
partial class HomePage : Component<HomePageState>
{
    public override VisualNode Render()
    {
        return //NavigationPage(
            ContentPage("Home",
                new SlidingView
                {
                    new VisualNode[]{ new DiscoveryView(), new FollowingView() }
                }
            );
        //);
        
        //return NavigationPage(
        //    TabbedPage(
        //        new DiscoveryPage(),
        //        new FollowingPage()
        //    )

        //)
        //.Title("Home");
    }

    

    //VisualNode RenderView(HomePageView view)
    //    => view switch
    //    {
    //        HomePageView.Discover => new DiscoveryView(),
    //        HomePageView.Following => new FollowingView(),
    //        _ => throw new NotSupportedException(),
    //    };
}
