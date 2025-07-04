using BlueSkyApp.Components.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSkyApp.Components.Home;

partial class FollowingView : Component, ISlidingViewItem
{
    public string Title => "Following";

    private Action<MauiControls.ItemsViewScrolledEventArgs>? _scrolled;
    public void OnScrolled(Action<MauiControls.ItemsViewScrolledEventArgs>? action)
    {
        _scrolled = action;
    }

    public override VisualNode Render()
    {
        return Label("Following").Center();
    }
}
