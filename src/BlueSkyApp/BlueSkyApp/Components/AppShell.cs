//using BlueSkyApp.Components.Home;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BlueSkyApp.Components;

//partial class AppShell : Component
//{
//    public override VisualNode Render()
//    {
//        return FlyoutPage(
//        );
//    }
//    //public override VisualNode Render()
//    //{
//    //    return Shell(
//    //        FlyoutItem(
//    //            Tab("Home", "home_black.png",
//    //                ShellContent("Home", () => new HomePage())
//    //                //ShellContent("Discover", () => new DiscoverPage()),
//    //                //ShellContent("Following", () => new FollowingPage())
//    //            ),
//    //            Tab("Search", "search_black.png",
//    //                ShellContent("Search", ()=> new SearchPage())
//    //            )
//    //        )

//    //        .FlyoutDisplayOptions(MauiControls.FlyoutDisplayOptions.AsMultipleItems)

//    //    );
//    //}
//}

using BlueSkyApp.Components.Home;
using BlueSkyApp.Components.Profile;

enum PageType
{
    Home,

    Search,

    Messages
}

class MainPageState
{
    public PageType CurrentPageType { get; set; }

    public bool LoggedIn { get; set; }

}

class MainPage : Component<MainPageState>
{
    public override VisualNode Render()
    {
        return new FlyoutPage
        {
            State.LoggedIn ?
            DetailPage()
            :
            new LoginPage().OnLoggedIn(Login)
        }
        .Flyout(new FlyoutMenuPage()
            .OnPageSelected(pageType => SetState(s => s.CurrentPageType = pageType))
        );
    }

    VisualNode DetailPage()
    {
        return State.CurrentPageType switch
        {
            PageType.Home => new HomePage(),
            PageType.Search => new TodoListPage(),
            PageType.Messages => new RemindersPage(),
            _ => throw new InvalidOperationException(),
        };
    }

    void Login()
    {
        SetState(s => s.LoggedIn = true);
    }
}

class FlyoutMenuPage : Component
{
    private Action<PageType> _selectAction;

    public FlyoutMenuPage OnPageSelected(Action<PageType> action)
    {
        _selectAction = action;
        return this;
    }

    public override VisualNode Render()
    {
        return new ContentPage("Personal Organiser")
        {
            new CollectionView()
                .ItemsSource(Enum.GetValues<PageType>(), pageType =>
                    new Label(pageType.ToString())
                        .Margin(10,5)
                        .VCenter())
                .SelectionMode(Microsoft.Maui.Controls.SelectionMode.Single)
                .OnSelected<CollectionView, PageType>(pageType => _selectAction?.Invoke(pageType))
        };
    }
}

class ContactsPage : Component
{
    public override VisualNode Render()
        => new NavigationPage
        {
            new ContentPage("Contacts")
            {
                new Label("Contacts")
                    .VCenter()
                    .HCenter()
            }
        };
}

class TodoListPage : Component
{
    public override VisualNode Render()
        => new NavigationPage
        {
            new ContentPage("TodoList")
            {
                new Label("TodoList")
                    .VCenter()
                    .HCenter()
            }
        };
}

class RemindersPage : Component
{
    public override VisualNode Render() =>
        new NavigationPage
        {
            new ContentPage("Reminders")
            {
                new Label("Reminders")
                    .VCenter()
                    .HCenter()
            }
        };
}