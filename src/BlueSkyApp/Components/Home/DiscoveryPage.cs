using BlueSkyApp.Components.Shared;
using BlueSkyApp.Framework;
using BlueSkyApp.Resources.Styles;
using BlueSkyApp.Services;
using CommunityToolkit.Maui.Core.Extensions;
using FishyFlip.Lexicon.App.Bsky.Feed;
using FishyFlip.Lexicon.Fyi.Unravel.Frontpage;
using FishyFlip.Models;
using MauiReactor.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSkyApp.Components.Home;


partial class DiscoveryPage : Component
{
    public override VisualNode Render()
    {
        return ContentPage("Discovery",
            new DiscoveryView()
        );
    }
}
